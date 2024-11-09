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
        public ImportQuestionForm()
        {
            InitializeComponent();
            freshButton.Click += FreshButton_Click;
            asLayersButton.Click += AsLayersButton_Click;
            insertButton.Click += InsertButton_Click;
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            SelectedInsert?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void AsLayersButton_Click(object sender, EventArgs e)
        {
            SelectedAsLayers?.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void FreshButton_Click(object sender, EventArgs e)
        {
            SelectedFresh?.Invoke(this, EventArgs.Empty);
            Close();
        }
    }
}
