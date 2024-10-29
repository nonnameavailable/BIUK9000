using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public class GifFrame : IDisposable
    {
        private bool disposedValue;

        public event EventHandler LayerCountChanged;
        public int FrameDelay {  get; set; }
        protected virtual void OnLayerCountChanged()
        {
            LayerCountChanged?.Invoke(this, EventArgs.Empty);
        }
        public Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = new Rectangle(Position, new Size(Width, Height));
            Layers.ForEach(layer => layer.Save());
        }
        public void MoveFromOBR(int x, int y)
        {
            Layers.ForEach(layer => layer.MoveFromOBR(x, y));
        }
        public Point Center()
        {
            return new Point(Position.X + Width / 2, Position.Y + Height / 2);
        }
        public virtual void Resize(int xSizeDif, int ySizeDif)
        {
            Width = OBR.Width + xSizeDif * 2;
            Height = OBR.Height + ySizeDif * 2;
        }

        public List<GFL> Layers { get; } = new List<GFL>();
        public int Width { get; set; }
        public int Height { get; set; }
        public Point Position { get; set; }
        public GifFrame(Bitmap bitmap, int frameDelay)
        {
            GFL gfl = new BitmapGFL(bitmap);
            Layers.Add(gfl);
            Width = bitmap.Width;
            Height = bitmap.Height;
            Position = new Point(0, 0);
            FrameDelay = frameDelay;
        }

        public void AddSpace(int up, int right, int down, int left)
        {
            Width += left + right;
            Height += up + down;

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
            int absWidth = Math.Abs(Width);
            int absHeight = Math.Abs(Height);
            Bitmap result = new Bitmap(absWidth, absHeight);
            using Graphics g = Graphics.FromImage(result);
            //g.TranslateTransform(Position.X, Position.Y);
            foreach (GFL layer in Layers)
            {
                layer.DrawLayer(g, drawHelp);
            }
            if (drawHelp)
            {
                using Pen boundsPen = new Pen(Color.Red, 2f);
                g.DrawRectangle(boundsPen, 0, 0, absWidth, absHeight);
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

        public void RemoveLayer(int index)
        {
            if(index > 0 && index < Layers.Count)
            {
                Layers[index].Dispose();
                Layers.RemoveAt(index);
                OnLayerCountChanged();
            }
        }
        public void RemoveLayer(GFL layer)
        {
            Layers.Remove(layer);
            layer.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    for(int i = 0; i < Layers.Count; i++)
                    {
                        Layers[i].Dispose();
                        Layers[i] = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
