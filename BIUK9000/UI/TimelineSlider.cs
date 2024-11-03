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
        private Timer playTimer;
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
        public event EventHandler FrameDelayChanged;
        public TimelineSlider()
        {
            InitializeComponent();
            timeLineTrackBar.ValueChanged += (sender, args) =>
            {
                frameDelayNUD.ValueChanged -= frameDelayNUD_ValueChanged;
                if (_giffer != null) frameDelayNUD.Value = SelectedFrame.FrameDelay;
                frameDelayNUD.ValueChanged += frameDelayNUD_ValueChanged;
                SelectedFrameChanged?.Invoke(this, EventArgs.Empty);
            };
            frameDelayNUD.KeyPress += FrameDelayNUD_KeyPress;
            playTimer = new Timer();
            playTimer.Tick += PlayTimer_Tick;
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            Slider.Value = (Slider.Value + 1) % Slider.Maximum;
            playTimer.Interval = SelectedFrame.FrameDelay;
        }

        private void FrameDelayNUD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            e.KeyChar = e.KeyChar = (char)Keys.D2;
        }

        private void frameDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_giffer == null) return;
            SelectedFrame.FrameDelay = (int)frameDelayNUD.Value;
            FrameDelayChanged?.Invoke(this, EventArgs.Empty);
        }
        public void UpdateDelayNUD()
        {
            frameDelayNUD.Value = SelectedFrame.FrameDelay;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (_giffer == null) return;
            if (playTimer.Enabled)
            {
                playTimer.Stop();
                playButton.Text = "start";

            } else
            {
                playTimer.Interval = SelectedFrame.FrameDelay;
                playTimer.Start();
                playButton.Text = "stop";
            }

        }
    }
}
