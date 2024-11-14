using AnimatedGif;
using BIUK9000.Dithering;
using BIUK9000.GifferComponents.GFLVariants;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        private Image originalGif;
        private bool _disposed;
        private int _nextLayerID;
        public string OriginalImagePath {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public OVector Position { get; set; }
        public int FrameCount { get =>  Frames.Count; }

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
            try
            {
                Image gif = Image.FromFile(path);
                originalGif = gif;
                Frames = FramesFromGif(gif);
                OriginalImagePath = path;
                Width = gif.Width;
                Height = gif.Height;
                Position = new OVector(0, 0);
            } catch(Exception ex)
            {
                throw new ArgumentException(path + " is not an image file", ex);
            }

        }

        private List<GifFrame> FramesFromGif(Image gif)
        {
            List<GifFrame> result = new();
            int frameCount = ImageFrameCount(gif);
            int firstLayerID = NextLayerID();
            if(frameCount == 1)
            {
                result.Add(new GifFrame(new Bitmap(gif), 20, firstLayerID));
                return result;
            }
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new GifFrame(new Bitmap(gif), FrameDelay(gif), firstLayerID));
            }
            return result;
        }

        private int FrameDelay(Image img)
        {
            PropertyItem propertyItem = img.GetPropertyItem(0x5100);
            return BitConverter.ToInt32(propertyItem.Value, 0) * 10;
        }
        private static int ImageFrameCount(Image img)
        {
            int frameCount = 1;
            try
            {
                frameCount = img.GetFrameCount(FrameDimension.Time);
            } catch (ExternalException ex)
            {
                Debug.Print(ex.Message);
                frameCount = 1;
            }
            return frameCount;
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
        public void AddGifferAsLayers(Giffer newGiffer, bool spread)
        {
            int nextLayerID = NextLayerID();
            if (newGiffer.FrameCount == 1)
            {
                using Bitmap bmp = newGiffer.FrameAsBitmap(0, false);
                Frames.ForEach(frame => frame.AddLayer(new BitmapGFL(new Bitmap(bmp), nextLayerID)));
                return;
            }
            for (int i = 0; i < FrameCount; i++)
            {
                int newGifferIndex;
                if (spread)
                {
                    newGifferIndex = (int)(i / (double)FrameCount * newGiffer.FrameCount);
                } else
                {
                    newGifferIndex = i % newGiffer.FrameCount;
                }
                GifFrame cgf = Frames[i];
                cgf.AddLayer(new BitmapGFL(newGiffer.FrameAsBitmap(newGifferIndex, false), nextLayerID));
            }
        }
        public void AddGifferAsFrames(Giffer newGiffer, int insertAt)
        {
            int[] layerIDs = new int[newGiffer.Frames[0].Layers.Count];
            for(int i = 0; i < layerIDs.Length; i++)
            {
                layerIDs[i] = NextLayerID();
            }
            foreach(GifFrame frame in newGiffer.Frames)
            {
                for(int i = 0; i < frame.Layers.Count; i++)
                {
                    frame.Layers[i].LayerID = layerIDs[i];
                }
            }
            Frames.InsertRange(insertAt, newGiffer.Frames);
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
        private bool RemoveFrame(int index)
        {
            if(index < 0 || index >= Frames.Count || FrameCount <= 1)
            {
                return false;
            } else
            {
                Frames.RemoveAt(index);
                return true;
            }
        }
        public bool RemoveFrames(List<int> indexes)
        {
            if (indexes.Count == 0 || FrameCount <= 1) return false;

            if(indexes.Count == 1) return RemoveFrame(indexes[0]);

            int firstIndex = Math.Min(indexes[0], indexes[1]);
            int lastIndex = Math.Max(indexes[0], indexes[1]);
            if (lastIndex >= FrameCount || lastIndex - firstIndex + 1 >= FrameCount) return false;
            for(int i = firstIndex; i <= lastIndex; i++)
            {
                GifFrame frameToRemove = Frames[firstIndex];
                RemoveFrame(firstIndex);
                frameToRemove.Dispose();
            }
            return true;
        }
        public void Dispose()
        {
            if (_disposed) return;
            for (int i = 0; i < FrameCount; i++)
            {
                Frames[i].Dispose();
            }
            originalGif?.Dispose();
            _disposed = true;
        }
    }
}
