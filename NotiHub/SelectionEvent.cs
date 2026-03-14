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
    public partial class SelectionEvent : Form
    {
        private List<EventData> _events;
        private string _date;
        private CalendarSchedule _calendarControl;

        public SelectionEvent()
        {
            InitializeComponent();
        }

        public SelectionEvent(List<EventData> events, string date, CalendarSchedule calendarControl)
        {
            InitializeComponent();
            _events = events;
            _date = date;
            _calendarControl = calendarControl;

            this.Text = $"Select Event - {date}";
            PopulateEventList();
        }

        private void PopulateEventList()
        {
            eventListBox.Items.Clear();

            for (int i = 0; i < _events.Count; i++)
            {
                string eventText = $"{i + 1}. {_events[i].EventName} ({_events[i].TimeFrom} {_events[i].FromAMPM})";
                if (!string.IsNullOrEmpty(_events[i].EventLocation))
                {
                    eventText += $" - {_events[i].EventLocation}";
                }
                eventListBox.Items.Add(eventText);
            }

            if (eventListBox.Items.Count > 0)
            {
                eventListBox.SelectedIndex = 0;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            CalendarAddSchedule eventForm = new CalendarAddSchedule(_date, _calendarControl, null);
            eventForm.ShowDialog();
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (eventListBox.SelectedIndex >= 0 && eventListBox.SelectedIndex < _events.Count)
            {
                CalendarAddSchedule eventForm = new CalendarAddSchedule(_date, _calendarControl, _events[eventListBox.SelectedIndex]);
                eventForm.ShowDialog();
                Close();
            }
        }
    }
}
