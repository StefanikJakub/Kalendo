using System;
using System.Globalization;
using System.Management.Instrumentation;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kalendar.Views
{
    public partial class Event : Form
    {
        // Pripojovacie údaje k MySQL databáze
        private readonly string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private readonly string userId;
        private int month;
        private int year;
        private int day;

        
        public Event(string userId, int month, int year, int day)
        {
            InitializeComponent();
            this.userId = userId;
            this.month = month;
            this.year = year;
            this.day = day;
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.MouseDown += new MouseEventHandler(Event_MouseDown);
        }

        private void Event_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 12; i++)
            {
                comboBoxMonth.Items.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i));
            }

            numericUpDownYear.Minimum = 2020;
            numericUpDownYear.Maximum = 1000000;
            numericUpDownYear.Value = year;

            comboBox1.SelectedIndex = 1;

            comboBoxMonth.SelectedIndex = month - 1;
            UpdateDaysComboBox();

            if (comboBoxDay.Items.Contains(day))
            {
                comboBoxDay.SelectedItem = day;
            }
            else
            {
                comboBoxDay.SelectedIndex = 0;
            }
        }
        private void Event_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        // Metóda pre aktualizáciu zoznamu dní podľa vybraného mesiaca a roka
        private void UpdateDaysComboBox()
        {
            int selectedMonth = comboBoxMonth.SelectedIndex + 1;
            int selectedYear = (int)numericUpDownYear.Value;

            int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);

            comboBoxDay.Items.Clear();

            for (int i = 1; i <= daysInMonth; i++)
            {
                comboBoxDay.Items.Add(i);
            }

            if (comboBoxDay.Items.Count > 0)
            {
                if (comboBoxDay.Items.Contains(day))
                {
                    comboBoxDay.SelectedItem = day;
                }
                else
                {
                    comboBoxDay.SelectedIndex = 0;
                }
            }
        }

        // Obsluha tlačidla pre uloženie udalosti
        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    int day = int.Parse(comboBoxDay.SelectedItem.ToString());
                    int month = comboBoxMonth.SelectedIndex + 1;
                    int year = (int)numericUpDownYear.Value;

                    DateTime selectedDate = new DateTime(year, month, day);
                    string formattedDate = selectedDate.ToString("yyyy-MM-dd");

                    string place = Miesto.Text;
                    string requirements = Poziadavky.Text;
                    string eventName = Názov.Text;
                    bool repeatYearly = comboBox1.SelectedItem.ToString() == "Každý rok";

                    // SQL príkaz pre vloženie alebo aktualizáciu udalosti
                    string sql = @"
                    INSERT INTO tbl_calendar 
                    (date, place, requirements, event, user_id, repeat_yearly, day_of_month, month_of_year) 
                    VALUES 
                    (@date, @place, @requirements, @event, @userId, @repeatYearly, @dayOfMonth, @monthOfYear)
                    ON DUPLICATE KEY UPDATE 
                    place = VALUES(place), 
                    requirements = VALUES(requirements), 
                    event = VALUES(event), 
                    repeat_yearly = VALUES(repeat_yearly), 
                    day_of_month = VALUES(day_of_month), 
                    month_of_year = VALUES(month_of_year)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Pridanie parametrov do SQL príkazu
                        cmd.Parameters.AddWithValue("@date", formattedDate);
                        cmd.Parameters.AddWithValue("@place", string.IsNullOrEmpty(place) ? (object)DBNull.Value : place);
                        cmd.Parameters.AddWithValue("@requirements", string.IsNullOrEmpty(requirements) ? (object)DBNull.Value : requirements);
                        cmd.Parameters.AddWithValue("@event", string.IsNullOrEmpty(eventName) ? (object)DBNull.Value : eventName);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@repeatYearly", repeatYearly ? 1 : 0);
                        cmd.Parameters.AddWithValue("@dayOfMonth", day);
                        cmd.Parameters.AddWithValue("@monthOfYear", month);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Udalosť bola úspešne uložená!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba: " + ex.Message);
                }
            }
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDaysComboBox();
        }

        private void comboBoxDay_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void Názov_TextChanged(object sender, EventArgs e) { }
        private void Miesto_TextChanged(object sender, EventArgs e) { }
        private void Poziadavky_TextChanged(object sender, EventArgs e) { }
        private void guna2CircleButton1_Click(object sender, EventArgs e) { }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Menu1 newForm = new Menu1();
            newForm.Show();
            this.Hide();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}