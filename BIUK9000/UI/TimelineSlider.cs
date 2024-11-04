﻿using System;
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
        //private Giffer _giffer;
        private Timer playTimer;
        public int SelectedFrameIndex { get => timeLineTrackBar.Value; set => timeLineTrackBar.Value = value; }
        public bool PlayTimerRunning { get => playTimer.Enabled; }
        public int Maximum { get => timeLineTrackBar.Maximum; set => timeLineTrackBar.Maximum = value; }
        public int FrameDelay
        {
            get 
            { 
                return (int)frameDelayNUD.Value;
            }
            set
            {
                if(playTimer.Interval != value) playTimer.Interval = value;
                if(frameDelayNUD.Value != value)frameDelayNUD.Value = value;
            }
        }

        public event EventHandler SelectedFrameChanged;
        public event EventHandler FrameDelayChanged;
        public TimelineSlider()
        {
            InitializeComponent();
            timeLineTrackBar.ValueChanged += (sender, args) =>
            {
                SelectedFrameChanged?.Invoke(this, EventArgs.Empty);
            };
            frameDelayNUD.KeyPress += FrameDelayNUD_KeyPress;
            playTimer = new Timer();
            playTimer.Tick += PlayTimer_Tick;
            frameDelayNUD.ValueChanged += (sender, args) => FrameDelayChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            timeLineTrackBar.Value = (timeLineTrackBar.Value + 1) % timeLineTrackBar.Maximum;
        }

        private void FrameDelayNUD_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            e.KeyChar = e.KeyChar = (char)Keys.D2;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (playTimer.Enabled)
            {
                playTimer.Stop();
                playButton.Text = "start";
                SelectedFrameChanged?.Invoke(this, EventArgs.Empty);
            } else
            {
                playTimer.Start();
                playButton.Text = "stop";
            }

        }
    }
}
