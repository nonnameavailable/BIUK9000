using BIUK9000.MyGraphics;
using BIUK9000.UI.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BIUK9000.IO
{
    //GPT-5 wrote most of this. It seems to work fine.
    public static class VideoFrameExtractor
    {

        public static List<Bitmap> ExtractFrames(string path, FrameExtractOptions feo)
        {
            if (!IsFFInPath("ffmpeg") || !IsFFInPath("ffprobe"))
            {
                throw new Exception("Both ffmpeg and ffprobe must be in PATH for this to work!");
            }

            // Get original video info
            VideoInfo vi = GetVideoInfo(path);

            // Calculate new dimensions while keeping aspect ratio
            Size newSize = NewSize(vi, feo);

            return FramesFromCommand(FffmpegCommand(path, feo), newSize.Width, newSize.Height);
        }
        public static List<Bitmap> ExtractFramesFast(string path, int maxSideLength, int frameCount)
        {
            VideoInfo vi = GetVideoInfo(path);
            TimeSpan timeIncrement = TimeSpan.FromSeconds(vi.DurationSeconds / frameCount);
            List<Bitmap> result = new();
            for(int i = 0; i < frameCount; i++)
            {
                var feo = new FrameExtractOptions
                {
                    StartTime = timeIncrement * i,
                    FrameCount = 1,
                    MaxSideLength = maxSideLength
                };
                string command = FffmpegCommand(path, feo);
                Size size = NewSize(vi, feo);
                result.AddRange(FramesFromCommand(command, size.Width, size.Height));
            }
            return result;
        }
        public static Size NewSize(VideoInfo vi, FrameExtractOptions feo)
        {
            int width = vi.Width;
            int height = vi.Height;
            if (feo.MaxSideLength.HasValue && (width > feo.MaxSideLength || height > feo.MaxSideLength))
            {
                int largerSide = Math.Max(vi.Width, vi.Height);
                double multiplier = feo.MaxSideLength.Value / (double)largerSide;
                width = (int)(vi.Width * multiplier);
                height = (int)(vi.Height * multiplier);
            }
            width = width / 4 * 4;
            height = height / 4 * 4;
            return new Size(width, height);
        }
        private static string FffmpegCommand(string path, FrameExtractOptions feo)
        {
            VideoInfo vi = GetVideoInfo(path);
            List<string> filters = new();
            string timeArgs = "";
            if (feo.StartTime.HasValue)
            {
                timeArgs += $"-ss {feo.StartTime.Value.ToString("c", CultureInfo.InvariantCulture)} ";
            }
            if (feo.Duration.HasValue)
            {
                timeArgs += $"-t {feo.Duration?.ToString("c", CultureInfo.InvariantCulture)} ";
            }
            if (feo.TargetFPS.HasValue)
            {
                filters.Add($"fps={feo.TargetFPS.Value.ToString(CultureInfo.InvariantCulture)}");
            }
            if (feo.MaxSideLength.HasValue)
            {
                int newWidth = vi.Width;
                int newHeight = vi.Height;
                if (newWidth > feo.MaxSideLength || newHeight > feo.MaxSideLength)
                {
                    int largerSide = Math.Max(vi.Width, vi.Height);
                    double multiplier = feo.MaxSideLength.Value / (double)largerSide;
                    newWidth = ((int)(vi.Width * multiplier / 4)) * 4;
                    newHeight = ((int)(vi.Height * multiplier / 4)) * 4;
                    filters.Add($"scale={newWidth}:{newHeight}");
                }
            }
            string frameCount = "";
            if (feo.FrameCount.HasValue)
            {
                frameCount = "-frames:v " + feo.FrameCount.Value;
            }
            string filterArg = filters.Count > 0 ? $"-vf \"{string.Join(",", filters)}\" " : "";
            return $"{timeArgs} -i \"{path}\" {filterArg} {frameCount} -pix_fmt bgr24 -f image2pipe -vcodec rawvideo -";
        }
        private static List<Bitmap> FramesFromCommand(string command, int width, int height)
        {
            List<Bitmap> result = new();
            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            using var stdout = process.StandardOutput.BaseStream;
            int frameSize = width * height * 3; // 3 bytes per pixel (BGR)
            byte[] buffer = new byte[frameSize];

            while (true)
            {
                int bytesRead = 0;
                while (bytesRead < frameSize)
                {
                    int read = stdout.Read(buffer, bytesRead, frameSize - bytesRead);
                    if (read == 0) // End of stream
                        return result;
                    bytesRead += read;
                }

                var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                var bmpData = bmp.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.WriteOnly,
                    bmp.PixelFormat);

                Marshal.Copy(buffer, 0, bmpData.Scan0, buffer.Length);
                bmp.UnlockBits(bmpData);

                result.Add(bmp);
            }
        }
        //public static string FfmpegCommandSingleFrame(string path, TimeSpan time)
        //{
        //    return $"ffmpeg - ss {time.ToString("c", CultureInfo.InvariantCulture)} - i {path} - frames:v 1 - f rawvideo - pix_fmt bgr24 -";
        //}
        public static VideoInfo GetVideoInfo(string videoPath)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ffprobe",
                Arguments = $"-v error -select_streams v:0 -show_entries stream=width,height,r_frame_rate,duration -of csv=p=0 \"{videoPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            using (var reader = process.StandardOutput)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Could not read video info from ffprobe output.");

                var parts = line.Split(',');
                if (parts.Length != 4)
                    throw new Exception("Unexpected ffprobe output format.");

                int width = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int height = int.Parse(parts[1], CultureInfo.InvariantCulture);

                // Parse framerate
                double framerate;
                var frParts = parts[2].Split('/');
                if (frParts.Length == 2 &&
                    double.TryParse(frParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double num) &&
                    double.TryParse(frParts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double den) &&
                    den != 0)
                {
                    framerate = num / den;
                }
                else if (double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double simpleFps))
                {
                    framerate = simpleFps;
                }
                else
                {
                    throw new Exception($"Could not parse framerate. Ffprobe returned:{Environment.NewLine}{line}");
                }

                // Parse duration
                if (!double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double durationSeconds))
                {
                    //throw new Exception($"Could not parse duration. Ffprobe returned:{Environment.NewLine}{line}");
                    durationSeconds = -1;
                }

                return new VideoInfo
                {
                    Width = width,
                    Height = height,
                    FPS = framerate,
                    DurationSeconds = durationSeconds
                };
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
    public struct VideoInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public double FPS { get; set; }
        public double DurationSeconds { get; set; }
        public double FrameDelay { get => 1000d / FPS; }
        public int FrameCount { get => (int)(FPS * DurationSeconds); }
        public long EstimatedMemoryUsageBytes
        {
            get
            {
                return (long)Width * Height * FrameCount * 4;
            }
        }
        public string DurationString
        {
            get
            {
                string result = "";
                double duration = DurationSeconds;
                if(duration > 3600)
                {
                    result += (int)(duration / 3600) + "h ";
                    duration = duration % 3600;
                }
                if(duration > 60)
                {
                    result += (int)(duration / 60) + "m ";
                    duration = duration % 60;
                }
                result += Math.Round(duration).ToString() + "s";
                return result;
            }
        }
        public override string ToString()
        {
            return $"Resolution: {Width}x{Height}, FPS: {FPS}, Duration: {DurationString}";
        }
        public VideoInfo Copy()
        {
            return new()
            {
                Width = Width,
                Height = Height,
                FPS = FPS,
                DurationSeconds = DurationSeconds
            };
        }
    }
    public struct FrameExtractOptions
    {
        public double? TargetFPS { get; set; }
        public int? MaxSideLength { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? FrameCount { get; set; }
        public override string ToString()
        {
            return $"******" + $"Target FPS: {TargetFPS}{Environment.NewLine}" +
                $"Max side: {MaxSideLength}{Environment.NewLine}" +
                "******";
        }
    }
}
