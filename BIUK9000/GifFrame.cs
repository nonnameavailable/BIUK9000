using Emgu.CV.Reg;
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
        public event EventHandler LayerCountChanged;
        protected virtual void OnLayerCountChanged()
        {
            LayerCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<GifFrameLayer> Layers { get; } = new List<GifFrameLayer>();
        public int Width { get; set; }
        public int Height { get; set; }
        public GifFrame(Bitmap bitmap)
        {
            GifFrameLayer gfl = new GifFrameLayer(bitmap);
            Layers.Add(gfl);
            Width = bitmap.Width;
            Height = bitmap.Height;
        }
        public GifFrame(int width, int height)
        {
            GifFrameLayer gfl = new GifFrameLayer(new Bitmap(width, height));
            Layers.Add(gfl);
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

        public void AddLayer(Bitmap bitmap)
        {
            Layers.Add(new GifFrameLayer(bitmap));
            OnLayerCountChanged();
        }
        public void AddLayer(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, 0, 0, width,height);
            Layers.Add(new GifFrameLayer(bitmap));
            OnLayerCountChanged();
        }
    }
}
