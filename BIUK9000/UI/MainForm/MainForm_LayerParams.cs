using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.UI.LayerParamControls;
using System.Diagnostics;

namespace BIUK9000.UI
{
    public partial class MainForm
    {
        private GFL PreviousSelectedLayer {  get; set; }
        private bool LayerTypeChanged()
        {
            if (PreviousSelectedLayer == null || SelectedLayer == null) return true;
            return !(SelectedLayer.GetType().Name == PreviousSelectedLayer.GetType().Name);
        }

        private void UpdateLayerParamsUI(bool layerTypeChanged)
        {
            if(layerTypeChanged)
            {
                if(layerParamsPanel.Controls.Count > 0)
                {
                    layerParamsPanel.Controls[0].Dispose();
                }
                layerParamsPanel.Controls.Clear();
                IGFLParamControl lpc = GFLParamsControlFactory.GFLParamControl(SelectedLayer);
                lpc.LoadParams(SelectedLayer);
                lpc.ParamsChanged += (sender, args) =>
                {
                    lpc.SaveParams(SelectedLayer);
                    UpdateMainPictureBox();
                    ApplyCurrentLayerParamsToSubsequentLayers();
                };
                Control lpcc = lpc as Control;
                lpcc.Dock = DockStyle.Fill;
                layerParamsPanel.Controls.Add(lpcc);
            } else
            {
                if(layerParamsPanel.Controls.Count > 0)
                {
                    IGFLParamControl lpc = layerParamsPanel.Controls[0] as IGFLParamControl;
                    lpc.LoadParams(SelectedLayer);
                }
            }

        }
    }
}
