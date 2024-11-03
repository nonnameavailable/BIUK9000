using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.Dithering
{
    public class KMeans
    {
        public static List<Color> Palette(Bitmap bmp, int colorCount)
        {
            FastBitmap fbm = new FastBitmap(bmp);
            List<int> result = new List<int>();
            for(int i = 0; i < colorCount; i++)
            {
                result.Add(NextCentroid(fbm, result));
            }
            fbm.Dispose();
            return result.Select(c => Color.FromArgb(c)).ToList();
        }
        public static List<Color> Palette(Giffer giffer, int colorCount)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < colorCount; i++)
            {
                List<int> candidates = new();
                foreach(GifFrame frame in giffer.Frames)
                {
                    FastBitmap fbm = new FastBitmap(frame.CompleteBitmap(false));
                    candidates.Add(NextCentroid(fbm, result));
                    fbm.Dispose();
                }
                result.Add(FurthestCentroidFromList(candidates, result));
            }
            return result.Select(c => Color.FromArgb(c)).ToList();
        }
        private static int FurthestCentroidFromList(List<int> candidates, List<int> palette)
        {
            if(palette.Count == 0) return candidates[new Random().Next(0, candidates.Count)];
            int result = candidates[0];
            double maxDist = 0;
            foreach(int candidate in candidates)
            {
                foreach(int paletteColor in palette)
                {
                    double dist = ColorDistance(candidate, paletteColor);
                    if(dist > maxDist)
                    {
                        maxDist = dist;
                        result = candidate;
                    }
                }
            }
            return result;
        }
        private static int NextCentroid(FastBitmap fbm, List<int> palette)
        {
            Random rnd = new Random();
            if(palette.Count == 0)
            {
                int x = rnd.Next(fbm.Width);
                int y = rnd.Next(fbm.Height);
                return fbm.GetPixelAsInt(x, y);
            } else
            {
                double[] distances = Distances(fbm, palette);
                int wri = WeightedRandomIndex(distances);
                int newX = wri % fbm.Width;
                int newY = (int)(wri / fbm.Width);
                return fbm.GetPixelAsInt(newX, newY);
            }
        }
        private static int WeightedRandomIndex(double[] values)
        {
            // Calculate the cumulative distribution array
            double[] cumulative = new double[values.Length];
            cumulative[0] = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                cumulative[i] = cumulative[i - 1] + values[i];
            }

            // Generate a random number between 0 and the total weight
            Random rand = new Random();
            double randomValue = rand.NextDouble() * cumulative[cumulative.Length - 1];

            // Use binary search to find the index
            int index = Array.BinarySearch(cumulative, randomValue);
            if (index < 0)
            {
                // If not found, BinarySearch returns the bitwise complement of the next element
                index = ~index;
            }

            return index;
        }
        //private static int WeightedRandomIndex(double[] values)
        //{
        //    // Calculate the total weight
        //    double totalWeight = values.Sum();

        //    // Generate a random number between 0 and the total weight
        //    Random rand = new Random();
        //    double randomValue = rand.NextDouble() * totalWeight;

        //    // Find the index corresponding to the random number
        //    double cumulativeWeight = 0.0;
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        cumulativeWeight += values[i];
        //        if (randomValue < cumulativeWeight)
        //        {
        //            return i;
        //        }
        //    }

        //    // In case of rounding errors, return the last index
        //    return values.Length - 1;
        //}
        private static double[] Distances(FastBitmap fbm, List<int> palette)
        {
            double[] result = new double[fbm.Width * fbm.Height];
            for(int i = 0; i < fbm.Width; i++)
            {
                for(int j = 0; j < fbm.Height; j++)
                {
                    int cc = fbm.GetPixelAsInt(i, j);
                    result[j * fbm.Width + i] = Math.Pow(ColorDistance(cc, ClosestPaletteColor(cc, palette)), 2);
                }
            }
            return result;
        }
        private static int ClosestPaletteColor(int color, List<int> palette)
        {
            double minDist = 100000;
            int result = palette[0];
            foreach (int d in palette)
            {
                double newDist = ColorDistance(d, color);
                if (newDist < minDist)
                {
                    minDist = newDist;
                    result = d;
                }
            }
            return result;
        }
        private static double ColorDistance(int c1, int c2)
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
