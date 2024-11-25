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
        public HSBPanel()
        {
            InitializeComponent();
            brightnessTrackBar.ValueChanged += (sender, args) => OnHueSatChanged();
            saturationTrackBar.ValueChanged += (sender, args) => OnHueSatChanged();

            brightnessTrackBar.MouseDown += (sender, args) => ChangeStarted?.Invoke(this, EventArgs.Empty);
            saturationTrackBar.MouseDown += (sender, args) => ChangeStarted?.Invoke(this, EventArgs.Empty);

            brightnessTrackBar.MouseUp += (sender, args) => ChangeEnded?.Invoke(this, EventArgs.Empty);
            saturationTrackBar.MouseUp += (sender, args) => ChangeEnded?.Invoke(this, EventArgs.Empty);
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
