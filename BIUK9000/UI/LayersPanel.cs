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
    public partial class LayersPanel : UserControl
    {
        private LayerHolder clickedLayerHolder;
        public GifFrameLayer ActiveLayer { get => clickedLayerHolder.HeldGFL; }
        public LayersPanel()
        {
            InitializeComponent();
        }
        public void DisplayLayers(GifFrame frame)
        {
            foreach(Control c in layersFLP.Controls)
            {
                c.Dispose();
            }
            foreach (var layer in frame.Layers)
            {
                LayerHolder lh = new LayerHolder(layer);
                lh.BMHClicked += Lh_BMHClicked;
                layersFLP.Controls.Add(lh);
            }
        }

        private void Lh_BMHClicked(object sender, EventArgs e)
        {
            clickedLayerHolder?.BMH.Highlight(false);
            LayerHolder lh = sender as LayerHolder;
            lh.BMH.Highlight(true);
            clickedLayerHolder = lh;
        }
    }
}
