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
using System.Drawing.Text;
using BIUK9000.UI.LayerParamControls;
using System.Drawing.Drawing2D;
using BIUK9000.UI.CustomControls;
using BIUK9000.UI.ExtendedControls;
using BIUK9000.UI.InputHandling;
using BIUK9000.UI.TopControls.LayerParamControls;
using BIUK9000.ScreenRecording;
using BIUK9000.Helpers;
using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.GifferManipulation;
using BIUK9000.MyGraphics.Effects.OrderedDithering;
using BIUK9000.UI.Forms;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        public ControlsPanel MainControlsPanel { get => controlsPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public GifFrame SelectedFrame { get => GifferC.SelectedFrame; }
        public int SFI { get => GifferC.SFI; set => GifferC.SFI = value; }
        public int SLI { get => GifferC.SLI; set => GifferC.SLI = value; }
        public bool PositionSnap { get => controlsPanel.PositionSnap; }
        public bool RotationSnap { get => controlsPanel.RotationSnap; }
        public float RotationChange { get => _rotationChange; set => _rotationChange = value; }
        public float OriginalLayerRotation { get => _originalLayerRotation; set => _originalLayerRotation = value; }
        public Bitmap SelectedFrameAsBitmap { get => MainGiffer.FrameAsBitmap(SelectedFrame, false, controlsPanel.InterpolationMode); }
        public GFL SelectedLayer { get => GifferC == null ? null : GifferC.GetLayer(SFI, SLI); }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        private float _originalLayerRotation;
        private OVector _previousLRCtM;
        private float _rotationChange;
        private PaintControl _paintControl;
        private RecordControl _recordControl;
        private Timer _updateTimer;
        private Point _prevMousePos;
        public OVector ClickedLRCtM { get; set; }
        public GifferController GifferC { get; private set; }
        private ScreenStateLogger _ssl;
        private int _selectedLayerID;
        private List<string> _tempFilePaths = new();
        public string FormTitle { get => Text; set => Text = value; }
        public Control UpperControl
        {
            get
            {
                if (topPanel.Controls.Count > 0)
                {
                    return topPanel.Controls[0];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                topPanel.Controls.Clear();
                if (value != null) topPanel.Controls.Add(value);
            }
        }
        public Mode Mode { get => controlsPanel.SelectedMode; }
        private UpperControlManager _ucm;
        public MenuStrip MainMenu { get => mainMenuStrip; }
        private MenuEventHandler _menuEventHandler;
        public List<int> Marks { get => mainTimelineSlider.Marks; }
        public bool AskBeforeFrameDelete { get => askDeleteCB.Checked; }
        public ApplyParamsMode ApplyParamsMode { get => controlsPanel.SelectedApplyParamsMode; }
        public List<Giffer> GifferHistory { get; }
        private InputTranslator InputTranslator { get; }
        private InputHandler InputBinder { get; set; }
        private AudioRecorder AudioRecorder { get; set; }
        private RecordForm _recordForm;
        public MainForm()
        {
            InitializeComponent();
            mainTimelineSlider.SelectedFrameChanged += mainTimelineSlider_SelectedFrameChanged;


            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;

            _updateTimer = new Timer();
            _updateTimer.Interval = 17;
            _updateTimer.Tick += (sender, args) =>
            {
                UpdateMainPictureBox();
            };
            KeyPreview = true;

            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;

            rightPanel.LayerOrderChanged += mainLayersPanel_LayerOrderChanged;
            rightPanel.SelectedLayerChanged += MainLayersPanel_SelectedLayerChanged;
            rightPanel.LayerVisibilityChanged += mainLayersPanel_LayerVisibilityChanged;
            rightPanel.LayerDeleteButtonClicked += mainLayersPanel_LayerDeleteButtonClicked;
            rightPanel.SnapLayerToFrame += (sender, args) =>
            {
                GifferC.SnapLayerToFrame(SFI, args.Index, controlsPanel.SelectedApplyParamsMode);
                UpdateMainPictureBox();
            };
            rightPanel.RestoredRatio += (sender, args) =>
            {
                GifferC.RestoreRatio(SFI, args.Index, controlsPanel.SelectedApplyParamsMode);
                UpdateMainPictureBox();
            };

            controlsPanel.MustRedraw += (sender, args) => { if (MainGiffer != null) UpdateMainPictureBox(); };
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;
            controlsPanel.InterpolationModeChanged += (sender, args) => mainPictureBox.InterpolationMode = ((ControlsPanel)sender).InterpolationMode;

            controlsPanel.ModeChanged += ControlsPanel_ModeChanged;

            mainTimelineSlider.FrameDelayChanged += mainTimelineSlider_FrameDelayChanged;

            lerpButton.Click += LerpButton_Click;

            deleteFramesButton.Click += DeleteFramesButton_Click;
            dupeFrameButton.Click += DupeFrameButton_Click;

            _paintControl = new PaintControl();
            _recordControl = new RecordControl();
            _ucm = new UpperControlManager(_paintControl, _recordControl);
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
            _ssl = new ScreenStateLogger();

            _recordControl.Start += (sender, args) =>
            {
                _recordControl_Start(sender, args);
                _recordForm.SetRecordMode(true);
            };
            _recordControl.Stop += (sender, args) =>
            {
                _recordControl_Stop(sender, args);
                _recordForm.SetRecordMode(false);
            };
            _recordControl.Screenshot += (sender, args) => CaptureSingleFrame();

            _menuEventHandler = new MenuEventHandler(this);
            GifferHistory = new();

            InputTranslator = new();

            KeyPreview = true;

            _recordForm = new();
            _recordForm.Move += _recordForm_Move;
            _recordForm.StartRecording += (sender, args) =>
            {
                this.Enabled = false;
                this.WindowState = FormWindowState.Minimized;
                _recordControl_Start(sender, args);
            };
            _recordForm.StopRecording += (sender, args) =>
            {
                this.Enabled = true;
                this.WindowState = FormWindowState.Normal;
                _recordControl_Stop(sender, args);
            };
            _recordForm.Screenshot += (sender, args) =>
            {
                CaptureSingleFrame();
            };
            FormClosed += MainForm_FormClosed;
            _recordForm.FormHidden += (sender, args) => controlsPanel.SelectedMode = Mode.Move;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_tempFilePaths.Count == 0) return;
            int failedCount = 0;
            foreach(string path in _tempFilePaths)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                } else
                {
                    failedCount++;
                }
            }
            MessageBox.Show("Successfully deleted " + (_tempFilePaths.Count - failedCount) +
                " temporary files." + Environment.NewLine + "Failed to find " + failedCount + " files.");

        }

        private void _recordForm_Move(object sender, EventArgs e)
        {
            Point p = _recordForm.TopLeft;
            if (_recordForm.CanRecord())
            {
                _ssl.X = p.X;
                _ssl.Y = p.Y;
            }
        }

        public void SaveGiffer()
        {
            if (MainGiffer == null) return;
            GifferHistory.Add(MainGiffer.Clone());
            if (GifferHistory.Count > 1) //temporary, until I make it so that going further into history is possible
            {
                GifferHistory[0].Dispose();
                GifferHistory.RemoveAt(0);
            }
            Report("Giffer saved");
        }
        public void LoadGiffer()
        {
            if (GifferHistory.Count == 0)
            {
                Report("No saved giffers!");
                return;
            }
            SetNewGiffer(GifferHistory[GifferHistory.Count - 1].Clone());
            Report("Giffer loaded!");
        }
        public void Report(string message)
        {
            statusLabel.Text = message;
        }

        private void ControlsPanel_ModeChanged(object sender, EventArgs e)
        {
            Mode m = controlsPanel.SelectedMode;
            if (m == Mode.Move)
            {
                //SetRecordMode(false);
                _recordForm.Hide();
                if (GifferC != null) UpdateMainPictureBox();
                ControlsEnable(true);
            }
            else if (m == Mode.Paint)
            {
                //SetRecordMode(false);
                _recordForm.Hide();
                if (GifferC == null)
                {
                    controlsPanel.SelectedMode = Mode.Move;
                    ControlsEnable(true);
                }
                else
                {
                    if (SelectedLayer is not BitmapGFL)
                    {
                        MessageBox.Show("Only image layers can be painted on, select an image layer.");
                        controlsPanel.SelectedMode = Mode.Move;
                        ControlsEnable(true);
                    }
                    else
                    {
                        ControlsEnable(false);
                    }
                }
                if (GifferC != null) UpdateMainPictureBox();
            }
            else if (m == Mode.Record)
            {
                //SetRecordMode(true);
                _recordForm.Show();
                ControlsEnable(false);
            }
            _ucm.UpdateUpperControl(this);
        }
        private void CaptureSingleFrame()
        {
            Point p = _recordForm.TopLeft;
            if (!_recordForm.CanRecord())
            {
                MessageBox.Show("You are either off screen or the recording area is too small!");
                _recordControl.RecMode(false);
                return;
            }
            _ssl.X = p.X;
            _ssl.Y = p.Y;
            _ssl.Width = _recordForm.RecordWidth - 3;
            _ssl.Height = _recordForm.RecordHeight - 3;
            _ssl.Screenshot();
            if (GifferC == null)
            {
                SetNewGiffer(new Giffer(_ssl.Frames, _ssl.FPS));
            }
            else
            {
                GifferIO.GifImport(this, new Giffer(_ssl.Frames, _ssl.FPS));
            }
            CompleteUIUpdate();
            _ssl.ClearFrames();
            Report("Screenshot captured.");
        }
        private void _recordControl_Stop(object sender, EventArgs e)
        {
            _ssl.Stop();
            if (_recordControl.RecordSound) AudioRecorder.StopRecording();
            if (_ssl.Frames.Count == 0) return;
            if (GifferC == null)
            {
                SetNewGiffer(new Giffer(_ssl.Frames, _ssl.FPS));
            }
            else
            {
                GifferIO.GifImport(this, new Giffer(_ssl.Frames, _ssl.FPS));
            }
            if (_recordControl.RecordSound) MainGiffer.SoundPath = AudioRecorder.Path;
            CompleteUIUpdate();
            _ssl.ClearFrames();
            Report("Recording stopped.");
        }
        private void _recordControl_Start(object sender, EventArgs e)
        {
            Point p = _recordForm.TopLeft;
            if (!_recordForm.CanRecord())
            {
                MessageBox.Show("The recording window is off screen!");
                _recordControl.RecMode(false);
                return;
            }
            _ssl.X = p.X;
            _ssl.Y = p.Y;
            _ssl.Width = _recordForm.RecordWidth - 3;
            _ssl.Height = _recordForm.RecordHeight - 3;
            _ssl.FPS = _recordControl.FPS;
            try
            {
                _ssl.Start();
                if (_recordControl.RecordSound)
                {
                    AudioRecorder?.Dispose();
                    AudioRecorder = new AudioRecorder(Path.ChangeExtension(Path.GetTempFileName(), ".wav"));
                    AudioRecorder.StartRecording();
                }
                Report("Now recording.");
            }
            catch (Exception ex)
            {
                _recordControl.RecMode(false);
                MessageBox.Show(ex.Message);
            }
        }

        private void ControlsPanel_ToolRecordSelected(object sender, EventArgs e)
        {
            topPanel.Controls.Clear();
            topPanel.Controls.Add(_recordControl);
            ControlsEnable(false);
            _recordForm.Show();
        }
        public void ApplyLayerParams()
        {
            GifferC.ApplyLayerParams(SFI, SLI, controlsPanel.SelectedApplyParamsMode);
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

        private void MainLayersPanel_SelectedLayerChanged(object sender, LayersPanel.IndexEventArgs e)
        {
            SLI = e.Index;
            _ucm.UpdateUpperControl(this);
            _selectedLayerID = SelectedLayer.LayerID;
            UpdateHSBPanel();
            GifferC.SaveLayerStateForApply(SFI, SLI);
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
            if (GifferC == null) return;
            GifferC.DupeFrames(Marks, SFI, (int)frameDupeCountNUD.Value);
            CompleteUIUpdate();
        }
        private void ControlsEnable(bool val)
        {
            //mainTimelineSlider.Enabled = val;
            rightPanel.Enabled = val;
            markLerpPanel.Enabled = val;
            controlsPanel.SetPaintMode(!val);
            AllowDrop = val;
        }
        private void DeleteFramesButton_Click(object sender, EventArgs e)
        {
            if (MainGiffer == null || MainGiffer.FrameCount < 2) return;
            if (GifferC.DeleteFramesBetweenMarksOrSelected(mainTimelineSlider.Marks, askDeleteCB.Checked))
            {
                mainTimelineSlider.ClearMarks();
            }
            SFI = mainTimelineSlider.SelectedFrameIndex;
            CompleteUIUpdate();
        }

        private void LerpButton_Click(object sender, EventArgs e)
        {
            if (GifferC == null) return;
            string lerpMode = lerpModeCBB.SelectedItem.ToString();
            if (lerpMode == "trace")
            {
                GifferC.LerpExecute(mainTimelineSlider.Marks, SLI, mainPictureBox.MouseTrace);
            }
            else if (lerpMode == "line")
            {
                GifferC.LerpExecute(mainTimelineSlider.Marks, SLI);
            }
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
                rightPanel.SelectNewestLayer();
            }
        }

        public void SetNewGiffer(Giffer newGiffer)
        {
            MainGiffer?.Dispose();

            MainGiffer = newGiffer;
            GifferC = new GifferController(newGiffer);
            if (topPanel.Controls.Count > 0)
            {
                //layerParamsPanel.Controls[0].Dispose();
                if (topPanel.Controls[0] is IGFLParamControl)
                {
                    topPanel.Controls[0].Dispose();
                }
                else
                {
                    topPanel.Controls.Clear();
                }
            }
            //if (!preserveMode) controlsPanel.SelectedMode = Mode.Move;
            SFI = 0;
            SLI = 0;
            CompleteUIUpdate();
            GifferC.SaveLayerStateForApply(0, 0);
            if (InputBinder == null)
            {
                InputBinder = new(InputTranslator, GifferC, this);
            }
            else
            {
                InputBinder.SetNewController(GifferC);
            }
            Report($"New gif Width: {MainGiffer.Width}, Height: {MainGiffer.Height}, Framecount: {MainGiffer.FrameCount}");
        }
        private bool ShouldIgnoreKeyPresses()
        {
            return (Mode == Mode.Record || MainGiffer == null ||
                (SelectedLayer is TextGFL && UpperControl is TextGFLParamControl && ((TextGFLParamControl)UpperControl).TextBoxHasFocus())
                );
        }
        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (ShouldIgnoreKeyPresses()) return base.ProcessKeyPreview(ref m);
            return InputTranslator.HandleKey(m);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right ||
                keyData == Keys.Up || keyData == Keys.Down)
            {
                return InputTranslator.HandleKey(msg);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;
            try
            {
                InputTranslator.HandleMouseUp(e, Mode, _paintControl.SelectedPaintTool);
            }
            catch(Exception ex)
            {
                Report("Error: " + ex.Message);
                return;
            }
            
            MainGiffer.MakeSizeDivisible4();
            Report($"New gif Width: {MainGiffer.Width}, Height: {MainGiffer.Height}");
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
            _rotationChange = 0;
            ClickedLRCtM = LayerRotationCenterToMouse();
            InputTranslator.HandleMouseDown(e, Mode, _paintControl.SelectedPaintTool);
            _prevMousePos = MousePositionOnLayer();
            _previousLRCtM = LayerRotationCenterToMouse();
        }

        private void MainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (MainGiffer == null) return;

            InputTranslator.HandleMouseMove(e, Mode, _paintControl.SelectedPaintTool);

            _previousLRCtM = LayerRotationCenterToMouse();
            _prevMousePos = MousePositionOnLayer();
        }
        private void mainLayersPanel_LayerOrderChanged(object sender, LayersPanel.LayerOrderEventArgs e)
        {
            GifferC.ChangeLayerOrder(SFI, e.OriginalIndex, e.TargetIndex);
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
        private void mainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            SFI = mainTimelineSlider.SelectedFrameIndex;
            TimelineSlider ts = sender as TimelineSlider;
            if (!ts.PlayTimerRunning && !ts.MouseButtonIsDown)
            {
                CompleteUIUpdate();
                GifferC.SaveLayerStateForApply(SFI, SLI);
            }
            else
            {
                DateTime beforeUpdate = DateTime.Now;
                UpdateMainPictureBox();
                TimeSpan duration = DateTime.Now - beforeUpdate;
                mainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay - duration.Milliseconds;
            }
        }
        private void mainTimelineSlider_FrameDelayChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            TimelineSlider ts = sender as TimelineSlider;
            SelectedFrame.FrameDelay = ts.FrameDelay;
            if (controlsPanel.SelectedApplyParamsMode == ApplyParamsMode.None) return;
            for (int i = mainTimelineSlider.SelectedFrameIndex + 1; i < MainGiffer.FrameCount; i++)
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
            else if (cp.IsRMBDown)
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
        public void UpdateLayersPanel()
        {
            if (MainGiffer == null) return;
            SLI = GifferC.TryGetLayerIndexById(SFI, _selectedLayerID);
            rightPanel.SelectedLayerIndex = SLI;
            rightPanel.DisplayLayers(GifferC.GetFrame(SFI));
        }
        public void UpdateTimeline()
        {
            if (MainGiffer == null) return;
            mainTimelineSlider.Maximum = MainGiffer.FrameCount - 1;
            mainTimelineSlider.SelectedFrameIndex = Math.Clamp(SFI, 0, mainTimelineSlider.Maximum);
            mainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
        }
        public void UpdateUpperControl()
        {
            _ucm.UpdateUpperControl(this);
        }
        public void UpdateHSBPanel()
        {
            if (MainGiffer == null) return;
            hsbPanel.Saturation = SelectedLayer.Saturation;
            hsbPanel.Brightness = SelectedLayer.Brightness;
            hsbPanel.Transparency = SelectedLayer.Transparency;
            hsbPanel.Hue = SelectedLayer.Hue;
        }
        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = MainGiffer.FrameAsBitmap(SelectedFrame, controlsPanel.DrawHelp, controlsPanel.InterpolationMode);
        }
        public void CompleteUIUpdate()
        {
            UpdateLayersPanel();
            UpdateTimeline();
            UpdateMainPictureBox();
            UpdateUpperControl();
            UpdateHSBPanel();
        }
        public void StartUpdateTimer() => UpdateTimer.Start();
        public void StopUpdateTimer() => UpdateTimer.Stop();
        public Point MousePositionOnLayer()
        {
            return GifferC.MousePositionOnLayer(SFI, SLI, mainPictureBox.MousePositionOnImage);
        }
        public Point[] TraceToMPOL()
        {
            return mainPictureBox.MouseTrace.Select(point => point = GifferC.MousePositionOnLayer(SFI, SLI, point)).ToArray();
        }
        public LassoParams LassoParams() => _paintControl.GetLassoParams();
        public PaintParams PaintParams() => _paintControl.GetPaintParams();
        public Point PreviousMousePositionOnLayer() => _prevMousePos;
        public void SetPaintColor()
        {
            Point p = mainPictureBox.MousePositionOnImage;
            _paintControl.PaintColorRGB = ((Bitmap)MainImage).GetPixel(p.X, p.Y);
        }
        public List<Point> MouseTrace() => mainPictureBox.MouseTrace;
        public Point ScaledDragVector() => mainPictureBox.ScaledDragDifference;
        public float RotationAngleDifference()
        {
            return (float)(LayerRotationCenterToMouse().Rotation - _previousLRCtM.Rotation);
        }
        public float SizeDifference()
        {
            return (float)(LayerRotationCenterToMouse().Magnitude - ClickedLRCtM.Magnitude);
        }
        public void InvalidatePictureBox() => mainPictureBox.Invalidate();
        public void SelectLastLayer() => rightPanel.SelectNewestLayer();
        private OVector LayerRotationCenterToMouse()
        {
            OVector center = SelectedLayer.Center();
            Point ptm = mainPictureBox.PointToMouse(new Point(center.Xint, center.Yint));
            return new OVector(ptm);
        }
        public void SelectNewestLayer()
        {
            rightPanel.SelectNewestLayer();
            GifferC.SLI = rightPanel.SelectedLayerIndex;
        }
        public int NextMark() => mainTimelineSlider.NextMark();
        public int PreviousMark() => mainTimelineSlider.PreviousMark();
        public void MarkEveryNthFrame(int n)
        {
            for (int i = 0; i <= mainTimelineSlider.Maximum; i += n)
            {
                MainTimelineSlider.AddMark(i);
            }
        }
        public void AddTempFilePath(string path)
        {
            _tempFilePaths.Add(path);
        }
    }
}
