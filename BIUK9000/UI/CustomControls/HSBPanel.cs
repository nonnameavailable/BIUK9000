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
    public partial class HSBPanel : UserControl
    {
        public event EventHandler HueSatChanged;
        public event EventHandler ChangeStarted;
        public event EventHandler ChangeEnded;
        private bool _isCodeValueChange;
        public float Saturation
        {
            get
            {
                return saturationTrackBar.Value / 100f;
            }
            set
            {
                _isCodeValueChange = true;
                saturationTrackBar.Value = (int)(value * 100);
                saturationNUD.Value = (decimal)value;
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
                brightnessNUD.Value = (decimal)value;
                _isCodeValueChange = false;
            }
        }
        public float Transparency
        {
            get
            {
                return transparencyTrackBar.Value / 200f;
            }
            set
            {
                _isCodeValueChange = true;
                transparencyTrackBar.Value = (int)(value * 200);
                transparencyNUD.Value = (decimal)value;
                _isCodeValueChange = false;
            }
        }
        public HSBPanel()
        {
            InitializeComponent();
            foreach(Control c in this.Controls)
            {
                if(c is TrackBar tc)
                {
                    tc.ValueChanged += (sender, args) => OnHueSatChanged();
                    tc.MouseDown += (sender, args) => ChangeStarted?.Invoke(this, EventArgs.Empty);
                    tc.MouseUp += (sender, args) => ChangeEnded?.Invoke(this, EventArgs.Empty);
                }
                if(c is NumericUpDown nc)
                {
                    nc.ValueChanged += (sender, args) => OnHueSatChanged();
                }
            }
            saturationNUD.ValueChanged += SaturationNUD_ValueChanged;
            brightnessNUD.ValueChanged += BrightnessNUD_ValueChanged;
            transparencyNUD.ValueChanged += TransparencyNUD_ValueChanged;

            saturationTrackBar.ValueChanged += SaturationTrackBar_ValueChanged;
            brightnessTrackBar.ValueChanged += BrightnessTrackBar_ValueChanged;
            transparencyTrackBar.ValueChanged += TransparencyTrackBar_ValueChanged;
        }

        private void TransparencyTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)(transparencyTrackBar.Value / 200f);
            _isCodeValueChange = true;
            transparencyNUD.Value = (decimal)newValue;
            _isCodeValueChange = false;
        }

        private void BrightnessTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)(brightnessTrackBar.Value / 100f);
            _isCodeValueChange = true;
            brightnessNUD.Value = (decimal)newValue;
            _isCodeValueChange = false;
        }

        private void SaturationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)(saturationTrackBar.Value / 100f);
            _isCodeValueChange = true;
            saturationNUD.Value = (decimal)newValue;
            _isCodeValueChange = false;
        }

        private void TransparencyNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)transparencyNUD.Value;
            _isCodeValueChange = true;
            transparencyTrackBar.Value = (int)(newValue * 200);
            _isCodeValueChange = false;
        }

        private void BrightnessNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)brightnessNUD.Value;
            _isCodeValueChange = true;
            brightnessTrackBar.Value = (int)(newValue * 100);
            _isCodeValueChange = false;
        }

        private void SaturationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_isCodeValueChange) return;
            float newValue = (float)saturationNUD.Value;
            _isCodeValueChange = true;
            saturationTrackBar.Value = (int)(newValue * 100);
            _isCodeValueChange = false;
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
