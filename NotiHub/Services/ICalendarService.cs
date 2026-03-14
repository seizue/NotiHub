using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace NotiHub.Services
{
    /// <summary>
    /// Service for importing/exporting events using iCalendar (.ics) format
    /// This is a simpler alternative to OAuth that works with any calendar app
    /// </summary>
    public class ICalendarService
    {
        private static ICalendarService _instance;

        public static ICalendarService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ICalendarService();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Import events from an .ics file
        /// </summary>
        public List<EventData> ImportFromICS(string filePath)
        {
            var events = new List<EventData>();

            try
            {
                string content = File.ReadAllText(filePath);
                var icsEvents = ParseICS(content);

                foreach (var icsEvent in icsEvents)
                {
                    var eventData = ConvertICSToEventData(icsEvent);
                    if (eventData != null)
                    {
                        events.Add(eventData);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error importing ICS: {ex.Message}");
            }

            return events;
        }

        /// <summary>
        /// Export events to an .ics file
        /// </summary>
        public bool ExportToICS(List<EventData> events, string filePath)
        {
            try
            {
                var icsContent = GenerateICS(events);
                File.WriteAllText(filePath, icsContent, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error exporting ICS: {ex.Message}");
                return false;
            }
        }

        private List<Dictionary<string, string>> ParseICS(string content)
        {
            var events = new List<Dictionary<string, string>>();
            var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            
            Dictionary<string, string> currentEvent = null;
            string currentKey = null;
            StringBuilder currentValue = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.Trim() == "BEGIN:VEVENT")
                {
                    currentEvent = new Dictionary<string, string>();
                }
                else if (line.Trim() == "END:VEVENT" && currentEvent != null)
                {
                    if (currentKey != null)
                    {
                        currentEvent[currentKey] = currentValue.ToString();
                    }
                    events.Add(currentEvent);
                    currentEvent = null;
                    currentKey = null;
                    currentValue.Clear();
                }
                else if (currentEvent != null)
                {
                    // Handle line continuation (lines starting with space)
                    if (line.StartsWith(" ") || line.StartsWith("\t"))
                    {
                        currentValue.Append(line.Substring(1));
                    }
                    else
                    {
                        // Save previous key-value
                        if (currentKey != null)
                        {
                            currentEvent[currentKey] = currentValue.ToString();
                        }

                        // Parse new key-value
                        int colonIndex = line.IndexOf(':');
                        if (colonIndex > 0)
                        {
                            currentKey = line.Substring(0, colonIndex).Split(';')[0]; // Remove parameters
                            currentValue.Clear();
                            currentValue.Append(line.Substring(colonIndex + 1));
                        }
                    }
                }
            }

            return events;
        }

        private EventData ConvertICSToEventData(Dictionary<string, string> icsEvent)
        {
            try
            {
                // Get required fields
                if (!icsEvent.ContainsKey("DTSTART"))
                    return null;

                DateTime startDate = ParseICSDateTime(icsEvent["DTSTART"]);

                var eventData = new EventData
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = icsEvent.ContainsKey("SUMMARY") ? UnescapeICS(icsEvent["SUMMARY"]) : "Untitled Event",
                    EventDate = startDate.ToString("M/d/yyyy"),
                    TimeFrom = startDate.ToString("h:mm"),
                    FromAMPM = startDate.ToString("tt"),
                    EventLocation = icsEvent.ContainsKey("LOCATION") ? UnescapeICS(icsEvent["LOCATION"]) : "",
                    Notes = icsEvent.ContainsKey("DESCRIPTION") ? UnescapeICS(icsEvent["DESCRIPTION"]) : "",
                    Status = "Pending"
                };

                // Set end time if available
                if (icsEvent.ContainsKey("DTEND"))
                {
                    DateTime endDate = ParseICSDateTime(icsEvent["DTEND"]);
                    eventData.TimeTo = endDate.ToString("h:mm");
                    eventData.ToAMPM = endDate.ToString("tt");
                }

                return eventData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error converting ICS event: {ex.Message}");
                return null;
            }
        }

        private string GenerateICS(List<EventData> events)
        {
            var sb = new StringBuilder();

            // ICS header
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//NotiHub//Event Calendar//EN");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");

            // Add events
            foreach (var evt in events)
            {
                sb.AppendLine("BEGIN:VEVENT");
                sb.AppendLine($"UID:{evt.Id}@notihub.local");
                sb.AppendLine($"DTSTAMP:{FormatICSDateTime(DateTime.Now)}");
                
                // Start time
                DateTime startDate = ParseEventDateTime(evt);
                sb.AppendLine($"DTSTART:{FormatICSDateTime(startDate)}");

                // End time
                if (!string.IsNullOrEmpty(evt.TimeTo))
                {
                    DateTime endDate = ParseEventEndDateTime(evt);
                    sb.AppendLine($"DTEND:{FormatICSDateTime(endDate)}");
                }
                else
                {
                    // Default 1 hour duration
                    sb.AppendLine($"DTEND:{FormatICSDateTime(startDate.AddHours(1))}");
                }

                sb.AppendLine($"SUMMARY:{EscapeICS(evt.EventName)}");
                
                if (!string.IsNullOrEmpty(evt.EventLocation))
                    sb.AppendLine($"LOCATION:{EscapeICS(evt.EventLocation)}");
                
                if (!string.IsNullOrEmpty(evt.Notes))
                    sb.AppendLine($"DESCRIPTION:{EscapeICS(evt.Notes)}");

                sb.AppendLine($"STATUS:{evt.Status.ToUpper()}");
                sb.AppendLine("END:VEVENT");
            }

            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();
        }

        private DateTime ParseICSDateTime(string icsDateTime)
        {
            // Format: 20240101T120000Z or 20240101T120000
            icsDateTime = icsDateTime.Replace("Z", "").Replace("T", "");
            
            int year = int.Parse(icsDateTime.Substring(0, 4));
            int month = int.Parse(icsDateTime.Substring(4, 2));
            int day = int.Parse(icsDateTime.Substring(6, 2));
            
            if (icsDateTime.Length >= 14)
            {
                int hour = int.Parse(icsDateTime.Substring(8, 2));
                int minute = int.Parse(icsDateTime.Substring(10, 2));
                int second = int.Parse(icsDateTime.Substring(12, 2));
                return new DateTime(year, month, day, hour, minute, second);
            }
            
            return new DateTime(year, month, day);
        }

        private string FormatICSDateTime(DateTime dt)
        {
            return dt.ToString("yyyyMMddTHHmmss");
        }

        private DateTime ParseEventDateTime(EventData evt)
        {
            DateTime date = DateTime.Parse(evt.EventDate);
            var timeParts = evt.TimeFrom.Split(':');
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            if (evt.FromAMPM == "PM" && hour != 12)
                hour += 12;
            else if (evt.FromAMPM == "AM" && hour == 12)
                hour = 0;

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        private DateTime ParseEventEndDateTime(EventData evt)
        {
            DateTime date = DateTime.Parse(evt.EventDate);
            var timeParts = evt.TimeTo.Split(':');
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            if (evt.ToAMPM == "PM" && hour != 12)
                hour += 12;
            else if (evt.ToAMPM == "AM" && hour == 12)
                hour = 0;

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        private string EscapeICS(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text
                .Replace("\\", "\\\\")
                .Replace(",", "\\,")
                .Replace(";", "\\;")
                .Replace("\n", "\\n");
        }

        private string UnescapeICS(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            return text
                .Replace("\\n", "\n")
                .Replace("\\;", ";")
                .Replace("\\,", ",")
                .Replace("\\\\", "\\");
        }
    }
}
