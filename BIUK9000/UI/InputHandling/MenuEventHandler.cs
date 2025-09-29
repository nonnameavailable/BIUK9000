using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.InputHandling
{
    class MenuEventHandler
    {
        private MainForm _mf;
        private MenuStrip _menu;
        public MenuEventHandler(MainForm mainForm)
        {
            _mf = mainForm;
            _menu = _mf.MainMenu;
            var framesReverseMI = _menu.Items.Find("framesReverseMI", true)[0];
            var framesAddReversedMI = _menu.Items.Find("framesAddReversedMI", true)[0];
            var framesDeleteBetweenMarksMI = _menu.Items.Find("framesDeleteBetweenMarksMI", true)[0];
            var framesDeleteOutsideOfMarksMI = _menu.Items.Find("framesDeleteOutsideOfMarksMI", true)[0];
            var framesDeleteDuplicatesMI = _menu.Items.Find("framesDeleteDuplicatesMI", true)[0];

            //var layerAddTextMI = _menu.Items.Find("layerAddTextMI", true)[0];
            var layerAddTextMI = _menu.Items.Find("layerAddTextMI", true)[0];
            var layerAddShapeMI = _menu.Items.Find("layerAddShapeMI", true)[0];
            var layerFlattenMI = _menu.Items.Find("layerFlattenMI", true)[0];
            var layerSnapToFrameMI = _menu.Items.Find("layerSnapToFrameMI", true)[0];
            var layerRestoreRatioMI = _menu.Items.Find("layerRestoreRatioMI", true)[0];
            var layerShiftHereMI = _menu.Items.Find("layerShiftHereMI", true)[0];
            var layerMirrorMI = _menu.Items.Find("layerMirrorMI", true)[0];
            var layerMakePreviousInvisibleMI = _menu.Items.Find("layerMakePreviousInvisibleMI", true)[0];
            var layerConvertToBitmapMI = _menu.Items.Find("layerConvertToBitmapMI", true)[0];


            framesReverseMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.ReverseFrames(_mf.Marks));
            framesAddReversedMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddReversedFrames());
            framesDeleteBetweenMarksMI.Click += FramesDeleteBetweenMarksMI_Click;
            framesDeleteOutsideOfMarksMI.Click += FramesDeleteOutsideOfMarksMI_Click;
            framesDeleteDuplicatesMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.DeleteDuplicateFrames());

            layerRestoreRatioMI.Click += (sender, args) => CheckNullActionUpdate(()=> _mf.GifferC.RestoreRatio(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode));
            layerSnapToFrameMI.Click += (sender, args) => CheckNullActionUpdate(()=> _mf.GifferC.SnapLayerToFrame(_mf.SFI, _mf.SLI, _mf.MainControlsPanel.SelectedApplyParamsMode));
            layerFlattenMI.Click += LayerFlattenMI_Click;
            layerAddTextMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddLayerFromKey(Keys.T));
            layerAddShapeMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.AddLayerFromKey(Keys.B));
            layerShiftHereMI.Click += LayerShiftHereMI_Click;
            layerMirrorMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.Mirror(_mf.SFI, _mf.SLI, _mf.PaintOnSubsequentFrames));
            layerMakePreviousInvisibleMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.MakePreviousLayersInvisible(_mf.SFI, _mf.SLI));
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
