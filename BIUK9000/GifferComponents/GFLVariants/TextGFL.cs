using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents.GFLVariants
{
    public class TextGFL : GFL
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public Color FontColor { get; set; }
        public Color FontBorderColor { get; set; }
        public float FontBorderWidth { get; set; }
        public float _savedFontSize;
        public float FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if (value > 1 && value < 300)
                {
                    _fontSize = value;
                }
            }
        }
        private float _fontSize;
        public override void CopyParameters(GFL layer)
        {
            TextGFL tl = (TextGFL)layer;
            base.CopyParameters(layer);
            Text = tl.Text;
            FontName = tl.FontName;
            FontColor = tl.FontColor;
            FontBorderColor = tl.FontBorderColor;
            FontBorderWidth = tl.FontBorderWidth;
            FontSize = tl.FontSize;
            Visible = tl.Visible;
        }
        public override void CopyDifferingParams(GFL ogState, GFL newState)
        {
            base.CopyDifferingParams(ogState, newState);
            TextGFL otl = (TextGFL)ogState;
            TextGFL ntl = (TextGFL)newState;
            if (otl.Text != ntl.Text) Text = ntl.Text;
            if (otl.FontName != ntl.FontName) FontName = ntl.FontName;
            if (otl.FontColor != ntl.FontColor) FontColor = ntl.FontColor;
            if (otl.FontBorderColor != ntl.FontBorderColor) FontBorderColor = ntl.FontBorderColor;
            if (otl.FontBorderWidth != ntl.FontBorderWidth) FontBorderWidth = ntl.FontBorderWidth;
            if (otl.FontSize != ntl.FontSize) FontSize = ntl.FontSize;
            if (otl.Visible != ntl.Visible) Visible = ntl.Visible;
        }
        public override void Save()
        {
            base.Save();
            _savedFontSize = FontSize;
        }
        public override void Resize(int sizeDif)
        {
            Size bs = TextSize();
            FontSize = _savedFontSize + sizeDif / 5;
            Size s = TextSize();
            Width = s.Width;
            Height = s.Height;
            double xDif = s.Width - bs.Width;
            double yDif = s.Height - bs.Height;
            OVector lDif = new OVector(xDif * _xMult, yDif * _yMult);
            Position = Position.Copy().Subtract(lDif);
        }
        public override void Resize(int xSizeDif, int ySizeDif)
        {
            //DO NOTHING, NOT IMPLEMENTED AND PROBABLY WILL NEVER BE
        }
        public Size TextSize()
        {
            using Font font = new Font(FontName, FontSize);
            return TextRenderer.MeasureText(Text, font);
        }

        public override OVector Center()
        {
            SizeF textSize = TextSize();
            return new OVector(Position.X + textSize.Width * _xMult, Position.Y + textSize.Height * _yMult);
        }
        public override OVector AbsoluteCenter()
        {
            SizeF textSize = TextSize();
            return new OVector(textSize.Width * _xMult, textSize.Height *_yMult);
        }
        public override void OverrideCenter(double xMult, double yMult)
        {
            OVector ltc = LTCorner();
            _xMult = xMult;
            _yMult = yMult;
            OVector nltc = LTCorner();
            OVector dif = nltc.Copy().Subtract(ltc);
            Position = Position.Copy().Subtract(dif);

        }
        public override Rectangle BoundingRectangle
        {
            get
            {
                Size size = TextSize();
                return new Rectangle(Position.Xint, Position.Yint, size.Width, size.Height);
            }
        }

        public override Bitmap MorphedBitmap(InterpolationMode interpolationMode)
        {
            Size ts = TextSize();
            Bitmap bitmap = new Bitmap(Math.Max(ts.Width, 1), Math.Max(ts.Height, 1));
            using Graphics g = Graphics.FromImage(bitmap);

            float scaledFs = FontSize * g.DpiY / 72f; //FUCKING FINALLY FUCK THIS FUCKING SHIT WHY??
            // Font and brush for the text
            using Font font = new Font(FontName, scaledFs);
            using Brush textBrush = new SolidBrush(FontColor);

            using GraphicsPath path = new GraphicsPath();
            path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, new Point(0, 0), StringFormat.GenericDefault);

            //if(interpolationMode != InterpolationMode.NearestNeighbor) g.SmoothingMode = SmoothingMode.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.InterpolationMode = interpolationMode;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw the border
            if (FontBorderWidth > 0)
            {
                using Pen borderPen = new Pen(FontBorderColor, FontBorderWidth);
                borderPen.LineJoin = LineJoin.Round; // Smooth the corners
                g.DrawPath(borderPen, path);
            }

            // Fill the text
            g.FillPath(textBrush, path);

            return bitmap;
        }

        public override GFL Clone()
        {
            TextGFL clone = new TextGFL(LayerID);
            clone.CopyParameters(this);
            return clone;
        }

        public TextGFL(int layerID) : base(layerID)
        {
            Text = "YOUR TEXT";
            FontName = "Impact";
            FontBorderColor = Color.Black;
            FontColor = Color.White;
            FontBorderWidth = 5;
            FontSize = 20;
            Size s = TextSize();
            Width = s.Width;
            Height = s.Height;
        }
        public override void Lerp(GFL start, GFL end, double distance, OVector position = null)
        {
            base.Lerp(start, end, distance, position);
            TextGFL tstart = (TextGFL)start;
            TextGFL tend = (TextGFL)end;
            FontSize = Lerper.Lerp(tstart.FontSize, tend.FontSize, distance);
            FontColor = Lerper.LerpColor(tstart.FontColor, tend.FontColor, distance);
            FontBorderColor = Lerper.LerpColor(tstart.FontBorderColor, tend.FontBorderColor, distance);
            FontBorderWidth = Lerper.Lerp(tstart.FontBorderWidth, tend.FontBorderWidth, distance);
            Text = Lerper.LerpText(tstart.Text, tend.Text, distance);
        }
    }
}
