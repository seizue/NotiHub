using Newtonsoft.Json;
using NotiHub.Models;
using NotiHub.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class CalendarAddSchedule : Form
    {
        private EventData _eventData;
        private CalendarSchedule _calendarControl;
        private string _selectedDate;
        private bool _isFormattingEventName = false;
        private bool _isFormattingLocation = false;
        private bool _isFormattingNotes = false;

        public CalendarAddSchedule(string date, CalendarSchedule calendar, EventData existingEvent = null)
        {
            _selectedDate = date;
            _calendarControl = calendar;
            _eventData = existingEvent ?? new EventData { EventDate = date };

            InitializeComponent();
            SetupControls();
            LoadEventData();
        }

        private void SetupControls()
        {
            // Setup time hour combo boxes (1-12)
            for (int i = 1; i <= 12; i++)
            {
                cbTimeFromHour.Items.Add(i.ToString());
                cbTimeToHour.Items.Add(i.ToString());
            }
            cbTimeFromHour.SelectedIndex = 0;
            cbTimeToHour.SelectedIndex = 0;

            // Setup time minute combo boxes (00-55 in 5-minute intervals)
            for (int i = 0; i < 60; i += 5)
            {
                string minute = i.ToString("00");
                cbTimeFromMinute.Items.Add(minute);
                cbTimeToMinute.Items.Add(minute);
            }
            cbTimeFromMinute.SelectedIndex = 0;
            cbTimeToMinute.SelectedIndex = 0;

            // Setup recurrence interval (1-60)
            for (int i = 1; i <= 60; i++)
            {
                cbRecurrenceInterval.Items.Add(i);
            }
            cbRecurrenceInterval.SelectedIndex = 0;

            // Setup recurrence type
            cbRecurrenceType.Items.Clear();
            cbRecurrenceType.Items.AddRange(new[] { "None", "Every X Minutes", "Every X Hours", "Daily", "Weekly", "Monthly", "Yearly" });
            cbRecurrenceType.SelectedIndex = 0;


            // Setup event handlers
            cbRecurrenceType.SelectedIndexChanged += CbRecurrenceType_SelectedIndexChanged;
            chkUseEndDate.CheckedChanged += ChkUseEndDate_CheckedChanged;
            chkUseMaxOccurrences.CheckedChanged += ChkUseMaxOccurrences_CheckedChanged;
            chkEnableReminders.CheckedChanged += ChkEnableReminders_CheckedChanged;

            // Setup tag checkboxes event handlers
            ckTagWork.CheckedChanged += CkTag_CheckedChanged;
            ckTagPersonal.CheckedChanged += CkTag_CheckedChanged;
            ckTagImportant.CheckedChanged += CkTag_CheckedChanged;
            ckTagBirthday.CheckedChanged += CkTag_CheckedChanged;
            ckTagHoliday.CheckedChanged += CkTag_CheckedChanged;
            ckTagMeeting.CheckedChanged += CkTag_CheckedChanged;

            // Setup text input formatting handlers - using TextChanged with flag guards
            txtEventName.TextChanged += TxtEventName_TextChanged;
            txtLocation.TextChanged += TxtLocation_TextChanged;
            txtNotes.TextChanged += TxtNotes_TextChanged;

            // Set default values     
            nudMaxOccurrences.ValueNumber = 10;
            nudMaxOccurrences.Enabled = false;
            cbRecurrenceInterval.Enabled = false;

            // Set selected date 
            lblSelectedDate.Text = $"Date: {_selectedDate}";
        }

        private void CbRecurrenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = cbRecurrenceType.SelectedIndex > 0;
            cbRecurrenceInterval.Enabled = enabled;
            chkUseEndDate.Enable = enabled;
            chkUseMaxOccurrences.Enable = enabled;

            // Update interval label based on recurrence type
            if (label15 != null)
            {
                switch (cbRecurrenceType.SelectedIndex)
                {
                    case 1: // Minutely
                        label15.Text = "Every X Minutes";
                        break;
                    case 2: // Hourly
                        label15.Text = "Every X Hours";
                        break;
                    case 3: // Daily
                        label15.Text = "Every X Days";
                        break;
                    case 4: // Weekly
                        label15.Text = "Every X Weeks";
                        break;
                    case 5: // Monthly
                        label15.Text = "Every X Months";
                        break;
                    case 6: // Yearly
                        label15.Text = "Every X Years";
                        break;
                    default:
                        label15.Text = "Interval";
                        break;
                }
            }
        }

        private void LoadEventData()
        {
            if (_eventData == null) return;

            // Basic tab
            txtEventName.Text = _eventData.EventName ?? "";
            txtLocation.Text = _eventData.EventLocation ?? "";
            txtNotes.Text = _eventData.Notes ?? "";

            // Parse time from
            if (!string.IsNullOrEmpty(_eventData.TimeFrom))
            {
                var timeParts = _eventData.TimeFrom.Split(':');
                if (timeParts.Length == 2)
                {
                    int hourIndex = cbTimeFromHour.Items.IndexOf(timeParts[0]);
                    if (hourIndex >= 0) cbTimeFromHour.SelectedIndex = hourIndex;

                    int minuteIndex = cbTimeFromMinute.Items.IndexOf(timeParts[1]);
                    if (minuteIndex >= 0) cbTimeFromMinute.SelectedIndex = minuteIndex;
                }
            }

            if (!string.IsNullOrEmpty(_eventData.FromAMPM))
            {
                int ampmIndex = cbFromAMPM.Items.IndexOf(_eventData.FromAMPM);
                if (ampmIndex >= 0) cbFromAMPM.SelectedIndex = ampmIndex;
            }

            // Parse time to
            if (!string.IsNullOrEmpty(_eventData.TimeTo))
            {
                var timeParts = _eventData.TimeTo.Split(':');
                if (timeParts.Length == 2)
                {
                    int hourIndex = cbTimeToHour.Items.IndexOf(timeParts[0]);
                    if (hourIndex >= 0) cbTimeToHour.SelectedIndex = hourIndex;

                    int minuteIndex = cbTimeToMinute.Items.IndexOf(timeParts[1]);
                    if (minuteIndex >= 0) cbTimeToMinute.SelectedIndex = minuteIndex;
                }
            }

            if (!string.IsNullOrEmpty(_eventData.ToAMPM))
            {
                int ampmIndex = cbToAMPM.Items.IndexOf(_eventData.ToAMPM);
                if (ampmIndex >= 0) cbToAMPM.SelectedIndex = ampmIndex;
            }

            if (!string.IsNullOrEmpty(_eventData.Status))
            {
                int statusIndex = cbStatus.Items.IndexOf(_eventData.Status);
                if (statusIndex >= 0) cbStatus.SelectedIndex = statusIndex;
            }

            cbPriority.SelectedIndex = _eventData.Priority;

            // Recurrence tab
            if (_eventData.Recurrence != null)
            {
                cbRecurrenceType.SelectedIndex = (int)_eventData.Recurrence.Type;

                int intervalIndex = cbRecurrenceInterval.Items.IndexOf(_eventData.Recurrence.Interval);
                if (intervalIndex >= 0) cbRecurrenceInterval.SelectedIndex = intervalIndex;

                if (_eventData.Recurrence.EndDate.HasValue)
                {
                    chkUseEndDate.Checked = true;
                    datePickerEnd.Value = _eventData.Recurrence.EndDate.Value;
                }

                if (_eventData.Recurrence.MaxOccurrences.HasValue)
                {
                    chkUseMaxOccurrences.Checked = true;
                    nudMaxOccurrences.ValueNumber = _eventData.Recurrence.MaxOccurrences.Value;
                }
            }

            // Tags tab
            if (_eventData.Tags != null)
            {
                foreach (var tag in _eventData.Tags)
                {
                    switch (tag.Name)
                    {
                        case "Work": ckTagWork.Checked = true; break;
                        case "Personal": ckTagPersonal.Checked = true; break;
                        case "Important": ckTagImportant.Checked = true; break;
                        case "Birthday": ckTagBirthday.Checked = true; break;
                        case "Holiday": ckTagHoliday.Checked = true; break;
                        case "Meeting": ckTagMeeting.Checked = true; break;
                    }
                }
            }

            // Reminders tab
            if (_eventData.Reminders != null)
            {
                chkEnableReminders.Checked = _eventData.Reminders.IsEnabled;

                foreach (var reminder in _eventData.Reminders.Reminders)
                {
                    int index = GetReminderIndex(reminder);
                    if (index >= 0 && index < flowLayoutPanel1.Controls.Count)
                    {
                        var checkbox = flowLayoutPanel1.Controls[index] as ReaLTaiizor.Controls.CheckBox;
                        if (checkbox != null) checkbox.Checked = true;
                    }
                }
            }
        }

        private void UpdateTagColorPreview()
        {
            pnlTagColors.Controls.Clear();

            var checkedTags = new List<EventTag>();
            if (ckTagWork.Checked) checkedTags.Add(EventTag.Work);
            if (ckTagPersonal.Checked) checkedTags.Add(EventTag.Personal);
            if (ckTagImportant.Checked) checkedTags.Add(EventTag.Important);
            if (ckTagBirthday.Checked) checkedTags.Add(EventTag.Birthday);
            if (ckTagHoliday.Checked) checkedTags.Add(EventTag.Holiday);
            if (ckTagMeeting.Checked) checkedTags.Add(EventTag.Meeting);

            foreach (var tag in checkedTags)
            {
                var colorBox = new Panel
                {
                    Size = new Size(30, 30),
                    BackColor = tag.Color,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                var label = new Label
                {
                    Text = tag.Name,
                    ForeColor = Color.White,
                    AutoSize = true,
                    Margin = new Padding(5, 10, 5, 5)
                };

                pnlTagColors.Controls.Add(colorBox);
                pnlTagColors.Controls.Add(label);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEventName.Text))
            {
                MessageBox.Show("Event name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update basic info
            _eventData.EventName = txtEventName.Text;

            // Combine hour and minute for TimeFrom
            string timeFromHour = cbTimeFromHour.SelectedItem?.ToString() ?? "12";
            string timeFromMinute = cbTimeFromMinute.SelectedItem?.ToString() ?? "00";
            _eventData.TimeFrom = $"{timeFromHour}:{timeFromMinute}";
            _eventData.FromAMPM = cbFromAMPM.SelectedItem?.ToString() ?? "AM";

            // Combine hour and minute for TimeTo
            string timeToHour = cbTimeToHour.SelectedItem?.ToString() ?? "12";
            string timeToMinute = cbTimeToMinute.SelectedItem?.ToString() ?? "00";
            _eventData.TimeTo = $"{timeToHour}:{timeToMinute}";
            _eventData.ToAMPM = cbToAMPM.SelectedItem?.ToString() ?? "AM";

            _eventData.EventLocation = txtLocation.Text;
            _eventData.Status = cbStatus.SelectedItem?.ToString() ?? "Pending";
            _eventData.Priority = cbPriority.SelectedIndex;
            _eventData.Notes = txtNotes.Text;
            _eventData.EventDate = _selectedDate;

            // Update recurrence
            if (cbRecurrenceType.SelectedIndex > 0)
            {
                _eventData.Recurrence = new RecurrencePattern
                {
                    Type = (RecurrenceType)cbRecurrenceType.SelectedIndex,
                    Interval = (int)cbRecurrenceInterval.SelectedItem,
                    EndDate = chkUseEndDate.Checked ? datePickerEnd.Value : (DateTime?)null,
                    MaxOccurrences = chkUseMaxOccurrences.Checked ? (int)nudMaxOccurrences.ValueNumber : (int?)null
                };
            }
            else
            {
                _eventData.Recurrence = null;
            }

            // Update tags
            _eventData.Tags.Clear();
            if (ckTagWork.Checked) _eventData.Tags.Add(EventTag.Work);
            if (ckTagPersonal.Checked) _eventData.Tags.Add(EventTag.Personal);
            if (ckTagImportant.Checked) _eventData.Tags.Add(EventTag.Important);
            if (ckTagBirthday.Checked) _eventData.Tags.Add(EventTag.Birthday);
            if (ckTagHoliday.Checked) _eventData.Tags.Add(EventTag.Holiday);
            if (ckTagMeeting.Checked) _eventData.Tags.Add(EventTag.Meeting);

            // Update reminders
            _eventData.Reminders.IsEnabled = chkEnableReminders.Checked;
            _eventData.Reminders.Reminders.Clear();

            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                var checkbox = flowLayoutPanel1.Controls[i] as ReaLTaiizor.Controls.CheckBox;
                if (checkbox != null && checkbox.Checked)
                {
                    var reminderTime = GetReminderTimeFromIndex(i);
                    _eventData.Reminders.AddReminder(reminderTime);
                }
            }

            // Save to file
            EventDataService.Instance.SaveEvent(_eventData);
            AuditLogger.LogEvent(_eventData, "Saved");

            // Refresh calendar
            _calendarControl?.RefreshEventData();

            MessageBox.Show("Event saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to delete this event?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(_eventData.Id))
                {
                    EventDataService.Instance.DeleteEvent(_eventData.Id);
                }
                else
                {
                    EventDataService.Instance.DeleteEventByDate(_selectedDate);
                }

                AuditLogger.LogEvent(_eventData, "Deleted");
                _calendarControl?.RefreshEventData();

                MessageBox.Show("Event deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChkUseEndDate_CheckedChanged(object sender)
        {
            datePickerEnd.Enabled = chkUseEndDate.Checked;
        }

        private void ChkUseMaxOccurrences_CheckedChanged(object sender)
        {
            nudMaxOccurrences.Enabled = chkUseMaxOccurrences.Checked;
        }

        private void ChkEnableReminders_CheckedChanged(object sender)
        {
            flowLayoutPanel1.Enabled = chkEnableReminders.Checked;
        }

        private void CkTag_CheckedChanged(object sender)
        {
            UpdateTagColorPreview();
        }

        private int GetReminderIndex(ReminderTime time)
        {
            switch (time)
            {
                case ReminderTime.FiveMinutes: return 0;
                case ReminderTime.FifteenMinutes: return 1;
                case ReminderTime.TwoHours: return 2;
                case ReminderTime.OneHour: return 3;
                case ReminderTime.OneDay: return 4;
                case ReminderTime.TwoDays: return 5;
                case ReminderTime.OneWeek: return 6;
                default: return -1;
            }
        }

        private ReminderTime GetReminderTimeFromIndex(int index)
        {
            switch (index)
            {
                case 0: return ReminderTime.FiveMinutes;
                case 1: return ReminderTime.FifteenMinutes;
                case 2: return ReminderTime.TwoHours;
                case 3: return ReminderTime.OneHour;
                case 4: return ReminderTime.OneDay;
                case 5: return ReminderTime.TwoDays;
                case 6: return ReminderTime.OneWeek;
                default: return ReminderTime.None;
            }
        }

        private void TxtEventName_TextChanged(object sender, EventArgs e)
        {
            if (_isFormattingLocation) return;

            try
            {
                if (!string.IsNullOrEmpty(txtEventName.Text))
                {
                    System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    string formattedText = textInfo.ToTitleCase(txtEventName.Text);

                    if (txtEventName.Text != formattedText)
                    {
                        _isFormattingLocation = true;
                        int cursorPos = txtEventName.SelectionStart;
                        txtEventName.Text = formattedText;
                        txtEventName.SelectionStart = Math.Min(cursorPos, txtEventName.Text.Length);
                        _isFormattingLocation = false;
                    }
                }
            }
            catch { }
        }

        private void TxtLocation_TextChanged(object sender, EventArgs e)
        {
            if (_isFormattingLocation) return;

            try
            {
                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    string formattedText = textInfo.ToTitleCase(txtLocation.Text);
                    
                    if (txtLocation.Text != formattedText)
                    {
                        _isFormattingLocation = true;
                        int cursorPos = txtLocation.SelectionStart;
                        txtLocation.Text = formattedText;
                        txtLocation.SelectionStart = Math.Min(cursorPos, txtLocation.Text.Length);
                        _isFormattingLocation = false;
                    }
                }
            }
            catch { }
        }

        private void TxtNotes_TextChanged(object sender, EventArgs e)
        {
            if (_isFormattingNotes) return;

            try
            {
                if (!string.IsNullOrEmpty(txtNotes.Text))
                {
                    System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                    System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                    string formattedText = textInfo.ToTitleCase(txtNotes.Text);
                    
                    if (txtNotes.Text != formattedText)
                    {
                        _isFormattingNotes = true;
                        int cursorPos = txtNotes.SelectionStart;
                        txtNotes.Text = formattedText;
                        txtNotes.SelectionStart = Math.Min(cursorPos, txtNotes.Text.Length);
                        _isFormattingNotes = false;
                    }
                }
            }
            catch { }
        }

        private void ApplyTitleCase(Control textControl, EventHandler eventHandler)
        {
            string currentText = textControl.Text;
            
            if (string.IsNullOrEmpty(currentText))
                return;

            System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
            System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
            
            string formattedText = textInfo.ToTitleCase(currentText);
            
            if (currentText != formattedText)
            {
                if (textControl is ReaLTaiizor.Controls.HopeTextBox hopeTextBox)
                {
                    int selectionStart = hopeTextBox.SelectionStart;
                    hopeTextBox.TextChanged -= eventHandler;
                    hopeTextBox.Text = formattedText;
                    hopeTextBox.TextChanged += eventHandler;
                    hopeTextBox.SelectionStart = Math.Min(selectionStart, hopeTextBox.Text.Length);
                }
            }
        }

        private void TxtEventName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEventName.Text))
            {
                System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                txtLocation.Text = textInfo.ToTitleCase(txtLocation.Text);
            }
        }

        private void TxtLocation_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLocation.Text))
            {
                System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                txtLocation.Text = textInfo.ToTitleCase(txtLocation.Text);
            }
        }

        private void TxtNotes_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNotes.Text))
            {
                System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                System.Globalization.TextInfo textInfo = cultureInfo.TextInfo;
                txtNotes.Text = textInfo.ToTitleCase(txtNotes.Text);
            }
        }
    }
}
