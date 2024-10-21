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

        public override Bitmap MorphedBitmap()
        {
            return new Bitmap(OriginalBitmap, Math.Max(Math.Abs(Width), 1), Math.Max(Math.Abs(Height), 1));
        }

        public override Point Center()
        {
            return new Point(Position.X + Width / 2, Position.Y + Height / 2);
        }
        public override Rectangle BoundingRectangle
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

        public BitmapGFL(Bitmap bitmap)
        {
            Initialize(bitmap);
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
