﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class CalendarSchedule : UserControl
    {       
        public static int _year, _month;
        public event Action EventsUpdated;
        private CalendarViewType _currentView = CalendarViewType.Month;
        private DateTime _selectedDate = DateTime.Now;

        public enum CalendarViewType
        {
            Month,
            Week,
            Day,
            Agenda
        }

        public CalendarSchedule()
        {
            InitializeComponent();
        }

        private void CalendarSchedule_Load(object sender, EventArgs e)
        {
            showDays(DateTime.Now.Month, DateTime.Now.Year);
            flowLayoutPanel1.SizeChanged += (s, ev) =>
            {
                if (_currentView == CalendarViewType.Day)
                    RenderDayView();
                else if (_currentView == CalendarViewType.Agenda)
                    RenderAgendaView();
            };
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            _month -= 1;
            if (_month < 1)
            {
                _month = 12;
                _year -= 1;
            }
            showDays(_month, _year);
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _month += 1;
            if (_month > 12)
            {
                _month = 1;
                _year += 1;
            }
            showDays(_month, _year);
        }

        private void btnEventCount_Click(object sender, EventArgs e)
        {
            // Get events for the current month
            var eventsForMonth = CalendarDay.GetEventsForMonth(_month, _year);

            // Show NotificationWindow with events for this month
            using (NotificationWindow notifWindow = new NotificationWindow(null))
            {
                notifWindow.LoadMonthEvents(eventsForMonth, _month, _year);
                notifWindow.ShowDialog();
            }
        }

        private void lbMonth_Click(object sender, EventArgs e)
        {
            using (MonthPicker picker = new MonthPicker(_month, _year))
            {
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    showDays(picker.SelectedMonth, picker.SelectedYear);
                }
            }
        }

        public void showDays(int month, int year)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = false;
            _year = year;
            _month = month;

            string monthName = new DateTimeFormatInfo().GetMonthName(month);
            lbMonth.Text = monthName + " " + year;
            DateTime startOfTheMonth = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int week = (int)startOfTheMonth.DayOfWeek + 1;

            // Add empty placeholders for days before the start of the month
            for (int i = 1; i < week; i++)
            {
                CalendarDay placeholderDay = new CalendarDay("", this); // Pass empty day
                flowLayoutPanel1.Controls.Add(placeholderDay);
            }

            // Add actual days of the month
            for (int i = 1; i <= daysInMonth; i++)
            {
                CalendarDay actualDay = new CalendarDay(i.ToString(), this);
                flowLayoutPanel1.Controls.Add(actualDay);
            }

            // Update event count after rendering
            UpdateEventCount();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
        "Do you want to refresh the events?",
        "Confirm Refresh",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    RefreshEventData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error refreshing events: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

        }

        public void RefreshEventData()
        {
            showDays(_month, _year);
            UpdateEventCount();
            EventsUpdated?.Invoke();
        }

        public void UpdateEventCount()
        {
            // Get events for the current month and year from the static method in uDay
            var eventsForMonth = CalendarDay.GetEventsForMonth(_month, _year);

            // Get the event count
            int eventCount = eventsForMonth.Count;

            // Update the btnEventCount text with the event count
            btnEventCount.Text = $"{eventCount}";
        }

        public void SwitchView(CalendarViewType viewType)
        {
            _currentView = viewType;
            
            switch (viewType)
            {
                case CalendarViewType.Month:
                    ShowMonthView();
                    break;
                case CalendarViewType.Week:
                    ShowWeekView();
                    break;
                case CalendarViewType.Day:
                    ShowDayView();
                    break;
                case CalendarViewType.Agenda:
                    ShowAgendaView();
                    break;
            }
        }

        private void ShowMonthView()
        {
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = false;
            showDays(_month, _year);
        }

        private void ShowWeekView()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = true;

            // Get the start of the week (Sunday)
            DateTime startOfWeek = _selectedDate.AddDays(-(int)_selectedDate.DayOfWeek);

            // Update label
            DateTime endOfWeek = startOfWeek.AddDays(6);
            lbMonth.Text = $"{startOfWeek:MMM d} - {endOfWeek:MMM d, yyyy}";

            // Available width: flowLayoutPanel1 is 784px, 7 columns with 4px margin each side
            int colWidth = (flowLayoutPanel1.Width - 20) / 7; // ~109px per column

            for (int i = 0; i < 7; i++)
            {
                DateTime currentDay = startOfWeek.AddDays(i);
                Panel dayColumn = CreateWeekDayColumn(currentDay, colWidth);
                flowLayoutPanel1.Controls.Add(dayColumn);
            }
        }

        private Panel CreateWeekDayColumn(DateTime date, int width)
        {
            bool isToday = date.Date == DateTime.Now.Date;

            Panel column = new Panel
            {
                Width = width,
                Height = flowLayoutPanel1.Height - 10,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(43, 50, 52),
                Margin = new Padding(1, 0, 1, 0)
            };

            // Day header
            Label dayLabel = new Label
            {
                Text = $"{date:ddd}\n{date:MMM d}",
                Dock = DockStyle.Top,
                Height = 45,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = isToday ? Color.FromArgb(52, 152, 219) : Color.White,
                BackColor = isToday ? Color.FromArgb(30, 52, 152, 219) : Color.FromArgb(50, 58, 61)
            };
            column.Controls.Add(dayLabel);

            // Scrollable events area
            Panel eventsArea = new Panel
            {
                Location = new Point(0, 45),
                Width = width,
                Height = column.Height - 45,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            var events = CalendarDay.GetEventsForMonth(date.Month, date.Year)
                .Where(e => DateTime.TryParse(e.EventDate, out DateTime eventDate) && eventDate.Date == date.Date)
                .OrderBy(e => e.TimeFrom)
                .ToList();

            int yPos = 4;
            foreach (var evt in events)
            {
                Panel eventCard = CreateWeekEventCard(evt, width - 6);
                eventCard.Location = new Point(3, yPos);
                eventsArea.Controls.Add(eventCard);
                yPos += eventCard.Height + 3;
            }

            column.Controls.Add(eventsArea);
            return column;
        }

        private Panel CreateWeekEventCard(EventData evt, int width)
        {
            Color bg = GetPriorityColor(evt.Priority);
            string nameText = !string.IsNullOrWhiteSpace(evt.EventName) ? evt.EventName : "(No Title)";

            Panel card = new Panel
            {
                Width = width,
                Height = 52,
                BackColor = bg,
                Cursor = Cursors.Hand
            };

            Label timeLabel = new Label
            {
                Text = $"{evt.TimeFrom} {evt.FromAMPM}",
                Location = new Point(4, 3),
                Width = width - 8,
                Height = 16,
                Font = new Font("Segoe UI", 7.5F),
                ForeColor = Color.FromArgb(220, 220, 220),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft
            };

            Label nameLabel = new Label
            {
                Text = nameText,
                Location = new Point(4, 20),
                Width = width - 8,
                Height = 28,
                Font = new Font("Segoe UI Semibold", 8.5F),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft,
                AutoEllipsis = true
            };

            card.Controls.Add(timeLabel);
            card.Controls.Add(nameLabel);

            card.Click += (s, e) => OpenEventDetails(evt);
            timeLabel.Click += (s, e) => OpenEventDetails(evt);
            nameLabel.Click += (s, e) => OpenEventDetails(evt);

            return card;
        }

        private int _dayViewPage = 0;
        private const int DayViewHoursPerPage = 6;

        private int _agendaPage = 0;

        private void ShowDayView()
        {
            _dayViewPage = 0;
            RenderDayView();
        }

        private void RenderDayView()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = false;

            lbMonth.Text = _selectedDate.ToString("dddd, MMMM d, yyyy");

            const int btnSize = 30;
            const int barPaddingTop = 10;
            const int barPaddingBottom = 10;
            int barHeight = btnSize + barPaddingTop + barPaddingBottom;
            int separatorH = 1;
            int totalReserved = barHeight + separatorH;
            int slotHeight = Math.Max(40, (flowLayoutPanel1.Height - totalReserved) / DayViewHoursPerPage);
            int slotsHeight = DayViewHoursPerPage * slotHeight;
            int timeColWidth = 70;
            int panelWidth = flowLayoutPanel1.Width - 4;
            int totalPages = 24 / DayViewHoursPerPage;
            int startHour = _dayViewPage * DayViewHoursPerPage;
            int endHour = startHour + DayViewHoursPerPage;

            // One outer container — added to flowLayoutPanel1 as single child
            int outerHeight = slotsHeight + separatorH + barHeight;
            Panel outer = new Panel
            {
                Location = new Point(0, 0),
                Width = panelWidth,
                Height = outerHeight,
                MinimumSize = new Size(panelWidth, outerHeight),
                MaximumSize = new Size(panelWidth, outerHeight),
                BackColor = Color.FromArgb(40, 48, 51)
            };

            // Time slots
            var events = CalendarDay.GetEventsForMonth(_selectedDate.Month, _selectedDate.Year)
                .Where(e => DateTime.TryParse(e.EventDate, out DateTime ed) && ed.Date == _selectedDate.Date)
                .ToList();

            for (int hour = startHour; hour < endHour; hour++)
            {
                int localY = (hour - startHour) * slotHeight;

                Panel timeSlot = new Panel
                {
                    Location = new Point(0, localY),
                    Width = panelWidth,
                    Height = slotHeight,
                    BackColor = hour % 2 == 0 ? Color.FromArgb(45, 53, 56) : Color.FromArgb(42, 50, 53)
                };
                timeSlot.Controls.Add(new Panel { Dock = DockStyle.Bottom, Height = 1, BackColor = Color.FromArgb(55, 63, 66) });
                timeSlot.Controls.Add(new Label
                {
                    Text = $"{(hour == 0 ? 12 : hour > 12 ? hour - 12 : hour):00} {(hour < 12 ? "AM" : "PM")}",
                    Location = new Point(5, 0),
                    Width = timeColWidth - 5,
                    Height = slotHeight,
                    Font = new Font("Segoe UI", 8),
                    ForeColor = Color.FromArgb(120, 130, 135),
                    TextAlign = ContentAlignment.MiddleLeft
                });
                outer.Controls.Add(timeSlot);
            }

            foreach (var evt in events)
            {
                if (!DateTime.TryParse($"{evt.TimeFrom} {evt.FromAMPM}", out DateTime st)) continue;
                if (st.Hour < startHour || st.Hour >= endHour) continue;

                int localY = (st.Hour - startHour) * slotHeight + (st.Minute * slotHeight / 60) + 4;
                string nameText = !string.IsNullOrWhiteSpace(evt.EventName) ? evt.EventName : "(No Title)";
                Color bg = GetPriorityColor(evt.Priority);

                Panel card = new Panel
                {
                    Location = new Point(timeColWidth + 4, localY),
                    Width = panelWidth - timeColWidth - 8,
                    Height = slotHeight - 8,
                    BackColor = bg,
                    Cursor = Cursors.Hand
                };
                card.Controls.Add(new Panel { Location = new Point(0, 0), Width = 3, Height = card.Height, BackColor = Color.FromArgb(Math.Max(0, bg.R - 40), Math.Max(0, bg.G - 40), Math.Max(0, bg.B - 40)) });
                card.Controls.Add(new Label { Text = $"{nameText}   {evt.TimeFrom} {evt.FromAMPM}", Location = new Point(10, 0), Width = card.Width - 14, Height = card.Height, Font = new Font("Segoe UI Semibold", 9), ForeColor = Color.White, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleLeft, AutoEllipsis = true });
                card.Click += (s, e) => OpenEventDetails(evt);
                card.Controls[0].Click += (s, e) => OpenEventDetails(evt);
                card.Controls[1].Click += (s, e) => OpenEventDetails(evt);
                outer.Controls.Add(card);
                card.BringToFront();
            }

            // Separator 
            outer.Controls.Add(new Panel
            {
                Location = new Point(0, slotsHeight),
                Width = panelWidth,
                Height = separatorH,
                BackColor = Color.FromArgb(55, 63, 66)
            });

            // Pagination bar 
            int pillWidth = 100;
            int pillSpacing = 5;
            int btnGap = 8;
            int totalPillsWidth = totalPages * pillWidth + (totalPages - 1) * pillSpacing;
            int groupWidth = btnSize + btnGap + totalPillsWidth + btnGap + btnSize;
            int groupStartX = (panelWidth - groupWidth) / 2;
            int barY = slotsHeight + separatorH;
            int btnY = barY + barPaddingTop;

            outer.Controls.Add(new Panel
            {
                Location = new Point(0, barY),
                Width = panelWidth,
                Height = barHeight,
                BackColor = Color.FromArgb(33, 40, 43)
            });

            // Prev
            Button btnPrev = new Button
            {
                Text = "❮",
                Location = new Point(groupStartX, btnY),
                Width = btnSize, Height = btnSize,
                FlatStyle = FlatStyle.Flat,
                BackColor = _dayViewPage > 0 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(50, 58, 61),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Enabled = _dayViewPage > 0, Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.Click += (s, e) => { _dayViewPage--; RenderDayView(); };
            MakeRoundButton(btnPrev, 6);
            outer.Controls.Add(btnPrev);
            btnPrev.BringToFront();

            int pillStartX = groupStartX + btnSize + btnGap;
            for (int i = 0; i < totalPages; i++)
            {
                int idx = i;
                bool isActive = i == _dayViewPage;
                Button pill = new Button
                {
                    Text = $"{FormatHour(i * DayViewHoursPerPage)}–{FormatHour((i + 1) * DayViewHoursPerPage)}",
                    Location = new Point(pillStartX + i * (pillWidth + pillSpacing), btnY),
                    Width = pillWidth, Height = btnSize,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = isActive ? Color.FromArgb(52, 152, 219) : Color.FromArgb(50, 58, 61),
                    ForeColor = isActive ? Color.White : Color.FromArgb(160, 170, 175),
                    Font = new Font("Segoe UI Semibold", 7.5F),
                    Cursor = Cursors.Hand, TextAlign = ContentAlignment.MiddleCenter
                };
                pill.FlatAppearance.BorderSize = 0;
                pill.Click += (s, e) => { _dayViewPage = idx; RenderDayView(); };
                outer.Controls.Add(pill);
                pill.BringToFront();
            }

            // Next
            Button btnNext = new Button
            {
                Text = "❯",
                Location = new Point(pillStartX + totalPillsWidth + btnGap, btnY),
                Width = btnSize, Height = btnSize,
                FlatStyle = FlatStyle.Flat,
                BackColor = _dayViewPage < totalPages - 1 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(50, 58, 61),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Enabled = _dayViewPage < totalPages - 1, Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += (s, e) => { _dayViewPage++; RenderDayView(); };
            outer.Controls.Add(btnNext);
            btnNext.BringToFront();

            flowLayoutPanel1.Controls.Add(outer);
        }

        private string FormatHour(int hour)
        {
            int h = hour % 24;
            string ampm = h < 12 ? "AM" : "PM";
            int display = h == 0 ? 12 : h > 12 ? h - 12 : h;
            return $"{display:00} {ampm}";
        }

        private void MakeRoundButton(Button btn, int radius = 6)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Paint += (s, e) =>
            {
                var b = (Button)s;
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int r = radius * 2;
                    path.AddArc(0, 0, r, r, 180, 90);
                    path.AddArc(b.Width - r, 0, r, r, 270, 90);
                    path.AddArc(b.Width - r, b.Height - r, r, r, 0, 90);
                    path.AddArc(0, b.Height - r, r, r, 90, 90);
                    path.CloseFigure();
                    using (var brush = new SolidBrush(b.BackColor))
                        g.FillPath(brush, path);
                    using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                    using (var textBrush = new SolidBrush(b.ForeColor))
                        g.DrawString(b.Text, b.Font, textBrush, new RectangleF(0, 0, b.Width, b.Height), sf);
                }
                b.Region = new Region(new System.Drawing.Drawing2D.GraphicsPath());
            };
        }





        private void ShowAgendaView()
        {
            _agendaPage = 0;
            RenderAgendaView();
        }

        private void RenderAgendaView()
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoScroll = false;

            lbMonth.Text = $"Agenda - {new DateTimeFormatInfo().GetMonthName(_month)} {_year}";

            var events = CalendarDay.GetEventsForMonth(_month, _year)
                .OrderBy(e => DateTime.TryParse(e.EventDate, out DateTime d) ? d : DateTime.MaxValue)
                .ThenBy(e => e.TimeFrom)
                .ToList();

            var groupedEvents = events.GroupBy(e => e.EventDate).ToList();
            int totalGroups = groupedEvents.Count;

            const int btnSize = 30;
            const int barPaddingTop = 10;
            const int barPaddingBottom = 10;
            int barHeight = btnSize + barPaddingTop + barPaddingBottom;
            int separatorH = 1;
            int panelWidth = flowLayoutPanel1.Width - 4;
            int contentHeight = flowLayoutPanel1.Height - separatorH - barHeight;
            const int groupGap = 6;

            // Measure all group heights first
            var groupHeights = groupedEvents.Select(g =>
            {
                var p = CreateAgendaDateGroup(g.Key, g.ToList());
                return p.Height + groupGap;
            }).ToList();

            // Build pages: fill each page until contentHeight is exceeded ─
            var pages = new List<(int start, int end)>();
            int gi = 0;
            while (gi < totalGroups)
            {
                int used = 0;
                int pageStart = gi;
                while (gi < totalGroups && used + groupHeights[gi] <= contentHeight)
                {
                    used += groupHeights[gi];
                    gi++;
                }
                // ensure at least one group per page to avoid infinite loop
                if (gi == pageStart) gi++;
                pages.Add((pageStart, gi));
            }

            int totalPages = Math.Max(1, pages.Count);
            _agendaPage = Math.Max(0, Math.Min(_agendaPage, totalPages - 1));

            int outerHeight = contentHeight + separatorH + barHeight;
            Panel outer = new Panel
            {
                Location = new Point(0, 0),
                Width = panelWidth,
                Height = outerHeight,
                MinimumSize = new Size(panelWidth, outerHeight),
                MaximumSize = new Size(panelWidth, outerHeight),
                BackColor = Color.FromArgb(40, 48, 51)
            };

            // Clipping panel 
            Panel clip = new Panel
            {
                Location = new Point(0, 0),
                Width = panelWidth,
                Height = contentHeight,
                BackColor = Color.FromArgb(40, 48, 51)
            };

            if (totalGroups == 0)
            {
                clip.Controls.Add(new Label
                {
                    Text = "No events this month",
                    Font = new Font("Segoe UI", 12),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(20, 20)
                });
            }
            else
            {
                var (startIdx, endIdx) = pages[_agendaPage];
                int cardY = 0;
                for (int i = startIdx; i < endIdx; i++)
                {
                    var group = groupedEvents[i];
                    Panel dateGroup = CreateAgendaDateGroup(group.Key, group.ToList());
                    dateGroup.Location = new Point(0, cardY);
                    dateGroup.Width = panelWidth;
                    clip.Controls.Add(dateGroup);
                    cardY += dateGroup.Height + groupGap;
                }
            }

            outer.Controls.Add(clip);

            // Layout constants 
            int barY = contentHeight + separatorH;
            int btnY = barY + barPaddingTop;
            int btnGap = 6;
            int totalBtnsWidth = btnSize * 4 + btnGap * 3;
            int rightStartX = panelWidth - 16 - totalBtnsWidth;

            // Separator
            outer.Controls.Add(new Panel
            {
                Location = new Point(20, contentHeight),
                Width = rightStartX + totalBtnsWidth - 20,
                Height = separatorH,
                BackColor = Color.FromArgb(55, 63, 66)
            });

            // Bar background
            outer.Controls.Add(new Panel
            {
                Location = new Point(0, barY),
                Width = panelWidth,
                Height = barHeight,
                BackColor = Color.FromArgb(40, 48, 51)
            });

            // Nav buttons
            var navButtons = new[]
            {
                new { Text = "«", Enabled = _agendaPage > 0,              Action = (Action)(() => { _agendaPage = 0; RenderAgendaView(); }) },
                new { Text = "❮", Enabled = _agendaPage > 0,              Action = (Action)(() => { _agendaPage--; RenderAgendaView(); }) },
                new { Text = "❯", Enabled = _agendaPage < totalPages - 1, Action = (Action)(() => { _agendaPage++; RenderAgendaView(); }) },
                new { Text = "»", Enabled = _agendaPage < totalPages - 1, Action = (Action)(() => { _agendaPage = totalPages - 1; RenderAgendaView(); }) },
            };

            for (int i = 0; i < navButtons.Length; i++)
            {
                var nb = navButtons[i];
                Button btn = new Button
                {
                    Text = nb.Text,
                    Location = new Point(rightStartX + i * (btnSize + btnGap), btnY),
                    Width = btnSize, Height = btnSize,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = nb.Enabled ? Color.FromArgb(52, 152, 219) : Color.FromArgb(50, 58, 61),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Enabled = nb.Enabled, Cursor = Cursors.Hand,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                btn.FlatAppearance.BorderSize = 0;
                var action = nb.Action;
                btn.Click += (s, ev) => action();
                MakeRoundButton(btn, 6);
                outer.Controls.Add(btn);
                btn.BringToFront();
            }

            flowLayoutPanel1.Controls.Add(outer);
        }

        private Panel CreateAgendaDateGroup(string date, List<EventData> events)
        {
            int cardWidth = 740;
            int groupWidth = 780;

            Panel group = new Panel
            {
                Width = groupWidth,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, 0, 8)
            };

            // Date header
            Label dateLabel = new Label
            {
                Text = DateTime.TryParse(date, out DateTime d) ? d.ToString("dddd, MMMM d, yyyy") : date,
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI Semibold", 10),
                ForeColor = Color.FromArgb(52, 152, 219),
                Padding = new Padding(20, 5, 0, 0)
            };
            group.Controls.Add(dateLabel);

            // Events stacked below header
            int yOffset = 30;
            foreach (var evt in events)
            {
                Panel eventCard = CreateAgendaEventCard(evt, cardWidth);
                eventCard.Location = new Point(20, yOffset);
                group.Controls.Add(eventCard);
                yOffset += eventCard.Height + 4;
            }

            group.Height = yOffset + 4;
            return group;
        }

        private Panel CreateAgendaEventCard(EventData evt, int cardWidth = 740)
        {
            Color priorityColor = GetPriorityColor(evt.Priority);
            Color statusColor = GetStatusColor(evt.Status);
            string nameText = !string.IsNullOrWhiteSpace(evt.EventName) ? evt.EventName : "(No Title)";
            string timeText = $"{evt.TimeFrom} {evt.FromAMPM} - {evt.TimeTo} {evt.ToAMPM}";
            string locationText = evt.EventLocation ?? "";
            string statusText = evt.Status ?? "";

            int statusWidth = 90;
            int barWidth = 5;
            int contentX = barWidth + 8;
            int contentWidth = cardWidth - contentX - statusWidth - 8;

            Panel card = new Panel
            {
                Width = cardWidth,
                Height = 50,
                BackColor = Color.FromArgb(43, 50, 52),
                Margin = new Padding(0, 3, 0, 3),
                Cursor = Cursors.Hand
            };

            // Priority bar
            Panel bar = new Panel
            {
                BackColor = priorityColor,
                Location = new Point(0, 0),
                Width = barWidth,
                Height = 50
            };
            card.Controls.Add(bar);

            // Event name
            Label lblName = new Label
            {
                Text = nameText,
                Font = new Font("Segoe UI Semibold", 10),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(contentX, 5),
                Width = contentWidth,
                Height = 22,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            card.Controls.Add(lblName);

            // Time + location
            string subText = string.IsNullOrEmpty(locationText) ? timeText : $"{timeText}   {locationText}";
            Label lblSub = new Label
            {
                Text = subText,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray,
                BackColor = Color.Transparent,
                Location = new Point(contentX, 28),
                Width = contentWidth,
                Height = 18,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            card.Controls.Add(lblSub);

            // Status label — anchored to right edge, vertically centered
            Label lblStatus = new Label
            {
                Text = statusText,
                Font = new Font("Segoe UI Semibold", 9),
                ForeColor = statusColor,
                BackColor = Color.Transparent,
                Location = new Point(cardWidth - statusWidth, 0),
                Width = statusWidth,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblStatus);

            card.Click += (s, e) => OpenEventDetails(evt);
            bar.Click += (s, e) => OpenEventDetails(evt);
            lblName.Click += (s, e) => OpenEventDetails(evt);
            lblSub.Click += (s, e) => OpenEventDetails(evt);
            lblStatus.Click += (s, e) => OpenEventDetails(evt);
            
            return card;
        }

        private Color GetPriorityColor(int priority)
        {
            switch (priority)
            {
                case 2: return Color.FromArgb(231, 76, 60); // Urgent - Red
                case 1: return Color.FromArgb(243, 156, 18); // High - Orange
                default: return Color.FromArgb(52, 152, 219); // Normal - Blue
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "completed": return Color.FromArgb(46, 204, 113);
                case "pending": return Color.FromArgb(241, 196, 15);
                case "expired": return Color.FromArgb(231, 76, 60);
                case "cancel": return Color.FromArgb(149, 165, 166);
                default: return Color.Gray;
            }
        }

        private void OpenEventDetails(EventData evt)
        {
            // Open event details dialog
            using (CalendarAddSchedule addSchedule = new CalendarAddSchedule(evt.EventDate, this, evt))
            {
                addSchedule.ShowDialog();
            }
        }

        private void btnViewMonth_Click(object sender, EventArgs e)
        {
            UpdateViewButtonStyles(btnViewMonth);
            SwitchView(CalendarViewType.Month);
        }

        private void btnViewWeek_Click(object sender, EventArgs e)
        {
            UpdateViewButtonStyles(btnViewWeek);
            SwitchView(CalendarViewType.Week);
        }

        private void btnViewDay_Click(object sender, EventArgs e)
        {
            UpdateViewButtonStyles(btnViewDay);
            SwitchView(CalendarViewType.Day);
        }

        private void btnViewAgenda_Click(object sender, EventArgs e)
        {
            UpdateViewButtonStyles(btnViewAgenda);
            SwitchView(CalendarViewType.Agenda);
        }

        private void UpdateViewButtonStyles(Button activeButton)
        {
            // Reset all buttons to inactive style
            btnViewMonth.BackColor = Color.FromArgb(50, 58, 61);
            btnViewMonth.ForeColor = Color.LightGray;
            btnViewWeek.BackColor = Color.FromArgb(50, 58, 61);
            btnViewWeek.ForeColor = Color.LightGray;
            btnViewDay.BackColor = Color.FromArgb(50, 58, 61);
            btnViewDay.ForeColor = Color.LightGray;
            btnViewAgenda.BackColor = Color.FromArgb(50, 58, 61);
            btnViewAgenda.ForeColor = Color.LightGray;

            // Set active button style
            activeButton.BackColor = Color.FromArgb(52, 152, 219);
            activeButton.ForeColor = Color.White;
        }
    }

    public class EventData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string EventName { get; set; }
        public string TimeFrom { get; set; }
        public string FromAMPM { get; set; }
        public string TimeTo { get; set; }
        public string ToAMPM { get; set; }
        public string EventLocation { get; set; }
        public string EventDate { get; set; }
        public string Status { get; set; } = "Pending"; // Default status
        
        // New properties for enhancements
        public Models.RecurrencePattern Recurrence { get; set; }
        public List<Models.EventTag> Tags { get; set; } = new List<Models.EventTag>();
        public Models.ReminderSettings Reminders { get; set; } = new Models.ReminderSettings();
        public string Notes { get; set; }
        public int Priority { get; set; } = 0; // 0=Normal, 1=High, 2=Urgent
    }
}
