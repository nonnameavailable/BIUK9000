using AnimatedGif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        private Image originalGif;
        private bool disposedValue;

        public event EventHandler FrameCountChanged;
        protected virtual void OnFrameCountChanged()
        {
            FrameCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public Giffer(string path)
        {
            Image gif = Image.FromFile(path);
            originalGif = gif;
            Frames = FramesFromGif(gif);
        }

        public Giffer()
        {
            Frames = new List<GifFrame>();
        }

        public void AddSpace(int up, int right, int down, int left)
        {
            foreach (GifFrame gf in Frames)
            {
                gf.AddSpace(up, right, down, left);
            }
        }
        public void MoveFromOBR(int x, int y)
        {
            Frames.ForEach(frame => frame.MoveFromOBR(x, y));
        }
        public void Resize(int xSizeDif, int ySizeDif)
        {
            Frames.ForEach(frame => frame.Resize(xSizeDif, ySizeDif));
        }
        public void Save()
        {
            Frames.ForEach(frame => frame.Save());
        }

        private List<GifFrame> FramesFromGif(Image gif)
        {
            List<GifFrame> result = new();

            int frameCount = gif.GetFrameCount(FrameDimension.Time);

            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new GifFrame(new Bitmap(gif)));
            }
            return result;
        }

        private int FrameDelay(Image gif)
        {
            PropertyItem propertyItem = gif.GetPropertyItem(0x5100);
            return BitConverter.ToInt32(propertyItem.Value, 0) * 10;
        }

        public Image GifFromFrames()
        {
            MemoryStream stream = new MemoryStream();
            int frameDelay = FrameDelay(originalGif);
            AnimatedGifCreator agc = new AnimatedGifCreator(stream, frameDelay);
            foreach (GifFrame frame in Frames)
            {
                agc.AddFrame(frame.CompleteBitmap(false), frameDelay, GifQuality.Bit8);
            }
            return Image.FromStream(stream);
        }
        public void AddFrame(GifFrame frame)
        {
            Frames.Add(frame);
            OnFrameCountChanged();
        }
        public void RemoveFrame(GifFrame frame)
        {
            Frames.Remove(frame);
            frame.Dispose();
            OnFrameCountChanged();
        }
        public void Crop(GifFrame frameWithCropLayer)
        {
            GFL cl = frameWithCropLayer.Layers.Last();
            if (cl is not CropGFL) return;
            Rectangle newRectangle = cl.BoundingRectangle;
            foreach(GifFrame frame in Frames)
            {
                foreach (GFL layer in frame.Layers)
                {
                    layer.Move(-newRectangle.X + layer.Position.X, -newRectangle.Y + layer.Position.Y);
                }
                frame.Width = newRectangle.Width;
                frame.Height = newRectangle.Height;
                //frame.AddSpace(-newRectangle.Y, newRectangle.Right - frame.Width, newRectangle.Bottom - frame.Height, -newRectangle.X);
            }
            frameWithCropLayer.RemoveLayer(cl);
        }
        public void AddGifferAsLayers(Giffer newGiffer)
        {
            for(int i = 0; i < Frames.Count; i++)
            {
                int newGifferIndex = (int)(i / (double)Frames.Count * newGiffer.Frames.Count);
                GifFrame cgf = Frames[i];
                cgf.AddLayer(newGiffer.Frames[newGifferIndex].CompleteBitmap(false));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    for(int i = 0;  i < Frames.Count; i++)
                    {
                        Frames[i].Dispose();
                        Frames[i] = null;
                    }
                    originalGif.Dispose();
                    originalGif = null;
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
