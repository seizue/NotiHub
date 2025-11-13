using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class CalendarSchedule : UserControl
    {       
        public static int _year, _month;
        public event Action EventsUpdated;

        public CalendarSchedule()
        {
            InitializeComponent();
        }

        private void CalendarSchedule_Load(object sender, EventArgs e)
        {
            showDays(DateTime.Now.Month, DateTime.Now.Year);
        }

        public void RefreshEventData()
        {
            showDays(_month, _year);
            UpdateEventCount();
            EventsUpdated?.Invoke();
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            _month -= 1;
            if (_month < 1)
            {
                _month = 12;
                _year -= 1;
            }
            showDays(_month, _year);
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _month += 1;
            if (_month > 12)
            {
                _month = 1;
                _year += 1;
            }
            showDays(_month, _year);
        }

        private void btnEventCount_Click(object sender, EventArgs e)
        {
            // Handle button click to show detailed events or other behavior
            MessageBox.Show($"There are {btnEventCount.Text} events for the month of {_month}/{_year}");
        }

        public void showDays(int month, int year)
        {
            flowLayoutPanel1.Controls.Clear();
            _year = year;
            _month = month;

            string monthName = new DateTimeFormatInfo().GetMonthName(month);
            lbMonth.Text = monthName + " " + year;
            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int week = (int)startOfTheMonth.DayOfWeek + 1;

            // Add empty placeholders for days before the start of the month
            for (int i = 1; i < week; i++)
            {
                CalendarDay placeholderDay = new CalendarDay("", this); // Pass empty day
                flowLayoutPanel1.Controls.Add(placeholderDay);
            }

            // Add actual days of the month
            for (int i = 1; i <= daysInMonth; i++)
            {
                CalendarDay actualDay = new CalendarDay(i.ToString(), this);
                flowLayoutPanel1.Controls.Add(actualDay);
            }


            // Update event count after rendering
            UpdateEventCount();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "Do you want to refresh the events?",
                "Confirm Refresh",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                RefreshEventData();
                MessageBox.Show("Events refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void UpdateEventCount()
        {
            // Get events for the current month and year from the static method in uDay
            var eventsForMonth = CalendarDay.GetEventsForMonth(_month, _year);

            // Get the event count
            int eventCount = eventsForMonth.Count;

            // Update the btnEventCount text with the event count
            btnEventCount.Text = $"{eventCount}";
        }
    }

    public class EventData
    {
        public string EventName { get; set; }
        public string TimeFrom { get; set; }
        public string FromAMPM { get; set; }
        public string TimeTo { get; set; }
        public string ToAMPM { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }
        public string Status { get; set; } = "Pending"; // Default status
    }
}
