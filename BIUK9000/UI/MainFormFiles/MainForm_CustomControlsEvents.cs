using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BIUK9000.GifferComponents;
using System.Drawing;
using BIUK9000.Dithering;

namespace BIUK9000.UI
{
    public partial class MainForm
    {
        private void MainLayersPanel_LayerOrderChanged(object sender, LayersPanel.LayerOrderEventArgs e)
        {
            for (int i = MainTimelineSlider.SelectedFrameIndex; i < MainGiffer.Frames.Count; i++)
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
            if(MessageBox.Show("Are you sure you want to delete this layer?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return;
            int layerIDToDelete = (sender as LayerHolder).HeldLayer.LayerID;
            foreach (GifFrame gf in MainGiffer.Frames)
            {
                GFL layerToDelete = gf.Layers.Find(layer => layer.LayerID == layerIDToDelete);
                if (layerToDelete != null) gf.Layers.Remove(layerToDelete);
            }
            UpdateMainPictureBox();
            MainLayersPanel.DisplayLayers(SelectedFrame);
        }
        private void MainLayersPanel_LayerVisibilityChanged(object sender, EventArgs e)
        {
            UpdateMainPictureBox();
            SavePreviousState();
            PreviousLayerState.Visible = !SelectedLayer.Visible;
            ApplyCurrentLayerParamsToSubsequentLayers();
        }
        private void MainTimelineSlider_SelectedFrameChanged(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            TimelineSlider ts = sender as TimelineSlider;
            if (!ts.PlayTimerRunning)
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
            TimelineSlider ts = sender as TimelineSlider;
            SelectedFrame.FrameDelay = ts.FrameDelay;
            if (controlsPanel.SelectedApplyParamsMode == ApplyParamsMode.applyNone) return;
            for (int i = MainTimelineSlider.SelectedFrameIndex + 1; i < MainGiffer.Frames.Count; i++)
            {
                MainGiffer.Frames[i].FrameDelay = SelectedFrame.FrameDelay;
            }
        }
        private void ControlsPanel_ShouldStartDragDrop(object sender, EventArgs e)
        {
            if(MainGiffer == null) return;
            ControlsPanel cp = sender as ControlsPanel;
            if (cp.IsLMBDown)
            {
                cp.DraggingFileForExport = true;
                Bitmap bitmap = SelectedFrame.CompleteBitmap(false);
                if (controlsPanel.UseDithering)
                {
                    Bitmap bitmapRefBackup = bitmap;
                    using Ditherer dtr = new Ditherer(bitmap);
                    bitmap = dtr.DitheredBitmap(KMeans.Palette(bitmap, controlsPanel.GifExportColors, false));
                    bitmapRefBackup.Dispose();
                }
                string tempPath = Path.ChangeExtension(Path.GetTempFileName(), cp.ImageExportFormat);
                switch (cp.ImageExportFormat)
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
                cp.IsLMBDown = false;
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
                cp.DraggingFileForExport = false;
            }
            //else if (cp.IsRMBDown)
            //{
            //    cp.DraggingFileForExport = true;
            //    using Image gif = MainGiffer.GifFromFrames();
            //    string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".gif");
            //    gif.Save(tempPath);
            //    OBIMP.CompressGif(tempPath, tempPath, cp.GifExportColors, cp.GifExportLossy);
            //    DataObject data = new DataObject(DataFormats.FileDrop, new string[] { tempPath });
            //    DoDragDrop(data, DragDropEffects.Copy);
            //    cp.IsRMBDown = false;
            //    if (File.Exists(tempPath))
            //    {
            //        File.Delete(tempPath);
            //    }
            //    cp.DraggingFileForExport = false;
            //}
        }
        private void ControlsPanel_SaveButtonClicked(object sender, EventArgs e)
        {
            if (MainGiffer == null) return;
            SaveFileDialog sfd = saveFileDialog;
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                if (sfd.FileName == MainGiffer.OriginalImagePath)
                {
                    MessageBox.Show("Do not use the same file name for export as you did for import.");
                    return;
                }
                string tempPath = SaveGifToTempFile();
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
    }
}
