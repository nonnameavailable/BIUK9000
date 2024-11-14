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
        public static void DrawLine(Bitmap bmp, Point p1, Point p2, PaintControl pc)
        {
            using Graphics g = Graphics.FromImage(bmp);
            Color paintColor = pc.PaintColor;
            Color paintColorWithTransparency = Color.FromArgb(pc.Transparency, paintColor.R, paintColor.G, paintColor.B);
            using Pen p = new Pen(new SolidBrush(paintColorWithTransparency), pc.Thickness);
            p.LineJoin = LineJoin.Round;
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.DrawLine(p, p1, p2);
        }
    }
}
