using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace kalendar
{
    public partial class Friends : Form
    {
        private string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private int userId;

        public Friends(int loggedInUserId)
        {
            InitializeComponent();
            this.userId = loggedInUserId;
            LoadFriends();
            this.MouseDown += new MouseEventHandler(Friends_MouseDown);
        }

        // Načíta a zobrazí zoznam priateľov
        private void LoadFriends()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;

            List<Friend> friends = GetFriendsList(); // Získa zoznam priateľov z DB
            bool alternateColor = false;

            foreach (var friend in friends)
            {
                // Vytvorí panel pre každého priateľa
                Panel friendPanel = CreateFriendPanel(friend, alternateColor);
                flowLayoutPanel1.Controls.Add(friendPanel);
                alternateColor = !alternateColor;
            }
        }

        // Vytvorí panel pre jedného priateľa
        private Panel CreateFriendPanel(Friend friend, bool useAlternateColor)
        {
            // Hlavný panel pre priateľa
            Panel friendPanel = new Panel
            {
                Width = flowLayoutPanel1.Width - 10,
                Height = 80,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.None,
                Tag = friend.Id, 
                BackColor = useAlternateColor
                    ? Color.FromArgb(245, 248, 250) 
                    : Color.FromArgb(240, 240, 240) 
            };

            PictureBox profilePicture = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.None,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = friend.Id,
                Image = friend.ProfilePicture ?? Properties.Resources.pngwing_com11 // Predvolená fotka ak nie je nastavená
            };

            Label nameLabel = new Label
            {
                Text = $"{friend.FirstName} {friend.LastName}",
                Font = new Font("Montserrat", 14, FontStyle.Bold),
                Location = new Point(75, 30),
                AutoSize = true,
                Tag = friend.Id,
                BackColor = Color.Transparent
            };

            Button removeButton = new Button
            {
                Text = "Odobrať",
                Size = new Size(100, 40),
                Tag = friend.Id,
                BackColor = Color.FromArgb(255, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Montserrat", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right
            };

            // Pozícia tlačidla
            int verticalCenter = (friendPanel.Height - removeButton.Height) / 2;
            removeButton.Location = new Point(friendPanel.Width - removeButton.Width - 10, verticalCenter);

            Size originalSize = removeButton.Size;
            Point originalLocation = removeButton.Location;

            removeButton.FlatAppearance.BorderSize = 0;
            removeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 70, 70);
            removeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 50, 50);

            // Zväčšenie tlačidla pri najazdení
            removeButton.MouseEnter += (s, e) =>
            {
                removeButton.Size = new Size(originalSize.Width + 5, originalSize.Height + 2);
                removeButton.Location = new Point(originalLocation.X - 2, originalLocation.Y - 1);
            };

            removeButton.MouseLeave += (s, e) =>
            {
                removeButton.Size = originalSize;
                removeButton.Location = originalLocation;
            };

            removeButton.Click += RemoveFriend_Click;

            // Pridanie prvkov do panelu
            friendPanel.Controls.Add(profilePicture);
            friendPanel.Controls.Add(nameLabel);
            friendPanel.Controls.Add(removeButton);

            return friendPanel;
        }

        // Odstránenie priateľa
        private void RemoveFriend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Naozaj chcete odstrániť tohto priateľa?", "Potvrdenie",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Button removeButton = (Button)sender;
                int friendId = (int)removeButton.Tag;

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        conn.Open();

                        string query = @"DELETE FROM tbl_invitations 
                               WHERE (sender_id = @userId AND receiver_id = @friendId)
                               OR (sender_id = @friendId AND receiver_id = @userId)";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@userId", Session.UserId);
                        cmd.Parameters.AddWithValue("@friendId", friendId);

                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Priateľ bol úspešne odobraný");
                            LoadFriends();
                        }
                        else
                        {
                            MessageBox.Show("Nepodarilo sa odstrániť priateľa");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri odstraňovaní priateľa: " + ex.Message);
                }
            }
        }

        // Získa zoznam priateľov z databázy
        private List<Friend> GetFriendsList()
        {
            var friends = new List<Friend>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // SQL na získanie priateľov (potvrdené žiadosti)
                    string query = @"SELECT u.id, u.meno, u.priezvisko, u.profile_picture, u.telefon, u.email 
                                    FROM tbl_users u
                                    JOIN tbl_invitations i ON 
                                        (i.sender_id = u.id AND i.receiver_id = @userId) OR 
                                        (i.receiver_id = u.id AND i.sender_id = @userId)
                                    WHERE i.status = 'accepted'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var friend = new Friend
                            {
                                Id = reader.GetInt32("id"),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("meno")) ? string.Empty : reader.GetString("meno"),
                                LastName = reader.IsDBNull(reader.GetOrdinal("priezvisko")) ? string.Empty : reader.GetString("priezvisko"),
                                Phone = reader.IsDBNull(reader.GetOrdinal("telefon")) ? string.Empty : reader.GetString("telefon"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString("email")
                            };

                            if (!reader.IsDBNull(reader.GetOrdinal("profile_picture")))
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (var ms = new MemoryStream(imageBytes))
                                {
                                    friend.ProfilePicture = Image.FromStream(ms);
                                }
                            }

                            friends.Add(friend);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní priateľov: " + ex.Message);
                }
            }

            return friends;
        }

        // Vyhľadávanie v zozname priateľov
        private void Search_TextChanged(object sender, EventArgs e)
        {
            string hladanyText = Search.Text.ToLower().Trim();

            foreach (Control panel in flowLayoutPanel1.Controls)
            {
                bool zobrazit = false;

                // Kontrola či meno obsahuje hľadaný text
                foreach (Control control in panel.Controls)
                {
                    if (control is Label label)
                    {
                        if (label.Text.ToLower().Contains(hladanyText))
                        {
                            zobrazit = true;
                            break;
                        }
                    }
                }

                panel.Visible = zobrazit;
            }
        }


        private void Friends_Load(object sender, EventArgs e) { }

        private class Friend
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public Image ProfilePicture { get; set; }
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Menu1 newForm = new Menu1();
            newForm.Show();
            this.Close();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Invite inviteForm = new Invite(Session.UserId);
            inviteForm.Show();
            this.Close();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Message m = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                base.WndProc(ref m); // Vyvoláme presúvanie okna
            }
        }
        private void Friends_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
    }
}