using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public abstract class GFL
    {
        public Point Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Visible { get; set; }
        public float Rotation { get; set; }
        public abstract Bitmap MorphedBitmap { get; }
        public abstract Point Center();
        public abstract void DrawLayer(Graphics g);
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
