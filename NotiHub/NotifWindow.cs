using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class NotifWindow : Form
    {
        private bool isUrgent;
        private EventData eventData;
        private DateTime snoozeUntilTime;

        public NotifWindow(string title, string message, bool isUrgent = false, EventData eventData = null)
        {
            this.isUrgent = isUrgent;
            this.eventData = eventData;
            InitializeComponent();
            SetupWindow(title, message, isUrgent);
        }

        private void SetupWindow(string title, string message, bool isUrgent)
        {
            this.Text = "NotiHub - Event Reminder";
            this.TopMost = true;
            this.ShowInTaskbar = true;

            // Update header color based on urgency
            headerPanel.BackColor = isUrgent ? Color.FromArgb(231, 76, 60) : Color.FromArgb(52, 152, 219);

            // Update header label based on urgency
            headerLabel.Text = isUrgent ? "⚠ URGENT REMINDER" : "🔔 Event Reminder";

            // Create icon based on urgency
            using (Bitmap bmp = new Bitmap(60, 60))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Color iconColor = isUrgent ? Color.FromArgb(231, 76, 60) : Color.FromArgb(52, 152, 219);
                using (SolidBrush brush = new SolidBrush(iconColor))
                {
                    g.FillEllipse(brush, 5, 5, 50, 50);
                }
                using (Font iconFont = new Font("Segoe UI", 24, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    string iconText = isUrgent ? "!" : "i";
                    SizeF textSize = g.MeasureString(iconText, iconFont);
                    g.DrawString(iconText, iconFont, textBrush,
                        (60 - textSize.Width) / 2, (60 - textSize.Height) / 2);
                }
                iconBox.Image = (Bitmap)bmp.Clone();
            }

            // Set title and message
            lblTitle.Text = title;
            lblMessage.Text = message;
            lblTime.Text = $"{DateTime.Now:hh:mm:ss tt}";

            // Update timer tick event
            updateTimer.Tick += (s, e) => lblTime.Text = $"{DateTime.Now:hh:mm:ss tt}";
            updateTimer.Start();

            // Setup button click events
            btnSnooze.Click += btnSnooze_Click;
            btnViewEvent.Click += btnViewEvent_Click;
            btnClose.Click += (s, e) => this.Close();

            // Play alert sound for urgent notifications
            if (isUrgent)
            {
                System.Media.SystemSounds.Exclamation.Play();
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
            }

            // Flash taskbar to get attention
            FlashWindow();

            // Add fade-in animation (cap at 0.95 to respect designer opacity setting)
            this.Opacity = 0;
            Timer fadeTimer = new Timer { Interval = 20 };
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 0.90)
                    this.Opacity += 0.05;
                else
                {
                    this.Opacity = 0.90;
                    fadeTimer.Stop();
                }
            };
            fadeTimer.Start();
        }

        private void btnSnooze_Click(object sender, EventArgs e)
        {
            // Snooze for 5 minutes
            snoozeUntilTime = DateTime.Now.AddMinutes(5);
            
            if (eventData != null)
            {
                // Register snooze with NotificationService
                Services.NotificationService.Instance.SnoozeEvent(eventData, snoozeUntilTime);
                
                MessageBox.Show($"SNOOZED: {eventData.EventName}\nYou will be reminded again in 5 minutes.",
                    "Snoozed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("This event will remind you again in 5 minutes.",
                    "Snoozed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            this.Close();
        }

        private void btnViewEvent_Click(object sender, EventArgs e)
        {
            if (eventData != null)
            {
                // Open the main NotiHub window to the event
                Main mainWindow = Application.OpenForms.OfType<Main>().FirstOrDefault();
                
                if (mainWindow != null)
                {
                    // Restore/Maximize the main window
                    if (mainWindow.WindowState == FormWindowState.Minimized)
                    {
                        mainWindow.WindowState = FormWindowState.Normal;
                    }
                    
                    mainWindow.BringToFront();
                    mainWindow.Focus();
                    
                    // Show the calendar view
                    mainWindow.SetActiveView(Main.ActiveView.Calendar);
                    
                    // Navigate to the event's date
                    if (!string.IsNullOrEmpty(eventData.EventDate))
                    {
                        // This will be handled by the Main form's calendar control
                        mainWindow.ShowEventOnCalendar(eventData.EventDate);
                    }
                    
                }
                else
                {
                    MessageBox.Show("NotiHub main window not found. Please open NotiHub.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Event details not available.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.Close();
        }

        private void FlashWindow()
        {
            // Flash the taskbar icon to get user's attention
            try
            {
                NativeMethods.FlashWindow(this.Handle, true);
            }
            catch
            {
                // Ignore if flash fails
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (updateTimer != null)
            {
                updateTimer.Stop();
                updateTimer.Dispose();
            }

            // Fade out animation
            Timer fadeTimer = new Timer { Interval = 20 };
            fadeTimer.Tick += (s, ev) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                }
            };

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                fadeTimer.Tick += (s, ev) =>
                {
                    if (this.Opacity <= 0)
                    {
                        e.Cancel = false;
                        base.OnFormClosing(e);
                        this.Dispose();
                    }
                };
                fadeTimer.Start();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Position in bottom-right after form is fully sized
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - this.Width - 20, workingArea.Bottom - this.Height - 20);
            this.Activate();
            this.BringToFront();
            this.Focus();
        }

        // Native methods for window flashing
        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
        }
    }
}
