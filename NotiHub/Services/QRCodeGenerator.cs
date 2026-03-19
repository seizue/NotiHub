using System;
using System.Drawing;

namespace NotiHub.Services
{
    public class SimpleQRCodeGenerator
    {
        // Professional QR code generator using QRCoder library
        // Install via NuGet: Install-Package QRCoder
        public static Bitmap GenerateQRCode(string url, int size = 300)
        {
            try
            {
                // Using QRCoder library for proper QR code generation
                var qrGenerator = new QRCoder.QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCoder.QRCode(qrCodeData);
                
                // Generate QR code with white background and black foreground
                Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, true);
                
                // Resize to requested size if needed
                if (qrCodeImage.Width != size || qrCodeImage.Height != size)
                {
                    Bitmap resized = new Bitmap(qrCodeImage, new Size(size, size));
                    qrCodeImage.Dispose();
                    return resized;
                }
                
                return qrCodeImage;
            }
            catch (Exception ex)
            {
                // Fallback to simple placeholder if QRCoder fails
                return GeneratePlaceholderQRCode(url, size);
            }
        }

        // Fallback method if QRCoder is not available
        private static Bitmap GeneratePlaceholderQRCode(string url, int size)
        {
            Bitmap qrBitmap = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(qrBitmap))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int padding = 20;
                int qrSize = size - (padding * 2);

                // Draw border
                using (Pen borderPen = new Pen(Color.Black, 3))
                {
                    g.DrawRectangle(borderPen, padding, padding, qrSize, qrSize);
                }

                // Draw corner squares (QR code style)
                int cornerSize = qrSize / 5;
                DrawCornerSquare(g, padding, padding, cornerSize);
                DrawCornerSquare(g, padding + qrSize - cornerSize, padding, cornerSize);
                DrawCornerSquare(g, padding, padding + qrSize - cornerSize, cornerSize);

                // Draw center text
                string displayText = "QR Code\nPlaceholder";
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    g.DrawString(displayText, font, Brushes.Black, 
                        new RectangleF(padding, padding, qrSize, qrSize), sf);
                }

                // Draw URL at bottom
                using (Font urlFont = new Font("Arial", 7))
                {
                    string shortUrl = url.Length > 40 ? url.Substring(0, 37) + "..." : url;
                    g.DrawString(shortUrl, urlFont, Brushes.Gray, 
                        new PointF(padding + 5, size - 15));
                }
            }

            return qrBitmap;
        }

        private static void DrawCornerSquare(Graphics g, int x, int y, int size)
        {
            // Outer square
            g.FillRectangle(Brushes.Black, x, y, size, size);
            // Inner white square
            int innerPadding = size / 5;
            g.FillRectangle(Brushes.White, x + innerPadding, y + innerPadding, 
                size - (innerPadding * 2), size - (innerPadding * 2));
            // Center black square
            int centerPadding = size / 3;
            g.FillRectangle(Brushes.Black, x + centerPadding, y + centerPadding, 
                size - (centerPadding * 2), size - (centerPadding * 2));
        }
    }
}
