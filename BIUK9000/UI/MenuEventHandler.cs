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
            var layerShiftHereMI = FindMenuItemByName("layerShiftHereMI");
            var layerMirrorMI = FindMenuItemByName("layerMirrorMI");
            var layerMakePreviousInvisibleMI = FindMenuItemByName("layerMakePreviousInvisibleMI");
            var framesDeleteBetweenMarksMI = FindMenuItemByName("framesDeleteBetweenMarksMI");
            var framesDeleteOutsideOfMarksMI = FindMenuItemByName("framesDeleteOutsideOfMarksMI");
            var layerConvertToBitmapMI = FindMenuItemByName("layerConvertToBitmapMI");

            layerRestoreRatioMI.Click += (sender, args) => CheckNullActionUpdate(()=> _mf.GifferC.RestoreRatio(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode));
            layerSnapToFrameMI.Click += (sender, args) => CheckNullActionUpdate(()=> _mf.GifferC.SnapLayerToFrame(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode));
            layerFlattenMI.Click += LayerFlattenMI_Click;
            framesReverseMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.ReverseFrames());
            framesAddReversedMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddReversedFrames());
            layerAddTextMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddLayerFromKey(Keys.T));
            layerAddShapeMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddLayerFromKey(Keys.B));
            layerShiftHereMI.Click += LayerShiftHereMI_Click;
            layerMirrorMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.Mirror(_mf.SFI, _mf.SLI));
            layerMakePreviousInvisibleMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.MakePreviousLayersInvisible(_mf.SFI, _mf.SLI));
            framesDeleteBetweenMarksMI.Click += FramesDeleteBetweenMarksMI_Click;
            framesDeleteOutsideOfMarksMI.Click += FramesDeleteOutsideOfMarksMI_Click;
            layerConvertToBitmapMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.ConvertLayerToBitmap(_mf.SFI, _mf.SLI));
        }

        private void FramesDeleteOutsideOfMarksMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            if(_mf.GifferC.DeleteFramesOutsideOfMarks(_mf.Marks, _mf.AskBeforeFrameDelete))
            {
                _mf.MainTimelineSlider.ClearMarks();
                _mf.CompleteUIUpdate();
            }
        }

        private void FramesDeleteBetweenMarksMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            if(_mf.GifferC.DeleteFramesBetweenMarks(_mf.Marks, _mf.AskBeforeFrameDelete))
            {
                _mf.MainTimelineSlider.ClearMarks();
                _mf.CompleteUIUpdate();
            }
        }

        private void LayerFlattenMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.Flatten();
            _mf.CompleteUIUpdate(false, true);
        }

        private void LayerShiftHereMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            if(_mf.Marks.Count != 1)
            {
                MessageBox.Show("There must be exactly one mark for this to work!");
                return;
            }
            int shiftValue = _mf.Marks[0] - _mf.SFI ;
            _mf.GifferC.ShiftLayer(_mf.SFI, _mf.SLI, shiftValue);
            _mf.CompleteUIUpdate();
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

        private void CheckNullActionUpdate(Action action)
        {
            if (CheckNull()) return;
            action();
            _mf.CompleteUIUpdate();
        }
        private bool CheckNull()
        {
            return _mf.GifferC == null;
        }
    }
}
