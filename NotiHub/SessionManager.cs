using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiHub
{
    public static class SessionManager
    {
        // Basic user info
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static string Password { get; set; }

        // Useful flags
        public static bool IsLoggedIn => !string.IsNullOrEmpty(FullName);

        // Helper method to clear session when user logs out
        public static void Clear()
        {
            Username = null;
            FullName = null;
            Role = null;
            Password = null;
        }
    }
}
