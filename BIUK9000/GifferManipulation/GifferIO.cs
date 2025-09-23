using AnimatedGif;
using BIUK9000.GifferComponents;
using BIUK9000.IO;
using BIUK9000.MyGraphics;
using BIUK9000.MyGraphics.Dithering;
using BIUK9000.UI;
using BIUK9000.UI.CustomControls;
using GifskiNet;
using Microsoft.VisualBasic;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferManipulation
{
    public class GifferIO
    {
        public static Image GifFromGiffer(Giffer giffer, GifSFDForm sfdf, InterpolationMode interpolationMode)
        {
            double delayMultiplier = 1;
            double frameStep = 1;
            List<Bitmap> bitmaps = giffer.Frames.Select(frame => frame.CompleteBitmap(giffer.Width, giffer.Height, false, interpolationMode)).ToList();
            List<int> delays = giffer.Frames.Select(frame => frame.FrameDelay).ToList();
            Image result;
            if (sfdf.ChangeFramerate)
            {
                delayMultiplier = sfdf.CurrentFramerate / sfdf.NewFramerate;
                frameStep = delayMultiplier;
            }
            if (sfdf.ChosenExportLibrary == GifSFDForm.ExportLibrary.AnimatedGif)
            {
                result = BMPToGifConvertor.GifAnimatedGif(bitmaps, delays, frameStep, sfdf.GifQuality);
            } else
            {
                result = BMPToGifConvertor.GifGifski(bitmaps, delays, frameStep, 90);
            }
            bitmaps.ForEach(bitmap =>  bitmap.Dispose());
            bitmaps.Clear();
            return result;
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
        public static string SaveGifToTempFile(Giffer giffer, GifSFDForm sfdf, InterpolationMode interpolationMode)
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            using Image gif = GifFromGiffer(giffer, sfdf, interpolationMode);
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
            //if (cp.UseDithering)
            //{
            //    Bitmap bitmapRefBackup = bitmap;
            //    using Ditherer dtr = new Ditherer(bitmap);
            //    bitmap = dtr.DitheredBitmap(KMeans.Palette(bitmap, cp.GifExportColors, false));
            //    bitmapRefBackup.Dispose();
            //}
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
            //if (cp.UseDithering)
            //{
            //    Bitmap bitmapRefBackup = bitmap;
            //    using Ditherer dtr = new Ditherer(bitmap);
            //    bitmap = dtr.DitheredBitmap(KMeans.Palette(bitmap, cp.GifExportColors, false));
            //    bitmapRefBackup.Dispose();
            //}
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
                List<Bitmap> bitmaps = FileToBitmapConvertor.FilesToBitmapList(filePaths);
                int delay = FileToBitmapConvertor.FrameDelayFromFile(filePaths[0]);
                Giffer giffer = new Giffer(bitmaps, 1000 / delay);
                bitmaps.ForEach(bitmap => bitmap.Dispose());
                bitmaps.Clear();
                result = GifImport(mf, giffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }
            return result;
        }
        public static bool GifImport(MainForm mf, Giffer giffer)
        {
            using ImportQuestionForm iqf = new ImportQuestionForm();
            iqf.DisplayOriginalSize(new Size(giffer.Width, giffer.Height));
            if (mf.MainGiffer == null) iqf.SetOnlyFreshMode();
            iqf.SelectedFresh += (sender, args) => ImportAsFresh(mf, iqf, giffer);
            iqf.SelectedAsLayers += (sender, args) => ImportAsLayers(mf, iqf, giffer);
            iqf.SelectedInsert += (sender, args) => ImportAsInsert(mf, iqf, giffer);
            iqf.SelectedReplace += (sender, args) => ImportAsReplace(mf, iqf, giffer);
            if (mf.MainGiffer != null) iqf.SpreadCount = Math.Max((int)Math.Round(mf.MainGiffer.FrameCount / giffer.FrameCount / mf.MainGiffer.AverageFramerate() * giffer.AverageFramerate()), 1);
            if (iqf.ShowDialog() == DialogResult.OK && mf.MainGiffer != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void ImportAsReplace(MainForm mf, ImportQuestionForm iqf, Giffer giffer)
        {
            try
            {
                if (iqf.ReduceSize) giffer.ReduceSize(iqf.NewMaxSideLength);
                if (iqf.OLayersRepeat)
                {
                    mf.GifferC.AddGifferAsReplaceRepeat(giffer, mf.SFI, mf.SLI, mf.Marks);
                }else
                {
                    mf.GifferC.AddGifferAsReplaceSpread(giffer, mf.SFI, mf.SLI, mf.Marks);
                }
                mf.MainTimelineSlider.ClearMarks();
                giffer.Dispose();
            }catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ImportAsFresh(MainForm mf, ImportQuestionForm iqf, Giffer giffer)
        {
            try
            {
                if (iqf.ReduceSize) giffer.ReduceSize(iqf.NewMaxSideLength);
                mf.SetNewGiffer(giffer);
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void ImportAsLayers(MainForm mf, ImportQuestionForm iqf, Giffer giffer)
        {
            try
            {
                if (iqf.ReduceSize) giffer.ReduceSize(iqf.NewMaxSideLength);
                mf.GifferC.AddGifferAsLayers(giffer, iqf.OLayersSpread, iqf.SpreadCount);
                giffer.Dispose();
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private static void ImportAsInsert(MainForm mf, ImportQuestionForm iqf, Giffer giffer)
        {
            GifferController gc = mf.GifferC;
            try
            {
                if (iqf.ReduceSize) giffer.ReduceSize(iqf.NewMaxSideLength);
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
        public static void SaveGif(Giffer giffer, string path, InterpolationMode interpolationMode)
        {

            if (Path.GetExtension(path) == string.Empty || Path.GetExtension(path) != ".gif") path += ".gif";
            //if (cp.UseDithering)
            //{
            //    tempPath = SaveGifToTempFileDithered(giffer, cp.GifExportColors, gifQuality, interpolationMode);
            //}
            //else
            //{
            //    tempPath = SaveGifToTempFile(giffer, gifQuality, interpolationMode);
            //}
            string tempPath;
            using GifSFDForm sfdf = new GifSFDForm();
            sfdf.CurrentFramerate = giffer.AverageFramerate();
            if (sfdf.ShowDialog() != DialogResult.OK) return;
            if (sfdf.CreateGIF)
            {
                tempPath = SaveGifToTempFile(giffer, sfdf, interpolationMode);
                if (sfdf.UseGifsicle && sfdf.ChosenExportLibrary == GifSFDForm.ExportLibrary.AnimatedGif)
                {
                    OBIMP.CompressGif(tempPath, tempPath, sfdf.GifExportColors, sfdf.GifExportLossy);
                }
                if (tempPath != null)
                {
                    File.Copy(tempPath, path, true);
                    File.Delete(tempPath);
                }
                else
                {
                    MessageBox.Show("Gif was not created");
                    return;
                }
            }
            if (sfdf.CreateFrames) SaveGifAsFrames(giffer, path, sfdf, interpolationMode);
            if (sfdf.CreateVideo) SaveGifAsMp4(giffer, path, sfdf, interpolationMode);
        }
        public static void SaveGifAsFrames(Giffer giffer, string path, GifSFDForm sfdf, InterpolationMode interpolationMode)
        {
            string framesPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_frames");
            int counter = 0;
            if (Directory.Exists(framesPath))
            {
                string newPath = framesPath + "_" + counter.ToString();
                while(Directory.Exists(newPath))
                {
                    counter++;
                    newPath = framesPath + "_" + counter.ToString();
                }
                Directory.CreateDirectory(newPath);
            } else
            {
                Directory.CreateDirectory(framesPath);
            }
            double frameStep = 1;
            if (sfdf.ChangeFramerate)
            {
                frameStep = sfdf.CurrentFramerate / sfdf.NewFramerate;
            }
            int frameCounter = 0;
            for (double i = 0; i < giffer.FrameCount; i += frameStep)
            {
                GifFrame frame = giffer.Frames[(int)i];
                using Bitmap bitmap = frame.CompleteBitmap(giffer.Width, giffer.Height, false, interpolationMode);
                string frameFileName = $"frame_{frameCounter:D5}" + sfdf.ImageExportFormat;
                string framePath = Path.Combine(framesPath, frameFileName);
                switch (sfdf.ImageExportFormat)
                {
                    case ".jpeg":
                        OBIMP.SaveJpeg(framePath, bitmap, sfdf.ImageExportJpegQuality);
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
                frameCounter++;
            }
        }
        public static void SaveGifAsMp4(Giffer giffer, string path, GifSFDForm sfdf, InterpolationMode interpolationMode)
        {
            if (!IsFFInPath("ffmpeg"))
            {
                MessageBox.Show("ffmpeg must be in PATH for this to work!");
                return;
            }
            string videoPath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_video.mp4");
            if(File.Exists(videoPath))
            {
                if(MessageBox.Show("The video file " + Environment.NewLine + videoPath + Environment.NewLine + "already exists." + Environment.NewLine + "Do you want to overwrite it?", "careful", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(videoPath);
                } else
                {
                    return;
                }
            }
            string soundCmdPart = "";
            if (!string.IsNullOrEmpty(giffer.SoundPath))
            {
                soundCmdPart = $"-i \"{giffer.SoundPath}\" -c:a aac";
            }

            string command = $"-f rawvideo -pix_fmt bgr24 -s {giffer.Width}x{giffer.Height} " +
                             $"-r {giffer.AverageFramerate().ToString(CultureInfo.InvariantCulture)} " +
                             $"-i pipe:0 {soundCmdPart} -c:v libx264 -pix_fmt yuv420p " +
                             $"-map 0:v:0 {(string.IsNullOrEmpty(soundCmdPart) ? "" : "-map 1:a:0 ")} " +
                             $"\"{videoPath}\"";
            Debug.Print(command);
            var ffmpeg = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = command,
                    RedirectStandardInput = true,
                    //RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            ffmpeg.Start();
            using var stream = ffmpeg.StandardInput.BaseStream;
            int counter = 0;
            foreach(GifFrame frame in giffer.Frames)
            {
                //Debug.Print(counter.ToString());
                var bitmapData = new byte[giffer.Width * giffer.Height * 3];
                var rect = new Rectangle(0, 0, giffer.Width, giffer.Height);
                using Bitmap bitmap = frame.CompleteBitmap24rgb(giffer.Width, giffer.Height, false, interpolationMode);
                var bitmapLock = bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                Marshal.Copy(bitmapLock.Scan0, bitmapData, 0, bitmapData.Length);
                bitmap.UnlockBits(bitmapLock);
                stream.Write(bitmapData, 0, bitmapData.Length);
                counter++;
            }
            stream.Close();
            ffmpeg.WaitForExit();
        }
        public static bool IsFFInPath(string fileName)
        {
            try
            {
                // Create a process to check ffmpeg
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = "-version", // Check ffmpeg version
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                process.WaitForExit();

                // If ffmpeg runs successfully, it should exit with a code of 0
                return process.ExitCode == 0;
            }
            catch
            {
                // If an exception occurs, ffmpeg is likely not in the PATH
                return false;
            }
        }
    }
}
