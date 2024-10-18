using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Bitmap OriginalBitmap { get; set; }
        public Bitmap MorphedBitmap
        {
            get
            {
                Bitmap result = new Bitmap(Width, Height);
                using Graphics g = Graphics.FromImage(result);
                DrawLayer(g);
                return result;
            }
        }

        public event EventHandler ParameterChanged;

        public GifFrameLayer(Bitmap bitmap)
        {
            Initialize(bitmap);
        }
        public void DrawLayer(Graphics g)
        {
            g.TranslateTransform(Position.X + Width / 2, Position.Y + Height / 2);
            g.RotateTransform(Rotation);
            if(Visible)g.DrawImage(OriginalBitmap, -Width / 2, -Height / 2, Width, Height);
            g.ResetTransform();
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
