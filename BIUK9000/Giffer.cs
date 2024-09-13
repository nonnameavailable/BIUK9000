﻿using AnimatedGif;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    internal class Giffer
    {
        public List<Bitmap> Frames {  get; set; }
        private Image originalGif;

        public Giffer(string path)
        {
            Image gif = Image.FromFile(path);
            originalGif = gif;
            Frames = FramesFromGif(gif);
        }

        private List<Bitmap> FramesFromGif(Image gif)
        {
            List<Bitmap> result = new();

            int frameCount = gif.GetFrameCount(FrameDimension.Time);

            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new Bitmap(gif));
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
            foreach(Bitmap frame in Frames)
            {
                agc.AddFrame(frame, frameDelay, GifQuality.Bit8);
            }
            return Image.FromStream(stream);
        }
    }
}
