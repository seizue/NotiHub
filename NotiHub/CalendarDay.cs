using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class CalendarDay : UserControl
    {
        private string _day, date, weekday;
        private CalendarSchedule calendarControl;

        private static List<EventData> eventsList = new List<EventData>();
        private const string FolderName = "NotiHub";
        private const string SubFolderName = "EventCalendar";
        private const string FileName = "eventcalendar.json";
        private static CalendarDay _selectedDay;

        public CalendarDay(string day, CalendarSchedule calendar)
        {
            InitializeComponent();
            _day = day;
            calendarControl = calendar;
            label1.Text = day;

            checkBox1.Hide();

            // Construct date only if the day is valid
            if (!string.IsNullOrWhiteSpace(_day) && int.TryParse(_day, out _))
            {
                date = $"{CalendarSchedule._month}/{_day}/{CalendarSchedule._year}";
            }
            else
            {
                date = string.Empty; // Empty date for invalid day
            }
        }

        private void HandlePanelDaysClick()
        {
            try
            {
                DateTime parsedDate = DateTime.Parse(date);

                if (_selectedDay != null && _selectedDay != this)
                {
                    _selectedDay.DeselectDay();
                }

                _selectedDay = this;
                SelectDay();

                EventData eventForDay = eventsList.FirstOrDefault(eventItem => eventItem.EventDate == date);

                CalendarAddSchedule scheduleCalendarForm;
                if (eventForDay != null)
                {
                    scheduleCalendarForm = new CalendarAddSchedule(date, eventForDay, calendarControl);
                }
                else
                {
                    scheduleCalendarForm = new CalendarAddSchedule(date, calendarControl);
                }

                scheduleCalendarForm.ShowDialog();
            }
            catch (FormatException ex)
            {
                Debug.WriteLine($"Invalid date format in HandlePanelDaysClick(): {date}. Exception: {ex.Message}");
            }
        }

        private void panelDays_Click(object sender, EventArgs e)
        {
            HandlePanelDaysClick();
        }

        private void pictureBoxScheduleEvent_Click(object sender, EventArgs e)
        {
            HandlePanelDaysClick();
        }


        // Method to select (highlight) the current day
        private void SelectDay()
        {
            checkBox1.Checked = true;
            this.BackColor = Color.FromArgb(255, 151, 127); // Highlight color
        }

        // Method to deselect the current day
        private void DeselectDay()
        {
            checkBox1.Checked = false;
            this.BackColor = Color.FromArgb(43, 50, 52); // Default color
        }

        public static List<EventData> GetEventsForMonth(int month, int year)
        {
            var filteredEvents = new List<EventData>();

            foreach (var e in eventsList)
            {
                try
                {
                    DateTime eventDate = DateTime.Parse(e.EventDate);
                    if (eventDate.Month == month && eventDate.Year == year)
                    {
                        filteredEvents.Add(e);
                    }
                }
                catch (FormatException ex)
                {
                    Debug.WriteLine($"Invalid EventDate format in GetEventsForMonth(): {e.EventDate}. Exception: {ex.Message}");
                }
            }

            return filteredEvents;
        }


        public static void LoadEventData()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, FolderName, SubFolderName);
            string filePath = Path.Combine(folderPath, FileName);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                try
                {
                    eventsList = JsonConvert.DeserializeObject<List<EventData>>(json)?
                        .Where(e =>
                        {
                            if (!DateTime.TryParse(e.EventDate, out _))
                            {
                                Debug.WriteLine($"Invalid EventDate in LoadEventData(): {e.EventDate}");
                                return false;
                            }
                            return true;
                        })
                        .ToList() ?? new List<EventData>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error deserializing event data: {ex.Message}");
                }
            }
        }

  
        public void CheckEventForDay()
        {
            string currentDay = date;
            EventData eventForDay = eventsList.FirstOrDefault(e => e.EventDate == currentDay);

            if (eventForDay != null)
            {
                pictureBoxScheduleEvent.Visible = true;
            }
            else
            {
                pictureBoxScheduleEvent.Visible = false;
            }
        }

        private void sundays()
        {
            // Check if the date is empty or invalid
            if (string.IsNullOrWhiteSpace(date) || !DateTime.TryParse(date, out DateTime day))
            {
                label1.ForeColor = Color.White; // Default color for invalid dates
                return;
            }

            // Get the day of the week and update the label's color
            weekday = day.ToString("ddd");
            label1.ForeColor = weekday == "Sun" ? Color.FromArgb(255, 128, 128) : Color.White;
        }

        private void CalendarDay_Load(object sender, EventArgs e)
        {
            sundays();
            LoadEventData();
            CheckEventForDay();
        }

       
    }
}
