using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public class OBIMP
    {
        public static void CompressGif(string originalPath, string targetPath, int colors, int lossy)
        {
            string gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle.exe");

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
    }
}
