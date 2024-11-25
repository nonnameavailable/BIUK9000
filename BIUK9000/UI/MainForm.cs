﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using BIUK9000.GifferComponents;
using System.Drawing.Text;
using BIUK9000.Properties;
using BIUK9000.Dithering;
using BIUK9000.UI.LayerParamControls;
using System.Drawing.Drawing2D;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public ControlsPanel MainControlsPanel { get => controlsPanel; }
        public GifFrame SelectedFrame { get => MainGiffer.Frames[MainTimelineSlider.SelectedFrameIndex]; }
        public int SelectedFrameIndex { get => MainTimelineSlider.SelectedFrameIndex; }
        public int SelectedLayerIndex { get => mainLayersPanel.SelectedLayerIndex; }
        public Bitmap SelectedFrameAsBitmap { get => MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp, controlsPanel.InterpolationMode); }
        private GFL SelectedLayer { get => mainLayersPanel.SelectedLayer; }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        public bool IsShiftDown { get; set; }
        public bool IsCtrlDown { get; set; }
        private float _originalLayerRotation;
        private OVector _originalLCtM;
        private Control _lpcBackup;
        private Control _paintControl;
        public bool IsLMBDown { get => mainPictureBox.IsLMBDown; }
        public bool IsRMBDown { get => mainPictureBox.IsRMBDown; }
        public bool IsMMBDown { get => mainPictureBox.IsMMBDown; }
        private Timer _updateTimer;
        private Point _prevMousePos;
        public GifferController GifferC { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            mainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;


            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;

            _updateTimer = new Timer();
            _updateTimer.Interval = 17;
            _updateTimer.Tick += (sender, args) => UpdateMainPictureBox();

            KeyPreview = true;

            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;

            mainLayersPanel.LayerOrderChanged += mainLayersPanel_LayerOrderChanged;
            mainLayersPanel.SelectedLayerChanged += MainLayersPanel_SelectedLayerChanged;
            mainLayersPanel.LayerVisibilityChanged += mainLayersPanel_LayerVisibilityChanged;
            mainLayersPanel.LayerDeleteButtonClicked += mainLayersPanel_LayerDeleteButtonClicked;

            controlsPanel.MustRedraw += (sender, args) => { if (MainGiffer != null) UpdateMainPictureBox(); };
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;
            controlsPanel.InterpolationModeChanged += (sender, args) => mainPictureBox.InterpolationMode = ((ControlsPanel)sender).InterpolationMode;
            controlsPanel.ToolPaintSelected += (sender, args) => SetPaintMode(true);
            controlsPanel.ToolMoveSelected += (sender, args) => SetPaintMode(false);

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;

            lerpButton.Click += LerpButton_Click;

            deleteFramesButton.Click += DeleteFramesButton_Click;
            dupeFrameButton.Click += DupeFrameButton_Click;

            _paintControl = new PaintControl();
            lerpModeCBB.SelectedIndex = 0;
            mainPictureBox.InterpolationMode = controlsPanel.InterpolationMode;

            hsbPanel.HueSatChanged += HsbPanel_HueSatChanged;
            hsbPanel.ChangeStarted += (sender, args) => GifferC?.SaveLayerStateForApply(SelectedFrameIndex, SelectedLayerIndex);
            hsbPanel.ChangeEnded += (sender, args) => GifferC?.ApplyLayerParams(SelectedFrameIndex, SelectedLayerIndex, controlsPanel.SelectedApplyParamsMode);
        }

        private void MainLayersPanel_SelectedLayerChanged(object sender, EventArgs e)
        {
            UpdateLayerParamsUI();
            GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
            hsbPanel.Saturation = SelectedLayer.Saturation;
            hsbPanel.Brightness = SelectedLayer.Brightness;
        }

        private void HsbPanel_HueSatChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            SelectedLayer.Saturation = hsbPanel.Saturation;
            SelectedLayer.Brightness = hsbPanel.Brightness;
            UpdateMainPictureBox();
        }

        private void DupeFrameButton_Click(object sender, EventArgs e)
        {
            GifferC.DupeFrame(SelectedFrameIndex, (int)frameDupeCountNUD.Value);
            CompleteUIUpdate();
        }

        private void SetPaintMode(bool setValue)
        {
            MainTimelineSlider.Enabled = !setValue;
            mainLayersPanel.Enabled = !setValue;
            markLerpPanel.Enabled = !setValue;
            controlsPanel.SetPaintMode(setValue);
            AllowDrop = !setValue;
            if (GifferC == null) return;
            if (setValue)
            {
                if (layerParamsPanel.Controls.Count > 0)
                {
                    _lpcBackup = layerParamsPanel.Controls[0];
                    layerParamsPanel.Controls.Clear();
                }
                layerParamsPanel.Controls.Add(_paintControl);
            }
            else
            {
                layerParamsPanel.Controls.Clear();
                if(_lpcBackup != null)layerParamsPanel.Controls.Add(_lpcBackup);
            }
        }

        private void DeleteFramesButton_Click(object sender, EventArgs e)
        {
            if (MainGiffer == null || MainGiffer.FrameCount < 2) return;
            if (askDeleteCB.Checked)
            {
                if (MessageBox.Show("Do you really want to delete these frames?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            }
            List<int> marks = MainTimelineSlider.Marks;
            if(marks.Count == 0)
            {
                MainGiffer.RemoveFrames(new List<int>() { SelectedFrameIndex });
            } else if(marks.Count > 0)
            {
                MainGiffer.RemoveFrames(MainTimelineSlider.Marks);
            }
            mainTimelineSlider.ClearMarks();
            CompleteUIUpdate();
        }

        private void LerpButton_Click(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            string lerpMode = lerpModeCBB.SelectedItem.ToString();
            if(lerpMode == "trace")
            {
                GifferC.LerpExecute(MainTimelineSlider.Marks, SelectedLayerIndex, mainPictureBox.MouseTrace);
            } else if(lerpMode == "line")
            {
                GifferC.LerpExecute(MainTimelineSlider.Marks, SelectedLayerIndex);
            }
            MainTimelineSlider.ClearMarks();
        }
        public void ApplyLayerParamsToSubsequentLayers(int index = -1)
        {
            int i = SelectedLayerIndex;
            if (index >= 0)
            {
                i = index;
            }
            GifferC.ApplyLayerParams(SelectedFrameIndex, i, controlsPanel.SelectedApplyParamsMode);
        }
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (controlsPanel.DraggingFileForExport) return;
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            bool importSucceeded = GifferIO.FileImport(filePaths, this);
            if (importSucceeded)
            {
                CompleteUIUpdate();
                mainLayersPanel.SelectNewestLayer();
            }
        }

        public void SetNewGiffer(Giffer newGiffer)
        {
            MainGiffer?.Dispose();
            MainGiffer = newGiffer;
            GifferC = new GifferController(newGiffer);
            CompleteUIUpdate(false);
            GifferC.SaveLayerStateForApply(SelectedFrameIndex, SelectedLayerIndex);
            GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
        }

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp, InterpolationMode.Low);
            //if (MainImage == null)
            //{
            //    MainImage = MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp);
            //}
            //else
            //{
            //    using Graphics g = Graphics.FromImage(MainImage);
            //    g.CompositingMode = CompositingMode.SourceCopy;
            //    g.Clear(Color.FromArgb(0, 0, 0, 0));
            //    MainGiffer.DrawFrame(SelectedFrame, controlsPanel.DrawHelp, g);
            //}
            //mainPictureBox.Invalidate();
        }
        private void UpdateLayerParamsUI()
        {
            bool cond = GifferC.ShouldSwitchLPC(SelectedFrameIndex, mainLayersPanel.SelectedLayerIndex);
            if (cond)
            {
                if (layerParamsPanel.Controls.Count > 0)
                {
                    layerParamsPanel.Controls[0].Dispose();
                }
                layerParamsPanel.Controls.Clear();
                IGFLParamControl lpc = GFLParamsControlFactory.GFLParamControl(SelectedLayer);
                if(lpc != null)
                {
                    lpc.LoadParams(SelectedLayer);
                    lpc.ParamsChanged += (sender, args) =>
                    {
                        GifferC.SaveLayerStateForApply(SelectedFrameIndex, mainLayersPanel.SelectedLayerIndex);
                        lpc.SaveParams(SelectedLayer);
                        UpdateMainPictureBox();
                        ApplyLayerParamsToSubsequentLayers();
                    };
                    Control lpcc = lpc as Control;
                    lpcc.Dock = DockStyle.Fill;
                    layerParamsPanel.Controls.Add(lpcc);
                }
            }
            else
            {
                if (layerParamsPanel.Controls.Count > 0)
                {
                    IGFLParamControl lpc = layerParamsPanel.Controls[0] as IGFLParamControl;
                    lpc.LoadParams(SelectedLayer);
                }
            }

        }

        #region key event handling
        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (MainGiffer == null || ActiveControl is TextBox || ActiveControl is IGFLParamControl || controlsPanel.ToolPaintSelectedFlag) return base.ProcessKeyPreview(ref m);
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    if (MainTimelineSlider.SelectedFrameIndex < MainTimelineSlider.Maximum) MainTimelineSlider.SelectedFrameIndex += 1;
                    CompleteUIUpdate();
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    if (MainTimelineSlider.SelectedFrameIndex > 0) MainTimelineSlider.SelectedFrameIndex -= 1;
                    CompleteUIUpdate();
                    return true;
                }
                else if (keyData == Keys.T || keyData == Keys.B)
                {
                    GifferC.AddLayerFromKey(keyData);
                    CompleteUIUpdate();
                    mainLayersPanel.SelectNewestLayer();
                    return true;
                }
                else if (keyData == Keys.F)
                {
                    GifferC.Mirror();
                    CompleteUIUpdate();
                }
                else if (keyData == Keys.ShiftKey)
                {
                    IsShiftDown = true;
                    return true;
                }
                else if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = true;
                    return true;
                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {

                    IsShiftDown = false;
                    return true;
                }
                if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = false;
                    return true;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }
        #endregion
        #region mouse event handling

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            if (controlsPanel.ToolPaintSelectedFlag)
            {
                if(SelectedLayer is BitmapGFL)
                {
                    PaintControl pc = _paintControl as PaintControl;
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.Lasso)
                    {
                        List<Point> translatedPoints = mainPictureBox.MouseTrace.Select(point => point = GifferC.MousePositionOnLayer(SelectedFrameIndex, SelectedLayerIndex, point)).ToList();
                        Bitmap cutoutBitmap = Painter.LassoCutout(((BitmapGFL)SelectedLayer).OriginalBitmap, translatedPoints.ToArray(), pc.LassoConstrainBounds);
                        BitmapGFL bgfl = new BitmapGFL(cutoutBitmap, MainGiffer.NextLayerID());
                        bgfl.CopyParameters(SelectedLayer);
                        GifferC.AddLayer(bgfl);
                        if (pc.LassoIncludeComplement)
                        {
                            Bitmap complementBitmap = Painter.LassoComplement(((BitmapGFL)SelectedLayer).OriginalBitmap, translatedPoints.ToArray());
                            BitmapGFL cbgfl = new BitmapGFL(complementBitmap, MainGiffer.NextLayerID());
                            cbgfl.CopyParameters(SelectedLayer);
                            GifferC.AddLayer(cbgfl);
                        }
                        mainLayersPanel.DisplayLayers(SelectedFrame);
                    }
                }
            }
            if (!IsLMBDown && !IsMMBDown & !IsRMBDown)
            {
                UpdateTimer.Stop();
            }
            ApplyLayerParamsToSubsequentLayers();
            UpdateMainPictureBox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mainPictureBox.Focus();
            if (MainGiffer == null) return;
            GFL cgfl = SelectedLayer;
            MainGiffer.Save();
            GifferC.SaveLayerStateForApply(SelectedFrameIndex, SelectedLayerIndex);
            _originalLayerRotation = cgfl.Rotation;
            _originalLCtM = LayerCenterToMouse();
            if (controlsPanel.ToolPaintSelectedFlag && SelectedLayer is BitmapGFL)
            {
                if (IsLMBDown)
                {
                    PaintControl pc = _paintControl as PaintControl;
                    Point mpoi = GifferC.MousePositionOnLayer(SelectedFrameIndex, SelectedLayerIndex, mainPictureBox.MousePositionOnImage);
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.DrawLine)
                    {
                        _updateTimer.Start();
                        using Graphics g = Graphics.FromImage(((BitmapGFL)SelectedLayer).OriginalBitmap);
                        Painter.DrawLine(g, mpoi, mpoi, pc.PaintColorARGB, pc.Thickness);
                    }
                    else if (pc.SelectedPaintTool == PaintControl.PaintTool.ReplaceColor)
                    {
                        GifferC.ReplaceColor(SelectedFrameIndex, SelectedLayerIndex, mpoi, pc.PaintColorARGB, pc.Tolerance);
                    }
                    else if (pc.SelectedPaintTool == PaintControl.PaintTool.FillColor)
                    {
                        GifferC.FloodFill(SelectedFrameIndex, SelectedLayerIndex, mpoi, pc.PaintColorARGB, pc.Tolerance);
                    }
                }
                UpdateMainPictureBox();
                return;
            }
            _updateTimer.Start();
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            Point dd = mainPictureBox.ScaledDragDifference;
            OVector currentLCtM = LayerCenterToMouse();
            Point mpoi = GifferC.MousePositionOnLayer(SelectedFrameIndex, SelectedLayerIndex, mainPictureBox.MousePositionOnImage);
            if (controlsPanel.ToolPaintSelectedFlag)
            {
                if (SelectedLayer is BitmapGFL)
                {
                    PaintControl pc = layerParamsPanel.Controls[0] as PaintControl;
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.DrawLine)
                    {
                        if (IsLMBDown)
                        {
                            using Graphics g = Graphics.FromImage(((BitmapGFL)SelectedLayer).OriginalBitmap);
                            Painter.DrawLine(g, _prevMousePos, mpoi, pc.PaintColorARGB, pc.Thickness);
                        }
                    } else if(pc.SelectedPaintTool == PaintControl.PaintTool.Lasso)
                    {
                        if (IsLMBDown)
                        {
                            using Graphics g = Graphics.FromImage(MainImage);
                            Painter.DrawLinesFromPoints(g, mainPictureBox.MouseTrace, [Color.Red, Color.Cyan], pc.Thickness);
                            mainPictureBox.Invalidate();
                        }
                    }
                }
                _prevMousePos = mpoi;
                return;
            }
            if (IsRMBDown || IsLMBDown || IsMMBDown)
            {
                GFL gfl = SelectedLayer;
                if (IsLMBDown && !IsRMBDown)
                {
                    //MOVE
                    if (IsCtrlDown)
                    {
                        //MOVE WHOLE GIF (ALL LAYERS, ALL FRAMES)
                        MainGiffer.MoveFromOBR(dd.X, dd.Y);
                    }
                    else
                    {
                        //MOVE JUST LAYER
                        gfl.MoveFromOBR(dd.X, dd.Y);
                        if (controlsPanel.PositionSnap) gfl.Position = SnappedPosition(gfl.Position, 10);
                    }
                }
                else if (!IsLMBDown && IsRMBDown)
                {
                    //ROTATE
                    double angle = currentLCtM.Rotation;
                    float newRotation = _originalLayerRotation + (float)angle - (float)_originalLCtM.Rotation;
                    if (controlsPanel.RotationSnap)
                    {
                        gfl.Rotation = SnappedRotation(newRotation, 10);
                    }
                    else
                    {
                        gfl.Rotation = newRotation;
                    }
                }
                else if (IsMMBDown)
                {
                    //RESIZE
                    OVector sdv = new OVector(mainPictureBox.ScaledDragDifference);
                    int xSizeDif = (int)sdv.X;
                    int ySizeDif = (int)sdv.Y;
                    if (IsCtrlDown)
                    {
                        //RESIZE GIF FREE (ALL FRAMES)
                        MainGiffer.Resize(xSizeDif, ySizeDif);
                    }
                    else
                    {
                        //RESIZE LAYER
                        if (!IsShiftDown)
                        {
                            //RESIZE LAYER KEEP RATIO
                            int sizeDif = (int)(currentLCtM.Magnitude - _originalLCtM.Magnitude);
                            gfl.Resize(sizeDif);
                        }
                        else
                        {
                            //RESIZE LAYER FREE
                            gfl.Resize(xSizeDif, ySizeDif);
                        }
                    }
                }
            }
            _prevMousePos = mpoi;
        }

        private OVector LayerCenterToMouse()
        {
            OVector center = SelectedLayer.Center();
            Point ptm = mainPictureBox.PointToMouse(new Point(center.Xint, center.Yint));
            return new OVector(ptm);
        }
        private static float SnappedRotation(float rotation, float snapAngle)
        {
            if (rotation < 0) rotation += 360;
            float roundedRotation = (float)(Math.Round(rotation / 90f, 0) * 90);
            float mod90 = rotation % 90;
            if (mod90 > 90 - snapAngle || mod90 < snapAngle)
            {
                return roundedRotation;
            }
            else
            {
                return rotation;
            }
        }

        private static OVector SnappedPosition(OVector position, double snapDistance)
        {
            if (position.Magnitude < snapDistance)
            {
                return new OVector(0, 0);
            }
            else
            {
                return position;
            }
        }
        #endregion
        #region custom controls event handling
        private void mainLayersPanel_LayerOrderChanged(object sender, LayersPanel.LayerOrderEventArgs e)
        {
            GifferC.MoveLayer(SelectedFrameIndex, e.OriginalIndex, e.TargetIndex);
            CompleteUIUpdate();
        }
        private void mainLayersPanel_LayerDeleteButtonClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this layer?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            int layerIDToDelete = ((LayerHolder)sender).HeldLayer.LayerID;
            GifferC.DeleteLayerByID(layerIDToDelete);
            CompleteUIUpdate();
        }
        private void mainLayersPanel_LayerVisibilityChanged(object sender, LayersPanel.SelectedIndexEventArgs e)
        {
            UpdateMainPictureBox();
            GifferC.SaveLayerStateForApply(SelectedFrameIndex, e.Index);
            GifferC.SetSavedLayerVisibility(!MainGiffer.Frames[SelectedFrameIndex].Layers[e.Index].Visible);
            ApplyLayerParamsToSubsequentLayers(e.Index);
        }
        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            TimelineSlider ts = sender as TimelineSlider;
            if (!ts.PlayTimerRunning && !ts.MouseButtonIsDown)
            {
                CompleteUIUpdate();
            }
            else
            {
                MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
                UpdateMainPictureBox();
            }
        }
        public void CompleteUIUpdate(bool keepSelectedFrameAndLayer = true)
        {
            MainTimelineSlider.Maximum = MainGiffer.FrameCount - 1;
            MainTimelineSlider.SelectedFrameChanged -= MainTimelineSlider_SelectedFrameChanged;
            MainTimelineSlider.SelectedFrameIndex = Math.Clamp(SelectedFrameIndex, 0, MainTimelineSlider.Maximum);
            MainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;
            int sfi, sli, slid;
            if (keepSelectedFrameAndLayer)
            {
                sfi = SelectedFrameIndex;
                sli = SelectedLayerIndex;
                slid = GifferC.PreviousLayerID();
            }
            else
            {
                sfi = 0;
                sli = 0;
                slid = 0;
            }
            mainLayersPanel.DisplayLayers(MainGiffer.Frames[sfi]);
            if (!keepSelectedFrameAndLayer) MainTimelineSlider.ClearMarks();
            mainLayersPanel.TrySelectLayerByID(slid);
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            UpdateLayerParamsUI();
            GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
            UpdateMainPictureBox();
            hsbPanel.Saturation = SelectedLayer.Saturation;
            hsbPanel.Brightness = SelectedLayer.Brightness;
        }
        private void MainTimelineSlider_FrameDelayChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            TimelineSlider ts = sender as TimelineSlider;
            SelectedFrame.FrameDelay = ts.FrameDelay;
            if (controlsPanel.SelectedApplyParamsMode == ApplyParamsMode.applyNone) return;
            for (int i = MainTimelineSlider.SelectedFrameIndex + 1; i < MainGiffer.FrameCount; i++)
            {
                MainGiffer.Frames[i].FrameDelay = SelectedFrame.FrameDelay;
            }
        }
        private void ControlsPanel_ShouldStartDragDrop(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            ControlsPanel cp = sender as ControlsPanel;
            if (cp.IsLMBDown)
            {
                cp.DraggingFileForExport = true;
                GifferIO.FrameExportDragDrop(this);
                cp.DraggingFileForExport = false;
            } else if (cp.IsRMBDown)
            {
                cp.DraggingFileForExport = true;
                GifferIO.LayerExportDragDrop(this);
                cp.DraggingFileForExport = false;
            }
        }
        private void ControlsPanel_SaveButtonClicked(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            SaveFileDialog sfd = saveFileDialog;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GifferIO.SaveGif(MainGiffer, controlsPanel, sfd.FileName, controlsPanel.GifQuality, controlsPanel.CreateFrames, controlsPanel.InterpolationMode);
            }
        }
        #endregion
    }
}
