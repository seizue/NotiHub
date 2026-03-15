using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class NotificationWindow : Form
    {
        private Main mainForm;

        public NotificationWindow(Main parentForm)
        {
            InitializeComponent();
            mainForm = parentForm;
            
            // Configure flow layout panel for vertical scrolling only
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.HorizontalScroll.Enabled = false;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            flowLayoutPanel1.VerticalScroll.Enabled = true;
            flowLayoutPanel1.VerticalScroll.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadUpcomingEvents(List<EventData> events)
        {
            lbMonth.Text = "UPCOMING EVENTS";
            flowLayoutPanel1.Controls.Clear();

            if (events.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = "No upcoming events",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Gray,
                    Size = new Size(380, 40),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(10)
                };
                flowLayoutPanel1.Controls.Add(noEventsLabel);
                lblPageInfo.Text = "0 events";
                return;
            }

            // Sort events by date (earliest first)
            var sortedEvents = events.OrderBy(e =>
            {
                TryParseEventDate(e.EventDate, out DateTime date);
                return date;
            }).ToList();

            // Calculate event status counts
            int currentCount = 0;
            int pastCount = 0;
            DateTime today = DateTime.Now.Date;

            foreach (var evt in sortedEvents)
            {
                if (TryParseEventDate(evt.EventDate, out DateTime eventDate))
                {
                    if (eventDate.Date < today)
                        pastCount++;
                    else if (eventDate.Date >= today)
                        currentCount++;
                }
            }

            // Calculate if scrollbar will be needed
            int totalHeight = CalculateTotalHeight(sortedEvents);
            bool needsScrollbar = totalHeight > flowLayoutPanel1.Height;

            foreach (var evt in sortedEvents)
            {
                ReaLTaiizor.Controls.Panel eventCard = CreateEventCard(evt, needsScrollbar);
                flowLayoutPanel1.Controls.Add(eventCard);
            }

            // Update page info with counts
            lblPageInfo.Text = $"Current: {currentCount} | Past: {pastCount} | Total: {sortedEvents.Count}";
        }

        public void LoadMonthEvents(List<EventData> events, int month, int year)
        {
            string monthName = new System.Globalization.DateTimeFormatInfo().GetMonthName(month);
            lbMonth.Text = $"{monthName.ToUpper()} {year} EVENTS";
            flowLayoutPanel1.Controls.Clear();

            if (events.Count == 0)
            {
                Label noEventsLabel = new Label
                {
                    Text = $"No events in {monthName} {year}",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.Gray,
                    Size = new Size(380, 40),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(10)
                };
                flowLayoutPanel1.Controls.Add(noEventsLabel);
                lblPageInfo.Text = "0 events";
                return;
            }

            // Sort events by date (earliest first)
            var sortedEvents = events.OrderBy(e =>
            {
                TryParseEventDate(e.EventDate, out DateTime date);
                return date;
            }).ToList();

            // Calculate event status counts
            int currentCount = 0;
            int pastCount = 0;
            DateTime today = DateTime.Now.Date;

            foreach (var evt in sortedEvents)
            {
                if (TryParseEventDate(evt.EventDate, out DateTime eventDate))
                {
                    if (eventDate.Date < today)
                        pastCount++;
                    else if (eventDate.Date >= today)
                        currentCount++;
                }
            }

            // Calculate if scrollbar will be needed
            int totalHeight = CalculateTotalHeight(sortedEvents);
            bool needsScrollbar = totalHeight > flowLayoutPanel1.Height;

            foreach (var evt in sortedEvents)
            {
                ReaLTaiizor.Controls.Panel eventCard = CreateEventCard(evt, needsScrollbar);
                flowLayoutPanel1.Controls.Add(eventCard);
            }

            // Update page info with counts
            lblPageInfo.Text = $"Current: {currentCount} | Past: {pastCount} | Total: {sortedEvents.Count}";
        }

        private int CalculateTotalHeight(List<EventData> events)
        {
            int totalHeight = 0;
            foreach (var evt in events)
            {
                int baseHeight = 60;
                if (!string.IsNullOrEmpty(evt.EventLocation))
                {
                    baseHeight += 20;
                }
                totalHeight += baseHeight + 13; // Include margin (3 + 10)
            }
            return totalHeight;
        }

        private ReaLTaiizor.Controls.Panel CreateEventCard(EventData evt, bool needsScrollbar)
        {
            // Calculate height based on content
            int baseHeight = 60;
            int extraHeight = 0;
            
            // Add extra height if location is present
            if (!string.IsNullOrEmpty(evt.EventLocation))
            {
                extraHeight += 20; // Add space for location line
            }
            
            int cardHeight = baseHeight + extraHeight;
            
            // Adjust width based on scrollbar presence
            int cardWidth = needsScrollbar ? 370 : 387;
            int labelWidth = needsScrollbar ? 270 : 280;
            int badgeX = needsScrollbar ? 290 : 305;

            ReaLTaiizor.Controls.Panel card = new ReaLTaiizor.Controls.Panel
            {
                Size = new Size(cardWidth, cardHeight),
                BackColor = Color.FromArgb(38, 44, 48),
                EdgeColor = Color.FromArgb(40, 48, 51),
                Margin = new Padding(3, 3, 3, 10),
                Cursor = Cursors.Hand,
                Padding = new Padding(10)
            };

            ReaLTaiizor.Controls.BigLabel nameLabel = new ReaLTaiizor.Controls.BigLabel
            {
                Text = evt.EventName,
                Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 128, 128),
                Location = new Point(10, 10),
                Size = new Size(labelWidth, 19),
                AutoEllipsis = true,
                BackColor = Color.Transparent
            };
            card.Controls.Add(nameLabel);

            string details = $"{evt.EventDate} • {evt.TimeFrom} {evt.FromAMPM}";
            if (!string.IsNullOrEmpty(evt.EventLocation))
            {
                details += $"\n{evt.EventLocation}";
            }

            ReaLTaiizor.Controls.BigLabel detailsLabel = new ReaLTaiizor.Controls.BigLabel
            {
                Text = details,
                Font = new Font("Segoe UI", 8.25F, FontStyle.Italic),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location = new Point(10, 32),
                Size = new Size(labelWidth, cardHeight - 42),
                BackColor = Color.Transparent
            };
            card.Controls.Add(detailsLabel);

            if (TryParseEventDate(evt.EventDate, out DateTime eventDate))
            {
                int daysUntil = (eventDate - DateTime.Now).Days;
                string daysText = daysUntil == 0 ? "Today" : daysUntil == 1 ? "Tomorrow" : $"{daysUntil}d";
                
                // Center the badge vertically in the card
                int badgeY = (cardHeight - 30) / 2;
                
                Label badgeLabel = new Label
                {
                    Text = daysText,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = daysUntil <= 1 ? Color.FromArgb(231, 76, 60) : Color.FromArgb(52, 152, 219),
                    Location = new Point(badgeX, badgeY),
                    Size = new Size(70, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(badgeLabel);
            }

            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(50, 58, 61);
            card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(38, 44, 48);

            EventHandler clickHandler = (s, e) =>
            {
                if (mainForm != null)
                {
                    mainForm.SetActiveView(Main.ActiveView.Calendar);
                    mainForm.ShowEventOnCalendar(evt.EventDate);
                    this.Close();
                }
            };
            
            card.Click += clickHandler;
            foreach (Control ctrl in card.Controls)
            {
                ctrl.Click += clickHandler;
            }

            return card;
        }

        private bool TryParseEventDate(string dateString, out DateTime result)
        {
            string[] formats = { "M/d/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy", "MM-dd-yyyy" };
            return DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }
    }
}
