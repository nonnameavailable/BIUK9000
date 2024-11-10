using AnimatedGif;
using BIUK9000.Dithering;
using BIUK9000.GifferComponents.GFLVariants;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        private Image originalGif;
        private bool disposedValue;
        private bool _createdEmpty;
        private int _nextLayerID;
        public string OriginalImagePath {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public OVector Position { get; set; }

        public event EventHandler FrameCountChanged;
        protected virtual void OnFrameCountChanged()
        {
            FrameCountChanged?.Invoke(this, EventArgs.Empty);
        }
        public Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = new Rectangle(Position.Xint, Position.Yint, Width, Height);
            foreach (GifFrame frame in Frames)
            {
                frame.Layers.ForEach(layer => layer.Save());
            }
        }
        public void MoveFromOBR(int x, int y)
        {
            foreach(GifFrame frame in Frames)
            {
                frame.Layers.ForEach(layer => layer.MoveFromOBR(x, y));
            }
        }
        public virtual void Resize(int xSizeDif, int ySizeDif)
        {
            Width = OBR.Width + xSizeDif * 2;
            Height = OBR.Height + ySizeDif * 2;
        }

        public Giffer(string path)
        {
            _nextLayerID = 0;
            Image gif = Image.FromFile(path);
            originalGif = gif;
            Frames = FramesFromGif(gif);
            _createdEmpty = false;
            OriginalImagePath = path;
            Width = gif.Width;
            Height = gif.Height;
            Position = new OVector(0, 0);
        }

        public Giffer()
        {
            _nextLayerID = 0;
            Frames = new List<GifFrame>();
            _createdEmpty = true;
            OriginalImagePath = "";
            Width = 50;
            Height = 50;
            Position = new OVector(0, 0);
        }

        private List<GifFrame> FramesFromGif(Image gif)
        {
            List<GifFrame> result = new();

            int frameCount = gif.GetFrameCount(FrameDimension.Time);
            int firstLayerID = NextLayerID();
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new GifFrame(new Bitmap(gif), FrameDelay(), firstLayerID));
            }
            return result;
        }

        public int FrameDelay()
        {
            if (_createdEmpty) return 20;
            PropertyItem propertyItem = originalGif.GetPropertyItem(0x5100);
            return BitConverter.ToInt32(propertyItem.Value, 0) * 10;
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
                    layer.Move(-newRectangle.X + layer.Position.Xint, -newRectangle.Y + layer.Position.Yint);
                }
                //frame.AddSpace(-newRectangle.Y, newRectangle.Right - frame.Width, newRectangle.Bottom - frame.Height, -newRectangle.X);
            }
            Width = newRectangle.Width;
            Height = newRectangle.Height;
            frameWithCropLayer.RemoveLayer(cl);
        }
        public void AddGifferAsLayers(Giffer newGiffer)
        {
            int nextLayerID = NextLayerID();
            for(int i = 0; i < Frames.Count; i++)
            {
                int newGifferIndex = (int)(i / (double)Frames.Count * newGiffer.Frames.Count);
                GifFrame cgf = Frames[i];
                cgf.AddLayer(new BitmapGFL(newGiffer.FrameAsBitmap(newGifferIndex, false), nextLayerID));
            }
        }
        public Bitmap FrameAsBitmap(GifFrame frame, bool drawHelp)
        {
            return frame.CompleteBitmap(Width, Height, drawHelp);
        }
        public Bitmap FrameAsBitmap(int frameIndex, bool drawHelp)
        {
            return Frames[frameIndex].CompleteBitmap(Width, Height, drawHelp);
        }
        public int NextLayerID()
        {
            return _nextLayerID++;
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
                    originalGif?.Dispose();
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
