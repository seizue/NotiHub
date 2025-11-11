using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class EventNoteCards : UserControl
    {
        private static EventNoteCards _selectedCard;
        public EventNoteCards()
        {
            InitializeComponent();
        }

        public void LoadEventData(EventData eventData)
        {
            // Set the event details
            richTextBoxTitle.Text = eventData.EventName;
            richTextBoxLocation.Text = eventData.EventLocation;
            labelDate.Text = eventData.EventDate;

            // Combine TimeFrom and FromAMPM with TimeTo and ToAMPM
            string timeFromFormatted = $"{eventData.TimeFrom} {eventData.FromAMPM}";
            string timeToFormatted = $"{eventData.TimeTo} {eventData.ToAMPM}";

            // Combine the times into a single string
            labelTime.Text = $"{timeFromFormatted} - {timeToFormatted}";
        }

        private void HandleTaskCardsClick()
        {
            // Deselect the previously selected card (if any)
            if (_selectedCard != null && _selectedCard != this)
            {
                _selectedCard.DeselectedCard();
            }

            // Select the current card
            _selectedCard = this;
            SelectCard(); // Highlight the current card
        }

        private void SelectCard()
        {
            this.BackColor = Color.FromArgb(255, 151, 127); // Highlight color
        }

        private void DeselectedCard()
        {
            this.BackColor = Color.FromArgb(43, 50, 52); // Default color
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
    }
}
