using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class MyMath
    {
        public static double AngleBetweenLines(Point A, Point S, Point B)
        {
            // Calculate vectors SA and SB
            (double x, double y) SA = (A.X - S.X, A.Y - S.Y);
            (double x, double y) SB = (B.X - S.X, B.Y - S.Y);

            // Calculate dot product of SA and SB
            double dotProduct = SA.x * SB.x + SA.y * SB.y;

            // Calculate magnitudes of SA and SB
            double magnitudeSA = Math.Sqrt(SA.x * SA.x + SA.y * SA.y);
            double magnitudeSB = Math.Sqrt(SB.x * SB.x + SB.y * SB.y);

            // Calculate the cosine of the angle
            double cosTheta = dotProduct / (magnitudeSA * magnitudeSB);

            // Calculate the angle in radians
            double angleRadians = Math.Acos(cosTheta);

            // Convert the angle to degrees
            double angleDegrees = angleRadians * (180.0 / Math.PI);
            return angleDegrees;
        }
    }
}
