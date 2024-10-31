using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class ControlsPanel : UserControl
    {
        private OVector OriginalMousePosition { get; set; }
        private bool IsLMBDown {  get; set; }
        private bool IsRMBDown {  get; set; }
        public bool DraggingFileForExport { get; set; }
        private MainForm MF { get => ParentForm as MainForm; }
        private Giffer MG { get => MF.MainGiffer; }
        public int GifExportLossy { get => (int)(GifExportLossyNUD.Value); }
        public int GifExportColors { get => (int)GifExportColorsNUD.Value; }
        public string ImageExportFormat { get => ImageExportFormatCBB.Text; }
        public bool RotationSnap { get => rotationSnapCB.Checked; }
        public bool PositionSnap { get => positionSnapCB.Checked; }
        public bool DrawHelp { get => drawHelpCB.Checked; }
        public event EventHandler MustRedraw;
        public event EventHandler SaveGifDialogOKed;
        public ControlsPanel()
        {
            InitializeComponent();
            SaveButton.MouseDown += SaveButton_MouseDown;
            SaveButton.MouseUp += SaveButton_MouseUp;
            SaveButton.MouseMove += SaveButton_MouseMove;
            ImageExportFormatCBB.SelectedIndex = 0;
            drawHelpCB.CheckedChanged += DrawHelpCB_CheckedChanged;
            OriginalMousePosition = new OVector(0, 0);
            IsLMBDown = false;
            IsRMBDown = false;
        }

        private void SaveButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (MG == null) return;
            if (ShouldStartDragdrop(e, 10))
            {
                if (IsLMBDown)
                {
                    DraggingFileForExport = true;
                    using Bitmap bitmap = MF.MainTimelineSlider.SelectedFrame.CompleteBitmap(false);
                    string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ImageExportFormat);
                    switch (ImageExportFormat)
                    {
                        case ".jpeg":
                            OBIMP.SaveJpeg(tempPath, bitmap, 80);
                            break;
                        default:
                            bitmap.Save(tempPath);
                            break;
                    }
                    DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
                    DoDragDrop(data, DragDropEffects.Copy);
                    IsLMBDown = false;
                    if (File.Exists(tempPath))
                    {
                        File.Delete(tempPath);
                    }
                    DraggingFileForExport = false;
                }
                else if (IsRMBDown)
                {
                    DraggingFileForExport = true;
                    using Image gif = MG.GifFromFrames();
                    string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
                    gif.Save(tempPath);
                    OBIMP.CompressGif(tempPath, tempPath, GifExportColors, GifExportLossy);
                    DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
                    DoDragDrop(data, DragDropEffects.Copy);
                    IsRMBDown = false;
                    if (File.Exists(tempPath))
                    {
                        File.Delete(tempPath);
                    }
                    DraggingFileForExport = false;
                }
            }
        }

        private bool ShouldStartDragdrop(MouseEventArgs e, double distanceLimit)
        {
            OVector currentPosOV = new OVector(e.X, e.Y);
            double draggedDist = currentPosOV.Subtract(OriginalMousePosition).Magnitude;
            return draggedDist > distanceLimit;
        }
        private void DrawHelpCB_CheckedChanged(object sender, EventArgs e)
        {
            MustRedraw?.Invoke(this, EventArgs.Empty);
        }

        private void SaveButton_MouseDown(object sender, MouseEventArgs e)
        {
            OriginalMousePosition = new OVector(e.X, e.Y);
            if(e.Button == MouseButtons.Left)
            {
                IsLMBDown = true;
            } else if(e.Button == MouseButtons.Right)
            {
                IsRMBDown = true;
            }
            
        }
        private void SaveButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsLMBDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                IsRMBDown = false;
            }
            DraggingFileForExport = false;
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveGifDialogOKed?.Invoke(saveFileDialog, EventArgs.Empty);
            }
        }
    }
}
