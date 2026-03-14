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
    public partial class CalendarImportExport : Form
    {
        public CalendarImportExport()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "iCalendar files (*.ics)|*.ics|All files (*.*)|*.*";
                openFileDialog.Title = "Select Calendar File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    lblStatus.Text = "File selected. Click 'Import' to import events.";
                    lblStatus.ForeColor = Color.LightBlue;
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a file first.", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnImport.Enabled = false;
                btnImport.Text = "Importing...";
                lblStatus.Text = "Importing events...";
                lblStatus.ForeColor = Color.Yellow;

                var events = ICalendarService.Instance.ImportFromICS(txtFilePath.Text);

                if (events.Count > 0)
                {
                    // Save imported events
                    int importedCount = 0;
                    var existingEvents = EventDataService.Instance.LoadAllEvents();

                    foreach (var evt in events)
                    {
                        // Check for duplicates
                        bool exists = existingEvents.Exists(existing =>
                            existing.EventName == evt.EventName &&
                            existing.EventDate == evt.EventDate &&
                            existing.TimeFrom == evt.TimeFrom);

                        if (!exists)
                        {
                            EventDataService.Instance.SaveEvent(evt);
                            importedCount++;
                        }
                    }

                    lblStatus.Text = $"Successfully imported {importedCount} events (skipped {events.Count - importedCount} duplicates)";
                    lblStatus.ForeColor = Color.LightGreen;

                    MessageBox.Show($"Import complete!\n\nImported: {importedCount} events\nSkipped: {events.Count - importedCount} duplicates",
                        "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblStatus.Text = "No events found in file.";
                    lblStatus.ForeColor = Color.Orange;
                    MessageBox.Show("No events found in the selected file.", "No Events",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Import failed!";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Error importing file:\n{ex.Message}", "Import Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnImport.Enabled = true;
                btnImport.Text = "Import from .ics File";
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var events = EventDataService.Instance.LoadAllEvents();

                if (events.Count == 0)
                {
                    MessageBox.Show("No events to export.", "No Events",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "iCalendar files (*.ics)|*.ics";
                    saveFileDialog.Title = "Export Calendar";
                    saveFileDialog.FileName = $"NotiHub_Export_{DateTime.Now:yyyyMMdd}.ics";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        btnExport.Enabled = false;
                        btnExport.Text = "Exporting...";
                        lblStatus.Text = "Exporting events...";
                        lblStatus.ForeColor = Color.Yellow;

                        bool success = ICalendarService.Instance.ExportToICS(events, saveFileDialog.FileName);

                        if (success)
                        {
                            lblStatus.Text = $"Successfully exported {events.Count} events";
                            lblStatus.ForeColor = Color.LightGreen;

                            MessageBox.Show($"Successfully exported {events.Count} events to:\n{saveFileDialog.FileName}\n\n" +
                                          "You can now import this file into Google Calendar, Outlook, or Apple Calendar.",
                                "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            lblStatus.Text = "Export failed!";
                            lblStatus.ForeColor = Color.Red;
                            MessageBox.Show("Failed to export events.", "Export Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        btnExport.Enabled = true;
                        btnExport.Text = "Export to .ics File";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Export failed!";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Error exporting events:\n{ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnExport.Enabled = true;
                btnExport.Text = "Export to .ics File";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}
