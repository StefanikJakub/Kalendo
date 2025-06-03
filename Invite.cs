using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading.Tasks;

namespace kalendar
{
    public partial class Invite : Form
    {
        private readonly string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private readonly string userId;
        private int selectedInviteeId;
        private DateTime lastSearchTime = DateTime.MinValue;
        private const int SearchDelayMs = 300;
        private int currentPage = 1;
        private const int PageSize = 20;
        private bool isLoading = false;
        private string lastSearchTerm = "";
        private Dictionary<string, List<(int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture)>> searchCache = new Dictionary<string, List<(int, string, string, string, string, byte[])>>();
        public Invite(string loggedInUserId)
        {
            InitializeComponent();
            this.userId = loggedInUserId;
            this.MouseDown += Invite_MouseDown;
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered",
        System.Reflection.BindingFlags.SetProperty |
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.NonPublic,
        null, flowLayoutPanel1, new object[] { true });
        }

        private void Invite_Load(object sender, EventArgs e) { }

        private void Invite_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = Search.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length < 2)
            {
                flowLayoutPanel1.Controls.Clear();
                return;
            }

            lastSearchTerm = searchTerm;
            currentPage = 1; // Reset stránkovania pre nové vyhľadávanie

            if (searchCache.ContainsKey(searchTerm))
            {
                DisplaySearchResults(searchCache[searchTerm], searchTerm);
                return;
            }

            LoadMoreResults(searchTerm);
        }
        private async void LoadMoreResults(string searchTerm)
        {
            if (isLoading) return;
            isLoading = true;

            try
            {
                var newUsers = await Task.Run(() => SearchUsers(searchTerm, currentPage));

                if (!searchCache.ContainsKey(searchTerm))
                {
                    searchCache[searchTerm] = new List<(int, string, string, string, string, byte[])>();
                }

                if (currentPage == 1)
                {
                    searchCache[searchTerm].Clear();
                    flowLayoutPanel1.Controls.Clear();
                }

                searchCache[searchTerm].AddRange(newUsers);
                currentPage++;

                this.Invoke((MethodInvoker)delegate
                {
                    DisplaySearchResults(searchCache[searchTerm], searchTerm);
                });
            }
            catch (Exception ex)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Chyba pri načítaní používateľov: " + ex.Message);
                });
            }
            finally
            {
                isLoading = false;
            }
        }
        private List<(int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture)> SearchUsers(string searchTerm, int page)
        {
            var users = new List<(int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture)>();

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string query = @"SELECT id, meno, priezvisko, telefon, email, profile_picture 
                               FROM tbl_users 
                               WHERE (meno LIKE @search OR priezvisko LIKE @search) 
                               AND id != @currentUserId
                               LIMIT @offset, @pageSize";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                    cmd.Parameters.AddWithValue("@currentUserId", this.userId);
                    cmd.Parameters.AddWithValue("@offset", (page - 1) * PageSize);
                    cmd.Parameters.AddWithValue("@pageSize", PageSize);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) { int id = reader.GetInt32("id"); 
                            string meno = reader.GetString("meno"); string priezvisko = reader.GetString("priezvisko"); 
                            string telefon = reader.GetString("telefon"); 
                            string email = reader.GetString("email"); 
                            byte[] profilePicture = reader.IsDBNull(reader.GetOrdinal("profile_picture")) ? null : (byte[])reader["profile_picture"]; 
                            users.Add((id, meno, priezvisko, telefon, email, profilePicture)); }
                    }
                }
            }

            return users;
        }

        private void DisplaySearchResults(List<(int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture)> users, string searchTerm)
        {
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Visible = false; // Dočasne skryjeme panel

            // Odstránime iba panely používateľov, nie všetky kontroly
            var controlsToRemove = flowLayoutPanel1.Controls.OfType<Guna2Panel>().ToList();
            foreach (var control in controlsToRemove)
            {
                flowLayoutPanel1.Controls.Remove(control);
                control.Dispose(); // Uvoľníme zdroje
            }

            var sortedUsers = users
                .OrderBy(u => u.meno.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                .ThenBy(u => u.meno.Contains(searchTerm) ? 0 : 1)
                .ThenBy(u => u.meno)
                .ToList();

            // Vytvárame nové panely mimo UI vlákna
            var panels = new List<Guna2Panel>();
            foreach (var user in sortedUsers)
            {
                var panel = CreateUserPanel(user);
                panels.Add(panel);
            }

            // Pridávame všetky panely naraz
            flowLayoutPanel1.Controls.AddRange(panels.ToArray());

            flowLayoutPanel1.Visible = true;
            flowLayoutPanel1.ResumeLayout(true);
            flowLayoutPanel1.PerformLayout(); // Vynútiť prekreslenie
        }

        private Guna2Panel CreateUserPanel((int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture) user)
        {
            var panel = new Guna2Panel
            {
                Width = flowLayoutPanel1.Width - SystemInformation.VerticalScrollBarWidth - 25,
                Height = 70,
                Margin = new Padding(5),
                BorderRadius = 10,
                FillColor = Color.FromArgb(240, 240, 240),
                BorderColor = Color.Transparent,
                Tag = user.id
            };

            // Prednačítať obrázok do pamäte
            Image profileImage = user.profilePicture != null ?
                ByteArrayToImage(user.profilePicture) :
                Properties.Resources.pngwing_com11;

            var profilePic = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = profileImage
            };

            var nameLabel = new Label
            {
                Text = $"{user.meno} {user.priezvisko}",
                Font = new Font("Tahoma", 14, FontStyle.Bold),
                Location = new Point(70, 17),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };

            // Spoločný event handler pre celý panel
            panel.Click += (s, e) => DisplayUserInfo(user);
            profilePic.Click += (s, e) => DisplayUserInfo(user); // Priamo voláme handler
            nameLabel.Click += (s, e) => DisplayUserInfo(user); // Priamo voláme handler

            panel.Controls.Add(profilePic);
            panel.Controls.Add(nameLabel);

            return panel;
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void DisplayUserInfo((int id, string meno, string priezvisko, string telefon, string email, byte[] profilePicture) user)
        {
            Nameandsurname.Text = $"{user.meno} {user.priezvisko}";
            telnumber.Text = user.telefon;
            email.Text = user.email;
            pictureBox1.Image = user.profilePicture != null ? ByteArrayToImage(user.profilePicture) : Properties.Resources.pngwing_com11;

            Button.Visible = true;
            Button.Click -= Button_Click;
            Button.Click += (s, e) => SendFriendRequest(user.id);

            selectedInviteeId = user.id;
        }
        private void flowLayoutPanel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (!isLoading &&
                flowLayoutPanel1.VerticalScroll.Value > flowLayoutPanel1.VerticalScroll.Maximum - 500 &&
                searchCache.ContainsKey(lastSearchTerm))
            {
                LoadMoreResults(lastSearchTerm);
            }
        }
        private void SendFriendRequest(int receiverId)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    // Kombinovaný dotaz na získanie mena a kontrolu existujúcej pozvánky
                    string senderName = "Neznámy používateľ";
                    var senderQuery = "SELECT meno, priezvisko FROM tbl_users WHERE id = @senderId";

                    using (var senderCmd = new MySqlCommand(senderQuery, conn))
                    {
                        senderCmd.Parameters.AddWithValue("@senderId", this.userId);
                        using (var reader = senderCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                senderName = $"{reader.GetString("meno")} {reader.GetString("priezvisko")}";
                            }
                        }
                    }

                    var checkQuery = "SELECT COUNT(*) FROM tbl_invitations WHERE sender_id = @senderId AND receiver_id = @receiverId";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@senderId", this.userId);
                        checkCmd.Parameters.AddWithValue("@receiverId", receiverId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            using (var insertCmd = new MySqlCommand(
                                "INSERT INTO tbl_invitations (sender_id, receiver_id, status, sender_name) " +
                                "VALUES (@senderId, @receiverId, 'waiting', @senderName)", conn))
                            {
                                insertCmd.Parameters.AddWithValue("@senderId", this.userId);
                                insertCmd.Parameters.AddWithValue("@receiverId", receiverId);
                                insertCmd.Parameters.AddWithValue("@senderName", senderName);
                                insertCmd.ExecuteNonQuery();
                                MessageBox.Show("Pozvánka bola odoslaná.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Pozvánka už bola odoslaná.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba pri odosielaní pozvánky: " + ex.Message);
            }
        }

        // Pôvodné metódy (nezmenené)
        private void guna2TextBox5_Paint(object sender, PaintEventArgs e) { }
        private void Nameandsurname_TextChanged(object sender, EventArgs e) { }
        private void telnumber_TextChanged(object sender, EventArgs e) { }
        private void email_TextChanged(object sender, EventArgs e) { }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }
        private void Button_Click(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void PozvankaUdalost_Click(object sender, EventArgs e)
        {
            Zoznam zoznamForm = new Zoznam(this.userId, this.selectedInviteeId);
            zoznamForm.isFromInvite = true;
            zoznamForm.ShowDialog();
            this.Close();
        }

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