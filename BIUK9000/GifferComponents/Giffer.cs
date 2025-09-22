using BIUK9000.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using BIUK9000.GifferComponents.GFLVariants;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        public int SFI { get; set; }
        public int SLI { get; set; }
        private bool _disposed;
        private int _nextLayerID;
        private int _width, _height;
        public int Width 
        {
            get { return _width; }
            set { _width = Math.Max(value, 8); }
        }
        public int Height 
        {
            get { return _height; }
            set { _height = Math.Max(value, 8);}
        }
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
        public string SoundPath { get; set; }
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
            if(Width >= 8)
            {
                Width -= Width % 4;
            } else
            {
                Width = 8;
            }
            if(Height >= 8)
            {
                Height -= Height % 4;
            }else
            {
                Height = 8;
            }
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
            SFI = 0;
            SLI = 0;
            SoundPath = "";
            MakeSizeDivisible4();
        }
        private Giffer(Giffer gifferToCopy)
        {
            Width = gifferToCopy.Width;
            Height = gifferToCopy.Height;
            Position = new OVector(0, 0);
            SFI = gifferToCopy.SFI;
            SLI = gifferToCopy.SLI;
            _nextLayerID = gifferToCopy._nextLayerID;
            Frames = new();
            SoundPath = gifferToCopy.SoundPath;
            foreach(GifFrame gf in gifferToCopy.Frames)
            {
                Frames.Add(gf.Clone());
            }
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
        public Giffer Clone()
        {
            return new Giffer(this);
        }
        public void ReduceSize(int maxSideLength)
        {
            int largerSide = Math.Max(Width, Height);
            if (largerSide <= maxSideLength) return;
            double multiplier = maxSideLength / (double)largerSide;
            Width = (int)(Width * multiplier);
            Height = (int)(Height * multiplier);
            foreach (GifFrame gf in Frames)
            {
                GFL gfl = gf.Layers[0];
                gfl.Width = Width;
                gfl.Height = Height;
                if(gfl is BitmapGFL bgfl)
                {
                    using Bitmap newBitmap = new Bitmap(bgfl.OriginalBitmap, Width, Height);
                    bgfl.ReplaceOriginalBitmap(newBitmap);
                }
            }
        }
    }
}
