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

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public Image MainImage { get => mainPictureBox.Image; set => mainPictureBox.Image = value; }
        private LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        private GifFrame SelectedFrame { get => MainTimelineSlider.SelectedFrame; }
        private GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
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
            _updateTimer.Tick += UpdateTimer_Tick;

            KeyPreview = true;

            mainPictureBox.MouseMove += MainPictureBox_MouseMove;
            mainPictureBox.MouseDown += MainPictureBox_MouseDown;
            mainPictureBox.MouseUp += MainPictureBox_MouseUp;

            MainLayersPanel.LayerOrderChanged += MainLayersPanel_LayerOrderChanged;
            MainLayersPanel.SelectedLayerChanged += MainLayersPanel_SelectedLayerChanged;
            textLayerParamsGB.Visible = false;
            PopulateFontComboBox();

            textLayerFontCBB.SelectedIndexChanged += TextLayerFontCBB_SelectedIndexChanged;
            textLayerTextTB.TextChanged += TextLayerTextTB_TextChanged;
            borderColorButton.ColorChanged += BorderColorButton_ColorChanged;
            fontColorButton.ColorChanged += FontColorButton_ColorChanged;

            controlsPanel.MustRedraw += (sender, args) => UpdateMainPictureBox();
            controlsPanel.SaveGifDialogOKed += ControlsPanel_SaveGifDialogOKed;
        }

        private void ControlsPanel_SaveGifDialogOKed(object sender, EventArgs e)
        {
            SaveFileDialog sfd = sender as SaveFileDialog;
            if(sfd.FileName == MainGiffer.OriginalImagePath)
            {
                MessageBox.Show("Do not use the same file name for export as you did for import.");
                return;
            }
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            MainGiffer.GifFromFrames().Save(tempPath);
            OBIMP.CompressGif(tempPath, sfd.FileName, controlsPanel.GifExportColors, controlsPanel.GifExportLossy);
            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }

        private void FontColorButton_ColorChanged(object sender, EventArgs e)
        {
            TextGFL tgfl = SelectedLayer as TextGFL;
            tgfl.FontColor = (sender as ColorButton).Color;
            UpdateMainPictureBox();
            ApplyCurrentLayerParamsToSubsequentLayers();
        }

        private void BorderColorButton_ColorChanged(object sender, EventArgs e)
        {
            TextGFL tgfl = SelectedLayer as TextGFL;
            tgfl.FontBorderColor = (sender as ColorButton).Color;
            UpdateMainPictureBox();
            ApplyCurrentLayerParamsToSubsequentLayers();
        }

        private void MainLayersPanel_SelectedLayerChanged(object sender, EventArgs e)
        {
            if (sender is TextGFL)
            {
                TextGFL tgfl = (TextGFL)sender;
                textLayerParamsGB.Visible = true;
                textLayerTextTB.Text = tgfl.Text;
                textLayerFontCBB.SelectedItem = tgfl.FontName;
            }
            else
            {
                textLayerParamsGB.Visible = false;
            }
        }

        private void PopulateFontComboBox()
        {
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (FontFamily fontFamily in installedFonts.Families)
            {
                textLayerFontCBB.Items.Add(fontFamily.Name);
            }
            if (textLayerFontCBB.Items.Contains("Impact"))
            {
                textLayerFontCBB.SelectedItem = "Impact";
            }
            else
            {
                textLayerFontCBB.SelectedIndex = 0;
            }
        }

        private void MainLayersPanel_LayerOrderChanged(object sender, LayersPanel.LayerOrderEventArgs e)
        {
            for (int i = mainTimelineSlider.Slider.Value; i < MainGiffer.Frames.Count; i++)
            {
                GifFrame cf = MainGiffer.Frames[i];
                GFL gflToInsert = cf.Layers[e.OriginalIndex];
                cf.Layers.RemoveAt(e.OriginalIndex);
                cf.Layers.Insert(e.TargetIndex, gflToInsert);
            }
            UpdateMainPictureBox();
            MainLayersPanel.DisplayLayers(SelectedFrame);
        }

        public void ApplyCurrentLayerParamsToSubsequentLayers()
        {
            int cli = SelectedFrame.Layers.IndexOf(SelectedLayer);
            int cgfi = MainGiffer.Frames.IndexOf(SelectedFrame);
            for (int i = cgfi + 1; i < MainGiffer.Frames.Count; i++)
            {
                GifFrame gf = MainGiffer.Frames[i];
                if (cli >= 0 && cli < gf.Layers.Count)
                {
                    gf.Layers[cli].CopyParameters(SelectedLayer);
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateMainPictureBox();
        }

        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            MainLayersPanel.DisplayLayers(SelectedFrame);
            UpdateMainPictureBox();
            UpdateTextUI();
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
                            ImportQuestionForm iqf = new ImportQuestionForm();
                            if(iqf.ShowDialog() == DialogResult.OK)
                            {
                                switch(iqf.SelectedImportType)
                                {
                                    case ImportQuestionForm.IMPORT_AS_LAYERS:
                                        MainGiffer.AddGifferAsLayers(newGiffer);
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
                        try
                        {
                            Bitmap bitmap = new Bitmap(filePath);
                            if(MainGiffer == null)
                            {
                                MainGiffer = new Giffer();
                                MainGiffer.AddFrame(new GifFrame(bitmap, 20));
                                mainLayersPanel.DisplayLayers(MainGiffer.Frames[0]);
                                mainTimelineSlider.Giffer = MainGiffer;
                                UpdateMainPictureBox();
                            } else
                            {
                                foreach (GifFrame gifFrame in MainGiffer.Frames)
                                {
                                    gifFrame.AddLayer(bitmap);
                                }
                            }

                        }
                        catch
                        {
                            MessageBox.Show(Path.GetFileName(filePath) + "is not an image file!");
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
            mainTimelineSlider.Giffer = newGiffer;
            UpdateMainPictureBox();
            foreach (GifFrame gifFrame in MainGiffer.Frames)
            {
                gifFrame.LayerCountChanged += UpdateTimer_Tick;
            }
            MainLayersPanel.SelectedLayerIndex = 0;
            mainLayersPanel.DisplayLayers(SelectedFrame);
            oldGiffer?.Dispose();
        }
        private void TextLayerTextTB_TextChanged(object sender, EventArgs e)
        {
            TextGFL tgfl = SelectedLayer as TextGFL;
            tgfl.Text = textLayerTextTB.Text;
            UpdateMainPictureBox();
            ApplyCurrentLayerParamsToSubsequentLayers();
        }

        private void TextLayerFontCBB_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextGFL tgfl = SelectedLayer as TextGFL;
            tgfl.FontName = textLayerFontCBB.Text;
            UpdateMainPictureBox();
            ApplyCurrentLayerParamsToSubsequentLayers();
        }

        private void TextLayerBorderWidthNUD_ValueChanged(object sender, EventArgs e)
        {
            TextGFL tgfl = SelectedLayer as TextGFL;
            tgfl.FontBorderWidth = (float)TextLayerBorderWidthNUD.Value;
            UpdateMainPictureBox();
            ApplyCurrentLayerParamsToSubsequentLayers();
        }
        private void UpdateTextUI()
        {
            if (SelectedLayer == null) return;
            if(SelectedLayer is TextGFL)
            {
                TextGFL tgfl = SelectedLayer as TextGFL;
                TextLayerBorderWidthNUD.Value = (decimal)tgfl.FontBorderWidth;
                textLayerTextTB.Text = tgfl.Text;
                textLayerFontCBB.SelectedItem = tgfl.FontName;
                fontColorButton.Color = tgfl.FontColor;
                borderColorButton.Color = tgfl.FontBorderColor;
            }
        }
        public void UpdateMainPictureBox()
        {
            MainImage?.Dispose();
            MainImage = SelectedFrame.CompleteBitmap(controlsPanel.DrawHelp);
            mainTimelineSlider.UpdateDelayNUD();
        }
    }
}
