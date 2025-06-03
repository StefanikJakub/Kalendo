namespace kalendar.Views
{
    partial class Notifikacie
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notifikacie));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.containerControl1 = new System.Windows.Forms.ContainerControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Event = new System.Windows.Forms.TabPage();
            this.Refuse = new Guna.UI2.WinForms.Guna2Button();
            this.Accept = new Guna.UI2.WinForms.Guna2Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Miesto = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Názov = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Datum = new Guna.UI2.WinForms.Guna2TextBox();
            this.Poziadavky = new Guna.UI2.WinForms.Guna2TextBox();
            this.FriendRequest = new System.Windows.Forms.TabPage();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.email = new Guna.UI2.WinForms.Guna2TextBox();
            this.Nameandsurname = new Guna.UI2.WinForms.Guna2TextBox();
            this.telnumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2CircleButton3 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.containerControl1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Event.SuspendLayout();
            this.FriendRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(23, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(510, 763);
            this.flowLayoutPanel1.TabIndex = 7;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // containerControl1
            // 
            this.containerControl1.BackColor = System.Drawing.Color.Gainsboro;
            this.containerControl1.Controls.Add(this.flowLayoutPanel1);
            this.containerControl1.Location = new System.Drawing.Point(81, 85);
            this.containerControl1.Name = "containerControl1";
            this.containerControl1.Size = new System.Drawing.Size(557, 800);
            this.containerControl1.TabIndex = 8;
            this.containerControl1.Text = "containerControl1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Event);
            this.tabControl1.Controls.Add(this.FriendRequest);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 18);
            this.tabControl1.Location = new System.Drawing.Point(747, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(975, 822);
            this.tabControl1.TabIndex = 9;
            // 
            // Event
            // 
            this.Event.Controls.Add(this.Refuse);
            this.Event.Controls.Add(this.Accept);
            this.Event.Controls.Add(this.label4);
            this.Event.Controls.Add(this.Miesto);
            this.Event.Controls.Add(this.label3);
            this.Event.Controls.Add(this.Názov);
            this.Event.Controls.Add(this.label2);
            this.Event.Controls.Add(this.label1);
            this.Event.Controls.Add(this.Datum);
            this.Event.Controls.Add(this.Poziadavky);
            this.Event.Location = new System.Drawing.Point(4, 22);
            this.Event.Name = "Event";
            this.Event.Padding = new System.Windows.Forms.Padding(3);
            this.Event.Size = new System.Drawing.Size(967, 796);
            this.Event.TabIndex = 1;
            this.Event.Text = "Pozvánka na udalosť";
            this.Event.UseVisualStyleBackColor = true;
            // 
            // Refuse
            // 
            this.Refuse.AutoRoundedCorners = true;
            this.Refuse.BorderRadius = 26;
            this.Refuse.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Refuse.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Refuse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Refuse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Refuse.FillColor = System.Drawing.Color.CornflowerBlue;
            this.Refuse.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Refuse.ForeColor = System.Drawing.Color.White;
            this.Refuse.Location = new System.Drawing.Point(511, 548);
            this.Refuse.Name = "Refuse";
            this.Refuse.Size = new System.Drawing.Size(273, 54);
            this.Refuse.TabIndex = 70;
            this.Refuse.Text = "Odmietnuť";
            this.Refuse.Click += new System.EventHandler(this.Refuse_Click);
            // 
            // Accept
            // 
            this.Accept.AutoRoundedCorners = true;
            this.Accept.BorderRadius = 26;
            this.Accept.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Accept.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Accept.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Accept.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Accept.FillColor = System.Drawing.Color.CornflowerBlue;
            this.Accept.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Accept.ForeColor = System.Drawing.Color.White;
            this.Accept.Location = new System.Drawing.Point(178, 548);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(273, 54);
            this.Accept.TabIndex = 69;
            this.Accept.Text = "Prijať";
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(186, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 27);
            this.label4.TabIndex = 65;
            this.label4.Text = "Miesto";
            // 
            // Miesto
            // 
            this.Miesto.BorderColor = System.Drawing.Color.Black;
            this.Miesto.BorderRadius = 5;
            this.Miesto.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Miesto.DefaultText = "";
            this.Miesto.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Miesto.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Miesto.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Miesto.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Miesto.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Miesto.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Miesto.ForeColor = System.Drawing.Color.Black;
            this.Miesto.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Miesto.Location = new System.Drawing.Point(178, 365);
            this.Miesto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Miesto.Name = "Miesto";
            this.Miesto.PasswordChar = '\0';
            this.Miesto.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Miesto.PlaceholderText = "";
            this.Miesto.ReadOnly = true;
            this.Miesto.SelectedText = "";
            this.Miesto.Size = new System.Drawing.Size(606, 48);
            this.Miesto.TabIndex = 64;
            this.Miesto.TextChanged += new System.EventHandler(this.Miesto_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(431, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 40);
            this.label3.TabIndex = 63;
            this.label3.Text = "Názov";
            // 
            // Názov
            // 
            this.Názov.BorderColor = System.Drawing.Color.Black;
            this.Názov.BorderRadius = 5;
            this.Názov.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Názov.DefaultText = "";
            this.Názov.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Názov.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Názov.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Názov.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Názov.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Názov.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Názov.ForeColor = System.Drawing.Color.Black;
            this.Názov.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Názov.Location = new System.Drawing.Point(178, 181);
            this.Názov.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Názov.Name = "Názov";
            this.Názov.PasswordChar = '\0';
            this.Názov.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Názov.PlaceholderText = "";
            this.Názov.ReadOnly = true;
            this.Názov.SelectedText = "";
            this.Názov.Size = new System.Drawing.Size(606, 48);
            this.Názov.TabIndex = 62;
            this.Názov.TextChanged += new System.EventHandler(this.Názov_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(186, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 27);
            this.label2.TabIndex = 59;
            this.label2.Text = "Dátum";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(186, 430);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 27);
            this.label1.TabIndex = 58;
            this.label1.Text = "Požiadavky";
            // 
            // Datum
            // 
            this.Datum.BorderColor = System.Drawing.Color.Black;
            this.Datum.BorderRadius = 5;
            this.Datum.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Datum.DefaultText = "";
            this.Datum.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Datum.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Datum.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Datum.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Datum.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Datum.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Datum.ForeColor = System.Drawing.Color.Black;
            this.Datum.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Datum.Location = new System.Drawing.Point(178, 272);
            this.Datum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Datum.Name = "Datum";
            this.Datum.PasswordChar = '\0';
            this.Datum.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Datum.PlaceholderText = "";
            this.Datum.ReadOnly = true;
            this.Datum.SelectedText = "";
            this.Datum.Size = new System.Drawing.Size(606, 48);
            this.Datum.TabIndex = 57;
            this.Datum.TextChanged += new System.EventHandler(this.Datum_TextChanged);
            // 
            // Poziadavky
            // 
            this.Poziadavky.AutoScroll = true;
            this.Poziadavky.BorderColor = System.Drawing.Color.Black;
            this.Poziadavky.BorderRadius = 5;
            this.Poziadavky.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Poziadavky.DefaultText = "";
            this.Poziadavky.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Poziadavky.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Poziadavky.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Poziadavky.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Poziadavky.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Poziadavky.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.Poziadavky.ForeColor = System.Drawing.Color.Black;
            this.Poziadavky.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Poziadavky.Location = new System.Drawing.Point(178, 457);
            this.Poziadavky.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Poziadavky.Name = "Poziadavky";
            this.Poziadavky.PasswordChar = '\0';
            this.Poziadavky.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Poziadavky.PlaceholderText = "";
            this.Poziadavky.ReadOnly = true;
            this.Poziadavky.SelectedText = "";
            this.Poziadavky.Size = new System.Drawing.Size(606, 48);
            this.Poziadavky.TabIndex = 56;
            this.Poziadavky.TextChanged += new System.EventHandler(this.Poziadavky_TextChanged);
            // 
            // FriendRequest
            // 
            this.FriendRequest.Controls.Add(this.guna2Button2);
            this.FriendRequest.Controls.Add(this.guna2Button1);
            this.FriendRequest.Controls.Add(this.email);
            this.FriendRequest.Controls.Add(this.Nameandsurname);
            this.FriendRequest.Controls.Add(this.telnumber);
            this.FriendRequest.Controls.Add(this.pictureBox1);
            this.FriendRequest.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FriendRequest.Location = new System.Drawing.Point(4, 22);
            this.FriendRequest.Name = "FriendRequest";
            this.FriendRequest.Padding = new System.Windows.Forms.Padding(3);
            this.FriendRequest.Size = new System.Drawing.Size(967, 796);
            this.FriendRequest.TabIndex = 2;
            this.FriendRequest.Text = "Žiadosť o priateľstvo";
            this.FriendRequest.UseVisualStyleBackColor = true;
            // 
            // guna2Button2
            // 
            this.guna2Button2.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.Location = new System.Drawing.Point(292, 684);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(180, 45);
            this.guna2Button2.TabIndex = 0;
            this.guna2Button2.Text = "Prijať";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(519, 684);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(180, 45);
            this.guna2Button1.TabIndex = 11;
            this.guna2Button1.Text = "Odmietnuť";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // email
            // 
            this.email.BorderColor = System.Drawing.Color.Black;
            this.email.BorderRadius = 5;
            this.email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.email.DefaultText = "";
            this.email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.email.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.email.ForeColor = System.Drawing.Color.Black;
            this.email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.email.Location = new System.Drawing.Point(234, 596);
            this.email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.email.Name = "email";
            this.email.PasswordChar = '\0';
            this.email.PlaceholderForeColor = System.Drawing.Color.Black;
            this.email.PlaceholderText = "Email";
            this.email.ReadOnly = true;
            this.email.SelectedText = "";
            this.email.Size = new System.Drawing.Size(510, 50);
            this.email.TabIndex = 56;
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // Nameandsurname
            // 
            this.Nameandsurname.BorderColor = System.Drawing.Color.Black;
            this.Nameandsurname.BorderRadius = 5;
            this.Nameandsurname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Nameandsurname.DefaultText = "";
            this.Nameandsurname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Nameandsurname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Nameandsurname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Nameandsurname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Nameandsurname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Nameandsurname.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Nameandsurname.ForeColor = System.Drawing.Color.Black;
            this.Nameandsurname.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Nameandsurname.Location = new System.Drawing.Point(234, 414);
            this.Nameandsurname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Nameandsurname.Name = "Nameandsurname";
            this.Nameandsurname.PasswordChar = '\0';
            this.Nameandsurname.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Nameandsurname.PlaceholderText = "Name and Surname";
            this.Nameandsurname.ReadOnly = true;
            this.Nameandsurname.SelectedText = "";
            this.Nameandsurname.Size = new System.Drawing.Size(510, 50);
            this.Nameandsurname.TabIndex = 54;
            this.Nameandsurname.TextChanged += new System.EventHandler(this.Nameandsurname_TextChanged);
            // 
            // telnumber
            // 
            this.telnumber.BorderColor = System.Drawing.Color.Black;
            this.telnumber.BorderRadius = 5;
            this.telnumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.telnumber.DefaultText = "";
            this.telnumber.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.telnumber.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.telnumber.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.telnumber.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.telnumber.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.telnumber.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.telnumber.ForeColor = System.Drawing.Color.Black;
            this.telnumber.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.telnumber.Location = new System.Drawing.Point(234, 504);
            this.telnumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.telnumber.Name = "telnumber";
            this.telnumber.PasswordChar = '\0';
            this.telnumber.PlaceholderForeColor = System.Drawing.Color.Black;
            this.telnumber.PlaceholderText = "tel.number";
            this.telnumber.ReadOnly = true;
            this.telnumber.SelectedText = "";
            this.telnumber.Size = new System.Drawing.Size(510, 50);
            this.telnumber.TabIndex = 55;
            this.telnumber.TextChanged += new System.EventHandler(this.telnumber_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::kalendar.Properties.Resources.profilovka2;
            this.pictureBox1.Location = new System.Drawing.Point(331, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // guna2CircleButton3
            // 
            this.guna2CircleButton3.BackgroundImage = global::kalendar.Properties.Resources.Desktop___button;
            this.guna2CircleButton3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton3.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton3.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton3.Location = new System.Drawing.Point(1, 1);
            this.guna2CircleButton3.Name = "guna2CircleButton3";
            this.guna2CircleButton3.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton3.Size = new System.Drawing.Size(61, 61);
            this.guna2CircleButton3.TabIndex = 79;
            this.guna2CircleButton3.Click += new System.EventHandler(this.guna2CircleButton3_Click);
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.BackgroundImage = global::kalendar.Properties.Resources.Desktop___2button;
            this.guna2CircleButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2CircleButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2CircleButton1.FillColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.Location = new System.Drawing.Point(72, 0);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(66, 62);
            this.guna2CircleButton1.TabIndex = 81;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // Notifikacie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1798, 932);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.guna2CircleButton3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.containerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Notifikacie";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notifikacie";
            this.Load += new System.EventHandler(this.Notifikacie_Load);
            this.containerControl1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Event.ResumeLayout(false);
            this.FriendRequest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContainerControl containerControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Event;
        private System.Windows.Forms.TabPage FriendRequest;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2TextBox email;
        private Guna.UI2.WinForms.Guna2TextBox Nameandsurname;
        private Guna.UI2.WinForms.Guna2TextBox telnumber;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2TextBox Poziadavky;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox Datum;
        private Guna.UI2.WinForms.Guna2TextBox Názov;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox Miesto;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button Refuse;
        private Guna.UI2.WinForms.Guna2Button Accept;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton3;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
    }
}