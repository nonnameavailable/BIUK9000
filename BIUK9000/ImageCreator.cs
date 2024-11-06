using AnimatedGif;
using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using BIUK9000.Dithering;
using BIUK9000.UI;
using System.Windows.Forms;

namespace BIUK9000
{
    public class ImageCreator
    {
        public static Image GifFromGiffer(Giffer giffer)
        {
            MemoryStream stream = new MemoryStream();
            int frameDelay = giffer.FrameDelay();
            using AnimatedGifCreator agc = new AnimatedGifCreator(stream, frameDelay);
            foreach (GifFrame frame in giffer.Frames)
            {
                agc.AddFrame(frame.CompleteBitmap(false), frame.FrameDelay, GifQuality.Bit8);
            }
            return Image.FromStream(stream);
        }
        public static Image GifFromGiffer(Giffer giffer, List<Color> paletteForDithering)
        {
            MemoryStream stream = new MemoryStream();
            int frameDelay = giffer.FrameDelay();
            AnimatedGifCreator agc = new AnimatedGifCreator(stream, frameDelay);
            foreach (GifFrame frame in giffer.Frames)
            {
                Bitmap cbm = frame.CompleteBitmap(false);
                Ditherer dtr = new Ditherer(cbm);
                cbm = dtr.DitheredBitmap(paletteForDithering);
                agc.AddFrame(cbm, frame.FrameDelay, GifQuality.Bit8);
                dtr.Dispose();
            }
            return Image.FromStream(stream);
        }
        public static string SaveGifToTempFile(Giffer giffer)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            GifFromGiffer(giffer).Save(tempPath);
            if (File.Exists(tempPath))
            {
                return tempPath;
            }
            else
            {
                return null;
            }
        }

        public static string SaveGifToTempFile(Giffer giffer, int ditherColorCount)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            Image gif;
            using Bitmap bmpForPalette = giffer.Frames[0].CompleteBitmap(false);
            List<Color> palette = KMeans.Palette(bmpForPalette, ditherColorCount, false);
            gif = GifFromGiffer(giffer, palette);
            if (File.Exists(tempPath))
            {
                return tempPath;
            }
            else
            {
                return null;
            }
        }

        public static void FrameExportDragDrop(MainForm mf)
        {
            Bitmap bitmap = mf.SelectedFrame.CompleteBitmap(false);
            ControlsPanel cp = mf.MainControlsPanel;
            if (cp.UseDithering)
            {
                Bitmap bitmapRefBackup = bitmap;
                using Ditherer dtr = new Ditherer(bitmap);
                bitmap = dtr.DitheredBitmap(KMeans.Palette(bitmap, cp.GifExportColors, false));
                bitmapRefBackup.Dispose();
            }
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), mf.MainControlsPanel.ImageExportFormat);
            switch (cp.ImageExportFormat)
            {
                case ".jpeg":
                    OBIMP.SaveJpeg(tempPath, bitmap, 80);
                    break;
                default:
                    bitmap.Save(tempPath);
                    break;
            }
            bitmap.Dispose();
            DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
            mf.DoDragDrop(data, DragDropEffects.Copy);
            cp.IsLMBDown = false;
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }
}
