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
    public partial class ImportQuestionForm : Form
    {
        public event EventHandler SelectedFresh;
        public event EventHandler SelectedAsLayers;
        public event EventHandler SelectedInsert;
        public event EventHandler SelectedReplace;

        public bool OLayersSpread { get => layerSpreadRB.Checked; }
        public bool OLayersRepeat { get => layerRepeatRB.Checked; }
        public bool OInsertStart { get => insertStartRB.Checked; }
        public bool OInsertEnd { get =>  insertEndRB.Checked; }
        public bool OInsertHere { get => insertHereRB.Checked; }
        public int SpreadCount { get => (int)spreadCountNUD.Value; set => spreadCountNUD.Value = value; }
        public int NewMaxSideLength { get => (int)newMaxSideLengthNUD.Value; private set => newMaxSideLengthNUD.Value = value; }
        public bool ReduceSize { get => reduceSizeCB.Checked; }

        public ImportQuestionForm()
        {
            InitializeComponent();
            freshButton.Click += FreshButton_Click;
            asLayersButton.Click += AsLayersButton_Click;
            insertButton.Click += InsertButton_Click;
            replaceButton.Click += ReplaceButton_Click;

            freshButton.DialogResult = DialogResult.OK;
            asLayersButton.DialogResult = DialogResult.OK;
            insertButton.DialogResult = DialogResult.OK;
            replaceButton.DialogResult = DialogResult.OK;

            NewMaxSideLength = 800;
        }
        public void DisplayOriginalSize(Size size)
        {
            originalSizeLabel.Text = size.Width + "x" + size.Height;
        }
        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            SelectedReplace?.Invoke(this, EventArgs.Empty);
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            SelectedInsert?.Invoke(this, EventArgs.Empty);
        }

        private void AsLayersButton_Click(object sender, EventArgs e)
        {
            SelectedAsLayers?.Invoke(this, EventArgs.Empty);
        }

        private void FreshButton_Click(object sender, EventArgs e)
        {
            SelectedFresh?.Invoke(this, EventArgs.Empty);
        }
        public void SetOnlyFreshMode()
        {
            insertButton.Enabled = false;
            asLayersButton.Enabled = false;
            replaceButton.Enabled = false;
        }
    }
}
