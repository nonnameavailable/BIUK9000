using AnimatedGif;
using BIUK9000.Dithering;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.GifferComponents
{
    public class Giffer : IDisposable
    {
        public List<GifFrame> Frames { get; set; }
        private Image originalGif;
        private bool disposedValue;
        private bool _createdEmpty;
        private int _nextLayerID;
        public string OriginalImagePath {  get; set; }

        public event EventHandler FrameCountChanged;
        protected virtual void OnFrameCountChanged()
        {
            FrameCountChanged?.Invoke(this, EventArgs.Empty);
        }

        public Giffer(string path)
        {
            _nextLayerID = 0;
            Image gif = Image.FromFile(path);
            originalGif = gif;
            Frames = FramesFromGif(gif);
            _createdEmpty = false;
            OriginalImagePath = path;
        }

        public Giffer()
        {
            _nextLayerID = 0;
            Frames = new List<GifFrame>();
            _createdEmpty = true;
            OriginalImagePath = "";
        }

        public void AddSpace(int up, int right, int down, int left)
        {
            foreach (GifFrame gf in Frames)
            {
                gf.AddSpace(up, right, down, left);
            }
        }
        public void MoveFromOBR(int x, int y)
        {
            Frames.ForEach(frame => frame.MoveFromOBR(x, y));
        }
        public void Resize(int xSizeDif, int ySizeDif)
        {
            Frames.ForEach(frame => frame.Resize(xSizeDif, ySizeDif));
        }
        public void Save()
        {
            Frames.ForEach(frame => frame.Save());
        }

        private List<GifFrame> FramesFromGif(Image gif)
        {
            List<GifFrame> result = new();

            int frameCount = gif.GetFrameCount(FrameDimension.Time);
            int firstLayerID = NextLayerID();
            for (int i = 0; i < frameCount; i++)
            {
                gif.SelectActiveFrame(FrameDimension.Time, i);
                result.Add(new GifFrame(new Bitmap(gif), FrameDelay(gif), firstLayerID));
            }
            return result;
        }

        private int FrameDelay(Image gif)
        {
            if (_createdEmpty) return 20;
            PropertyItem propertyItem = gif.GetPropertyItem(0x5100);
            return BitConverter.ToInt32(propertyItem.Value, 0) * 10;
        }

        public Image GifFromFrames()
        {
            MemoryStream stream = new MemoryStream();
            int frameDelay = FrameDelay(originalGif);
            using AnimatedGifCreator agc = new AnimatedGifCreator(stream, frameDelay);
            foreach (GifFrame frame in Frames)
            {
                agc.AddFrame(frame.CompleteBitmap(false), frame.FrameDelay, GifQuality.Bit8);
            }
            return Image.FromStream(stream);
        }
        public Image GifFromFrames(List<Color> paletteForDithering)
        {
            MemoryStream stream = new MemoryStream();
            int frameDelay = FrameDelay(originalGif);
            AnimatedGifCreator agc = new AnimatedGifCreator(stream, frameDelay);
            foreach (GifFrame frame in Frames)
            {
                Bitmap cbm = frame.CompleteBitmap(false);
                Ditherer dtr = new Ditherer(cbm);
                cbm = dtr.DitheredBitmap(paletteForDithering);
                agc.AddFrame(cbm, frame.FrameDelay, GifQuality.Bit8);
                dtr.Dispose();
            }
            return Image.FromStream(stream);
        }
        public void AddFrame(GifFrame frame)
        {
            Frames.Add(frame);
            OnFrameCountChanged();
        }
        public void RemoveFrame(GifFrame frame)
        {
            Frames.Remove(frame);
            frame.Dispose();
            OnFrameCountChanged();
        }
        public void Crop(GifFrame frameWithCropLayer)
        {
            GFL cl = frameWithCropLayer.Layers.Last();
            if (cl is not CropGFL) return;
            Rectangle newRectangle = cl.BoundingRectangle;
            foreach(GifFrame frame in Frames)
            {
                foreach (GFL layer in frame.Layers)
                {
                    layer.Move(-newRectangle.X + layer.Position.Xint, -newRectangle.Y + layer.Position.Yint);
                }
                frame.Width = newRectangle.Width;
                frame.Height = newRectangle.Height;
                //frame.AddSpace(-newRectangle.Y, newRectangle.Right - frame.Width, newRectangle.Bottom - frame.Height, -newRectangle.X);
            }
            frameWithCropLayer.RemoveLayer(cl);
        }
        public void AddGifferAsLayers(Giffer newGiffer)
        {
            int nextLayerID = NextLayerID();
            for(int i = 0; i < Frames.Count; i++)
            {
                int newGifferIndex = (int)(i / (double)Frames.Count * newGiffer.Frames.Count);
                GifFrame cgf = Frames[i];
                cgf.AddLayer(newGiffer.Frames[newGifferIndex].CompleteBitmap(false), nextLayerID);
            }
        }
        public int NextLayerID()
        {
            return _nextLayerID++;
        }
        public ColorPalette GifColorPalette()
        {
            return originalGif.Palette;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    for(int i = 0;  i < Frames.Count; i++)
                    {
                        Frames[i].Dispose();
                        Frames[i] = null;
                    }
                    originalGif.Dispose();
                    originalGif = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        //private void test()
        //{
        //    //Variable declaration
        //    StringCollection stringCollection;
        //    MemoryStream memoryStream;
        //    BinaryWriter binaryWriter;
        //    Image image;
        //    Byte[] buf1;
        //    Byte[] buf2;
        //    Byte[] buf3;
        //    //Variable declaration

        //    stringCollection = a_StringCollection_containing_images;

        //    Response.ContentType = "Image/gif";
        //    memoryStream = new MemoryStream();
        //    buf2 = new Byte[19];
        //    buf3 = new Byte[8];
        //    buf2[0] = 33;  //extension introducer
        //    buf2[1] = 255; //application extension
        //    buf2[2] = 11;  //size of block
        //    buf2[3] = 78;  //N
        //    buf2[4] = 69;  //E
        //    buf2[5] = 84;  //T
        //    buf2[6] = 83;  //S
        //    buf2[7] = 67;  //C
        //    buf2[8] = 65;  //A
        //    buf2[9] = 80;  //P
        //    buf2[10] = 69; //E
        //    buf2[11] = 50; //2
        //    buf2[12] = 46; //.
        //    buf2[13] = 48; //0
        //    buf2[14] = 3;  //Size of block
        //    buf2[15] = 1;  //
        //    buf2[16] = 0;  //
        //    buf2[17] = 0;  //
        //    buf2[18] = 0;  //Block terminator
        //    buf3[0] = 33;  //Extension introducer
        //    buf3[1] = 249; //Graphic control extension
        //    buf3[2] = 4;   //Size of block
        //    buf3[3] = 9;   //Flags: reserved, disposal method, user input, transparent color
        //    buf3[4] = 10;  //Delay time low byte
        //    buf3[5] = 3;   //Delay time high byte
        //    buf3[6] = 255; //Transparent color index
        //    buf3[7] = 0;   //Block terminator
        //    binaryWriter = new BinaryWriter(Response.OutputStream);
        //    for (int picCount = 0; picCount < stringCollection.Count; picCount++)
        //    {
        //        image = Bitmap.FromFile(stringCollection[picCount]);
        //        image.Save(memoryStream, ImageFormat.Gif);
        //        buf1 = memoryStream.ToArray();

        //        if (picCount == 0)
        //        {
        //            //only write these the first time....
        //            binaryWriter.Write(buf1, 0, 781); //Header & global color table
        //            binaryWriter.Write(buf2, 0, 19); //Application extension
        //        }

        //        binaryWriter.Write(buf3, 0, 8); //Graphic extension
        //        binaryWriter.Write(buf1, 789, buf1.Length - 790); //Image data

        //        if (picCount == stringCollection.Count - 1)
        //        {
        //            //only write this one the last time....
        //            binaryWriter.Write(";"); //Image terminator
        //        }

        //        memoryStream.SetLength(0);
        //    }
        //    binaryWriter.Close();
        //    Response.End();
        //}
    }
}
