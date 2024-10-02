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
        private bool isDragging;
        private Timer updateTimer;
        public Form1()
        {
            InitializeComponent();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            string imageDirectory = Path.Combine(Directory.GetParent(projectDirectory).FullName, "images");
            MainGiffer = new Giffer(Path.Combine(imageDirectory, "tldr-didnt.gif"));
            mainTimelineSlider.Giffer = MainGiffer;

            mainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;

            isDragging = false;
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

            int pbHeight = mainPictureBox.Height;
            int pbWidth = mainPictureBox.Width;
            int imgHeight = mainPictureBox.Image.Height;
            int imgWidth = mainPictureBox.Image.Width;
            double widthScale = (double)pbWidth / (double)imgWidth;
            double heightScale = (double)pbHeight / (double)imgHeight;
            double zoom = Math.Min(widthScale, heightScale);
            if (isDragging)
            {
                GifFrameLayer gfl = mainLayersPanel.ActiveLayer;
                int newX = (int)(xDif / zoom + originalLayerPosition.X);
                int newY = (int)(yDif / zoom + originalLayerPosition.Y);
                Point pos = new Point(newX, newY);
                gfl.Position = pos;
            }
        }

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            updateTimer.Stop();
            UpdateMainPicturebox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClickedPosition = e.Location;
            originalLayerPosition = mainLayersPanel.ActiveLayer.Position;
            updateTimer.Start();
            if(e.Button == MouseButtons.Left)
            {
                isDragging = true;
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

                return true; // Indicate that the key has been processed
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            UpdateMainPicturebox();
            MainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
        }
        private Stopwatch stopwatch = new Stopwatch();
        private void UpdateMainPicturebox()
        {
            mainPictureBox.Image?.Dispose();
            mainPictureBox.Image = mainTimelineSlider.SelectedFrame.CompleteBitmap();
        }
    }
}
