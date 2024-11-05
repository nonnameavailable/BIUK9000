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
        private bool disposedValue;
        public OVector Position { get; set; }
        private int _width, _height;

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
            Rotation = 0;
        }
        public bool Visible { get; set; }
        public float Rotation { get; set; }
        public abstract Bitmap MorphedBitmap();
        protected Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = BoundingRectangle;
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
        public void DrawLayer(Graphics g, bool drawHelp)
        {
            if (Visible)
            {
                GraphicsState gs = g.Save();

                OVector c = Center();
                g.TranslateTransform(c.Xint, c.Yint);
                g.RotateTransform(Rotation);

                using Bitmap morphedBitmap = MorphedBitmap();
                g.DrawImage(morphedBitmap, -morphedBitmap.Width / 2, - morphedBitmap.Height / 2);

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
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
    }
}
