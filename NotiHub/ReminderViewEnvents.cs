using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class ReminderViewEnvents : Form
    {
        public ReminderViewEnvents(EventData eventData)
        {
            InitializeComponent();
            LoadEventData(eventData);
        }

        private void LoadEventData(EventData eventData)
        {
            if (eventData == null) return;

            richTextBoxTitle.Text = eventData.EventName;
            btnStatus.Text = eventData.Status ?? "Pending";
            btnDate.Text = eventData.EventDate;
            richTextBoxLocation.Text = eventData.EventLocation;

            // Notes/link
            richTextBoxNotesLink.DetectUrls = true;
            richTextBoxNotesLink.LinkClicked += (s, e) =>
            {
                try { Process.Start(new ProcessStartInfo(e.LinkText) { UseShellExecute = true }); }
                catch { }
            };
            richTextBoxNotesLink.Text = eventData.Notes ?? string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
