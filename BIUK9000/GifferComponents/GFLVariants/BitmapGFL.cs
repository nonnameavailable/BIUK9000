using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents.GFLVariants
{
    public class BitmapGFL : GFL
    {
        public Bitmap OriginalBitmap { get; set; }
        //private Bitmap cachedMorphedBitmap;
        public double HRatio { get => Width / (double)OriginalBitmap.Width; }
        public double VRatio { get => Height / (double)OriginalBitmap.Height; }
        private int cachedWidth, cachedHeight;
        public override Bitmap MorphedBitmap()
        {
            //if (cachedMorphedBitmap == null || Width != cachedWidth || Height != cachedHeight)
            //{
            //    cachedMorphedBitmap?.Dispose();
            //    cachedMorphedBitmap = new Bitmap(OriginalBitmap, Width, Height);
            //    cachedWidth = Width;
            //    cachedHeight = Height;
            //}
            //return new Bitmap(cachedMorphedBitmap);
            return new Bitmap(OriginalBitmap, Width, Height);
        }
        public BitmapGFL(Bitmap bitmap, int layerID) : base(layerID)
        {
            OriginalBitmap = new Bitmap(bitmap);
            Width = bitmap.Width;
            Height = bitmap.Height;
            //cachedMorphedBitmap = new Bitmap(bitmap);
        }
        public override OVector AbsoluteCenter()
        {
            return new OVector(OriginalBitmap.Width / 2d, OriginalBitmap.Height / 2d);
        }
        public void ReplaceOriginalBitmap(Bitmap bitmap)
        {
            OriginalBitmap?.Dispose();
            OriginalBitmap = new Bitmap(bitmap);
            //cachedMorphedBitmap?.Dispose();
            //cachedMorphedBitmap = new Bitmap(bitmap);
        }
        //public void UpdateAfterPaint()
        //{
        //    cachedMorphedBitmap?.Dispose();
        //    cachedMorphedBitmap = new Bitmap(OriginalBitmap);
        //    cachedWidth = cachedMorphedBitmap.Width;
        //    cachedHeight = cachedMorphedBitmap.Height;
        //}
        public override void Dispose()
        {
            OriginalBitmap?.Dispose();
            //cachedMorphedBitmap?.Dispose();
            base.Dispose();
        }
        public override GFL Clone()
        {
            BitmapGFL clone = new BitmapGFL(OriginalBitmap, LayerID);
            clone.CopyParameters(this);
            return clone;
        }
    }
}
