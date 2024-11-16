using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;

namespace BIUK9000
{
    public class OVector
    {
        public double X {  get; set; }
        public double Y { get; set; }
        public int Xint { get => (int)X; }
        public int Yint { get => (int)Y; }
        public double Magnitude
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public double Rotation
        {
            get
            {
                double angle = Math.Atan2(Y, X);
                return angle * 180 / Math.PI;
            }
        }
        public double DotProduct(OVector vector)
        {
            return X * vector.X + Y * vector.Y;
        }
        public OVector(double x, double y)
        {
            X = x;
            Y = y;
        }
        public OVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public OVector(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public OVector Add(OVector vector)
        {
            X += vector.X;
            Y += vector.Y;
            return this;
        }
        public OVector Subtract(OVector vector)
        {
            X -= vector.X;
            Y -= vector.Y;
            return this;
        }
        public OVector Multiply(double scalar)
        {
            X *= scalar;
            Y *= scalar;
            return this;
        }

        public OVector Divide(double scalar)
        {
            X /= scalar; Y /= scalar; return this;
        }

        public OVector Rotate (double angle)
        {
            double angleInRadians = angle / 180 * Math.PI;
            double xb = X;
            X = X * Math.Cos(angleInRadians) - Y * Math.Sin(angleInRadians);
            Y = xb * Math.Sin(angleInRadians) + Y * Math.Cos(angleInRadians);
            return this;
        }

        public OVector Normalize()
        {
            Divide(Magnitude); return this;
        }

        public OVector Copy()
        {
            return new OVector(X, Y);
        }
        public override bool Equals(object obj)
        {
            if(obj is not OVector)
            {
                return false;
            } else
            {
                OVector ov = obj as OVector;
                return ov.X == X && ov.Y == Y;
            }
        }

        public static OVector Lerp(OVector start, OVector end, double distance)
        {
            double x = start.X + (end.X - start.X) * distance;
            double y = start.Y + (end.Y - start.Y) * distance;
            return new OVector(x, y);
        }
        public double Distance(OVector end)
        {
            return Math.Sqrt(Math.Pow(end.X - X, 2) + Math.Pow(end.Y - Y, 2));
        }
        public override string ToString()
        {
            return ("X: " + X + ", Y: " + Y);
        }
        public Point ToPoint()
        {
            return new Point(Xint, Yint);
        }
        public OVector Mult2(double hMult, double vMult)
        {
            X *= hMult;
            Y *= vMult;
            return this;
        }
        public OVector Div2(double hMult, double vMult)
        {
            X /= hMult;
            Y /= vMult;
            return this;
        }
    }
}
