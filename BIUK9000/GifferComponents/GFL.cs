using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public abstract class GFL : IDisposable
    {
        protected bool _disposed;
        public OVector Position { get; set; }
        protected int _width, _height;
        public int LayerID {  get; set; }
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = Math.Max(1, value);
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = Math.Max(1, value);
            }
        }
        public GFL (int layerID)
        {
            LayerID = layerID;
            Position = new OVector(0, 0);
            Visible = true;
            Width = 50;
            Height = 50;
            _rotation = 0;
            _previousRotation = 0;
            _spinCount = 0;
        }
        public bool Visible { get; set; }
        //public float Rotation
        //{
        //    get
        //    {
        //        return _rotation + _spinCount * 360;
        //    }
        //    set
        //    {
        //        _rotation = value;
        //        float dif = _rotation - _previousRotation;
        //        if(dif > 180)
        //        {
        //            _spinCount--;
        //            _rotation += 360;
        //        }
        //        else if (dif < -180)
        //        {
        //            _spinCount++;
        //            _rotation -= 360;
        //        }
        //        _previousRotation = value;
        //    }
        //}
        public float Rotation
        {
            get
            {
                return _rotation + _spinCount * 360;
            }
            set
            {
                _rotation = value;
                float dif = _rotation - _previousRotation;
                if (dif > 180)
                {
                    _spinCount--;
                    _rotation += 360;
                }
                else if (dif < -180)
                {
                    _spinCount++;
                    _rotation -= 360;
                }
                _previousRotation = value;
            }
        }
        protected float _rotation;
        protected float _previousRotation;
        protected int _spinCount;
        public abstract Bitmap MorphedBitmap();
        protected Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = BoundingRectangle;
        }
        public void ResetSpins()
        {
            _spinCount = 0;
            while(_rotation > 180)
            {
                _rotation -= 360;
            }
            while(_rotation < -180)
            {
                _rotation += 360;
            }
            Debug.Print("reset spins");
        }
        public virtual void Resize(int sizeDif)
        {
            double aspect = (double)OBR.Width / OBR.Height;
            Position = new OVector(OBR.X - sizeDif, (int)(OBR.Y - sizeDif / aspect));
            Width = OBR.Width + sizeDif * 2;
            Height = (int)((OBR.Width + sizeDif * 2) / aspect);
        }
        public virtual void Resize(int xSizeDif, int ySizeDif)
        {
            Position = new OVector(OBR.X - xSizeDif, OBR.Y - ySizeDif);
            Width = OBR.Width + xSizeDif * 2;
            Height = OBR.Height + ySizeDif * 2;
        }
        public virtual OVector Center()
        {
            return new OVector(Position.X + Width / 2, Position.Y + Height / 2);
        }
        public virtual OVector AbsoluteCenter()
        {
            return new OVector(Width / 2d, Height / 2d);
        }
        public void DrawLayer(Graphics g, bool drawHelp)
        {
            if (Visible)
            {
                GraphicsState gs = g.Save();

                OVector c = Center();
                g.TranslateTransform(c.Xint, c.Yint);
                g.RotateTransform(Rotation);

                Bitmap morphedBitmap = MorphedBitmap();
                g.DrawImage(morphedBitmap, -morphedBitmap.Width / 2, -morphedBitmap.Height / 2);

                if(drawHelp)
                {
                    using Pen boundsPen = new Pen(Color.Red, 2f);
                    g.DrawRectangle(boundsPen, -morphedBitmap.Width / 2, -morphedBitmap.Height / 2, morphedBitmap.Width, morphedBitmap.Height);
                }

                g.Restore(gs);
            }
        }
        public virtual Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(Position.Xint, Position.Yint, Width, Height);
            }
        }
        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Move(int x, int y)
        {
            Position = new OVector(Position.X + x, Position.Y + y);
        }
        public void MoveFromOBR(int x, int y)
        {
            //this method sets the position depending on the saved bounding rectangle
            int newX = x + OBR.X;
            int newY = y + OBR.Y;
            Position = new OVector(newX, newY);
        }

        public virtual void Dispose()
        {
            _disposed = true;
        }
        public virtual void CopyParameters(GFL layer)
        {
            Width = layer.Width;
            Height = layer.Height;
            Position = layer.Position;
            Rotation = layer.Rotation;
        }
        public abstract GFL Clone();
        public virtual void CopyDifferingParams(GFL ogState, GFL newState)
        {
            if (!ogState.Position.Equals(newState.Position)) Position = newState.Position;
            if(ogState.Rotation != newState.Rotation) Rotation = newState.Rotation;
            if(ogState.Width != newState.Width) Width = newState.Width;
            if(ogState.Height != newState.Height) Height = newState.Height;
            if(ogState.Visible != newState.Visible) Visible = newState.Visible;
        }
        public virtual void Lerp(GFL start, GFL end, double distance, OVector position = null)
        {
            if(position == null)
            {
                Position = OVector.Lerp(start.Position, end.Position, distance);
            }
            else
            {
                 Position = position;
            }
            Width = Lerper.Lerp(start.Width, end.Width, distance);
            Height = Lerper.Lerp(start.Height, end.Height, distance);
            Rotation = Lerper.Lerp(start.Rotation, end.Rotation, distance);
        }
    }
}
