using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BIUK9000.Helpers
{
    //GPT-5 wrote this. It seems to work fine.
    public static class VideoFrameExtractor
    {
        public static List<Bitmap> ExtractFrames(string videoPath)
        {
            // 1. Get video dimensions using ffprobe
            (int width, int height) = GetVideoDimensions(videoPath);

            var frames = new List<Bitmap>();

            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{videoPath}\" -f image2pipe -pix_fmt bgr24 -vcodec rawvideo -",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true, // ffmpeg writes logs to stderr
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            using (var stdout = process.StandardOutput.BaseStream)
            {
                int frameSize = width * height * 3; // 3 bytes per pixel (BGR)
                byte[] buffer = new byte[frameSize];

                while (true)
                {
                    int bytesRead = 0;
                    while (bytesRead < frameSize)
                    {
                        int read = stdout.Read(buffer, bytesRead, frameSize - bytesRead);
                        if (read == 0) // End of stream
                            return frames;
                        bytesRead += read;
                    }

                    var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                    var bmpData = bmp.LockBits(
                        new Rectangle(0, 0, width, height),
                        ImageLockMode.WriteOnly,
                        bmp.PixelFormat);

                    Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);
                    bmp.UnlockBits(bmpData);

                    frames.Add(bmp);
                }
            }
        }

        private static (int width, int height) GetVideoDimensions(string videoPath)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams v:0 -show_entries stream=width,height -of csv=p=0 \"{videoPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            using (var reader = process.StandardOutput)
            {
                string? line = reader.ReadLine();
                if (line == null)
                    throw new Exception("Could not read video dimensions from ffprobe output.");

                var parts = line.Split(',');
                if (parts.Length != 2)
                    throw new Exception("Unexpected ffprobe output format.");

                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
        }
    }
}
