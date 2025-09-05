using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BIUK9000.IO
{
    //GPT-5 wrote this. It seems to work fine.
    public static class VideoFrameExtractor
    {
        public static List<Bitmap> ExtractFrames(string videoPath)
        {
            if(!IsFFInPath("ffmpeg") || !IsFFInPath("ffprobe"))
            {
                throw new Exception("Both ffmpeg and ffprobe must be in PATH for this to work!");
            }
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
                string line = reader.ReadLine();
                if (line == null)
                    throw new Exception("Could not read video dimensions from ffprobe output.");

                var parts = line.Split(',');
                if (parts.Length != 2)
                    throw new Exception("Unexpected ffprobe output format.");

                return (int.Parse(parts[0]), int.Parse(parts[1]));
            }
        }
        public static double GetVideoFrameDelay(string videoPath)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams v:0 -show_entries stream=r_frame_rate -of csv=p=0 \"{videoPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            using (var reader = process.StandardOutput)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Could not read framerate from ffprobe output.");

                // r_frame_rate is usually like "30000/1001" or "25/1"
                var parts = line.Split('/');
                if (parts.Length == 2 &&
                    double.TryParse(parts[0], out double numerator) &&
                    double.TryParse(parts[1], out double denominator) &&
                    denominator != 0)
                {
                    return numerator / denominator;
                }
                else if (double.TryParse(line, out double fps))
                {
                    return 1000d / fps;
                }
                else
                {
                    throw new Exception("Unexpected framerate format from ffprobe.");
                }
            }
        }
        private static bool IsFFInPath(string fileName)
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
