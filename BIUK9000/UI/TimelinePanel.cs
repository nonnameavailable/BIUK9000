using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI
{
    public partial class TimelinePanel : UserControl
    {
        private GifFrameHolder clickedGifFrameHolder;
        public GifFrame ActiveGifFrame { get => clickedGifFrameHolder.HeldGifFrame; }
        public TimelinePanel()
        {
            InitializeComponent();
        }

        public void AddFrame(Bitmap bitmap)
        {
            GifFrameHolder gfh = new GifFrameHolder(bitmap);
            gfh.BMHClicked += Gfh_BMHClicked;
            timelineFLP.Controls.Add(gfh);
        }

        private void Gfh_BMHClicked(object sender, EventArgs e)
        {
            GifFrameHolder gfh = sender as GifFrameHolder;
            if (gfh == clickedGifFrameHolder) return;
            clickedGifFrameHolder?.BMH.Highlight(false);
            gfh.BMH.Highlight(true);
            Form1 f = (Form1)ParentForm;
            f.MainPictureBox.Image?.Dispose();
            f.MainPictureBox.Image = gfh.CompleteBitmap;
            clickedGifFrameHolder = gfh;
            f.MainLayersPanel.DisplayLayers(ActiveGifFrame);
        }

        public void AddGifFrames(Giffer giffer)
        {
            foreach(GifFrame frame in giffer.Frames)
            {
                AddFrame(frame.CompleteBitmap());
            }
        }
    }
}
