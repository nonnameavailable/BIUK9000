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
        public GifFrameLayer Layer { get; set; }
        public event EventHandler LayerHolderClicked;
        protected virtual void OnLayerHolderClicked()
        {
            LayerHolderClicked?.Invoke(this, EventArgs.Empty);
        }
        public LayerHolder(GifFrameLayer layer)
        {
            InitializeComponent();
            Layer = layer;
            layerImagePB.Click += LayerImagePB_Click;
        }

        private void LayerImagePB_Click(object sender, EventArgs e)
        {
            OnLayerHolderClicked();
        }
    }
}
