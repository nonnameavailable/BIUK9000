using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public bool DraggingFileForExport {  get; set; }
        private MainForm MF { get => ParentForm as MainForm; }
        private Giffer MG { get => MF.MainGiffer; }
        public int GifExportLossy { get => (int)(GifExportLossyNUD.Value); }
        public int GifExportColors { get => (int)GifExportColorsNUD.Value; }
        public ControlsPanel()
        {
            InitializeComponent();
            SaveButton.MouseDown += SaveButton_MouseDown;
        }

        private void SaveButton_MouseDown(object sender, MouseEventArgs e)
        {
            DraggingFileForExport = true;
            if (MG == null) return;
            if (e.Button == MouseButtons.Left)
            {
                using Bitmap bitmap = MF.MainTimelineSlider.SelectedFrame.CompleteBitmap(false);
                string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".jpeg");
                OBIMP.SaveJpeg(tempPath, bitmap, 80);
                DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
                DoDragDrop(data, DragDropEffects.Copy);
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                using Image gif = MG.GifFromFrames();
                string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
                gif.Save(tempPath);
                OBIMP.CompressGif(tempPath, tempPath, GifExportColors, GifExportLossy);
                DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
                DoDragDrop(data, DragDropEffects.Copy);
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
            }
        }
        private void SaveButton_MouseUp(object sender, MouseEventArgs e)
        {
            DraggingFileForExport = false;
        }
    }
}
