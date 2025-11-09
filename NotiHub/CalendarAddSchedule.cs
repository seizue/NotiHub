using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class CalendarAddSchedule : Form
    {
        private string selectedDate;
        private EventData eventData;
        private CalendarSchedule calendarControl;

        public CalendarAddSchedule(string date, CalendarSchedule calendar)
        {
            InitializeComponent();
            selectedDate = date;
            calendarControl = calendar;  // Store the reference to the calendar control
            lblSelectedDate.Text = $"Selected Date: {selectedDate}"; // Display the selected date in the label
            eventData = null;  // No event data by default
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public CalendarAddSchedule(string date, EventData eventDetails, CalendarSchedule calendar)
        {
            InitializeComponent();
            selectedDate = date;
            eventData = eventDetails;
            calendarControl = calendar;
            lblSelectedDate.Text = $"Selected Date: {selectedDate}"; // Display the selected date in the label
            LoadEventDetails();  // If there's an event, load its details into the form
        }

        // Load event details into the form fields if an event exists
        private void LoadEventDetails()
        {
            if (eventData != null)
            {
                txtboxEventName.Text = eventData.EventName;
                comboBoxTimeFrom.SelectedItem = eventData.TimeFrom;
                comboBoxFromAMPM.SelectedItem = eventData.FromAMPM;
                comboBoxTo.SelectedItem = eventData.TimeTo;
                comboBoxToAMPM.SelectedItem = eventData.ToAMPM;
                txtboxEventLocation.Text = eventData.EventLocation;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string eventName = txtboxEventName.Text;

            if (string.IsNullOrWhiteSpace(eventName))
            {
                MessageBox.Show("Event Name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string timeFrom = comboBoxTimeFrom.SelectedItem.ToString();
            string fromAMPM = comboBoxFromAMPM.SelectedItem.ToString();
            string timeTo = comboBoxTo.SelectedItem.ToString();
            string toAMPM = comboBoxToAMPM.SelectedItem.ToString();
            string eventLocation = txtboxEventLocation.Text;

            // Create the EventData object with the selected date
            EventData newEventData = new EventData
            {
                EventName = eventName,
                TimeFrom = timeFrom,
                FromAMPM = fromAMPM,
                TimeTo = timeTo,
                ToAMPM = toAMPM,
                EventLocation = eventLocation,
                EventDate = selectedDate // Include the date in the event data
            };

            // Save the event data to the JSON file
            SaveEventData(newEventData);

            // Refresh the calendar to reflect the new event
            calendarControl.RefreshEventData();

            MessageBox.Show("Event saved successfully!");

            this.Close();
        }


        private void SaveEventData(EventData newEventData)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, "NotiHub", "EventCalendar");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, "eventcalendar.json");

            List<EventData> events = File.Exists(filePath) ?
                                     JsonConvert.DeserializeObject<List<EventData>>(File.ReadAllText(filePath)) ?? new List<EventData>()
                                     : new List<EventData>();

            // Remove any existing event for this date
            events.RemoveAll(e => e.EventDate == newEventData.EventDate);

            // Add the new event
            events.Add(newEventData);

            // Serialize and save the updated list
            File.WriteAllText(filePath, JsonConvert.SerializeObject(events, Formatting.Indented));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Confirm that the user wants to delete the event
            var confirmResult = MessageBox.Show("Are you sure you want to delete this event?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                return; // Don't delete if the user cancels
            }

            // Remove the event data from the JSON file
            DeleteEventData(selectedDate);

            // Refresh the calendar control
            calendarControl.RefreshEventData();

            MessageBox.Show("Event deleted successfully!");

            this.Close();
        }

        private void DeleteEventData(string eventDate)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, "NotiHub", "EventCalendar");
            string filePath = Path.Combine(folderPath, "eventcalendar.json");

            if (File.Exists(filePath))
            {
                // Read the existing events from the file
                List<EventData> events = JsonConvert.DeserializeObject<List<EventData>>(File.ReadAllText(filePath)) ?? new List<EventData>();

                // Remove the event matching the selected date
                events.RemoveAll(e => e.EventDate == eventDate);

                // Save the updated list back to the file
                File.WriteAllText(filePath, JsonConvert.SerializeObject(events, Formatting.Indented));
            }
            else
            {
                MessageBox.Show("Event calendar file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
