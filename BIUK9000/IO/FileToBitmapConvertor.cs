using BIUK9000.UI.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BIUK9000.IO
{
    public static class FileToBitmapConvertor
    {
        public static List<string> SupportedImageFormats = [".jpg", "jpeg", ".bmp", ".tiff", ".png"];
        //public static List<string> SupportedVideoFormats = [".mp4", ".mov", ".avi", ".mkv", ".wmv"];
        public static List<string> SupportedVideoFormats =
            [
                ".mp4", ".m4v",
                ".mkv",
                ".avi",
                ".mov", ".qt",
                ".wmv", ".asf",
                ".flv", ".f4v",
                ".webm",
                ".mpeg", ".mpg", ".mpe", ".m2v",
                ".ts", ".m2ts", ".mts",
                ".ogv", ".ogg",
                ".3gp", ".3g2",
                ".rmvb", ".rm",
                ".vob",
                ".divx",
                ".mxf",
            ];
        public static List<Bitmap> FilesToBitmapList(string[] paths)
        {
            List<Bitmap> result = new();
            string errList = string.Empty;
            foreach (string path in paths)
            {
                try
                {
                    result.AddRange(BitmapListFromFile(path));
                }
                catch (Exception ex)
                {
                    errList += $"File {path} failed to import because:" + Environment.NewLine + ex.Message;
                }
            }
            if(result.Count == 0)
            {
                if(errList != string.Empty)
                {
                    throw new Exception(errList);
                } else
                {
                    throw new Exception("Failed to generate Bitmap objects from files.");
                }
            }
            if(errList != string.Empty)
            {
                MessageBox.Show(errList);
            }
            return result;
        }
        private static List<Bitmap> BitmapListFromFile(string path)
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            if (SupportedImageFormats.Contains(extension))
            {
                return [(Bitmap)Image.FromFile(path)];
            }
            else if (SupportedVideoFormats.Contains(extension))
            {
                return VideoFromPath(path);
            }
            else if(extension == ".gif")
            {
                return FramesFromGif(Image.FromFile(path));
            }
            else if(extension is ".wmf" or ".emf")
            {
                return [BitmapFromMetafile(path)];
            }
            else
            {
                throw new Exception("Unsupported file format.");
            }
            //return extension switch
            //{
            //    ".mp4" or ".mov" or ".avi" or ".mkv" or ".wmv" => VideoFromPath(path),
            //    ".gif" => FramesFromGif(Image.FromFile(path)),
            //    ".bmp" or ".jpeg" or ".png" or ".tiff" or ".jpg" => [(Bitmap)Image.FromFile(path)],
            //    _ => throw new Exception("Unsupported file format.")
            //};
        }
        private static List<Bitmap> VideoFromPath(string path)
        {
            VideoInfo vi = VideoFrameExtractor.GetVideoInfo(path);
            Debug.Print(vi.ToString());
            if(vi.DurationSeconds > 0 && vi.EstimatedMemoryUsageBytes < 1024 * 1024 * 1024)
            {
                return VideoFrameExtractor.ExtractFrames(path, new FrameExtractOptions());
            } else
            {
                using VideoImportForm vif = new();
                vif.LoadVideo(path);
                if (vif.ShowDialog() == DialogResult.OK)
                {
                    var feo = vif.FrameExtractOptions();
                    return VideoFrameExtractor.ExtractFrames(path, feo);
                }
                else
                {
                    throw new Exception("User closed form");
                }
            }
            //return VideoFrameExtractor.ExtractFrames(path);
        }
        public static int FrameDelayFromFile(string path)
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            if (SupportedImageFormats.Contains(extension) || extension is ".wmf" or ".emf")
            {
                return 100;
            }
            else if (SupportedVideoFormats.Contains(extension))
            {
                return (int)VideoFrameExtractor.GetVideoInfo(path).FrameDelay;
            }
            else if (extension == ".gif")
            {
                return GifFrameDelay(path);
            }
            else
            {
                throw new Exception("Unsupported file format.");
            }
        }
        //public static (List<Bitmap> bitmaps, int delay) 
        private static List<Bitmap> FramesFromGif(Image gif)
        {
            List<Bitmap> result = new();
            int frameCount = ImageFrameCount(gif);
            if (frameCount == 1)
            {
                result.Add(new Bitmap(gif));
                gif.Dispose();
                return result;
            }
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new Bitmap(gif));
            }
            gif.Dispose();
            return result;
        }
        private static int GifFrameDelay(string path)
        {
            using Image img = Image.FromFile(path);
            PropertyItem propertyItem = img.GetPropertyItem(0x5100);
            int result = BitConverter.ToInt32(propertyItem.Value, 0) * 10;
            return result;
        }
        private static int ImageFrameCount(Image img)
        {
            int frameCount = 1;
            try
            {
                frameCount = img.GetFrameCount(FrameDimension.Time);
            }
            catch (ExternalException ex)
            {
                Debug.Print(ex.Message);
                frameCount = 1;
            }
            return frameCount;
        }
        private static Bitmap BitmapFromMetafile(string path, int width = 0, int height = 0)
        {
            using var metafile = new Metafile(path);

            // Use the metafile's native resolution if no size specified
            if (width == 0 || height == 0)
            {
                var size = metafile.Size;
                width = size.Width > 0 ? size.Width : 800;
                height = size.Height > 0 ? size.Height : 600;
            }

            var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.Transparent);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(metafile, new Rectangle(0, 0, width, height));

            return bitmap;
        }
    }
}
