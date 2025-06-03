using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kalendar
{
    public partial class Prihlasenie : Form
    {
        private const string ConnStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";

        public Prihlasenie()
        {
            InitializeComponent();
            txtUsername.KeyDown += TxtFields_KeyDown;
            txtPassword.KeyDown += TxtFields_KeyDown;
            this.MouseDown += new MouseEventHandler(Prihlasenie_MouseDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void TxtFields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_Click_1(sender, EventArgs.Empty);
                e.SuppressKeyPress = true;
            }
        }
        private void Prihlasenie_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        private async void Login_Click_1(object sender, EventArgs e)
        {
            // Validácia vstupných údajov
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Prosím, vyplňte všetky polia!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Asynchrónne overenie prihlasovacích údajov
                bool isAuthenticated = await AuthenticateUserAsync(txtUsername.Text.Trim(), txtPassword.Text);

                if (isAuthenticated)
                {
                    Menu1 menuForm = new Menu1();
                    menuForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nesprávne meno alebo heslo.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba počas prihlasovania: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Asynchrónna metóda pre overenie prihlasovacích údajov
        private async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnStr))
                using (var cmd = new MySqlCommand("SELECT id, password FROM tbl_users WHERE username = @username LIMIT 1", conn))
                {
                    await conn.OpenAsync();

                    cmd.Parameters.AddWithValue("@username", username);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string hashedPasswordFromDb = reader["password"].ToString();
                            string userId = reader["id"].ToString();

                            // Overenie hesla pomocou BCrypt
                            if (BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDb))
                            {
                                Session.Prihlaseny = true;
                                Session.UserId = userId;
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba počas prihlasovania: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e) { }
        private void txtUsername_TextChanged(object sender, EventArgs e) { }

        private void guna2CircleButton3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }
    }
}