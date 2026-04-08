using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotiHub
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.EnableSignIn)
            {
                Application.Run(new LogIn());
            }
            else
            {
                // Skip login — set a default session and go straight to Main
                SessionManager.Username = "Admin";
                SessionManager.FullName = "ADMINISTRATOR";
                SessionManager.Role = "Admin";
                Main mainForm = new Main();
                mainForm.Opacity = 0;
                mainForm.InitializeControls();
                mainForm.Shown += async (s, ev) =>
                {
                    for (double op = 0; op <= 1.0; op += 0.05)
                    {
                        mainForm.Opacity = op;
                        await System.Threading.Tasks.Task.Delay(15);
                    }
                    mainForm.Opacity = 1.0;
                };
                Application.Run(mainForm);
            }
        }
    }
}
