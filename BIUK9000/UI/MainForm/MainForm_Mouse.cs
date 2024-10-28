using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    partial class MainForm
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        private Point prevMousePosition, mousePosition, mouseClickedPosition;
        private float originalLayerRotation;
        private OVector originalLCtM;
        private bool isLMBDown, isRMBDown, isMMBDown;

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
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
            if (!isLMBDown && !isMMBDown & !isRMBDown)
            {
                UpdateTimer.Stop();
            }
            ApplyCurrentLayerParamsToSubsequentLayers();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            GFL cgfl = SelectedLayer;
            MainGiffer.Save();
            mouseClickedPosition = e.Location;
            originalLayerRotation = cgfl.Rotation;
            originalLCtM = LayerCenterToMouse();
            UpdateTimer.Start();
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

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            mousePosition = new Point(e.X, e.Y);
            int xDragDif = mousePosition.X - mouseClickedPosition.X;
            int yDragDif = mousePosition.Y - mouseClickedPosition.Y;
            int xMoveDif = mousePosition.X - prevMousePosition.X;
            int yMoveDif = mousePosition.Y - prevMousePosition.Y;

            double zoom = Zoom();
            OVector currentLCtM = LayerCenterToMouse();
            if (isRMBDown || isLMBDown || isMMBDown)
            {
                GFL gfl = SelectedLayer;
                if (isLMBDown && !isRMBDown)
                {
                    //MOVE
                    if (IsCtrlDown)
                    {
                        //MOVE WHOLE GIF (ALL LAYERS, ALL FRAMES)
                        //SelectedFrame.MoveFromOBR((int)(xDragDif / zoom), (int)(yDragDif / zoom));
                        MainGiffer.MoveFromOBR((int)(xDragDif / zoom), (int)(yDragDif / zoom));
                    }
                    else
                    {
                        //MOVE JUST LAYER
                        gfl.MoveFromOBR((int)(xDragDif / zoom), (int)(yDragDif / zoom));
                    }
                    //gfl.Move((int)(xMoveDif / zoom), (int)(yMoveDif / zoom));
                }
                else if (!isLMBDown && isRMBDown)
                {
                    //ROTATE
                    double angle = currentLCtM.Rotation;
                    gfl.Rotation = SnappedRotation(originalLayerRotation + (float)angle - (float)originalLCtM.Rotation, 20);
                }
                else if (isMMBDown)
                {
                    //RESIZE
                    OVector sdv = new OVector((int)(mousePosition.X - mouseClickedPosition.X), -(int)(mousePosition.Y - mouseClickedPosition.Y));
                    int xSizeDif = (int)sdv.X;
                    int ySizeDif = (int)sdv.Y;
                    if (IsCtrlDown)
                    {
                        //RESIZE GIF FREE (ALL FRAMES)
                        MainGiffer.Resize(xSizeDif, ySizeDif);
                    }
                    else
                    {
                        //RESIZE LAYER
                        if (!IsShiftDown)
                        {
                            //RESIZE LAYER KEEP RATIO
                            int sizeDif = (int)(currentLCtM.Magnitude - originalLCtM.Magnitude);
                            gfl.Resize(sizeDif);
                        }
                        else
                        {
                            //RESIZE LAYER FREE
                            sdv.Rotate(gfl.Rotation);
                            gfl.Resize(xSizeDif, ySizeDif);
                        }
                    }


                }
            }
            prevMousePosition = new Point(mousePosition.X, mousePosition.Y);
        }
        private double Zoom()
        {
            int pbHeight = mainPictureBox.Height;
            int pbWidth = mainPictureBox.Width;
            int iMainGifferHeight = mainPictureBox.Image.Height;
            int iMainGifferWidth = mainPictureBox.Image.Width;
            double widthScale = (double)pbWidth / (double)iMainGifferWidth;
            double heightScale = (double)pbHeight / (double)iMainGifferHeight;
            return Math.Min(widthScale, heightScale);
        }
        private OVector LayerCenterToMouse()
        {
            GFL gfl = MainLayersPanel.SelectedLayer;
            Point LayerCenter = gfl.Center();
            double pbAspect = (double)mainPictureBox.Width / mainPictureBox.Height;
            double frameAspect = (double)MainTimelineSlider.SelectedFrame.Width / MainTimelineSlider.SelectedFrame.Height;
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

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = MainTimelineSlider.SelectedFrame.CompleteBitmap(true);
        }
        private float SnappedRotation(float rotation, float snapAngle)
        {
            if (rotation < 0) rotation += 360;
            float roundedRotation = (float)(Math.Round(rotation / 90f, 0) * 90);
            float mod90 = rotation % 90;
            if (mod90 > 90 - snapAngle || mod90 < snapAngle)
            {
                return roundedRotation;
            }
            else
            {
                return rotation;
            }
        }

    }
}
