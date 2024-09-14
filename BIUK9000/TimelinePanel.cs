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
        private List<BitmapHolder> bitmapHolders;
        private BitmapHolder ClickedBitmapHolder;
        public TimelinePanel()
        {
            InitializeComponent();
            bitmapHolders = new();
        }

        public void AddImage(Bitmap bitmap)
        {
            BitmapHolder bmh = new BitmapHolder(bitmap);
            bmh.BitmapHolderClicked += Bmh_BitmapHolderClicked;
            bitmapHolders.Add(bmh);
            timelineFLP.Controls.Add(bmh);
        }

        private void Bmh_BitmapHolderClicked(object sender, EventArgs e)
        {
            BitmapHolder bmh = (BitmapHolder)sender;
            if(ClickedBitmapHolder != null )
            {
                ClickedBitmapHolder.Highlight(false);
            }
            bmh.Highlight(true);
            Form1 f = (Form1)ParentForm;
            f.MainPictureBox.Image = bmh.HeldImage;
            ClickedBitmapHolder = bmh;
        }

        public void AddGifFrames(Giffer giffer)
        {
            foreach(Bitmap frame in giffer.Frames)
            {
                AddImage(frame);
            }
        }
    }
}
