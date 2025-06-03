using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace kalendar
{
    public partial class UserProfil : Form
    {
        private string userId;
        private string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278"; // Pripojovací reťazec k MySQL databáze

        public UserProfil(string userId)
        {
            InitializeComponent();
            this.userId = userId;
            this.MouseDown += new MouseEventHandler(UserProfil_MouseDown);
        }

        private void UserProfil_Load_1(object sender, EventArgs e)
        {
            try
            {
                LoadUserProfile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri načítaní údajov: " + ex.Message);
            }
        }
        private void UserProfil_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        // Metóda na načítanie údajov používateľa z databázy
        private void LoadUserProfile()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open(); 
                    // SQL dotaz na získanie údajov používateľa
                    string query = "SELECT username, email, meno, priezvisko, telefon, profile_picture FROM tbl_users WHERE id = @userId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) // Ak sa našiel používateľ
                    {
                        Username.Text = reader["username"].ToString();
                        email.Text = reader["email"].ToString();
                        telnumber.Text = reader["telefon"].ToString();

                        // Spracovanie profilového obrázka
                        if (!reader.IsDBNull(reader.GetOrdinal("profile_picture")))
                        {
                            byte[] imageBytes = (byte[])reader["profile_picture"];
                            using (var ms = new System.IO.MemoryStream(imageBytes))
                            {
                                pictureBox1.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }

                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní údajov: " + ex.Message);
                }
            }
        }

        private void ulozit_Click(object sender, EventArgs e)
        {
            UpdateUserProfile();
        }

        private void UpdateUserProfile()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
     
                    string query = "UPDATE tbl_users SET username = @username, email = @email, telefon = @telefon WHERE id = @userId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@username", Username.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", email.Text.Trim());
                    cmd.Parameters.AddWithValue("@telefon", telnumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@userId", userId);

                    int rowsAffected = cmd.ExecuteNonQuery(); 

                    if (rowsAffected > 0) // Ak sa niečo zmenilo
                    {
                        MessageBox.Show("Údaje boli úspešne uložené.", "Úspech", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Údaje neboli uložené. Skontrolujte, či je užívateľ existujúci.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri aktualizácii údajov: " + ex.Message);
                }
            }
        }

        private void zmenafotky_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) // Vytvorí dialóg na výber súboru
            {
                ofd.Filter = "Obrázky (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK) 
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    SaveProfilePicture(ofd.FileName); // Uloží obrázok do databázy
                }
            }
        }

        // Metóda na uloženie profilového obrázka do databázy
        private void SaveProfilePicture(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open(); 
    
                    string query = "UPDATE tbl_users SET profile_picture = @imageBytes WHERE id = @userId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@imageBytes", imageBytes);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery(); // Vykoná aktualizáciu
                    MessageBox.Show("Profilová fotka bola úspešne zmenená.", "Úspech", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri ukladaní profilovej fotky: " + ex.Message);
                }
            }
        }

        private void Username_TextChanged(object sender, EventArgs e) { }
        private void telnumber_TextChanged(object sender, EventArgs e) { }
        private void email_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Menu1 newForm = new Menu1();
            newForm.Show();
            this.Close();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}