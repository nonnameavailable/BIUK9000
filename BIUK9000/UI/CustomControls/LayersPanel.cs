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
        public int SelectedLayerIndex { get; set; }
        public event EventHandler<LayerOrderEventArgs> LayerOrderChanged;
        public event EventHandler<IndexEventArgs> SelectedLayerChanged;
        public event EventHandler<IndexEventArgs> LayerVisibilityChanged;
        public event EventHandler<IndexEventArgs> LayerDeleteButtonClicked;
        public event EventHandler<IndexEventArgs> SnapLayerToFrame;
        public event EventHandler<IndexEventArgs> RestoredRatio;
        public LayersPanel()
        {
            InitializeComponent();
            SelectedLayerIndex = 0;
        }
        public void DisplayLayers(IBitmapProvider bitmapProvider)
        {
            layersFLP.Visible = false;
            for (int i = layersFLP.Controls.Count - 1; i >= 0; i--)
            {
                Control c = layersFLP.Controls[i];
                c.Dispose();
            }
            List<Bitmap> bitmaps = bitmapProvider.GetBitmaps();
            List<bool> visibles = bitmapProvider.GetVisibles();
            for (int i = 0; i < bitmaps.Count; i++)
            {
                Bitmap bitmap = bitmaps[i];
                bool visible = visibles[i];
                LayerHolder lh = new LayerHolder(bitmap, visible);
                lh.LayerClicked += Lh_LayerClicked;
                lh.DragDropped += Lh_DragDropped;
                lh.LayerVisibilityChanged += (sender, args) => InvokeIndexEvent(sender, LayerVisibilityChanged);
                lh.DeleteButtonClicked += Lh_DeleteButtonClicked;
                lh.SnappedLayerToFrame += (sender, args) => InvokeIndexEvent(sender, SnapLayerToFrame);
                lh.RestoredRatio += (sender, args) => InvokeIndexEvent(sender, RestoredRatio);
                layersFLP.Controls.Add(lh);
            }
            for(int i = 0; i < bitmaps.Count; i++)
            {
                bitmaps[i]?.Dispose();
            }
            SelectHolder(SelectedLayerIndex);
            layersFLP.Visible = true;
        }
        private void InvokeIndexEvent(object sender, EventHandler<IndexEventArgs> ev)
        {
            int index = layersFLP.Controls.IndexOf((Control)sender);
            ev?.Invoke(sender, new IndexEventArgs(index));
        }

        private void Lh_DeleteButtonClicked(object sender, EventArgs e)
        {
            SelectedLayerIndex = 0;
            LayerDeleteButtonClicked?.Invoke(sender, new IndexEventArgs(layersFLP.Controls.IndexOf(sender as LayerHolder)));
        }

        private void Lh_DragDropped(object sender, DragEventArgs e)
        {
            LayerHolder droppedLh = (LayerHolder)e.Data.GetData(typeof(LayerHolder));
            int originalIndex = layersFLP.Controls.IndexOf(droppedLh);
            int targetIndex = layersFLP.Controls.IndexOf((LayerHolder)sender);
            LayerOrderEventArgs loea = new LayerOrderEventArgs(originalIndex, targetIndex);
            LayerOrderChanged?.Invoke(this, loea);
            SelectedLayerChanged?.Invoke(this, new IndexEventArgs(SelectedLayerIndex));
        }

        public void SelectHolder(int index)
        {
            if (index < 0) return;
            ClickedLayerHolder?.Highlight(false);
            SelectedLayerIndex = index;
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
            SelectedLayerChanged?.Invoke(this, new IndexEventArgs(SelectedLayerIndex));
        }

        public void SelectNewestLayer()
        {
            int lhIndex = layersFLP.Controls.Count - 1;
            SelectHolder(lhIndex);
            SelectedLayerChanged?.Invoke(this, new IndexEventArgs(SelectedLayerIndex));
        }
        public class IndexEventArgs : EventArgs
        {
            public int Index { get; }

            public IndexEventArgs(int index)
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
        public interface IBitmapProvider
        {
            List<Bitmap> GetBitmaps();
            List<bool> GetVisibles();
        }
    }
}
