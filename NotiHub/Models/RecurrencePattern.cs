using System;

namespace NotiHub.Models
{
    public enum RecurrenceType
    {
        None,
        Minutely,
        Hourly,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class RecurrencePattern
    {
        public RecurrenceType Type { get; set; } = RecurrenceType.None;
        public int Interval { get; set; } = 1; // Every X minutes/hours/days/weeks/months
        public DateTime? EndDate { get; set; } // When recurrence ends (null = never)
        public int? MaxOccurrences { get; set; } // Max number of occurrences (null = unlimited)

        public RecurrencePattern()
        {
        }

        public RecurrencePattern(RecurrenceType type, int interval = 1, DateTime? endDate = null, int? maxOccurrences = null)
        {
            Type = type;
            Interval = interval;
            EndDate = endDate;
            MaxOccurrences = maxOccurrences;
        }
    }
}
