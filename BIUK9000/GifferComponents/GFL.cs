using System;
using System.Collections.Generic;
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

        public Point Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Visible { get; set; }
        public float Rotation { get; set; }
        //public bool IsTextLayer { get; set; }
        public abstract Bitmap MorphedBitmap();
        protected Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = BoundingRectangle;
        }
        public virtual void Resize(int sizeDif)
        {
            double aspect = (double)OBR.Width / OBR.Height;
            Position = new Point(OBR.X - sizeDif, (int)(OBR.Y - sizeDif / aspect));
            Width = OBR.Width + sizeDif * 2;
            Height = (int)((OBR.Width + sizeDif * 2) / aspect);
        }
        public virtual void Resize(int xSizeDif, int ySizeDif)
        {
            Position = new Point(OBR.X - xSizeDif, OBR.Y - ySizeDif);
            Width = OBR.Width + xSizeDif * 2;
            Height = OBR.Height + ySizeDif * 2;
        }
        public virtual Point Center()
        {
            return new Point(Position.X + Width / 2, Position.Y + Height / 2);
        }
        public void DrawLayer(Graphics g, bool drawHelp)
        {
            if (Visible)
            {
                GraphicsState gs = g.Save();

                Point c = Center();
                g.TranslateTransform(c.X, c.Y);
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
                return new Rectangle(Position.X, Position.Y, Width, Height);
            }
        }
        public void Rotate(float angle)
        {
            Rotation += angle;
        }

        public void Move(int x, int y)
        {
            Position = new Point(Position.X + x, Position.Y + y);
        }
        public void MoveFromOBR(int x, int y)
        {
            //this method sets the position depending on the saved bounding rectangle
            int newX = x + OBR.X;
            int newY = y + OBR.Y;
            Position = new Point(newX, newY);
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
    }
}
