using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents
{
    public class TextGFL : GFL
    {
        public string Text { get; set; }
        public string FontName { get; set; }
        public Color FontColor { get; set; }
        public Color FontBorderColor { get; set; }
        public float FontBorderWidth { get; set; }
        public float FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                if(value > 1 && value < 300)
                {
                    _fontSize = value;
                }
            }
        }
        private float _fontSize;
        public override void CopyParameters(GFL layer)
        {
            if (layer.IsTextLayer)
            {
                TextGFL tl = (TextGFL)layer;
                base.CopyParameters(layer);
                Text = tl.Text;
                FontName = tl.FontName;
                FontColor = tl.FontColor;
                FontBorderColor = tl.FontBorderColor;
                FontBorderWidth = tl.FontBorderWidth;
                FontSize = tl.FontSize;
            }

        }
        public Size TextSize()
        {
            using Font font = new Font(FontName, FontSize);
            return TextRenderer.MeasureText(Text, font);
        }

        public override Point Center()
        {
            SizeF textSize = TextSize();
            return new Point((int)(Position.X + textSize.Width / 2), (int)(Position.Y + textSize.Height / 2));
        }
        public override Rectangle BoundingRectangle
        {
            get
            {
                Size size = TextSize();
                return new Rectangle(Position.X, Position.Y, size.Width, size.Height);
            }
        }

        public override Bitmap MorphedBitmap()
        {
            Size ts = TextSize();
            Bitmap bitmap = new Bitmap(Math.Max(ts.Width, 1), Math.Max(ts.Height, 1));
            using Graphics g = Graphics.FromImage(bitmap);

            float scaledFs = FontSize * g.DpiY / 72f; //FUCKING FINALLY FUCK THIS FUCKING SHIT WHY??
            // Font and brush for the text
            using Font font = new Font(FontName, scaledFs);
            using Brush textBrush = new SolidBrush(FontColor);

            // Pen for the border
            using Pen borderPen = new Pen(FontBorderColor, FontBorderWidth);
            borderPen.LineJoin = LineJoin.Round; // Smooth the corners

            using GraphicsPath path = new GraphicsPath();
            path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, new Point(0, 0), StringFormat.GenericDefault);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            // Draw the border
            g.DrawPath(borderPen, path);
            // Fill the text
            g.FillPath(textBrush, path);

            return bitmap;
        }

        public TextGFL(string text)
        {
            Text = text;
            Position = new Point(0, 0);
            Visible = true;
            Rotation = 0;
            IsTextLayer = true;
        }
        public TextGFL(TextGFL textGFL)
        {
            CopyParameters(textGFL);
        }

    }
}
