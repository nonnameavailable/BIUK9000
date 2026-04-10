using BIUK9000.UI.InputHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.Forms
{
    public partial class RecordForm : Form
    {
        public Point TopLeft { get => recordPanel.PointToScreen(Point.Empty); }
        public int RecordWidth { get => recordPanel.Width; }
        public int RecordHeight { get => recordPanel.Height; }
        public bool IsRecording { get; private set; }
        public event EventHandler StartRecording, StopRecording, Screenshot, FormHidden, FramerateChanged;
        private int _framerate;
        public int Framerate
        {
            get
            {
                return _framerate;
            }
            set
            {
                if(value <= 60 && value >= 1)
                {
                    _framerate = value;
                    SetText(value);
                }
                
            }
        }
        public RecordForm()
        {
            InitializeComponent();
            TransparencyKey = Color.LimeGreen;
            recordPanel.BackColor = Color.LimeGreen;
            FormClosing += RecordForm_FormClosing;
            MinimumSize = new Size(50, 150);
            IsRecording = false;
        }

        private void RecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            FormHidden?.Invoke(this, EventArgs.Empty);
        }
        public void SetRecordMode(bool recording)
        {
            if (recording)
            {
                IsRecording = true;
                Report("Now recording!");
                BackColor = Color.Green;
            }
            else
            {
                IsRecording = false;
                Report("Now NOT recording.");
                BackColor = SystemColors.Control;
            }
        }
        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            if(m.Msg == WM_KEYDOWN)
            {
                if(keyData == Keys.Enter && !IsRecording)
                {
                    if (!CanRecord())
                    {
                        MessageBox.Show("The recording window must be on screen entirely.");
                        return false;
                    }
                    StartRecording?.Invoke(this, EventArgs.Empty);
                    SetRecordMode(true);
                    return true;
                }
                else if(keyData == Keys.Enter && IsRecording)
                {
                    StopRecording?.Invoke(this, EventArgs.Empty);
                    SetRecordMode(false);
                    return true;
                }
                else if(keyData == Keys.S && !IsRecording)
                {
                    if (!CanRecord())
                    {
                        MessageBox.Show("The recording window must be on screen entirely.");
                        return false;
                    }
                    Screenshot?.Invoke(this, EventArgs.Empty);
                    return true;
                }
                else if((keyData == Keys.Oemplus || keyData == Keys.Add) && !IsRecording)
                {
                    Framerate++;
                    FramerateChanged?.Invoke(this, EventArgs.Empty);
                }
                else if((keyData == Keys.OemMinus || keyData == Keys.Subtract) && !IsRecording)
                {
                    Framerate--;
                    FramerateChanged?.Invoke(this, EventArgs.Empty);
                }
            }
            return base.ProcessCmdKey(ref m, keyData);
        }
        public bool CanRecord()
        {
            Rectangle totalScreenBounds = GetTotalScreenBounds();
            Point screenLocation = TopLeft;
            Rectangle pictureBoxBounds = new Rectangle(screenLocation, new Size(RecordWidth, RecordHeight));

            return totalScreenBounds.Contains(pictureBoxBounds);
        }
        private Rectangle GetTotalScreenBounds()
        {
            Rectangle totalBounds = Screen.AllScreens[0].Bounds;
            foreach (Screen screen in Screen.AllScreens)
            {
                totalBounds = Rectangle.Union(totalBounds, screen.Bounds);
            }
            return totalBounds;
        }
        private void Report(string message)
        {
            statusLabel.Text = message;
        }
        private void SetText(int framerate)
        {
            Text = "Enter - start / stop, S - one frame, +- adjust framerate (" + framerate.ToString() + ")";
        }
    }
}
