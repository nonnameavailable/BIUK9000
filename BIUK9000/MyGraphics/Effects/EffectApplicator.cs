using BIUK9000.MyGraphics.Effects.OrderedDithering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                _ => null
            };
        }
    }
    public enum EffectType
    {
        Halftone
    }
}
