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
using BIUK9000.GifferComponents;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public Giffer MainGiffer { get; set; }
        private Point mousePosition, mouseClickedPosition;
        private Rectangle originalLayerBR;
        private float originalLayerRotation;
        private OVector originalLCtM;
        private Timer updateTimer;
        private bool isLMBDown, isRMBDown, isMMBDown, isShiftDown;
        public MainForm()
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
                GFL gfl = mainLayersPanel.ActiveLayer;
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
                    double angle = currentLCtM.Rotation;
                    gfl.Rotation = originalLayerRotation + (float)angle - (float)originalLCtM.Rotation;
                }
                else if (isMMBDown && !isShiftDown)
                {
                    //RESIZE
                    int sizeDif = (int)(currentLCtM.Magnitude - originalLCtM.Magnitude);
                    double aspect = (double)originalLayerBR.Width / originalLayerBR.Height;
                    gfl.BoundingRectangle = new Rectangle(originalLayerBR.X - sizeDif, (int)(originalLayerBR.Y - sizeDif / aspect), originalLayerBR.Width + sizeDif * 2, (int)((originalLayerBR.Width + sizeDif * 2) / aspect));
                } else if (isMMBDown && isShiftDown)
                {
                    //RESIZE WITHOUT ASPECT
                    OVector sdv = new OVector((int)(mousePosition.X - mouseClickedPosition.X), -(int)(mousePosition.Y - mouseClickedPosition.Y));
                    sdv.Rotate(gfl.Rotation);
                    int xSizeDif = (int)sdv.X;
                    int ySizeDif = (int)sdv.Y;
                    Rectangle gflbr = originalLayerBR;
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
            GFL gfl = MainLayersPanel.ActiveLayer;
            Point LayerCenter = gfl.Center();
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
        protected override bool ProcessKeyPreview(ref Message m)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value < mts.Maximum) mts.Value += 1;
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value > 0) mts.Value -= 1;
                    return true;
                }
                else if (keyData == Keys.ShiftKey)
                {
                    isShiftDown = true;
                    return true;
                } else if(keyData == Keys.T)
                {
                    TextGFL tgfl = new TextGFL("ffa" + System.Environment.NewLine + "herffasdfasdfasdfas" + System.Environment.NewLine + "her" + System.Environment.NewLine + "her" + System.Environment.NewLine + "her");
                    tgfl.Font = "Impact";
                    tgfl.FontBorderColor = Color.Black;
                    tgfl.FontColor = Color.White;
                    tgfl.FontBorderWidth = 5;
                    tgfl.FontSize = 20;
                    mainTimelineSlider.SelectedFrame.AddLayer(tgfl);
                } else if(keyData == Keys.L)
                {
                    //Bitmap bmp = new Bitmap(500, 500);
                    //using Graphics g = Graphics.FromImage(bmp);
                    //g.Clear(Color.Black);
                    //g.DrawString("fuuuuuuuuu", new Font("Impact", 50), new SolidBrush(Color.White), new Point(0, 0));
                    //Size s = TextRenderer.MeasureText("fuuuuuuuuu", new Font("Impact", 50));
                    //g.DrawRectangle(Pens.Red, 0, 0, s.Width, s.Height);
                    //mainPictureBox.Image = bmp;

                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {
                    
                    isShiftDown = false;
                    return true;
                }
            }

            return base.ProcessKeyPreview(ref m);
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
            using Graphics g = Graphics.FromImage(mainPictureBox.Image);
            g.DrawLine(Pens.Red, new Point(0, 0), new Point(0, 0));
        }
    }
}
