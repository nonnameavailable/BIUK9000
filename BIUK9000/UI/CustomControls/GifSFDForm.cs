using AnimatedGif;
using GifskiNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.CustomControls
{
    public partial class GifSFDForm : Form
    {
        public ExportLibrary ChosenExportLibrary
        {
            get
            {
                if (gifskiRB.Checked)
                {
                    return ExportLibrary.Gifski;
                }
                else
                {
                    return ExportLibrary.AnimatedGif;
                }
            }
        }
        public GifQuality GifQuality { get => (GifQuality)gifExportModeCBB.SelectedItem; }
        public bool UseGifsicle { get => useGifSicleCB.Checked; }
        public int GifExportLossy { get => (int)GifExportLossyNUD.Value; }
        public int GifExportColors { get => (int)GifExportColorsNUD.Value; }
        public bool CreateFrames { get => createFramesCB.Checked; }
        public string ImageExportFormat { get => ImageExportFormatCBB.Text; }
        public int ImageExportJpegQuality { get => (int)ImageExportJpegQualNUD.Value; }
        public GifSFDForm()
        {
            InitializeComponent();
            gifExportModeCBB.DataSource = Enum.GetValues(typeof(GifQuality));
            gifExportModeCBB.SelectedIndex = 1;
            saveBTN.DialogResult = DialogResult.OK;
            gifskiRB.CheckedChanged += GifskiRB_CheckedChanged;
            animatedGifRB.CheckedChanged += AnimatedGifRB_CheckedChanged;
            animatedGifOptionsGB.Enabled = false;
        }

        private void AnimatedGifRB_CheckedChanged(object sender, EventArgs e)
        {
            if (animatedGifRB.Checked)
            {
                animatedGifOptionsGB.Enabled = true;
            }
        }

        private void GifskiRB_CheckedChanged(object sender, EventArgs e)
        {
            if (gifskiRB.Checked)
            {
                animatedGifOptionsGB.Enabled = false;
            }
        }

        public enum ExportLibrary
        {
            AnimatedGif,
            Gifski
        }
    }
}
