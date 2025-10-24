using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;
using BIUK9000.UI.LayerParamControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.InputHandling
{
    public class InputTranslator
    {
        public event EventHandler NextFrame, PreviousFrame, NextMark, PreviousMark;
        public event EventHandler SaveGif, LoadGif;
        public event EventHandler AddTextLayer, AddShapeLayer;
        public event EventHandler MoveAll, ResizeLayerKeepRatio, ResizeLayerFree, ResizeFrame, RotateLayer;
        public event MouseEventHandler DrawLine, ReplaceColor, FloodFill, LassoPointAdded, Pipette, OverrideLayerCenter, MoveLayer, ShiftLayer;
        public event MouseEventHandler LassoCompleted, DrawLineCompleted;
        public event EventHandler StartTimer, StopTimer, ApplyLayerParams;
        public event EventHandler DeleteFramesBetweenMarksOrSelected, DeleteFramesOutsideOfMarks;
        public event EventHandler PlaceMark;
        private bool IsShiftDown { get; set; }
        private bool IsCtrlDown { get; set; }
        private bool IsLMBDown { get; set; }
        private bool IsRMBDown { get; set; }
        private bool IsMMBDown { get; set; }

        public InputTranslator()
        {
            IsShiftDown = false;
            IsCtrlDown = false;
            IsLMBDown = false;
            IsRMBDown = false;
            IsMMBDown = false;
        }
        public bool HandleKey(Message m)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_KEYUP = 0x101;
            Keys keyData = (Keys)m.WParam.ToInt32();
            if (m.Msg == WM_KEYDOWN)
            {
                if (keyData == Keys.D)
                {
                    if (IsShiftDown)
                    {
                        NextMark?.Invoke(this, null);
                    }
                    else
                    {
                        NextFrame?.Invoke(this, null);
                    }
                    return true;
                }
                else if (keyData == Keys.A)
                {
                    if (IsShiftDown)
                    {
                        PreviousMark?.Invoke(this, null);
                    }
                    else
                    {
                        PreviousFrame?.Invoke(this, null);
                    }
                    return true;
                }
                else if (keyData == Keys.ShiftKey)
                {
                    IsShiftDown = true;
                    return true;
                }
                else if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = true;
                    return true;
                }
                else if (keyData == Keys.S && IsCtrlDown)
                {
                    SaveGif?.Invoke(this, null);
                    return true;
                }
                else if (keyData == Keys.L && IsCtrlDown)
                {
                    LoadGif?.Invoke(this, null);
                    return true;
                }
                else if (keyData == Keys.T)
                {
                    AddTextLayer?.Invoke(this, null);
                    return true;
                }
                else if (keyData == Keys.B)
                {
                    AddShapeLayer?.Invoke(this, null);
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    int moveValue = IsShiftDown ? 2 : 1;
                    MoveLayer?.Invoke(this, DefaultMouseEventArgs(moveValue, 0));
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    int moveValue = IsShiftDown ? 2 : 1;
                    MoveLayer?.Invoke(this, DefaultMouseEventArgs(-moveValue, 0));
                    return true;
                }
                else if(keyData == Keys.Up)
                {
                    int moveValue = IsShiftDown ? 2 : 1;
                    MoveLayer?.Invoke(this, DefaultMouseEventArgs(0, -moveValue));
                    return true;
                }
                else if(keyData == Keys.Down)
                {
                    int moveValue = IsShiftDown ? 2 : 1;
                    MoveLayer?.Invoke(this, DefaultMouseEventArgs(0, moveValue));
                    return true;
                }
                else if(keyData == Keys.Delete)
                {
                    if (IsShiftDown)
                    {
                        DeleteFramesOutsideOfMarks?.Invoke(this, null);
                        IsShiftDown = false;
                    }
                    else
                    {
                        DeleteFramesBetweenMarksOrSelected?.Invoke(this, null);
                    }
                    return true;
                }
                else if(keyData == Keys.M)
                {
                    PlaceMark?.Invoke(this, null);
                }
            }
            else if (m.Msg == WM_KEYUP)
            {
                if (keyData == Keys.ShiftKey)
                {
                    IsShiftDown = false;
                    return true;
                }
                if (keyData == Keys.ControlKey)
                {
                    IsCtrlDown = false;
                    return true;
                }
            }
            return false;
        }
        public void HandleMouseDown(MouseEventArgs e, Mode mode, PaintControl.PaintTool tool)
        {
            if(!(mode == Mode.Paint && tool == PaintControl.PaintTool.Lasso)) StartTimer?.Invoke(this, EventArgs.Empty);
            if(e.Button == MouseButtons.Left)
            {
                IsLMBDown = true;
            } else if(e.Button == MouseButtons.Right)
            {
                IsRMBDown = true;
            } else if(e.Button == MouseButtons.Middle)
            {
                IsMMBDown = true;
            }

            if (mode == Mode.Paint)
            {
                if (IsLMBDown)
                {
                    if (tool == PaintControl.PaintTool.DrawLine)
                    {
                        DrawLine?.Invoke(this, e);
                    }
                    else if (tool == PaintControl.PaintTool.ReplaceColor)
                    {
                        ReplaceColor?.Invoke(this, e);
                    }
                    else if (tool == PaintControl.PaintTool.FillColor)
                    {
                        FloodFill?.Invoke(this, e);
                    }
                    else if(tool == PaintControl.PaintTool.Lasso)
                    {
                        LassoPointAdded?.Invoke(this, e);
                    }
                }
                else if (IsRMBDown)
                {
                    Pipette?.Invoke(this, e);
                }
            }
            else if (IsCtrlDown && IsShiftDown && IsLMBDown && mode == Mode.Move)
            {
                OverrideLayerCenter?.Invoke(this, e);
            }
        }
        public void HandleMouseUp(MouseEventArgs e, Mode mode, PaintControl.PaintTool tool)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (IsShiftDown)
                {
                    ShiftLayer?.Invoke(this, e);
                }
                IsLMBDown = false;
            }
            else if (e.Button == MouseButtons.Right)
            {
                IsRMBDown = false;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                IsMMBDown = false;
            }

            if (mode == Mode.Paint)
            {
                if (tool == PaintControl.PaintTool.Lasso)
                {
                    LassoCompleted?.Invoke(this, e);
                }
                else if (tool == PaintControl.PaintTool.DrawLine)
                {
                    DrawLineCompleted?.Invoke(this, e);
                }
            }
            if (!IsLMBDown && !IsMMBDown & !IsRMBDown)
            {
                StopTimer?.Invoke(this, EventArgs.Empty);
                if (!IsCtrlDown) ApplyLayerParams?.Invoke(this, EventArgs.Empty);
            }
        }
        public void HandleMouseMove(MouseEventArgs e, Mode mode, PaintControl.PaintTool tool)
        {
            if(mode == Mode.Move)
            {
                if (IsRMBDown || IsLMBDown || IsMMBDown)
                {
                    if (IsLMBDown && !IsRMBDown)
                    {
                        //MOVE
                        if (IsCtrlDown && !IsShiftDown)
                        {
                            //MOVE WHOLE GIF (ALL LAYERS, ALL FRAMES)
                            MoveAll?.Invoke(this, null);
                        }
                        else
                        {
                            //MOVE JUST LAYER
                            MoveLayer?.Invoke(this, null);
                        }
                    }
                    else if (!IsLMBDown && IsRMBDown)
                    {
                        //ROTATE
                        RotateLayer?.Invoke(this, null);
                    }
                    else if (IsMMBDown)
                    {
                        //RESIZE
                        if (IsCtrlDown)
                        {
                            //RESIZE GIF FREE (ALL FRAMES)
                            ResizeFrame?.Invoke(this, null);
                        }
                        else
                        {
                            //RESIZE LAYER
                            if (!IsShiftDown)
                            {
                                //RESIZE LAYER KEEP RATIO
                                ResizeLayerKeepRatio?.Invoke(this, null);
                            }
                            else
                            {
                                //RESIZE LAYER FREE
                                ResizeLayerFree?.Invoke(this, null);
                            }
                        }
                    }
                }
            }
            else if (mode == Mode.Paint)
            {
                if(tool == PaintControl.PaintTool.DrawLine && IsLMBDown)
                {
                    DrawLine?.Invoke(this, e);
                }
                else if(tool == PaintControl.PaintTool.Lasso && IsLMBDown)
                {
                    LassoPointAdded?.Invoke(this, e);
                }
            }
        }
        private MouseEventArgs DefaultMouseEventArgs(int x, int y)
        {
            return new MouseEventArgs(MouseButtons.Left, 1, x, y, 0);
        }
    }
}
