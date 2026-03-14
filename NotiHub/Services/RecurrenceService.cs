using System;
using System.Collections.Generic;
using System.Linq;
using NotiHub.Models;

namespace NotiHub.Services
{
    public class RecurrenceService
    {
        private static RecurrenceService _instance;

        public static RecurrenceService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RecurrenceService();
                }
                return _instance;
            }
        }

        private RecurrenceService()
        {
        }

        /// <summary>
        /// Generate all occurrences of a recurring event within a date range
        /// </summary>
        public List<DateTime> GenerateOccurrences(EventData eventData, DateTime startDate, DateTime endDate)
        {
            var occurrences = new List<DateTime>();

            if (eventData.Recurrence == null || eventData.Recurrence.Type == RecurrenceType.None)
            {
                // Non-recurring event
                if (DateTime.TryParse(eventData.EventDate, out DateTime eventDate))
                {
                    if (eventDate >= startDate && eventDate <= endDate)
                    {
                        occurrences.Add(eventDate);
                    }
                }
                return occurrences;
            }

            // Parse the original event date
            if (!DateTime.TryParse(eventData.EventDate, out DateTime originalDate))
            {
                return occurrences;
            }

            DateTime currentDate = originalDate;
            int count = 0;

            // Generate occurrences based on recurrence pattern
            while (currentDate <= endDate)
            {
                // Check if we've reached the recurrence end date
                if (eventData.Recurrence.EndDate.HasValue && currentDate > eventData.Recurrence.EndDate.Value)
                {
                    break;
                }

                // Check if we've reached max occurrences
                if (eventData.Recurrence.MaxOccurrences.HasValue && count >= eventData.Recurrence.MaxOccurrences.Value)
                {
                    break;
                }

                // Add occurrence if it's within the requested range
                if (currentDate >= startDate && currentDate <= endDate)
                {
                    occurrences.Add(currentDate);
                }

                count++;

                // Calculate next occurrence
                currentDate = GetNextOccurrence(currentDate, eventData.Recurrence);
            }

            return occurrences;
        }

        private DateTime GetNextOccurrence(DateTime currentDate, RecurrencePattern recurrence)
        {
            switch (recurrence.Type)
            {
                case RecurrenceType.Minutely:
                    return currentDate.AddMinutes(recurrence.Interval);

                case RecurrenceType.Hourly:
                    return currentDate.AddHours(recurrence.Interval);

                case RecurrenceType.Daily:
                    return currentDate.AddDays(recurrence.Interval);

                case RecurrenceType.Weekly:
                    return currentDate.AddDays(7 * recurrence.Interval);

                case RecurrenceType.Monthly:
                    return currentDate.AddMonths(recurrence.Interval);

                case RecurrenceType.Yearly:
                    return currentDate.AddYears(recurrence.Interval);

                default:
                    return currentDate;
            }
        }

        /// <summary>
        /// Get all events for a specific date, including recurring events
        /// </summary>
        public List<EventData> GetEventsForDate(List<EventData> allEvents, DateTime targetDate)
        {
            var eventsForDate = new List<EventData>();

            foreach (var eventData in allEvents)
            {
                var occurrences = GenerateOccurrences(eventData, targetDate.Date, targetDate.Date);
                if (occurrences.Any())
                {
                    eventsForDate.Add(eventData);
                }
            }

            return eventsForDate;
        }

        /// <summary>
        /// Get upcoming events that need reminders
        /// </summary>
        public List<(EventData Event, int MinutesUntil)> GetUpcomingEventsWithReminders()
        {
            var result = new List<(EventData, int)>();
            var allEvents = EventDataService.Instance.LoadAllEvents();
            var now = DateTime.Now;

            foreach (var eventData in allEvents)
            {
                if (eventData.Reminders == null || !eventData.Reminders.IsEnabled || eventData.Reminders.Reminders.Count == 0)
                {
                    continue;
                }

                // Get next occurrence of this event
                var occurrences = GenerateOccurrences(eventData, now.Date, now.Date.AddDays(7));
                
                foreach (var occurrence in occurrences)
                {
                    // Parse event time
                    if (!TryParseEventTime(eventData, occurrence, out DateTime eventDateTime))
                    {
                        continue;
                    }

                    // Check each reminder
                    foreach (var reminder in eventData.Reminders.Reminders)
                    {
                        var reminderTime = eventDateTime.AddMinutes(-(int)reminder);
                        var minutesUntilReminder = (reminderTime - now).TotalMinutes;

                        // Trigger if reminder is within the next minute
                        if (minutesUntilReminder >= 0 && minutesUntilReminder < 1)
                        {
                            int minutesUntilEvent = (int)(eventDateTime - now).TotalMinutes;
                            result.Add((eventData, minutesUntilEvent));
                        }
                    }
                }
            }

            return result;
        }

        private bool TryParseEventTime(EventData eventData, DateTime date, out DateTime result)
        {
            result = date;

            try
            {
                // Parse time from TimeFrom and FromAMPM
                if (string.IsNullOrWhiteSpace(eventData.TimeFrom))
                {
                    return false;
                }

                string[] timeParts = eventData.TimeFrom.Split(':');
                if (timeParts.Length != 2)
                {
                    return false;
                }

                int hour = int.Parse(timeParts[0]);
                int minute = int.Parse(timeParts[1]);

                // Adjust for AM/PM
                if (eventData.FromAMPM == "PM" && hour != 12)
                {
                    hour += 12;
                }
                else if (eventData.FromAMPM == "AM" && hour == 12)
                {
                    hour = 0;
                }

                result = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
