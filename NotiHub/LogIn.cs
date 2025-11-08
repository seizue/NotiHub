using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class LogIn : Form
    {
        private const string FolderName = "NotiHub";
        private const string SubFolderName = "Credential";
        private const string FileName = "userRegistrations.json";

        public LogIn()
        {
            InitializeComponent();

            LoadRememberedCredentials();

            // Ensure default Admin exists
            EnsureDefaultAdmin();

            //KeyDown event to handle Enter key press
            txtboxUsername.KeyDown += Txtbox_KeyDown;
            txtboxPass.KeyDown += Txtbox_KeyDown;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void EnsureDefaultAdmin()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, FolderName, SubFolderName);
            string filePath = Path.Combine(folderPath, FileName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            List<dynamic> registrations;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                registrations = JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new List<dynamic>();
            }
            else
            {
                registrations = new List<dynamic>();
            }

            // Check if an Admin user already exists
            bool adminExists = registrations.Any(r => r.Username == "Admin");
            if (!adminExists)
            {
                // Add default admin
                var adminUser = new
                {
                    Username = "Admin",
                    Password = "1727",
                    FullName = "ADMINISTRATOR",
                    AccessRole = "Admin",
                    RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd")
                };

                registrations.Add(adminUser);

                string json = JsonConvert.SerializeObject(registrations, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
        }

        private async void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtboxUsername.Text;
            string password = txtboxPass.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var user = ValidateCredentials(username, password);
            if (user != null)
            {
                SessionManager.Username = user.Username;
                SessionManager.FullName = user.FullName != null ? (string)user.FullName : (string)user.Username;
                SessionManager.Role = user.AccessRole;
                SessionManager.Password = user.Password;

                if (checkBoxRemember.Checked)
                {
                    SaveRememberedCredentials(username, password);
                }
                else
                {
                    ClearRememberedCredentials();
                }

                // Hide the current form
                this.Hide();

                // Instantiate the MainForm but keep it hidden
                Main mainForm = new Main();
                mainForm.InitializeControls();

                // Hide the MainForm
                mainForm.Opacity = 0;
                mainForm.Visible = false;

                // Show the MainForm 
                mainForm.Show();
                await Task.Delay(1000);
                mainForm.Opacity = 100;
                mainForm.Visible = true;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private dynamic ValidateCredentials(string username, string password)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataPath, FolderName, SubFolderName, FileName);

            if (!File.Exists(filePath))
                return null;

            try
            {
                string json = File.ReadAllText(filePath);
                var registrations = JsonConvert.DeserializeObject<List<dynamic>>(json);

                return registrations.FirstOrDefault(r => r.Username == username && r.Password == password);
            }
            catch
            {
                return null;
            }
        }

        private void checkBoxRemember_ClientSizeChanged(object sender, EventArgs e)
        {
            if (!checkBoxRemember.Checked)
            {
                ClearRememberedCredentials();
            }
        }

        private void SaveRememberedCredentials(string username, string password)
        {
            Properties.Settings.Default.RememberedUsername = username;
            Properties.Settings.Default.RememberedPassword = password;
            Properties.Settings.Default.RememberLogin = true;
            Properties.Settings.Default.Save();
        }

        private void ClearRememberedCredentials()
        {
            Properties.Settings.Default.RememberedUsername = string.Empty;
            Properties.Settings.Default.RememberedPassword = string.Empty;
            Properties.Settings.Default.RememberLogin = false;
            Properties.Settings.Default.Save();
        }

        private void LoadRememberedCredentials()
        {
            if (Properties.Settings.Default.RememberLogin)
            {
                txtboxUsername.Text = Properties.Settings.Default.RememberedUsername;
                txtboxPass.Text = Properties.Settings.Default.RememberedPassword;
                checkBoxRemember.Checked = true;
            }
        }

        private void Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                btnSignIn_Click(sender, e);
            }
        }
    }
}
