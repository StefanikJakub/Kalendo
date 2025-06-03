using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace kalendar.Views
{
    public partial class UserControlDays : UserControl
    {
        private readonly string userId;
        private readonly int month;
        private readonly int year;
        private readonly ToolTip toolTip = new ToolTip();
        private int day;
        private Menu1 menuForm;
        private bool isHovered = false;
        private Color originalBackColor;
        private Color hoverBackColor;

        private Color borderColor = Color.Gray;
        private const int BorderWidth = 2;

        public UserControlDays(string userId, int month, int year)
        {
            InitializeComponent();
            this.userId = userId;
            this.month = month;
            this.year = year;

            // Set up control
            this.DoubleBuffered = true;
            this.BorderStyle = BorderStyle.None;
            this.Padding = new Padding(BorderWidth);

            // Initialize events
            this.MouseEnter += UserControlDays_MouseEnter;
            this.MouseLeave += UserControlDays_MouseLeave;
            this.Click += UserControlDays_Click;

            // Initialize events for child controls
            SubscribeChildControls(this);
        }

        private void SubscribeChildControls(Control control)
        {
            foreach (Control child in control.Controls)
            {
                child.MouseEnter += Control_MouseEnter;
                child.MouseLeave += Control_MouseLeave;
                child.Click += ChildControl_Click;
                SubscribeChildControls(child);
            }
        }

        private void ChildControl_Click(object sender, EventArgs e)
        {
            UserControlDays_Click(sender, e);
        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {
            UserControlDays_MouseEnter(sender, e);
        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                UserControlDays_MouseLeave(sender, e);
            }
        }

        private void UserControlDays_MouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            this.Cursor = Cursors.Hand;
            SetChildCursors(Cursors.Hand);
            UpdateAppearance();
        }

        private void UserControlDays_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            {
                isHovered = false;
                this.Cursor = Cursors.Default;
                SetChildCursors(Cursors.Default);
                UpdateAppearance();
            }
        }

        private void SetChildCursors(Cursor cursor)
        {
            foreach (Control child in this.Controls)
            {
                child.Cursor = cursor;
            }
        }

        private void UpdateAppearance()
        {
            if (isHovered)
            {
                this.BackColor = hoverBackColor;
                this.Padding = new Padding(BorderWidth - 1); // Slightly reduce padding to make border appear thicker
                borderColor = Color.FromArgb(100, 150, 255);
            }
            else
            {
                this.BackColor = originalBackColor;
                this.Padding = new Padding(BorderWidth);
                borderColor = Color.Gray;
            }

            this.Invalidate(); // Force redraw to update border
        }

        public void SetDay(int dayNum, List<string> events, bool isRecurringOnly)
        {
            SuspendLayout();
            lbdays.Text = dayNum.ToString();
            day = dayNum;

            // Set default colors
            Color textColor = Color.Black;
            Color backColor = Color.FromArgb(185,185,185);
            Color borderColor = Color.FromArgb(200, 200, 200);

            if (events.Count > 0)
            {
                label1.Text = string.Join("\n", events);
                SetToolTips(label1.Text);

                if (isRecurringOnly)
                {
                    // Recurring events - softer blue
                    backColor = Color.FromArgb(60, 150, 255);
                    borderColor = Color.FromArgb(100, 160, 220);
                }
                else
                {
                    // One-time events - soft green
                    backColor = Color.FromArgb(250, 104, 0); 

                    borderColor = Color.FromArgb(100, 200, 150);
                }
            }
            else
            {
                label1.Text = string.Empty;
                ClearToolTips();
            }

            // Highlight current day - gold with dark text
            if (day == DateTime.Now.Day && month == DateTime.Now.Month && year == DateTime.Now.Year)
            {
                backColor = Color.FromArgb(255,215,0); // Light gold
                
                borderColor = Color.FromArgb(220, 180, 60);
            }

            // Apply colors
            this.BackColor = backColor;
            this.ForeColor = textColor;
            lbdays.ForeColor = textColor;
            label1.ForeColor = textColor;
            this.borderColor = borderColor;

            originalBackColor = this.BackColor;
            hoverBackColor = ControlPaint.Light(originalBackColor, 0.3f); // Slightly lighter on hover

            ResumeLayout(false);
            Invalidate(); // Refresh to update border
        }

        private void SetToolTips(string text)
        {
            toolTip.SetToolTip(this, text);
            foreach (Control child in this.Controls)
            {
                toolTip.SetToolTip(child, text);
            }
        }

        private void ClearToolTips()
        {
            toolTip.SetToolTip(this, null);
            foreach (Control child in this.Controls)
            {
                toolTip.SetToolTip(child, null);
            }
        }

        public void SetMenuForm(Menu1 form) => menuForm = form;

        private void UserControlDays_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lbdays.Text, out int parsedDay))
            {
                day = parsedDay;
                var eventForm = new Views.Event(userId, month, year, day);
                eventForm.Show();
                menuForm?.Hide();
            }
        }

        private void label1_Click(object sender, EventArgs e) => UserControlDays_Click(sender, e);

        private void UserControlDays_Load(object sender, EventArgs e)
        {
        }
    }
}