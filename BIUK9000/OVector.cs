using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class OVector
    {
        public double X {  get; set; }
        public double Y { get; set; }
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
                double quad = 0;
                if (X < 0 && Y >= 0) quad = 1;
                if (X <= 0 && Y < 0) quad = 2;
                if (X > 0 && Y <= 0) quad = 3;
                double helpRot = Math.PI / 2 * (quad + 1);

                OVector help = Copy().Rotate(-90 * quad);
                double angle = Math.Asin(help.Y / help.Magnitude);

                return (angle + helpRot) * 180 / Math.PI;
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

    }
}
