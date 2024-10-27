using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.GifferComponents;

namespace BIUK9000.UI
{
    public partial class LayerHolder : UserControl
    {
        public GFL HeldLayer { get; set; }
        public bool StayHighlighted { get; set; }
        public OVector OriginalMousePosition { get; set; }
        private MainForm MF { get => ParentForm as MainForm; }
        public event EventHandler LayerClicked;
        public event MouseEventHandler LayerMouseMove;
        private bool _isLmbDown;
        //public event EventHandler LayerChanged;
        public LayerHolder(GFL layer)
        {
            InitializeComponent();
            HeldLayer = layer;
            mainPictureBox.MouseEnter += MainPictureBox_MouseEnter;
            mainPictureBox.MouseLeave += MainPictureBox_MouseLeave;
            mainPictureBox.MouseClick += (sender, args) => LayerClicked?.Invoke(this, EventArgs.Empty);
            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;
            mainPictureBox.Image = layer.MorphedBitmap();
            DragDrop += LayerHolder_DragDrop;
            DragEnter += LayerHolder_DragEnter;
            StayHighlighted = false;
            this.AllowDrop = true;
            //HeldLayer.ParameterChanged += HeldLayer_ParameterChanged;
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

        private void LayerHolder_DragDrop(object sender, DragEventArgs e)
        {
            //horrible, shouldn't use MF
            LayerHolder draggedLH = (LayerHolder)e.Data.GetData(typeof(LayerHolder));
            FlowLayoutPanel pFlp = Parent as FlowLayoutPanel;
            int originalIndex = pFlp.Controls.IndexOf(draggedLH);
            int targetIndex = pFlp.Controls.IndexOf(this);
            //pFlp.Controls.SetChildIndex(draggedLH, desiredIndex);
            GifFrame sf = MF.SelectedFrame;
            for (int i = MF.MainGiffer.Frames.IndexOf(sf); i < MF.MainGiffer.Frames.Count; i++)
            {
                GifFrame cf = MF.MainGiffer.Frames[i];
                GFL gflToInsert = cf.Layers[originalIndex];
                cf.Layers.RemoveAt(originalIndex);
                cf.Layers.Insert(targetIndex, gflToInsert);
            }
            MF.UpdatePicture();
            MF.MainLayersPanel.DisplayLayers(sf);
            //MessageBox.Show(pFlp.Controls.IndexOf(draggedLH) + "x" + pFlp.Controls.IndexOf(this));
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

        //private void HeldLayer_ParameterChanged(object sender, EventArgs e)
        //{
        //    LayerChanged?.Invoke(this, EventArgs.Empty);
        //}

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
