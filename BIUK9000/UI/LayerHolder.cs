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
        public event EventHandler LayerClicked;
        //public event EventHandler LayerChanged;
        public LayerHolder(GFL layer)
        {
            InitializeComponent();
            HeldLayer = layer;
            mainPictureBox.MouseEnter += MainPictureBox_MouseEnter;
            mainPictureBox.MouseLeave += MainPictureBox_MouseLeave;
            mainPictureBox.MouseClick += (sender, args) => LayerClicked?.Invoke(this, EventArgs.Empty);
            mainPictureBox.Image = layer.MorphedBitmap;
            StayHighlighted = false;
            //HeldLayer.ParameterChanged += HeldLayer_ParameterChanged;
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
