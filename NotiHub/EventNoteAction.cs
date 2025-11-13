using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace NotiHub
{
    public partial class EventNoteAction : Form
    {
        public EventNoteAction()
        {
            InitializeComponent();
            LoadStatusOptions();
        }

        /// <summary>
        /// Loads all available status options into the combo box
        /// </summary>
        private void LoadStatusOptions()
        {
            // Clear existing items
            comboBoxSelection.Items.Clear();

            // Add all status options
            string[] statuses = new string[]
            {           
                "Completed",
                "Pending",
                "Reschedule",
                "Cancel",
                "Expired",             
            };

            // Add each status to the combo box
            foreach (var status in statuses)
            {
                comboBoxSelection.Items.Add(status);
            }

            // Set the first item as default
            if (comboBoxSelection.Items.Count > 0)
            {
                comboBoxSelection.SelectedIndex = 0;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the selected status from the combo box
                string selectedStatus = comboBoxSelection.SelectedItem?.ToString();

                // Validate that a status has been selected
                if (string.IsNullOrWhiteSpace(selectedStatus))
                {
                    MessageBox.Show("Please select a status before saving changes.", "No Status Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get all selected note cards
                List<EventNoteCards> selectedCards = EventNoteCards.GetSelectedCards();

                // Check if any cards are selected
                if (selectedCards.Count == 0)
                {
                    MessageBox.Show("Please select at least one event card before saving changes.", "No Cards Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Apply the selected status to each selected card and update EventData
                foreach (var card in selectedCards)
                {
                    card.UpdateStatus(selectedStatus);
                }

                // Save all updated events to JSON file
                SaveUpdatedEventsToJSON(selectedCards);

                MessageBox.Show($"Successfully applied '{selectedStatus}' status to {selectedCards.Count} event(s).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear selection after saving
                EventNoteCards.ClearSelection();

                // Close the form after successful save
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves the updated events to the JSON file
        /// </summary>
        private void SaveUpdatedEventsToJSON(List<EventNoteCards> selectedCards)
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folderPath = Path.Combine(appDataPath, "NotiHub", "EventCalendar");
                string filePath = Path.Combine(folderPath, "eventcalendar.json");

                // Read existing events from file
                List<EventData> allEvents = new List<EventData>();
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    allEvents = JsonConvert.DeserializeObject<List<EventData>>(jsonContent) ?? new List<EventData>();
                }

                // Update the events that were modified
                foreach (var card in selectedCards)
                {
                    EventData eventData = card.GetEventData();
                    if (eventData != null)
                    {
                        // Find and update the event in the list
                        var existingEvent = allEvents.FirstOrDefault(e =>
                            e.EventName == eventData.EventName &&
                            e.EventDate == eventData.EventDate);

                        if (existingEvent != null)
                        {
                            existingEvent.Status = eventData.Status;
                        }
                    }
                }

                // Save the updated list back to file
                string jsonOutput = JsonConvert.SerializeObject(allEvents, Formatting.Indented);
                File.WriteAllText(filePath, jsonOutput);

                Console.WriteLine("Events updated successfully in JSON file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving updated events to JSON: {ex.Message}");
                throw;
            }
        }
    }
}
