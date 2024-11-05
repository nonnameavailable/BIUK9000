using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static AnimatedGif.Quantizer;

namespace BIUK9000.Dithering
{
    public class Ditherer : IDisposable
    {
        private FastBitmap obm {  get; set; }
        public Ditherer(Bitmap bitmap)
        {
            obm = new FastBitmap(bitmap);
            disposeValue = false;
        }

        public Bitmap DitheredBitmap(List<Color> palette)
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<double[]> doublePalette = palette.Select(c => doubleFromColor(c)).ToList();
            double[] doubleBitmap = DoubleBitmap();
            double[] ditheredBitmap = DitheredDoubleBitmap(doubleBitmap, doublePalette);
            for(int j = 0; j < obm.Height; j++)
            {
                for(int i = 0; i < obm.Width; i++)
                {
                    double[] doubleCc = GetDoublePixel(i, j, ditheredBitmap);
                    Color cc = Color.FromArgb((int)(doubleCc[0] * 255), (int)(doubleCc[1] * 255), (int)(doubleCc[2] * 255));
                    obm.SetPixel(i, j, cc);
                }
            }
            Debug.Print("Dithering with " + palette.Count.ToString() + " colors took " + (sw.ElapsedMilliseconds / 1000d).ToString() + " seconds");
            sw.Stop();
            return new Bitmap(obm.Bitmap);
        }

        private double[] DitheredDoubleBitmap(double[] doubleBitmap, List<double[]> palette)
        {
            for(int j = 0; j < obm.Height; j++)
            {
                for(int i = 0; i < obm.Width; i++)
                {
                    double[] currentColor = GetDoublePixel(i, j, doubleBitmap);
                    double[] closestPaletteColor = ClosestPaletteColor(currentColor, palette);

                    //double randomFactor = (new Random().NextDouble() - 0.5) * 0.1;
                    double randomFactor = 0;

                    double rDist = currentColor[0] - closestPaletteColor[0] + randomFactor;
                    double gDist = currentColor[1] - closestPaletteColor[1] + randomFactor;
                    double bDist = currentColor[2] - closestPaletteColor[2] + randomFactor;

                    double[] addPixel = [rDist, gDist, bDist];
                    if(i < obm.Width - 1) AddDoublePixel(MultipliedDoubleArray(addPixel, 7 / 16d), i + 1, j, doubleBitmap, false);
                    if(i > 0 && j < obm.Height - 1) AddDoublePixel(MultipliedDoubleArray(addPixel, 3 / 16d), i - 1, j + 1, doubleBitmap, false);
                    if(j < obm.Height - 1) AddDoublePixel(MultipliedDoubleArray(addPixel, 5 / 16d), i, j + 1, doubleBitmap, false);
                    if (i < obm.Width - 1 && j < obm.Height - 1) AddDoublePixel(MultipliedDoubleArray(addPixel, 1 / 16d), i + 1, j + 1, doubleBitmap, false);

                    SetDoublePixel(closestPaletteColor, i, j, doubleBitmap);
                }
            }
            return doubleBitmap;
        }
        private double[] doubleFromColor(Color color)
        {
            double[] result = new double[3];
            result[0] = color.R / 255d;
            result[1] = color.G / 255d;
            result[2] = color.B / 255d;
            return result;
        }
        private static double[] MultipliedDoubleArray(double[] da, double scalar)
        {
            return da.Select(d =>  d * scalar).ToArray();
        }
        private double[] ClosestPaletteColor(double[] color, List<double[]> palette)
        {
            double minDist = 100000;
            double[] result = palette[0];
            foreach (double[] d in palette)
            {
                double newDist = DoubleColorDistance(d, color);
                if (newDist < minDist)
                {
                    minDist = newDist;
                    result = d;
                }
            }
            return result;
        }
        private double[] DoubleBitmap()
        {
            double[] result = new double[obm.Width * obm.Height * 3];
            for(int j = 0; j < obm.Height; j++)
            {
                for(int i = 0; i < obm.Width; i++)
                {
                    Color cc = obm.GetPixel(i, j);
                    int index = (j * obm.Width + i) * 3;
                    result[index] = cc.R / 255d;
                    result[index + 1] = cc.G / 255d;
                    result[index + 2] = cc.B / 255d;
                }
            }
            return result;
        }

        private double[] GetDoublePixel(int x, int y, double[] doubleBitmap)
        {
            double[] result = new double[3];
            int index = (y * obm.Width + x) * 3;
            result[0] = doubleBitmap[index];
            result[1] = doubleBitmap[index + 1];
            result[2] = doubleBitmap[index + 2];
            return result;
        }

        private void SetDoublePixel(double[] pixel, int x, int y, double[] doubleBitmap)
        {
            int index = (y * obm.Width + x) * 3;
            doubleBitmap[index] = pixel[0];
            doubleBitmap[index + 1] = pixel[1];
            doubleBitmap[index + 2] = pixel[2];
        }
        private void AddDoublePixel(double[] pixel, int x, int y, double[] doubleBitmap, bool allowHighLow)
        {
            int index = (y * obm.Width + x) * 3;
            doubleBitmap[index] += pixel[0];
            doubleBitmap[index + 1] += pixel[1];
            doubleBitmap[index + 2] += pixel[2];
            if (!allowHighLow)
            {
                doubleBitmap[index] = Math.Min(Math.Max(doubleBitmap[index], 0), 1);
                doubleBitmap[index + 1] = Math.Min(Math.Max(doubleBitmap[index + 1], 0), 1);
                doubleBitmap[index + 2] = Math.Min(Math.Max(doubleBitmap[index + 2], 0), 1);
            }
        }

        private double DoubleColorDistance(double[] c1, double[] c2)
        {
            double r1 = c1[0];
            double g1 = c1[1];
            double b1 = c1[2];

            double r2 = c2[0];
            double g2 = c2[1];
            double b2 = c2[2];

            double distance = Math.Sqrt(
                Math.Pow(r1 - r2, 2) +
                Math.Pow(g1 - g2, 2) +
                Math.Pow(b1 - b2, 2));

            return distance;
        }

        private bool disposeValue;
        public void Dispose()
        {
            if (!disposeValue)
            {
                obm.Dispose();
                disposeValue = true;
            }
        }
    }
}
