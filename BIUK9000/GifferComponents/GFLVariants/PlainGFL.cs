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
        public Color BackColor { get; set; }
        public PlainGFL(Color color, int width, int height, int layerID) : base(layerID)
        {
            BackColor = color;
            layerID = LayerID;
            Width = width;
            Height = height;
        }
        public override GFL Clone()
        {
            PlainGFL result = new PlainGFL(BackColor, Width, Height, LayerID);
            result.CopyParameters(this);
            return result;
        }
        public override void CopyDifferingParams(GFL ogState, GFL newState)
        {
            base.CopyDifferingParams(ogState, newState);
            PlainGFL ogl = (PlainGFL)ogState;
            PlainGFL ngl = (PlainGFL)newState;
            if (ogl.BackColor != ngl.BackColor) BackColor = ngl.BackColor;
        }
        public override void CopyParameters(GFL layer)
        {
            base.CopyParameters(layer);
            BackColor = (layer as PlainGFL).BackColor;
        }

        public override Bitmap MorphedBitmap()
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            g.Clear(BackColor);
            return result;
        }
    }
}
