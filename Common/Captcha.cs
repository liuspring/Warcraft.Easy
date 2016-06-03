using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class Captcha:IDisposable
    {
        private static readonly Random m_rand = new Random();
        // Public properties (all read-only).
        public string Text
        {
            get { return m_text; }
        }
        public Bitmap Image
        {
            get { return m_image; }
        }
        public int Width
        {
            get { return m_width; }
        }
        public int Height
        {
            get { return m_height; }
        }
        // Internal properties.
        private readonly string m_text;
        private readonly int m_width;
        private readonly int m_height;
        private Bitmap m_image;
        private Color m_color = Color.PaleGreen;
        public Captcha(string s, Color bc, int width, int height)
        {
            m_text = s;
            m_width = width;
            m_height = height;
            m_color = bc;
            GenerateImage();
        }
        /// <summary>
        /// 验证码图片流
        /// </summary>
        /// <returns></returns>
        public byte[] GetImageBytes()
        {
            byte[] b;
            using (MemoryStream stream = new MemoryStream())
            {
                Image.Save(stream, ImageFormat.Jpeg);
                b = stream.ToArray();
            }
            return b;
        }
        ~Captcha()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose of the bitmap.
                m_image.Dispose();
            }
        }

        private readonly FontFamily[] m_fonts = {
            //new FontFamily("Times New Roman"), 
            new FontFamily("Georgia"), 
            new FontFamily("Arial"), 
            new FontFamily("Comic Sans MS")
        };

        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(m_width, m_height, PixelFormat.Format32bppArgb);
            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, m_width, m_height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            using (Brush b = new System.Drawing.Drawing2D.LinearGradientBrush(rect, Color.WhiteSmoke, m_color, 50f, false))
            {
                g.FillRectangle(b, rect);
            }
            //g.Clear(Color.Black);
            int nRed, nGreen, nBlue;  // 背景的三元色
            System.Random rd = new Random();
            nRed = rd.Next(32);
            nGreen = rd.Next(32);
            nBlue = rd.Next(32);
            //Color bgColor = Color.FromArgb(nRed + 204, nGreen + 204, nBlue + 204);
            //g.Clear(bgColor);
            int y = rd.Next(4);
            Color fontColor = System.Drawing.Color.FromArgb(nRed * y, nGreen * y, nBlue * y);

            /*
            using (SolidBrush b = new SolidBrush(bgColor))
            {
                g.FillRectangle(b, rect);
            }
            */
            // Set up the text font.
            int emSize = 20;// (int) (m_width * 1.2 / m_text.Length);
            FontFamily family = m_fonts[m_rand.Next(0, m_fonts.Length - 1)];
            //Font font = new Font("Arial", emSize, FontStyle.Bold);
            Font font = new Font(family, emSize, FontStyle.Bold);
            // Adjust the font size until the text fits within the image.
            SizeF measured = new SizeF(0, 0);
            SizeF workingSize = new SizeF(m_width, m_height);
            //while (emSize > 2 &&
            //    (measured = g.MeasureString(text, font)).Width > workingSize.Width ||
            //    measured.Height > workingSize.Heikght)
            //{
            //    font.Dispose();
            //    font = new Font(family, emSize -= 2, FontStyle.Bold);
            //}
            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(m_text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            // Set font color to a color that is visible within background color
            SolidBrush sBrush = new SolidBrush(fontColor);
            g.FillPath(sBrush, path);

            //干扰线
            lock (_rnd)
            {
                int height = m_height;
                int width = m_width;
                g.DrawBezier(new Pen(sBrush, 1), new Point(_rnd.Next(1, 11), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.85), (int)((Double)width * 0.95)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))));
                g.DrawBezier(new Pen(sBrush, 1), new Point(_rnd.Next(1, 11), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.85), (int)((Double)width * 0.95)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))));
                g.DrawBezier(new Pen(sBrush, 1), new Point(_rnd.Next(1, 11), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.1), (int)((Double)width * 0.9)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))),
                    new Point(_rnd.Next((int)((Double)width * 0.85), (int)((Double)width * 0.95)), _rnd.Next((int)((Double)height * 0.1), (int)((Double)height * 0.9))));
            }
            for (int i = 0; i < 200; i++)
            {
                int w = _rnd.Next(bitmap.Width);
                int h = _rnd.Next(bitmap.Height);
                bitmap.SetPixel(w, h, Color.FromArgb(_rnd.Next()));
            }

            // Clean up.
            font.Dispose();
            sBrush.Dispose();
            g.Dispose();

            m_image = bitmap;
        }

        private static Random _rnd = new Random();

        private static char[] constant =   
      {   
        '0','1','2','3','4','5','6','7','8','9',  
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
      };
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}
