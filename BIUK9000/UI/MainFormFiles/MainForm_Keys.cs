using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.LayerParamControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class MainForm
    {
        public bool IsShiftDown { get; set; }
        public bool IsCtrlDown { get; set; }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (MainGiffer == null || ActiveControl is TextBox || ActiveControl is IGFLParamControl) return base.ProcessKeyPreview(ref m);
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    if (MainTimelineSlider.SelectedFrameIndex < MainTimelineSlider.Maximum) MainTimelineSlider.SelectedFrameIndex += 1;
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    if (MainTimelineSlider.SelectedFrameIndex > 0) MainTimelineSlider.SelectedFrameIndex -= 1;
                    return true;
                }
                else if (keyData == Keys.T || keyData == Keys.B)
                {
                    AddNewLayer(keyData);
                    return true;
                }
                else if (keyData == Keys.ShiftKey)
                {
                    IsShiftDown = true;
                    return true;
                }
                else if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = true;
                    return true;
                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {

                    IsShiftDown = false;
                    return true;
                }
                if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = false;
                    return true;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }
        private void AddNewLayer(Keys keyData)
        {
            int nextId = MainGiffer.NextLayerID();
            GFL nextLayer = null;
            if (keyData == Keys.T)
            {
                nextLayer = new TextGFL(nextId);
            }
            else if (keyData == Keys.B)
            {
                nextLayer = new PlainGFL(nextId);
            }
            if (nextLayer == null) return;
            foreach(GifFrame gf in MainGiffer.Frames)
            {
                gf.AddLayer(nextLayer);
            }
            MainLayersPanel.DisplayLayers(SelectedFrame);
            UpdateMainPictureBox();
            MainLayersPanel.SelectNewestLayer();
        }
    }
}
