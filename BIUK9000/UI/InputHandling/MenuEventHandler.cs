using BIUK9000.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.MyGraphics.Effects;

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

            var layerAddTextMI = _menu.Items.Find("layerAddTextMI", true)[0];
            var layerAddShapeMI = _menu.Items.Find("layerAddShapeMI", true)[0];
            var layerFlattenMI = _menu.Items.Find("layerFlattenMI", true)[0];
            var layerSnapToFrameMI = _menu.Items.Find("layerSnapToFrameMI", true)[0];
            var layerRestoreRatioMI = _menu.Items.Find("layerRestoreRatioMI", true)[0];
            var layerShiftHereMI = _menu.Items.Find("layerShiftHereMI", true)[0];
            var layerMirrorMI = _menu.Items.Find("layerMirrorMI", true)[0];
            var layerMakePreviousInvisibleMI = _menu.Items.Find("layerMakePreviousInvisibleMI", true)[0];
            var layerConvertToBitmapMI = _menu.Items.Find("layerConvertToBitmapMI", true)[0];
            var animationLerpLineMI = _menu.Items.Find("animationLerpLineMI", true)[0];
            var animationLerpTraceMI = _menu.Items.Find("animationLerpTraceMI", true)[0];
            var animationMarkMI = _menu.Items.Find("animationMarkMI", true)[0];

            var effectsHalftoneMI = _menu.Items.Find("effectsHalftoneMI", true)[0];


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
            layerMirrorMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.Mirror(_mf.SFI, _mf.SLI, _mf.ApplyParamsMode));
            layerMakePreviousInvisibleMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.MakePreviousLayersInvisible(_mf.SFI, _mf.SLI));
            layerConvertToBitmapMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.ConvertLayerToBitmap(_mf.SFI, _mf.SLI));

            animationMarkMI.Click += AnimationMarkMI_Click;
            animationLerpLineMI.Click += AnimationLerpLineMI_Click;
            animationLerpTraceMI.Click += AnimationLerpTraceMI_Click;

            effectsHalftoneMI.Click += (sender, args) => CheckNullActionUpdate(() => _mf.GifferC.ApplyEffect(EffectType.Halftone));
        }

        private void AnimationLerpLineMI_Click(object sender, EventArgs e)
        {
            _mf.GifferC.LerpExecute(_mf.Marks, _mf.SLI);
            _mf.MouseTrace().Clear();
        }
        private void AnimationLerpTraceMI_Click(object sender, EventArgs e)
        {
            _mf.GifferC.LerpExecute(_mf.Marks, _mf.SLI, _mf.MouseTrace());
            _mf.MouseTrace().Clear();
        }

        private void AnimationMarkMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            PromptForm pf = new()
            {
                NudDescription = "Mark spacing"
            };
            pf.ShowDialog();
            if (pf.DialogResult == DialogResult.OK)
            {
                _mf.MarkEveryNthFrame((int)pf.NudValue);
            }
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
            if(_mf.GifferC.DeleteFramesBetweenMarksOrSelected(_mf.Marks, _mf.AskBeforeFrameDelete))
            {
                _mf.MainTimelineSlider.ClearMarks();
                _mf.CompleteUIUpdate();
            }
        }

        private void LayerFlattenMI_Click(object sender, EventArgs e)
        {
            if (CheckNull()) return;
            _mf.GifferC.Flatten();
            _mf.CompleteUIUpdate();
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
