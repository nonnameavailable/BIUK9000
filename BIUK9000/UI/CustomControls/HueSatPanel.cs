using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.CustomControls
{
    public partial class HueSatPanel : UserControl
    {
        public event EventHandler HueSatChanged;
        private bool _isCodeValueChange;
        public float Saturation {
            get
            {
                return saturationTrackBar.Value / 100f;
            }
            set
            {
                _isCodeValueChange = true;
                saturationTrackBar.Value = (int) (value * 100);
                _isCodeValueChange = false;
            }
        }
        public float Brightness
        {
            get
            {
                return brightnessTrackBar.Value / 100f;
            }
            set
            {
                _isCodeValueChange = true;
                brightnessTrackBar.Value = (int)(value * 100);
                _isCodeValueChange = false;
            }
        }
        public HueSatPanel()
        {
            InitializeComponent();
            brightnessTrackBar.ValueChanged += (sender, args) => OnHueSatChanged();
            saturationTrackBar.ValueChanged += (sender, args) => OnHueSatChanged();
        }
        private void OnHueSatChanged()
        {
            if (!_isCodeValueChange)
            {
                HueSatChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
