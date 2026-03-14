using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NotiHub.Models;

namespace NotiHub.Services
{
    public class EventDataService
    {
        private static EventDataService _instance;
        private const string FolderName = "NotiHub";
        private const string SubFolderName = "EventCalendar";
        private const string FileName = "eventcalendar.json";

        public static EventDataService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventDataService();
                }
                return _instance;
            }
        }

        private EventDataService()
        {
        }

        public string GetFilePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, FolderName, SubFolderName);
            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, FileName);
        }

        public List<EventData> LoadAllEvents()
        {
            string filePath = GetFilePath();

            if (!File.Exists(filePath))
            {
                return new List<EventData>();
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<EventData>>(jsonContent) ?? new List<EventData>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                return new List<EventData>();
            }
        }

        public void SaveEvents(List<EventData> events)
        {
            string filePath = GetFilePath();

            try
            {
                string jsonContent = JsonConvert.SerializeObject(events, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving events: {ex.Message}");
                throw;
            }
        }

        public void SaveEvent(EventData eventData)
        {
            var events = LoadAllEvents();
            
            // Remove existing event with same ID or date (for backward compatibility)
            events.RemoveAll(e => 
                (!string.IsNullOrEmpty(e.Id) && e.Id == eventData.Id) || 
                (string.IsNullOrEmpty(e.Id) && e.EventDate == eventData.EventDate));

            events.Add(eventData);
            SaveEvents(events);
        }

        public void DeleteEvent(string eventId)
        {
            var events = LoadAllEvents();
            events.RemoveAll(e => e.Id == eventId);
            SaveEvents(events);
        }

        public void DeleteEventByDate(string eventDate)
        {
            var events = LoadAllEvents();
            events.RemoveAll(e => e.EventDate == eventDate);
            SaveEvents(events);
        }

        /// <summary>
        /// Search events by name, location, or tags
        /// </summary>
        public List<EventData> SearchEvents(string searchTerm, DateTime? startDate = null, DateTime? endDate = null, List<string> tags = null)
        {
            var allEvents = LoadAllEvents();
            var results = new List<EventData>();

            foreach (var eventData in allEvents)
            {
                bool matchesSearch = string.IsNullOrWhiteSpace(searchTerm) ||
                    eventData.EventName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (eventData.EventLocation != null && eventData.EventLocation.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

                bool matchesTags = tags == null || tags.Count == 0 ||
                    (eventData.Tags != null && eventData.Tags.Any(t => tags.Contains(t.Name, StringComparer.OrdinalIgnoreCase)));

                if (matchesSearch && matchesTags)
                {
                    // Check date range
                    if (startDate.HasValue || endDate.HasValue)
                    {
                        var occurrences = RecurrenceService.Instance.GenerateOccurrences(
                            eventData,
                            startDate ?? DateTime.MinValue,
                            endDate ?? DateTime.MaxValue);

                        if (occurrences.Any())
                        {
                            results.Add(eventData);
                        }
                    }
                    else
                    {
                        results.Add(eventData);
                    }
                }
            }

            return results;
        }
    }
}
