using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.Helpers;
using BIUK9000.MyGraphics;
using BIUK9000.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIUK9000.GifferComponents;
using BIUK9000.GifferManipulation;

namespace BIUK9000.UI.InputHandling
{
    public class InputHandler
    {
        private readonly InputTranslator _translator;
        private GifferController _controller;
        private readonly MainForm _mainForm;
        private int SFI { get => _controller.SFI; }
        private int SLI { get => _controller.SLI; }

        public InputHandler(InputTranslator translator, GifferController controller, MainForm mainForm)
        {
            _translator = translator;
            _controller = controller;
            _mainForm = mainForm;

            _translator.AddShapeLayer += OnAddShapeLayer;
            _translator.AddTextLayer += OnAddTextLayer;
            _translator.SaveGif += (sender, args) => _mainForm.SaveGiffer();
            _translator.LoadGif += (sender, args) => _mainForm.LoadGiffer();
            _translator.NextFrame += OnNextFrame;
            _translator.PreviousFrame += OnPreviousFrame;
            _translator.NextMark += OnNextMark;
            _translator.PreviousMark += OnPreviousMark;

            _translator.LassoCompleted += OnLassoCompleted;
            _translator.DrawLineCompleted += OnDrawLineCompleted;

            _translator.StartTimer += (sender, args) => _mainForm.StartUpdateTimer();
            _translator.StopTimer += (sender, args) => _mainForm.StopUpdateTimer();
            _translator.ApplyLayerParams += (sender, args) => _mainForm.ApplyLayerParams();

            _translator.DrawLine += OnDrawLine;
            _translator.ReplaceColor += OnReplaceColor;
            _translator.FloodFill += OnFloodFill;
            _translator.Pipette += (sender, args) => _mainForm.SetPaintColor();
            _translator.OverrideLayerCenter += OnOverrideLayerCenter;
            _translator.LassoPointAdded += OnLassoPointAdded;
            _translator.MoveAll += OnMoveAll;
            _translator.MoveLayer += OnMoveLayer;
            _translator.RotateLayer += OnRotateLayer;
            _translator.ResizeFrame += OnResizeFrame;
            _translator.ResizeLayerKeepRatio += OnResizeLayerKeepRatio;
            _translator.ResizeLayerFree += OnResizeLayerFree;

            _translator.DeleteFramesBetweenMarksOrSelected += _translator_DeleteFrames;
            _translator.DeleteFramesOutsideOfMarks += _translator_DeleteFramesOutsideOfMarks;

            _translator.PlaceMark += (sender, args) => _mainForm.MainTimelineSlider.AddMark(_controller.SFI);
        }

        private void _translator_DeleteFramesOutsideOfMarks(object sender, EventArgs e)
        {
            if (_controller.FrameCount > 2)
            {
                _controller.DeleteFramesOutsideOfMarks(_mainForm.Marks, _mainForm.AskBeforeFrameDelete);
                _controller.SFI = _mainForm.MainTimelineSlider.SelectedFrameIndex;
                _mainForm.Marks.Clear();
                CompleteUIUpdate();
            }
        }

        private void _translator_DeleteFrames(object sender, EventArgs e)
        {
            if(_controller.FrameCount > 2)
            {
                _controller.DeleteFramesBetweenMarksOrSelected(_mainForm.Marks, _mainForm.AskBeforeFrameDelete);
                _controller.SFI = _mainForm.MainTimelineSlider.SelectedFrameIndex;
                _mainForm.Marks.Clear();
                CompleteUIUpdate();
            }
        }

        private void OnPreviousMark(object sender, EventArgs e)
        {
            _controller.SFI = _mainForm.PreviousMark();
            CompleteUIUpdate();
        }

        private void OnNextMark(object sender, EventArgs e)
        {
            _controller.SFI = _mainForm.NextMark();
            CompleteUIUpdate();
        }

        private void OnPreviousFrame(object sender, EventArgs e)
        {
            if(_controller.SFI == 0)
            {
                _controller.SFI = _controller.FrameCount - 1;
            } else
            {
                _controller.SFI--;
            }
            CompleteUIUpdate();
        }

        private void OnNextFrame(object sender, EventArgs e)
        {
            if (_controller.SFI == _controller.FrameCount - 1)
            {
                _controller.SFI = 0;
            } else
            {
                _controller.SFI++;
            }
            CompleteUIUpdate();
        }

        private void OnResizeLayerFree(object sender, EventArgs e)
        {
            Point drag = _mainForm.ScaledDragVector();
            _controller.SelectedLayer.Resize(drag.X, drag.Y);
        }

        private void OnResizeLayerKeepRatio(object sender, EventArgs e)
        {
            _controller.SelectedLayer.Resize((int)_mainForm.SizeDifference());
        }

        private void OnResizeFrame(object sender, EventArgs e)
        {
            Point drag = _mainForm.ScaledDragVector();
            _controller.ResizeFrame(drag.X, drag.Y);
            _mainForm.Report($"New gif Width: {_controller.Width}, Height: {_controller.Height}");
        }

