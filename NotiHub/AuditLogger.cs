using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace NotiHub
{
    public static class AuditLogger
    {
        private static string GetDatabasePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, "NotiHub", "SQLite");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return Path.Combine(folderPath, "NotiHub_Audit.db");
        }

        private static SQLiteConnection GetConnection()
        {
            string dbPath = GetDatabasePath();
            bool newDb = !File.Exists(dbPath);

            var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            conn.Open();

            if (newDb)
            {
                CreateTables(conn);
            }

            return conn;
        }

        private static void CreateTables(SQLiteConnection conn)
        {
            string createTableSql = @"
                CREATE TABLE IF NOT EXISTS EventAudit (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EventName TEXT,
                    EventDate TEXT,
                    TimeFrom TEXT,
                    FromAMPM TEXT,
                    TimeTo TEXT,
                    ToAMPM TEXT,
                    EventLocation TEXT,
                    Status TEXT,
                    ActionType TEXT,
                    Timestamp TEXT
                );
            ";
            using (var cmd = new SQLiteCommand(createTableSql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void LogEvent(EventData data, string actionType)
        {
            using (var conn = GetConnection())
            {
                string insertSql = @"
                    INSERT INTO EventAudit 
                    (EventName, EventDate, TimeFrom, FromAMPM, TimeTo, ToAMPM, EventLocation, Status, ActionType, Timestamp)
                    VALUES (@EventName, @EventDate, @TimeFrom, @FromAMPM, @TimeTo, @ToAMPM, @EventLocation, @Status, @ActionType, @Timestamp);
                ";

                using (var cmd = new SQLiteCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@EventName", data.EventName);
                    cmd.Parameters.AddWithValue("@EventDate", data.EventDate);
                    cmd.Parameters.AddWithValue("@TimeFrom", data.TimeFrom);
                    cmd.Parameters.AddWithValue("@FromAMPM", data.FromAMPM);
                    cmd.Parameters.AddWithValue("@TimeTo", data.TimeTo);
                    cmd.Parameters.AddWithValue("@ToAMPM", data.ToAMPM);
                    cmd.Parameters.AddWithValue("@EventLocation", data.EventLocation);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(data.Status) ? "Pending" : data.Status); // <-- Save status
                    cmd.Parameters.AddWithValue("@ActionType", actionType);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
