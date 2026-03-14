using System.Drawing;

namespace NotiHub.Models
{
    public class EventTag
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public EventTag()
        {
        }

        public EventTag(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        // Predefined tags
        public static EventTag Work = new EventTag("Work", Color.FromArgb(52, 152, 219));
        public static EventTag Personal = new EventTag("Personal", Color.FromArgb(46, 204, 113));
        public static EventTag Important = new EventTag("Important", Color.FromArgb(231, 76, 60));
        public static EventTag Meeting = new EventTag("Meeting", Color.FromArgb(155, 89, 182));
        public static EventTag Birthday = new EventTag("Birthday", Color.FromArgb(241, 196, 15));
        public static EventTag Holiday = new EventTag("Holiday", Color.FromArgb(26, 188, 156));
    }
}
