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
using Microsoft.VisualBasic;
using GifskiNet;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BIUK9000
{
    public class GifferIO
    {
        public static Image GifFromGiffer(Giffer giffer, GifQuality gifQuality, InterpolationMode interpolationMode)
        {
            //MemoryStream stream = new MemoryStream();
            //using AnimatedGifCreator agc = new AnimatedGifCreator(stream, 20);
            //foreach (GifFrame frame in giffer.Frames)
            //{
            //    agc.AddFrame(giffer.FrameAsBitmap(frame, false, interpolationMode), frame.FrameDelay, gifQuality);
            //}
            //return Image.FromStream(stream);

            using var gifski = Gifski.Create(@"resources\gifski.dll", settings =>
            {
                settings.Quality = 100;
            });
            var stream = new MemoryStream();
            gifski.SetStreamOutput(stream);
            //gifski.SetFileOutput(@"C:\MOJE\MyRepos\images\fasdfasdfas.gif");
            int counter = 0;
            double delayCumulation = 0;
            foreach (GifFrame gf in giffer.Frames)
            {
                delayCumulation += gf.FrameDelay / 1000d;
                using Bitmap frame = giffer.FrameAsBitmap(gf, false, interpolationMode);
                using FastBitmap fbm = new FastBitmap(frame);
                for (int i = 0; i < fbm.Width; i++)
                {
                    for (int j = 0; j < fbm.Height; j++)
                    {
                        Color c = fbm.GetPixel(i, j);
                        fbm.SetPixel(i, j, Color.FromArgb(c.A, c.B, c.G, c.R));
                    }
                }
                byte[] argb = ImageToByte(fbm.Bitmap);
                //byte[] rgba = new byte[argb.Length];

                //for (int i = 0; i < argb.Length; i += 4)
                //{
                //    byte a = argb[i];       // A
                //    byte r = argb[i + 1];   // R
                //    byte g = argb[i + 2];   // G
                //    byte b = argb[i + 3];   // B

                //    // Assign values to the rgba array
                //    rgba[i] = a;    // R
                //    rgba[i + 1] = r; // G
                //    rgba[i + 2] = g; // B
                //    rgba[i + 3] = b; // A
                //}
                gifski.AddFrameRgba((uint)counter, delayCumulation, (uint)frame.Width, (uint)frame.Height, argb);
                counter++;
            }
            gifski.Finish();
            return Image.FromStream(stream);
        }
        public static byte[] ImageToByte(Bitmap bitmap)
        {
            BitmapData bmpdata = null;

            try
            {
                bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
                int numbytes = bmpdata.Stride * bitmap.Height;
                byte[] bytedata = new byte[numbytes];
                IntPtr ptr = bmpdata.Scan0;

                Marshal.Copy(ptr, bytedata, 0, numbytes);

                return bytedata;
            }
            finally
            {
                if (bmpdata != null)
                    bitmap.UnlockBits(bmpdata);
            }
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
            bool result = false;
            try
            {
                using ImportQuestionForm iqf = new ImportQuestionForm();
                if (mf.MainGiffer == null) iqf.SetOnlyFreshMode();
                iqf.SelectedFresh += (sender, args) => ImportAsFresh(mf, iqf, filePaths);
                iqf.SelectedAsLayers += (sender, args) => ImportAsLayers(mf, iqf, filePaths);
                iqf.SelectedInsert += (sender, args) => ImportAsInsert(mf, iqf, filePaths);
                if (iqf.ShowDialog() == DialogResult.OK && mf.MainGiffer != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return result;
        }
        private static void ImportAsFresh(MainForm mf, ImportQuestionForm iqf, string[] filePaths)
        {
            try
            {
                mf.SetNewGiffer(new Giffer(filePaths));
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ImportAsLayers(MainForm mf, ImportQuestionForm iqf, string[] filePaths)
        {
            try
            {
                Giffer giffer = new Giffer(filePaths);
                mf.GifferC.AddGifferAsLayers(giffer, iqf.OLayersSpread);
                giffer.Dispose();
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private static void ImportAsInsert(MainForm mf, ImportQuestionForm iqf, string[] filePaths)
        {
            GifferController gc = mf.GifferC;
            try
            {
                Giffer giffer = new Giffer(filePaths);
                if (iqf.OInsertStart)
                {
                    gc.AddGifferAsFrames(giffer, 0);
                }
                else if (iqf.OInsertEnd)
                {
                    gc.AddGifferAsFrames(giffer, gc.FrameCount);
                }
                else if (iqf.OInsertHere)
                {
                    gc.AddGifferAsFrames(giffer, mf.SFI);
                }
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void SaveGif(Giffer giffer, ControlsPanel cp, string path, GifQuality gifQuality, bool createFrames, InterpolationMode interpolationMode)
        {
            if (Path.GetExtension(path) == string.Empty || Path.GetExtension(path) != ".gif") path += ".gif";
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
