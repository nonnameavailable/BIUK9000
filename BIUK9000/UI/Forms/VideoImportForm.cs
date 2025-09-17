using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.IO;

namespace BIUK9000.UI.Forms
{
    public partial class VideoImportForm : Form
    {
        private const int MaxPreviewFrames = 50;
        private const int MaxPreviewSideLength = 300;
        private double PreviewFPS
        {
            get
            {
                return (double)MaxPreviewFrames / _vi.DurationSeconds;
            }
        }
        private VideoInfo _vi;
        public VideoInfo VI { get => _vi; }
        private List<Bitmap> _previewFrames;
        private List<int> Marks { get => timelineSlider1.Marks; }
        private Image MainImage { get => myPictureBox1.Image; set => myPictureBox1.Image = value; }
        private int SFI { get => timelineSlider1.SelectedFrameIndex; set => timelineSlider1.SelectedFrameIndex = value; }
        public TimeSpan? StartTime
        {
            get
            {
                if(Marks.Count == 0) return null;
                double ratio = (double)Marks[0] / timelineSlider1.Maximum;
                return TimeSpan.FromSeconds(_vi.DurationSeconds * ratio);
            }
        }
        public TimeSpan? EndTime
        {
            get
            {
                if (Marks.Count < 2) return null;
                double ratio = (double)Marks[1] / timelineSlider1.Maximum;
                return TimeSpan.FromSeconds(_vi.DurationSeconds * ratio);
            }
        }
        public TimeSpan? MarkedDuration
        {
            get
            {
                if (EndTime.HasValue && StartTime.HasValue)
                {
                    return EndTime - StartTime;
                }
                else return null;
            }
        }
        public VideoImportForm()
        {
            InitializeComponent();
            timelineSlider1.SelectedFrameChanged += OnSFIChanged;
            FormClosed += OnFormClosed;
            changeFpsCB.CheckedChanged += ChangeFpsCB_CheckedChanged;
            maxSideLengthCB.CheckedChanged += MaxSideLengthCB_CheckedChanged;
            _previewFrames = new();
        }

        private void MaxSideLengthCB_CheckedChanged(object sender, EventArgs e)
        {
            if (maxSideLengthCB.Checked)
            {
                maxSideLengthNUD.Enabled = true;
            }
            else maxSideLengthNUD.Enabled = false;
        }

        private void ChangeFpsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (changeFpsCB.Checked)
            {
                changeFpsNUD.Enabled = true;
            } else changeFpsNUD.Enabled = false;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _previewFrames.ForEach(frame =>  frame.Dispose());
            _previewFrames.Clear();
        }

        private void OnSFIChanged(object sender, EventArgs e)
        {
            SFI = timelineSlider1.SelectedFrameIndex;
            UpdateMainPictureBox();
        }

        public void LoadVideo(string path)
        {
            _vi = VideoFrameExtractor.GetVideoInfo(path);
            if(VI.DurationSeconds < 60)
            {
                var feo = new FrameExtractOptions
                {
                    MaxSideLength = MaxPreviewSideLength,
                    TargetFPS = PreviewFPS
                };
                _previewFrames = VideoFrameExtractor.ExtractFrames(path, feo);
            } else
            {
                _previewFrames = VideoFrameExtractor.ExtractFramesFast(path, MaxPreviewSideLength, MaxPreviewFrames);
            }
            timelineSlider1.Maximum = _previewFrames.Count - 1;
            Report(_vi.ToString());
            UpdateMainPictureBox();
        }
        private void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = new Bitmap(_previewFrames[SFI]);
        }
        public FrameExtractOptions FrameExtractOptions()
        {
            double? fps = changeFpsCB.Checked ? (double)changeFpsNUD.Value : null;
            int? maxSide = maxSideLengthCB.Checked ? (int)maxSideLengthNUD.Value : null;

            return new FrameExtractOptions
            {
                TargetFPS = fps,
                MaxSideLength = maxSide,
                StartTime = StartTime,
                Duration = MarkedDuration
            };
        }
        private void Report(string text)
        {
            statusLabel.Text = text;
        }
    }
}
