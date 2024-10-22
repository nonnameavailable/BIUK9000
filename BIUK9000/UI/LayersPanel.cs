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
    public partial class LayersPanel : UserControl
    {
        private LayerHolder clickedLayerHolder;
        public GFL SelectedLayer { get => clickedLayerHolder.HeldLayer; }
        private GifFrame ActiveFrame { get; set; }
        public LayersPanel()
        {
            InitializeComponent();
            ActiveFrame = null;
        }
        public void DisplayLayers(GifFrame frame)
        {
            if (ActiveFrame != null) ActiveFrame.LayerCountChanged -= Frame_LayersChanged;
            ActiveFrame = frame;
            frame.LayerCountChanged += Frame_LayersChanged;
            for (int i = layersFLP.Controls.Count - 1; i >= 0; i--)
            {
                Control c = layersFLP.Controls[i];
                c.Dispose();
            }
            foreach (var layer in frame.Layers)
            {
                LayerHolder lh = new LayerHolder(layer);
                lh.LayerClicked += Lh_LayerClicked;
                layersFLP.Controls.Add(lh);
            }
            if(layersFLP.Controls.Count > 1)
            {
                clickedLayerHolder = (LayerHolder)(layersFLP.Controls[1]);
            } else
            {
                clickedLayerHolder = (LayerHolder)(layersFLP.Controls[0]);
            }
            
            clickedLayerHolder.Highlight(true);
            clickedLayerHolder.StayHighlighted = true;
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
