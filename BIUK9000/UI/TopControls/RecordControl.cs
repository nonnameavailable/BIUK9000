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
    public partial class RecordControl : UserControl
    {
        public event EventHandler Start;
        public event EventHandler Stop;
        public event EventHandler Screenshot;

        public bool RecordSound { get => recordSoundCB.Checked; }
        public int FPS { get => (int)fpsNUD.Value; set => fpsNUD.Value = value; }
        public RecordControl()
        {
            InitializeComponent();
            startRecBtn.Click += (sender, args) =>
            {
                RecMode(true);
                Start?.Invoke(sender, args);
            };
            stopRecBtn.Click += (sender, args) =>
            {
                RecMode(false);
                Stop?.Invoke(sender, args);
            };
            screenshotBTN.Click += (sender, args) => Screenshot?.Invoke(sender, args);
            RecMode(false);
        }
        public void RecMode(bool val)
        {
            if (val)
            {
                startRecBtn.Text = "Recording";
                startRecBtn.Enabled = false;
                stopRecBtn.Enabled = true;
            } else
            {
                startRecBtn.Text = "Start";
                startRecBtn.Enabled = true;
                stopRecBtn.Enabled = false;
            }
        }
    }
}
