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
    public class Giffer
    {
        public List<GifFrame> Frames { get; set; }
        private Image originalGif;
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
            OnFrameCountChanged();
        }
    }
}
