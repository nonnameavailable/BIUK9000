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
    public partial class GifFrameHolder : UserControl
    {
        public event EventHandler BMHClicked;
        public GifFrame HeldGifFrame { get; }
        public BitmapHolder BMH { get; set; }

        private bool stayHighlighted = false;
        public GifFrameHolder(GifFrame gifFrame)
        {
            InitializeComponent();
            HeldGifFrame = gifFrame;
            CreateBitmapHolder();
        }
        public GifFrameHolder(Bitmap bitmap)
        {
            InitializeComponent();
            HeldGifFrame = new GifFrame(bitmap);
            CreateBitmapHolder();
        }
        private void CreateBitmapHolder()
        {
            BMH = new BitmapHolder(HeldGifFrame.CompleteBitmap());
            BMH.Dock = DockStyle.Fill;
            Controls.Add(BMH);
            BMH.Clicked += (sender, args) => BMHClicked?.Invoke(this, EventArgs.Empty);
        }

        public Bitmap CompleteBitmap { get => HeldGifFrame.CompleteBitmap(); }
    }
}
