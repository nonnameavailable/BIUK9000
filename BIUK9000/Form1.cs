using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnimatedGif;
using System.IO;
using System.Diagnostics;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace BIUK9000
{
    public partial class Form1 : Form
    {
        public PictureBox MainPictureBox { get => mainPictureBox; }
        public Form1()
        {
            InitializeComponent();
            this.MouseEnter += Form1_MouseEnter;
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");
            mainTimelinePanel.AddGifFrames(new Giffer(Path.Combine(imageDirectory, "minions.gif")));
            //Testicek();
            //compressGif();
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            MessageBox.Show("ha");
        }

        public void Testicek()
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");

            string inputPath = imageDirectory + "/dancing.gif";
            string outputPath = imageDirectory + "/output.gif";
            string testOutputPath = imageDirectory + "/testOutput.gif";

            Giffer giffer = new Giffer(inputPath);
            Bitmap faceImage = (Bitmap)Image.FromFile(Path.Combine(imageDirectory, "faces.jpg"));
            giffer.Frames = Facer.FaceSwappedBitmapList(giffer.Frames, faceImage);
            giffer.GifFromFrames().Save(testOutputPath);


        }

        private void compressGif()
        {
            // This will get the current WORKING directory (i.e. \bin\Debug)
            string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");

            string outputPath = imageDirectory + "/output.gif";
            string testOutputPath = imageDirectory + "/testOutput.gif";
            string gifsiclePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "gifsicle.exe");

            string cmd = $"/C {gifsiclePath} -O3 --colors 256 --lossy=30 -o {testOutputPath} {testOutputPath}";
            Process.Start("CMD.exe", cmd);
        }

        static void SaveJpeg(string path, Bitmap img, long quality)
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

        static ImageCodecInfo GetEncoderInfo(string mimeType)
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
