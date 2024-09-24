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
    public partial class GifFrameHolder : UserControl
    {
        public event EventHandler BitmapHolderClicked;
        protected virtual void OnBitmapHolderClicked()
        {
            BitmapHolderClicked?.Invoke(this, EventArgs.Empty);
        }
        public GifFrame HeldGifFrame { get; }

        private bool stayHighlighted = false;
        public GifFrameHolder(GifFrame gifFrame)
        {
            InitializeComponent();
            HeldGifFrame = gifFrame;
            pictureBox.Image = gifFrame.CompleteBitmap();
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.MouseClick += PictureBox_MouseClick;
        }
        public GifFrameHolder(Bitmap bitmap)
        {
            InitializeComponent();
            HeldGifFrame = new GifFrame(bitmap);
            pictureBox.Image = HeldGifFrame.CompleteBitmap();
            pictureBox.MouseEnter += PictureBox_MouseEnter;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.MouseClick += PictureBox_MouseClick;
        }

        public Bitmap CompleteBitmap { get => HeldGifFrame.CompleteBitmap(); }

        public void Highlight(bool highlight)
        {
            if (highlight)
            {
                BorderStyle = BorderStyle.FixedSingle;
                BackColor = Color.Blue;
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
            OnBitmapHolderClicked();
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
