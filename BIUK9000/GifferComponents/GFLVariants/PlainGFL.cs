using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents.GFLVariants
{
    internal class PlainGFL : GFL
    {
        public Color Color { get; set; }
        public PlainGFL(Color color, int width, int height, int layerID) : base(layerID)
        {
            Color = color;
            layerID = LayerID;
            Width = width;
            Height = height;
        }
        public override GFL Clone()
        {
            PlainGFL result = new PlainGFL(Color, Width, Height, LayerID);
            result.CopyParameters(this);
            return result;
        }
        public override void CopyDifferingParams(GFL ogState, GFL newState)
        {
            base.CopyDifferingParams(ogState, newState);
            PlainGFL ogl = (PlainGFL)ogState;
            PlainGFL ngl = (PlainGFL)newState;
            if (ogl.Color != ngl.Color) Color = ngl.Color;
        }
        public override void CopyParameters(GFL layer)
        {
            base.CopyParameters(layer);
            Color = (layer as PlainGFL).Color;
        }

        public override Bitmap MorphedBitmap()
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            g.Clear(Color);
            return result;
        }
        public override void Lerp(GFL start, GFL end, double distance)
        {
            base.Lerp(start, end, distance);
            Color = LerpColor((start as PlainGFL).Color, (end as PlainGFL).Color, distance);
        }
    }
}
