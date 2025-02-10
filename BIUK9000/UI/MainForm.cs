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
using BIUK9000.UI.LayerParamControls;
using System.Drawing.Drawing2D;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;
using BIUK9000.UI.ExtendedControls;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public ControlsPanel MainControlsPanel { get => controlsPanel; }
        public GifFrame SelectedFrame { get => MainGiffer.Frames[MainTimelineSlider.SelectedFrameIndex]; }
        public int SFI { get => MainTimelineSlider.SelectedFrameIndex; }
        public int SLI { get => mainLayersPanel.SelectedLayerIndex; }
        public Bitmap SelectedFrameAsBitmap { get => MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp, controlsPanel.InterpolationMode); }
        private GFL SelectedLayer { get => GifferC.GetLayer(SFI, SLI); }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        public bool IsShiftDown { get; set; }
        public bool IsCtrlDown { get; set; }
        private float _originalLayerRotation;
        private OVector _previousLCtM;
        private float _rotationChange;
        private Control _lpcBackup;
        private PaintControl _paintControl;
        private RecordControl _recordControl;
        public bool IsLMBDown { get => mainPictureBox.IsLMBDown; }
        public bool IsRMBDown { get => mainPictureBox.IsRMBDown; }
        public bool IsMMBDown { get => mainPictureBox.IsMMBDown; }
        private Timer _updateTimer;
        private Point _prevMousePos;
        public GifferController GifferC { get; private set; }
        //private ScreenCapture _sc;
        private ScreenStateLogger _ssl;
        private List<byte[]> byteFrames;
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
            controlsPanel.ToolRecordSelected += ControlsPanel_ToolRecordSelected;

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;

            lerpButton.Click += LerpButton_Click;

            deleteFramesButton.Click += DeleteFramesButton_Click;
            dupeFrameButton.Click += DupeFrameButton_Click;

            _paintControl = new PaintControl();
            _recordControl = new RecordControl();
            lerpModeCBB.SelectedIndex = 0;
            mainPictureBox.InterpolationMode = controlsPanel.InterpolationMode;

            hsbPanel.HueSatChanged += HsbPanel_HueSatChanged;
            hsbPanel.ChangeStarted += HsbPanel_ChangeStarted;
            hsbPanel.ChangeEnded += HsbPanel_ChangeEnded;
            hsbPanel.ShouldUpdate += (sender, args) =>
            {
                if (GifferC == null) return;
                HsbPanel_HueSatChanged(sender, args);
                UpdateMainPictureBox();
            };
            //_sc = new ScreenCapture();
            byteFrames = new List<byte[]>();
            _ssl = new ScreenStateLogger();

            _recordControl.Start += _recordControl_Start;
            _recordControl.Stop += _recordControl_Stop;

            TransparencyKey = Color.LimeGreen;
            this.Move += MainForm_Move;
        }
        private Rectangle GetTotalScreenBounds()
        {
            Rectangle totalBounds = Screen.AllScreens[0].Bounds;
            foreach (Screen screen in Screen.AllScreens)
            {
                totalBounds = Rectangle.Union(totalBounds, screen.Bounds);
            }
            return totalBounds;
        }
        private int CurrentScreenIndex()
        {
            Screen currentScreen = Screen.FromControl(this);
            Screen[] allScreens = Screen.AllScreens;
            for(int i = 0; i < allScreens.Length; i++)
            {
                if(allScreens[i].Equals(currentScreen))
                {
                    return i;
                }
            }
            return -1;
        }
        private void MainForm_Move(object sender, EventArgs e)
        {
            Point p = mainPictureBox.PointToScreen(Point.Empty);
            if (CanRecord())
            {
                _ssl.X = p.X;
                _ssl.Y = p.Y;
            }
        }

        private void SetRecordMode(bool val)
        {
            if (val)
            {
                TransparencyKey = Color.Red;
                mainPictureBox.BackColor = Color.Red;
                if(MainImage != null){
                    using Graphics g = Graphics.FromImage(MainImage);
                    g.Clear(Color.Transparent);
                }
            } else
            {
                mainPictureBox.BackColor = default;
                TransparencyKey = default;
            }
        }

        private void _recordControl_Stop(object sender, EventArgs e)
        {
            _ssl.Stop();
            if (_ssl.Frames.Count == 0) return;
            SetNewGiffer(new Giffer(_ssl.Frames, _ssl.FPS));
            _ssl.ClearFrames();
        }
        private bool CanRecord()
        {
            //Point p = mainPictureBox.PointToScreen(Point.Empty);
            //bool widthCheck = (p.X + mainPictureBox.Width) >= Screen.FromControl(this).Bounds.Right;
            //bool heightCheck = (p.Y + mainPictureBox.Height) >= Screen.FromControl(this).Bounds.Bottom;
            //bool posCheck = p.X < 0 || p.Y < 0;
            //bool minwhCheck = mainPictureBox.Width < 5 || mainPictureBox.Height < 5;
            //return !(widthCheck || heightCheck || posCheck ||  minwhCheck);
            bool minwhCheck = mainPictureBox.Width < 5 || mainPictureBox.Height < 5;
            Rectangle totalScreenBounds = GetTotalScreenBounds();
            Point screenLocation = mainPictureBox.PointToScreen(Point.Empty);
            Rectangle pictureBoxBounds = new Rectangle(screenLocation, mainPictureBox.Size);

            return totalScreenBounds.Contains(pictureBoxBounds) && !minwhCheck;
        }
        private void _recordControl_Start(object sender, EventArgs e)
        {
            Point p = mainPictureBox.PointToScreen(Point.Empty);
            bool widthCheck = (p.X + mainPictureBox.Width) >= Screen.FromControl(this).Bounds.Right;
            bool heightCheck = (p.Y + mainPictureBox.Height) >= Screen.FromControl(this).Bounds.Bottom;
            if (p.X < 0 || p.Y < 0 || widthCheck || heightCheck)
            {
                MessageBox.Show("The entire recording area must be on screen!");
                _recordControl.RecMode(false);
                return;
            }
            if (mainPictureBox.Width < 5 || mainPictureBox.Height < 5)
            {
                _recordControl.RecMode(false);
                MessageBox.Show("Recording area must be larger!");
                return;
            }
            Rectangle screenBounds = GetTotalScreenBounds();
            Point pp = new Point(p.X - screenBounds.Left, p.Y - screenBounds.Top);
            _ssl.X = pp.X;
            _ssl.Y = pp.Y;
            //MessageBox.Show("X: " + p.X.ToString() + "  Y: " + p.Y.ToString());
            _ssl.Width = mainPictureBox.Width;
            _ssl.Height = mainPictureBox.Height;
            _ssl.FPS = _recordControl.FPS;
            _ssl.Start(CurrentScreenIndex());
        }

        private void ControlsPanel_ToolRecordSelected(object sender, EventArgs e)
        {
            layerParamsPanel.Controls.Clear();
            layerParamsPanel.Controls.Add(_recordControl);
            ControlsEnable(false);
            SetRecordMode(true);
        }

        private void HsbPanel_ChangeEnded(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            _updateTimer.Stop();
            GifferC.ApplyLayerParams(SFI, SLI, controlsPanel.SelectedApplyParamsMode);
        }

        private void HsbPanel_ChangeStarted(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            GifferC.SaveLayerStateForApply(SFI, SLI);
            _updateTimer.Start();
        }

        private void MainLayersPanel_SelectedLayerChanged(object sender, EventArgs e)
        {
            UpdateLayerParamsUI();
            GifferC.SaveLayerForLPC(SFI, SLI);
        }

        private void HsbPanel_HueSatChanged(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            SelectedLayer.Saturation = hsbPanel.Saturation;
            SelectedLayer.Brightness = hsbPanel.Brightness;
            SelectedLayer.Transparency = hsbPanel.Transparency;
            SelectedLayer.Hue = hsbPanel.Hue;
        }

        private void DupeFrameButton_Click(object sender, EventArgs e)
        {
            GifferC.DupeFrame(SFI, (int)frameDupeCountNUD.Value);
            CompleteUIUpdate();
        }
        
        private void SetPaintMode(bool setValue)
        {
            SetRecordMode(false);
            if (layerParamsPanel.Controls.Count > 0 && layerParamsPanel.Controls[0] is RecordControl)
            {
                layerParamsPanel.Controls.Clear();
            }
            if (GifferC == null)
            {
                controlsPanel.ToolMoveSelectedFlag = true;
                return;
            }
            if(setValue && SelectedLayer is not BitmapGFL)
            {
                MessageBox.Show("Only image layers can be painted on, select an image layer.");
                controlsPanel.ToolMoveSelectedFlag = true;
                ControlsEnable(true);
                return;
            }
            ControlsEnable(!setValue);
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
        private void ControlsEnable(bool val)
        {
            MainTimelineSlider.Enabled = val;
            mainLayersPanel.Enabled = val;
            markLerpPanel.Enabled = val;
            controlsPanel.SetPaintMode(!val);
            AllowDrop = val;
        }
        private void SetRecordMode()
        {

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
                MainGiffer.RemoveFrames(new List<int>() { SFI });
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
                GifferC.LerpExecute(MainTimelineSlider.Marks, SLI, mainPictureBox.MouseTrace);
            } else if(lerpMode == "line")
            {
                GifferC.LerpExecute(MainTimelineSlider.Marks, SLI);
            }
            MainTimelineSlider.ClearMarks();
            mainPictureBox.MouseTrace.Clear();
        }
        public void ApplyLayerParamsToSubsequentLayers(int index = -1)
        {
            int i = SLI;
            if (index >= 0)
            {
                i = index;
            }
            GifferC.ApplyLayerParams(SFI, i, controlsPanel.SelectedApplyParamsMode);
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
            if(layerParamsPanel.Controls.Count > 0)
            {
                //layerParamsPanel.Controls[0].Dispose();
                if (layerParamsPanel.Controls[0] is IGFLParamControl)
                {
                    layerParamsPanel.Controls[0].Dispose();
                } else
                {
                    layerParamsPanel.Controls.Clear();
                }
            }
            controlsPanel.ToolMoveSelectedFlag = true;
            SetPaintMode(false);
            CompleteUIUpdate(false);
            GifferC.SaveLayerStateForApply(0, 0);
            GifferC.SaveLayerForLPC(0, 0);
        }

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp, controlsPanel.InterpolationMode);
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
            if (SelectedLayer == null) return;
            bool cond = GifferC.ShouldSwitchLPC(SFI, SLI);
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
                        GifferC.SaveLayerStateForApply(SFI, mainLayersPanel.SelectedLayerIndex);
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
            hsbPanel.Saturation = SelectedLayer.Saturation;
            hsbPanel.Brightness = SelectedLayer.Brightness;
            hsbPanel.Transparency = SelectedLayer.Transparency;
        }

        #region key event handling
        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (MainGiffer == null || ActiveControl is TextBox || ActiveControl is IGFLParamControl || controlsPanel.ToolPaintSelectedFlag || controlsPanel.ToolRecordSelectedFlag) return base.ProcessKeyPreview(ref m);
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
                    GifferC.SaveLayerForLPC(SFI, SLI);
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
                    PaintControl pc = _paintControl;
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.Lasso)
                    {
                        Point[] translatedPoints = mainPictureBox.MouseTrace.Select(point => point = GifferC.MousePositionOnLayer(SFI, SLI, point)).ToArray();
                        GifferC.Lasso(SFI, SLI, translatedPoints, pc.LassoIncludeComplement, pc.LassoConstrainBounds, pc.LassoAnimateCutout, pc.LassoAnimateComplement);
                        mainLayersPanel.DisplayLayers(SelectedFrame);
                    }
                }
            }
            if (!IsLMBDown && !IsMMBDown & !IsRMBDown)
            {
                UpdateTimer.Stop();
                if(!IsCtrlDown)ApplyLayerParamsToSubsequentLayers();
            }
            UpdateMainPictureBox();
        }

        private void MainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mainPictureBox.Focus();
            if (MainGiffer == null) return;
            GFL cgfl = SelectedLayer;
            MainGiffer.Save();
            GifferC.SaveLayerStateForApply(SFI, SLI);
            _originalLayerRotation = cgfl.Rotation;
            _previousLCtM = LayerCenterToMouse();
            _rotationChange = 0;
            if (controlsPanel.ToolPaintSelectedFlag && SelectedLayer is BitmapGFL)
            {
                if (IsLMBDown)
                {
                    PaintControl pc = _paintControl;
                    Point mpoi = GifferC.MousePositionOnLayer(SFI, SLI, mainPictureBox.MousePositionOnImage);
                    if (pc.SelectedPaintTool == PaintControl.PaintTool.DrawLine)
                    {
                        _updateTimer.Start();
                        using Graphics g = Graphics.FromImage(((BitmapGFL)SelectedLayer).OriginalBitmap);
                        Painter.DrawLine(g, mpoi, mpoi, pc.PaintColorARGB, pc.Thickness);
                    }
                    else if (pc.SelectedPaintTool == PaintControl.PaintTool.ReplaceColor)
                    {
                        GifferC.ReplaceColor(SFI, SLI, mpoi, pc.PaintColorARGB, pc.Tolerance);
                    }
                    else if (pc.SelectedPaintTool == PaintControl.PaintTool.FillColor)
                    {
                        GifferC.FloodFill(SFI, SLI, mpoi, pc.PaintColorARGB, pc.Tolerance);
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
            Point mpoi = GifferC.MousePositionOnLayer(SFI, SLI, mainPictureBox.MousePositionOnImage);
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
                            Painter.DrawLinesFromPoints(g, mainPictureBox.MouseTrace, [Color.Red, Color.Cyan], 3);
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
                    float angleDif = (float)currentLCtM.Rotation - (float)_previousLCtM.Rotation;
                    if (angleDif > 180) angleDif -= 360;
                    if (angleDif < -180) angleDif += 360;
                    _rotationChange += angleDif;
                    float newRotation = _originalLayerRotation + _rotationChange;
                    if (controlsPanel.RotationSnap)
                    {
                        gfl.Rotation = SnappedRotation(newRotation, 5);
                    } else
                    {
                        gfl.Rotation = newRotation;
                    }
                    _previousLCtM = currentLCtM;
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
                            int sizeDif = (int)(currentLCtM.Magnitude - _previousLCtM.Magnitude);
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
            float roundedRotation = (float)(Math.Round(rotation / 90, 0) * 90);
            float mod90 = Math.Abs(rotation % 90);
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
            GifferC.MoveLayer(SFI, e.OriginalIndex, e.TargetIndex);
            CompleteUIUpdate();
        }
        private void mainLayersPanel_LayerDeleteButtonClicked(object sender, LayersPanel.IndexEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this layer?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            GifferC.DeleteLayerByIndex(SFI, e.Index);
            CompleteUIUpdate();
        }
        private void mainLayersPanel_LayerVisibilityChanged(object sender, LayersPanel.IndexEventArgs e)
        {
            GifferC.GetLayer(SFI, e.Index).Visible = !GifferC.GetLayer(SFI, e.Index).Visible;
            UpdateMainPictureBox();
            GifferC.SaveLayerStateForApply(SFI, e.Index);
            GifferC.SetSavedLayerVisibility(!GifferC.GetLayer(SFI, e.Index).Visible);
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
            MainTimelineSlider.SelectedFrameIndex = Math.Clamp(SFI, 0, MainTimelineSlider.Maximum);
            MainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;
            int sfi, sli, slid;
            if (keepSelectedFrameAndLayer)
            {
                sfi = SFI;
                sli = SLI;
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
            mainLayersPanel.SelectHolder(GifferC.TryGetLayerIndexById(SFI, slid));
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            UpdateLayerParamsUI();
            GifferC.SaveLayerForLPC(SFI, SLI);
            UpdateMainPictureBox();
            if(SelectedLayer != null)
            {
                hsbPanel.Saturation = SelectedLayer.Saturation;
                hsbPanel.Brightness = SelectedLayer.Brightness;
                hsbPanel.Transparency = SelectedLayer.Transparency;
            }
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
                GifferIO.SaveGif(MainGiffer, sfd.FileName, controlsPanel.InterpolationMode);
            }
        }
        #endregion
    }
}
