namespace NotiHub
{
    partial class EventNoteCards
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
            this.panelCards = new ReaLTaiizor.Controls.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.panelNav = new System.Windows.Forms.Panel();
            this.labelTime = new System.Windows.Forms.Label();
            this.richTextBoxTitle = new System.Windows.Forms.RichTextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.richTextBoxLocation = new System.Windows.Forms.RichTextBox();
            this.panelCards.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCards
            // 
            this.panelCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.panelCards.Controls.Add(this.labelStatus);
            this.panelCards.Controls.Add(this.panelNav);
            this.panelCards.Controls.Add(this.labelTime);
            this.panelCards.Controls.Add(this.richTextBoxTitle);
            this.panelCards.Controls.Add(this.labelDate);
            this.panelCards.Controls.Add(this.richTextBoxLocation);
            this.panelCards.EdgeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(48)))), ((int)(((byte)(51)))));
            this.panelCards.Location = new System.Drawing.Point(1, 2);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(2);
            this.panelCards.Size = new System.Drawing.Size(283, 92);
            this.panelCards.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.panelCards.TabIndex = 7;
            this.panelCards.Text = "panel5";
            this.panelCards.Click += new System.EventHandler(this.panelCards_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelStatus.Location = new System.Drawing.Point(133, 69);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(34, 12);
            this.labelStatus.TabIndex = 250;
            this.labelStatus.Text = "Status";
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.Silver;
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNav.Location = new System.Drawing.Point(2, 2);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(279, 5);
            this.panelNav.TabIndex = 249;
            this.panelNav.Click += new System.EventHandler(this.panelNav_Click);
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelTime.Location = new System.Drawing.Point(15, 68);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(123, 15);
            this.labelTime.TabIndex = 248;
            this.labelTime.Text = "00:00 AM - 00:00 PM";
            // 
            // richTextBoxTitle
            // 
            this.richTextBoxTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.richTextBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.75F, System.Drawing.FontStyle.Bold);
            this.richTextBoxTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.richTextBoxTitle.Location = new System.Drawing.Point(18, 14);
            this.richTextBoxTitle.Name = "richTextBoxTitle";
            this.richTextBoxTitle.ReadOnly = true;
            this.richTextBoxTitle.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxTitle.Size = new System.Drawing.Size(246, 30);
            this.richTextBoxTitle.TabIndex = 247;
            this.richTextBoxTitle.Text = "Title";
            this.richTextBoxTitle.Click += new System.EventHandler(this.richTextBoxTitle_Click);
            // 
            // labelDate
            // 
            this.labelDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelDate.Location = new System.Drawing.Point(199, 68);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(85, 15);
            this.labelDate.TabIndex = 245;
            this.labelDate.Text = "MM/DD/YYYY";
            // 
            // richTextBoxLocation
            // 
            this.richTextBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.richTextBoxLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 7.75F, System.Drawing.FontStyle.Bold);
            this.richTextBoxLocation.ForeColor = System.Drawing.Color.Gainsboro;
            this.richTextBoxLocation.Location = new System.Drawing.Point(18, 47);
            this.richTextBoxLocation.Multiline = false;
            this.richTextBoxLocation.Name = "richTextBoxLocation";
            this.richTextBoxLocation.ReadOnly = true;
            this.richTextBoxLocation.Size = new System.Drawing.Size(246, 16);
            this.richTextBoxLocation.TabIndex = 244;
            this.richTextBoxLocation.Text = "Location";
            this.richTextBoxLocation.Click += new System.EventHandler(this.richTextBoxLocation_Click);
            // 
            // EventNoteCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.panelCards);
            this.Name = "EventNoteCards";
            this.Size = new System.Drawing.Size(285, 95);
            this.panelCards.ResumeLayout(false);
            this.panelCards.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.Panel panelCards;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.RichTextBox richTextBoxTitle;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.RichTextBox richTextBoxLocation;
        private System.Windows.Forms.Label labelStatus;
    }
}
