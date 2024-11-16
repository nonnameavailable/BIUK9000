﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BIUK9000.GifferComponents.GFLVariants;

namespace BIUK9000.GifferComponents
{
    public class GifFrame : IDisposable
    {
        private bool _disposed;

        public int FrameDelay {  get; set; }



        public List<GFL> Layers { get; private set; } = new List<GFL>();

        public GifFrame(Bitmap bitmap, int frameDelay, int firstLayerID)
        {
            GFL gfl = new BitmapGFL(bitmap, firstLayerID);
            Layers.Add(gfl);

            FrameDelay = frameDelay;
        }

        public Bitmap CompleteBitmap(int width, int height, bool drawHelp, InterpolationMode interpolationMode)
        {
            int absWidth = Math.Abs(width);
            int absHeight = Math.Abs(height);
            Bitmap result = new Bitmap(absWidth, absHeight);
            using Graphics g = Graphics.FromImage(result);
            g.InterpolationMode = interpolationMode;
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
        public void DrawCompleteBitmap(int width, int height, bool drawHelp, Graphics g)
        {
            int absWidth = Math.Abs(width);
            int absHeight = Math.Abs(height);
            foreach (GFL layer in Layers)
            {
                layer.DrawLayer(g, drawHelp);
            }
            if (drawHelp)
            {
                using Pen boundsPen = new Pen(Color.Red, 2f);
                g.DrawRectangle(boundsPen, 0, 0, absWidth, absHeight);
            }
        }
        public void AddLayer(GFL layer)
        {
            Layers.Add(layer);
        }

        public void RemoveLayer(int index)
        {
            if(index > 0 && index < Layers.Count)
            {
                Layers[index].Dispose();
                Layers.RemoveAt(index);
            }
        }
        public void RemoveLayer(GFL layer)
        {
            Layers.Remove(layer);
            layer.Dispose();
        }

        public void Dispose()
        {
            if(_disposed) return;
            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Dispose();
            }
            _disposed = true;
        }
    }
}
