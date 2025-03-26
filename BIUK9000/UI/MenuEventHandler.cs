using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    class MenuEventHandler
    {
        private MainForm _mf;
        private MenuStrip _menu;
        public MenuEventHandler(MainForm mainForm)
        {
            _mf = mainForm;
            _menu = _mf.MainMenu;
            var framesReverseMI = FindMenuItemByName("framesReverseMI");
            
            var framesAddReversedMI = FindMenuItemByName("framesAddReversedMI");
            var layerAddTextMI = FindMenuItemByName("layerAddTextMI");
            var layerAddShapeMI = FindMenuItemByName("layerAddShapeMI");
            var layerFlattenMI = FindMenuItemByName("layerFlattenMI");
            var layerSnapToFrameMI = FindMenuItemByName("layerSnapToFrameMI");
            var layerRestoreRatioMI = FindMenuItemByName("layerRestoreRatioMI");
            layerRestoreRatioMI.Click += LayerRestoreRatioMI_Click;
            layerSnapToFrameMI.Click += LayerSnapToFrameMI_Click;
            layerFlattenMI.Click += LayerFlattenMI_Click;
            framesReverseMI.Click += FramesReverseMI_Click;
            framesAddReversedMI.Click += FramesAddReversedMI_Click;
            layerAddTextMI.Click += LayerAddTextMI_Click;
            layerAddShapeMI.Click += LayerAddShapeMI_Click;
        }


        private ToolStripMenuItem FindMenuItemByName(string name)
        {
            return FindMenuItemByName(_menu.Items, name);
        }

        private ToolStripMenuItem FindMenuItemByName(ToolStripItemCollection items, string name)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    if (menuItem.Name == name)
                        return menuItem;

                    var foundItem = FindMenuItemByName(menuItem.DropDownItems, name);
                    if (foundItem != null)
                        return foundItem;
                }
            }
            return null;
        }
        private void LayerRestoreRatioMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.RestoreRatio(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode);
            _mf.UpdateMainPictureBox();
        }
        private void LayerSnapToFrameMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.SnapLayerToFrame(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode);
            _mf.UpdateMainPictureBox();
        }
        private void LayerFlattenMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.Flatten();
            _mf.CompleteUIUpdate(false);
        }
        private void LayerAddShapeMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.AddLayerFromKey(Keys.B);
            _mf.GifferC.Mirror();
            _mf.CompleteUIUpdate();
        }

        private void LayerAddTextMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.AddLayerFromKey(Keys.T);
            _mf.CompleteUIUpdate();
        }

        private void FramesAddReversedMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.AddReversedFrames();
            _mf.CompleteUIUpdate();
        }

        private void FramesReverseMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.ReverseFrames();
            _mf.CompleteUIUpdate();
        }
        private bool CheckNull()
        {
            return _mf.GifferC == null;
        }
    }
}
