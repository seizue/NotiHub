namespace NotiHub
{
    partial class AuditTrail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel4 = new ReaLTaiizor.Controls.Panel();
            this.btnCSV = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.panel13 = new ReaLTaiizor.Controls.Panel();
            this.labelProductPromo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.datePickerEnd = new NotiHub.CustomDatePicker();
            this.datePickerStart = new NotiHub.CustomDatePicker();
            this.comboBoxSelection = new ReaLTaiizor.Controls.DungeonComboBox();
            this.txtSearch = new ReaLTaiizor.Controls.HopeTextBox();
            this.btnClearSearchText = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel3 = new ReaLTaiizor.Controls.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblPageInfo = new System.Windows.Forms.TextBox();
            this.panel6 = new ReaLTaiizor.Controls.Panel();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.panel7 = new ReaLTaiizor.Controls.Panel();
            this.dataGridAuditTrail = new ReaLTaiizor.Controls.PoisonDataGridView();
            this.auID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auEventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auEventDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auEventDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auEventLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auActionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.auTimestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAuditTrail)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnCSV);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.panel13);
            this.panel1.Controls.Add(this.labelProductPromo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 49);
            this.panel1.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.DarkGray;
            this.btnRefresh.Image = global::NotiHub.Properties.Resources.database_backup_24px;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRefresh.Location = new System.Drawing.Point(703, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRefresh.Size = new System.Drawing.Size(34, 30);
            this.btnRefresh.TabIndex = 373;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel4.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel4.Location = new System.Drawing.Point(746, 15);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(1, 20);
            this.panel4.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel4.TabIndex = 372;
            this.panel4.Text = "panel4";
            // 
            // btnCSV
            // 
            this.btnCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCSV.BackColor = System.Drawing.Color.Transparent;
            this.btnCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCSV.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCSV.FlatAppearance.BorderSize = 0;
            this.btnCSV.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCSV.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.btnCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCSV.Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCSV.ForeColor = System.Drawing.Color.DarkGray;
            this.btnCSV.Image = global::NotiHub.Properties.Resources.export_csv_24px;
            this.btnCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCSV.Location = new System.Drawing.Point(755, 10);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCSV.Size = new System.Drawing.Size(34, 30);
            this.btnCSV.TabIndex = 370;
            this.btnCSV.UseVisualStyleBackColor = false;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Lucida Handwriting", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Gray;
            this.label26.Location = new System.Drawing.Point(103, 17);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(69, 16);
            this.label26.TabIndex = 362;
            this.label26.Text = "NotiHub";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel13.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel13.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel13.Location = new System.Drawing.Point(0, 48);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(5);
            this.panel13.Size = new System.Drawing.Size(814, 1);
            this.panel13.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel13.TabIndex = 359;
            this.panel13.Text = "panel13";
            // 
            // labelProductPromo
            // 
            this.labelProductPromo.AutoSize = true;
            this.labelProductPromo.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductPromo.ForeColor = System.Drawing.Color.Silver;
            this.labelProductPromo.Location = new System.Drawing.Point(22, 15);
            this.labelProductPromo.Name = "labelProductPromo";
            this.labelProductPromo.Size = new System.Drawing.Size(84, 18);
            this.labelProductPromo.TabIndex = 355;
            this.labelProductPromo.Text = "AUDIT TRAIL";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.datePickerEnd);
            this.panel2.Controls.Add(this.datePickerStart);
            this.panel2.Controls.Add(this.comboBoxSelection);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.btnClearSearchText);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 59);
            this.panel2.TabIndex = 434;
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.BorderColor = System.Drawing.Color.Gray;
            this.datePickerEnd.BorderSize = 0;
            this.datePickerEnd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.datePickerEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.datePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerEnd.Location = new System.Drawing.Point(353, 12);
            this.datePickerEnd.MinimumSize = new System.Drawing.Size(4, 35);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.ShowIconOnly = false;
            this.datePickerEnd.Size = new System.Drawing.Size(133, 35);
            this.datePickerEnd.TabIndex = 460;
            this.datePickerEnd.TextColor = System.Drawing.Color.White;
            this.datePickerEnd.ValueChanged += new System.EventHandler(this.datePickerEnd_ValueChanged);
            // 
            // datePickerStart
            // 
            this.datePickerStart.BorderColor = System.Drawing.Color.Gray;
            this.datePickerStart.BorderSize = 0;
            this.datePickerStart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.datePickerStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.datePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerStart.Location = new System.Drawing.Point(212, 12);
            this.datePickerStart.MinimumSize = new System.Drawing.Size(4, 35);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.ShowIconOnly = false;
            this.datePickerStart.Size = new System.Drawing.Size(133, 35);
            this.datePickerStart.TabIndex = 459;
            this.datePickerStart.TextColor = System.Drawing.Color.White;
            this.datePickerStart.ValueChanged += new System.EventHandler(this.datePickerStart_ValueChanged);
            // 
            // comboBoxSelection
            // 
            this.comboBoxSelection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.ColorA = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(132)))), ((int)(((byte)(85)))));
            this.comboBoxSelection.ColorB = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(108)))), ((int)(((byte)(57)))));
            this.comboBoxSelection.ColorC = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.ColorD = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.ColorE = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.ColorF = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.comboBoxSelection.ColorG = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(118)))));
            this.comboBoxSelection.ColorH = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.ColorI = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.comboBoxSelection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxSelection.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSelection.DropDownHeight = 160;
            this.comboBoxSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelection.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxSelection.ForeColor = System.Drawing.Color.White;
            this.comboBoxSelection.FormattingEnabled = true;
            this.comboBoxSelection.HoverSelectionColor = System.Drawing.Color.Transparent;
            this.comboBoxSelection.IntegralHeight = false;
            this.comboBoxSelection.ItemHeight = 25;
            this.comboBoxSelection.Location = new System.Drawing.Point(25, 13);
            this.comboBoxSelection.Name = "comboBoxSelection";
            this.comboBoxSelection.Size = new System.Drawing.Size(168, 31);
            this.comboBoxSelection.StartIndex = 0;
            this.comboBoxSelection.TabIndex = 458;
            this.comboBoxSelection.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelection_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.txtSearch.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.txtSearch.BorderColorA = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.txtSearch.BorderColorB = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.txtSearch.Font = new System.Drawing.Font("Calibri", 9.25F, System.Drawing.FontStyle.Bold);
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Hint = "Search";
            this.txtSearch.Location = new System.Drawing.Point(547, 12);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.Size = new System.Drawing.Size(206, 32);
            this.txtSearch.TabIndex = 435;
            this.txtSearch.TabStop = false;
            this.txtSearch.UseSystemPasswordChar = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnClearSearchText
            // 
            this.btnClearSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSearchText.BackColor = System.Drawing.Color.Transparent;
            this.btnClearSearchText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClearSearchText.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnClearSearchText.FlatAppearance.BorderSize = 0;
            this.btnClearSearchText.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnClearSearchText.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnClearSearchText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchText.Font = new System.Drawing.Font("Calibri", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnClearSearchText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(53)))), ((int)(((byte)(44)))));
            this.btnClearSearchText.Image = global::NotiHub.Properties.Resources.broom_24px;
            this.btnClearSearchText.Location = new System.Drawing.Point(510, 13);
            this.btnClearSearchText.Name = "btnClearSearchText";
            this.btnClearSearchText.Size = new System.Drawing.Size(31, 31);
            this.btnClearSearchText.TabIndex = 434;
            this.btnClearSearchText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearSearchText.UseVisualStyleBackColor = false;
            this.btnClearSearchText.Click += new System.EventHandler(this.btnClearSearchText_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Calibri", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(53)))), ((int)(((byte)(44)))));
            this.btnSearch.Image = global::NotiHub.Properties.Resources.search_24px;
            this.btnSearch.Location = new System.Drawing.Point(759, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 31);
            this.btnSearch.TabIndex = 433;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(814, 1);
            this.panel3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel3.TabIndex = 359;
            this.panel3.Text = "panel3";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.txtCount);
            this.panel5.Controls.Add(this.lblPageInfo);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.btnLastPage);
            this.panel5.Controls.Add(this.btnStart);
            this.panel5.Controls.Add(this.btnNextPage);
            this.panel5.Controls.Add(this.btnPreviousPage);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 549);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(814, 43);
            this.panel5.TabIndex = 435;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkGray;
            this.label24.Location = new System.Drawing.Point(19, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(50, 15);
            this.label24.TabIndex = 279;
            this.label24.Text = "ACTIVE:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.txtCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.ForeColor = System.Drawing.Color.White;
            this.txtCount.Location = new System.Drawing.Point(74, 13);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(98, 18);
            this.txtCount.TabIndex = 280;
            this.txtCount.Text = "-";
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.lblPageInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageInfo.ForeColor = System.Drawing.Color.White;
            this.lblPageInfo.Location = new System.Drawing.Point(511, 13);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.ReadOnly = true;
            this.lblPageInfo.Size = new System.Drawing.Size(124, 18);
            this.lblPageInfo.TabIndex = 278;
            this.lblPageInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel6.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel6.Location = new System.Drawing.Point(642, 10);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(1, 25);
            this.panel6.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel6.TabIndex = 277;
            this.panel6.Text = "panel6";
            // 
            // btnLastPage
            // 
            this.btnLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastPage.BackColor = System.Drawing.Color.Transparent;
            this.btnLastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLastPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnLastPage.FlatAppearance.BorderSize = 0;
            this.btnLastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnLastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastPage.ForeColor = System.Drawing.Color.LightGray;
            this.btnLastPage.Image = global::NotiHub.Properties.Resources.last_24px;
            this.btnLastPage.Location = new System.Drawing.Point(758, 10);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(25, 25);
            this.btnLastPage.TabIndex = 276;
            this.btnLastPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLastPage.UseVisualStyleBackColor = false;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.LightGray;
            this.btnStart.Image = global::NotiHub.Properties.Resources.Previous_24px;
            this.btnStart.Location = new System.Drawing.Point(670, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(25, 25);
            this.btnStart.TabIndex = 275;
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextPage.BackColor = System.Drawing.Color.Transparent;
            this.btnNextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNextPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnNextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNextPage.ForeColor = System.Drawing.Color.LightGray;
            this.btnNextPage.Image = global::NotiHub.Properties.Resources.forward_24px;
            this.btnNextPage.Location = new System.Drawing.Point(727, 10);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(25, 25);
            this.btnNextPage.TabIndex = 270;
            this.btnNextPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousPage.BackColor = System.Drawing.Color.Transparent;
            this.btnPreviousPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPreviousPage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnPreviousPage.FlatAppearance.BorderSize = 0;
            this.btnPreviousPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnPreviousPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            this.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviousPage.ForeColor = System.Drawing.Color.LightGray;
            this.btnPreviousPage.Image = global::NotiHub.Properties.Resources.forward_24px1;
            this.btnPreviousPage.Location = new System.Drawing.Point(700, 10);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(25, 25);
            this.btnPreviousPage.TabIndex = 269;
            this.btnPreviousPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(43)))), ((int)(((byte)(46)))));
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(5);
            this.panel7.Size = new System.Drawing.Size(814, 1);
            this.panel7.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panel7.TabIndex = 267;
            this.panel7.Text = "panel7";
            // 
            // dataGridAuditTrail
            // 
            this.dataGridAuditTrail.AllowUserToAddRows = false;
            this.dataGridAuditTrail.AllowUserToDeleteRows = false;
            this.dataGridAuditTrail.AllowUserToResizeColumns = false;
            this.dataGridAuditTrail.AllowUserToResizeRows = false;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.PaleGoldenrod;
            this.dataGridAuditTrail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridAuditTrail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridAuditTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridAuditTrail.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.dataGridAuditTrail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridAuditTrail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridAuditTrail.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridAuditTrail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Segoe UI Semibold", 7.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridAuditTrail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridAuditTrail.ColumnHeadersHeight = 45;
            this.dataGridAuditTrail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridAuditTrail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.auID,
            this.auEventName,
            this.auEventDate,
            this.auEventDuration,
            this.auEventLocation,
            this.auStatus,
            this.auActionType,
            this.auTimestamp});
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(58)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.PaleGoldenrod;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridAuditTrail.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridAuditTrail.EnableHeadersVisualStyles = false;
            this.dataGridAuditTrail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridAuditTrail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(49)))));
            this.dataGridAuditTrail.Location = new System.Drawing.Point(25, 113);
            this.dataGridAuditTrail.Name = "dataGridAuditTrail";
            this.dataGridAuditTrail.ReadOnly = true;
            this.dataGridAuditTrail.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridAuditTrail.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridAuditTrail.RowHeadersWidth = 5;
            this.dataGridAuditTrail.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridAuditTrail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridAuditTrail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAuditTrail.Size = new System.Drawing.Size(758, 431);
            this.dataGridAuditTrail.TabIndex = 436;
            this.dataGridAuditTrail.UseCustomBackColor = true;
            this.dataGridAuditTrail.UseCustomForeColor = true;
            this.dataGridAuditTrail.UseStyleColors = true;
            // 
            // auID
            // 
            this.auID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.auID.DividerWidth = 2;
            this.auID.FillWeight = 8F;
            this.auID.HeaderText = "ID";
            this.auID.Name = "auID";
            this.auID.ReadOnly = true;
            this.auID.Width = 42;
            // 
            // auEventName
            // 
            this.auEventName.FillWeight = 30F;
            this.auEventName.HeaderText = "Name";
            this.auEventName.Name = "auEventName";
            this.auEventName.ReadOnly = true;
            // 
            // auEventDate
            // 
            this.auEventDate.FillWeight = 15F;
            this.auEventDate.HeaderText = "Date";
            this.auEventDate.Name = "auEventDate";
            this.auEventDate.ReadOnly = true;
            // 
            // auEventDuration
            // 
            this.auEventDuration.FillWeight = 23F;
            this.auEventDuration.HeaderText = "Duration";
            this.auEventDuration.Name = "auEventDuration";
            this.auEventDuration.ReadOnly = true;
            this.auEventDuration.Visible = false;
            // 
            // auEventLocation
            // 
            this.auEventLocation.DividerWidth = 2;
            this.auEventLocation.FillWeight = 30F;
            this.auEventLocation.HeaderText = "Location";
            this.auEventLocation.Name = "auEventLocation";
            this.auEventLocation.ReadOnly = true;
            // 
            // auStatus
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.auStatus.DefaultCellStyle = dataGridViewCellStyle33;
            this.auStatus.DividerWidth = 2;
            this.auStatus.FillWeight = 15F;
            this.auStatus.HeaderText = "";
            this.auStatus.Name = "auStatus";
            this.auStatus.ReadOnly = true;
            // 
            // auActionType
            // 
            this.auActionType.FillWeight = 20F;
            this.auActionType.HeaderText = "Action Type";
            this.auActionType.Name = "auActionType";
            this.auActionType.ReadOnly = true;
            // 
            // auTimestamp
            // 
            this.auTimestamp.FillWeight = 20F;
            this.auTimestamp.HeaderText = "Time Stamp";
            this.auTimestamp.Name = "auTimestamp";
            this.auTimestamp.ReadOnly = true;
            // 
            // AuditTrail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.dataGridAuditTrail);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "AuditTrail";
            this.Size = new System.Drawing.Size(814, 592);
            this.Load += new System.EventHandler(this.AuditTrail_Load);
            this.Resize += new System.EventHandler(this.AuditTrail_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAuditTrail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label26;
        private ReaLTaiizor.Controls.Panel panel13;
        private System.Windows.Forms.Label labelProductPromo;
        private System.Windows.Forms.Panel panel2;
        private ReaLTaiizor.Controls.HopeTextBox txtSearch;
        private System.Windows.Forms.Button btnClearSearchText;
        private System.Windows.Forms.Button btnSearch;
        private ReaLTaiizor.Controls.Panel panel3;
        private ReaLTaiizor.Controls.DungeonComboBox comboBoxSelection;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.Button btnRefresh;
        private ReaLTaiizor.Controls.Panel panel4;
        private CustomDatePicker datePickerEnd;
        private CustomDatePicker datePickerStart;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox lblPageInfo;
        private ReaLTaiizor.Controls.Panel panel6;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private ReaLTaiizor.Controls.Panel panel7;
        private ReaLTaiizor.Controls.PoisonDataGridView dataGridAuditTrail;
        private System.Windows.Forms.DataGridViewTextBoxColumn auID;
        private System.Windows.Forms.DataGridViewTextBoxColumn auEventName;
        private System.Windows.Forms.DataGridViewTextBoxColumn auEventDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn auEventDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn auEventLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn auStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn auActionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn auTimestamp;
    }
}
