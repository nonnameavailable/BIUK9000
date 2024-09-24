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
        private List<GifFrameHolder> GifFrameHolders;
        private GifFrameHolder ClickedGifFrameHolder;
        public TimelinePanel()
        {
            InitializeComponent();
            GifFrameHolders = new();
        }

        public void AddFrame(Bitmap bitmap)
        {
            GifFrameHolder gfh = new GifFrameHolder(bitmap);
            gfh.BMHClicked += Gfh_BMHClicked;
            GifFrameHolders.Add(gfh);
            timelineFLP.Controls.Add(gfh);
        }

        private void Gfh_BMHClicked(object sender, EventArgs e)
        {
            ClickedGifFrameHolder?.BMH.Highlight(false);
            GifFrameHolder gfh = sender as GifFrameHolder;
            gfh.BMH.Highlight(true);
            Form1 f = (Form1)ParentForm;
            f.MainPictureBox.Image = gfh.CompleteBitmap;
            ClickedGifFrameHolder = gfh;
        }

        public void AddGifFrames(Giffer giffer)
        {
            foreach(Bitmap frame in giffer.Frames)
            {
                AddFrame(frame);
            }
        }
    }
}
