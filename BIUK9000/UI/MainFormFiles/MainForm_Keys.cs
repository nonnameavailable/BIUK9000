using BIUK9000.GifferComponents;
using BIUK9000.UI.LayerParamControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class MainForm
    {
        private bool IsShiftDown { get; set; }
        private bool IsCtrlDown { get; set; }

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
                else if (keyData == Keys.T)
                {
                    TextGFL tgfl = new TextGFL("YOUR TEXT", 0);
                    tgfl.FontName = "Impact";
                    tgfl.FontBorderColor = Color.Black;
                    tgfl.FontColor = Color.White;
                    tgfl.FontBorderWidth = 5;
                    tgfl.FontSize = 20;
                    tgfl.Visible = true;
                    int nextLayerID = MainGiffer.NextLayerID();
                    foreach(GifFrame frame in MainGiffer.Frames)
                    {
                        frame.LayerCountChanged -= GifFrame_LayerCountChanged;
                        frame.AddLayer(new TextGFL(tgfl, nextLayerID));
                        frame.LayerCountChanged += GifFrame_LayerCountChanged;
                    }
                    GifFrame_LayerCountChanged(null, null);
                    mainLayersPanel.SelectNewestLayer();
                    return true;
                }
                else if (keyData == Keys.C)
                {
                    MainGiffer.Crop(SelectedFrame);
                    MainLayersPanel.DisplayLayers(SelectedFrame);
                    UpdateMainPictureBox();
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
    }
}
