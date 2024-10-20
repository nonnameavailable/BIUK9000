using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV.Reg;
using System.Linq;
using BIUK9000.GifferComponents;

namespace BIUK9000
{
    public class Facer
    {
        public static Bitmap FaceSwappedImage(Bitmap backgroundImage, Bitmap substituteImage)
        {
            Bitmap result = new Bitmap(backgroundImage);
            using Graphics g = Graphics.FromImage(result);
            List<Rectangle> faceRectangles = FaceRectangles(result);
            foreach (Rectangle faceRectangle in faceRectangles) 
            {
                g.DrawImage(substituteImage, faceRectangle);   
            }
            return result;
        }

        public static List<Rectangle> FaceRectangles(Bitmap bitmap)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;

            // Path to the Haar Cascade file
            string haarCascadePath = Path.Combine(projectDirectory, "BIUK9000", "Resources", "haarcascade_frontalface_default.xml");

            using Image<Bgr, byte> image = bitmap.ToImage<Bgr, byte>();

            // Load the Haar Cascade for face detection
            CascadeClassifier faceCascade = new CascadeClassifier(haarCascadePath);

            // Convert the image to grayscale using CvInvoke
            using Mat grayImage = new Mat();
            CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

            // Apply histogram equalization
            CvInvoke.EqualizeHist(grayImage, grayImage);

            // Detect faces
            Rectangle[] faces = faceCascade.DetectMultiScale(
            grayImage,
            scaleFactor: 1.08,
            minNeighbors: 8,
            minSize: new Size(20, 20),
            maxSize: Size.Empty);
            return faces.ToList();
        }
        public static List<GifFrame> FaceSwappedFrameList(List<GifFrame> frames, Bitmap substituteImage)
        {
            List<GifFrame> result = new();
            for (int i = 0; i < frames.Count; i++)
            {
                GifFrame frame = frames[i];
                result.Add(new GifFrame(FaceSwappedImage(frame.CompleteBitmap(), substituteImage)));
            }
            return result;
        }
    }
}
