using Emgu.CV.Reg;
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
    public partial class BitmapHolder : UserControl
    {
        private Color highlightColor;
        public BitmapHolder(Bitmap bitmap, Color highlightColor)
        {
            InitializeComponent();
            pictureBox.Image = bitmap;
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.MouseClick += PictureBox_MouseClick;
            this.highlightColor = highlightColor;
        }
        public event EventHandler Clicked;
        protected virtual void OnClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
        public Bitmap HeldBitmap { get; }

        private bool stayHighlighted = false;

        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                BorderStyle = BorderStyle.FixedSingle;
                BackColor = highlightColor;
            }
            else
            {
                BorderStyle = BorderStyle.None;
                BackColor = SystemColors.Control;
                stayHighlighted = false;
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            stayHighlighted = true;
            OnClicked();
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (!stayHighlighted) Highlight(false);
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            Highlight(true);
        }
    }
}
