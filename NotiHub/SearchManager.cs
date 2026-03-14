using NotiHub.Services;
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
    public partial class SearchManager : Form
    {
        private const int DefaultRowHeight = 28;
        // Pagination properties
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;
        private List<EventData> allResults = new List<EventData>();

        public SearchManager()
        {
            InitializeComponent();

            this.Load += (s, e) =>
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
                UpdatePageSize();
            };


            // Handle resize to update page size dynamically
            this.Resize += (s, e) => UpdatePageSize();
            this.SizeChanged += (s, e) => UpdatePageSize();
            dataGridResult.RowTemplate.Height = DefaultRowHeight;

            SetupEventHandlers();
            PopulateTags();
        }

        private void SetupEventHandlers()
        {
            chkUseStartDate.CheckedChanged += (sender) => dtpStartDate.Enabled = chkUseStartDate.Checked;
            chkUseEndDate.CheckedChanged += (sender) => dtpEndDate.Enabled = chkUseEndDate.Checked;
        }

        private void PopulateTags()
        {
            string[] tags = { "Work", "Personal", "Important", "Meeting", "Birthday", "Holiday" };
            foreach (var tag in tags)
            {
                var chkTag = new ReaLTaiizor.Controls.CheckBox
                {
                    Text = tag,
                    Size = new Size(120, 20),
                    ForeColor = Color.Silver,
                    Enable = true,
                    CheckedBackColor = Color.FromArgb(66, 76, 85),
                    CheckedEnabledColor = Color.DarkGoldenrod,
                    Cursor = Cursors.Hand
                };
                flpTags.Controls.Add(chkTag);
            }
        }

        private void UpdatePageSize()
        {
            // Calculate available height for the grid
            int gridHeight = dataGridResult.Height;
            int rowHeight = dataGridResult.RowTemplate.Height;
            int headerHeight = dataGridResult.ColumnHeadersHeight;

            // Calculate rows that can fit (with some margin)
            int availableHeight = gridHeight - headerHeight - 20;
            pageSize = Math.Max(1, availableHeight / rowHeight);

            // Recalculate pagination
            if (allResults.Count > 0)
            {
                totalPages = (int)Math.Ceiling(allResults.Count / (double)pageSize);
                if (currentPage > totalPages)
                {
                    currentPage = totalPages;
                }
                DisplayPage();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {

            try
            {              
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    // Maximize the window without covering the taskbar
                    this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            finally
            {
                
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            DateTime? startDate = chkUseStartDate.Checked ? dtpStartDate.Value.Date : (DateTime?)null;
            DateTime? endDate = chkUseEndDate.Checked ? dtpEndDate.Value.Date : (DateTime?)null;

            List<string> selectedTags = new List<string>();
            foreach (ReaLTaiizor.Controls.CheckBox chk in flpTags.Controls.OfType<ReaLTaiizor.Controls.CheckBox>())
            {
                if (chk.Checked)
                {
                    selectedTags.Add(chk.Text);
                }
            }

            var results = EventDataService.Instance.SearchEvents(searchTerm, startDate, endDate, selectedTags.Count > 0 ? selectedTags : null);

            // Filter by priority if not "All"
            if (cbPriority.SelectedIndex > 0)
            {
                int priority = cbPriority.SelectedIndex - 1;
                results = results.Where(ev => ev.Priority == priority).ToList();
            }

            DisplayResults(results);
        }

        private void DisplayResults(List<EventData> results)
        {
            // Store all results and reset to first page
            allResults = results;
            currentPage = 1;
            totalPages = (int)Math.Ceiling(allResults.Count / (double)pageSize);

            if (allResults.Count == 0)
            {
                dataGridResult.Rows.Clear();
                MessageBox.Show("No events found matching your criteria.", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DisplayPage();
            }
        }

        private void DisplayPage()
        {
            dataGridResult.Rows.Clear();

            if (allResults.Count == 0)
                return;

            // Calculate start and end indices
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, allResults.Count);

            // Add rows for current page
            for (int i = startIndex; i < endIndex; i++)
            {
                var eventData = allResults[i];
                dataGridResult.Rows.Add(
                    eventData.EventName,
                    eventData.EventDate,
                    eventData.EventLocation ?? ""
                );
            }

            // Update pagination info
            UpdatePaginationInfo();
            UpdatePaginationButtons();
        }

        private void UpdatePaginationInfo()
        {
            // Update page info text
            if (lblPageInfo != null)
            {
                lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
            }

            // Update item count
            if (txtCount != null)
            {
                txtCount.Text = allResults.Count.ToString();
            }
        }

        private void UpdatePaginationButtons()
        {
            // Enable/Disable navigation buttons based on current page
            btnStart.Enabled = currentPage > 1;
            btnPreviousPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
            btnLastPage.Enabled = currentPage < totalPages;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            chkUseStartDate.Checked = false;
            chkUseEndDate.Checked = false;
            cbPriority.SelectedIndex = 0;

            foreach (ReaLTaiizor.Controls.CheckBox chk in flpTags.Controls.OfType<ReaLTaiizor.Controls.CheckBox>())
            {
                chk.Checked = false;
            }

            dataGridResult.Rows.Clear();
            allResults.Clear();
            currentPage = 1;
            totalPages = 1;
            UpdatePaginationInfo();
            UpdatePaginationButtons();
        }

        private void btnShowTagPanel_Click(object sender, EventArgs e)
        {
            // Toggle the visibility of flpTags panel
            flpTags.Visible = !flpTags.Visible;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayPage();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            DisplayPage();
        }
    }
}
