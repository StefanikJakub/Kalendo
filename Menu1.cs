using Microsoft.Toolkit.Uwp.Notifications;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kalendar
{
    public partial class Menu1 : Form
    {
        private readonly string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private int mesiac, rok;
        private Dictionary<int, List<CalendarEvent>> udalostiJednorazové;
        private Dictionary<int, List<CalendarEvent>> udalostiOpakujúceSa;
        private static bool upozornenieZobrazené;
        private List<UserControlDays> cachedDayControls = new List<UserControlDays>();
        private ConcurrentDictionary<DateTime, List<CalendarEvent>> eventCache = new ConcurrentDictionary<DateTime, List<CalendarEvent>>();

        private class CalendarEvent
        {
            public string Text { get; set; }
            public bool IsRecurring { get; set; }
        }

        public Menu1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            udalostiJednorazové = new Dictionary<int, List<CalendarEvent>>();
            udalostiOpakujúceSa = new Dictionary<int, List<CalendarEvent>>();
            this.MouseDown += Menu1_MouseDown;
        }

        private async void Menu1_Load(object sender, EventArgs e)
        {
            DateTime teraz = DateTime.Now;
            mesiac = teraz.Month;
            rok = teraz.Year;

            if (string.IsNullOrEmpty(Session.UserId) || !Session.Prihlaseny)
            {
                MessageBox.Show("Session is not initialized correctly!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            AktualizovaťTextLBDATE();
            await NačítaťUdalostiAsync();

            if (!upozornenieZobrazené)
            {
                SkontrolujUdalostiNaDnes();
                upozornenieZobrazené = true;
            }

            await AktualizovaťKalendárAsync();
            LBDATE.Click += LBDATE_Click_OpenEvent;
        }

        private void Menu1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private void LBDATE_Click_OpenEvent(object sender, EventArgs e)
        {
            var eventForm = new Event(Session.UserId, mesiac, rok, DateTime.Now.Day);
            eventForm.Show();
            this.Hide();
        }

        private void AktualizovaťTextLBDATE()
        {
            LBDATE.Text = $"{new DateTime(rok, mesiac, 1):MMMM yyyy}";
            LBDATE.Text = char.ToUpper(LBDATE.Text[0]) + LBDATE.Text.Substring(1);
        }

        private async Task NačítaťUdalostiAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            udalostiJednorazové.Clear();
            udalostiOpakujúceSa.Clear();
            eventCache.Clear();

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    await conn.OpenAsync();

                    string sql = @"SELECT DATE(date) as full_date, DAY(date) AS day, event, repeat_yearly 
                                 FROM tbl_calendar 
                                 WHERE (MONTH(date) = @month AND YEAR(date) = @year AND user_id = @userId) 
                                       OR (repeat_yearly = 1 AND MONTH(date) = @month AND user_id = @userId)
                                 LIMIT 1000";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@month", mesiac);
                        cmd.Parameters.AddWithValue("@year", rok);
                        cmd.Parameters.AddWithValue("@userId", Session.UserId);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DateTime fullDate = reader.GetDateTime(0);
                                int deň = reader.GetInt32(1);
                                string eventText = reader.GetString(2);
                                bool isYearly = reader.GetBoolean(3);

                                var calendarEvent = new CalendarEvent
                                {
                                    Text = eventText,
                                    IsRecurring = isYearly
                                };

                                if (!eventCache.TryGetValue(fullDate, out var events))
                                {
                                    events = new List<CalendarEvent>();
                                    eventCache[fullDate] = events;
                                }
                                events.Add(calendarEvent);

                                if (isYearly)
                                    PridaťDoOpakujúcich(deň, calendarEvent);
                                else
                                    PridaťDoJednorazových(deň, calendarEvent);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("Error loading events", ex);
            }
        }

        private void PridaťDoJednorazových(int deň, CalendarEvent calendarEvent)
        {
            if (!udalostiJednorazové.ContainsKey(deň))
                udalostiJednorazové[deň] = new List<CalendarEvent>();
            udalostiJednorazové[deň].Add(calendarEvent);
        }

        private void PridaťDoOpakujúcich(int deň, CalendarEvent calendarEvent)
        {
            if (!udalostiOpakujúceSa.ContainsKey(deň))
                udalostiOpakujúceSa[deň] = new List<CalendarEvent>();
            udalostiOpakujúceSa[deň].Add(calendarEvent);
        }

        private async Task AktualizovaťKalendárAsync()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    daycontainer.SuspendLayout();
                    try
                    {
                        DateTime začiatokMesiaci = new DateTime(rok, mesiac, 1);
                        int dniVMesiaci = DateTime.DaysInMonth(rok, mesiac);
                        int deňVTýždni = (int)začiatokMesiaci.DayOfWeek;

                        // Zistiť, či potrebujeme aktualizovať počet dní
                        if (daycontainer.Controls.Count != dniVMesiaci + deňVTýždni ||
                            cachedDayControls.Count != dniVMesiaci)
                        {
                            daycontainer.Controls.Clear();

                            // Pridať prázdne dni pre prvý týždeň
                            for (int i = 0; i < deňVTýždni; i++)
                                daycontainer.Controls.Add(new UserControlBlank());

                            // Aktualizovať zoznam cachedDayControls podľa aktuálneho počtu dní
                            if (cachedDayControls.Count < dniVMesiaci)
                            {
                                // Pridať chýbajúce dni
                                for (int i = cachedDayControls.Count; i < dniVMesiaci; i++)
                                {
                                    var deňOvládacíPrvok = new UserControlDays(Session.UserId, mesiac, rok);
                                    deňOvládacíPrvok.SetMenuForm(this);
                                    cachedDayControls.Add(deňOvládacíPrvok);
                                }
                            }
                            else if (cachedDayControls.Count > dniVMesiaci)
                            {
                                // Odstrániť prebytočné dni (ak nejaké sú)
                                int rozdiel = cachedDayControls.Count - dniVMesiaci;
                                cachedDayControls.RemoveRange(dniVMesiaci, rozdiel);
                            }

                            // Pridať všetky dni do kontajnera
                            foreach (var deňOvládacíPrvok in cachedDayControls)
                            {
                                daycontainer.Controls.Add(deňOvládacíPrvok);
                            }
                        }

                        // Aktualizovať obsah každého dňa
                        for (int deň = 1; deň <= dniVMesiaci; deň++)
                        {
                            if (deň - 1 >= cachedDayControls.Count) break;

                            var deňOvládacíPrvok = cachedDayControls[deň - 1];
                            var hasOneTimeEvents = udalostiJednorazové.TryGetValue(deň, out var jednorazové);
                            var hasRecurringEvents = udalostiOpakujúceSa.TryGetValue(deň, out var opakujúceSa);
                            var eventTexts = new List<string>();

                            if (hasOneTimeEvents && jednorazové != null)
                                eventTexts.AddRange(jednorazové.Select(e => e.Text));
                            if (hasRecurringEvents && opakujúceSa != null)
                                eventTexts.AddRange(opakujúceSa.Select(e => e.Text));

                            bool isRecurringOnly = hasRecurringEvents && !hasOneTimeEvents;

                            deňOvládacíPrvok.SuspendLayout();
                            deňOvládacíPrvok.SetDay(deň, eventTexts, isRecurringOnly);
                            deňOvládacíPrvok.ResumeLayout(false);
                        }
                    }
                    finally
                    {
                        daycontainer.ResumeLayout(true);
                    }
                });
            });
        }

        private void SkontrolujUdalostiNaDnes()
        {
            try
            {
                DateTime dnes = DateTime.Now;
                int dnesDeň = dnes.Day;

                List<string> dnešnéUdalosti = new List<string>();

                if (udalostiJednorazové.TryGetValue(dnesDeň, out var jednorazové))
                    dnešnéUdalosti.AddRange(jednorazové.Select(e => e.Text));
                if (udalostiOpakujúceSa.TryGetValue(dnesDeň, out var opakujúceSa))
                    dnešnéUdalosti.AddRange(opakujúceSa.Select(e => e.Text));

                if (dnešnéUdalosti.Count > 0)
                {
                    new ToastContentBuilder()
                        .AddText("Today's Events")
                        .AddText(string.Join(", ", dnešnéUdalosti))
                        .Show();
                }
            }
            catch (Exception ex)
            {
                LogError("Error checking today's events", ex);
            }
        }

        private async void btnnext_Click(object sender, EventArgs e)
        {
            mesiac++;
            if (mesiac > 12)
            {
                mesiac = 1;
                rok++;
            }

            cachedDayControls.Clear(); // Reset cache pri zmene mesiaca
            AktualizovaťTextLBDATE();
            await NačítaťUdalostiAsync();
            await AktualizovaťKalendárAsync();
        }

        private async void btnprevious_Click(object sender, EventArgs e)
        {
            mesiac--;
            if (mesiac < 1)
            {
                mesiac = 12;
                rok--;
            }

            cachedDayControls.Clear(); // Reset cache pri zmene mesiaca
            AktualizovaťTextLBDATE();
            await NačítaťUdalostiAsync();
            await AktualizovaťKalendárAsync();
        }

        private void Event_Click(object sender, EventArgs e)
        {
            var formUdalosti = new Event(Session.UserId, mesiac, rok, DateTime.Now.Day);
            formUdalosti.Show();
            this.Hide();
        }

        private void Zoznam_Click(object sender, EventArgs e)
        {
            var formUdalosti = new Zoznam(Session.UserId, mesiac, rok)
            {
                isFromInvite = false
            };
            formUdalosti.Show();
            this.Close();
        }

        private void Pozvi_Click(object sender, EventArgs e)
        {
            var formUdalosti = new Invite(Session.UserId);
            formUdalosti.Show();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Session.UserId, out int userId))
            {
                var notifikacieForm = new Notifikacie(userId);
                notifikacieForm.ShowDialog();
                this.Close();
            }
            else
            {
                LogError("UserId nie je platné číslo.", new FormatException());
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var userProfilForm = new UserProfil(Session.UserId);
            userProfilForm.Show();
            this.Hide();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(Session.UserId, out int userId))
            {
                var friendsForm = new Friends(userId);
                friendsForm.Show();
                this.Hide();
            }
        }

        private void LogError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LBDATE_Click(object sender, EventArgs e) { }
    }
}