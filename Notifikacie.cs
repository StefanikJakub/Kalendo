using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Windows.Networking.NetworkOperators;

namespace kalendar
{
    public partial class Notifikacie : Form
    {
        private string connStr = "server=82.208.23.39;user=Kalendo_difficulty;database=Kalendo_difficulty;port=3307;password=283387157515b569aef7cf99df2ba2cda488d278";
        private int userId;
        private Notification selectedNotification;

        public Notifikacie(int loggedInUserId)
        {
            InitializeComponent();
            this.userId = loggedInUserId;
            this.MouseDown += new MouseEventHandler(Notifikacie_MouseDown);
        }

        public class Notification
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public int InvitationId { get; set; } // ID pozvánky alebo udalosti
            public bool IsEventInvitation { get; set; } // Typ notifikácie
        }

        private void Notifikacie_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            LoadNotifications();
        }
        private void Notifikacie_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // To umožní presúvanie okna ťahaním myšou
                this.Capture = false;
                Message msg = Message.Create(this.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            }
        }
        // Spracovanie kliknutia na panel s notifikáciou
        private void OnNotificationPanelClick(Notification notification)
        {
            selectedNotification = notification;

            if (notification.IsEventInvitation)
            {
                var eventInfo = GetEventDetails(notification.InvitationId);

                if (eventInfo.Name != null)
                {
                    Názov.Text = eventInfo.Name;
                    Datum.Text = eventInfo.Date.ToString("dd.MM.yyyy");
                    Miesto.Text = eventInfo.Location;
                    Poziadavky.Text = eventInfo.Requirements;
                    pictureBox1.Image = eventInfo.ProfilePicture ?? Properties.Resources.pngwing_com11;
                    ShowEventTab();
                }
            }
            else
            {
                var userInfo = GetUserInfo(notification.InvitationId);
                if (!string.IsNullOrEmpty(userInfo.Name))
                {
                    DisplayUserInfo((userInfo.Name, userInfo.Phone, userInfo.Email, userInfo.ProfilePicture));
                    ShowFriendRequestTab();
                }
            }
        }

        // Získanie všetkých notifikácií
        private List<Notification> GetNotifications()
        {
            var notifications = new List<Notification>();
            notifications.AddRange(GetFriendRequests());
            notifications.AddRange(GetEventInvitations());
            return notifications;
        }

        // Získanie žiadostí o priateľstvo
        private List<Notification> GetFriendRequests()
        {
            List<Notification> friendRequests = new List<Notification>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT i.id, CONCAT(u.meno, ' chce byť vaším priateľom') AS message, i.sender_id AS invitationId " +
                                   "FROM tbl_invitations i " +
                                   "JOIN tbl_users u ON i.sender_id = u.id " +
                                   "WHERE i.receiver_id = @userId AND i.status = 'waiting'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            friendRequests.Add(new Notification
                            {
                                Id = reader.GetInt32("id"),
                                Message = reader.GetString("message"),
                                InvitationId = reader.GetInt32("invitationId"),
                                IsEventInvitation = false
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní priateľských požiadaviek: " + ex.Message);
                }
            }
            return friendRequests;
        }

        // Získanie pozvánok na udalosti
        private List<Notification> GetEventInvitations()
        {
            List<Notification> eventInvitations = new List<Notification>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT n.id, CONCAT(u.meno, ' vás pozval na udalosť') AS message, n.event_id AS invitationId " +
                                   "FROM tbl_notifications n " +
                                   "JOIN tbl_users u ON n.invitee_id = u.id " +
                                   "WHERE n.user_id = @userId AND n.status = 'waiting'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventInvitations.Add(new Notification
                            {
                                Id = reader.GetInt32("id"),
                                Message = reader.GetString("message"),
                                InvitationId = reader.GetInt32("invitationId"),
                                IsEventInvitation = true
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní pozvánok na udalosti: " + ex.Message);
                }
            }
            return eventInvitations;
        }

        private void LoadNotifications()
        {
            flowLayoutPanel1.Controls.Clear();
            var notificationsData = GetNotifications();

            foreach (var notification in notificationsData)
            {
                Image profileImage = null;
                string message = "";

                if (notification.IsEventInvitation)
                {
                    var eventDetails = GetEventDetails(notification.InvitationId);
                    profileImage = eventDetails.ProfilePicture;
                    message = $"{eventDetails.Name} vás pozval na udalosť";
                }
                else
                {
                    var userInfo = GetUserInfo(notification.InvitationId);
                    profileImage = userInfo.ProfilePicture;
                    message = $"{userInfo.Name} chce byť vaším priateľom";
                }

                // Vytvorenie panelu pre notifikáciu
                Panel notificationPanel = new Panel
                {
                    Width = flowLayoutPanel1.Width - 20,
                    Height = 80,
                    Margin = new Padding(5),
                    BackColor = Color.FromArgb(240, 240, 240),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = notification
                };

                PictureBox profilePicture = new PictureBox
                {
                    Size = new Size(50, 50),
                    Location = new Point(10, 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Image = profileImage ?? Properties.Resources.pngwing_com11,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                Label notificationLabel = new Label
                {
                    Text = message,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Location = new Point(70, 25),
                    AutoSize = true
                };

                notificationPanel.Controls.Add(profilePicture);
                notificationPanel.Controls.Add(notificationLabel);
                notificationPanel.Click += (s, e) => OnNotificationPanelClick(notification);
                profilePicture.Click += (s, e) => OnNotificationPanelClick(notification);
                notificationLabel.Click += (s, e) => OnNotificationPanelClick(notification);

                flowLayoutPanel1.Controls.Add(notificationPanel);
            }
        }

        private void ShowEventTab()
        {
            if (tabControl1 != null)
            {
                TabPage eventTab = tabControl1.TabPages["Event"];
                if (eventTab != null)
                {
                    tabControl1.SelectedTab = eventTab;
                }
            }
        }

        private void ShowFriendRequestTab()
        {
            if (tabControl1 != null)
            {
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Name == "FriendRequest")
                    {
                        tabControl1.SelectedTab = tabPage;
                        break;
                    }
                }
            }
        }

        // Získanie informácií o používateľovi
        private (string Name, string Phone, string Email, Image ProfilePicture) GetUserInfo(int senderId)
        {
            string name = "", phone = "", email = "";
            Image profilePicture = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT meno, telefon, email, profile_picture FROM tbl_users WHERE id = @senderId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@senderId", senderId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader.GetString("meno");
                            phone = reader.GetString("telefon");
                            email = reader.GetString("email");

                            if (!reader.IsDBNull(reader.GetOrdinal("profile_picture")))
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (var ms = new MemoryStream(imageBytes))
                                {
                                    profilePicture = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní údajov o používateľovi: " + ex.Message);
                }
            }
            return (name, phone, email, profilePicture);
        }

        // Získanie detailov udalosti
        private (string Name, DateTime Date, string Location, string Requirements, Image ProfilePicture) GetEventDetails(int eventId)
        {
            string name = "";
            DateTime date = DateTime.MinValue;
            string location = "";
            string requirements = "";
            Image profilePicture = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT c.event, c.date, c.place, c.requirements, u.profile_picture 
                                   FROM tbl_calendar c 
                                   JOIN tbl_users u ON c.user_id = u.id 
                                   WHERE c.id = @eventId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            name = reader.GetString("event");
                            date = reader.GetDateTime("date");
                            location = reader.GetString("place");
                            requirements = reader.GetString("requirements");

                            if (!reader.IsDBNull(reader.GetOrdinal("profile_picture")))
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (var ms = new MemoryStream(imageBytes))
                                {
                                    profilePicture = Image.FromStream(ms);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri načítaní údajov o udalosti: " + ex.Message);
                }
            }
            return (name, date, location, requirements, profilePicture);
        }

        private void UpdateRequestStatus(int notificationId, string newStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tbl_invitations SET status = @status WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", notificationId);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba pri aktualizácii stavu: " + ex.Message);
                }
            }
        }

        private void DisplayUserInfo((string meno, string telefon, string email, Image profilePicture) user)
        {
            Nameandsurname.Text = user.meno;
            telnumber.Text = user.telefon;
            email.Text = user.email;
            pictureBox1.Image = user.profilePicture ?? Properties.Resources.pngwing_com11;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (selectedNotification != null)
            {
                UpdateRequestStatus(selectedNotification.Id, "accepted");
                MessageBox.Show("Pozvánka bola prijatá.");
                LoadNotifications();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (selectedNotification != null)
            {
                UpdateRequestStatus(selectedNotification.Id, "refused");
                MessageBox.Show("Pozvánka bola odmietnutá.");
                LoadNotifications();
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (selectedNotification != null)
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();

                        // Kontrola stavu pozvánky
                        string checkQuery = "SELECT COUNT(*) FROM tbl_notifications WHERE id = @notificationId AND status = 'waiting'";
                        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@notificationId", selectedNotification.Id);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // Aktualizácia stavu
                            string updateQuery = "UPDATE tbl_notifications SET status = 'accepted' WHERE id = @notificationId";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@notificationId", selectedNotification.Id);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Pridanie udalosti do kalendára
                                string eventQuery = "SELECT event, date, place, requirements FROM tbl_calendar WHERE id = @eventId";
                                MySqlCommand eventCmd = new MySqlCommand(eventQuery, conn);
                                eventCmd.Parameters.AddWithValue("@eventId", selectedNotification.InvitationId);

                                using (MySqlDataReader reader = eventCmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string insertQuery = @"INSERT INTO tbl_calendar 
                                                            (event, date, place, requirements, user_id, created_at) 
                                                            VALUES (@event, @date, @place, @requirements, @userId, @createdAt)";
                                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                                        insertCmd.Parameters.AddWithValue("@event", reader.GetString("event"));
                                        insertCmd.Parameters.AddWithValue("@date", reader.GetDateTime("date"));
                                        insertCmd.Parameters.AddWithValue("@place", reader.GetString("place"));
                                        insertCmd.Parameters.AddWithValue("@requirements", reader.GetString("requirements"));
                                        insertCmd.Parameters.AddWithValue("@userId", userId);
                                        insertCmd.Parameters.AddWithValue("@createdAt", DateTime.Now);

                                        if (insertCmd.ExecuteNonQuery() > 0)
                                        {
                                            MessageBox.Show("Pozvánka bola prijatá a udalosť bola pridaná do vášho kalendára.");
                                        }
                                    }
                                }
                            }
                        }
                        LoadNotifications();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba pri prijímaní pozvánky: " + ex.Message);
                    }
                }
            }
        }

        private void Refuse_Click(object sender, EventArgs e)
        {
            if (selectedNotification != null)
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM tbl_notifications WHERE id = @notificationId";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@notificationId", selectedNotification.Id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pozvánka bola odmietnutá.");
                        LoadNotifications();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Chyba pri odmietnutí pozvánky: " + ex.Message);
                    }
                }
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            new Menu1().Show();
            this.Close();
        }

        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Nameandsurname_TextChanged(object sender, EventArgs e) { }
        private void telnumber_TextChanged(object sender, EventArgs e) { }
        private void email_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Názov_TextChanged(object sender, EventArgs e) { }
        private void Datum_TextChanged(object sender, EventArgs e) { }
        private void Miesto_TextChanged(object sender, EventArgs e) { }
        private void Poziadavky_TextChanged(object sender, EventArgs e) { }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
    }
}