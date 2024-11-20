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
        private LayerHolder ClickedLayerHolder {
            get
            {
                if(SelectedLayerIndex >= layersFLP.Controls.Count)
                {
                    return layersFLP.Controls[0] as LayerHolder;
                } else
                {
                    return layersFLP.Controls[SelectedLayerIndex] as LayerHolder;
                }
            }
        }
        public GFL SelectedLayer { get => ClickedLayerHolder.HeldLayer; }
        private GifFrame ActiveFrame { get; set; }
        public int SelectedLayerIndex { get; set; }
        public event EventHandler<LayerOrderEventArgs> LayerOrderChanged;
        public event EventHandler SelectedLayerChanged;
        public event EventHandler<SelectedIndexEventArgs> LayerVisibilityChanged;
        public event EventHandler LayerDeleteButtonClicked;
        public LayersPanel()
        {
            InitializeComponent();
            ActiveFrame = null;
            SelectedLayerIndex = 0;
        }
        public void DisplayLayers(GifFrame frame)
        {
            layersFLP.Visible = false;
            ActiveFrame = frame;
            for (int i = layersFLP.Controls.Count - 1; i >= 0; i--)
            {
                Control c = layersFLP.Controls[i];
                c.Dispose();
            }
            foreach (var layer in frame.Layers)
            {
                LayerHolder lh = new LayerHolder(layer);
                lh.LayerClicked += Lh_LayerClicked;
                lh.DragDropped += Lh_DragDropped;
                lh.LayerVisibilityChanged += Lh_LayerVisibilityChanged;
                lh.DeleteButtonClicked += Lh_DeleteButtonClicked;
                layersFLP.Controls.Add(lh);
            }
            SelectLayerHolder(SelectedLayerIndex);
            layersFLP.Visible = true;
        }

        private void Lh_LayerVisibilityChanged(object sender, EventArgs e)
        {
            int index = layersFLP.Controls.IndexOf(sender as Control);
            LayerVisibilityChanged?.Invoke(sender, new SelectedIndexEventArgs(index));
        }

        private void Lh_DeleteButtonClicked(object sender, EventArgs e)
        {
            if (layersFLP.Controls.Count > 1)
            {
                SelectedLayerIndex = 0;
                LayerDeleteButtonClicked?.Invoke(sender, e);
            }
        }

        public void TrySelectLayerByID(int layerID)
        {
            GFL newSelectedLayer = ActiveFrame.Layers.Find(layer => layer.LayerID == layerID);
            if(newSelectedLayer != null)
            {
                SelectLayerHolder(ActiveFrame.Layers.IndexOf(newSelectedLayer));
            }
        }
        private void Lh_DragDropped(object sender, DragEventArgs e)
        {
            LayerHolder droppedLh = (LayerHolder)e.Data.GetData(typeof(LayerHolder));
            int originalIndex = layersFLP.Controls.IndexOf(droppedLh);
            int targetIndex = layersFLP.Controls.IndexOf(sender as LayerHolder);
            LayerOrderEventArgs loea = new LayerOrderEventArgs(originalIndex, targetIndex);
            LayerOrderChanged?.Invoke(this, loea);
            SelectedLayerChanged?.Invoke(ClickedLayerHolder.HeldLayer, EventArgs.Empty);
        }

        private void SelectLayerHolder(int i)
        {
            ClickedLayerHolder?.Highlight(false);
            SelectedLayerIndex = i;
            ClickedLayerHolder.Highlight(true);
            ClickedLayerHolder.StayHighlighted = true;
        }

        private void Lh_LayerClicked(object sender, EventArgs e)
        {
            LayerHolder lh = sender as LayerHolder;
            if (lh == ClickedLayerHolder) return;
            ClickedLayerHolder?.Highlight(false);
            lh.Highlight(true);
            SelectedLayerIndex = layersFLP.Controls.IndexOf(lh);
            if (ClickedLayerHolder != null) ClickedLayerHolder.StayHighlighted = true;
            SelectedLayerChanged?.Invoke(ClickedLayerHolder.HeldLayer, EventArgs.Empty);
        }

        public void SelectNewestLayer()
        {
            int lhIndex = layersFLP.Controls.Count - 1;
            SelectLayerHolder(lhIndex);
            SelectedLayerIndex = lhIndex;
            SelectedLayerChanged?.Invoke(ClickedLayerHolder.HeldLayer, EventArgs.Empty);
        }
        public class SelectedIndexEventArgs : EventArgs
        {
            public int Index { get; }

            public SelectedIndexEventArgs(int index)
            {
                Index = index;
            }
        }
        public class LayerOrderEventArgs : EventArgs
        {
            public int TargetIndex { get; }
            public int OriginalIndex { get; }

            public LayerOrderEventArgs(int originalIndex, int targetIndex)
            {
                TargetIndex = targetIndex;
                OriginalIndex = originalIndex;
            }
        }
    }
}
