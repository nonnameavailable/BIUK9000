using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public class GifFrameLayer
    {
        public Point Position { get => _position; set { _position = value; OnParameterChanged(); } }
        public int Width { get => _width; set { _width = value; OnParameterChanged(); } }
        public int Height { get => _height; set { _height = value; OnParameterChanged(); } }
        public bool Visible { get => _visible; set { _visible = value; OnParameterChanged(); } }

        private int _width, _height;
        private bool _visible;
        private Point _position;

        private Bitmap _originalBitmap;
        public Bitmap OriginalBitmap { get => _originalBitmap; }

        public event EventHandler ParameterChanged;
        protected virtual void OnParameterChanged()
        {
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }
        public GifFrameLayer(Bitmap bitmap)
        {
            _originalBitmap = bitmap;
            _position = new Point(0, 0);
            _width = bitmap.Width;
            _height = bitmap.Height;
            _visible = true;
        }
        public void DrawLayer(Graphics g)
        {
            if(Visible)g.DrawImage(OriginalBitmap, Position.X, Position.Y, Width, Height);
        }
    }
}
