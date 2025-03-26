using AnimatedGif;
using BIUK9000.Dithering;
using BIUK9000.GifferComponents.GFLVariants;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        private bool _disposed;
        private int _nextLayerID;
        public int Width { get; set; }
        public int Height { get; set; }
        public OVector Position { get; set; }
        public int FrameCount { get =>  Frames.Count; }

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
        public void MakeSizeDivisible4()
        {
            Width -= Width % 4;
            Height -= Height % 4;
        }
        //public Giffer(string path)
        //{
        //    _nextLayerID = 0;
        //    try
        //    {
        //        using Image gif = Image.FromFile(path);
        //        Frames = FramesFromGif(gif);
        //        //OriginalImagePath = path;
        //        Width = gif.Width;
        //        Height = gif.Height;
        //        Position = new OVector(0, 0);
        //    } catch(Exception ex)
        //    {
        //        throw new ArgumentException(path + " is not an image file", ex);
        //    }
        //}
        public Giffer(string[] paths)
        {
            int lid = NextLayerID();
            Frames = new List<GifFrame>();
            if(paths.Length > 1)
            {
                for (int i = 0; i < paths.Length; i++)
                {
                    try
                    {
                        using Bitmap bmp = new Bitmap(paths[i]);
                        Frames.Add(new GifFrame(bmp, 50, lid));
                    }
                    catch(Exception ex)
                    {
                        Frames.ForEach(frame => frame.Dispose());
                        throw new ArgumentException(paths[i] + " is not an image file", ex);
                    }
                }
            } else
            {
                try
                {
                    using Image gif = Image.FromFile(paths[0]);
                    Frames = FramesFromGif(gif);
                } catch(Exception ex)
                {
                    throw new ArgumentException(paths[0] + " is not an image file", ex);
                }
            }
            GFL fl = Frames[0].Layers[0];
            Width = fl.Width;
            Height = fl.Height;
            Position = new OVector(0, 0);
            MakeSizeDivisible4();
        }
        public Giffer(List<Bitmap> bitmapList, int fps)
        {
            Frames = new List<GifFrame>();
            int id = NextLayerID();
            foreach (Bitmap bmp in bitmapList)
            {
                Frames.Add(new GifFrame(bmp, 1000 / fps, id));
            }
            GFL fl = Frames[0].Layers[0];
            Width = fl.Width;
            Height = fl.Height;
            Position = new OVector(0, 0);
            MakeSizeDivisible4();
        }
        private List<GifFrame> FramesFromGif(Image gif)
        {
            List<GifFrame> result = new();
            int frameCount = ImageFrameCount(gif);
            int firstLayerID = NextLayerID();
            if(frameCount == 1)
            {
                using Bitmap bitmap = new Bitmap(gif);
                result.Add(new GifFrame(bitmap, 20, firstLayerID));
                return result;
            }
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                using Bitmap bitmap = new Bitmap(gif);
                result.Add(new GifFrame(bitmap, FrameDelay(gif), firstLayerID));
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
        }
        public void RemoveFrame(GifFrame frame)
        {
            Frames.Remove(frame);
            frame.Dispose();
        }

        public Bitmap FrameAsBitmap(GifFrame frame, bool drawHelp, InterpolationMode interpolationMode)
        {
            return frame.CompleteBitmap(Width, Height, drawHelp, interpolationMode);
        }
        public void DrawFrame(GifFrame frame, bool drawHelp, Graphics g)
        {
            frame.DrawCompleteBitmap(Width, Height, drawHelp, g);
        }
        public Bitmap FrameAsBitmap(int frameIndex, bool drawHelp, InterpolationMode interpolationMode)
        {
            return Frames[frameIndex].CompleteBitmap(Width, Height, drawHelp, interpolationMode);
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
            _disposed = true;
        }
        public double AverageFramerate()
        {
            double totalDelay = 0;
            foreach(GifFrame frame in Frames)
            {
                totalDelay += frame.FrameDelay;
            }
            return Math.Round(FrameCount / totalDelay * 1000, 2); 
        }
    }
}
