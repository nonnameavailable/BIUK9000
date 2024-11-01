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

        public BitmapGFL(Bitmap bitmap, int layerID) : base(layerID)
        {
            Initialize(bitmap);
        }

        private void Initialize(Bitmap bitmap)
        {
            OriginalBitmap = bitmap;
            Position = new OVector(0, 0);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (OriginalBitmap != null)
                {
                    OriginalBitmap.Dispose();
                    OriginalBitmap = null;
                }
            }
            base.Dispose(disposing);
        }

        public override GFL Clone()
        {
            BitmapGFL clone = new BitmapGFL(OriginalBitmap, LayerID);
            clone.CopyParameters(this);
            return clone;
        }
    }
}
