namespace NotiHub
{
    partial class NotifWindow
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
            this.components = new System.ComponentModel.Container();
            this.nightForm1 = new ReaLTaiizor.Forms.NightForm();
            this.btnSnooze = new ReaLTaiizor.Controls.HopeButton();
            this.btnViewEvent = new ReaLTaiizor.Controls.HopeButton();
            this.btnGotIt = new ReaLTaiizor.Controls.HopeButton();
            this.iconBox = new System.Windows.Forms.PictureBox();
            this.lblTitle = new ReaLTaiizor.Controls.BigLabel();
            this.lblMessage = new ReaLTaiizor.Controls.BigLabel();
            this.lblTime = new ReaLTaiizor.Controls.BigLabel();
            this.separator = new System.Windows.Forms.Panel();
            this.panel1 = new ReaLTaiizor.Controls.Panel();
            this.panel3 = new ReaLTaiizor.Controls.Panel();
            this.parrotGradientPanel1 = new ReaLTaiizor.Controls.ParrotGradientPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new ReaLTaiizor.Controls.Panel();
            this.headerLabel = new ReaLTaiizor.Controls.BigLabel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.nightForm1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.nightForm1.Controls.Add(this.panel2);
            this.nightForm1.Controls.Add(this.btnSnooze);
            this.nightForm1.Controls.Add(this.btnViewEvent);
            this.nightForm1.Controls.Add(this.btnGotIt);
            this.nightForm1.Controls.Add(this.iconBox);
            this.nightForm1.Controls.Add(this.lblMessage);
            this.nightForm1.Controls.Add(this.lblTime);
            this.nightForm1.Controls.Add(this.separator);
            this.nightForm1.Controls.Add(this.headerPanel);
            this.nightForm1.Controls.Add(this.panel1);
            this.nightForm1.Controls.Add(this.panel3);
            this.nightForm1.Controls.Add(this.parrotGradientPanel1);
            this.nightForm1.Controls.Add(this.btnClose);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = false;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(498, 317);
            this.nightForm1.TabIndex = 3;
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // btnSnooze
            // 
            this.btnSnooze.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.btnSnooze.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            this.btnSnooze.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSnooze.DangerColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.btnSnooze.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSnooze.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSnooze.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.btnSnooze.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.btnSnooze.Location = new System.Drawing.Point(29, 248);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.PrimaryColor = System.Drawing.Color.Salmon;
            this.btnSnooze.Size = new System.Drawing.Size(125, 30);
            this.btnSnooze.SuccessColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(194)))), ((int)(((byte)(58)))));
            this.btnSnooze.TabIndex = 474;
            this.btnSnooze.Text = "SNOOZE 5 MIN";
            this.btnSnooze.TextColor = System.Drawing.Color.White;
            this.btnSnooze.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(162)))), ((int)(((byte)(60)))));
            this.btnSnooze.Click += new System.EventHandler(this.btnSnooze_Click);
            // 
            // btnViewEvent
            // 
            this.btnViewEvent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.btnViewEvent.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            this.btnViewEvent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewEvent.DangerColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.btnViewEvent.DefaultColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnViewEvent.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewEvent.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.btnViewEvent.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.btnViewEvent.Location = new System.Drawing.Point(188, 248);
            this.btnViewEvent.Name = "btnViewEvent";
            this.btnViewEvent.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.btnViewEvent.Size = new System.Drawing.Size(125, 30);
            this.btnViewEvent.SuccessColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(194)))), ((int)(((byte)(58)))));
            this.btnViewEvent.TabIndex = 475;
            this.btnViewEvent.Text = "VIEW EVENT";
            this.btnViewEvent.TextColor = System.Drawing.Color.White;
            this.btnViewEvent.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(162)))), ((int)(((byte)(60)))));
            this.btnViewEvent.Click += new System.EventHandler(this.btnViewEvent_Click);
            // 
            // btnGotIt
            // 
            this.btnGotIt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.btnGotIt.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            this.btnGotIt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGotIt.DangerColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.btnGotIt.DefaultColor = System.Drawing.Color.Silver;
            this.btnGotIt.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGotIt.HoverTextColor = System.Drawing.Color.White;
            this.btnGotIt.InfoColor = System.Drawing.Color.Gray;
            this.btnGotIt.Location = new System.Drawing.Point(344, 248);
            this.btnGotIt.Name = "btnGotIt";
            this.btnGotIt.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(194)))), ((int)(((byte)(58)))));
            this.btnGotIt.Size = new System.Drawing.Size(125, 30);
            this.btnGotIt.SuccessColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(194)))), ((int)(((byte)(58)))));
            this.btnGotIt.TabIndex = 473;
            this.btnGotIt.Text = "✓ GOT IT";
            this.btnGotIt.TextColor = System.Drawing.Color.WhiteSmoke;
            this.btnGotIt.WarningColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(162)))), ((int)(((byte)(60)))));
            // 
            // iconBox
            // 
            this.iconBox.BackColor = System.Drawing.Color.Transparent;
            this.iconBox.Image = global::NotiHub.Properties.Resources.clock_alert_48px;
            this.iconBox.Location = new System.Drawing.Point(29, 131);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(70, 65);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.iconBox.TabIndex = 262;
            this.iconBox.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblTitle.Location = new System.Drawing.Point(38, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 19);
            this.lblTitle.TabIndex = 263;
            this.lblTitle.Text = "EVENT TITLE";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblMessage.Location = new System.Drawing.Point(152, 129);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(335, 55);
            this.lblMessage.TabIndex = 264;
            this.lblMessage.Text = "EVENT MESSAGE\r\nTEST\r\nTEST";
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblTime.Location = new System.Drawing.Point(152, 187);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(335, 20);
            this.lblTime.TabIndex = 265;
            this.lblTime.Text = "00:00:00 AM";
            // 
            // separator
            // 
            this.separator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.separator.Location = new System.Drawing.Point(1, 217);
            this.separator.Name = "separator";
            this.separator.Size = new System.Drawing.Size(499, 1);
            this.separator.TabIndex = 266;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel1.Location = new System.Drawing.Point(497, 31);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1, 278);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 260;
            this.panel1.Text = "panel1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(1, 278);
            this.panel3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel3.TabIndex = 259;
            this.panel3.Text = "panel3";
            // 
            // parrotGradientPanel1
            // 
            this.parrotGradientPanel1.BottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.parrotGradientPanel1.BottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.parrotGradientPanel1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.parrotGradientPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.parrotGradientPanel1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.parrotGradientPanel1.Location = new System.Drawing.Point(0, 309);
            this.parrotGradientPanel1.Name = "parrotGradientPanel1";
            this.parrotGradientPanel1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotGradientPanel1.PrimerColor = System.Drawing.Color.White;
            this.parrotGradientPanel1.Size = new System.Drawing.Size(498, 8);
            this.parrotGradientPanel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotGradientPanel1.Style = ReaLTaiizor.Controls.ParrotGradientPanel.GradientStyle.Corners;
            this.parrotGradientPanel1.TabIndex = 12;
            this.parrotGradientPanel1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.parrotGradientPanel1.TopLeft = System.Drawing.Color.Lime;
            this.parrotGradientPanel1.TopRight = System.Drawing.Color.Red;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.Location = new System.Drawing.Point(464, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(34, 31);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.lblTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 45);
            this.panel2.TabIndex = 478;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel6.Location = new System.Drawing.Point(0, 44);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(496, 1);
            this.panel6.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel6.TabIndex = 11;
            this.panel6.Text = "panel6";
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.Transparent;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(25, 7);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(157, 25);
            this.headerLabel.TabIndex = 1;
            this.headerLabel.Text = "🔔 Event Reminder";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(34)))));
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(1, 31);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(496, 40);
            this.headerPanel.TabIndex = 261;
            // 
            // NotifWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 317);
            this.Controls.Add(this.nightForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1366, 738);
            this.Name = "NotifWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NotifWindow";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.nightForm1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.NightForm nightForm1;
        private ReaLTaiizor.Controls.Panel panel1;
        private ReaLTaiizor.Controls.Panel panel3;
        private ReaLTaiizor.Controls.ParrotGradientPanel parrotGradientPanel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox iconBox;
        private ReaLTaiizor.Controls.BigLabel lblTitle;
        private ReaLTaiizor.Controls.BigLabel lblMessage;
        private ReaLTaiizor.Controls.BigLabel lblTime;
        private System.Windows.Forms.Panel separator;
        private ReaLTaiizor.Controls.HopeButton btnSnooze;
        private ReaLTaiizor.Controls.HopeButton btnViewEvent;
        private ReaLTaiizor.Controls.HopeButton btnGotIt;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Panel panel2;
        private ReaLTaiizor.Controls.Panel panel6;
        private System.Windows.Forms.Panel headerPanel;
        private ReaLTaiizor.Controls.BigLabel headerLabel;
    }
}