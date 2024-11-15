using BIUK9000.Dithering;
using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class Painter
    {
        public static void DrawLine(Bitmap bmp, Point p1, Point p2, Color paintColor,int transparency, float thickness)
        {
            using Graphics g = Graphics.FromImage(bmp);
            Color paintColorWithTransparency = Color.FromArgb(transparency, paintColor.R, paintColor.G, paintColor.B);
            using Pen p = new Pen(new SolidBrush(paintColorWithTransparency), thickness);
            p.LineJoin = LineJoin.Round;
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.DrawLine(p, p1, p2);
        }
        public static Bitmap DeleteColor(Bitmap bmp, Point p, int tolerance)
        {
            using FastBitmap fbmp = new FastBitmap(bmp);
            Color c1 = fbmp.GetPixel(p.X, p.Y);
            for(int i = 0; i < bmp.Width; i++)
            {
                for(int j = 0; j < bmp.Height; j++)
                {
                    Color c2 = fbmp.GetPixel(i, j);
                    if(ColorIsWithinTolerance(c1, c2, tolerance))
                    {
                        Color transparentC2 = Color.FromArgb(0, c2.R, c2.G, c2.B);
                        fbmp.SetPixel(i, j, transparentC2);
                    }
                }
            }
            return new Bitmap(fbmp.Bitmap);
        }
        private static int[] ColorDistances(Color c1, Color c2)
        {
            int[] result = new int[3];
            result[0] = Math.Abs(c2.R - c1.R);
            result[1] = Math.Abs(c2.G - c1.G);
            result[2] = Math.Abs(c2.B - c1.B);

            return result;
        }
        private static bool ColorIsWithinTolerance(Color c1, Color c2, int tolerance)
        {
            int[] distances = ColorDistances(c1, c2);
            return !(distances[0] > tolerance || distances[1] > tolerance || distances[2] > tolerance);
        }
    }
}
