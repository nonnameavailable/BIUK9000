using BIUK9000.MyGraphics.Dithering;
using BIUK9000.MyGraphics.Effects.OrderedDithering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.MyGraphics.Effects
{
    public static class EffectApplicator
    {
        public static Bitmap BitmapWithEffect(Bitmap bitmap, EffectType effectType)
        {
            return effectType switch
            {
                EffectType.Halftone => Patterns.DitheredBitmap(bitmap),
                EffectType.FloydSteinberg => DitheredBitmap(bitmap),
                _ => null
            };
        }

        public static TimeSpan OneBitmapProcessingTime(Bitmap bitmap, EffectType effectType)
        {
            DateTime beforeProcessing = DateTime.Now;
            using Bitmap bmp = BitmapWithEffect(bitmap, effectType);
            return DateTime.Now - beforeProcessing;
        }
        private static Bitmap DitheredBitmap(Bitmap bitmap)
        {
            using Ditherer dt = new Ditherer(bitmap);
            return dt.DitheredBitmap([Color.Black, Color.White]);
        }
    }
    public enum EffectType
    {
        Halftone,
        FloydSteinberg
    }
}
