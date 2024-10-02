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
        public GifFrameLayer ActiveLayer { get => clickedLayerHolder.HeldLayer; }
        public GifFrame ActiveFrame { get; set; }
        public event EventHandler LayerChanged;
        public LayersPanel()
        {
            InitializeComponent();
        }
        public void DisplayLayers(GifFrame frame)
        {
            if (ActiveFrame != null) ActiveFrame.LayersChanged -= Frame_LayersChanged;
            ActiveFrame = frame;
            frame.LayersChanged += Frame_LayersChanged;
            for (int i = layersFLP.Controls.Count - 1; i >= 0; i--)
            {
                Control c = layersFLP.Controls[i];
                c.Dispose();
            }
            foreach (var layer in frame.Layers)
            {
                LayerHolder lh = new LayerHolder(layer);
                lh.LayerClicked += Lh_LayerClicked;
                lh.LayerChanged += (sender, args) => LayerChanged?.Invoke(this, EventArgs.Empty);
                layersFLP.Controls.Add(lh);
            }
        }

        private void Frame_LayersChanged(object sender, EventArgs e)
        {
            DisplayLayers(ActiveFrame);
        }

        private void Lh_LayerClicked(object sender, EventArgs e)
        {
            clickedLayerHolder?.Highlight(false);
            LayerHolder lh = sender as LayerHolder;
            lh.Highlight(true);
            clickedLayerHolder = lh;
            if (clickedLayerHolder != null) clickedLayerHolder.StayHighlighted = true;
        }
    }
}
