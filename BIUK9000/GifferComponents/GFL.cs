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
        public int LayerID {  get; set; }
        public float Saturation {  get; set; }
        public float Brightness { get; set; }
        public float Transparency { get; set; }
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

                using Bitmap morphedBitmap = MorphedBitmap(g.InterpolationMode);
                g.DrawImage(morphedBitmap, new Rectangle(-morphedBitmap.Width / 2, -morphedBitmap.Height / 2, morphedBitmap.Width, morphedBitmap.Height), 0, 0, morphedBitmap.Width, morphedBitmap.Height, GraphicsUnit.Pixel, LayerImageAttributes());
                if (drawHelp)
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
            Saturation = layer.Saturation;
            Brightness = layer.Brightness;
            Transparency = layer.Transparency;
        }
        public abstract GFL Clone();
        public virtual void CopyDifferingParams(GFL ogState, GFL newState)
        {
            if (!ogState.Position.Equals(newState.Position)) Position = newState.Position;
            if(ogState.Rotation != newState.Rotation) Rotation = newState.Rotation;
            if (ogState.Width != newState.Width) Width = newState.Width;
            if(ogState.Height != newState.Height) Height = newState.Height;
            if(ogState.Visible != newState.Visible) Visible = newState.Visible;
            if(ogState.Saturation != newState.Saturation) Saturation = newState.Saturation;
            if(ogState.Brightness != newState.Brightness) Brightness = newState.Brightness;
            if(ogState.Transparency != newState.Transparency) Transparency = newState.Transparency;
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
            Saturation = Lerper.Lerp(start.Saturation, end.Saturation, distance);
            Brightness = Lerper.Lerp(start.Brightness, end.Brightness, distance);
            Transparency = Lerper.Lerp(start.Transparency, end.Transparency, distance);
        }
        private ImageAttributes LayerImageAttributes()
        {
            //adapted from https://stackoverflow.com/a/14384449/9852011
            // Luminance vector for linear RGB
            const float rwgt = 0.3086f;
            const float gwgt = 0.6094f;
            const float bwgt = 0.0820f;

            // Create a new color matrix
            ColorMatrix colorMatrix = new ColorMatrix();

            // Adjust saturation
            float baseSat = 1.0f - Saturation;
            colorMatrix[0, 0] = baseSat * rwgt + Saturation;
            colorMatrix[0, 1] = baseSat * rwgt;
            colorMatrix[0, 2] = baseSat * rwgt;
            colorMatrix[1, 0] = baseSat * gwgt;
            colorMatrix[1, 1] = baseSat * gwgt + Saturation;
            colorMatrix[1, 2] = baseSat * gwgt;
            colorMatrix[2, 0] = baseSat * bwgt;
            colorMatrix[2, 1] = baseSat * bwgt;
            colorMatrix[2, 2] = baseSat * bwgt + Saturation;

            // Adjust brightness
            float adjustedBrightness = Brightness - 1f;
            colorMatrix[4, 0] = adjustedBrightness;
            colorMatrix[4, 1] = adjustedBrightness;
            colorMatrix[4, 2] = adjustedBrightness;

            //colorMatrix.Matrix33 = transparency;
            colorMatrix[3, 3] = Transparency;
            // Create image attributes
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            // Draw the image with the new color matrix
            return imageAttributes;
        }
    }
}
