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
        public RecordForm()
        {
            InitializeComponent();
            TransparencyKey = Color.LimeGreen;
            recordPanel.BackColor = Color.LimeGreen;
            FormClosing += RecordForm_FormClosing;
            MinimumSize = new Size(50, 100);
        }

        private void RecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
