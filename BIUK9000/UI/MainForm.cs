using System;
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
        private LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public ControlsPanel MainControlsPanel { get => controlsPanel; }
        public GifFrame SelectedFrame { get => MainGiffer.Frames[MainTimelineSlider.SelectedFrameIndex]; }
        public int SelectedFrameIndex { get => MainTimelineSlider.SelectedFrameIndex; }
        public int SelectedLayerIndex { get => MainLayersPanel.SelectedLayerIndex; }
        public Bitmap SelectedFrameAsBitmap { get => MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp); }
        private GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
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

            MainLayersPanel.LayerOrderChanged += MainLayersPanel_LayerOrderChanged;
            MainLayersPanel.SelectedLayerChanged += (sender, args) =>
            {
                UpdateLayerParamsUI();
                GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
            };
            mainLayersPanel.LayerVisibilityChanged += MainLayersPanel_LayerVisibilityChanged;
            mainLayersPanel.LayerDeleteButtonClicked += MainLayersPanel_LayerDeleteButtonClicked;

            controlsPanel.MustRedraw += (sender, args) => { if (MainGiffer != null) UpdateMainPictureBox(); };
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;
            controlsPanel.InterpolationModeChanged += (sender, args) => mainPictureBox.InterpolationMode = ((ControlsPanel)sender).InterpolationMode;
            controlsPanel.ToolPaintSelected += (sender, args) => SetPaintMode(true);
            controlsPanel.ToolMoveSelected += (sender, args) => SetPaintMode(false);

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;

            lerpButton.Click += LerpButton_Click;

            markButton.Click += (sender, args) => MainTimelineSlider.AddMark(SelectedFrameIndex);
            unmarkButton.Click += (sender, args) => MainTimelineSlider.RemoveMark(SelectedFrameIndex);
            deleteFramesButton.Click += DeleteFramesButton_Click;

            _paintControl = new PaintControl();
        }
        private void SetPaintMode(bool setValue)
        {
            MainTimelineSlider.Enabled = !setValue;
            MainLayersPanel.Enabled = !setValue;
            controlsPanel.SetPaintMode(setValue);
            if (GifferC == null) return;
            if (setValue)
            {
                if (layerParamsPanel.Controls.Count > 0)
                {
                    _lpcBackup = layerParamsPanel.Controls[0];
                    layerParamsPanel.Controls.Clear();
                    layerParamsPanel.Controls.Add(_paintControl);
                }
            }
            else
            {
                layerParamsPanel.Controls.Clear();
                layerParamsPanel.Controls.Add(_lpcBackup);
            }
        }

        private void DeleteFramesButton_Click(object sender, EventArgs e)
        {
            if(MainGiffer == null || MainGiffer.FrameCount < 2)return;
            MainGiffer.RemoveFrames(MainTimelineSlider.Marks);
            CompleteUIUpdate();
        }

        private void LerpButton_Click(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            GifferC.LerpExecute(MainTimelineSlider.Marks, SelectedLayerIndex);
            MainTimelineSlider.ClearMarks();
        }
        public void ApplyLayerParamsToSubsequentLayers(int index = -1)
        {
            int i = SelectedLayerIndex;
            if(index >= 0)
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
                MainLayersPanel.SelectNewestLayer();
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
                lpc.LoadParams(SelectedLayer);
                lpc.ParamsChanged += (sender, args) =>
                {
                    GifferC.SaveLayerStateForApply(SelectedFrameIndex, MainLayersPanel.SelectedLayerIndex);
                    lpc.SaveParams(SelectedLayer);
                    UpdateMainPictureBox();
                    ApplyLayerParamsToSubsequentLayers();
                };
                Control lpcc = lpc as Control;
                lpcc.Dock = DockStyle.Fill;
                layerParamsPanel.Controls.Add(lpcc);
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
            if (MainGiffer == null || ActiveControl is TextBox || ActiveControl is IGFLParamControl) return base.ProcessKeyPreview(ref m);
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    if (MainTimelineSlider.SelectedFrameIndex < MainTimelineSlider.Maximum) MainTimelineSlider.SelectedFrameIndex += 1;
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    if (MainTimelineSlider.SelectedFrameIndex > 0) MainTimelineSlider.SelectedFrameIndex -= 1;
                    return true;
                }
                else if (keyData == Keys.T || keyData == Keys.B)
                {
                    GifferC.AddNewLayer(keyData);
                    MainLayersPanel.DisplayLayers(SelectedFrame);
                    UpdateMainPictureBox();
                    MainLayersPanel.SelectNewestLayer();
                    GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
                    UpdateLayerParamsUI();
                    return true;
                }
                else if(keyData == Keys.F)
                {
                    MainGiffer.Mirror();
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
            UpdateTimer.Start();
            if (controlsPanel.ToolPaintSelectedFlag)
            {
                if (SelectedLayer is BitmapGFL)
                {
                    PaintControl pc = layerParamsPanel.Controls[0] as PaintControl;
                    Point mpoi = GifferC.MousePositionOnLayer(SelectedFrameIndex, SelectedLayerIndex, mainPictureBox.MousePositionOnImage);
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.DrawLine)
                    {
                        Bitmap lbmp = (SelectedLayer as BitmapGFL).OriginalBitmap;
                        if (IsLMBDown)
                        {
                            Painter.DrawLine(lbmp, mpoi, mpoi, pc.PaintColor, pc.Transparency, pc.Thickness);
                        }
                    }
                    else if(pc.SelectedPaintTool == PaintControl.PaintTool.DeleteColor)
                    {
                        GifferC.DeleteColor(SelectedFrameIndex, SelectedLayerIndex, mpoi, pc.Transparency);
                    }
                    (SelectedLayer as BitmapGFL).UpdateAfterPaint();
                    UpdateMainPictureBox();
                }
            }
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            Point dd = mainPictureBox.ScaledDragDifference;
            OVector currentLCtM = LayerCenterToMouse();
            Point mpoi = GifferC.MousePositionOnLayer(SelectedFrameIndex, SelectedLayerIndex, mainPictureBox.MousePositionOnImage);
            if (controlsPanel.ToolPaintSelectedFlag)
            {
                if(SelectedLayer is BitmapGFL)
                {
                    PaintControl pc = layerParamsPanel.Controls[0] as PaintControl;
                    if(pc.SelectedPaintTool == PaintControl.PaintTool.DrawLine)
                    {
                        Bitmap lbmp = (SelectedLayer as BitmapGFL).OriginalBitmap;
                        if (IsLMBDown) Painter.DrawLine(lbmp, mpoi, _prevMousePos, pc.PaintColor, pc.Transparency, pc.Thickness);
                        (SelectedLayer as BitmapGFL).UpdateAfterPaint();
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
        private void MainLayersPanel_LayerOrderChanged(object sender, LayersPanel.LayerOrderEventArgs e)
        {
            GifferC.MoveLayer(SelectedFrameIndex, e.OriginalIndex, e.TargetIndex);
            CompleteUIUpdate();
        }
        private void MainLayersPanel_LayerDeleteButtonClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this layer?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            int layerIDToDelete = (sender as LayerHolder).HeldLayer.LayerID;
            GifferC.DeleteLayerByID(layerIDToDelete);
            CompleteUIUpdate();
        }
        private void MainLayersPanel_LayerVisibilityChanged(object sender, LayersPanel.SelectedIndexEventArgs e)
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
                MainTimelineSlider.FrameDelayChanged -= MainTimelineSlider_FrameDelayChanged;
                MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
                MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;
                UpdateMainPictureBox();
            }
        }
        public void CompleteUIUpdate(bool keepSelectedFrameAndLayer = true)
        {
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
            MainLayersPanel.SelectedLayerIndex = sli;
            MainLayersPanel.DisplayLayers(MainGiffer.Frames[sfi]);
            MainTimelineSlider.ClearMarks();
            MainTimelineSlider.Maximum = MainGiffer.FrameCount - 1;
            MainTimelineSlider.SelectedFrameChanged -= MainTimelineSlider_SelectedFrameChanged;
            MainTimelineSlider.SelectedFrameIndex = Math.Clamp(sfi, 0, MainTimelineSlider.Maximum);
            MainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;
            Debug.Print("prevlayerid: " + slid.ToString());
            MainLayersPanel.TrySelectLayerByID(slid);
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            UpdateLayerParamsUI();
            GifferC.SaveLayerForLPC(SelectedFrameIndex, SelectedLayerIndex);
            UpdateMainPictureBox();
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
            }
        }
        private void ControlsPanel_SaveButtonClicked(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            SaveFileDialog sfd = saveFileDialog;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GifferIO.SaveGif(MainGiffer, controlsPanel, sfd.FileName);
            }
        }
        #endregion
    }
}
