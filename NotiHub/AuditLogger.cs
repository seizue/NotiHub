using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(data.Status) ? "Pending" : data.Status);
                    cmd.Parameters.AddWithValue("@ActionType", actionType);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }

            // Save to JSON file
            SaveToJson(data, actionType);
        }


        private static void SaveToJson(EventData data, string actionType)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, "NotiHub", "SQLite");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "NotiHub.json");

            List<object> auditList;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                auditList = JsonConvert.DeserializeObject<List<object>>(json) ?? new List<object>();
            }
            else
            {
                auditList = new List<object>();
            }

            // Create audit log entry
            var auditEntry = new
            {
                data.EventName,
                data.EventDate,
                data.TimeFrom,
                data.FromAMPM,
                data.TimeTo,
                data.ToAMPM,
                data.EventLocation,
                Status = string.IsNullOrWhiteSpace(data.Status) ? "Pending" : data.Status,
                ActionType = actionType,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            auditList.Add(auditEntry);

            File.WriteAllText(filePath, JsonConvert.SerializeObject(auditList, Formatting.Indented));
        }

    }
}
