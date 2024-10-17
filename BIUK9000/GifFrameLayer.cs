using Emgu.CV;
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
        public float Rotation { get => _rotation; set { _rotation = value; OnParameterChanged(); } }
        public Point Center
        { 
            get
            {
                return new Point(Position.X + Width / 2, Position.Y + Height / 2);
            }
        }

        private int _width, _height;
        private bool _visible;
        private Point _position;
        private float _rotation;

        private Bitmap _originalBitmap;
        public Bitmap OriginalBitmap { get => _originalBitmap; }
        public Bitmap MorphedBitmap
        {
            get
            {
                Bitmap result = new Bitmap(Width, Height);
                using Graphics g = Graphics.FromImage(result);
                DrawLayer(g);
                return result;
            }
        }

        public event EventHandler ParameterChanged;
        protected virtual void OnParameterChanged()
        {
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }
        public GifFrameLayer(Bitmap bitmap)
        {
            Initialize(bitmap);
        }
        public void DrawLayer(Graphics g)
        {
            g.TranslateTransform(Position.X + Width / 2, Position.Y + Height / 2);
            g.RotateTransform(Rotation);
            if(Visible)g.DrawImage(OriginalBitmap, -Width / 2, -Height / 2, Width, Height);
            g.ResetTransform();
        }
        private void Initialize(Bitmap bitmap)
        {
            _originalBitmap = bitmap;
            _position = new Point(0, 0);
            _width = bitmap.Width;
            _height = bitmap.Height;
            _visible = true;
            _rotation = 0;
        }

        public void ReplaceOriginalBitmap(Bitmap bitmap)
        {
            _originalBitmap.Dispose();
            Initialize(bitmap);
        }

        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Move(int x, int y)
        {
            Position = new Point(Position.X + x, Position.Y + y);
        }
    }
}
