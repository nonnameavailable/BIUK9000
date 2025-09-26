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
        private bool ChangeFPS { get => changeFpsCB.Checked; }
        private bool ChangeMaxSide { get => maxSideLengthCB.Checked; }
        public VideoImportForm()
        {
            InitializeComponent();
            timelineSlider1.SelectedFrameChanged += OnSFIChanged;
            FormClosed += OnFormClosed;
            changeFpsCB.CheckedChanged += ChangeFpsCB_CheckedChanged;
            maxSideLengthCB.CheckedChanged += MaxSideLengthCB_CheckedChanged;
            maxSideLengthNUD.ValueChanged += (sender, args) => UpdateMemoryLabel();
            changeFpsNUD.ValueChanged += (sender, args) => UpdateMemoryLabel();
            _previewFrames = new();
        }
        private void UpdateMemoryLabel()
        {
            memoryLabel.Text = EstimatedMemoryConsumption();
        }
        private void MaxSideLengthCB_CheckedChanged(object sender, EventArgs e)
        {
            if (maxSideLengthCB.Checked)
            {
                maxSideLengthNUD.Enabled = true;
            }
            else maxSideLengthNUD.Enabled = false;
            UpdateMemoryLabel();
        }

        private void ChangeFpsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (changeFpsCB.Checked)
            {
                changeFpsNUD.Enabled = true;
            } else changeFpsNUD.Enabled = false;
            UpdateMemoryLabel();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _previewFrames.ForEach(frame =>  frame.Dispose());
            _previewFrames.Clear();
            myPictureBox1.Image?.Dispose();
        }

        private void OnSFIChanged(object sender, EventArgs e)
        {
            SFI = timelineSlider1.SelectedFrameIndex;
            UpdateMainPictureBox();
        }

        public void LoadVideo(string path)
        {
            _vi = VideoFrameExtractor.GetVideoInfo(path);
            if (_vi.DurationSeconds < 0)
            {
                PutDurationNotFoundImage();
                timelineSlider1.Enabled = false;
                return;
            } else if (VI.DurationSeconds < 60)
            {
                var feo = new FrameExtractOptions
                {
                    MaxSideLength = MaxPreviewSideLength,
                    TargetFPS = PreviewFPS
                };
                _previewFrames = VideoFrameExtractor.ExtractFrames(path, feo);
            }
            else
            {
                _previewFrames = VideoFrameExtractor.ExtractFramesFast(path, MaxPreviewSideLength, MaxPreviewFrames);
            }
            timelineSlider1.Maximum = _previewFrames.Count - 1;
            Report(_vi.ToString());
            memoryLabel.Text = EstimatedMemoryConsumption();
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
        private void PutDurationNotFoundImage()
        {
            Bitmap bmp = new(myPictureBox1.Width, myPictureBox1.Height);
            using Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            using Font f = new Font("Arial", myPictureBox1.Height * 0.1f);
            g.DrawString($"Duration could not be determined{Environment.NewLine}Preview will not be loaded", f, Brushes.Black, 10, 10);
            myPictureBox1.Image = bmp;
        }
        private string EstimatedMemoryConsumption()
        {
            VideoInfo tvi = VI.Copy();
            if (ChangeFPS)
            {
                tvi.FPS = (double)changeFpsNUD.Value;
            }
            if (ChangeMaxSide)
            {
                Size s = VideoFrameExtractor.NewSize(tvi, FrameExtractOptions());
                tvi.Width = s.Width;
                tvi.Height = s.Height;
            }
            long bytes = tvi.EstimatedMemoryUsageBytes;
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}
