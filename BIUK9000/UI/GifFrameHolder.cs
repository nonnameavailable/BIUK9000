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
        private ContextMenuStrip contextMenu;
        public event EventHandler BMHClicked;
        public GifFrame HeldGifFrame { get; }
        public BitmapHolder BMH { get; set; }

        private bool stayHighlighted = false;
        public GifFrameHolder(GifFrame gifFrame)
        {
            InitializeComponent();
            HeldGifFrame = gifFrame;
            CreateBitmapHolder();
            CreateContextMenu();
        }
        public GifFrameHolder(Bitmap bitmap)
        {
            InitializeComponent();
            HeldGifFrame = new GifFrame(bitmap);
            CreateBitmapHolder();
            CreateContextMenu();
        }
        private void CreateBitmapHolder()
        {
            BMH = new BitmapHolder(HeldGifFrame.CompleteBitmap(), Color.Indigo);
            BMH.Dock = DockStyle.Fill;
            Controls.Add(BMH);
            BMH.Clicked += (sender, args) => BMHClicked?.Invoke(this, EventArgs.Empty);
        }
        private void CreateContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            var addLayerItem = new ToolStripMenuItem("Add layer");
            addLayerItem.Click += AddLayerItem_Click;
            contextMenu.Items.Add(addLayerItem);
            this.ContextMenuStrip = contextMenu;
        }

        private void AddLayerItem_Click(object sender, EventArgs e)
        {
            HeldGifFrame.AddLayer(50, 50);
        }

        public Bitmap CompleteBitmap { get => HeldGifFrame.CompleteBitmap(); }
    }
}
