using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NotiHub
{
    public partial class AuditTrail : UserControl
    {
        private const int DefaultRowHeight = 28;
        private string currentStatus = "All";
        private DateTime? currentStartDate = null;
        private DateTime? currentEndDate = null;
        private string currentSearchText = "";

        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;

        private List<EventRow> allRows = new List<EventRow>();
        private List<EventRow> filteredRows = new List<EventRow>();

        public AuditTrail()
        {
            InitializeComponent();
            this.Resize += AuditTrail_Resize;
            dataGridAuditTrail.RowTemplate.Height = DefaultRowHeight;
        }

        private string GetDatabasePath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folderPath = Path.Combine(appDataPath, "NotiHub", "SQLite");
            return Path.Combine(folderPath, "NotiHub.json");
        }

        private void AuditTrail_Load(object sender, EventArgs e)
        {
            if (dataGridAuditTrail.Columns.Count == 0)
            {
                dataGridAuditTrail.AutoGenerateColumns = false;
                dataGridAuditTrail.Columns.Add("auID", "ID");
                dataGridAuditTrail.Columns.Add("auEventName", "Event Name");
                dataGridAuditTrail.Columns.Add("auEventDate", "Event Date");
                dataGridAuditTrail.Columns.Add("auEventDuration", "Duration");
                dataGridAuditTrail.Columns.Add("auEventLocation", "Location");
                dataGridAuditTrail.Columns.Add("auStatus", "Status");
                dataGridAuditTrail.Columns.Add("auActionType", "Action Type");
                dataGridAuditTrail.Columns.Add("auTimestamp", "Timestamp");
            }

            InitializeStatusComboBox();

            // Load data from JSON
            LoadAuditData();

            // Default today filter
            datePickerStart.Value = DateTime.Today;
            datePickerEnd.Value = DateTime.Today;
            datePickerStart.Checked = true;
            datePickerEnd.Checked = true;
            currentStartDate = datePickerStart.Value.Date;
            currentEndDate = datePickerEnd.Value.Date;

            UpdatePageSize();
            ApplyAllFilters();
            AutoResizeColumns();
        }


        private void InitializeStatusComboBox()
        {
            comboBoxSelection.Items.Clear();
            comboBoxSelection.Items.AddRange(new string[]
            {
                "All",
                "Pending",
                "Completed",
                "Reschedule",
                "Cancel",
                "Expired"
            });
            comboBoxSelection.SelectedIndex = 0;
        }

        public void LoadAuditData()
        {
            string jsonPath = GetDatabasePath();
            if (!File.Exists(jsonPath))
                return;

            allRows.Clear();

            try
            {
                string json = File.ReadAllText(jsonPath);
                var auditList = JsonConvert.DeserializeObject<List<dynamic>>(json);

                if (auditList == null) return;

                int autoId = 1;

                foreach (var item in auditList)
                {
                    allRows.Add(new EventRow
                    {
                        Id = autoId.ToString(),
                        EventName = item.EventName,
                        EventDate = item.EventDate,
                        EventLocation = item.EventLocation,
                        Status = item.Status,
                        ActionType = item.ActionType,
                        Timestamp = item.Timestamp,
                        Duration = $"{item.TimeFrom} {item.FromAMPM} - {item.TimeTo} {item.ToAMPM}"
                    });

                    autoId++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading audit JSON:\n" + ex.Message);
            }

            currentPage = 1;
            ApplyAllFilters();
        }

        private void ApplyAllFilters()
        {
            filteredRows = allRows.FindAll(row =>
            {
                if (currentStatus != "All" &&
                    !row.Status.Equals(currentStatus, StringComparison.OrdinalIgnoreCase))
                    return false;

                if (DateTime.TryParse(row.EventDate, out DateTime rowDate))
                {
                    if (currentStartDate != null && rowDate < currentStartDate.Value) return false;
                    if (currentEndDate != null && rowDate > currentEndDate.Value) return false;
                }

                if (!string.IsNullOrWhiteSpace(currentSearchText))
                {
                    string src = $"{row.EventName} {row.EventLocation} {row.Status} {row.ActionType}";
                    if (!src.ToLower().Contains(currentSearchText.ToLower())) return false;
                }

                return true;
            });

            currentPage = 1;
            ApplyPagination();
        }

        private void ApplyPagination()
        {
            UpdatePageSize();
            dataGridAuditTrail.Rows.Clear();

            txtCount.Text = filteredRows.Count.ToString();

            if (filteredRows.Count == 0)
            {
                lblPageInfo.Text = "Page 0 of 0";
                return;
            }

            totalPages = (int)Math.Ceiling(filteredRows.Count / (double)pageSize);

            if (currentPage < 1) currentPage = 1;
            if (currentPage > totalPages) currentPage = totalPages;

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, filteredRows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                var row = filteredRows[i];
                dataGridAuditTrail.Rows.Add(
                    row.Id,
                    row.EventName,
                    row.EventDate,
                    row.Duration,
                    row.EventLocation,
                    row.Status,
                    row.ActionType,
                    row.Timestamp
                );
            }

            lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
            btnStart.Enabled = currentPage > 1;
            btnPreviousPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
            btnLastPage.Enabled = currentPage < totalPages;
        }


        private void UpdatePageSize()
        {
            if (dataGridAuditTrail.RowTemplate.Height <= 0) return;

            int height = dataGridAuditTrail.Height - dataGridAuditTrail.ColumnHeadersHeight;
            pageSize = Math.Max(1, height / dataGridAuditTrail.RowTemplate.Height);
        }

        private void AuditTrail_Resize(object sender, EventArgs e)
        {
            ApplyPagination();
            AutoResizeColumns();
        }

        private void AutoResizeColumns()
        {
            dataGridAuditTrail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnClearSearchText_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            currentSearchText = txtSearch.Text.Trim();
            currentPage = 1;
            ApplyAllFilters();
        }

        private void comboBoxSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentStatus = comboBoxSelection.SelectedItem.ToString();
            currentPage = 1;
            ApplyAllFilters();
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            currentStartDate = datePickerStart.Checked ? datePickerStart.Value.Date : (DateTime?)null;
            currentPage = 1;
            ApplyAllFilters();
        }

        private void datePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            currentEndDate = datePickerEnd.Checked ? datePickerEnd.Value.Date : (DateTime?)null;
            currentPage = 1;
            ApplyAllFilters();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            currentSearchText = txtSearch.Text.Trim();
            currentPage = 1;
            ApplyAllFilters();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyPagination();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1) currentPage--;
            ApplyPagination();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages) currentPage++;
            ApplyPagination();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            ApplyPagination();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAuditData();
            ApplyAllFilters();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            if (dataGridAuditTrail.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveDialog.FileName = $"NotiHub_Audit_{DateTime.Now:yyyyMMdd_HHmm}.csv";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();

                    // Header
                    sb.AppendLine("ID,Name,Date,Location,Status,Action Type,Timestamp,Duration");

                    // Rows displayed in DataGridView
                    foreach (DataGridViewRow row in dataGridAuditTrail.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            sb.AppendLine(
                                $"{row.Cells["auID"].Value}," +
                                $"{Escape(row.Cells["auEventName"].Value?.ToString())}," +
                                $"{row.Cells["auEventDate"].Value}," +
                                $"{Escape(row.Cells["auEventLocation"].Value?.ToString())}," +
                                $"{row.Cells["auStatus"].Value}," +
                                $"{row.Cells["auActionType"].Value}," +
                                $"{row.Cells["auTimestamp"].Value}," +
                                $"{Escape(row.Cells["auEventDuration"].Value?.ToString())}"
                            );
                        }
                    }

                    File.WriteAllText(saveDialog.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show("CSV exported successfully!",
                        "Export CSV",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting CSV:\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private string Escape(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            if (text.Contains(",") || text.Contains("\""))
                return $"\"{text.Replace("\"", "\"\"")}\"";

            return text;
        }

        private class EventRow
        {
            public string Id { get; set; }
            public string EventName { get; set; }
            public string EventDate { get; set; }
            public string EventLocation { get; set; }
            public string Status { get; set; }
            public string ActionType { get; set; }
            public string Timestamp { get; set; }
            public string Duration { get; set; }
        }

      
    }
}
