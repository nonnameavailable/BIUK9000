using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BIUK9000
{
    public class ScreenCapture : IDisposable
    {
        public List<Bitmap> Frames {  get; set; }
        private Timer _timer;
        private Bitmap _bitmap;
        private Graphics _graphics;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FPS {  get; set; }
        public ScreenCapture()
        {
            Frames = new List<Bitmap>();
            _timer = new Timer();
            _timer.Tick += _timer_Tick;
        }
        public void StartCapture(int x, int y, int width, int height, int fps)
        {
            DisposeFrames();
            X = x;
            Y = y;
            Width = width;
            Height = height;
            FPS = fps;

            _bitmap = new Bitmap(Width, Height);
            _graphics = Graphics.FromImage(_bitmap);

            _timer.Interval = 1000/fps;
            _timer.Start();
        }
        public static Bitmap CaptureSingleFrame(int x, int y, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(result);
            g.CopyFromScreen(x, y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);
            return result;
        }
        public void StartCapture()
        {
            DisposeFrames();
            _graphics?.Dispose();
            _bitmap?.Dispose();
            _bitmap = new Bitmap(Width, Height);
            _graphics = Graphics.FromImage(_bitmap);
            _timer.Start();
        }

        public void StopCapture()
        {
            _timer.Stop();
        }
        private void _timer_Tick(object sender, EventArgs e)
        {
            _graphics.CopyFromScreen(X, Y, 0, 0, new Size(Width, Height), CopyPixelOperation.SourceCopy);
            Frames.Add(new Bitmap(_bitmap));
        }

        public void Dispose()
        {
            DisposeFrames();
            _graphics.Dispose();
            _bitmap.Dispose();
        }
        public void DisposeFrames()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                Frames[i].Dispose();
            }
            Frames.Clear();
        }
    }
}
