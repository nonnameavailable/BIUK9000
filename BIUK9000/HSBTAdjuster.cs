using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class HSBTAdjuster
    {
        //Hue 0 - 360, 0 is no change
        //Saturation 1 is no change, <1 for decrease, >1 for increase
        //Brightness 1 is no change, <1 for decrease, >1 for increase
        //Transparency 1 is fully opaque, <1 for transparency
        public static Bitmap HSBTAdjustedBitmap(Bitmap bitmap, float hue, float saturation, float brightness, float transparency)
        {
            ImageAttributes imageAttributes = HSBTAdjustedImageAttributes(hue, saturation, brightness, transparency);

            Bitmap result = new Bitmap(bitmap.Width, bitmap.Height);
            using Graphics g = Graphics.FromImage(result);
            g.DrawImage(bitmap,
                new Rectangle(0,0, bitmap.Width, bitmap.Height),
                0, 0, bitmap.Width, bitmap.Height,
                GraphicsUnit.Pixel, imageAttributes);
            return result;
        }

        public static ImageAttributes HSBTAdjustedImageAttributes(float hue, float saturation, float brightness, float transparency)
        {
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(
                MultipliedColorMatrix(
                    SBTShiftedColorMatrix(saturation, brightness, transparency),
                    HueRotatedColorMatrix(hue)),
                ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return ia;
        }

        private static ColorMatrix MultipliedColorMatrix(ColorMatrix sbtCm, ColorMatrix hCm)
        {
            ColorMatrix result = new ColorMatrix();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    result[i, j] = sbtCm[i, 0] * hCm[0, j];
                    result[i, j] += sbtCm[i, 1] * hCm[1, j];
                    result[i, j] += sbtCm[i, 2] * hCm[2, j];
                    result[i, j] += sbtCm[i, 3] * hCm[3, j];
                    result[i, j] += sbtCm[i, 4] * hCm[4, j];
                }
            }
            return result;
        }
        private static ColorMatrix SBTShiftedColorMatrix(float saturation, float brightness, float transparency)
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

            colorMatrix[3, 3] = transparency;

            return colorMatrix;
        }
        private static ColorMatrix HueRotatedColorMatrix(float hueShiftDegrees)
        {
            float theta = (float)(hueShiftDegrees / 360 * 2 * Math.PI); //Degrees --> Radians
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            float A00 = (float)(0.213 + 0.787 * c - 0.213 * s);
            float A01 = (float)(0.213 - 0.213 * c + 0.413 * s);
            float A02 = (float)(0.213 - 0.213 * c - 0.787 * s);

            float A10 = (float)(0.715 - 0.715 * c - 0.715 * s);
            float A11 = (float)(0.715 + 0.285 * c + 0.140 * s);
            float A12 = (float)(0.715 - 0.715 * c + 0.715 * s);

            float A20 = (float)(0.072 - 0.072 * c + 0.928 * s);
            float A21 = (float)(0.072 - 0.072 * c - 0.283 * s);
            float A22 = (float)(0.072 + 0.928 * c + 0.072 * s);

            ColorMatrix cm = new ColorMatrix();
            cm.Matrix00 = A00;
            cm.Matrix01 = A01;
            cm.Matrix02 = A02;
            cm.Matrix10 = A10;
            cm.Matrix11 = A11;
            cm.Matrix12 = A12;
            cm.Matrix20 = A20;
            cm.Matrix21 = A21;
            cm.Matrix22 = A22;
            
            cm.Matrix44 = 1;
            cm.Matrix33 = 1;
            return cm;
        }
    }
}
