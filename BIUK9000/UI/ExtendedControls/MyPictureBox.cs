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

namespace BIUK9000.UI.ExtendedControls
{
    public class MyPictureBox : PictureBox
    {
        public bool IsLMBDown { get; set; }
        public bool IsRMBDown { get; set; }
        public bool IsMMBDown { get; set; }
        public List<Point> MouseTrace { get; private set; }
        public Point ScaledDragDifference { get; set; }
        public Point MousePositionOnImage
        {
            get
            {
                int[] bsp = BlankSpace();
                int hbp = bsp[0];
                int vbp = bsp[1];

                int x = (int)Math.Round((mousePosition.X - hbp / 2d) / Zoom());
                int y = (int)Math.Round((mousePosition.Y - vbp / 2d) / Zoom());
                return new Point(x, y);
            }
        }
        private Point mouseClickedPosition;
        private Point mousePosition;
        public InterpolationMode InterpolationMode { get; set; }
        public MyPictureBox() : base()
        {
            MouseDown += MyPictureBox_MouseDown;
            MouseUp += MyPictureBox_MouseUp;
            MouseMove += MyPictureBox_MouseMove;
            InterpolationMode = InterpolationMode.HighQualityBicubic;
            MouseTrace = new List<Point>();
        }
        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }

        private void MyPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location;
            if (Image == null) return;
            ScaledDragDifference = new Point((int)((e.X - mouseClickedPosition.X) / Zoom()), (int)((e.Y - mouseClickedPosition.Y) / Zoom()));
            if (IsLMBDown && !IsRMBDown && !IsMMBDown) MouseTrace.Add(MousePositionOnImage);
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
            if (IsLMBDown && !IsRMBDown && !IsMMBDown)
            {
                MouseTrace.Clear();
                MouseTrace.Add(MousePositionOnImage);
            }
        }
        public double Zoom()
        {
            double widthScale = Width / (double)Image.Width;
            double heightScale = Height / (double)Image.Height;
            return Math.Min(widthScale, heightScale);
        }
        public Point PointToMouse(Point p)
        {
            int[] bsp = BlankSpace();
            int horizontalBlankSpace = bsp[0];
            int verticalBlankSpace = bsp[1];
            Point zoomedPoint = new Point((int)(p.X * Zoom()), (int)(p.Y * Zoom()));
            Point zoomedMP = new Point(mousePosition.X - horizontalBlankSpace / 2, mousePosition.Y - verticalBlankSpace / 2);
            return new Point(zoomedPoint.X - zoomedMP.X, zoomedPoint.Y - zoomedMP.Y);
        }
        private int[] BlankSpace()
        {
            int[] result = new int[2];
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
            result[0] = Width - scaledWidth;
            result[1] = Height - scaledHeight;
            return result;
        }
    }
}
