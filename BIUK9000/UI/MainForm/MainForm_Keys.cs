using BIUK9000.GifferComponents;
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
            if (MainGiffer == null || ActiveControl is TextBox) return base.ProcessKeyPreview(ref m);
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value < mts.Maximum) mts.Value += 1;
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value > 0) mts.Value -= 1;
                    return true;
                }
                else if (keyData == Keys.T)
                {
                    TextGFL tgfl = new TextGFL("YOUR TEXT");
                    tgfl.FontName = "Impact";
                    tgfl.FontBorderColor = Color.Black;
                    tgfl.FontColor = Color.White;
                    tgfl.FontBorderWidth = 5;
                    tgfl.FontSize = 20;
                    tgfl.Visible = true;
                    MainGiffer.Frames.ForEach(frame => frame.AddLayer(new TextGFL(tgfl)));
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
