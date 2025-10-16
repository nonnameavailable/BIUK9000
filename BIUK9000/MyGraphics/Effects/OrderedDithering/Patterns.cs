using BIUK9000.MyGraphics.Dithering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.MyGraphics.Effects.OrderedDithering
{
    public class Patterns
    {
        public static int[][] DitherPatterns()
        {
            int[][] result = new int[][]
            {
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0],
            [1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0],
            [1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0],
            [1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0],
            [1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1],
            [1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1],
            [0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0],
            [0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0],
            [0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0],
            [0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1],
            [0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1],
            [0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1],
            [0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1],
            [0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
            [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1]
        };
            return result;
        }

        public static Bitmap PatternBitmap()
        {
            int repeatCount = 4;
            int patternBlockSize = repeatCount * 4;
            int[][] patterns = DitherPatterns();
            int width = patterns.Length * repeatCount * 4;
            int height = repeatCount * 4;
            using FastBitmap fbm = new(width, height);
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    int[] pattern = patterns[i / (repeatCount * patternBlockSize)];
                    int val = patterns[i / patternBlockSize][(i % 4) + (j % 4 * 4)];
                    fbm.SetPixel(i, j, val == 0 ? Color.Black : Color.White);
                }
            }
            return new Bitmap(fbm.Bitmap);
        }
        public static Bitmap DitheredBitmap(Bitmap bmp)
        {
            int[][] patterns = DitherPatterns();
            using FastBitmap fbm = new(bmp);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c = fbm.GetPixel(i, j);
                    if(c.A == 0)
                    {
                        fbm.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                        continue;
                    }
                    int patternIndex = (int)((c.R + c.G + c.B) / 765d * (patterns.Length - 1));
                    int[] pattern = patterns[patternIndex];
                    int val = pattern[(i % 4) + (j % 4 * 4)];
                    fbm.SetPixel(i, j, val == 0 ? Color.Black : Color.White);
                }
            }
            return new Bitmap(fbm.Bitmap);
        }
    }
}
