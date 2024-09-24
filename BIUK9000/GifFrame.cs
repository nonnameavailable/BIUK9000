using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class GifFrame
    {
        public List<GifFrameLayer> Layers { get; } = new List<GifFrameLayer>();
        public int Width { get; set; }
        public int Height { get; set; }
        public GifFrame(Bitmap bitmap)
        {
            Layers.Add(new GifFrameLayer(bitmap));
            Width = bitmap.Width;
            Height = bitmap.Height;
        }
        public GifFrame(int width, int height)
        {
            Layers.Add(new GifFrameLayer(new Bitmap(width, height)));
            Width = width;
            Height = height;
        }
        public Bitmap CompleteBitmap()
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            foreach (GifFrameLayer layer in Layers)
            {
                layer.DrawLayer(g);
            }
            return result;
        }
    }
}
