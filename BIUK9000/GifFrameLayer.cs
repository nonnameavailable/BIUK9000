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
        public int X { get => _x; set { _x = value;OnParameterChanged(); } }
        public int Y { get => _y; set { _y = value; OnParameterChanged(); } }
        public int Width { get => _width; set { _width = value; OnParameterChanged(); } }
        public int Height { get => _height; set { _height = value; OnParameterChanged(); } }
        public bool Visible { get => _visible; set { _visible = value; OnParameterChanged(); } }

        private int _x, _y, _width, _height;
        private bool _visible;

        private Bitmap _originalBitmap;
        public Bitmap OriginalBitmap { get => _originalBitmap; }
        //public Bitmap ResultBitmap
        //{
        //    get
        //    {
        //        Bitmap result = new Bitmap(Width, Height);
        //        using Graphics g = Graphics.FromImage(result);
        //        DrawLayer(g);
        //        return result;
        //    }
        //}

        public event EventHandler ParameterChanged;
        protected virtual void OnParameterChanged()
        {
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }
        public GifFrameLayer(Bitmap bitmap)
        {
            _originalBitmap = bitmap;
            _x = 0;
            _y = 0;
            _width = bitmap.Width;
            _height = bitmap.Height;
            _visible = true;
        }
        public void DrawLayer(Graphics g)
        {
            if(Visible)g.DrawImage(OriginalBitmap, X, Y, Width, Height);
        }
    }
}
