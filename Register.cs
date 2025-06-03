using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BCrypt.Net;

namespace kalendar
{
    public partial class Register : Form
    {
        string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";

        public Register()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Register_MouseDown);
        }

        private void Register_Load(object sender, EventArgs e)
        {
            Password.PasswordChar = '*';
            guna2TextBox3.PasswordChar = '*';
        }
        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        // Obsluha tlačidla pre registráciu
        private void Button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text) ||
                string.IsNullOrEmpty(Password.Text) ||
                string.IsNullOrEmpty(guna2TextBox3.Text) ||
                string.IsNullOrEmpty(Email.Text) ||
                string.IsNullOrEmpty(guna2TextBox4.Text) ||
                string.IsNullOrEmpty(guna2TextBox2.Text) ||
                string.IsNullOrEmpty(guna2TextBox1.Text))
            {
                MessageBox.Show("Prosím, vyplň všetky polia.");
                return;
            }

            if (Password.Text != guna2TextBox3.Text)
            {
                MessageBox.Show("Heslá sa nezhodujú.");
                return;
            }

            if (!Regex.IsMatch(guna2TextBox1.Text, @"^\d+$"))
            {
                MessageBox.Show("Telefónne číslo môže obsahovať iba číslice.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Kontrola existencie používateľského mena
                    string checkUserQuery = "SELECT COUNT(*) FROM tbl_users WHERE username = @username";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkUserQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", Username.Text);
                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userExists > 0)
                        {
                            MessageBox.Show("Používateľské meno už existuje. Vyberte si iné meno.");
                            return;
                        }
                    }

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password.Text);

                    string sql = "INSERT INTO tbl_users (username, password, email, meno, priezvisko, telefon, is_verified) " +
                                 "VALUES (@username, @password, @email, @meno, @priezvisko, @telefon, 1)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", Username.Text);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@email", Email.Text);
                        cmd.Parameters.AddWithValue("@meno", guna2TextBox4.Text);
                        cmd.Parameters.AddWithValue("@priezvisko", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@telefon", guna2TextBox1.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registrácia úspešná");

                    this.Close();
                    Prihlasenie loginForm = new Prihlasenie();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba: " + ex.Message);
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(guna2TextBox1.Text, @"^\d*$"))
            {
                MessageBox.Show("Telefónne číslo môže obsahovať iba číslice.");
                guna2TextBox1.Text = Regex.Replace(guna2TextBox1.Text, @"\D", "");
                guna2TextBox1.SelectionStart = guna2TextBox1.Text.Length;
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox3_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox4_TextChanged(object sender, EventArgs e) { }
        private void Password_TextChanged(object sender, EventArgs e) { }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Prihlasenie newForm = new Prihlasenie();
            newForm.Show();
            this.Close();
        }
    }
}