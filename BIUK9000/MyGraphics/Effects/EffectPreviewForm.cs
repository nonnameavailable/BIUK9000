using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.MyGraphics.Effects
{
    public partial class EffectPreviewForm : Form
    {
        public Bitmap OriginalBitmap { get => (Bitmap)originalPictureBox.Image; set => originalPictureBox.Image = value; }
        public Bitmap EffectBitmap { get => (Bitmap)effectPictureBox.Image; set => effectPictureBox.Image = value; }
        public EffectPreviewForm()
        {
            InitializeComponent();
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            DialogResult = DialogResult.Cancel;

            FormClosed += EffectPreviewForm_FormClosed;
            Text = "Effect preview";
        }

        private void EffectPreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            OriginalBitmap?.Dispose();
            EffectBitmap?.Dispose();
        }

        private void Report(string message)
        {
            reportLabel.Text = message;
        }
        public void LoadPreview(Bitmap originalBitmap, EffectType effectType, int frameCount)
        {
            OriginalBitmap = new Bitmap(originalBitmap);
            DateTime before = DateTime.Now;
            EffectBitmap = EffectApplicator.BitmapWithEffect(originalBitmap, effectType);
            TimeSpan oneFrameTime = (DateTime.Now - before);
            Report("It will take " + (oneFrameTime * frameCount).ToString("h'h 'm'm 's's'") + " to process all frames.");
        }
    }
}
