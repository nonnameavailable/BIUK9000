using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
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
            gfh.BitmapHolderClicked += Bmh_BitmapHolderClicked;
            GifFrameHolders.Add(gfh);
            timelineFLP.Controls.Add(gfh);
        }

        private void Bmh_BitmapHolderClicked(object sender, EventArgs e)
        {
            GifFrameHolder gfh = (GifFrameHolder)sender;
            if(ClickedGifFrameHolder != null )
            {
                ClickedGifFrameHolder.Highlight(false);
            }
            gfh.Highlight(true);
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