        private void OnRotateLayer(object sender, EventArgs e)
        {
            float angleDif = _mainForm.RotationAngleDifference();
            if (angleDif > 180) angleDif -= 360;
            if (angleDif < -180) angleDif += 360;
            _mainForm.RotationChange += angleDif;
            GFL gfl = _controller.SelectedLayer;
            float newRotation = _mainForm.OriginalLayerRotation + _mainForm.RotationChange;
            if (_mainForm.RotationSnap)
            {
                gfl.Rotation = MathHelp.SnappedRotation(newRotation, 5);
            }
            else
            {
                gfl.Rotation = newRotation;
            }
        }

        private void OnMoveLayer(object sender, MouseEventArgs e)
        {
            GFL gfl = _controller.SelectedLayer;
            if (e == null)
            {
                Point drag = _mainForm.ScaledDragVector();
                gfl.MoveFromOBR(drag.X, drag.Y);
                if (_mainForm.PositionSnap) gfl.Position = MathHelp.SnappedPosition(gfl.Position, 10);
            } else
            {
               gfl.Move(e.X, e.Y);
                _mainForm.UpdateMainPictureBox();
            }
            if(_mainForm.ApplyParamsMode != ApplyParamsMode.None)
            {
                _mainForm.ApplyLayerParams();
            }
        }

        private void OnMoveAll(object sender, EventArgs e)
        {
            Point drag = _mainForm.ScaledDragVector();
            _controller.MoveFromOBR(drag.X, drag.Y);
        }

        private void OnLassoPointAdded(object sender, MouseEventArgs e)
        {
            using Graphics g = Graphics.FromImage(_mainForm.MainImage);
            Painter.DrawLinesFromPoints(g, _mainForm.MouseTrace(), [Color.Red, Color.Cyan], 3);
            _mainForm.InvalidatePictureBox();
        }

        private void OnOverrideLayerCenter(object sender, MouseEventArgs e)
        {
            Point mpol = _mainForm.MousePositionOnLayer();
            GFL cgfl = _controller.SelectedLayer;
            double xMult = (double)mpol.X / cgfl.Width;
            double yMult = (double)mpol.Y / cgfl.Height;
            _controller.OverrideLayerCenter(SFI, SLI, xMult, yMult);
            _mainForm.UpdateMainPictureBox();
        }

        private void OnFloodFill(object sender, MouseEventArgs e)
        {
            _controller.FloodFill(SFI, SLI, _mainForm.MousePositionOnLayer(), _mainForm.PaintParams(), _mainForm.ApplyParamsMode);
            _mainForm.UpdateMainPictureBox();
        }

        private void OnReplaceColor(object sender, MouseEventArgs e)
        {
            _controller.ReplaceColor(SFI, SLI, _mainForm.MousePositionOnLayer(), _mainForm.PaintParams(), _mainForm.ApplyParamsMode);
            _mainForm.UpdateMainPictureBox();
        }

        private void OnDrawLine(object sender, MouseEventArgs e)
        {
            using Graphics g = Graphics.FromImage(((BitmapGFL)_controller.SelectedLayer).OriginalBitmap);
            Painter.DrawLine(g, _mainForm.PreviousMousePositionOnLayer(), _mainForm.MousePositionOnLayer(), _mainForm.PaintParams());
        }

        private void OnDrawLineCompleted(object sender, MouseEventArgs e)
        {
            List<Point> points = _mainForm.TraceToMPOL().ToList();
            _controller.DrawLineOnSubsequentFrames(SFI, SLI, points, _mainForm.PaintParams(), _mainForm.ApplyParamsMode);
            _mainForm.UpdateMainPictureBox();
        }

        private void OnLassoCompleted(object sender, MouseEventArgs e)
        {
            Point[] translatedPoints = _mainForm.TraceToMPOL();
            _controller.Lasso(_controller.SFI, _controller.SLI, translatedPoints, _mainForm.LassoParams());
            CompleteUIUpdate();
        }

        private void OnAddShapeLayer(object sender, EventArgs e)
        {
            _controller.AddLayer(new ShapeGFL(_controller.NextLayerID()));
            CompleteUIUpdate();
            _mainForm.SelectNewestLayer();
        }

        private void OnAddTextLayer(object sender, EventArgs e)
        {
            _controller.AddLayer(new TextGFL(_controller.NextLayerID()));
            CompleteUIUpdate();
            _mainForm.SelectNewestLayer();
        }

        private void CompleteUIUpdate()
        {
            _mainForm.UpdateLayersPanel();
            _mainForm.UpdateTimeline();
            _mainForm.UpdateMainPictureBox();
            _mainForm.UpdateUpperControl();
            _mainForm.UpdateHSBPanel();
        }
        private void ActionAndCompleteUpdate(Action action)
        {
            action();
            CompleteUIUpdate();
        }
        public void SetNewController(GifferController controller)
        {
            _controller = controller;
        }
        private static class MathHelp
        {
            public static float SnappedRotation(float rotation, float snapAngle)
            {
                float roundedRotation = (float)(Math.Round(rotation / 90, 0) * 90);
                float mod90 = Math.Abs(rotation % 90);
                if (mod90 > 90 - snapAngle || mod90 < snapAngle)
                {
                    return roundedRotation;
                }
                else
                {
                    return rotation;
                }
            }

            public static OVector SnappedPosition(OVector position, double snapDistance)
            {
                if (position.Magnitude < snapDistance)
                {
                    return new OVector(0, 0);
                }
                else
                {
                    return position;
                }
            }
        }
    }
}
