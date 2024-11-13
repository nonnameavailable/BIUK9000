using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public class MyPictureBox : PictureBox
    {
        public bool IsLMBDown {  get; set; }
        public bool IsRMBDown { get; set; }
        public bool IsMMBDown { get; set; }
        public Point ScaledDragDifference { get; set; }
        private Point mouseClickedPosition;
        private Point mousePosition;
        public InterpolationMode InterpolationMode { get; set; }
        public MyPictureBox() : base()
        {
            MouseDown += MyPictureBox_MouseDown;
            MouseUp += MyPictureBox_MouseUp;
            MouseMove += MyPictureBox_MouseMove;
            InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }

        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            mousePosition = e.Location;
            ScaledDragDifference = new Point((int)((e.X - mouseClickedPosition.X) / Zoom()),(int)((e.Y - mouseClickedPosition.Y) / Zoom()));
        }

        private void MyPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            if (e.Button == MouseButtons.Left)
            {
                IsLMBDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                IsRMBDown = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                IsMMBDown = false;
            }
        }

        private void MyPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Image == null) return;
            mouseClickedPosition = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                IsLMBDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                IsRMBDown = true;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                IsMMBDown = true;
            }
        }
        public double Zoom()
        {
            int iMainGifferHeight = Image.Height;
            int iMainGifferWidth = Image.Width;
            double widthScale = (double)Width / (double)iMainGifferWidth;
            double heightScale = (double)Height / (double)iMainGifferHeight;
            return Math.Min(widthScale, heightScale);
        }
        public Point PointToMouse(Point p)
        {
            double pbAspect = Width / (double)Height;
            double frameAspect = (double)Image.Width / Image.Height;
            int scaledWidth, scaledHeight;
            if (frameAspect > pbAspect)
            {
                scaledWidth = Width;
                scaledHeight = (int)(Width / frameAspect);
            }
            else
            {
                scaledWidth = (int)(Height * frameAspect);
                scaledHeight = Height;
            }
            int horizontalBlankSpace = Width - scaledWidth;
            int verticalBlankSpace = Height - scaledHeight;
            Point zoomedPoint = new Point((int)(p.X * Zoom()), (int)(p.Y * Zoom()));
            Point zoomedMP = new Point(mousePosition.X - horizontalBlankSpace / 2, mousePosition.Y - verticalBlankSpace / 2);
            return new Point(zoomedPoint.X - zoomedMP.X, zoomedPoint.Y - zoomedMP.Y);
        }
    }
}
