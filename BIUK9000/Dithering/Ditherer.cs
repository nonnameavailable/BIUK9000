using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnimatedGif.Quantizer;

namespace BIUK9000.Dithering
{
    public class Ditherer
    {
        public FastBitmap Fbm {  get; set; }
        public Ditherer(Bitmap bitmap) 
        {
            Fbm = new FastBitmap(bitmap);
        }

        private double ColorDistance(int c1, int c2)
        {
            // Extract RGB components from the first color
            byte r1 = (byte)((c1 >> 16) & 0xFF);
            byte g1 = (byte)((c1 >> 8) & 0xFF);
            byte b1 = (byte)(c1 & 0xFF);

            // Extract RGB components from the second color
            byte r2 = (byte)((c2 >> 16) & 0xFF);
            byte g2 = (byte)((c2 >> 8) & 0xFF);
            byte b2 = (byte)(c2 & 0xFF);

            // Calculate the Euclidean distance
            double distance = Math.Sqrt(
            Math.Pow(r1 - r2, 2) +
            Math.Pow(g1 - g2, 2) +
            Math.Pow(b1 - b2, 2)
            );

            return distance;
        }
    }
}
