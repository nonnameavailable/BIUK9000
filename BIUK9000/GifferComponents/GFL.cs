using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        protected double _xMult, _yMult;
        public int LayerID {  get; set; }
        public float Saturation {  get; set; }
        public float Brightness { get; set; }
        public float Transparency { get; set; }
        public float Hue { get; set; }
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
            Rotation = 0;
            Saturation = 1;
            Brightness = 1;
            Transparency = 1;
            _xMult = 0.5;
            _yMult = 0.5;
            Hue = 0;
        }
        public bool Visible { get; set; }
        public float Rotation { get; set; }

        public abstract Bitmap MorphedBitmap(InterpolationMode interpolationMode);
        protected Rectangle OBR { get; set; }
        public virtual void Save()
        {
            OBR = BoundingRectangle;
        }
        public virtual void Resize(int sizeDif)
        {
            sizeDif *= 2;
            double aspect = (double)OBR.Width / OBR.Height;
            double xDif = sizeDif;
            double yDif = sizeDif / aspect;
            double xLeft = xDif * _xMult;
            double yLeft = yDif * _yMult;
            Position = new OVector(OBR.X - xLeft, (int)(OBR.Y - yLeft));
            Width = (int)(OBR.Width + xDif);
            Height = (int)(OBR.Height + yDif);
        }
        public virtual void Resize(int xSizeDif, int ySizeDif)
        {
            Position = new OVector(OBR.X - xSizeDif * _xMult, OBR.Y - ySizeDif * _yMult);
            Width = OBR.Width + xSizeDif;
            Height = OBR.Height + ySizeDif;
        }
        public virtual OVector Center()
        {
            return new OVector(Position.X + Width * _xMult, Position.Y + Height * _yMult);
        }
        public virtual OVector LTCorner()
        {
            OVector c = AbsoluteCenter();
            c.Multiply(-1);
            c.Rotate(Rotation);
            return Center().Add(c);
        }
        public virtual void OverrideCenter(double xMult, double yMult)
        {
            OVector ltc = LTCorner();
            _xMult = xMult;
            _yMult = yMult;
            OVector nltc = LTCorner();
            OVector dif = nltc.Subtract(ltc);
            Position.Subtract(dif);
        }
        public virtual OVector AbsoluteCenter()
        {
            return new OVector(Width * _xMult, Height * _yMult);
        }
        public void DrawLayer(Graphics g, bool drawHelp)
        {
            if (Visible)
            {
                GraphicsState gs = g.Save();

                //OVector c = Center();
                OVector c = Center();
                g.TranslateTransform(c.Xint, c.Yint);
                g.RotateTransform(Rotation);

                using Bitmap morphedBitmap = MorphedBitmap(g.InterpolationMode);
                ImageAttributes ia = HSBTAdjuster.HSBTAdjustedImageAttributes(Hue, Saturation, Brightness, Transparency);
                //g.DrawImage(morphedBitmap, new Rectangle(-morphedBitmap.Width / 2, -morphedBitmap.Height / 2, morphedBitmap.Width, morphedBitmap.Height), 0, 0, morphedBitmap.Width, morphedBitmap.Height, GraphicsUnit.Pixel, ia);
                g.DrawImage(morphedBitmap, new Rectangle((int)(-morphedBitmap.Width * _xMult), (int)(-morphedBitmap.Height * _yMult), morphedBitmap.Width, morphedBitmap.Height), 0, 0, morphedBitmap.Width, morphedBitmap.Height, GraphicsUnit.Pixel, ia);
                if (drawHelp)
                {
                    using Pen boundsPen = new Pen(Color.Red, 2f);
                    //g.DrawRectangle(boundsPen, -morphedBitmap.Width / 2, -morphedBitmap.Height / 2, morphedBitmap.Width, morphedBitmap.Height);
                    g.DrawRectangle(boundsPen, (int)(-morphedBitmap.Width * _xMult), (int)(-morphedBitmap.Height * _yMult), morphedBitmap.Width, morphedBitmap.Height);
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
            Position = layer.Position.Copy();
            Rotation = layer.Rotation;
            Saturation = layer.Saturation;
            Brightness = layer.Brightness;
            Transparency = layer.Transparency;
            Hue = layer.Hue;
            _xMult = layer._xMult;
            _yMult = layer._yMult;
            Visible = layer.Visible;
        }
        public abstract GFL Clone();
        public virtual void CopyDifferingParams(GFL ogState, GFL newState)
        {
            if (!ogState.Position.Equals(newState.Position)) Position = newState.Position.Copy();
            if(ogState.Rotation != newState.Rotation) Rotation = newState.Rotation;
            if (ogState.Width != newState.Width) Width = newState.Width;
            if(ogState.Height != newState.Height) Height = newState.Height;
            if(ogState.Visible != newState.Visible) Visible = newState.Visible;
            if(ogState.Saturation != newState.Saturation) Saturation = newState.Saturation;
            if(ogState.Brightness != newState.Brightness) Brightness = newState.Brightness;
            if(ogState.Transparency != newState.Transparency) Transparency = newState.Transparency;
            if (ogState.Hue != newState.Hue) Hue = newState.Hue;
            if(ogState._xMult != newState._xMult) _xMult = newState._xMult;
            if(ogState._yMult != newState._yMult) _yMult = newState._yMult;
        }
        public virtual void Lerp(GFL start, GFL end, double distance, OVector position = null)
        {
            if(position == null)
            {
                Position = OVector.Lerp(start.Position, end.Position, distance);
            }
            else
            {
                 Position = position.Copy();
            }
            Width = Lerper.Lerp(start.Width, end.Width, distance);
            Height = Lerper.Lerp(start.Height, end.Height, distance);
            Rotation = Lerper.Lerp(start.Rotation, end.Rotation, distance);
            Saturation = Lerper.Lerp(start.Saturation, end.Saturation, distance);
            Brightness = Lerper.Lerp(start.Brightness, end.Brightness, distance);
            Transparency = Lerper.Lerp(start.Transparency, end.Transparency, distance);
            Hue = Lerper.Lerp(start.Hue, end.Hue, distance);
        }
    }
}
