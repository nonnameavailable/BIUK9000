using BIUK9000.UI.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BIUK9000.IO
{
    public static class FileToBitmapConvertor
    {
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
            return extension switch
            {
                ".mp4" or ".mov" or ".avi" or ".mkv" or ".wmv" => VideoFromPath(path),
                ".gif" => FramesFromGif(Image.FromFile(path)),
                ".bmp" or ".jpeg" or ".png" or ".tiff" => [(Bitmap)Image.FromFile(path)],
                _ => throw new Exception("Unsupported file format.")
            };
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
            return extension switch
            {
                ".mp4" or ".mov" or ".avi" or ".mkv" or ".wmv" => (int)VideoFrameExtractor.GetVideoInfo(path).FrameDelay,
                ".gif" => GifFrameDelay(path),
                ".bmp" or ".jpeg" or ".png" or ".tiff" => 100,
                _ => throw new Exception("Unsupported file format.")
            };
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
    }
}
