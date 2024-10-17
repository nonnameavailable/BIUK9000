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

namespace BIUK9000.UI
{
    public partial class Form1 : Form
    {
        public LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public Giffer MainGiffer { get; set; }
        private Point mousePosition, mouseClickedPosition, originalLayerPosition;
        private float originalLayerRotation, mouseClickedRotation;
        private OVector originalLCtM;
        private Timer updateTimer;
        private bool isLMBDown, isRMBDown;
        public Form1()
        {
            InitializeComponent();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");
            MainGiffer = new Giffer(Path.Combine(imageDirectory, "tldr-didnt.gif"));
            mainTimelineSlider.Giffer = MainGiffer;
            isRMBDown = false;
            isLMBDown = false;

            mainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;

            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;

            mainPictureBox.Image = MainGiffer.Frames[0].CompleteBitmap();
            updateTimer = new Timer();
            updateTimer.Interval = 17;
            updateTimer.Tick += UpdateTimer_Tick;

            foreach(GifFrame gifFrame in MainGiffer.Frames)
            {
                gifFrame.LayerCountChanged += GifFrame_LayerCountChanged;
            }
            mainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
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
            if(isRMBDown || isLMBDown)
            {
                GifFrameLayer gfl = mainLayersPanel.ActiveLayer;
                if (isLMBDown && !isRMBDown)
                {
                    //MOVE
                    int newX = (int)(xDif / zoom + originalLayerPosition.X);
                    int newY = (int)(yDif / zoom + originalLayerPosition.Y);
                    Point pos = new Point(newX, newY);
                    gfl.Position = pos;
                }
                else if (!isLMBDown && isRMBDown)
                {
                    //ROTATE
                    double angle = LayerCenterToMouse().RotationInDegrees;
                    gfl.Rotation = originalLayerRotation + (float)angle - (float)mouseClickedRotation;
                }
                else if (isLMBDown && isRMBDown)
                {
                    //RESIZE
                    //Debug.Print((LayerCenterToMouse().Magnitude / originalLCtM.Magnitude).ToString());

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
            updateTimer.Stop();
            if(e.Button == MouseButtons.Left )
            {
                isLMBDown = false;
            } else if(e.Button == MouseButtons.Right)
            {
                isRMBDown = false;
            }

            UpdateMainPicturebox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClickedPosition = e.Location;
            originalLayerPosition = mainLayersPanel.ActiveLayer.Position;
            originalLayerRotation = mainLayersPanel.ActiveLayer.Rotation;
            originalLCtM = LayerCenterToMouse();
            updateTimer.Start();
            mouseClickedRotation = (float)LayerCenterToMouse().RotationInDegrees;
            if(e.Button == MouseButtons.Left)
            {
                isLMBDown = true;
            } else if(e.Button == MouseButtons.Right)
            {
                isRMBDown = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
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
                }
                else if (keyData == Keys.P)
                {
                    mainTimelineSlider.SelectedFrame.AddLayer(50, 50);
                }
                else if (keyData == Keys.A)
                {
                    //mainTimelineSlider.Giffer.AddSpace(0, 0, 0, 50); //FUNGUJE
                    //MainLayersPanel.ActiveLayer.Rotation += 10; //FUNGUJE
                }

                return true; // Indicate that the key has been processed
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
