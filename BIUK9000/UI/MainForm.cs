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
using AnimatedGif;
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
        public Bitmap SelectedFrameAsBitmap { get => MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp); }
        private GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
        private GFL PreviousLayerState { get; set; }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        private GFL PreviousSelectedLayer { get; set; }
        private FrameAndLayer LerpStart { get; set; }
        public bool IsShiftDown { get; set; }
        public bool IsCtrlDown { get; set; }
        private float originalLayerRotation;
        private OVector originalLCtM;
        public bool IsLMBDown { get => mainPictureBox.IsLMBDown; }
        public bool IsRMBDown { get => mainPictureBox.IsRMBDown; }
        public bool IsMMBDown { get => mainPictureBox.IsMMBDown; }
        private Timer _updateTimer;

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
                UpdateLayerParamsUI(LayerTypeChanged());
                PreviousSelectedLayer = SelectedLayer;
            };
            mainLayersPanel.LayerVisibilityChanged += MainLayersPanel_LayerVisibilityChanged;
            mainLayersPanel.LayerDeleteButtonClicked += MainLayersPanel_LayerDeleteButtonClicked;

            controlsPanel.MustRedraw += (sender, args) => { if (MainGiffer != null) UpdateMainPictureBox(); };
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;
            controlsPanel.InterpolationModeChanged += (sender, args) => mainPictureBox.InterpolationMode = ((ControlsPanel)sender).InterpolationMode;

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;

            lerpStartButton.Click += LerpStartButton_Click;
            lerpExecuteButton.Click += LerpExecuteButton_Click;

            markButton.Click += (sender, args) => MainTimelineSlider.AddMark(SelectedFrameIndex);
            unmarkButton.Click += (sender, args) => MainTimelineSlider.RemoveMark(SelectedFrameIndex);
            deleteFramesButton.Click += DeleteFramesButton_Click;
        }

        private void DeleteFramesButton_Click(object sender, EventArgs e)
        {
            if(MainGiffer == null || MainGiffer.FrameCount < 2)return;
            MainGiffer.RemoveFrames(MainTimelineSlider.Marks);
            CompleteUIUpdate();
        }

        private void LerpExecuteButton_Click(object sender, EventArgs e)
        {
            if(LerpStart.Layer == null || LerpStart.Frame == null)
            {
                MessageBox.Show("You must start the lerp first");
                return;
            }
            GFL startLayer = LerpStart.Layer;
            GifFrame startFrame = LerpStart.Frame;
            int startIndex = MainGiffer.Frames.IndexOf(startFrame);
            int endIndex = mainTimelineSlider.SelectedFrameIndex;
            int totalFrames = endIndex - startIndex;
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                double distance = 1 - (endIndex - i) / (double)totalFrames;
                GifFrame cgf = MainGiffer.Frames[i];
                GFL layerToLerp = cgf.Layers.Find(layer => layer.LayerID == startLayer.LayerID);
                layerToLerp?.Lerp(startLayer, SelectedLayer, distance);
            }
        }

        private void LerpStartButton_Click(object sender, EventArgs e)
        {
            if (MainGiffer == null)
            {
                MessageBox.Show("Import a gif first");
                return;
            }
            LerpStart = new FrameAndLayer(SelectedLayer, SelectedFrame);
        }

        private void SavePreviousLayerState()
        {
            PreviousLayerState = SelectedLayer.Clone();
        }

        public void ApplyCurrentLayerParamsToSubsequentLayers()
        {
            ApplyParamsMode apm = controlsPanel.SelectedApplyParamsMode;
            if (apm == ApplyParamsMode.applyNone) return;
            int cli = mainLayersPanel.SelectedLayerIndex;
            int cgfi = MainGiffer.Frames.IndexOf(SelectedFrame);
            for (int i = cgfi + 1; i < MainGiffer.FrameCount; i++)
            {
                GifFrame gf = MainGiffer.Frames[i];
                if (cli >= 0 && cli < gf.Layers.Count)
                {
                    GFL layerToUpdate;
                    if (gf.Layers[cli].LayerID != SelectedLayer.LayerID)
                    {
                        layerToUpdate = gf.Layers.Find(gfl => gfl.LayerID == SelectedLayer.LayerID);
                    } else
                    {
                        layerToUpdate = gf.Layers[cli];
                    }
                    if(layerToUpdate != null)
                    {
                        if (apm == ApplyParamsMode.applyChanged)
                        {
                            layerToUpdate.CopyDifferingParams(PreviousLayerState, SelectedLayer);
                        }
                        else
                        {
                            layerToUpdate.CopyParameters(SelectedLayer);
                        }
                    }
                }
            }
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
            GifferIO.FileImport(filePaths, this);
            //mainLayersPanel.DisplayLayers(SelectedFrame);
            //mainLayersPanel.SelectNewestLayer();
            //UpdateMainPictureBox();
            CompleteUIUpdate();
        }

        public void SetNewGiffer(Giffer newGiffer)
        {
            MainGiffer?.Dispose();
            MainGiffer = newGiffer;
            CompleteUIUpdate();
            SavePreviousLayerState();
        }
        public void CompleteUIUpdate()
        {
            int currentSFI = SelectedFrameIndex;
            MainTimelineSlider.ClearMarks();
            MainLayersPanel.SelectedLayerIndex = 0;
            MainTimelineSlider.Maximum = MainGiffer.FrameCount - 1;
            MainTimelineSlider.SelectedFrameChanged -= MainTimelineSlider_SelectedFrameChanged;
            MainTimelineSlider.SelectedFrameIndex = Math.Clamp(currentSFI, 0, MainTimelineSlider.Maximum);
            MainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;
            MainLayersPanel.DisplayLayers(SelectedFrame);
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            UpdateLayerParamsUI(LayerTypeChanged());
            UpdateMainPictureBox();
        }

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp);

        }
        private bool LayerTypeChanged()
        {
            if (PreviousSelectedLayer == null || SelectedLayer == null) return true;
            return !(SelectedLayer.GetType().Name == PreviousSelectedLayer.GetType().Name);
        }

        private void UpdateLayerParamsUI(bool layerTypeChanged)
        {
            if (layerTypeChanged)
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
                    SavePreviousLayerState();
                    lpc.SaveParams(SelectedLayer);
                    UpdateMainPictureBox();
                    ApplyCurrentLayerParamsToSubsequentLayers();
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
        private struct FrameAndLayer
        {
            public GFL Layer { get; private set; }
            public GifFrame Frame { get; private set; }
            public FrameAndLayer(GFL layer, GifFrame frame)
            {
                Layer = layer.Clone();
                Frame = frame;
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
                    AddNewLayer(keyData);
                    return true;
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
        private void AddNewLayer(Keys keyData)
        {
            int nextId = MainGiffer.NextLayerID();
            GFL nextLayer = null;
            if (keyData == Keys.T)
            {
                nextLayer = new TextGFL(nextId);
            }
            else if (keyData == Keys.B)
            {
                nextLayer = new PlainGFL(nextId);
            }
            if (nextLayer == null) return;
            foreach (GifFrame gf in MainGiffer.Frames)
            {
                gf.AddLayer(nextLayer.Clone());
            }
            MainLayersPanel.DisplayLayers(SelectedFrame);
            UpdateMainPictureBox();
            MainLayersPanel.SelectNewestLayer();
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
            ApplyCurrentLayerParamsToSubsequentLayers();
            UpdateMainPictureBox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mainPictureBox.Focus();
            if (MainGiffer == null) return;
            GFL cgfl = SelectedLayer;
            MainGiffer.Save();
            SavePreviousLayerState();
            originalLayerRotation = cgfl.Rotation;
            originalLCtM = LayerCenterToMouse();
            UpdateTimer.Start();
        }
        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            Point dd = mainPictureBox.ScaledDragDifference;
            OVector currentLCtM = LayerCenterToMouse();
            //OVector currentLCtM = new OVector(mainPictureBox.PointToMouse(new Point(SelectedLayer.Center().Xint, SelectedLayer.Center().Yint)));
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
                    float newRotation = originalLayerRotation + (float)angle - (float)originalLCtM.Rotation;
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
                            int sizeDif = (int)(currentLCtM.Magnitude - originalLCtM.Magnitude);
                            gfl.Resize(sizeDif);
                        }
                        else
                        {
                            //RESIZE LAYER FREE
                            sdv.Rotate(gfl.Rotation);
                            gfl.Resize(xSizeDif, ySizeDif);
                        }
                    }
                }
            }
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
            double realDistance = position.Subtract(new OVector(0, 0)).Magnitude;
            if (realDistance < snapDistance)
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
            for (int i = MainTimelineSlider.SelectedFrameIndex; i < MainGiffer.FrameCount; i++)
            {
                GifFrame cf = MainGiffer.Frames[i];
                GFL gflToInsert = cf.Layers[e.OriginalIndex];
                cf.Layers.RemoveAt(e.OriginalIndex);
                cf.Layers.Insert(e.TargetIndex, gflToInsert);
            }
            UpdateMainPictureBox();
            MainLayersPanel.DisplayLayers(SelectedFrame);
        }
        private void MainLayersPanel_LayerDeleteButtonClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this layer?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            int layerIDToDelete = (sender as LayerHolder).HeldLayer.LayerID;
            foreach (GifFrame gf in MainGiffer.Frames)
            {
                GFL layerToDelete = gf.Layers.Find(layer => layer.LayerID == layerIDToDelete);
                if (layerToDelete != null) gf.Layers.Remove(layerToDelete);
            }
            CompleteUIUpdate();
        }
        private void MainLayersPanel_LayerVisibilityChanged(object sender, EventArgs e)
        {
            UpdateMainPictureBox();
            SavePreviousLayerState();
            PreviousLayerState.Visible = !SelectedLayer.Visible;
            ApplyCurrentLayerParamsToSubsequentLayers();
        }
        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            TimelineSlider ts = sender as TimelineSlider;
            if (!ts.PlayTimerRunning && !ts.MouseButtonIsDown)
            {
                MainLayersPanel.DisplayLayers(SelectedFrame);
                MainLayersPanel.TrySelectLayerByID(PreviousSelectedLayer.LayerID);
                UpdateLayerParamsUI(LayerTypeChanged());
            }
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            UpdateMainPictureBox();
            PreviousSelectedLayer = SelectedLayer;
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
                if (sfd.FileName == MainGiffer.OriginalImagePath)
                {
                    MessageBox.Show("Do not use the same file name for export as you did for import.");
                    return;
                }
                string tempPath;
                if (controlsPanel.UseDithering)
                {
                    tempPath = GifferIO.SaveGifToTempFileDithered(MainGiffer, controlsPanel.GifExportColors);
                }
                else
                {
                    tempPath = GifferIO.SaveGifToTempFile(MainGiffer);
                }
                if (controlsPanel.UseGifsicle)
                {
                    OBIMP.CompressGif(tempPath, tempPath, controlsPanel.GifExportColors, controlsPanel.GifExportLossy);
                }
                if (tempPath != null)
                {
                    File.Copy(tempPath, sfd.FileName, true);
                }
                else
                {
                    MessageBox.Show("Gif was not created");
                    return;
                }
                File.Delete(tempPath);
            }
        }
        #endregion
    }
}
