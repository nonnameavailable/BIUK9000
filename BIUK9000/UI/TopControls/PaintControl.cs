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
        public Color PaintColorRGB { get => paintColorButton.Color; set => paintColorButton.Color = value; }
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
        public bool LassoAnimateCutout { get => animateCutoutCB.Checked; }
        public bool LassoAnimateComplement {  get => animateComplementCB.Checked; }

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
        public PaintParams GetPaintParams()
        {
            return new PaintParams(Transparency, Tolerance, Thickness, PaintColorARGB);
        }
        public LassoParams GetLassoParams()
        {
            return new LassoParams(LassoIncludeComplement, LassoConstrainBounds, LassoAnimateCutout, LassoAnimateComplement);
        }
    }
    public struct PaintParams
    {
        public PaintParams(int transparency, int tolerance, float thickness, Color color)
        {
            Transparency = transparency;
            Tolerance = tolerance;
            Thickness = thickness;
            Color = color;
        }
        public int Transparency { get; }
        public int Tolerance { get; }
        public float Thickness { get; }
        public Color Color { get; }
    }
    public struct LassoParams
    {
        public LassoParams(bool lic, bool lcb, bool lacu, bool laco)
        {
            IncludeComplement = lic;
            ConstrainBounds = lcb;
            AnimateCutout = lacu;
            AnimateComplement = laco;
        }
        public bool IncludeComplement { get; }
        public bool ConstrainBounds { get; }
        public bool AnimateCutout { get; }
        public bool AnimateComplement { get; }
    }
}
