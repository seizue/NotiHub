using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotiHub.Services;

namespace NotiHub
{
    public partial class ShareNote : Form
    {
        private string shareUrl;
        
        public ShareNote(string shareUrl)
        {
            InitializeComponent();
            this.shareUrl = shareUrl;
            InitializeShareNote();
        }

        private void InitializeShareNote()
        {
            // Set the title
            lblTitle.Text = "Note Shared Successfully!";
            lblTitle.ReadOnly = true;
            
            // Set the instruction
            lblInstruction.Text = "Scan the QR code or copy the link below to share:";
            lblInstruction.ReadOnly = true;
            
            // Set the share link
            txtShareLink.Text = shareUrl;
            
            // Generate and display QR code
            try
            {
                Bitmap qrCode = SimpleQRCodeGenerator.GenerateQRCode(shareUrl, 300);
                pictureBoxQR.Image = qrCode;
                pictureBoxQR.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(shareUrl))
                {
                    Clipboard.SetText(shareUrl);
                    MessageBox.Show("Link copied to clipboard!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No link to copy.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying to clipboard: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveQR_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxQR.Image == null)
                {
                    MessageBox.Show("No QR code to save.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save QR Code";
                    saveFileDialog.FileName = $"NotiHub_QRCode_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Determine image format based on file extension
                        ImageFormat format = ImageFormat.Png;
                        string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                        
                        switch (extension)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                            default:
                                format = ImageFormat.Png;
                                break;
                        }

                        // Save the QR code image
                        pictureBoxQR.Image.Save(saveFileDialog.FileName, format);
                        
                        MessageBox.Show("QR code saved successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving QR code: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
