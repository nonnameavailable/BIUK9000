﻿using System;
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
        public int SelectedLayerIndex { get; set; }
        public event EventHandler<LayerOrderEventArgs> LayerOrderChanged;
        public event EventHandler SelectedLayerChanged;
        public LayersPanel()
        {
            InitializeComponent();
            ActiveFrame = null;
            SelectedLayerIndex = 0;
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
                lh.DragDropped += Lh_DragDropped;
                layersFLP.Controls.Add(lh);
            }
            SelectLayerHolder(SelectedLayerIndex);
        }

        private void Lh_DragDropped(object sender, DragEventArgs e)
        {
            LayerHolder droppedLh = (LayerHolder)e.Data.GetData(typeof(LayerHolder));
            int originalIndex = layersFLP.Controls.IndexOf(droppedLh);
            int targetIndex = layersFLP.Controls.IndexOf(sender as LayerHolder);
            LayerOrderEventArgs loea = new LayerOrderEventArgs(originalIndex, targetIndex);
            clickedLayerHolder = layersFLP.Controls[SelectedLayerIndex] as LayerHolder;
            LayerOrderChanged?.Invoke(this, loea);
            SelectedLayerChanged?.Invoke(clickedLayerHolder.HeldLayer, EventArgs.Empty);
        }

        private void SelectLayerHolder(int i)
        {
            clickedLayerHolder?.Highlight(false);
            if (layersFLP.Controls.Count > 0 && i < layersFLP.Controls.Count)
            {
                clickedLayerHolder = (LayerHolder)(layersFLP.Controls[i]);
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
            LayerHolder lh = sender as LayerHolder;
            if (lh == clickedLayerHolder) return;
            clickedLayerHolder?.Highlight(false);
            lh.Highlight(true);
            clickedLayerHolder = lh;
            SelectedLayerIndex = layersFLP.Controls.IndexOf(lh);
            if (clickedLayerHolder != null) clickedLayerHolder.StayHighlighted = true;
            SelectedLayerChanged?.Invoke(clickedLayerHolder.HeldLayer, EventArgs.Empty);
        }

        public void SelectNewestLayer()
        {
            int lhIndex = layersFLP.Controls.Count - 1;
            SelectLayerHolder(lhIndex);
            SelectedLayerIndex = lhIndex;
            clickedLayerHolder = layersFLP.Controls[lhIndex] as LayerHolder;
            SelectedLayerChanged?.Invoke(clickedLayerHolder.HeldLayer, EventArgs.Empty);
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
