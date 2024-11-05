using BIUK9000.GifferComponents;
using BIUK9000.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class ControlsPanel : UserControl
    {
        private OVector OriginalMousePosition { get; set; }
        public bool IsLMBDown {  get; set; }
        public bool IsRMBDown {  get; set; }
        public bool DraggingFileForExport { get; set; }
        public int GifExportLossy { get => (int)(GifExportLossyNUD.Value); }
        public int GifExportColors { get => (int)GifExportColorsNUD.Value; }
        public string ImageExportFormat { get => ImageExportFormatCBB.Text; }
        public bool RotationSnap { get => rotationSnapCB.Checked; }
        public bool PositionSnap { get => positionSnapCB.Checked; }
        public ApplyParamsMode SelectedApplyParamsMode { get => (ApplyParamsMode)applyParamsCBB.SelectedItem; }
        public bool DrawHelp { get => drawHelpCB.Checked; }
        public bool UseGifsicle { get => useGifSicleCB.Checked; }
        public bool UseDithering { get => useDitheringCB.Checked; }
        public event EventHandler MustRedraw;
        public event EventHandler ShouldStartDragDrop;
        public event EventHandler SaveButtonClicked;
        public ControlsPanel()
        {
            InitializeComponent();
            SaveButton.MouseDown += SaveButton_MouseDown;
            SaveButton.MouseUp += SaveButton_MouseUp;
            SaveButton.MouseMove += SaveButton_MouseMove;
            ImageExportFormatCBB.SelectedIndex = 0;
            drawHelpCB.CheckedChanged += DrawHelpCB_CheckedChanged;
            OriginalMousePosition = new OVector(0, 0);
            IsLMBDown = false;
            IsRMBDown = false;
            applyParamsCBB.DataSource = Enum.GetValues(typeof(ApplyParamsMode));
            applyParamsCBB.SelectedIndex = 0;
            SaveButton.Click += (sender, args) => SaveButtonClicked?.Invoke(this, args);
            useDitheringCB.CheckedChanged += UseDitheringCB_CheckedChanged;
        }

        private void UseDitheringCB_CheckedChanged(object sender, EventArgs e)
        {
            if (UseDithering) ImageExportFormatCBB.SelectedItem = ".png";
        }

        private void SaveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (ShouldStartDragdrop(e, 10))
            {
                ShouldStartDragDrop?.Invoke(this, e);
            }
        }

        private bool ShouldStartDragdrop(MouseEventArgs e, double distanceLimit)
        {
            OVector currentPosOV = new OVector(e.X, e.Y);
            double draggedDist = currentPosOV.Subtract(OriginalMousePosition).Magnitude;
            return draggedDist > distanceLimit;
        }
        private void DrawHelpCB_CheckedChanged(object sender, EventArgs e)
        {
            MustRedraw?.Invoke(this, EventArgs.Empty);
        }

        private void SaveButton_MouseDown(object sender, MouseEventArgs e)
        {
            OriginalMousePosition = new OVector(e.X, e.Y);
            if(e.Button == MouseButtons.Left)
            {
                IsLMBDown = true;
            } else if(e.Button == MouseButtons.Right)
            {
                IsRMBDown = true;
            }
            
        }
        private void SaveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsLMBDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                IsRMBDown = false;
            }
            DraggingFileForExport = false;
        }
    }
    public enum ApplyParamsMode
    {
        applyChanged,
        applyAll,
        applyNone
    }
}
