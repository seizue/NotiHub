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
        private Dictionary<string, DateTime> _snoozedEvents; // Track snoozed events

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
            _snoozedEvents = new Dictionary<string, DateTime>();
            InitializeNotifyIcon();
            InitializeTimer();
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Text = "NotiHub - Event Reminders"
            };

            // Try to load custom icon from Resources folder
            try
            {
                // Get the base directory
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                
                // Try multiple paths
                string[] possiblePaths = new[]
                {
                    System.IO.Path.Combine(baseDir, "Resources", "NotiHub_Reminder.ico"),
                    System.IO.Path.Combine(baseDir, "NotiHub_Reminder.ico"),
                    System.IO.Path.Combine(baseDir, "..", "..", "Resources", "NotiHub_Reminder.ico"),
                    System.IO.Path.Combine(baseDir, "..", "..", "NotiHub", "Resources", "NotiHub_Reminder.ico"),
                };

                bool iconLoaded = false;
                foreach (var path in possiblePaths)
                {
                    string fullPath = System.IO.Path.GetFullPath(path);
                    System.Diagnostics.Debug.WriteLine($"[NotificationService] Trying icon path: {fullPath}");
                    
                    if (System.IO.File.Exists(fullPath))
                    {
                        _notifyIcon.Icon = new Icon(fullPath);
                        iconLoaded = true;
                        System.Diagnostics.Debug.WriteLine($"[NotificationService] Successfully loaded icon from: {fullPath}");
                        break;
                    }
                }

                if (!iconLoaded)
                {
                    System.Diagnostics.Debug.WriteLine("[NotificationService] Icon not found in any path, using fallback");
                    // Fallback to application icon or system icon
                    _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath) ?? SystemIcons.Information;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[NotificationService] Failed to load icon: {ex.Message}");
                _notifyIcon.Icon = SystemIcons.Information;
            }

            _notifyIcon.Visible = true;

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

                System.Diagnostics.Debug.WriteLine($"[NotificationService] Checking events at {now:yyyy-MM-dd HH:mm:ss}. Total events: {allEvents.Count}");

                // Clean up old notifications (older than 1 hour)
                CleanupOldNotifications(now);

                // Check snoozed events
                CheckSnoozedEvents(now);

                foreach (var eventData in allEvents)
                {
                    // Skip expired events
                    if (eventData.Status == "Expired" || eventData.Status == "Completed" || eventData.Status == "Cancel")
                    {
                        continue;
                    }

                    // Parse event date and time
                    if (!TryParseEventDate(eventData.EventDate, out DateTime eventDate))
                    {
                        System.Diagnostics.Debug.WriteLine($"[NotificationService] Failed to parse date: {eventData.EventDate}");
                        continue;
                    }

                    // Parse event time
                    if (!TryParseEventTime(eventData, eventDate, out DateTime eventDateTime))
                    {
                        System.Diagnostics.Debug.WriteLine($"[NotificationService] Failed to parse time for: {eventData.EventName}");
                        continue;
                    }

                    // Skip past events
                    if (eventDateTime < now)
                    {
                        continue;
                    }

                    var minutesUntil = (eventDateTime - now).TotalMinutes;

                    System.Diagnostics.Debug.WriteLine($"[NotificationService] Event: {eventData.EventName}, Time: {eventDateTime:yyyy-MM-dd HH:mm}, Minutes until: {minutesUntil:F2}");

                    // Check for 15-minute warning
                    if (minutesUntil <= 15 && minutesUntil > 14)
                    {
                        string notificationKey = $"{eventData.Id}_15min_{eventDateTime:yyyyMMddHHmm}";
                        if (!_notifiedEvents.Contains(notificationKey))
                        {
                            ShowEventNotification(eventData, 15, "15 Minutes Warning");
                            _notifiedEvents.Add(notificationKey);
                        }
                    }

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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[NotificationService] Error checking upcoming events: {ex.Message}\n{ex.StackTrace}");
            }
        }

        private void CheckSnoozedEvents(DateTime now)
        {
            var eventsToShow = new List<string>();

            // Find snoozed events that are ready to show
            foreach (var kvp in _snoozedEvents)
            {
                if (now >= kvp.Value)
                {
                    eventsToShow.Add(kvp.Key);
                }
            }

            // Show snoozed events and remove from snooze list
            foreach (var eventId in eventsToShow)
            {
                // Find the event data
                var allEvents = EventDataService.Instance.LoadAllEvents();
                var eventData = allEvents.FirstOrDefault(e => e.Id == eventId);

                if (eventData != null)
                {
                    System.Diagnostics.Debug.WriteLine($"[NotificationService] Showing snoozed event: {eventData.EventName}");
                    ShowEventNotification(eventData, 0, "⏰ Snoozed Reminder");
                }

                _snoozedEvents.Remove(eventId);
            }
        }

        public void SnoozeEvent(EventData eventData, DateTime snoozeUntil)
        {
            if (eventData != null && !string.IsNullOrEmpty(eventData.Id))
            {
                _snoozedEvents[eventData.Id] = snoozeUntil;
                System.Diagnostics.Debug.WriteLine($"[NotificationService] Event snoozed: {eventData.EventName} until {snoozeUntil:yyyy-MM-dd HH:mm:ss}");
            }
        }

        private bool TryParseEventDate(string dateString, out DateTime result)
        {
            string[] formats = {
                "M/d/yyyy",
                "d/M/yyyy",
                "yyyy-MM-dd",
                "yyyy/MM/dd",
                "dd-MM-yyyy",
                "MM-dd-yyyy"
            };

            return DateTime.TryParseExact(
                dateString,
                formats,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out result);
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
            try
            {
                // Ensure we're on the UI thread
                if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
                {
                    Application.OpenForms[0].Invoke(new Action(() =>
                    {
                        ShowNotificationWindow(title, message, isUrgent, eventData);
                    }));
                }
                else
                {
                    ShowNotificationWindow(title, message, isUrgent, eventData);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[NotificationService] Error showing notification: {ex.Message}\n{ex.StackTrace}");
                
                // Fallback to system tray balloon notification
                try
                {
                    _notifyIcon.BalloonTipTitle = title;
                    _notifyIcon.BalloonTipText = message;
                    _notifyIcon.BalloonTipIcon = isUrgent ? ToolTipIcon.Warning : ToolTipIcon.Info;
                    _notifyIcon.ShowBalloonTip(5000);
                }
                catch
                {
                    // Ignore if even balloon tip fails
                }
            }
        }

        private void ShowNotificationWindow(string title, string message, bool isUrgent, EventData eventData)
        {
            // Create notification window
            var notification = new NotifWindow(title, message, isUrgent, eventData);
            
            // Show as non-modal so it works even when main form is hidden
            notification.Show();
            
            // Bring to front and activate
            notification.BringToFront();
            notification.Activate();
            notification.TopMost = true;
            
            // Log notification
            System.Diagnostics.Debug.WriteLine($"[NotificationService] Showing notification: {title}");
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
