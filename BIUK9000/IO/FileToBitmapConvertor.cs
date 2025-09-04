using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
                    result.AddRange(FileToBitmapList(path));
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
        public static List<Bitmap> FileToBitmapList(string path)
        {
            string extension = Path.GetExtension(path).ToLowerInvariant();
            try
            {
                return extension switch
                {
                    ".mp4" or ".mov" or ".avi" or ".mkv" or ".wmv" => VideoFrameExtractor.ExtractFrames(path),
                    ".gif" => FramesFromGif(Image.FromFile(path)),
                    _ => [(Bitmap)Image.FromFile(path)]
                };
            }
            catch
            {
                throw new Exception("Failed to load file: " + path);
            }
        }
        private static List<Bitmap> FramesFromGif(Image gif)
        {
            List<Bitmap> result = new();
            int frameCount = ImageFrameCount(gif);
            if (frameCount == 1)
            {
                result.Add(new Bitmap(gif));
                return result;
            }
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new Bitmap(gif));
            }
            return result;
        }
        private static int FrameDelay(Image img)
        {
            PropertyItem propertyItem = img.GetPropertyItem(0x5100);
            return BitConverter.ToInt32(propertyItem.Value, 0) * 10;
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
