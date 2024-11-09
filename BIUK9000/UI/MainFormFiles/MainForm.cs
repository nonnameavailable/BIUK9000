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

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        private LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public ControlsPanel MainControlsPanel { get => controlsPanel; }
        public GifFrame SelectedFrame { get => MainGiffer.Frames[MainTimelineSlider.SelectedFrameIndex]; }
        private GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
        private GFL PreviousLayerState { get; set; }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        private GFL PreviousSelectedLayer { get; set; }
        private FrameAndLayer LerpStart { get; set; }


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

            controlsPanel.MustRedraw += (sender, args) => UpdateMainPictureBox();
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;

            lerpStartButton.Click += LerpStartButton_Click;
            lerpExecuteButton.Click += LerpExecuteButton_Click;
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
                if (layerToLerp != null)
                {
                    layerToLerp.Lerp(startLayer, SelectedLayer, distance);
                }
            }
        }

        private void LerpStartButton_Click(object sender, EventArgs e)
        {
            LerpStart = new FrameAndLayer(SelectedLayer, SelectedFrame);
        }

        private void SavePreviousState()
        {
            PreviousLayerState = SelectedLayer.Clone();
        }

        public void ApplyCurrentLayerParamsToSubsequentLayers()
        {
            ApplyParamsMode apm = controlsPanel.SelectedApplyParamsMode;
            if (apm == ApplyParamsMode.applyNone) return;
            int cli = mainLayersPanel.SelectedLayerIndex;
            int cgfi = MainGiffer.Frames.IndexOf(SelectedFrame);
            for (int i = cgfi + 1; i < MainGiffer.Frames.Count; i++)
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
                    if(apm == ApplyParamsMode.applyChanged)
                    {
                        layerToUpdate.CopyDifferingParams(PreviousLayerState, SelectedLayer);
                    } else
                    {
                        layerToUpdate.CopyParameters(SelectedLayer);
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
            if (filePaths.Length > 0)
            {
                foreach (string filePath in filePaths)
                {
                    string ext = Path.GetExtension(filePath);

                    if (ext == ".gif")
                    {
                        Giffer newGiffer = new Giffer(filePath);
                        if (MainGiffer == null)
                        {
                            SetNewGiffer(newGiffer);
                        } 
                         else
                        {
                            using ImportQuestionForm iqf = new ImportQuestionForm();
                            iqf.SelectedFresh += (sender, args) => SetNewGiffer(newGiffer);
                            iqf.SelectedAsLayers += (sender, args) =>
                            {
                                MainGiffer.AddGifferAsLayers(newGiffer);
                                newGiffer.Dispose();
                            };
                            iqf.ShowDialog();
                        }
                    }
                    else
                    {
                        Bitmap bitmap;
                        try
                        {
                            bitmap = new Bitmap(filePath);
                        }
                        catch
                        {
                            MessageBox.Show(Path.GetFileName(filePath) + "is not an image file!");
                            return;
                        }
                        if (MainGiffer == null)
                        {
                            MainGiffer = new Giffer();
                            MainGiffer.AddFrame(new GifFrame(bitmap, 20, MainGiffer.NextLayerID()));
                        }
                        else
                        {
                            int nextLayerID = MainGiffer.NextLayerID();
                            MainGiffer.Frames.ForEach(frame => frame.AddLayer(bitmap, nextLayerID));
                        }
                    }
                }
                mainLayersPanel.DisplayLayers(SelectedFrame);
                mainLayersPanel.SelectNewestLayer();
            }
            UpdateMainPictureBox();
        }

        private void SetNewGiffer(Giffer newGiffer)
        {
            MainGiffer?.Dispose();
            MainGiffer = newGiffer;
            MainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.DisplayLayers(SelectedFrame);
            MainTimelineSlider.Maximum = MainGiffer.Frames.Count - 1;
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            SavePreviousState();
        }

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            Bitmap bitmap = SelectedFrame.CompleteBitmap(controlsPanel.DrawHelp);
            MainImage = bitmap;

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
                    SavePreviousState();
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
    }
}
