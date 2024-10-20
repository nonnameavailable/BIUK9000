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
        public string Font { get; set; }
        public Color FontColor { get; set; }
        public Color FontBorderColor { get; set; }
        public float FontBorderWidth { get; set; }
        public float FontSize { get; set; }
        public Size TextSize()
        {
            using Font font = new Font(Font, FontSize);
            return TextRenderer.MeasureText(Text, font);
        }

        public override Point Center()
        {
            SizeF textSize = TextSize();
            return new Point((int)(Position.X + textSize.Width / 2), (int)(Position.Y + textSize.Height / 2));
        }

        public override Bitmap MorphedBitmap
        {
            get
            {
                return new Bitmap(50, 50);
            }
        }

        public TextGFL(string text)
        {
            Text = text;
            Position = new Point(0, 0);
            Visible = true;
            Rotation = 0;
        }
        public override void DrawLayer(Graphics g)
        {

            if (Visible)
            {
                GraphicsState gs = g.Save();

                SizeF ts = TextSize();
                Point c = Center();
                float scaledFs = FontSize * g.DpiY / 72f; //FUCKING FINALLY FUCK THIS FUCKING SHIT WHY??
                g.TranslateTransform(c.X, c.Y);
                g.RotateTransform(Rotation);
                g.DrawRectangle(Pens.Red, -ts.Width / 2, -ts.Height / 2, ts.Width, ts.Height);
                // Font and brush for the text
                using Font font = new Font(Font, scaledFs);
                using Brush textBrush = new SolidBrush(FontColor);

                // Pen for the border
                using Pen borderPen = new Pen(FontBorderColor, FontBorderWidth);
                borderPen.LineJoin = LineJoin.Round; // Smooth the corners
                // Create a GraphicsPath
                using GraphicsPath path = new GraphicsPath();
                path.AddString(Text, font.FontFamily, (int)font.Style, font.Size, new Point((int)(-ts.Width / 2), (int)(-ts.Height / 2)), StringFormat.GenericDefault);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                // Draw the border
                g.DrawPath(borderPen, path);
                // Fill the text
                g.FillPath(textBrush, path);
                g.Restore(gs);
            }
        }
    }
}
