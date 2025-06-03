using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace kalendar
{
    public partial class Zoznam : Form
    {
        private string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private string userId;
        private int mesiac;
        private int rok;
        private string selectedInviteeId;

        public Zoznam(string userId, int selectedInviteeId, int mesiac = 0, int rok = 0)
        {
            InitializeComponent();
            this.userId = userId;
            this.selectedInviteeId = selectedInviteeId.ToString();
            this.mesiac = mesiac;
            this.rok = rok;

            SetupForm();
            LoadEvents();
            this.MouseDown += Zoznam_MouseDown;
        }

        private void SetupForm()
        {
            BackColor = Color.FromArgb(240, 240, 240);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
        }

        private void LoadEvents()
        {
            flowLayoutPanel1.Controls.Clear();

            List<CalendarEvent> events = GetEventsList();
            bool alternateColor = false;

            foreach (var calendarEvent in events)
            {
                Panel eventPanel = CreateEventPanel(calendarEvent, alternateColor);
                flowLayoutPanel1.Controls.Add(eventPanel);
                alternateColor = !alternateColor;
            }
        }

        private Panel CreateEventPanel(CalendarEvent calendarEvent, bool useAlternateColor)
        {
            // Main panel with no margins and alternating colors
            Panel eventPanel = new Panel
            {
                Width = flowLayoutPanel1.Width,
                Height = 60,  // More compact height
                Margin = new Padding(0),  // No margin
                Tag = calendarEvent.Id,
                BackColor = useAlternateColor
                    ? Color.FromArgb(240, 240, 240)  // Light gray
                    : Color.FromArgb(230,230,230), // White
                BorderStyle = BorderStyle.None
            };

            // Date label
            Label dateLabel = new Label
            {
                Text = calendarEvent.Date.ToString("dd.MM.yyyy"),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true,
                Tag = calendarEvent.Id,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(60, 60, 60)  // Dark gray
            };

            // Event details label
            Label eventLabel = new Label
            {
                Text = calendarEvent.EventDetails,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 30),
                AutoSize = true,
                Tag = calendarEvent.Id,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(100, 100, 100)  // Medium gray
            };

            // Action button
            Button actionButton = new Button
            {
                Text = isFromInvite ? "Pozvať" : "Vymazať",
                Size = new Size(80, 28),
                Tag = calendarEvent.Id,
                BackColor = isFromInvite
                    ? Color.FromArgb(70, 130, 180)  // Muted blue
                    : Color.FromArgb(200, 70, 70),  // Muted red
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };

            // Position button
            actionButton.Location = new Point(
                eventPanel.Width - actionButton.Width - 10,
                (eventPanel.Height - actionButton.Height) / 2);

            // Button hover effects
            actionButton.FlatAppearance.BorderSize = 0;
            actionButton.FlatAppearance.MouseOverBackColor = isFromInvite
                ? Color.FromArgb(60, 120, 170)
                : Color.FromArgb(180, 60, 60);
            actionButton.FlatAppearance.MouseDownBackColor = isFromInvite
                ? Color.FromArgb(50, 110, 160)
                : Color.FromArgb(160, 50, 50);

            actionButton.Click += (s, e) => HandleEventAction(calendarEvent);

            // Add controls to panel
            eventPanel.Controls.Add(dateLabel);
            eventPanel.Controls.Add(eventLabel);
            eventPanel.Controls.Add(actionButton);

            return eventPanel;
        }

        private void HandleEventAction(CalendarEvent calendarEvent)
        {
            if (isFromInvite)
            {
                if (MessageBox.Show($"Pozvať používateľa na udalosť '{calendarEvent.EventDetails}'?",
                    "Potvrdenie pozvánky",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SendInvitation(userId, selectedInviteeId, calendarEvent.Id.ToString(),
                        calendarEvent.Date.ToString("dd.MM.yyyy"), calendarEvent.EventDetails);
                }
            }
            else
            {
                if (MessageBox.Show($"Skutočne chcete vymazať udalosť '{calendarEvent.EventDetails}'?",
                    "Potvrdenie vymazania",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteEvent(calendarEvent.Id.ToString());
                    LoadEvents();
                }
            }
        }

        private List<CalendarEvent> GetEventsList()
        {
            var events = new List<CalendarEvent>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT id, date, event FROM tbl_calendar WHERE user_id = @userId";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            events.Add(new CalendarEvent
                            {
                                Id = reader.GetInt32("id"),
                                Date = reader.GetDateTime("date"),
                                EventDetails = reader.GetString("event")
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítavaní udalostí: " + ex.Message, "Chyba",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return events;
        }

        private void SendInvitation(string senderId, string inviteeId, string eventId, string eventDate, string eventDetails)
        {
            if (string.IsNullOrEmpty(inviteeId))
            {
                MessageBox.Show("ID pozvaného používateľa nie je nastavené.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"INSERT INTO tbl_notifications 
                                (user_id, invitee_id, message, status, created_at, event_id) 
                                VALUES (@userId, @inviteeId, @message, @status, @createdAt, @eventId)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        string message = $"Pozvánka na udalosť: {eventDetails} dňa {eventDate}";
                        cmd.Parameters.AddWithValue("@userId", inviteeId);
                        cmd.Parameters.AddWithValue("@inviteeId", senderId);
                        cmd.Parameters.AddWithValue("@status", "waiting");
                        cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@eventId", eventId);
                        cmd.Parameters.AddWithValue("@message", message);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pozvánka bola úspešne odoslaná.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri odosielaní pozvánky: " + ex.Message);
            }
        }

        private void DeleteEvent(string eventId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    using (MySqlCommand deleteNotificationsCmd = new MySqlCommand(
                        "DELETE FROM tbl_notifications WHERE event_id = @eventId", conn))
                    {
                        deleteNotificationsCmd.Parameters.AddWithValue("@eventId", eventId);
                        deleteNotificationsCmd.ExecuteNonQuery();
                    }

                    using (MySqlCommand deleteEventCmd = new MySqlCommand(
                        "DELETE FROM tbl_calendar WHERE id = @id", conn))
                    {
                        deleteEventCmd.Parameters.AddWithValue("@id", eventId);
                        deleteEventCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri vymazávaní udalosti: " + ex.Message);
            }
        }

        public bool isFromInvite { get; set; } = false;

        private void Zoznam_Load(object sender, EventArgs e)
        {
            LoadEvents();
        }

        private void Zoznam_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private class CalendarEvent
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string EventDetails { get; set; }
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Menu1 newForm = new Menu1();
            newForm.Show();
            this.Close();
        }
    }
}