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
        public Color PaintColorRGB { get => paintColorButton.Color; }
        public Color PaintColorARGB
        {
            get
            {
                Color c = PaintColorRGB;
                return Color.FromArgb(Transparency, c.R, c.G, c.B);
            }
        }
        public int Transparency { get => transparencyTrackBar.Value; }
        public int Tolerance { get =>  toleranceTrackBar.Value; }
        public float Thickness { get => (float)thicknessNUD.Value; }
        public bool LassoIncludeComplement { get => lassoIncludeComplementCB.Checked; }
        public bool LassoConstrainBounds { get => lassoConstrainCB.Checked; }
        public PaintTool SelectedPaintTool
        {
            get
            {
                if (replaceColorRB.Checked)
                {
                    return PaintTool.ReplaceColor;
                } else if(drawLineRB.Checked)
                {
                    return PaintTool.DrawLine;
                } else if (lassoRB.Checked)
                {
                    return PaintTool.Lasso;
                } else if (fillColorRB.Checked)
                {
                    return PaintTool.FillColor;
                } 
                else
                {
                    return PaintTool.DrawLine;
                }
            }
        }
        public PaintControl()
        {
            InitializeComponent();
            transparencyDisplayLabel.Text = Transparency.ToString();
            toleranceDisplayLabel.Text = Tolerance.ToString();
            transparencyTrackBar.ValueChanged += (sender, args) => transparencyDisplayLabel.Text = Transparency.ToString();
            toleranceTrackBar.ValueChanged += (sender, args) => toleranceDisplayLabel.Text = Tolerance.ToString();
        }
        public enum PaintTool
        {
            DrawLine,
            ReplaceColor,
            FillColor,
            Lasso
        }
    }
}
