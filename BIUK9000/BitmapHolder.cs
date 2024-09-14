using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public partial class BitmapHolder : UserControl
    {
        public event EventHandler BitmapHolderClicked;
        protected virtual void OnBitmapHolderClicked()
        {
            BitmapHolderClicked?.Invoke(this, EventArgs.Empty);
        }
        public Bitmap HeldImage { get => (Bitmap)pictureBox.Image; }

        private bool stayHighlighted = false;
        public BitmapHolder(Bitmap image)
        {
            InitializeComponent();
            pictureBox.Image = image;
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.MouseClick += PictureBox_MouseClick;
        }

        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = Color.Blue;
            } else
            {
                this.BorderStyle = BorderStyle.None;
                this.BackColor = SystemColors.Control;
                stayHighlighted = false;
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            stayHighlighted = true;
            OnBitmapHolderClicked();
        }

        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            if(!stayHighlighted) Highlight(false);
        }

        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            Highlight(true);
        }
    }
}
