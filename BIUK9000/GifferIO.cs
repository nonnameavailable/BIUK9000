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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace BIUK9000
{
    public class GifferIO
    {
        public static Image GifFromGiffer(Giffer giffer, GifQuality gifQuality, InterpolationMode interpolationMode)
        {
            MemoryStream stream = new MemoryStream();
            using AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            foreach (GifFrame frame in giffer.Frames)
            {
                agc.AddFrame(giffer.FrameAsBitmap(frame, false, interpolationMode), frame.FrameDelay, gifQuality);
            }
            return Image.FromStream(stream);
        }
        private static Image GifFromGifferDithered(Giffer giffer, List<Color> paletteForDithering, GifQuality gifQuality, InterpolationMode interpolationMode)
        {
            MemoryStream stream = new MemoryStream();
            AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            foreach (GifFrame frame in giffer.Frames)
            {
                Bitmap cbm = giffer.FrameAsBitmap(frame, false, interpolationMode);
                Ditherer dtr = new Ditherer(cbm);
                cbm = dtr.DitheredBitmap(paletteForDithering);
                agc.AddFrame(cbm, frame.FrameDelay, gifQuality);
                dtr.Dispose();
            }
            return Image.FromStream(stream);
        }
        public static string SaveGifToTempFile(Giffer giffer, GifQuality gifQuality, InterpolationMode interpolationMode)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            using Image gif = GifFromGiffer(giffer, gifQuality, interpolationMode);
            gif.Save(tempPath, ImageFormat.Gif);
            if (File.Exists(tempPath))
            {
                return tempPath;
            }
            else
            {
                return null;
            }
        }

        public static string SaveGifToTempFileDithered(Giffer giffer, int ditherColorCount, GifQuality gifQuality, InterpolationMode interpolationMode)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            using Bitmap bmpForPalette = giffer.FrameAsBitmap(0, false, interpolationMode);
            List<Color> palette = KMeans.Palette(bmpForPalette, ditherColorCount, false);
            using Image gif = GifFromGifferDithered(giffer, palette, gifQuality, interpolationMode);
            gif.Save(tempPath, ImageFormat.Gif);
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
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), cp.ImageExportFormat);
            switch (cp.ImageExportFormat)
            {
                case ".jpeg":
                    OBIMP.SaveJpeg(tempPath, bitmap, cp.ImageExportJpegQuality);
                    break;
                case ".png":
                    bitmap.Save(tempPath, ImageFormat.Png);
                    break;
                case ".gif":
                    bitmap.Save(tempPath, ImageFormat.Gif);
                    break;
                default:
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
        public static void LayerExportDragDrop(MainForm mf)
        {
            Bitmap bitmap = mf.GifferC.GetLayer(mf.SFI, mf.SLI).MorphedBitmap(mf.MainControlsPanel.InterpolationMode);
            ControlsPanel cp = mf.MainControlsPanel;
            if (cp.UseDithering)
            {
                Bitmap bitmapRefBackup = bitmap;
                using Ditherer dtr = new Ditherer(bitmap);
                bitmap = dtr.DitheredBitmap(KMeans.Palette(bitmap, cp.GifExportColors, false));
                bitmapRefBackup.Dispose();
            }
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), cp.ImageExportFormat);
            switch (cp.ImageExportFormat)
            {
                case ".jpeg":
                    OBIMP.SaveJpeg(tempPath, bitmap, cp.ImageExportJpegQuality);
                    break;
                case ".png":
                    bitmap.Save(tempPath, ImageFormat.Png);
                    break;
                case ".gif":
                    bitmap.Save(tempPath, ImageFormat.Gif);
                    break;
                default:
                    break;
            }
            bitmap.Dispose();
            DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
            mf.DoDragDrop(data, DragDropEffects.Copy);
            cp.IsRMBDown = false;
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
                }
                catch (ArgumentException ex)
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
            }
            else if (iqf.OInsertEnd)
            {
                gc.AddGifferAsFrames(newGiffer, gc.FrameCount);
            }
            else if (iqf.OInsertHere)
            {
                gc.AddGifferAsFrames(newGiffer, mf.SFI);
            }
        }
        public static void SaveGif(Giffer giffer, ControlsPanel cp, string path, GifQuality gifQuality, bool createFrames, InterpolationMode interpolationMode)
        {
            if (Path.GetExtension(path) == string.Empty || Path.GetExtension(path) != ".gif") path += ".gif";
            if (path == giffer.OriginalImagePath)
            {
                MessageBox.Show("Do not use the same file name for export as you did for import.");
                return;
            }
            string tempPath;
            if (cp.UseDithering)
            {
                tempPath = SaveGifToTempFileDithered(giffer, cp.GifExportColors, gifQuality, interpolationMode);
            }
            else
            {
                tempPath = SaveGifToTempFile(giffer, gifQuality, interpolationMode);
            }
            if (cp.UseGifsicle)
            {
                OBIMP.CompressGif(tempPath, tempPath, cp.GifExportColors, cp.GifExportLossy);
            }
            if (tempPath != null)
            {
                File.Copy(tempPath, path, true);
                if(createFrames)SaveGifAsFrames(giffer, cp, path);
            }
            else
            {
                MessageBox.Show("Gif was not created");
                return;
            }
            File.Delete(tempPath);
        }
        public static void SaveGifAsFrames(Giffer giffer, ControlsPanel cp, string path)
        {
            string framesPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_frames");
            EnsureFolderExistsAndClean(framesPath);
            for (int i = 0; i < giffer.FrameCount; i++)
            {
                GifFrame frame = giffer.Frames[i];
                using Bitmap bitmap = frame.CompleteBitmap(giffer.Width, giffer.Height, false, cp.InterpolationMode);
                string frameFileName = $"frame_{i:D5}" + cp.ImageExportFormat;
                string framePath = Path.Combine(framesPath, frameFileName);
                switch (cp.ImageExportFormat)
                {
                    case ".jpeg":
                        OBIMP.SaveJpeg(framePath, bitmap, cp.ImageExportJpegQuality);
                        break;
                    case ".png":
                        bitmap.Save(framePath, ImageFormat.Png);
                        break;
                    case ".gif":
                        bitmap.Save(framePath, ImageFormat.Gif);
                        break;
                    default:
                        break;
                }
            }
        }
        public static void EnsureFolderExistsAndClean(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Folder created: {folderPath}");
            }
            else
            {
                string[] files = Directory.GetFiles(folderPath);

                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file: {file}. Exception: {ex.Message}");
                    }
                }
            }
        }
    }
}
