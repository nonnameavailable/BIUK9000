using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.ExtendedControls
{
    public class MyTrackBar : TrackBar
    {
        private List<int> marks = new();
        public List<int> Marks { get => marks; }
        public MyTrackBar() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_PAINT
            if (m.Msg == 0x0F)
            {
                using Graphics lgGraphics = Graphics.FromHwndInternal(m.HWnd);
                OnPaintOver(new PaintEventArgs(lgGraphics, ClientRectangle));
            }
        }
        protected virtual void OnPaintOver(PaintEventArgs e)
        {
            foreach (int i in marks)
            {
                int padding = 13;
                int mp = (int)(i / (double)(Maximum - Minimum) * (Width - padding * 2)) + padding;
                e.Graphics.DrawLine(Pens.Red, mp, 0, mp, Height);
            }
        }
        public bool AddMark(int mark)
        {
            if (mark <= Maximum && Maximum > 0 && mark >= Minimum)
            {
                if (marks.Contains(mark))
                {
                    marks.Remove(mark);
                }
                else
                {
                    marks.Add(mark);
                }
                Invalidate();
                return true;
            }
            else return false;
        }
        public void ClearMarks()
        {
            marks.Clear();
            Invalidate();
        }
    }
}
