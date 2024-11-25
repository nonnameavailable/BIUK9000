using BIUK9000.GifferComponents;
using System;
using System.Windows.Forms;
using BIUK9000.GifferComponents.GFLVariants;

namespace BIUK9000.UI.LayerParamControls
{
    public partial class PlainGFLParamControl : UserControl, IGFLParamControl
    {
        private ShapeGFL.DrawShape DrawShape
        {
            get
            {
                if (rectangleRB.Checked)
                {
                    return ShapeGFL.DrawShape.Rectangle;
                }
                else if (ellipseRB.Checked)
                {
                    return ShapeGFL.DrawShape.Ellipse;
                }
                else return ShapeGFL.DrawShape.Rectangle;
            }
        }
        public PlainGFLParamControl()
        {
            InitializeComponent();
            colorButton.ColorChanged += (sender, args) => OnParamsChanged();
            ellipseRB.CheckedChanged += EllipseRB_CheckedChanged;
            rectangleRB.CheckedChanged += RectangleRB_CheckedChanged;
        }

        private void RectangleRB_CheckedChanged(object sender, EventArgs e)
        {
            if (rectangleRB.Checked) OnParamsChanged();
        }

        private void EllipseRB_CheckedChanged(object sender, EventArgs e)
        {
            if(ellipseRB.Checked) OnParamsChanged();
        }

        private void OnParamsChanged()
        {
            ParamsChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ParamsChanged;

        public void LoadParams(GFL gfl)
        {
            ShapeGFL sgfl = (ShapeGFL)gfl;
            colorButton.Color = sgfl.Color;
            if (sgfl.Shape == ShapeGFL.DrawShape.Rectangle)
            {
                rectangleRB.Checked = true;
            } else if (sgfl.Shape == ShapeGFL.DrawShape.Ellipse)
            {
                ellipseRB.Checked = true;
            }
        }
        public void SaveParams(GFL gfl)
        {
            ShapeGFL sgfl = (ShapeGFL)gfl;
            sgfl.Color = colorButton.Color;
            sgfl.Shape = DrawShape;
        }
    }
}
