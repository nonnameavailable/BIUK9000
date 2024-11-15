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
    public partial class PaintControl : UserControl
    {
        public Color PaintColor { get => paintColorButton.Color; }
        public int Transparency { get => transparencyTrackBar.Value; }
        public float Thickness { get => (float)thicknessNUD.Value; }
        public PaintTool SelectedPaintTool
        {
            get
            {
                if (deleteColorRB.Checked)
                {
                    return PaintTool.DeleteColor;
                } else if(drawLineRB.Checked)
                {
                    return PaintTool.DrawLine;
                } else
                {
                    return PaintTool.DrawLine;
                }
            }
        }
        public PaintControl()
        {
            InitializeComponent();
            transparencyDisplayLabel.Text = Transparency.ToString();
            transparencyTrackBar.ValueChanged += (sender, args) => transparencyDisplayLabel.Text = Transparency.ToString();
        }
        public enum PaintTool
        {
            DrawLine,
            DeleteColor
        }
    }
}
