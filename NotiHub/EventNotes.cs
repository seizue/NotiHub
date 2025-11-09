using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class EventNotes : UserControl
    {
        private const string FolderName = "NotiHub";
        private const string SubFolderName = "EventCalendar";
        private const string FileName = "eventcalendar.json";
        private List<EventData> eventsList = new List<EventData>();
        private DateTime currentMonth;
        public EventNotes()
        {
            InitializeComponent();
            currentMonth = DateTime.Now;
            LoadEvents();
        }

        public void LoadEvents()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, FolderName, SubFolderName);
            string filePath = Path.Combine(folderPath, FileName);

            if (File.Exists(filePath))
            {
                try
                {
                    // Read JSON file content
                    string jsonContent = File.ReadAllText(filePath);

                    // Deserialize JSON content to a list of EventData
                    eventsList = JsonConvert.DeserializeObject<List<EventData>>(jsonContent);

                    // For debugging - log all events with their dates
                    Console.WriteLine("All loaded events:");
                    foreach (var evt in eventsList)
                    {
                        Console.WriteLine($"Event: {evt.EventName}, Date: {evt.EventDate}");
                    }

                    // Filter events based on the current month with proper date parsing
                    var filteredEvents = new List<EventData>();
                    foreach (var evt in eventsList)
                    {
                        // Try multiple date formats to ensure correct parsing
                        if (TryParseEventDate(evt.EventDate, out DateTime parsedDate))
                        {
                            Console.WriteLine($"Successfully parsed date: {evt.EventDate} to {parsedDate}");
                            if (parsedDate.Month == currentMonth.Month && parsedDate.Year == currentMonth.Year)
                            {
                                filteredEvents.Add(evt);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to parse date: {evt.EventDate}");
                        }
                    }

                    // Load schedule notes cards into the flow layout panel
                    LoadScheduleCards(filteredEvents);

                    // Update the month label to show the current month and year
                    lbMonth.Text = currentMonth.ToString("MMMM yyyy");

                    // Update the event count display
                    btnEventCount.Text = $"{filteredEvents.Count} Events";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading events: {ex.Message}");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Event calendar file not found.");
                return;
            }
        }

        // Helper method to try parsing dates in multiple formats
        private bool TryParseEventDate(string dateString, out DateTime result)
        {
            // Define multiple date formats to try
            string[] formats = {
                "M/d/yyyy", // American format (month/day/year)
                "d/M/yyyy", // European format (day/month/year)
                "yyyy-MM-dd", // ISO format
                "yyyy/MM/dd",
                "dd-MM-yyyy",
                "MM-dd-yyyy"
            };

            // Try to parse the date using each format
            return DateTime.TryParseExact(
                dateString,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result);
        }

        private void LoadScheduleCards(List<EventData> events)
        {
            flowLayoutPanel1.Controls.Clear(); // Clear existing controls before adding new ones

            foreach (var eventData in events)
            {
                // Create a new UserControlTaskCard for each event
                EventNoteCards scheduleNoteCard = new EventNoteCards();

                // Optionally, set the data in the task card
                scheduleNoteCard.LoadEventData(eventData);

                // Add the created task card to the FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(scheduleNoteCard);
            }
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);

            // Reload events for the previous month
            LoadEvents();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            // Move to the next month
            currentMonth = currentMonth.AddMonths(1);

            // Reload events for the next month
            LoadEvents();
        }

        private void ExportEventsToCSV(List<EventData> events, string filePath)
        {
            var sb = new StringBuilder();

            // Add CSV header
            sb.AppendLine("EventName,TimeFrom,FromAMPM,TimeTo,ToAMPM,EventLocation,EventDate");

            // Add each event as a CSV row
            foreach (var eventData in events)
            {
                sb.AppendLine($"{eventData.EventName},{eventData.TimeFrom},{eventData.FromAMPM},{eventData.TimeTo},{eventData.ToAMPM},{eventData.EventLocation},{eventData.EventDate}");
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, sb.ToString());
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Choose file path for saving the CSV file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV Files|*.csv";
                    saveFileDialog.Title = "Save Events as CSV";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Export filtered events to CSV
                        var filteredEvents = eventsList
                            .Where(eventData => DateTime.TryParse(eventData.EventDate, out DateTime parsedDate) &&
                                                parsedDate.Month == currentMonth.Month &&
                                                parsedDate.Year == currentMonth.Year)
                            .ToList();

                        ExportEventsToCSV(filteredEvents, saveFileDialog.FileName);
                        MessageBox.Show("Events exported successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting events: " + ex.Message);
            }
        }

        private void btnEventCount_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"There are {btnEventCount.Text} events for the month of {currentMonth}");
        }
    }
}
