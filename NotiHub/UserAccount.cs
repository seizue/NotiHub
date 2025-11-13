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
    public partial class UserAccount : Form
    {
        public UserAccount()
        {
            InitializeComponent();
            this.Load += UserAccount_Load;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            // Ask OLD password
            string oldPassInput = Prompt.ShowDialog("Enter current password:", "Confirm Password", true);
            if (string.IsNullOrEmpty(oldPassInput)) return;

            if (oldPassInput != SessionManager.Password)
            {
                MessageBox.Show("Incorrect old password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // New password from textbox (already typed)
            string newPass = txtNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("New password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update memory only
            SessionManager.Password = newPass;

            // Notify success & ask restart
            var result = MessageBox.Show(
                "Password changed successfully.\n\nYou must restart the app to apply this change.\n\nRestart now?",
                "Password Changed",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void UserAccount_Load(object sender, EventArgs e)
        {
            txtUsername.Text = SessionManager.Username ?? "(empty)";
            txtFullName.Text = SessionManager.FullName ?? "(empty)";
            txtRole.Text = SessionManager.Role ?? "(empty)";
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption, bool hideInput = false)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 180,
                    Text = caption,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label label = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
                TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
                if (hideInput) textBox.UseSystemPasswordChar = true;

                Button ok = new Button() { Text = "OK", Left = 190, Width = 80, Top = 85, DialogResult = DialogResult.OK };
                Button cancel = new Button() { Text = "Cancel", Left = 280, Width = 80, Top = 85, DialogResult = DialogResult.Cancel };

                prompt.Controls.Add(label);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(ok);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = ok;
                prompt.CancelButton = cancel;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }
        }
    }
}
