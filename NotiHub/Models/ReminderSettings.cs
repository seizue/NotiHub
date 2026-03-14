using System;
using System.Collections.Generic;

namespace NotiHub.Models
{
    public enum ReminderTime
    {
        None = 0,
        FiveMinutes = 5,
        FifteenMinutes = 15,
        ThirtyMinutes = 30,
        OneHour = 60,
        TwoHours = 120,
        OneDay = 1440,
        TwoDays = 2880,
        OneWeek = 10080
    }

    public class ReminderSettings
    {
        public List<ReminderTime> Reminders { get; set; } = new List<ReminderTime>();
        public bool IsEnabled { get; set; } = true;

        public ReminderSettings()
        {
        }

        public ReminderSettings(params ReminderTime[] reminders)
        {
            Reminders = new List<ReminderTime>(reminders);
        }

        public void AddReminder(ReminderTime reminder)
        {
            if (!Reminders.Contains(reminder) && reminder != ReminderTime.None)
            {
                Reminders.Add(reminder);
            }
        }

        public void RemoveReminder(ReminderTime reminder)
        {
            Reminders.Remove(reminder);
        }
    }
}
