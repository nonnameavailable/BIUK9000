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
    public partial class LayerHolder : UserControl
    {
        public GifFrameLayer HeldLayer { get; set; }
        public event EventHandler BMHClicked;
        public LayerHolder(GifFrameLayer layer)
        {
            InitializeComponent();
            HeldLayer = layer;
            CreateBitmapHolder();
        }
        public GifFrameLayer HeldGFL { get; }
        public BitmapHolder BMH { get; set; }

        private bool stayHighlighted = false;

        private void CreateBitmapHolder()
        {
            BMH = new BitmapHolder(HeldLayer.OriginalBitmap, Color.IndianRed);
            BMH.Dock = DockStyle.Fill;
            Controls.Add(BMH);
            BMH.Clicked += (sender, args) => BMHClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
