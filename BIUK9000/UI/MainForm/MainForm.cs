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

namespace BIUK9000.UI
{
    public partial class MainForm : Form
    {
        public LayersPanel MainLayersPanel { get => mainLayersPanel; }
        public TimelineSlider MainTimelineSlider { get => mainTimelineSlider; }
        public GifFrame SelectedFrame { get => MainTimelineSlider.SelectedFrame; }
        public GFL SelectedLayer { get => MainLayersPanel.SelectedLayer; }
        public bool IsShiftDown { get => _isShiftDown; }
        public bool IsCtrlDown { get => _isCtrlDown; }
        public Timer UpdateTimer { get => _updateTimer; }
        public Giffer MainGiffer { get; set; }
        private bool _isShiftDown, _isCtrlDown;
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
                if(cli >= 0 && cli < gf.Layers.Count)
                {
                    gf.Layers[cli].CopyParameters(SelectedLayer);
                }
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateMainPictureBox();
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
                else if(keyData == Keys.T)
                {
                    TextGFL tgfl = new TextGFL("YOUR TEXT");
                    tgfl.FontName = "Impact";
                    tgfl.FontBorderColor = Color.Black;
                    tgfl.FontColor = Color.White;
                    tgfl.FontBorderWidth = 5;
                    tgfl.FontSize = 20;
                    SelectedFrame.AddLayer(tgfl);
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
                    //SelectedFrame.AddLayer(new CropGFL(100, 100));
                    //SelectedFrame.AddSpace(100, 0, 0, 0);
                    TextGFL tl = new TextGFL("WHEN I TAKE PRE-WORKOUT" + Environment.NewLine + "FOR THE FIRST TIME");
                    tl.FontName = "Impact";
                    tl.FontColor = Color.White;
                    tl.FontBorderColor = Color.Black;
                    tl.FontBorderWidth = 3;
                    tl.FontSize = 20;
                    MainGiffer.Frames.ForEach(frame => frame.AddLayer(tl));
                    return true;
                } else if(keyData == Keys.C)
                {
                    MainGiffer.Crop(SelectedFrame);
                    MainLayersPanel.DisplayLayers(SelectedFrame);
                    UpdateMainPictureBox();
                    return true;
                }
                else if (keyData == Keys.ShiftKey)
                {
                    _isShiftDown = true;
                    return true;
                } else if(keyData == Keys.ControlKey)
                {
                    _isCtrlDown = true;
                    return true;
                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {
                    
                    _isShiftDown = false;
                    return true;
                } if(keyData == Keys.ControlKey)
                {
                    _isCtrlDown = false;
                    return true;
                }
            }

            return base.ProcessKeyPreview(ref m);
        }

        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            MainLayersPanel.DisplayLayers(SelectedFrame);
            UpdateMainPictureBox();
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
            //if (draggingFileForExport) return;
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
                            gifFrame.LayerCountChanged += UpdateTimer_Tick;
                        }
                        mainLayersPanel.DisplayLayers(SelectedFrame);
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
