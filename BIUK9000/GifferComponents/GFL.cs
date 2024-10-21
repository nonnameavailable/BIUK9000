using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public bool IsTextLayer {  get; set; }
        public abstract Bitmap MorphedBitmap();
        public abstract Point Center();
        public void DrawLayer(Graphics g, bool drawHelp)
        {
            if (Visible)
            {
                GraphicsState gs = g.Save();

                Point c = Center();
                g.TranslateTransform(c.X, c.Y);
                g.RotateTransform(Rotation);

                using Bitmap morphedBitmap = MorphedBitmap();
                g.DrawImage(morphedBitmap, -morphedBitmap.Width / 2, - morphedBitmap.Height / 2);

                if(drawHelp)
                {
                    using Pen boundsPen = new Pen(Color.Red, 2f);
                    g.DrawRectangle(boundsPen, -morphedBitmap.Width / 2, -morphedBitmap.Height / 2, morphedBitmap.Width, morphedBitmap.Height);
                }

                g.Restore(gs);
            }
        }
        public abstract Rectangle BoundingRectangle { get; set; }
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
