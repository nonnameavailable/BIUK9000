﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.GifferComponents;
using BIUK9000.Properties;

namespace BIUK9000.UI
{
    public partial class LayerHolder : UserControl
    {
        public bool StayHighlighted { get; set; }
        private OVector OriginalMousePosition { get; set; }
        public event EventHandler LayerClicked;
        public event EventHandler LayerVisibilityChanged;
        public event DragEventHandler DragDropped;
        public event EventHandler DeleteButtonClicked;
        public event EventHandler SnappedLayerToFrame;
        public event EventHandler RestoredRatio;
        private bool _isLmbDown, _visible;
        public LayerHolder(Bitmap bitmap, bool visible)
        {
            InitializeComponent();
            _visible = visible;
            mainPictureBox.MouseEnter += MainPictureBox_MouseEnter;
            mainPictureBox.MouseLeave += MainPictureBox_MouseLeave;
            mainPictureBox.MouseClick += (sender, args) => LayerClicked?.Invoke(this, EventArgs.Empty);
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            mainPictureBox.Image = new Bitmap(bitmap);
            DragDrop += (sender, args) => DragDropped?.Invoke(this, args);
            DragEnter += LayerHolder_DragEnter;
            StayHighlighted = false;
            this.AllowDrop = true;
            if (visible)
            {
                visibleButton.Image = Resources.eye_open_icon;
            } else
            {
                visibleButton.Image = Resources.eye_closed_icon;
            }
            visibleButton.Click += VisibleButton_Click;
            deleteButton.Image = Resources.dustbin_icon;
            deleteButton.Click += (sender, args) => DeleteButtonClicked?.Invoke(this, args);
            snapToFrameMI.Click += (sender, args) => SnappedLayerToFrame?.Invoke(this, EventArgs.Empty);
            restoreRatioMI.Click += (sender, args) => RestoredRatio?.Invoke(this, EventArgs.Empty);
        }

        private void VisibleButton_Click(object sender, EventArgs e)
        {
            if (_visible)
            {
                visibleButton.Image = Resources.eye_closed_icon;
                _visible = false;
            }
            else
            {
                visibleButton.Image = Resources.eye_open_icon;
                _visible = true;
            }
            OnLayerVisChanged();
        }
        protected void OnLayerVisChanged()
        {
            LayerVisibilityChanged?.Invoke(this, EventArgs.Empty);
        }
        public static Image ImageFromByte(byte[] b)
        {
            using MemoryStream ms = new MemoryStream(b);
            return Image.FromStream(ms);
        }
        private void LayerHolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(LayerHolder)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isLmbDown && ShouldStartDragdrop(e, 10))
            {
                DoDragDrop(this, DragDropEffects.Move);
                _isLmbDown = false;
            }
        }

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _isLmbDown = false;
            }
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            OriginalMousePosition = new OVector(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                _isLmbDown = true;
            }
        }
        public bool ShouldStartDragdrop(MouseEventArgs e, double distanceLimit)
        {
            OVector currentPosOV = new OVector(e.X, e.Y);
            double draggedDist = currentPosOV.Subtract(OriginalMousePosition).Magnitude;
            return draggedDist > distanceLimit;
        }

        private void MainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            Highlight(StayHighlighted);
        }

        private void MainPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Highlight(true);
        }
        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                BackColor = Color.IndianRed;
            } else
            {
                BackColor = SystemColors.Control;
                StayHighlighted = false;
            }
        }
    }
}
