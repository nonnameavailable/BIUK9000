using Emgu.CV.Reg;
using Emgu.CV.XImgproc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public class GifFrame
    {
        public event EventHandler LayerCountChanged;
        protected virtual void OnLayerCountChanged()
        {
            LayerCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<GFL> Layers { get; } = new List<GFL>();
        public int Width { get; set; }
        public int Height { get; set; }
        public GifFrame(Bitmap bitmap)
        {
            Bitmap background = new Bitmap(bitmap.Width, bitmap.Height);
            using Graphics g = Graphics.FromImage(background);
            g.Clear(Color.White);
            GFL bgfl = new BitmapGFL(background);
            Layers.Add(bgfl);
            GFL gfl = new BitmapGFL(bitmap);
            Layers.Add(gfl);
            Width = bitmap.Width;
            Height = bitmap.Height;
        }

        public void AddSpace(int up, int right, int down, int left)
        {
            BitmapGFL bgfl = (BitmapGFL)Layers[0];
            int oWidth = bgfl.OriginalBitmap.Width;
            int oHeight = bgfl.OriginalBitmap.Height;
            Bitmap replacementBitmap = new Bitmap(oWidth + left + right, oHeight + up + down);
            using Graphics g = Graphics.FromImage(replacementBitmap);
            g.Clear(Color.White);
            bgfl.ReplaceOriginalBitmap(replacementBitmap);
            Width = replacementBitmap.Width;
            Height = replacementBitmap.Height;

            for (int i = 0; i < Layers.Count; i++)
            {
                GFL cl = Layers[i];
                if (i > 0)
                {
                    cl.Move(left, up);
                }
            }
            OnLayerCountChanged();
        }
        public GifFrame(int width, int height)
        {
            GFL gfl = new BitmapGFL(new Bitmap(width, height));
            Layers.Add(gfl);
            Width = width;
            Height = height;
        }
        public Bitmap CompleteBitmap(bool drawHelp)
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            foreach (GFL layer in Layers)
            {
                layer.DrawLayer(g, drawHelp);
            }
            return result;
        }

        public void AddLayer(Bitmap bitmap)
        {
            Layers.Add(new BitmapGFL(bitmap));
            OnLayerCountChanged();
        }
        public void AddLayer(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(Brushes.Black, 0, 0, width, height);
            Layers.Add(new BitmapGFL(bitmap));
            OnLayerCountChanged();
        }
        public void AddLayer(GFL layer)
        {
            Layers.Add(layer);
            OnLayerCountChanged();
        }
    }
}
