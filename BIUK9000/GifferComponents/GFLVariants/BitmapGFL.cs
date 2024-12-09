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
        //private int cachedWidth, cachedHeight;
        public int OriginalWidth { get => OriginalBitmap.Width; }
        public int OriginalHeight {  get => OriginalBitmap.Height; }
        public override Bitmap MorphedBitmap(InterpolationMode interpolationMode)
        {
            //if (cachedMorphedBitmap == null || Width != cachedWidth || Height != cachedHeight)
            //{
            //    cachedMorphedBitmap?.Dispose();
            //    cachedMorphedBitmap = new Bitmap(OriginalBitmap, Width, Height);
            //    cachedWidth = Width;
            //    cachedHeight = Height;
            //}
            //return new Bitmap(cachedMorphedBitmap);
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = interpolationMode;
            g.DrawImage(OriginalBitmap, 0, 0, Width, Height);
            return result;
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
