using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class MyPictureBox : UserControl
    {
        public Image Image { get => pictureBox.Image; set => pictureBox.Image = value; }
        private Point mousePosition, mouseClickedPosition;
        private Rectangle originalLayerBR;
        private float originalLayerRotation, originalFontSize;
        private MainForm mainForm { get => ParentForm as MainForm; }
        private OVector originalLCtM;
        private bool isLMBDown, isRMBDown, isMMBDown;
        public Giffer MainGiffer { get; set; }

        public MyPictureBox()
        {
            InitializeComponent();
            pictureBox.MouseMove += MyPictureBox_MouseMove;
            pictureBox.MouseDown += MyPictureBox_MouseDown;
            pictureBox.MouseUp += MyPictureBox_MouseUp;
        }

        private void MyPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLMBDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isRMBDown = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                isMMBDown = false;
            }
            OnMouseUp(e);
        }

        private void MyPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            GFL cgfl = mainForm.MainLayersPanel.SelectedLayer;
            mouseClickedPosition = e.Location;
            originalLayerBR = cgfl.BoundingRectangle;
            originalLayerRotation = cgfl.Rotation;
            originalLCtM = LayerCenterToMouse();
            mainForm.UpdateTimer.Start();
            if (cgfl.IsTextLayer) originalFontSize = (cgfl as TextGFL).FontSize;
            if (e.Button == MouseButtons.Left)
            {
                isLMBDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isRMBDown = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                isMMBDown = true;
            }
        }

        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (mainForm.MainGiffer == null) return;
            int xDif = e.X - mouseClickedPosition.X;
            int yDif = e.Y - mouseClickedPosition.Y;
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
            double zoom = Zoom();
            OVector currentLCtM = LayerCenterToMouse();
            if (isRMBDown || isLMBDown || isMMBDown)
            {
                GFL gfl = (ParentForm as MainForm).MainLayersPanel.SelectedLayer;
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
                else if (isMMBDown && !mainForm.IsShiftDown)
                {
                    //RESIZE
                    int sizeDif = (int)(currentLCtM.Magnitude - originalLCtM.Magnitude);
                    if (gfl.IsTextLayer)
                    {
                        TextGFL tgfl = gfl as TextGFL;
                        float tSizeDif = mouseClickedPosition.X - e.X;
                        tgfl.FontSize = originalFontSize - tSizeDif / 10;

                    }
                    else
                    {
                        double aspect = (double)originalLayerBR.Width / originalLayerBR.Height;
                        gfl.Position = new Point(originalLayerBR.X - sizeDif, (int)(originalLayerBR.Y - sizeDif / aspect));
                        gfl.Width = originalLayerBR.Width + sizeDif * 2;
                        gfl.Height = (int)((originalLayerBR.Width + sizeDif * 2) / aspect);
                    }
                }
                else if (isMMBDown && mainForm.IsShiftDown)
                {
                    //RESIZE WITHOUT ASPECT
                    if (gfl.IsTextLayer)
                    {
                        //IMPLEMENT LATER MAYBE
                    }
                    else
                    {
                        OVector sdv = new OVector((int)(mousePosition.X - mouseClickedPosition.X), -(int)(mousePosition.Y - mouseClickedPosition.Y));
                        sdv.Rotate(gfl.Rotation);
                        int xSizeDif = (int)sdv.X;
                        int ySizeDif = (int)sdv.Y;
                        Rectangle gflbr = originalLayerBR;
                        gfl.Position = new Point(gflbr.X - xSizeDif, gflbr.Y - ySizeDif);
                        gfl.Width = gflbr.Width + xSizeDif * 2;
                        gfl.Height = gflbr.Height + ySizeDif * 2;
                    }
                }
            }
        }
        private double Zoom()
        {
            int pbHeight = pictureBox.Height;
            int pbWidth = pictureBox.Width;
            int imgHeight = pictureBox.Image.Height;
            int imgWidth = pictureBox.Image.Width;
            double widthScale = (double)pbWidth / (double)imgWidth;
            double heightScale = (double)pbHeight / (double)imgHeight;
            return Math.Min(widthScale, heightScale);
        }
        private OVector LayerCenterToMouse()
        {
            GFL gfl = mainForm.MainLayersPanel.SelectedLayer;
            Point LayerCenter = gfl.Center();
            double pbAspect = (double)pictureBox.Width / pictureBox.Height;
            double frameAspect = (double)mainForm.MainTimelineSlider.SelectedFrame.Width / mainForm.MainTimelineSlider.SelectedFrame.Height;
            int scaledWidth, scaledHeight;
            if (frameAspect > pbAspect)
            {
                scaledWidth = pictureBox.Width;
                scaledHeight = (int)(pictureBox.Width / frameAspect);
            }
            else
            {
                scaledWidth = (int)(pictureBox.Height * frameAspect);
                scaledHeight = pictureBox.Height;
            }
            int horizontalBlankSpace = pictureBox.Width - scaledWidth;
            int verticalBlankSpace = pictureBox.Height - scaledHeight;
            double zoom = Zoom();
            return new OVector(LayerCenter.X * zoom, LayerCenter.Y * zoom).Subtract(new OVector(mousePosition.X - horizontalBlankSpace / 2, mousePosition.Y - verticalBlankSpace / 2));
        }

        public void UpdatePictureBox()
        {
            Image.Dispose();
            Image = mainForm.MainTimelineSlider.SelectedFrame.CompleteBitmap(true);

        }
    }
}
