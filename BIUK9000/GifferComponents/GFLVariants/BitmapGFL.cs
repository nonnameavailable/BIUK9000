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
        public double OriginalWidthToHeight { get => (double)OriginalBitmap.Width /  (double)OriginalBitmap.Height; }
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
            //return new OVector(OriginalBitmap.Width / 2d, OriginalBitmap.Height / 2d);
            return new OVector(OriginalBitmap.Width * _xMult, OriginalBitmap.Height * _yMult);
        }
        public override void OverrideCenter(double xMult, double yMult)
        {
            OVector ltc = LTCorner();
            _xMult = xMult * Width / OriginalWidth;
            _yMult = yMult * Height / OriginalHeight;
            OVector nltc = LTCorner();
            OVector dif = nltc.Copy().Subtract(ltc);
            Position = Position.Copy().Subtract(dif);
        }
        public override OVector LTCorner()
        {
            OVector c = new OVector(Width * _xMult, Height * _yMult);
            c.Multiply(-1);
            c.Rotate(Rotation);
            return Center().Add(c);
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
