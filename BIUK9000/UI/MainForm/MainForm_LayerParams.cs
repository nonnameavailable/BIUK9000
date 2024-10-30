using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.UI
{
    public partial class MainForm
    {
        private bool _suppressLayerParamsEvents;
        private void HandleLayerParamChange(Action<TextGFL> updateAction)
        {
            if (_suppressLayerParamsEvents) return;
            TextGFL tgfl = SelectedLayer as TextGFL;
            if (tgfl != null)
            {
                updateAction(tgfl);
                UpdateMainPictureBox();
                ApplyCurrentLayerParamsToSubsequentLayers();
            }
        }
        private void FontColorButton_ColorChanged(object sender, EventArgs e)
        {
            HandleLayerParamChange(tgfl => tgfl.FontColor = (sender as ColorButton).Color);
        }

        private void BorderColorButton_ColorChanged(object sender, EventArgs e)
        {
            HandleLayerParamChange(tgfl => tgfl.FontBorderColor = (sender as ColorButton).Color);
        }

        private void TextLayerTextTB_TextChanged(object sender, EventArgs e)
        {
            HandleLayerParamChange(tgfl => tgfl.Text = textLayerTextTB.Text);
        }

        private void TextLayerFontCBB_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleLayerParamChange(tgfl => tgfl.FontName = textLayerFontCBB.Text);
        }

        private void TextLayerBorderWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            HandleLayerParamChange(tgfl => tgfl.FontBorderWidth = (float)TextLayerBorderWidthNUD.Value);
        }
        private void PopulateFontComboBox()
        {
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (FontFamily fontFamily in installedFonts.Families)
            {
                textLayerFontCBB.Items.Add(fontFamily.Name);
            }
            if (textLayerFontCBB.Items.Contains("Impact"))
            {
                textLayerFontCBB.SelectedItem = "Impact";
            }
            else
            {
                textLayerFontCBB.SelectedIndex = 0;
            }
        }

        private void UpdateLayerParamsUI()
        {
            _suppressLayerParamsEvents = true;
            if (SelectedLayer == null) return;
            if (SelectedLayer is TextGFL)
            {
                textLayerParamsGB.Visible = true;
                TextGFL tgfl = SelectedLayer as TextGFL;
                TextLayerBorderWidthNUD.Value = (decimal)tgfl.FontBorderWidth;
                textLayerTextTB.Text = tgfl.Text;
                textLayerFontCBB.SelectedItem = tgfl.FontName;
                fontColorButton.Color = tgfl.FontColor;
                borderColorButton.Color = tgfl.FontBorderColor;
            } else
            {
                textLayerParamsGB.Visible = false;
            }
            _suppressLayerParamsEvents = false;
        }
    }
}
