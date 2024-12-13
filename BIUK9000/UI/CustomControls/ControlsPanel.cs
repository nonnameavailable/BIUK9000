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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using AnimatedGif;

namespace BIUK9000.UI
{
    public partial class ControlsPanel : UserControl
    {
        private OVector OriginalMousePosition { get; set; }
        public bool IsLMBDown { get; set; }
        public bool IsRMBDown { get; set; }
        public bool DraggingFileForExport { get; set; }
        public int GifExportLossy { get => (int)(GifExportLossyNUD.Value); }
        public int GifExportColors { get => (int)GifExportColorsNUD.Value; }
        public string ImageExportFormat { get => ImageExportFormatCBB.Text; }
        public GifQuality GifQuality { get => (GifQuality)gifExportModeCBB.SelectedItem; }
        public int ImageExportJpegQuality { get => (int)ImageExportJpegQualNUD.Value; }
        public bool RotationSnap { get => rotationSnapCB.Checked; }
        public bool PositionSnap { get => positionSnapCB.Checked; }
        public ApplyParamsMode SelectedApplyParamsMode { get => (ApplyParamsMode)applyParamsCBB.SelectedItem; }
        public bool DrawHelp { get => drawHelpCB.Checked; }
        public bool UseGifsicle { get => useGifSicleCB.Checked; }
        public bool UseDithering { get => useDitheringCB.Checked; }
        public bool ToolMoveSelectedFlag { get => toolMoveRB.Checked; }
        public bool ToolPaintSelectedFlag { get => toolPaintRB.Checked; }
        public InterpolationMode InterpolationMode { get => (InterpolationMode)mpbAAModeCBB.SelectedItem; }
        public bool CreateFrames { get => createFramesCB.Checked; }

        public event EventHandler MustRedraw;
        public event EventHandler ShouldStartDragDrop;
        public event EventHandler SaveButtonClicked;
        public event EventHandler InterpolationModeChanged;
        public event EventHandler ToolMoveSelected, ToolPaintSelected;
        public ControlsPanel()
        {
            InitializeComponent();
            saveButton.MouseDown += SaveButton_MouseDown;
            saveButton.MouseUp += SaveButton_MouseUp;
            saveButton.MouseMove += SaveButton_MouseMove;
            drawHelpCB.CheckedChanged += (sender, args) => OnMustRedraw();
            OriginalMousePosition = new OVector(0, 0);
            IsLMBDown = false;
            IsRMBDown = false;
            gifExportModeCBB.DataSource = Enum.GetValues(typeof(GifQuality));
            applyParamsCBB.DataSource = Enum.GetValues(typeof(ApplyParamsMode));
            mpbAAModeCBB.DataSource = Enum.GetValues(typeof(InterpolationMode))
                .Cast<InterpolationMode>()
                .Where(mode => mode != InterpolationMode.Invalid)
                .ToList();
            mpbAAModeCBB.SelectedItem = InterpolationMode.HighQualityBicubic;
            mpbAAModeCBB.SelectedIndexChanged += MpbAAModeCBB_SelectedIndexChanged;
            applyParamsCBB.SelectedIndex = 0;
            ImageExportFormatCBB.SelectedIndex = 1;
            gifExportModeCBB.SelectedIndex = 1;
            saveButton.Click += (sender, args) => SaveButtonClicked?.Invoke(this, args);
            useDitheringCB.CheckedChanged += UseDitheringCB_CheckedChanged;
            toolMoveRB.CheckedChanged += ToolMoveRB_CheckedChanged;
            toolPaintRB.CheckedChanged += ToolPaintRB_CheckedChanged;
        }

        private void ToolPaintRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ToolPaintSelectedFlag) ToolPaintSelected?.Invoke(this, EventArgs.Empty);
        }

        private void ToolMoveRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ToolMoveSelectedFlag) ToolMoveSelected?.Invoke(this, EventArgs.Empty);
        }

        private void MpbAAModeCBB_SelectedIndexChanged(object sender, EventArgs e)
        {
            InterpolationModeChanged?.Invoke(this, new EventArgs());
            OnMustRedraw();
        }
        protected void OnMustRedraw()
        {
            MustRedraw?.Invoke(this, EventArgs.Empty);
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

        private void SaveButton_MouseDown(object sender, MouseEventArgs e)
        {
            OriginalMousePosition = new OVector(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                IsLMBDown = true;
            }
            else if (e.Button == MouseButtons.Right)
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
        public void SetPaintMode(bool setValue)
        {
            saveButton.Enabled = !setValue;
        }
    }
    public enum ApplyParamsMode
    {
        applyChanged,
        applyAll,
        applyNone
    }
}
