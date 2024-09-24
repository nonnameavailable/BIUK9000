using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class GifFrameLayer
    {
        public Bitmap OriginalBitmap { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public GifFrameLayer(Bitmap bitmap)
        {
            OriginalBitmap = bitmap;
            X = 0;
            Y = 0;
            Width = bitmap.Width;
            Height = bitmap.Height;
        }
        public void DrawLayer(Graphics g)
        {
            g.DrawImage(OriginalBitmap, X, Y, Width, Height);
        }
    }
}
