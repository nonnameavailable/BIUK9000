using BIUK9000.GifferComponents;
using System;
using System.Windows.Forms;
using BIUK9000.GifferComponents.GFLVariants;

namespace BIUK9000.UI.LayerParamControls
{
    public partial class PlainGFLParamControl : UserControl, IGFLParamControl
    {
        public PlainGFLParamControl()
        {
            InitializeComponent();
            colorButton.ColorChanged += (sender, args) => OnParamsChanged();
        }
        
        private void OnParamsChanged()
        {
            ParamsChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ParamsChanged;

        public void LoadParams(GFL gfl)
        {
            colorButton.Color = (gfl as PlainGFL).BackColor;
        }

        public void SaveParams(GFL gfl)
        {
            (gfl as  PlainGFL).BackColor = colorButton.Color;
        }
    }
}
