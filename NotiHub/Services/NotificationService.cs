using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NotiHub.Models;

namespace NotiHub.Services
{
    public class NotificationService
    {
        private static NotificationService _instance;
        private NotifyIcon _notifyIcon;
        private Timer _checkTimer;
        private HashSet<string> _notifiedEvents;

        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationService();
                }
                return _instance;
            }
        }

        private NotificationService()
        {
            _notifiedEvents = new HashSet<string>();
            InitializeNotifyIcon();
            InitializeTimer();
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = true,
                Text = "NotiHub - Event Reminders"
            };

            _notifyIcon.BalloonTipClicked += (s, e) =>
            {
                // Handle notification click - could open the main form
            };
        }

        private void InitializeTimer()
        {
            _checkTimer = new Timer
            {
                Interval = 30000 // Check every 30 seconds for more accurate reminders
            };
            _checkTimer.Tick += CheckUpcomingEvents;
            _checkTimer.Start();
        }

        private void CheckUpcomingEvents(object sender, EventArgs e)
        {
            try
            {
                var now = DateTime.Now;
                var allEvents = EventDataService.Instance.LoadAllEvents();

                // Clean up old notifications (older than 1 hour)
                CleanupOldNotifications(now);

                foreach (var eventData in allEvents)
                {
                    // Get next occurrence of this event
                    var occurrences = RecurrenceService.Instance.GenerateOccurrences(
                        eventData, 
                        now.Date, 
                        now.Date.AddDays(1));

                    foreach (var occurrence in occurrences)
                    {
                        // Parse event time
                        if (!TryParseEventTime(eventData, occurrence, out DateTime eventDateTime))
                        {
                            continue;
                        }

                        // Skip past events
                        if (eventDateTime < now)
                        {
                            continue;
                        }

                        var minutesUntil = (eventDateTime - now).TotalMinutes;

                        // Check for 5-minute warning
                        if (minutesUntil <= 5 && minutesUntil > 4)
                        {
                            string notificationKey = $"{eventData.Id}_5min_{eventDateTime:yyyyMMddHHmm}";
                            if (!_notifiedEvents.Contains(notificationKey))
                            {
                                ShowEventNotification(eventData, 5, "5 Minutes Warning");
                                _notifiedEvents.Add(notificationKey);
                            }
                        }

                        // Check for event start time (within 1 minute)
                        if (minutesUntil <= 1 && minutesUntil >= 0)
                        {
                            string notificationKey = $"{eventData.Id}_start_{eventDateTime:yyyyMMddHHmm}";
                            if (!_notifiedEvents.Contains(notificationKey))
                            {
                                ShowEventNotification(eventData, 0, "Event Starting Now!");
                                _notifiedEvents.Add(notificationKey);
                            }
                        }

                        // Check custom reminders
                        if (eventData.Reminders != null && eventData.Reminders.IsEnabled)
                        {
                            foreach (var reminder in eventData.Reminders.Reminders)
                            {
                                var reminderTime = eventDateTime.AddMinutes(-(int)reminder);
                                var minutesUntilReminder = (reminderTime - now).TotalMinutes;

                                // Trigger if reminder is within the next 30 seconds
                                if (minutesUntilReminder >= 0 && minutesUntilReminder < 0.5)
                                {
                                    string notificationKey = $"{eventData.Id}_{reminder}_{eventDateTime:yyyyMMddHHmm}";
                                    if (!_notifiedEvents.Contains(notificationKey))
                                    {
                                        int minutesUntilEvent = (int)(eventDateTime - now).TotalMinutes;
                                        ShowEventNotification(eventData, minutesUntilEvent, GetReminderTitle(reminder));
                                        _notifiedEvents.Add(notificationKey);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking upcoming events: {ex.Message}");
            }
        }

        private void CleanupOldNotifications(DateTime now)
        {
            // Remove notification keys older than 1 hour to prevent memory buildup
            var keysToRemove = new List<string>();
            
            foreach (var key in _notifiedEvents)
            {
                // Extract timestamp from key (format: id_type_yyyyMMddHHmm)
                var parts = key.Split('_');
                if (parts.Length >= 3)
                {
                    var timestampStr = parts[parts.Length - 1];
                    if (DateTime.TryParseExact(timestampStr, "yyyyMMddHHmm", null, 
                        System.Globalization.DateTimeStyles.None, out DateTime timestamp))
                    {
                        if ((now - timestamp).TotalHours > 1)
                        {
                            keysToRemove.Add(key);
                        }
                    }
                }
            }

            foreach (var key in keysToRemove)
            {
                _notifiedEvents.Remove(key);
            }
        }

        private string GetReminderTitle(ReminderTime reminder)
        {
            switch (reminder)
            {
                case ReminderTime.FiveMinutes:
                    return "5 Minutes Reminder";
                case ReminderTime.FifteenMinutes:
                    return "15 Minutes Reminder";
                case ReminderTime.ThirtyMinutes:
                    return "30 Minutes Reminder";
                case ReminderTime.OneHour:
                    return "1 Hour Reminder";
                case ReminderTime.TwoHours:
                    return "2 Hours Reminder";
                case ReminderTime.OneDay:
                    return "1 Day Reminder";
                case ReminderTime.TwoDays:
                    return "2 Days Reminder";
                case ReminderTime.OneWeek:
                    return "1 Week Reminder";
                default:
                    return "Event Reminder";
            }
        }

        private void ShowEventNotification(EventData eventData, int minutesUntil, string title)
        {
            string message;
            bool isUrgent = false;

            if (minutesUntil == 0)
            {
                message = $"{eventData.EventName?.ToUpper()}\n STARTING NOW! \n {(eventData.EventLocation ?? "NO LOCATION").ToUpper()}";
                isUrgent = true;
            }
            else if (minutesUntil <= 5)
            {
                message = $"{eventData.EventName?.ToUpper()}\n STARTING IN {minutesUntil} MINUTE(S) \n {(eventData.EventLocation ?? "NO LOCATION").ToUpper()}";
                isUrgent = true;
            }
            else if (minutesUntil < 60)
            {
                message = $"{eventData.EventName?.ToUpper()}\n STARTING IN {minutesUntil} MINUTE(S) \n {(eventData.EventLocation ?? "NO LOCATION").ToUpper()}";
            }
            else if (minutesUntil < 1440) // Less than a day
            {
                int hours = minutesUntil / 60;
                int mins = minutesUntil % 60;
                message = $"{eventData.EventName?.ToUpper()}\n STARTING IN {hours}H {mins}M \n {(eventData.EventLocation ?? "NO LOCATION").ToUpper()}";
            }
            else
            {
                int days = minutesUntil / 1440;
                message = $"{eventData.EventName?.ToUpper()}\n STARTING IN {days} DAY(S) \n {(eventData.EventLocation ?? "NO LOCATION").ToUpper()}";
            }

            // Add priority indicator
            if (eventData.Priority == 2)
            {
                title = "🔴 URGENT - " + title;
                isUrgent = true;
            }
            else if (eventData.Priority == 1)
            {
                title = "🟡 HIGH - " + title;
            }

            // Show custom notification window that stays until closed
            ShowCustomNotification(title, message, isUrgent, eventData);

            // Also log to debug
            System.Diagnostics.Debug.WriteLine($"[NOTIFICATION] {title}: {eventData.EventName} - {minutesUntil} minutes until start");
        }

        private void ShowCustomNotification(string title, string message, bool isUrgent, EventData eventData = null)
        {
            // Show notification as a modal dialog
            // This will block until user closes it
            var notification = new NotifWindow(title, message, isUrgent, eventData);
            notification.ShowDialog(); // Modal - requires user interaction to close
        }

        public void ShowNotification(EventData eventData, int minutesUntil)
        {
            ShowEventNotification(eventData, minutesUntil, "EVENT REMINDER");
        }

        public void ShowNotification(string title, string message)
        {
            ShowCustomNotification(title, message, false);
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

        public void Dispose()
        {
            _checkTimer?.Stop();
            _checkTimer?.Dispose();
            _notifyIcon?.Dispose();
        }
    }
}
