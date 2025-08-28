using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.Helpers
{
    public static class Lerper
    {
        public static int Lerp(int start, int end, double distance)
        {
            return (int)(start + (end - start) * distance);
        }
        public static float Lerp(float start, float end, double distance)
        {
            return (float)(start + (end - start) * distance);
        }
        public static Color LerpColor(Color start, Color end, double distance)
        {
            int newR = (int)(start.R + (end.R - start.R) * distance);
            int newG = (int)(start.G + (end.G - start.G) * distance);
            int newB = (int)(start.B + (end.B - start.B) * distance);
            return Color.FromArgb(newR, newG, newB);
        }
        public static string LerpText(string start, string end, double distance)
        {
            int newLength = (int)(start.Length + (end.Length - start.Length) * distance);
            return end.Substring(0, newLength);
        }

        public static GraphicsPath ArcPath(OVector start, OVector end, double radius, int steps)
        {
            GraphicsPath path = new GraphicsPath();
            double distance = start.Distance(end);
            double sweepAngle = Math.Asin(distance / 2 / radius) * 360 / Math.PI;
            OVector halfway = end.Copy().Subtract(start).Divide(2d);
            double triDist = Math.Sqrt(radius * radius - halfway.Magnitude * halfway.Magnitude);
            OVector center = halfway.Copy().Rotate(90).Normalize().Multiply(triDist).Add(start).Add(halfway);
            OVector arm = start.Copy().Subtract(center);
            double angleIncrement = sweepAngle / steps;
            for (int i = 0; i < steps; i++)
            {

                OVector lp1 = center.Copy().Add(arm);
                arm.Rotate(angleIncrement);
                OVector lp2 = center.Copy().Add(arm);
                path.AddLine(lp1.ToPoint(), lp2.ToPoint());
            }
            return path;
        }
    }
}
