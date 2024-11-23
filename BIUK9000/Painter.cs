using BIUK9000.Dithering;
using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class Painter
    {
        public static void DrawLine(Graphics g, Point p1, Point p2, Color paintColor, float thickness)
        {
            using Pen p = new Pen(new SolidBrush(paintColor), thickness);
            p.LineJoin = LineJoin.Round;
            p.StartCap = LineCap.Round;
            p.EndCap = LineCap.Round;
            g.CompositingMode = CompositingMode.SourceCopy;
            g.DrawLine(p, p1, p2);
        }
        public static void DrawLinesFromPoints(Graphics g, List<Point> points, Color paintColor, float thickness)
        {
            if(points.Count < 2) return;
            for(int i = 1; i < points.Count; i++)
            {
                DrawLine(g, points[i], points[i-1], paintColor, thickness);
            }
        }
        public static void DrawLinesFromPoints(Graphics g, List<Point> points, List<Color> colors, float thickness)
        {
            if (points.Count < 2) return;
            for (int i = 1; i < points.Count; i++)
            {
                Color color = colors[i % colors.Count];
                DrawLine(g, points[i], points[i - 1], color, thickness);
            }
        }
        public static Bitmap DeleteColor(Bitmap bmp, Point p, int tolerance)
        {
            if(p.X >= bmp.Width || p.Y >= bmp.Height || p.X < 0 || p.Y < 0)
            {
                Debug.Print("clicked outside");
                return new Bitmap(bmp);
            } else
            {
                return DeleteColor(bmp, bmp.GetPixel(p.X, p.Y), tolerance);
            }

        }
        public static Bitmap DeleteColor(Bitmap bmp, Color c, int tolerance)
        {
            using FastBitmap fbmp = new FastBitmap(bmp);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c2 = fbmp.GetPixel(i, j);
                    if (ColorIsWithinTolerance(c, c2, tolerance))
                    {
                        Color transparentC2 = Color.FromArgb(0, c2.R, c2.G, c2.B);
                        fbmp.SetPixel(i, j, transparentC2);
                    }
                }
            }
            return new Bitmap(fbmp.Bitmap);
        }
        public static Bitmap ReplaceColor(Bitmap bmp, Color oc, Color rc, int tolerance)
        {
            using FastBitmap fbmp = new FastBitmap(bmp);
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c2 = fbmp.GetPixel(i, j);
                    if (ColorIsWithinTolerance(oc, c2, tolerance)) fbmp.SetPixel(i, j, rc);
                }
            }
            return new Bitmap(fbmp.Bitmap);
        }
        private static int[] ColorDistances(Color c1, Color c2)
        {
            int[] result = new int[3];
            result[0] = Math.Abs(c2.R - c1.R);
            result[1] = Math.Abs(c2.G - c1.G);
            result[2] = Math.Abs(c2.B - c1.B);

            return result;
        }
        private static bool ColorIsWithinTolerance(Color c1, Color c2, int tolerance)
        {
            int[] distances = ColorDistances(c1, c2);
            return !(distances[0] > tolerance || distances[1] > tolerance || distances[2] > tolerance);
        }
        public static Bitmap LassoCutout(Bitmap originalBitmap, Point[] lassoPoints, bool constrainBounds)
        {
            // Create a GraphicsPath from the lasso points
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(lassoPoints);

            // Calculate the bounding box of the path
            RectangleF boundingBox = path.GetBounds();
            float x = 0;
            float y = 0;
            float width = originalBitmap.Width;
            float height = originalBitmap.Height;
            if (constrainBounds)
            {
                x = Math.Max(boundingBox.X, 0);
                y = Math.Max(boundingBox.Y, 0);
                width = Math.Min(boundingBox.Right, originalBitmap.Width) - x;
                height = Math.Min(boundingBox.Bottom, originalBitmap.Height) - y;
            }
            // Constrain the bounding box to the image dimensions


            // Create a new bitmap to hold the cutout
            Bitmap cutoutBitmap = new Bitmap((int)width, (int)height);

            using (Graphics g = Graphics.FromImage(cutoutBitmap))
            {
                // Clear the bitmap with a transparent background
                g.Clear(Color.Transparent);

                // Translate the graphics object to the bounding box location
                g.TranslateTransform(-x, -y);

                // Set the clipping region to the lasso region
                Region region = new Region(path);
                g.SetClip(region, CombineMode.Replace);

                // Draw the original bitmap within the clipping region
                g.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));
            }

            return cutoutBitmap;
        }
        public static Bitmap LassoComplement(Bitmap originalBitmap, Point[] lassoPoints)
        {
            // Create a GraphicsPath from the lasso points
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(lassoPoints);

            // Create a new bitmap to hold the complement
            Bitmap complementBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            using (Graphics g = Graphics.FromImage(complementBitmap))
            {
                // Draw the original bitmap onto the new bitmap
                g.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height));

                // Set the clipping region to the lasso region
                Region region = new Region(path);
                g.SetClip(region, CombineMode.Replace);

                // Clear the lasso region with a transparent background
                g.Clear(Color.Transparent);
            }

            return complementBitmap;
        }
        public static void AdjustImageAttributes(Image image, float brightness, float saturation)
        {
            //adapted from https://stackoverflow.com/a/14384449/9852011
            // Luminance vector for linear RGB
            const float rwgt = 0.3086f;
            const float gwgt = 0.6094f;
            const float bwgt = 0.0820f;

            // Create a new color matrix
            ColorMatrix colorMatrix = new ColorMatrix();

            // Adjust saturation
            float baseSat = 1.0f - saturation;
            colorMatrix[0, 0] = baseSat * rwgt + saturation;
            colorMatrix[0, 1] = baseSat * rwgt;
            colorMatrix[0, 2] = baseSat * rwgt;
            colorMatrix[1, 0] = baseSat * gwgt;
            colorMatrix[1, 1] = baseSat * gwgt + saturation;
            colorMatrix[1, 2] = baseSat * gwgt;
            colorMatrix[2, 0] = baseSat * bwgt;
            colorMatrix[2, 1] = baseSat * bwgt;
            colorMatrix[2, 2] = baseSat * bwgt + saturation;

            // Adjust brightness
            float adjustedBrightness = brightness - 1f;
            colorMatrix[4, 0] = adjustedBrightness;
            colorMatrix[4, 1] = adjustedBrightness;
            colorMatrix[4, 2] = adjustedBrightness;

            // Create image attributes
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            // Draw the image with the new color matrix
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                0, 0, image.Width, image.Height,
                GraphicsUnit.Pixel, imageAttributes);
            }
        }
        public static Bitmap FloodFill(Bitmap bitmap, Point p, Color fillColor, int tolerance)
        {
            using FastBitmap fbm = new FastBitmap(bitmap);
            Color originalColor = fbm.GetPixel(p);
            List<Point> s = [p];
            bool[,] processed = new bool[bitmap.Width, bitmap.Height];
            while (s.Count > 0)
            {
                int x = s[0].X;
                int y = s[0].Y;
                s.RemoveAt(0);
                int lx = x;
                while(Inside(fbm, new Point(lx - 1, y), originalColor, tolerance) && !processed[lx - 1, y])
                {
                    fbm.SetPixel(lx - 1, y, fillColor);
                    processed[lx - 1, y] = true;
                    lx--;
                }
                while(Inside(fbm, new Point(x, y), originalColor, tolerance) && !processed[x, y])
                {
                    fbm.SetPixel(x, y, fillColor);
                    processed[x, y] = true;
                    x++;
                }
                Scan(lx, x - 1, y + 1, s, fbm, originalColor, tolerance);
                Scan(lx, x - 1, y - 1, s, fbm, originalColor, tolerance);
            }
            return new Bitmap(fbm.Bitmap);
        }
        private static void Scan(int lx, int rx, int y, List<Point> s, FastBitmap fbm, Color originalColor, int tolerance)
        {
            bool spanAdded = false;
            for(int x = lx; x <=rx; x++)
            {
                Point xy = new Point(x, y);
                if(!Inside(fbm, xy, originalColor, tolerance))
                {
                    spanAdded = false;
                }else if (!spanAdded)
                {
                    s.Add(xy);
                    spanAdded = true;
                }
            }
        }
        private static bool Inside(FastBitmap fbm, Point p, Color c, int tolerance)
        {
            if (p.X >= fbm.Width || p.Y >= fbm.Height || p.X < 0 || p.Y < 0) return false;
            return ColorIsWithinTolerance(fbm.GetPixel(p), c, tolerance);
        }
    }
}
