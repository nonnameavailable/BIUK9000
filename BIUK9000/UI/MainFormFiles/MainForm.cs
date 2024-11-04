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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Text;
using BIUK9000.Properties;
using BIUK9000.Dithering;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        private LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        //private GifFrame SelectedFrame { get => MainTimelineSlider.SelectedFrame; }
        public GifFrame SelectedFrame { get => MainGiffer.Frames[MainTimelineSlider.SelectedFrameIndex]; }
        private GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
        private GFL PreviousLayerState { get; set; }
        private Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }

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
            mainLayersPanel.MustRedraw += (sender, args) => UpdateMainPictureBox();

            controlsPanel.MustRedraw += (sender, args) => UpdateMainPictureBox();
            controlsPanel.ShouldStartDragDrop += ControlsPanel_ShouldStartDragDrop;
            controlsPanel.SaveButtonClicked += ControlsPanel_SaveButtonClicked;

            MainTimelineSlider.FrameDelayChanged += MainTimelineSlider_FrameDelayChanged;
        }

        private void SavePreviousState()
        {
            PreviousLayerState = SelectedLayer.Clone();
        }

        public string SaveGifToTempFile()
        {
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            Image gif;
            if (controlsPanel.UseDithering)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                List<Color> palette = KMeans.Palette((Bitmap)MainImage, controlsPanel.GifExportColors, false);
                //List<Color> palette = KMeans.Palette(MainGiffer, controlsPanel.GifExportColors);
                MessageBox.Show("palette creation took: " + (stopwatch.ElapsedMilliseconds / 1000d).ToString());
                stopwatch.Restart();
                gif = MainGiffer.GifFromFrames(palette);
                //MessageBox.Show("dithering took: " + (stopwatch.ElapsedMilliseconds / 1000d).ToString());
            }
            else
            {
                gif = MainGiffer.GifFromFrames();
            }
            gif.Save(tempPath);
            if (controlsPanel.UseGifsicle)
            {
                OBIMP.CompressGif(tempPath, tempPath, controlsPanel.GifExportColors, controlsPanel.GifExportLossy);
            }
            if(File.Exists(tempPath))
            {
                return tempPath;
            } else
            {
                return null;
            }
        }

        public void ApplyCurrentLayerParamsToSubsequentLayers()
        {
            ApplyParamsMode apm = controlsPanel.SelectedApplyParamsMode;
            if (apm == ApplyParamsMode.applyNone) return;
            //int cli = SelectedFrame.Layers.IndexOf(SelectedLayer);
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
                            if(iqf.ShowDialog() == DialogResult.OK)
                            {
                                switch(iqf.SelectedImportType)
                                {
                                    case ImportQuestionForm.IMPORT_AS_LAYERS:
                                        MainGiffer.AddGifferAsLayers(newGiffer);
                                        newGiffer.Dispose();
                                        break;
                                    case ImportQuestionForm.IMPORT_INSERT:
                                        MessageBox.Show("Not implemented yet :)");
                                        break;
                                    default: //FRESH
                                        SetNewGiffer(newGiffer);
                                        break;
                                }
                            }
                            
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
                            mainLayersPanel.DisplayLayers(MainGiffer.Frames[0]);
                            //mainTimelineSlider.Giffer = MainGiffer;
                            UpdateMainPictureBox();
                        }
                        else
                        {
                            int nextLayerID = MainGiffer.NextLayerID();
                            foreach (GifFrame gifFrame in MainGiffer.Frames)
                            {
                                gifFrame.LayerCountChanged -= GifFrame_LayerCountChanged;
                                gifFrame.AddLayer(bitmap, nextLayerID);
                                gifFrame.LayerCountChanged += GifFrame_LayerCountChanged;
                            }
                            MainLayersPanel.DisplayLayers(SelectedFrame);
                            UpdateMainPictureBox();
                        }
                    }
                }
                mainLayersPanel.SelectNewestLayer();
            }
        }

        private void SetNewGiffer(Giffer newGiffer)
        {
            Giffer oldGiffer = MainGiffer;
            MainGiffer = newGiffer;
            //mainTimelineSlider.Giffer = newGiffer;
            UpdateMainPictureBox();
            foreach (GifFrame gifFrame in MainGiffer.Frames)
            {
                gifFrame.LayerCountChanged += GifFrame_LayerCountChanged;
            }
            MainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.DisplayLayers(SelectedFrame);
            oldGiffer?.Dispose();
            MainTimelineSlider.Maximum = MainGiffer.Frames.Count - 1;
            MainTimelineSlider.FrameDelay = SelectedFrame.FrameDelay;
            //mainTimelineSlider.UpdateDelayNUD();
        }

        private void GifFrame_LayerCountChanged(object sender, EventArgs e)
        {
            UpdateMainPictureBox();
            MainLayersPanel.DisplayLayers(SelectedFrame);
        }

        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            Bitmap bitmap = SelectedFrame.CompleteBitmap(controlsPanel.DrawHelp);
            //List<Color> palette = KMeans.Palette(bitmap, 10);
            //Ditherer dtr = new Ditherer(bitmap);
            //MainImage = dtr.DitheredBitmap(palette);
            //dtr.Dispose();
            MainImage = bitmap;
        }
    }
}
