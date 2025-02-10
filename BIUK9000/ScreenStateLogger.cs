using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace BIUK9000
{
    public class ScreenStateLogger
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FPS { get; set; }
        public List<Bitmap> Frames { get; set; }

        public int Size { get; private set; }
        public ScreenStateLogger()
        {
            X = 0;
            Y = 0;
            Width = 50;
            Height = 50;
            FPS = 5;
            Frames = new List<Bitmap>();
        }
        private SharpDX.Direct3D11.Device _device;
        private Output _output;
        private Output1 _output1;
        private Texture2DDescription _textureDesc;
        private Texture2D _screenTexture;
        private System.Threading.Timer _timer;
        private OutputDuplication _duplicatedOutput;

        public void Start(int currentScreenIndex)
        {
            _duplicatedOutput?.Dispose();
            var factory = new Factory1();
            //Get first adapter
            var adapter = factory.GetAdapter1(0);
            //Get device from adapter
            _device = new SharpDX.Direct3D11.Device(adapter);
            Debug.Print(currentScreenIndex.ToString());
            int outputNumber = (currentScreenIndex == -1 || currentScreenIndex > (adapter.GetOutputCount() - 1)) ? 0 : currentScreenIndex;
            //Get front buffer of the adapter
            _output = adapter.GetOutput(outputNumber);
            _output1 = _output.QueryInterface<Output1>();

            // Width/Height of desktop to capture
            int width = _output.Description.DesktopBounds.Right;
            int height = _output.Description.DesktopBounds.Bottom;

            // Create Staging texture CPU-accessible
            _textureDesc = new Texture2DDescription
            {
                CpuAccessFlags = CpuAccessFlags.Read,
                BindFlags = BindFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Width = width,
                Height = height,
                OptionFlags = ResourceOptionFlags.None,
                MipLevels = 1,
                ArraySize = 1,
                SampleDescription = { Count = 1, Quality = 0 },
                Usage = ResourceUsage.Staging
            };
            _screenTexture = new Texture2D(_device, _textureDesc);
            _duplicatedOutput = _output1.DuplicateOutput(_device);
            _timer = new System.Threading.Timer(TimerTick, null, 0, 1000/FPS);
        }

        private void TimerTick(Object o)
        {
            int xb = X;
            int yb = Y;
            try
            {
                SharpDX.DXGI.Resource screenResource;
                OutputDuplicateFrameInformation duplicateFrameInformation;

                // Try to get duplicated frame within given time is ms
                _duplicatedOutput.TryAcquireNextFrame(50, out duplicateFrameInformation, out screenResource);
                if (screenResource == null) return;
                // copy resource into memory that can be accessed by the CPU
                using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                    _device.ImmediateContext.CopyResource(screenTexture2D, _screenTexture);

                // Get the desktop capture texture
                var mapSource = _device.ImmediateContext.MapSubresource(_screenTexture, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                // Create Drawing.Bitmap
                var bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                var boundsRect = new Rectangle(0, 0, Width, Height);

                //// Copy pixels from screen capture Texture to GDI bitmap
                var mapDest = bitmap.LockBits(boundsRect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
                var sourcePtr = mapSource.DataPointer;
                var destPtr = mapDest.Scan0;

                int startPixel = xb;
                int pixelsToCopy = Width;
                int bytesPerPixel = Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int sourceStride = mapSource.RowPitch;
                int destStride = mapDest.Stride;

                sourcePtr = IntPtr.Add(sourcePtr, (int)((long)sourceStride * yb));
                //unsafe
                //{
                //    byte* srcPtr = (byte*)sourcePtr.ToPointer();
                //    byte* dstPtr = (byte*)destPtr.ToPointer();

                //    for (int y = 0; y < bitmap.Height; y++)
                //    {
                //        byte* sourceRow = srcPtr + y * sourceStride + startPixel * bytesPerPixel;
                //        byte* destRow = dstPtr + y * destStride;

                //        System.Buffer.MemoryCopy(sourceRow, destRow, pixelsToCopy * bytesPerPixel, pixelsToCopy * bytesPerPixel);
                //    }
                //}
                for (int y = 0; y < bitmap.Height; y++)
                {
                    IntPtr sourceRowPtr = IntPtr.Add(sourcePtr, y * sourceStride + startPixel * bytesPerPixel);
                    IntPtr destRowPtr = IntPtr.Add(destPtr, y * destStride);

                    byte[] rowData = new byte[pixelsToCopy * bytesPerPixel];
                    System.Runtime.InteropServices.Marshal.Copy(sourceRowPtr, rowData, 0, rowData.Length);
                    System.Runtime.InteropServices.Marshal.Copy(rowData, 0, destRowPtr, rowData.Length);
                }
                bitmap.UnlockBits(mapDest);
                _device.ImmediateContext.UnmapSubresource(_screenTexture, 0);
                Frames.Add(bitmap);
                screenResource.Dispose();
                _duplicatedOutput.ReleaseFrame();
            }
            catch (SharpDXException e)
            {
                if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {
                    Trace.TraceError(e.Message);
                    Trace.TraceError(e.StackTrace);
                }
            }
        }
        public void ClearFrames()
        {
            for (int i = 0; i < Frames.Count; i++)
            {
                Frames[i].Dispose();
            }
            Frames.Clear();
        }

        public void Stop()
        {
            //_run = false;
            _timer?.Dispose();
            if(Frames.Count > 0)
            {
                Frames[0].Dispose();
                Frames.RemoveAt(0);
            }

        }

        //public event EventHandler<byte[]> ScreenRefreshed;
    }
}
