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
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Microsoft.VisualBasic;
using BIUK9000.GifferComponents;
using Emgu.CV.XImgproc;

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public bool IsShiftDown { get => isShiftDown; }
        public Timer UpdateTimer { get => updateTimer; }
        public string WorkingDirectory { get => Environment.CurrentDirectory; }
        public string ProjectDirectory { get => Directory.GetParent(WorkingDirectory).Parent.Parent.Parent.FullName; }
        public string TempJpegPath { get => Path.Combine(ProjectDirectory, "tempJpeg.jpeg"); }
        public string TempGifPath { get => Path.Combine(ProjectDirectory, "tempGif.gif"); }
        public string TempCompressedGifPath { get => Path.Combine(ProjectDirectory, "tempGifc.gif"); }
        public Giffer MainGiffer { get; set; }
        private bool isShiftDown;
        private Timer updateTimer;
        private bool draggingFileForExport;
        public MainForm()
        {
            InitializeComponent();
            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
            //MainGiffer = new Giffer(Path.Combine(imageDirectory, "tldr-didnt.gif"));
            //mainTimelineSlider.Giffer = MainGiffer;
            
            mainTimelineSlider.SelectedFrameChanged += MainTimelineSlider_SelectedFrameChanged;

            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;

            updateTimer = new Timer();
            updateTimer.Interval = 17;
            updateTimer.Tick += UpdateTimer_Tick;

            KeyPreview = true;

            SaveButton.MouseDown += SaveButton_MouseDown;
            SaveButton.MouseUp += SaveButton_MouseUp;
        }

        private void SaveButton_MouseUp(object sender, MouseEventArgs e)
        {
            draggingFileForExport = false;
        }

        private void SaveButton_MouseDown(object sender, MouseEventArgs e)
        {
            draggingFileForExport = true;
            if (MainGiffer == null) return;
            if (e.Button == MouseButtons.Left)
            {
                using Bitmap bitmap = mainTimelineSlider.SelectedFrame.CompleteBitmap(false);
                string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".jpeg");
                bitmap.Save(tempPath);
                DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath});
                DoDragDrop(data, DragDropEffects.Copy);
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            } else if(e.Button == MouseButtons.Right)
            {
                using Image gif = MainGiffer.GifFromFrames();
                string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
                gif.Save(tempPath);
                OBIMP.CompressGif(tempPath, tempPath, 50, 100);
                DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath});
                DoDragDrop(data, DragDropEffects.Copy);
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }
        public void ApplyCurrentLayerParamsToSubsequentLayers()
        {
            GFL cgfl = mainLayersPanel.SelectedLayer;
            GifFrame cgf = MainTimelineSlider.SelectedFrame;
            int cli = cgf.Layers.IndexOf(cgfl);
            int cgfi = MainGiffer.Frames.IndexOf(cgf);
            for (int i = cgfi + 1; i < MainGiffer.Frames.Count; i++)
            {
                GifFrame gf = MainGiffer.Frames[i];
                if(cli > 0 && cli < gf.Layers.Count)
                {
                    gf.Layers[cli].CopyParameters(cgfl);
                }
            }
        }

        private void GifFrame_LayerCountChanged(object sender, EventArgs e)
        {
            mainPictureBox.UpdatePictureBox();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            mainPictureBox.UpdatePictureBox();
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if(MainGiffer == null) return base.ProcessKeyPreview(ref m);
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value < mts.Maximum) mts.Value += 1;
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    TrackBar mts = mainTimelineSlider.Slider;
                    if (mts.Value > 0) mts.Value -= 1;
                    return true;
                }
                else if (keyData == Keys.ShiftKey)
                {
                    isShiftDown = true;
                    return true;
                } else if(keyData == Keys.T)
                {
                    TextGFL tgfl = new TextGFL("YOUR TEXT");
                    tgfl.FontName = "Impact";
                    tgfl.FontBorderColor = Color.Black;
                    tgfl.FontColor = Color.White;
                    tgfl.FontBorderWidth = 5;
                    tgfl.FontSize = 20;
                    mainTimelineSlider.SelectedFrame.AddLayer(tgfl);
                    return true;
                } else if(keyData == Keys.L)
                {
                    //Bitmap bmp = new Bitmap(500, 500);
                    //using Graphics g = Graphics.FromImage(bmp);
                    //g.Clear(Color.Black);
                    //g.DrawString("fuuuuuuuuu", new Font("Impact", 50), new SolidBrush(Color.White), new Point(0, 0));
                    //Size s = TextRenderer.MeasureText("fuuuuuuuuu", new Font("Impact", 50));
                    //g.DrawRectangle(Pens.Red, 0, 0, s.Width, s.Height);
                    //mainPictureBox.Image = bmp;

                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {
                    
                    isShiftDown = false;
                    return true;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }

        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            MainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
            mainPictureBox.UpdatePictureBox();
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
            if (draggingFileForExport) return;
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if(filePaths.Length > 0)
            {
                string fullPath = filePaths[0];
                string ext = Path.GetExtension(fullPath);

                if(ext == ".gif")
                {
                    Giffer newGiffer = new Giffer(fullPath);
                    if (MainGiffer == null)
                    {
                        MainGiffer = newGiffer;
                        mainTimelineSlider.Giffer = newGiffer;
                        mainPictureBox.Image = MainGiffer.Frames[0].CompleteBitmap(true);
                        foreach (GifFrame gifFrame in MainGiffer.Frames)
                        {
                            gifFrame.LayerCountChanged += GifFrame_LayerCountChanged;
                        }
                        mainLayersPanel.DisplayLayers(mainTimelineSlider.SelectedFrame);
                    }
                    else
                    {
                        MainGiffer.AddGifferAsLayers(newGiffer);
                    }
                } else
                {
                    try
                    {
                        Bitmap bitmap = new Bitmap(fullPath);
                        foreach(GifFrame gifFrame in MainGiffer.Frames)
                        {
                            gifFrame.AddLayer(bitmap);
                        }
                    } catch (Exception ex)
                    {
                        MessageBox.Show("This is not an image file!");
                    }
                }
            }
        }
    }
}
