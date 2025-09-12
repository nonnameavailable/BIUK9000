using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.MyGraphics
{
    public class OBIMP
    {
        public static void CompressGif(string originalPath, string targetPath, int colors, int lossy)
        {
            //string gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle.exe");

            bool is64Bit = nint.Size == 8;
            string gifsiclePath;

            if (is64Bit)
            {
                gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle64.exe");
            }
            else
            {
                gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle32.exe");
            }

            if (!File.Exists(gifsiclePath))
            {
                throw new FileNotFoundException("Gifsicle executable not found.", gifsiclePath);
            }

            string message = "Gifsicle is compressing the file";
            string cmd = $"/C echo {message} && {gifsiclePath} -O3 --colors {colors} --lossy={lossy} -o \"{targetPath}\" \"{originalPath}\"";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "CMD.exe",
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false // Set to false if you want to see the CMD window
                }
            };

            process.Start();
            process.WaitForExit(); // Wait for the process to complete

            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                MessageBox.Show("Gifsicle error, file will not be compressed because:" + Environment.NewLine + error);
            }
        }

        public static void SaveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeType)
                    return codec;
            }
            return null;
        }
        public static Bitmap BitmapFromByte(byte[] b)
        {
            using MemoryStream ms = new MemoryStream(b);
            return (Bitmap)Image.FromStream(ms);
        }
        public static Bitmap ReducedSizeBitmap(Bitmap bmp, int maxSideLength)
        {
            int largerSide = Math.Max(bmp.Width, bmp.Height);
            if (largerSide <= maxSideLength) return new Bitmap(bmp);
            double multiplier = maxSideLength / (double)largerSide;
            int newWidth = (int)(bmp.Width * multiplier);
            int newHeight = (int)(bmp.Height * multiplier);
            return new Bitmap(bmp, newWidth, newHeight);
        }
    }
}
