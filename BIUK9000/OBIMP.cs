using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000
{
    public class OBIMP
    {
        public void compressGif(string originalGifPath)
        {
            string ogpDirectory = Path.GetDirectoryName(originalGifPath);
            string ogpFileName = Path.GetFileNameWithoutExtension(originalGifPath);
            string compressedGifPath = Path.Combine(ogpDirectory, ogpFileName + "_compressed.gif");

            string gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle.exe");

            string cmd = $"/C {gifsiclePath} -O3 --colors 256 --lossy=30 -o {compressedGifPath} {originalGifPath}";
            Process.Start("CMD.exe", cmd);
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
