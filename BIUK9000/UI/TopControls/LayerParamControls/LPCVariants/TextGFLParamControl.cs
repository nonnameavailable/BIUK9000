using BIUK9000.GifferComponents;
using BIUK9000.UI.TopControls.LayerParamControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.GifferComponents.GFLVariants;

namespace BIUK9000.UI.LayerParamControls
{
    public partial class TextGFLParamControl : UserControl, IGFLParamControl
    {
        private bool _suppressLayerParamsEvents;
        public TextGFLParamControl()
        {
            InitializeComponent();
            _suppressLayerParamsEvents = false;
            PopulateFontComboBox();
            TextLayerBorderWidthNUD.ValueChanged += (sender, args) => OnParamsChanged();
            textLayerTextTB.TextChanged += (sender, args) => OnParamsChanged();
            textLayerFontCBB.SelectedIndexChanged += (sender, args) => OnParamsChanged();
            fontColorButton.ColorChanged += (sender, args) => OnParamsChanged();
            borderColorButton.ColorChanged += (sender, args) => OnParamsChanged();
        }

        public event EventHandler ParamsChanged;

        public void LoadParams(GFL gfl)
        {
            _suppressLayerParamsEvents = true;
            TextGFL tgfl = (TextGFL)gfl;
            TextLayerBorderWidthNUD.Value = (decimal)tgfl.FontBorderWidth;
            textLayerTextTB.Text = tgfl.Text;
            textLayerFontCBB.SelectedItem = tgfl.FontName;
            fontColorButton.Color = tgfl.FontColor;
            borderColorButton.Color = tgfl.FontBorderColor;
            _suppressLayerParamsEvents = false;
        }

        public void SaveParams(GFL gfl)
        {
            TextGFL tgfl = (TextGFL)gfl;
            tgfl.FontBorderWidth = (float)TextLayerBorderWidthNUD.Value;
            tgfl.Text = textLayerTextTB.Text;
            tgfl.FontName = textLayerFontCBB.SelectedItem.ToString();
            tgfl.FontColor = fontColorButton.Color;
            tgfl.FontBorderColor = borderColorButton.Color;
        }

        private void OnParamsChanged()
        {
            if(!_suppressLayerParamsEvents) ParamsChanged?.Invoke(this, EventArgs.Empty);
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
        public bool TextBoxHasFocus()
        {
            return textLayerTextTB.Focused;
        }
    }
}
