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
        public Main()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

            // Initial load of event count
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
                // DisplayCurrentPage();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            base.OnFormClosing(e);

            try
            {
                // Unsubscribe from system events to prevent memory leaks
                Microsoft.Win32.SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
                Microsoft.Win32.SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
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
                // Re-enable handlers and refresh using the active date filter/search
                isStateChanging = false;
               
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public void UpdateUI(Point panelLocation, Color notesColor, Color calendarColor)
        {
            panelIndicator.Location = panelLocation;        
            btnNotes.ForeColor = notesColor;
            btnCalendar.ForeColor = calendarColor;
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            UpdateUI(new Point(-1, 132), Color.White, Color.DarkGray);
            calendarSchedule1.Visible = false;
            eventNotes1.Visible = true;
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            UpdateUI(new Point(-1, 187), Color.DarkGray, Color.White);
            calendarSchedule1.Visible = true;
            eventNotes1.Visible = false;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            UpdateUI(new Point(-1, 132), Color.White, Color.DarkGray);
            Settings settings = new Settings(this);
            settings.ShowDialog();
            calendarSchedule1.Visible = false;
            eventNotes1.Visible = true;
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

                    // Update button text with total event count
                    btnNotification.Text = $"{eventsList.Count} Events";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading events: {ex.Message}");
                    btnNotification.Text = "0 Events";
                }
            }
            else
            {
                Console.WriteLine("Event calendar file not found.");
                btnNotification.Text = "0 Events";
            }
        }

        private void btnNotification_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"There are {eventsList.Count} total events.");
        }
    }
}
