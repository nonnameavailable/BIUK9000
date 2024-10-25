using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.GifferComponents
{
    public class CropGFL : GFL
    {

        public override Bitmap MorphedBitmap()
        {
            Bitmap result = new Bitmap(Width, Height);
            using Graphics g = Graphics.FromImage(result);
            g.Clear(Color.FromArgb(120, 120, 0, 120));
            return result;
        }
        public CropGFL(int width, int height)
        {
            Position = new Point(0, 0);
            Width = width;
            Height = height;
            Visible = true;
        }
    }
}
