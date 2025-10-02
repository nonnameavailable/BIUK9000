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
    public partial class PromptForm : Form
    {
        public string NudDescription { get => nud1Label.Text; set => nud1Label.Text = value; }
        public decimal NudMin { get => numericUpDown1.Minimum; set => numericUpDown1.Minimum = value; }
        public decimal NudMax { get => numericUpDown1.Maximum; set => numericUpDown1.Maximum = value; }
        public decimal NudValue { get => numericUpDown1.Value; set => numericUpDown1.Value = value; }
        public PromptForm()
        {
            InitializeComponent();
        }
    }
}
