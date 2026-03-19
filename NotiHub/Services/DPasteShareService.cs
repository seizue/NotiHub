using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NotiHub.Services
{
    public class DPasteShareService
    {
        // Using dpaste.com - Completely free, no authentication required
        private const string DPasteApiUrl = "https://dpaste.com/api/v2/";

        public async Task<string> ShareNoteAsync(object noteData, string fileName = "note.json")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Convert to EventData if it's an anonymous object
                    string formattedContent = FormatNoteAsText(noteData);

                    // Prepare form data for dpaste
                    var formData = new Dictionary<string, string>
                    {
                        { "content", formattedContent },
                        { "syntax", "text" }, // Changed from json to text
                        { "expiry_days", "365" } // 1 year expiry (max allowed)
                    };

                    var content = new FormUrlEncodedContent(formData);

                    // Post to dpaste API
                    var response = await client.PostAsync(DPasteApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // dpaste returns the URL in the response body
                        string pasteUrl = await response.Content.ReadAsStringAsync();
                        pasteUrl = pasteUrl.Trim();
                        
                        // Add .txt to get raw text view
                        if (!pasteUrl.EndsWith(".txt"))
                        {
                            pasteUrl += ".txt";
                        }
                        
                        return pasteUrl;
                    }
                    else
                    {
                        string errorBody = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Failed to create share link: {response.StatusCode} - {errorBody}");
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Network error: {httpEx.Message}. Please check your internet connection.", httpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error sharing note: {ex.Message}", ex);
            }
        }

        private string FormatNoteAsText(object noteData)
        {
            try
            {
                // Try to deserialize as EventData
                string json = JsonConvert.SerializeObject(noteData);
                dynamic data = JsonConvert.DeserializeObject(json);

                var sb = new StringBuilder();
                
                // Header
                sb.AppendLine("================================================================");
                sb.AppendLine("                SHARED NOTE FROM NOTIHUB                        ");
                sb.AppendLine("================================================================");
                sb.AppendLine();

                // Event Details
                sb.AppendLine("EVENT DETAILS");
                sb.AppendLine("----------------------------------------------------------------");
                
                if (data.EventName != null)
                    sb.AppendLine($"Event Name    : {data.EventName}");
                
                if (data.EventDate != null)
                    sb.AppendLine($"Date          : {data.EventDate}");
                
                if (data.TimeFrom != null && data.FromAMPM != null && data.TimeTo != null && data.ToAMPM != null)
                    sb.AppendLine($"Time          : {data.TimeFrom} {data.FromAMPM} - {data.TimeTo} {data.ToAMPM}");
                
                if (data.EventLocation != null)
                    sb.AppendLine($"Location      : {data.EventLocation}");
                
                if (data.Status != null)
                    sb.AppendLine($"Status        : {data.Status}");

                if (data.Priority != null && data.Priority > 0)
                {
                    string priorityText = GetPriorityText((int)data.Priority);
                    sb.AppendLine($"Priority      : {priorityText}");
                }

                sb.AppendLine();

                // Notes section
                if (data.Notes != null && !string.IsNullOrWhiteSpace(data.Notes.ToString()))
                {
                    sb.AppendLine("NOTES");
                    sb.AppendLine("----------------------------------------------------------------");
                    sb.AppendLine(data.Notes.ToString());
                    sb.AppendLine();
                }

                // Tags section
                if (data.Tags != null && data.Tags.Count > 0)
                {
                    sb.AppendLine("TAGS");
                    sb.AppendLine("----------------------------------------------------------------");
                    foreach (var tag in data.Tags)
                    {
                        sb.AppendLine($"  - {tag}");
                    }
                    sb.AppendLine();
                }

                // Reminders section
                if (data.Reminders != null && data.Reminders.IsEnabled == true && 
                    data.Reminders.Reminders != null && data.Reminders.Reminders.Count > 0)
                {
                    sb.AppendLine("REMINDERS");
                    sb.AppendLine("----------------------------------------------------------------");
                    foreach (var reminder in data.Reminders.Reminders)
                    {
                        sb.AppendLine($"  - {reminder}");
                    }
                    sb.AppendLine();
                }

                // Footer
                sb.AppendLine("----------------------------------------------------------------");
                sb.AppendLine($"Shared on: {DateTime.Now:MMMM dd, yyyy 'at' hh:mm tt}");
                sb.AppendLine("Generated by NotiHub - Event & Schedule Manager");
                sb.AppendLine("================================================================");

                return sb.ToString();
            }
            catch
            {
                // Fallback to JSON if formatting fails
                return JsonConvert.SerializeObject(new
                {
                    app = "NotiHub",
                    sharedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    note = noteData
                }, Formatting.Indented);
            }
        }

        private string GetStatusEmoji(string status)
        {
            // No longer used, kept for compatibility
            return "";
        }

        private string GetPriorityText(int priority)
        {
            switch (priority)
            {
                case 1:
                    return "High";
                case 2:
                    return "Urgent";
                default:
                    return "Normal";
            }
        }
    }
}
