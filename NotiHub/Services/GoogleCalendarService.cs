using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace NotiHub.Services
{
    public class GoogleCalendarService
    {
        private static GoogleCalendarService _instance;
        private string _accessToken;
        private string _refreshToken;
        private DateTime _tokenExpiry;
        private const string CALENDAR_API_BASE = "https://www.googleapis.com/calendar/v3";

        public static GoogleCalendarService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GoogleCalendarService();
                }
                return _instance;
            }
        }

        private GoogleCalendarService()
        {
            LoadTokens();
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_accessToken) && _tokenExpiry > DateTime.Now;

        /// <summary>
        /// Authenticate with Google Calendar using OAuth 2.0
        /// </summary>
        public async Task<bool> AuthenticateAsync(string clientId, string clientSecret, string authCode)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var tokenRequest = new Dictionary<string, string>
                    {
                        { "code", authCode },
                        { "client_id", clientId },
                        { "client_secret", clientSecret },
                        { "redirect_uri", "urn:ietf:wg:oauth:2.0:oob" },
                        { "grant_type", "authorization_code" }
                    };

                    var content = new FormUrlEncodedContent(tokenRequest);
                    var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                        _accessToken = tokenResponse.access_token;
                        _refreshToken = tokenResponse.refresh_token;
                        _tokenExpiry = DateTime.Now.AddSeconds(tokenResponse.expires_in);

                        SaveTokens();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Authentication error: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Refresh the access token using refresh token
        /// </summary>
        private async Task<bool> RefreshTokenAsync(string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(_refreshToken))
                return false;

            try
            {
                using (var client = new HttpClient())
                {
                    var tokenRequest = new Dictionary<string, string>
                    {
                        { "refresh_token", _refreshToken },
                        { "client_id", clientId },
                        { "client_secret", clientSecret },
                        { "grant_type", "refresh_token" }
                    };

                    var content = new FormUrlEncodedContent(tokenRequest);
                    var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                        _accessToken = tokenResponse.access_token;
                        _tokenExpiry = DateTime.Now.AddSeconds(tokenResponse.expires_in);

                        SaveTokens();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Token refresh error: {ex.Message}");
            }

            return false;
        }

        /// <summary>
        /// Sync events from Google Calendar to NotiHub
        /// </summary>
        public async Task<List<EventData>> SyncFromGoogleCalendarAsync(DateTime startDate, DateTime endDate)
        {
            if (!IsAuthenticated)
                return new List<EventData>();

            var events = new List<EventData>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                    var timeMin = startDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    var timeMax = endDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    var url = $"{CALENDAR_API_BASE}/calendars/primary/events?timeMin={timeMin}&timeMax={timeMax}&singleEvents=true&orderBy=startTime";

                    var response = await client.GetAsync(url);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var calendarResponse = JsonConvert.DeserializeObject<GoogleCalendarResponse>(responseString);

                        foreach (var item in calendarResponse.items)
                        {
                            var eventData = ConvertGoogleEventToNotiHub(item);
                            if (eventData != null)
                            {
                                events.Add(eventData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Sync error: {ex.Message}");
            }

            return events;
        }

        /// <summary>
        /// Export NotiHub event to Google Calendar
        /// </summary>
        public async Task<bool> ExportToGoogleCalendarAsync(EventData eventData)
        {
            if (!IsAuthenticated)
                return false;

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                    var googleEvent = ConvertNotiHubEventToGoogle(eventData);
                    var json = JsonConvert.SerializeObject(googleEvent);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = $"{CALENDAR_API_BASE}/calendars/primary/events";
                    var response = await client.PostAsync(url, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Export error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Two-way sync: Import from Google and Export to Google
        /// </summary>
        public async Task<SyncResult> PerformTwoWaySyncAsync(DateTime startDate, DateTime endDate)
        {
            var result = new SyncResult();

            try
            {
                // Import from Google Calendar
                var googleEvents = await SyncFromGoogleCalendarAsync(startDate, endDate);
                var localEvents = EventDataService.Instance.LoadAllEvents();

                // Find new events from Google
                foreach (var googleEvent in googleEvents)
                {
                    var exists = localEvents.Any(e => e.EventName == googleEvent.EventName && e.EventDate == googleEvent.EventDate);
                    if (!exists)
                    {
                        EventDataService.Instance.SaveEvent(googleEvent);
                        result.ImportedCount++;
                    }
                }

                // Export local events to Google (optional - can be enabled)
                // foreach (var localEvent in localEvents)
                // {
                //     await ExportToGoogleCalendarAsync(localEvent);
                //     result.ExportedCount++;
                // }

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private EventData ConvertGoogleEventToNotiHub(GoogleCalendarEvent googleEvent)
        {
            try
            {
                DateTime startDateTime;
                if (googleEvent.start.dateTime != null)
                {
                    startDateTime = DateTime.Parse(googleEvent.start.dateTime);
                }
                else if (googleEvent.start.date != null)
                {
                    startDateTime = DateTime.Parse(googleEvent.start.date);
                }
                else
                {
                    return null;
                }

                var eventData = new EventData
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = googleEvent.summary ?? "Untitled Event",
                    EventDate = startDateTime.ToString("M/d/yyyy"),
                    TimeFrom = startDateTime.ToString("h:mm"),
                    FromAMPM = startDateTime.ToString("tt"),
                    EventLocation = googleEvent.location ?? "",
                    Notes = googleEvent.description ?? "",
                    Status = "Pending"
                };

                // Set end time if available
                if (googleEvent.end?.dateTime != null)
                {
                    var endDateTime = DateTime.Parse(googleEvent.end.dateTime);
                    eventData.TimeTo = endDateTime.ToString("h:mm");
                    eventData.ToAMPM = endDateTime.ToString("tt");
                }

                return eventData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Conversion error: {ex.Message}");
                return null;
            }
        }

        private object ConvertNotiHubEventToGoogle(EventData eventData)
        {
            DateTime eventDate = DateTime.Parse(eventData.EventDate);
            
            // Parse time
            var timeParts = eventData.TimeFrom.Split(':');
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            if (eventData.FromAMPM == "PM" && hour != 12)
                hour += 12;
            else if (eventData.FromAMPM == "AM" && hour == 12)
                hour = 0;

            var startDateTime = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, hour, minute, 0);
            
            // Calculate end time (default 1 hour if not specified)
            DateTime endDateTime = startDateTime.AddHours(1);
            if (!string.IsNullOrEmpty(eventData.TimeTo))
            {
                var endTimeParts = eventData.TimeTo.Split(':');
                int endHour = int.Parse(endTimeParts[0]);
                int endMinute = int.Parse(endTimeParts[1]);

                if (eventData.ToAMPM == "PM" && endHour != 12)
                    endHour += 12;
                else if (eventData.ToAMPM == "AM" && endHour == 12)
                    endHour = 0;

                endDateTime = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, endHour, endMinute, 0);
            }

            return new
            {
                summary = eventData.EventName,
                location = eventData.EventLocation,
                description = eventData.Notes,
                start = new
                {
                    dateTime = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    timeZone = TimeZoneInfo.Local.Id
                },
                end = new
                {
                    dateTime = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    timeZone = TimeZoneInfo.Local.Id
                }
            };
        }

        private void SaveTokens()
        {
            try
            {
                var tokens = new
                {
                    AccessToken = _accessToken,
                    RefreshToken = _refreshToken,
                    TokenExpiry = _tokenExpiry
                };

                var json = JsonConvert.SerializeObject(tokens);
                var filePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "NotiHub", "google_tokens.json");

                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));
                System.IO.File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save tokens error: {ex.Message}");
            }
        }

        private void LoadTokens()
        {
            try
            {
                var filePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "NotiHub", "google_tokens.json");

                if (System.IO.File.Exists(filePath))
                {
                    var json = System.IO.File.ReadAllText(filePath);
                    var tokens = JsonConvert.DeserializeAnonymousType(json, new
                    {
                        AccessToken = "",
                        RefreshToken = "",
                        TokenExpiry = DateTime.MinValue
                    });

                    _accessToken = tokens.AccessToken;
                    _refreshToken = tokens.RefreshToken;
                    _tokenExpiry = tokens.TokenExpiry;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Load tokens error: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            _accessToken = null;
            _refreshToken = null;
            _tokenExpiry = DateTime.MinValue;

            try
            {
                var filePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "NotiHub", "google_tokens.json");

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch { }
        }

        // Helper classes for JSON deserialization
        private class TokenResponse
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public int expires_in { get; set; }
        }

        private class GoogleCalendarResponse
        {
            public List<GoogleCalendarEvent> items { get; set; }
        }

        private class GoogleCalendarEvent
        {
            public string summary { get; set; }
            public string description { get; set; }
            public string location { get; set; }
            public EventDateTime start { get; set; }
            public EventDateTime end { get; set; }
        }

        private class EventDateTime
        {
            public string dateTime { get; set; }
            public string date { get; set; }
        }

        public class SyncResult
        {
            public bool Success { get; set; }
            public int ImportedCount { get; set; }
            public int ExportedCount { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
