using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                timeLineTrackBar.Maximum = value.Frames.Count - 1;
            }
        }
        public TrackBar Slider { get => timeLineTrackBar; }
        public GifFrame SelectedFrame { get => Giffer.Frames[timeLineTrackBar.Value]; }
        public event EventHandler SelectedFrameChanged;

        public TimelineSlider()
        {
            InitializeComponent();
            timeLineTrackBar.ValueChanged += TimeLineTrackBar_ValueChanged;
        }

        private void TimeLineTrackBar_ValueChanged(object sender, EventArgs e)
        {
            SelectedFrameChanged?.Invoke(this, EventArgs.Empty);
            SetFramePreview(SelectedFrame);
        }

        private void SetFramePreview(GifFrame frame)
        {
            framePreviewPictureBox.Image?.Dispose();
            framePreviewPictureBox.Image = frame.CompleteBitmap();
        }
    }
}
