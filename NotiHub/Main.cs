using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class Main : Form
    {
        private bool isStateChanging = false;
        private const int DEFAULT_WIDTH = 1068;
        private const int DEFAULT_HEIGHT = 640;
        private const string FolderName = "NotiHub";
        private const string SubFolderName = "EventCalendar";
        private const string FileName = "eventcalendar.json";
        private List<EventData> eventsList = new List<EventData>();
        private CalendarSchedule calendarControl;
        private NotificationWindow notificationWindow;
        private NotifyIcon notifyIcon;

        public Main()
        {
            InitializeComponent();
            InitializeControls();
            
            // Initialize notification service to enable event reminders
            var notificationService = Services.NotificationService.Instance;
            
            // Initialize system tray icon
            InitializeNotifyIcon();
            
            this.Load += (s, e) =>
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            };
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = this.Icon ?? SystemIcons.Application;
            notifyIcon.Text = "NotiHub";
            notifyIcon.Visible = false;

            // Double-click to restore
            notifyIcon.DoubleClick += (s, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            };

            // Context menu for system tray
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            
            ToolStripMenuItem showItem = new ToolStripMenuItem("Show");
            showItem.Click += (s, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            };
            
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) =>
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                Application.Exit();
            };

            contextMenu.Items.Add(showItem);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(exitItem);

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000, "NotiHub", "Application minimized to system tray", ToolTipIcon.Info);
        }

        public void InitializeControls()
        {
            // Add system event handlers for display and taskbar changes
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

            // Initialize form properties
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT);


            // Setup event handlers
            this.Load += Main_Load;
            this.Resize += Main_Resize;
            this.SizeChanged += Main_SizeChanged;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            BeginInvoke(new Action(UpdateFormPosition));
        }

        private void SystemEvents_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            // Check if the change is related to window metrics (including taskbar)
            if (e.Category == Microsoft.Win32.UserPreferenceCategory.Window)
            {
                BeginInvoke(new Action(UpdateFormPosition));
            }
        }

        private void UpdateFormPosition()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                // Store current state
                FormWindowState currentState = this.WindowState;

                // Temporarily restore to normal to force Windows to recalculate working area
                this.WindowState = FormWindowState.Normal;

                // Update the MaximizedBounds
                Screen screen = Screen.FromControl(this);
                this.MaximizedBounds = screen.WorkingArea;

                // Restore to maximized state
                this.WindowState = currentState;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                PositionForm();
            }
        }

        private void PositionForm()
        {
            if (this.WindowState != FormWindowState.Normal)
                return;

            Screen screen = Screen.FromControl(this);
            Rectangle workingArea = screen.WorkingArea;

            // Calculate center position within the working area
            int left = workingArea.Left + (workingArea.Width - this.Width) / 2;
            int top = workingArea.Top + (workingArea.Height - this.Height) / 2;

            // Ensure the form stays within the working area bounds
            if (left < workingArea.Left) left = workingArea.Left;
            if (top < workingArea.Top) top = workingArea.Top;
            if (left + this.Width > workingArea.Right) left = workingArea.Right - this.Width;
            if (top + this.Height > workingArea.Bottom) top = workingArea.Bottom - this.Height;

            this.Location = new Point(left, top);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (SessionManager.IsLoggedIn)
                labelCurrentSignedAccount.Text = $"ACCOUNT: {SessionManager.FullName}";
            else
                labelCurrentSignedAccount.Text = "NO USER.";

            calendarControl = calendarSchedule1; 

            // Subscribe to its update event
            calendarControl.EventsUpdated += CalendarControl_EventsUpdated;

            // Load events initially
            LoadEvents();

            BeginInvoke(new Action(() =>
            {
                try
                {
                   
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error during deferred load: " + ex.Message);
                }

                // Apply saved window state after we've done the initial load/layout work
                string savedWindowState = Properties.Settings.Default.MainFormWindowState;
                if (!string.IsNullOrEmpty(savedWindowState) &&
                    Enum.TryParse<FormWindowState>(savedWindowState, out FormWindowState state))
                {
                    isStateChanging = true;

                    // First ensure we're in the correct position for the normal state
                    if (state == FormWindowState.Normal)
                    {
                        PositionForm();
                    }
                    else if (state == FormWindowState.Maximized)
                    {
                        // Set MaximizedBounds before maximizing
                        Screen screen = Screen.FromControl(this);
                        this.MaximizedBounds = screen.WorkingArea;
                    }

                    // Then apply the window state
                    this.WindowState = state;
                    isStateChanging = false;
                }
                else
                {
                    PositionForm();
                }
            }));
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (!isStateChanging)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    PositionForm();
                }
            }
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (!isStateChanging)
            {
               
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(1000, "NotiHub", "Application minimized to system tray", ToolTipIcon.Info);
                return;
            }

            base.OnFormClosing(e);

            try
            {
                // Dispose notification service on exit
                Services.NotificationService.Instance.Dispose();
                
                // Unsubscribe from system events to prevent memory leaks
                Microsoft.Win32.SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
                Microsoft.Win32.SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
                
                // Dispose notify icon
                if (notifyIcon != null)
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during form closing: {ex.Message}");
            }
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            try
            {
                // Prevent size-change handlers from reloading during the state transition
                isStateChanging = true;

                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;

                    // Ensure correct position when restoring
                    PositionForm();
                }
                else
                {
                    // Maximize the window without covering the taskbar
                    this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            finally
            {
                // Re-enable handlers 
                isStateChanging = false;              
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public void UpdateUI(Point panelLocation, Color notesColor, Color calendarColor, Color globalSearch, Color auditColor)
        {
            panelIndicator.Location = panelLocation;        
            btnNotes.ForeColor = notesColor;
            btnCalendar.ForeColor = calendarColor;
            btnGlobalSearch.ForeColor = globalSearch;
            btnAudit.ForeColor = auditColor;
        }
        public enum ActiveView
        {
            Notes,
            Calendar,
            GlobalSearch,
            Settings,
            Audit
        }

        public void SetActiveView(ActiveView view)
        {
            switch (view)
            {
                case ActiveView.Notes:
                    UpdateUI(new Point(-1, 132), Color.White, Color.DarkGray, Color.DarkGray, Color.DarkGray);
                    eventNotes1.LoadEvents();
                    eventNotes1.Visible = true;
                    calendarSchedule1.Visible = false;
                    auditTrail1.Visible = false;
                    btnCDate.Visible = false;
                    labelCDate.Visible = false;
                    break;

                case ActiveView.Calendar:
                    UpdateUI(new Point(-1, 187), Color.DarkGray, Color.White, Color.DarkGray, Color.DarkGray);
                    calendarSchedule1.Visible = true;
                    eventNotes1.Visible = false;
                    auditTrail1.Visible = false;
                    btnCDate.Visible = true;
                    labelCDate.Visible = true;
                    break;

                case ActiveView.GlobalSearch:
                    UpdateUI(new Point(-1, 245), Color.White, Color.DarkGray, Color.DarkGray, Color.DarkGray);
                    calendarSchedule1.Visible = false;
                    eventNotes1.Visible = true;
                    auditTrail1.Visible = false;
                    btnCDate.Visible = false;
                    labelCDate.Visible = false;

                    //Show global search modal
                    using (var searchForm = new SearchManager())
                    {
                        searchForm.ShowDialog();
                    }
                  
                    UpdateUI(new Point(-1, 132), Color.White, Color.DarkGray, Color.DarkGray, Color.DarkGray);
                    break;

                case ActiveView.Audit:
                    UpdateUI(new Point(-1, 300), Color.DarkGray, Color.DarkGray, Color.DarkGray, Color.White);
                    calendarSchedule1.Visible = false;
                    eventNotes1.Visible = false;
                    auditTrail1.Visible = true;
                    btnCDate.Visible = false;
                    labelCDate.Visible = false;
                    break;

                case ActiveView.Settings:
                    UpdateUI(new Point(-1, 354), Color.White, Color.DarkGray, Color.DarkGray, Color.DarkGray);
                    calendarSchedule1.Visible = false;
                    eventNotes1.Visible = true;
                    auditTrail1.Visible = false;
                    btnCDate.Visible = false;
                    labelCDate.Visible = false;

                    // Show settings modal
                    using (var settings = new Settings(this))
                    {
                        settings.ShowDialog();
                    }
                    UpdateUI(new Point(-1, 132), Color.White, Color.DarkGray, Color.DarkGray, Color.DarkGray);
                    break;
            }
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            SetActiveView(ActiveView.Notes);
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            SetActiveView(ActiveView.Calendar);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetActiveView(ActiveView.Settings);
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            SetActiveView(ActiveView.Audit);
        }

        private void btnGlobalSearch_Click(object sender, EventArgs e)
        {
            SetActiveView(ActiveView.GlobalSearch);       
        }

        public void LoadEvents()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, FolderName, SubFolderName);
            string filePath = Path.Combine(folderPath, FileName);

            if (File.Exists(filePath))
            {
                try
                {
                    // Read JSON file content
                    string jsonContent = File.ReadAllText(filePath);

                    // Deserialize JSON content to a list of EventData
                    eventsList = JsonConvert.DeserializeObject<List<EventData>>(jsonContent) ?? new List<EventData>();

                    // Debug log all events with their dates
                    Console.WriteLine("All loaded events:");
                    foreach (var evt in eventsList)
                    {
                        Console.WriteLine($"Event: {evt.EventName}, Date: {evt.EventDate}");
                    }

                    // Get upcoming events (within next 7 days)
                    var upcomingEvents = GetUpcomingEvents();

                    // Update button to show indicator if there are upcoming events
                    if (upcomingEvents.Count > 0)
                    {
                        btnNotification.Text = $"{upcomingEvents.Count}";
                        btnNotification.ForeColor = Color.FromArgb(255, 128, 128); // Red indicator
                    }
                    else
                    {
                        btnNotification.Text = "";
                        btnNotification.ForeColor = Color.Gray;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading events: {ex.Message}");
                    btnNotification.Text = "";
                }
            }
            else
            {
                Console.WriteLine("Event calendar file not found.");
                btnNotification.Text = "";
            }
        }

        private List<EventData> GetUpcomingEvents()
        {
            var upcomingEvents = new List<EventData>();
            DateTime now = DateTime.Now;
            DateTime sevenDaysFromNow = now.AddDays(7);

            foreach (var evt in eventsList)
            {
                if (TryParseEventDate(evt.EventDate, out DateTime eventDate))
                {
                    // Check if event is within the next 7 days
                    if (eventDate >= now && eventDate <= sevenDaysFromNow)
                    {
                        upcomingEvents.Add(evt);
                    }
                }
            }

            // Sort by date (earliest first)
            upcomingEvents = upcomingEvents.OrderBy(e =>
            {
                TryParseEventDate(e.EventDate, out DateTime date);
                return date;
            }).ToList();

            return upcomingEvents;
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
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result);
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            if (notificationWindow == null || notificationWindow.IsDisposed)
            {
                notificationWindow = new NotificationWindow(this);
                var upcomingEvents = GetUpcomingEvents();
                notificationWindow.LoadUpcomingEvents(upcomingEvents);
                notificationWindow.Show();
            }
            else
            {
                notificationWindow.Close();
                notificationWindow = null;
            }
        }

        private void CalendarControl_EventsUpdated()
        {
            // Refresh the events count when the calendar updates
            LoadEvents();
        }

        public void ShowEventOnCalendar(string eventDate)
        {
            try
            {
                // Parse the date string (expected format: MM/DD/YYYY or similar)
                if (DateTime.TryParse(eventDate, out DateTime parsedDate))
                {
                    // If calendar control exists, navigate to the event's date
                    if (calendarControl != null)
                    {
                        // Call the calendar's showDays method to display the correct month/year
                        calendarControl.showDays(parsedDate.Month, parsedDate.Year);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error showing event on calendar: {ex.Message}");
            }
        }

        private void btnGithub_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/seizue/NotiHub/issues",
                UseShellExecute = true
            });
        }    
       
    }
}
