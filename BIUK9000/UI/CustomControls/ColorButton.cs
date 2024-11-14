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
    public partial class ColorButton : UserControl
    {
        public Color Color { get => button.BackColor; set => button.BackColor = value; }
        public event EventHandler ColorChanged;
        public ColorButton()
        {
            InitializeComponent();
            button.Click += Button_Click;
            button.BackColor = Color.Black;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                if(Color != colorDialog.Color)
                {
                    button.BackColor = colorDialog.Color;
                    ColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
