namespace NotiHub
{
    partial class CalendarDay
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
            this.panelDays = new System.Windows.Forms.Panel();
            this.pictureBoxScheduleEvent = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScheduleEvent)).BeginInit();
            this.SuspendLayout();
            // 
            // panelDays
            // 
            this.panelDays.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.panelDays.Controls.Add(this.pictureBoxScheduleEvent);
            this.panelDays.Controls.Add(this.checkBox1);
            this.panelDays.Controls.Add(this.label1);
            this.panelDays.Location = new System.Drawing.Point(2, 3);
            this.panelDays.Margin = new System.Windows.Forms.Padding(5);
            this.panelDays.Name = "panelDays";
            this.panelDays.Size = new System.Drawing.Size(101, 58);
            this.panelDays.TabIndex = 2;
            this.panelDays.Click += new System.EventHandler(this.panelDays_Click);
            // 
            // pictureBoxScheduleEvent
            // 
            this.pictureBoxScheduleEvent.Image = global::NotiHub.Properties.Resources.schedulecal_24px;
            this.pictureBoxScheduleEvent.Location = new System.Drawing.Point(70, 7);
            this.pictureBoxScheduleEvent.Name = "pictureBoxScheduleEvent";
            this.pictureBoxScheduleEvent.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxScheduleEvent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxScheduleEvent.TabIndex = 241;
            this.pictureBoxScheduleEvent.TabStop = false;
            this.pictureBoxScheduleEvent.Visible = false;
            this.pictureBoxScheduleEvent.Click += new System.EventHandler(this.pictureBoxScheduleEvent_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // CalendarDay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.Controls.Add(this.panelDays);
            this.Name = "CalendarDay";
            this.Size = new System.Drawing.Size(105, 63);
            this.Load += new System.EventHandler(this.CalendarDay_Load);
            this.panelDays.ResumeLayout(false);
            this.panelDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScheduleEvent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDays;
        private System.Windows.Forms.PictureBox pictureBoxScheduleEvent;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
    }
}
