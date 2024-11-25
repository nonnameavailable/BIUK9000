using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents.GFLVariants
{
    internal class ShapeGFL : GFL
    {
        public Color Color { get; set; }
        public DrawShape Shape { get; set; }
        public ShapeGFL(int layerID) : base(layerID)
        {
            Color = Color.White;
            Shape = DrawShape.Rectangle;
        }
        public override GFL Clone()
        {
            ShapeGFL result = new ShapeGFL(LayerID);
            result.CopyParameters(this);
            return result;
        }
        public override void CopyDifferingParams(GFL ogState, GFL newState)
        {
            base.CopyDifferingParams(ogState, newState);
            ShapeGFL ogl = (ShapeGFL)ogState;
            ShapeGFL ngl = (ShapeGFL)newState;
            if (ogl.Color != ngl.Color) Color = ngl.Color;
            if(ogl.Shape != ngl.Shape) Shape = ngl.Shape;
        }
        public override void CopyParameters(GFL layer)
        {
            base.CopyParameters(layer);
            Color = ((ShapeGFL)layer).Color;
            Shape = ((ShapeGFL)layer).Shape;
        }

        public override Bitmap MorphedBitmap(InterpolationMode interpolationMode)
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            if (interpolationMode != InterpolationMode.NearestNeighbor) g.SmoothingMode = SmoothingMode.AntiAlias;
                if (Shape == DrawShape.Ellipse)
            {
                g.Clear(Color.Transparent);
                using Brush brush = new SolidBrush(Color);
                g.FillEllipse(brush, 0, 0, Width, Height);
            } else if (Shape == DrawShape.Rectangle)
            {
                g.Clear(Color);
            }
            return result;
        }
        public override void Lerp(GFL start, GFL end, double distance, OVector position = null)
        {
            base.Lerp(start, end, distance, position);
            Color = Lerper.LerpColor(((ShapeGFL)start).Color, ((ShapeGFL)end).Color, distance);
        }
        public enum DrawShape
        {
            Ellipse,
            Rectangle
        }
    }
}
