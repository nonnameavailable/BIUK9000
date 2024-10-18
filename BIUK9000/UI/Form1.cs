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
using Microsoft.VisualBasic;

namespace BIUK9000.UI
{
    public partial class Form1 : Form
    {
        public LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public Giffer MainGiffer { get; set; }
        private Point mousePosition, mouseClickedPosition;
        private Rectangle originalLayerBR;
        private float originalLayerRotation;
        private OVector originalLCtM;
        private Timer updateTimer;
        private bool isLMBDown, isRMBDown, isMMBDown, isShiftDown;
        public Form1()
        {
            InitializeComponent();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");
            MainGiffer = new Giffer(Path.Combine(imageDirectory, "tldr-didnt.gif"));
            mainTimelineSlider.Giffer = MainGiffer;

            mainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;

            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;

            mainPictureBox.Image = MainGiffer.Frames[0].CompleteBitmap();
            updateTimer = new Timer();
            updateTimer.Interval = 17;
            updateTimer.Tick += UpdateTimer_Tick;

            foreach (GifFrame gifFrame in MainGiffer.Frames)
            {
                gifFrame.LayerCountChanged += GifFrame_LayerCountChanged;
            }
            mainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
            this.KeyPreview = true;

        }

        private void GifFrame_LayerCountChanged(object sender, EventArgs e)
        {
            UpdateMainPicturebox();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateMainPicturebox();
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            int xDif = e.X - mouseClickedPosition.X;
            int yDif = e.Y - mouseClickedPosition.Y;
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
            double zoom = Zoom();
            OVector currentLCtM = LayerCenterToMouse();
            if (isRMBDown || isLMBDown || isMMBDown)
            {
                GifFrameLayer gfl = mainLayersPanel.ActiveLayer;
                if (isLMBDown && !isRMBDown)
                {
                    //MOVE
                    int newX = (int)(xDif / zoom + originalLayerBR.X);
                    int newY = (int)(yDif / zoom + originalLayerBR.Y);
                    Point pos = new Point(newX, newY);
                    gfl.Position = pos;
                }
                else if (!isLMBDown && isRMBDown)
                {
                    //ROTATE
                    double angle = currentLCtM.RotationInDegrees;
                    gfl.Rotation = originalLayerRotation + (float)angle - (float)originalLCtM.RotationInDegrees;
                }
                else if (isMMBDown && !isShiftDown)
                {
                    //RESIZE
                    int sizeDif = (int)(currentLCtM.Magnitude - originalLCtM.Magnitude);
                    double aspect = (double)gfl.OriginalBitmap.Width / gfl.OriginalBitmap.Height;
                    gfl.BoundingRectangle = new Rectangle(originalLayerBR.X - sizeDif, (int)(originalLayerBR.Y - sizeDif / aspect), originalLayerBR.Width + sizeDif * 2, (int)((originalLayerBR.Width + sizeDif * 2) / aspect));
                } else if (isMMBDown && isShiftDown)
                {
                    //RESIZE WITHOUT ASPECT
                    Debug.Print("shiftpressed");
                    int xSizeDif = (int)(mousePosition.X - mouseClickedPosition.X);
                    int ySizeDif = (int)(mousePosition.Y - mouseClickedPosition.Y);
                    Rectangle gflbr = gfl.BoundingRectangle;
                    gfl.BoundingRectangle = new Rectangle(gflbr.X - xSizeDif, gflbr.Y - ySizeDif, gflbr.Width + xSizeDif * 2, gflbr.Height + ySizeDif * 2);
                }
            }

        }
        private double Zoom()
        {
            int pbHeight = mainPictureBox.Height;
            int pbWidth = mainPictureBox.Width;
            int imgHeight = mainPictureBox.Image.Height;
            int imgWidth = mainPictureBox.Image.Width;
            double widthScale = (double)pbWidth / (double)imgWidth;
            double heightScale = (double)pbHeight / (double)imgHeight;
            return Math.Min(widthScale, heightScale);
        }
        private OVector LayerCenterToMouse()
        {
            GifFrameLayer gfl = MainLayersPanel.ActiveLayer;
            Point LayerCenter = gfl.Center;
            double pbAspect = (double)mainPictureBox.Width / mainPictureBox.Height;
            double frameAspect = (double)mainTimelineSlider.SelectedFrame.Width / mainTimelineSlider.SelectedFrame.Height;
            int scaledWidth, scaledHeight;
            if (frameAspect > pbAspect)
            {
                scaledWidth = mainPictureBox.Width;
                scaledHeight = (int)(mainPictureBox.Width / frameAspect);
            }
            else
            {
                scaledWidth = (int)(mainPictureBox.Height * frameAspect);
                scaledHeight = mainPictureBox.Height;
            }
            int horizontalBlankSpace = mainPictureBox.Width - scaledWidth;
            int verticalBlankSpace = mainPictureBox.Height - scaledHeight;
            double zoom = Zoom();
            return new OVector(LayerCenter.X * zoom, LayerCenter.Y * zoom).Subtract(new OVector(mousePosition.X - horizontalBlankSpace / 2, mousePosition.Y - verticalBlankSpace / 2));
        }
        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                isLMBDown = false;
            } else if (e.Button == MouseButtons.Right)
            {
                isRMBDown = false;
            } else if (e.Button == MouseButtons.Middle)
            {
                isMMBDown = false;
            }
            if (!isLMBDown && !isRMBDown)
            {
                updateTimer.Stop();
            }
            UpdateMainPicturebox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClickedPosition = e.Location;
            originalLayerBR = mainLayersPanel.ActiveLayer.BoundingRectangle;
            originalLayerRotation = mainLayersPanel.ActiveLayer.Rotation;
            originalLCtM = LayerCenterToMouse();
            updateTimer.Start();
            if (e.Button == MouseButtons.Left)
            {
                isLMBDown = true;
            } else if (e.Button == MouseButtons.Right)
            {
                isRMBDown = true;
            } else if (e.Button == MouseButtons.Middle)
            {
                isMMBDown = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            const int WM_KEYUP = 0x101;
            const int WM_SYSKEYUP = 0x105;
            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                if (keyData == Keys.Right)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value < mts.Maximum) mts.Value += 1;
                }
                else if (keyData == Keys.Left)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value > 0) mts.Value -= 1;
                } else if ((keyData & Keys.Shift) == Keys.Shift)
                {
                    isShiftDown = true;
                }
            } else if (msg.Msg == WM_KEYUP ||  msg.Msg == WM_SYSKEYUP)
            {
                MessageBox.Show("keyup");
                if((keyData & Keys.Shift) == Keys.Shift)
                {
                    isShiftDown = false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            UpdateMainPicturebox();
            MainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
        }
        private void UpdateMainPicturebox()
        {
            mainPictureBox.Image?.Dispose();
            mainPictureBox.Image = mainTimelineSlider.SelectedFrame.CompleteBitmap();
        }
    }
}
