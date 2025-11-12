namespace NotiHub
{
    partial class Main
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
            this.nightForm1 = new ReaLTaiizor.Forms.NightForm();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelCDate = new System.Windows.Forms.Label();
            this.btnCDate = new System.Windows.Forms.Button();
            this.labelCurrentSignedAccount = new System.Windows.Forms.Label();
            this.panel7 = new ReaLTaiizor.Controls.Panel();
            this.panel3 = new ReaLTaiizor.Controls.Panel();
            this.panel2 = new ReaLTaiizor.Controls.Panel();
            this.panel1 = new ReaLTaiizor.Controls.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel5 = new ReaLTaiizor.Controls.Panel();
            this.panelIndicator = new ReaLTaiizor.Controls.Panel();
            this.parrotGradientPanel1 = new ReaLTaiizor.Controls.ParrotGradientPanel();
            this.btnGithub = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNotification = new System.Windows.Forms.Button();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.eventNotes1 = new NotiHub.EventNotes();
            this.calendarSchedule1 = new NotiHub.CalendarSchedule();
            this.nightForm1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nightForm1
            // 
            this.nightForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.nightForm1.Controls.Add(this.eventNotes1);
            this.nightForm1.Controls.Add(this.calendarSchedule1);
            this.nightForm1.Controls.Add(this.panel4);
            this.nightForm1.Controls.Add(this.panel3);
            this.nightForm1.Controls.Add(this.panel2);
            this.nightForm1.Controls.Add(this.btnMinimize);
            this.nightForm1.Controls.Add(this.btnMaximize);
            this.nightForm1.Controls.Add(this.btnClose);
            this.nightForm1.Controls.Add(this.panel5);
            this.nightForm1.Controls.Add(this.parrotGradientPanel1);
            this.nightForm1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nightForm1.DrawIcon = false;
            this.nightForm1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nightForm1.HeadColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.nightForm1.Location = new System.Drawing.Point(0, 0);
            this.nightForm1.MinimumSize = new System.Drawing.Size(100, 42);
            this.nightForm1.Name = "nightForm1";
            this.nightForm1.Padding = new System.Windows.Forms.Padding(0, 31, 0, 0);
            this.nightForm1.Size = new System.Drawing.Size(1068, 640);
            this.nightForm1.TabIndex = 1;
            this.nightForm1.TextAlignment = ReaLTaiizor.Forms.NightForm.Alignment.Left;
            this.nightForm1.TitleBarTextColor = System.Drawing.Color.Gainsboro;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel4.Controls.Add(this.labelCDate);
            this.panel4.Controls.Add(this.btnCDate);
            this.panel4.Controls.Add(this.labelCurrentSignedAccount);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(245, 589);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(823, 43);
            this.panel4.TabIndex = 372;
            // 
            // labelCDate
            // 
            this.labelCDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCDate.AutoSize = true;
            this.labelCDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCDate.ForeColor = System.Drawing.Color.DarkGray;
            this.labelCDate.Location = new System.Drawing.Point(685, 15);
            this.labelCDate.Name = "labelCDate";
            this.labelCDate.Size = new System.Drawing.Size(79, 13);
            this.labelCDate.TabIndex = 281;
            this.labelCDate.Text = "Current Date :";
            this.labelCDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCDate.Visible = false;
            // 
            // btnCDate
            // 
            this.btnCDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCDate.BackColor = System.Drawing.Color.LightGreen;
            this.btnCDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCDate.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnCDate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGreen;
            this.btnCDate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGreen;
            this.btnCDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCDate.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCDate.Location = new System.Drawing.Point(770, 15);
            this.btnCDate.Name = "btnCDate";
            this.btnCDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCDate.Size = new System.Drawing.Size(15, 14);
            this.btnCDate.TabIndex = 280;
            this.btnCDate.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnCDate.UseVisualStyleBackColor = false;
            this.btnCDate.Visible = false;
            // 
            // labelCurrentSignedAccount
            // 
            this.labelCurrentSignedAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCurrentSignedAccount.AutoSize = true;
            this.labelCurrentSignedAccount.Font = new System.Drawing.Font("Bahnschrift SemiBold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelCurrentSignedAccount.ForeColor = System.Drawing.Color.DarkGray;
            this.labelCurrentSignedAccount.Location = new System.Drawing.Point(33, 14);
            this.labelCurrentSignedAccount.Name = "labelCurrentSignedAccount";
            this.labelCurrentSignedAccount.Size = new System.Drawing.Size(62, 14);
            this.labelCurrentSignedAccount.TabIndex = 279;
            this.labelCurrentSignedAccount.Text = "ACCOUNT:";
            this.labelCurrentSignedAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(5);
            this.panel7.Size = new System.Drawing.Size(823, 1);
            this.panel7.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel7.TabIndex = 267;
            this.panel7.Text = "panel7";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Location = new System.Drawing.Point(244, 31);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(1, 601);
            this.panel3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel3.TabIndex = 208;
            this.panel3.Text = "panel3";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel2.Controls.Add(this.btnGithub);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnSettings);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnNotification);
            this.panel2.Controls.Add(this.btnCalendar);
            this.panel2.Controls.Add(this.btnNotes);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel2.Location = new System.Drawing.Point(10, 31);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(234, 601);
            this.panel2.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel2.TabIndex = 207;
            this.panel2.Text = "panel2";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel1.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel1.Location = new System.Drawing.Point(15, 231);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(180, 1);
            this.panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel1.TabIndex = 287;
            this.panel1.Text = "panel1";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Berlin Sans FB", 9.55F);
            this.label25.ForeColor = System.Drawing.Color.Gray;
            this.label25.Location = new System.Drawing.Point(69, 77);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(111, 15);
            this.label25.TabIndex = 285;
            this.label25.Text = "Event Management";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Lucida Handwriting", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Cornsilk;
            this.label26.Location = new System.Drawing.Point(71, 56);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(69, 16);
            this.label26.TabIndex = 284;
            this.label26.Text = "NotiHub";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnClose.Location = new System.Drawing.Point(1034, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(34, 31);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel5.Controls.Add(this.panelIndicator);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel5.Location = new System.Drawing.Point(0, 31);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(5);
            this.panel5.Size = new System.Drawing.Size(10, 601);
            this.panel5.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel5.TabIndex = 206;
            this.panel5.Text = "panel5";
            // 
            // panelIndicator
            // 
            this.panelIndicator.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panelIndicator.EdgeColor = System.Drawing.Color.LightGreen;
            this.panelIndicator.Location = new System.Drawing.Point(-1, 132);
            this.panelIndicator.Name = "panelIndicator";
            this.panelIndicator.Padding = new System.Windows.Forms.Padding(5);
            this.panelIndicator.Size = new System.Drawing.Size(4, 24);
            this.panelIndicator.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panelIndicator.TabIndex = 208;
            this.panelIndicator.Text = "panel7";
            // 
            // parrotGradientPanel1
            // 
            this.parrotGradientPanel1.BottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.parrotGradientPanel1.BottomRight = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.parrotGradientPanel1.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            this.parrotGradientPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.parrotGradientPanel1.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.parrotGradientPanel1.Location = new System.Drawing.Point(0, 632);
            this.parrotGradientPanel1.Name = "parrotGradientPanel1";
            this.parrotGradientPanel1.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.parrotGradientPanel1.PrimerColor = System.Drawing.Color.White;
            this.parrotGradientPanel1.Size = new System.Drawing.Size(1068, 8);
            this.parrotGradientPanel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.parrotGradientPanel1.Style = ReaLTaiizor.Controls.ParrotGradientPanel.GradientStyle.Corners;
            this.parrotGradientPanel1.TabIndex = 297;
            this.parrotGradientPanel1.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.parrotGradientPanel1.TopLeft = System.Drawing.Color.Lime;
            this.parrotGradientPanel1.TopRight = System.Drawing.Color.Red;
            // 
            // btnGithub
            // 
            this.btnGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGithub.BackColor = System.Drawing.Color.Transparent;
            this.btnGithub.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGithub.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.btnGithub.FlatAppearance.BorderSize = 0;
            this.btnGithub.FlatAppearance.MouseDownBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGithub.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGithub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGithub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(53)))), ((int)(((byte)(44)))));
            this.btnGithub.Image = global::NotiHub.Properties.Resources.github_24px;
            this.btnGithub.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGithub.Location = new System.Drawing.Point(94, 551);
            this.btnGithub.Name = "btnGithub";
            this.btnGithub.Size = new System.Drawing.Size(31, 31);
            this.btnGithub.TabIndex = 288;
            this.btnGithub.UseVisualStyleBackColor = false;
            this.btnGithub.Click += new System.EventHandler(this.btnGithub_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.DarkGray;
            this.btnSettings.Image = global::NotiHub.Properties.Resources.settings_24px;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSettings.Location = new System.Drawing.Point(12, 235);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSettings.Size = new System.Drawing.Size(209, 38);
            this.btnSettings.TabIndex = 286;
            this.btnSettings.Text = "       Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NotiHub.Properties.Resources.planner_48px;
            this.pictureBox1.Location = new System.Drawing.Point(16, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 283;
            this.pictureBox1.TabStop = false;
            // 
            // btnNotification
            // 
            this.btnNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotification.BackColor = System.Drawing.Color.Transparent;
            this.btnNotification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNotification.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnNotification.FlatAppearance.BorderSize = 0;
            this.btnNotification.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnNotification.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotification.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotification.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnNotification.Image = global::NotiHub.Properties.Resources.alarm_24px;
            this.btnNotification.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNotification.Location = new System.Drawing.Point(176, 4);
            this.btnNotification.Name = "btnNotification";
            this.btnNotification.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNotification.Size = new System.Drawing.Size(49, 38);
            this.btnNotification.TabIndex = 238;
            this.btnNotification.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnNotification.UseVisualStyleBackColor = false;
            this.btnNotification.Click += new System.EventHandler(this.btnNotification_Click);
            // 
            // btnCalendar
            // 
            this.btnCalendar.BackColor = System.Drawing.Color.Transparent;
            this.btnCalendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCalendar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCalendar.FlatAppearance.BorderSize = 0;
            this.btnCalendar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCalendar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCalendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendar.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalendar.ForeColor = System.Drawing.Color.DarkGray;
            this.btnCalendar.Image = global::NotiHub.Properties.Resources.schedule_24px;
            this.btnCalendar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalendar.Location = new System.Drawing.Point(12, 180);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCalendar.Size = new System.Drawing.Size(208, 38);
            this.btnCalendar.TabIndex = 204;
            this.btnCalendar.Text = "    Calendar";
            this.btnCalendar.UseVisualStyleBackColor = false;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // btnNotes
            // 
            this.btnNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnNotes.FlatAppearance.BorderSize = 0;
            this.btnNotes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnNotes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotes.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotes.ForeColor = System.Drawing.Color.White;
            this.btnNotes.Image = global::NotiHub.Properties.Resources.to_do_24px;
            this.btnNotes.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNotes.Location = new System.Drawing.Point(12, 125);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNotes.Size = new System.Drawing.Size(208, 38);
            this.btnNotes.TabIndex = 202;
            this.btnNotes.Text = "          Notes";
            this.btnNotes.UseVisualStyleBackColor = false;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnMinimize.BackgroundImage = global::NotiHub.Properties.Resources.minus_20px;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMinimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Location = new System.Drawing.Point(956, 5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(21, 19);
            this.btnMinimize.TabIndex = 9;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnMaximize.BackgroundImage = global::NotiHub.Properties.Resources.maximize_button_20px;
            this.btnMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMaximize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Location = new System.Drawing.Point(999, 5);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(21, 19);
            this.btnMaximize.TabIndex = 8;
            this.btnMaximize.UseVisualStyleBackColor = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // eventNotes1
            // 
            this.eventNotes1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventNotes1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.eventNotes1.Location = new System.Drawing.Point(248, 35);
            this.eventNotes1.Name = "eventNotes1";
            this.eventNotes1.Size = new System.Drawing.Size(814, 548);
            this.eventNotes1.TabIndex = 375;
            // 
            // calendarSchedule1
            // 
            this.calendarSchedule1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calendarSchedule1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.calendarSchedule1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.calendarSchedule1.Location = new System.Drawing.Point(248, 35);
            this.calendarSchedule1.Name = "calendarSchedule1";
            this.calendarSchedule1.Size = new System.Drawing.Size(814, 548);
            this.calendarSchedule1.TabIndex = 373;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 640);
            this.Controls.Add(this.nightForm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1366, 738);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Main_Load);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.nightForm1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Forms.NightForm nightForm1;
        private ReaLTaiizor.Controls.Panel panel3;
        private ReaLTaiizor.Controls.Panel panel2;
        private ReaLTaiizor.Controls.Panel panel1;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNotification;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnClose;
        private ReaLTaiizor.Controls.Panel panel5;
        private ReaLTaiizor.Controls.Panel panelIndicator;
        private ReaLTaiizor.Controls.ParrotGradientPanel parrotGradientPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelCurrentSignedAccount;
        private ReaLTaiizor.Controls.Panel panel7;
        private CalendarSchedule calendarSchedule1;
        private EventNotes eventNotes1;
        private System.Windows.Forms.Button btnCDate;
        private System.Windows.Forms.Label labelCDate;
        private System.Windows.Forms.Button btnGithub;
    }
}