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

namespace BIUK9000.GifferComponents.GFLVariants
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
            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        public void ReplaceOriginalBitmap(Bitmap bitmap)
        {
            OriginalBitmap.Dispose();
            Initialize(bitmap);
        }
        public override void Dispose()
        {
            OriginalBitmap?.Dispose();
            base.Dispose();
        }
        public override void CopyParameters(GFL layer)
        {
            base.CopyParameters(layer);
            OriginalBitmap = (layer as BitmapGFL).OriginalBitmap;
        }
        public override GFL Clone()
        {
            BitmapGFL clone = new BitmapGFL(OriginalBitmap, LayerID);
            clone.CopyParameters(this);
            return clone;
        }
    }
}
