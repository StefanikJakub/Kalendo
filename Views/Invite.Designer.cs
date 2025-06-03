namespace kalendar.Views
{
    partial class Invite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invite));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Search = new Guna.UI2.WinForms.Guna2TextBox();
            this.PozvankaUdalost = new Guna.UI2.WinForms.Guna2Button();
            this.Button = new Guna.UI2.WinForms.Guna2Button();
            this.email = new Guna.UI2.WinForms.Guna2TextBox();
            this.telnumber = new Guna.UI2.WinForms.Guna2TextBox();
            this.Nameandsurname = new Guna.UI2.WinForms.Guna2TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.webService1 = new System.Web.Services.WebService();
            this.guna2CircleButton3 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(136, 100);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.Search);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.PozvankaUdalost);
            this.splitContainer1.Panel2.Controls.Add(this.Button);
            this.splitContainer1.Panel2.Controls.Add(this.email);
            this.splitContainer1.Panel2.Controls.Add(this.telnumber);
            this.splitContainer1.Panel2.Controls.Add(this.Nameandsurname);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1632, 840);
            this.splitContainer1.SplitterDistance = 582;
            this.splitContainer1.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(38, 142);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(510, 654);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Search
            // 
            this.Search.AutoRoundedCorners = true;
            this.Search.BorderColor = System.Drawing.Color.Black;
            this.Search.BorderRadius = 24;
            this.Search.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Search.DefaultText = "";
            this.Search.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Search.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Search.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Search.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Search.ForeColor = System.Drawing.Color.Black;
            this.Search.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Search.IconLeft = global::kalendar.Properties.Resources.pngwing_com11;
            this.Search.IconLeftOffset = new System.Drawing.Point(5, 2);
            this.Search.IconLeftSize = new System.Drawing.Size(40, 40);
            this.Search.Location = new System.Drawing.Point(38, 46);
            this.Search.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Search.Name = "Search";
            this.Search.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.Search.PasswordChar = '\0';
            this.Search.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Search.PlaceholderText = "";
            this.Search.SelectedText = "";
            this.Search.Size = new System.Drawing.Size(510, 50);
            this.Search.TabIndex = 5;
            this.Search.TextChanged += new System.EventHandler(this.guna2TextBox5_TextChanged);
            this.Search.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2TextBox5_Paint);
            // 
            // PozvankaUdalost
            // 
            this.PozvankaUdalost.AutoRoundedCorners = true;
            this.PozvankaUdalost.BorderRadius = 22;
            this.PozvankaUdalost.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.PozvankaUdalost.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.PozvankaUdalost.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.PozvankaUdalost.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.PozvankaUdalost.FillColor = System.Drawing.Color.CornflowerBlue;
            this.PozvankaUdalost.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.PozvankaUdalost.ForeColor = System.Drawing.Color.White;
            this.PozvankaUdalost.Location = new System.Drawing.Point(560, 705);
            this.PozvankaUdalost.Name = "PozvankaUdalost";
            this.PozvankaUdalost.Size = new System.Drawing.Size(217, 46);
            this.PozvankaUdalost.TabIndex = 54;
            this.PozvankaUdalost.Text = "Pozvať na udalosť";
            this.PozvankaUdalost.Click += new System.EventHandler(this.PozvankaUdalost_Click);
            // 
            // Button
            // 
            this.Button.AutoRoundedCorners = true;
            this.Button.BorderRadius = 22;
            this.Button.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button.FillColor = System.Drawing.Color.CornflowerBlue;
            this.Button.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Button.ForeColor = System.Drawing.Color.White;
            this.Button.Location = new System.Drawing.Point(267, 705);
            this.Button.Name = "Button";
            this.Button.Size = new System.Drawing.Size(263, 46);
            this.Button.TabIndex = 53;
            this.Button.Text = "Pošlite žiadosť o priateľstvo";
            this.Button.Click += new System.EventHandler(this.Button_Click);
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
            this.email.Location = new System.Drawing.Point(267, 628);
            this.email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.email.Name = "email";
            this.email.PasswordChar = '\0';
            this.email.PlaceholderForeColor = System.Drawing.Color.Black;
            this.email.PlaceholderText = "Email";
            this.email.ReadOnly = true;
            this.email.SelectedText = "";
            this.email.Size = new System.Drawing.Size(510, 50);
            this.email.TabIndex = 5;
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
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
            this.telnumber.Location = new System.Drawing.Point(267, 536);
            this.telnumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.telnumber.Name = "telnumber";
            this.telnumber.PasswordChar = '\0';
            this.telnumber.PlaceholderForeColor = System.Drawing.Color.Black;
            this.telnumber.PlaceholderText = "Telefónne číslo";
            this.telnumber.ReadOnly = true;
            this.telnumber.SelectedText = "";
            this.telnumber.Size = new System.Drawing.Size(510, 50);
            this.telnumber.TabIndex = 4;
            this.telnumber.TextChanged += new System.EventHandler(this.telnumber_TextChanged);
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
            this.Nameandsurname.Location = new System.Drawing.Point(267, 446);
            this.Nameandsurname.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Nameandsurname.Name = "Nameandsurname";
            this.Nameandsurname.PasswordChar = '\0';
            this.Nameandsurname.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Nameandsurname.PlaceholderText = "Meno a priezvisko";
            this.Nameandsurname.ReadOnly = true;
            this.Nameandsurname.SelectedText = "";
            this.Nameandsurname.Size = new System.Drawing.Size(510, 50);
            this.Nameandsurname.TabIndex = 3;
            this.Nameandsurname.TextChanged += new System.EventHandler(this.Nameandsurname_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::kalendar.Properties.Resources.profilovka2;
            this.pictureBox1.Location = new System.Drawing.Point(373, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 320);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
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
            this.guna2CircleButton3.Size = new System.Drawing.Size(68, 64);
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
            this.guna2CircleButton1.Size = new System.Drawing.Size(68, 64);
            this.guna2CircleButton1.TabIndex = 81;
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // Invite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.guna2CircleButton1);
            this.Controls.Add(this.guna2CircleButton3);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Invite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invite";
            this.Load += new System.EventHandler(this.Invite_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TextBox email;
        private Guna.UI2.WinForms.Guna2TextBox telnumber;
        private Guna.UI2.WinForms.Guna2TextBox Nameandsurname;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2TextBox Search;
        private System.Web.Services.WebService webService1;
        private Guna.UI2.WinForms.Guna2Button Button;
        private Guna.UI2.WinForms.Guna2Button PozvankaUdalost;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton3;
        private Guna.UI2.WinForms.Guna2CircleButton guna2CircleButton1;
    }
}