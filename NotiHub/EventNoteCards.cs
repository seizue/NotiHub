using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotiHub.Services;

namespace NotiHub
{
    public partial class EventNoteCards : UserControl
    {
        private static List<EventNoteCards> _selectedCards = new List<EventNoteCards>();
        private EventData _eventData;
        private ContextMenuStrip contextMenu;

        public EventNoteCards()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            
            var shareMenuItem = new ToolStripMenuItem("Share Note");
            shareMenuItem.Click += ShareMenuItem_Click;
            
            contextMenu.Items.Add(shareMenuItem);
            
            // Attach context menu to the card
            this.ContextMenuStrip = contextMenu;
            panelCards.ContextMenuStrip = contextMenu;
        }

        private async void ShareMenuItem_Click(object sender, EventArgs e)
        {
            if (_eventData == null)
            {
                MessageBox.Show("No event data to share.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Show loading message
                Cursor = Cursors.WaitCursor;

                // Create share service
                var shareService = new DPasteShareService();

                // Share the note
                string shareUrl = await shareService.ShareNoteAsync(_eventData);

                Cursor = Cursors.Default;

                // Show share dialog with QR code
                using (var shareForm = new ShareNote(shareUrl))
                {
                    shareForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Failed to share note: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public EventData GetEventData()
        {
            return _eventData;
        }

        public void LoadEventData(EventData eventData)
        {
            // Store reference to event data for later use
            _eventData = eventData;

            // Set the event details
            richTextBoxTitle.Text = eventData.EventName;
            richTextBoxLocation.Text = eventData.EventLocation;
            labelDate.Text = eventData.EventDate;

            // Combine TimeFrom and FromAMPM with TimeTo and ToAMPM
            string timeFromFormatted = $"{eventData.TimeFrom} {eventData.FromAMPM}";
            string timeToFormatted = $"{eventData.TimeTo} {eventData.ToAMPM}";

            // Combine the times into a single string
            labelTime.Text = $"{timeFromFormatted} - {timeToFormatted}";

            // Display saved notes/links if available
            richTextBoxNoteLink.DetectUrls = true;
            richTextBoxNoteLink.LinkClicked -= RichTextBoxNoteLink_LinkClicked;
            richTextBoxNoteLink.LinkClicked += RichTextBoxNoteLink_LinkClicked;
            richTextBoxNoteLink.Text = !string.IsNullOrWhiteSpace(eventData.Notes)
                ? eventData.Notes
                : string.Empty;

            // Apply the status color if it exists
            if (!string.IsNullOrWhiteSpace(eventData.Status))
            {
                UpdateStatus(eventData.Status);
            }


            // Apply the status color and text if it exists
            if (!string.IsNullOrWhiteSpace(eventData.Status))
            {
                UpdateStatus(eventData.Status);
                labelStatus.Text = eventData.Status; // Display the status text
            }
            else
            {
                labelStatus.Text = "Pending"; // Default if no status found
            }

        }

        private void HandleTaskCardsClick()
        {
            // Check if Ctrl key is held for multiple selection
            if (Control.ModifierKeys == Keys.Control)
            {
                // Toggle selection for this card
                if (_selectedCards.Contains(this))
                {
                    _selectedCards.Remove(this);
                    DeselectedCard();
                }
                else
                {
                    _selectedCards.Add(this);
                    SelectCard();
                }
            }
            else
            {
                // Single selection: deselect all others
                foreach (var card in _selectedCards)
                {
                    if (card != this)
                    {
                        card.DeselectedCard();
                    }
                }

                _selectedCards.Clear();
                _selectedCards.Add(this);
                SelectCard();
            }
        }

        private void SelectCard()
        {
            this.BackColor = Color.FromArgb(255, 151, 127); // Highlight color
        }

        private void DeselectedCard()
        {
            this.BackColor = Color.FromArgb(43, 50, 52); // Default color
        }

        public static List<EventNoteCards> GetSelectedCards()
        {
            return _selectedCards;
        }

        public static void ClearSelection()
        {
            foreach (var card in _selectedCards)
            {
                card.DeselectedCard();
            }
            _selectedCards.Clear();
        }

        private void RichTextBoxNoteLink_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.LinkText) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void richTextBoxTitle_TextChanged(object sender, EventArgs e)
        {
            HandleTaskCardsClick();
        }

        private void panelCards_Click(object sender, EventArgs e)
        {
            HandleTaskCardsClick();
        }

        private void panelNav_Click(object sender, EventArgs e)
        {
            HandleTaskCardsClick();
        }

        private void richTextBoxTitle_Click(object sender, EventArgs e)
        {
            HandleTaskCardsClick();
        }

        private void richTextBoxLocation_Click(object sender, EventArgs e)
        {
            HandleTaskCardsClick();
        }

        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower() ?? "pending")
            {
                case "completed":
                    return Color.FromArgb(34, 177, 76); // Green
                case "pending":
                    return Color.Silver; // Silver
                case "reschedule":
                    return Color.LightPink; // LightPink
                case "cancel":
                    return Color.SandyBrown; // SandyBrown
                case "expired":
                    return Color.IndianRed; //IndianRed
                default:
                    return Color.Silver; // Silver
            }
        }

        public void UpdateStatus(string status)
        {
            panelNav.BackColor = GetStatusColor(status);
            labelStatus.Text = status; // Update label text dynamically

            if (_eventData != null)
            {
                _eventData.Status = status;
            }
        }

    }
}
