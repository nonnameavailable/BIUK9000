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

namespace BIUK9000.GifferComponents
{
    public class BitmapGFL : GFL
    {
        public Bitmap OriginalBitmap { get; set; }

        public override Bitmap MorphedBitmap
        {
            get
            {
                return new Bitmap(OriginalBitmap, Width, Height);
            }
        }

        public override Point Center()
        {
            return new Point(Position.X + Width / 2, Position.Y + Height / 2);
        }

        public BitmapGFL(Bitmap bitmap)
        {
            Initialize(bitmap);
        }
        public override void DrawLayer(Graphics g)
        {
            GraphicsState gs = g.Save();
            Point c = Center();
            g.TranslateTransform(c.X, c.Y);
            g.RotateTransform(Rotation);
            if (Visible)
            {
                g.DrawImage(OriginalBitmap, -Width / 2, -Height / 2, Width, Height);
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
    }
}
