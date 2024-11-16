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
using BIUK9000.GifferComponents.GFLVariants;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BIUK9000
{
    public class GifferIO
    {
        public static Image GifFromGiffer(Giffer giffer)
        {
            MemoryStream stream = new MemoryStream();
            using AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            foreach (GifFrame frame in giffer.Frames)
            {
                agc.AddFrame(giffer.FrameAsBitmap(frame, false), frame.FrameDelay, GifQuality.Bit8);
            }
            return Image.FromStream(stream);
        }
        private static Image GifFromGifferDithered(Giffer giffer, List<Color> paletteForDithering)
        {
            MemoryStream stream = new MemoryStream();
            AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            foreach (GifFrame frame in giffer.Frames)
            {
                Bitmap cbm = giffer.FrameAsBitmap(frame, false);
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
            using Image gif = GifFromGiffer(giffer);
            gif.Save(tempPath);
            if (File.Exists(tempPath))
            {
                return tempPath;
            }
            else
            {
                return null;
            }
        }

        public static string SaveGifToTempFileDithered(Giffer giffer, int ditherColorCount)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            using Bitmap bmpForPalette = giffer.FrameAsBitmap(0, false);
            List<Color> palette = KMeans.Palette(bmpForPalette, ditherColorCount, false);
            using Image gif = GifFromGifferDithered(giffer, palette);
            gif.Save(tempPath);
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
            Bitmap bitmap = mf.SelectedFrameAsBitmap;
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
        public static bool FileImport(string[] filePaths, MainForm mf)
        {
            Giffer mg = mf.MainGiffer;
            bool result = false;
            foreach (string filePath in filePaths)
            {
                try
                {
                    Giffer newGiffer = new Giffer(filePath);
                    if (mg == null)
                    {
                        mf.SetNewGiffer(newGiffer);
                        result = true;
                    }
                    else
                    {
                        using ImportQuestionForm iqf = new ImportQuestionForm();
                        iqf.SelectedFresh += (sender, args) => mf.SetNewGiffer(newGiffer);
                        iqf.SelectedAsLayers += (sender, args) => ImportAsLayers(mf, iqf, newGiffer);
                        iqf.SelectedInsert += (sender, args) => ImportAsInsert(mf, iqf, newGiffer);
                        if (iqf.ShowDialog() == DialogResult.OK) result = true;
                    }
                } catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                    continue;
                }
            }
            return result;
        }
        private static void ImportAsLayers(MainForm mf, ImportQuestionForm iqf, Giffer newGiffer)
        {
            mf.GifferC.AddGifferAsLayers(newGiffer, iqf.OLayersSpread);
            newGiffer.Dispose();
        }
        private static void ImportAsInsert(MainForm mf, ImportQuestionForm iqf, Giffer newGiffer)
        {
            GifferController gc = mf.GifferC;
            if (iqf.OInsertStart)
            {
                gc.AddGifferAsFrames(newGiffer, 0);
            } else if (iqf.OInsertEnd)
            {
                gc.AddGifferAsFrames(newGiffer, gc.FrameCount);
            } else if (iqf.OInsertHere)
            {
                gc.AddGifferAsFrames(newGiffer, mf.SelectedFrameIndex);
            }
        }
        public static void SaveGif(Giffer giffer, ControlsPanel cp, string path)
        {
            if (path == giffer.OriginalImagePath)
            {
                MessageBox.Show("Do not use the same file name for export as you did for import.");
                return;
            }
            string tempPath;
            if (cp.UseDithering)
            {
                tempPath = SaveGifToTempFileDithered(giffer, cp.GifExportColors);
            }
            else
            {
                tempPath = SaveGifToTempFile(giffer);
            }
            if (cp.UseGifsicle)
            {
                OBIMP.CompressGif(tempPath, tempPath, cp.GifExportColors, cp.GifExportLossy);
            }
            if (tempPath != null)
            {
                File.Copy(tempPath, path, true);
            }
            else
            {
                MessageBox.Show("Gif was not created");
                return;
            }
            File.Delete(tempPath);
        }
    }
}
