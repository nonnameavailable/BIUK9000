using Emgu.CV;
using Emgu.CV.Reg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BIUK9000
{
    public class GifFrameLayer
    {
        public Point Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Visible { get; set; }
        public float Rotation { get; set; }
        public string Text { get; set; }
        public Bitmap OriginalBitmap { get; set; }
        public bool IsTextLayer { get; set; }
        public string Font {  get; set; }
        public Color FontColor { get; set; }
        public Color FontBorderColor { get; set; }
        public float FontBorderWidth { get; set; }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(Position.X, Position.Y, Width, Height);
            }
            set
            {
                Position = new Point(value.X, value.Y);
                Width = value.Width;
                Height = value.Height;
            }
        }
        public Point Center
        { 
            get
            {
                return new Point(Position.X + Width / 2, Position.Y + Height / 2);
            }
        }

        public event EventHandler ParameterChanged;

        public GifFrameLayer(Bitmap bitmap)
        {
            Initialize(bitmap);
        }
        public GifFrameLayer(string text)
        {
            Text = text;
            IsTextLayer = true;
            Position = new Point(0, 0);
            Width = 100;
            Height = 100;
            Visible = true;
            Rotation = 0;
        }
        public void DrawLayer(Graphics g)
        {
            GraphicsState gs = g.Save();
            g.TranslateTransform(Position.X + Width / 2, Position.Y + Height / 2);
            g.RotateTransform(Rotation);
            if (Visible)
            {
                if (!IsTextLayer)
                {
                    g.DrawImage(OriginalBitmap, -Width / 2, -Height / 2, Width, Height);
                } else
                {
                    // Font and brush for the text
                    using System.Drawing.Font font = new System.Drawing.Font(Font, Width / 4); // width for fontsize
                    using Brush textBrush = new SolidBrush(FontColor);

                    // Pen for the border
                    using Pen borderPen = new Pen(FontBorderColor, FontBorderWidth);
                    borderPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round; // Smooth the corners

                    // Create a GraphicsPath
                    using GraphicsPath path = new GraphicsPath();
                    path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, new Point(10, 10), StringFormat.GenericDefault);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    // Draw the border
                    g.DrawPath(borderPen, path);
                    // Fill the text
                    g.FillPath(textBrush, path);
                }
            }
            g.Restore(gs);
        }

        private void Initialize(Bitmap bitmap)
        {
            OriginalBitmap = bitmap;
            Position = new Point(0, 0);
            Width = bitmap.Width;
            Height = bitmap.Height;
            Visible = true;
            Rotation = 0;
        }

        public void ReplaceOriginalBitmap(Bitmap bitmap)
        {
            OriginalBitmap.Dispose();
            Initialize(bitmap);
        }

        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Move(int x, int y)
        {
            Position = new Point(Position.X + x, Position.Y + y);
        }
    }
}
