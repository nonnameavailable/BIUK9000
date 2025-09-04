using AnimatedGif;
using BIUK9000.GifferComponents;
using BIUK9000.MyGraphics.Dithering;
using BIUK9000.UI.CustomControls;
using GifskiNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.IO
{
    public static class BMPToGifConvertor
    {
        public static Image GifGifski(List<Bitmap> bitmaps, List<int> frameDelays, double frameStep, byte quality)
        {
            using MemoryStream stream = new MemoryStream();
            using var gifski = Gifski.Create(@"resources\gifski.dll", settings =>
            {
                settings.Quality = quality;
            });
            gifski.SetStreamOutput(stream);
            int counter = 0;
            double delayCumulation = 0;
            for (double k = 0; k < bitmaps.Count; k += frameStep)
            {
                delayCumulation += frameDelays[(int)k] / 1000d * frameStep;
                Bitmap frame = bitmaps[(int)k];
                using FastBitmap fbm = new FastBitmap(frame);
                for (int i = 0; i < fbm.Width; i++)
                {
                    for (int j = 0; j < fbm.Height; j++)
                    {
                        Color c = fbm.GetPixel(i, j);
                        fbm.SetPixel(i, j, Color.FromArgb(c.A, c.B, c.G, c.R));
                    }
                }
                byte[] argb = ImageToByte(fbm.Bitmap);
                gifski.AddFrameRgba((uint)counter, delayCumulation, (uint)frame.Width, (uint)frame.Height, argb);
                counter++;
            }
            gifski.Finish();
            return Image.FromStream(stream);
        }
        public static Image GifAnimatedGif(List<Bitmap> bitmaps, List<int> frameDelays, double frameStep, GifQuality quality)
        {
            using MemoryStream stream = new MemoryStream();
            using AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            for (double i = 0; i < bitmaps.Count; i += frameStep)
            {
                agc.AddFrame(bitmaps[(int)i], (int)(frameDelays[(int)i] * frameStep), quality);
            }
            return Image.FromStream(stream);
        }

        private static byte[] ImageToByte(Bitmap bitmap)
        {
            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                nint ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
        }
    }
}
