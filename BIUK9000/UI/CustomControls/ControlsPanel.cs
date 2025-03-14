﻿using BIUK9000.GifferComponents;
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
using System.Diagnostics.Eventing.Reader;

namespace BIUK9000.UI
{
    public partial class ControlsPanel : UserControl
    {
        private OVector OriginalMousePosition { get; set; }
        public bool IsLMBDown { get; set; }
        public bool IsRMBDown { get; set; }
        public bool DraggingFileForExport { get; set; }
        public string ImageExportFormat { get => ImageExportFormatCBB.Text; }
        public int ImageExportJpegQuality { get => (int)ImageExportJpegQualNUD.Value; }
        public bool RotationSnap { get => rotationSnapCB.Checked; }
        public bool PositionSnap { get => positionSnapCB.Checked; }
        public ApplyParamsMode SelectedApplyParamsMode { get => (ApplyParamsMode)applyParamsCBB.SelectedItem; }
        public bool DrawHelp { get => drawHelpCB.Checked; }
        public Mode SelectedMode
        {
            get
            {
                if (toolMoveRB.Checked)
                {
                    return Mode.Move;
                } else if (toolPaintRB.Checked)
                {
                    return Mode.Paint;
                } else//if(toolRecordRB.Checked)
                {
                    return Mode.Record;
                }
            }
            set
            {
                _isCodeChange = true;
                if(value == Mode.Move)
                {
                    toolMoveRB.Checked = true;
                } else if(value == Mode.Paint)
                {
                    toolPaintRB.Checked = true;
                } else if(value == Mode.Record)
                {
                    toolRecordRB.Checked = true;
                }
                _isCodeChange = false;
            }
        }
        //public bool ToolMoveSelectedFlag
        //{
        //    get => toolMoveRB.Checked;
        //    set
        //    {
        //        _isCodeChange = true;
        //        toolMoveRB.Checked = true;
        //        _isCodeChange = false;
        //    }
        //}
        //public bool ToolPaintSelectedFlag { get => toolPaintRB.Checked; set => toolPaintRB.Checked = value; }
        //public bool ToolRecordSelectedFlag { get => toolRecordRB.Checked; set=> toolRecordRB.Checked = value; }
        public InterpolationMode InterpolationMode { get => (InterpolationMode)mpbAAModeCBB.SelectedItem; }

        public event EventHandler MustRedraw;
        public event EventHandler ShouldStartDragDrop;
        public event EventHandler SaveButtonClicked;
        public event EventHandler InterpolationModeChanged;
        //public event EventHandler ToolMoveSelected, ToolPaintSelected, ToolRecordSelected;
        public event EventHandler ModeChanged;

        private bool _isCodeChange;
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
            applyParamsCBB.DataSource = Enum.GetValues(typeof(ApplyParamsMode));
            mpbAAModeCBB.DataSource = Enum.GetValues(typeof(InterpolationMode))
                .Cast<InterpolationMode>()
                .Where(mode => mode != InterpolationMode.Invalid)
                .ToList();
            mpbAAModeCBB.SelectedItem = InterpolationMode.HighQualityBicubic;
            mpbAAModeCBB.SelectedIndexChanged += MpbAAModeCBB_SelectedIndexChanged;
            applyParamsCBB.SelectedIndex = 0;
            ImageExportFormatCBB.SelectedIndex = 1;
            saveButton.Click += (sender, args) => SaveButtonClicked?.Invoke(this, args);
            toolMoveRB.CheckedChanged += ToolMoveRB_CheckedChanged;
            toolPaintRB.CheckedChanged += ToolPaintRB_CheckedChanged;
            toolRecordRB.CheckedChanged += ToolRecordRB_CheckedChanged;
        }

        private void ToolRecordRB_CheckedChanged(object sender, EventArgs e)
        {
            //if (ToolRecordSelectedFlag && !_isCodeChange) ToolRecordSelected?.Invoke(this, EventArgs.Empty);
            if(toolRecordRB.Checked && !_isCodeChange) ModeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ToolPaintRB_CheckedChanged(object sender, EventArgs e)
        {
            //if (ToolPaintSelectedFlag && ! _isCodeChange) ToolPaintSelected?.Invoke(this, EventArgs.Empty);
            if (toolPaintRB.Checked && !_isCodeChange) ModeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ToolMoveRB_CheckedChanged(object sender, EventArgs e)
        {
            //if (ToolMoveSelectedFlag && !_isCodeChange) ToolMoveSelected?.Invoke(this, EventArgs.Empty);
            if (toolMoveRB.Checked && !_isCodeChange) ModeChanged?.Invoke(this, EventArgs.Empty);
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
    public enum Tools
    {
        Move,
        Paint,
        Record
    }
    public enum Mode
    {
        Move,
        Paint,
        Record
    }
}
