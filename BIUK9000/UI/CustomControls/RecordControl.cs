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
        public int FPS { get => (int)fpsNUD.Value; set => fpsNUD.Value = value; }
        public RecordControl()
        {
            InitializeComponent();
            startRecBtn.Click += (sender, args) => Start?.Invoke(sender, args);
            stopRecBtn.Click += (sender, args) => Stop?.Invoke(sender, args);
        }
    }
}
