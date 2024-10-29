using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.GifferComponents;

namespace BIUK9000.UI
{
    public partial class TimelineSlider : UserControl
    {
        private Giffer _giffer;
        public Giffer Giffer
        {
            get => _giffer;
            set
            {
                _giffer = value;
                if (value != null) timeLineTrackBar.Maximum = value.Frames.Count - 1;
            }
        }
        public TrackBar Slider { get => timeLineTrackBar; }
        public GifFrame SelectedFrame { get => Giffer.Frames[timeLineTrackBar.Value]; }
        public event EventHandler SelectedFrameChanged;

        public TimelineSlider()
        {
            InitializeComponent();
            timeLineTrackBar.ValueChanged += (sender, args) =>
            {
                SelectedFrameChanged?.Invoke(this, EventArgs.Empty);
                UpdateDelayNUD();
            };
            frameDelayNUD.KeyPress += FrameDelayNUD_KeyPress;
        }

        private void FrameDelayNUD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            e.KeyChar = e.KeyChar = (char)Keys.D2;
        }

        private void frameDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            SelectedFrame.FrameDelay = (int)frameDelayNUD.Value;
        }
        public void UpdateDelayNUD()
        {
            frameDelayNUD.Value = SelectedFrame.FrameDelay;
        }
    }
}
