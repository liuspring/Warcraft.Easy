using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ThoughtWorks.QRCode.Codec;

namespace Common
{
    public class CreateQRCode
    {
        public static byte[] GenerateImage(string title, int width, int height, string qrCodeContent, int qRCodeScale = 25, string line1 = "", string line2 = "", string line3 = "")
        {
            Bitmap bt = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bt);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Font fn1 = new Font("Tahoma", 20, FontStyle.Bold);
            Font fn2 = new Font("Tahoma", 15, FontStyle.Regular);
            Font fn = new Font("Tahoma", 5, FontStyle.Bold);
            g.Clear(Color.White);
            int y = 3; //∂•≤øº‰æ‡
            if (!string.IsNullOrEmpty(title))
            {
                int top_margin = 20;

                var rect = new Rectangle(0, y, width, top_margin);
                using (Brush b = new LinearGradientBrush(rect, Color.White, Color.White, 0f))
                {
                    g.FillRectangle(b, rect);
                }
                using (var format = new StringFormat{Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center})
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(title, fn1.FontFamily, (int) fn1.Style, fn1.Size, rect, format);
                        using (SolidBrush sBrush = new SolidBrush(Color.Black))
                        {
                            g.FillPath(sBrush, path);
                        }
                    }
                }
                y += top_margin;
            }
            var qrCodeEncoder = new QRCodeEncoder
            {
                QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
                QRCodeScale = qRCodeScale,
                QRCodeVersion = 0,
                QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M
            };
            using (var newimage = qrCodeEncoder.Encode(qrCodeContent, Encoding.UTF8))
            {
                var left = (width - newimage.Width)/2;

                y += 10; //º‰æ‡3PX
                g.DrawImage(newimage, left, y, newimage.Width, newimage.Height);
                y += newimage.Height;
                y += 10; //º‰æ‡3PX
            }

            if (!string.IsNullOrEmpty(line1))
            {
                int line1_height = 15;
                var rect = new Rectangle(0, y, width, line1_height);
                using (Brush b = new LinearGradientBrush(rect, Color.White, Color.White, 0f))
                {
                    g.FillRectangle(b, rect);
                }
                using (var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(line1, fn2.FontFamily, (int)fn2.Style, fn2.Size, rect, format);
                        using (SolidBrush sBrush = new SolidBrush(Color.Black))
                        {
                            g.FillPath(sBrush, path);
                        }
                    }
                }
                y += line1_height;
            }
            if (!string.IsNullOrEmpty(line2))
            {
                y += 15 + 50; //º‰æ‡30PX
                g.DrawString(line2, fn, Brushes.Black, new PointF(5, y));
            }
            if (!string.IsNullOrEmpty(line3))
            {
                y += 15 + 8; //º‰æ‡8PX
                g.DrawString(line3, fn, Brushes.Black, new PointF(5, y));
            }
            // Õ∑≈ÕºœÒª∫¥Ê
            g.Dispose();

            byte[] b2;
            using (MemoryStream stream = new MemoryStream())
            {
                bt.Save(stream, ImageFormat.Png);
                b2 = stream.ToArray();
            }
            return b2;
        }

    }
}