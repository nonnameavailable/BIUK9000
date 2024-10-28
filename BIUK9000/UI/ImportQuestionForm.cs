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
        public const int IMPORT_FRESH = 0;
        public const int IMPORT_AS_LAYERS = 1;
        public const int IMPORT_INSERT = 2;
        public const int FORM_CLOSED = -1;
        public int SelectedImportType {  get; set; }
        //public event EventHandler<ImportEventArgs> UserSelectedImportType;
        public ImportQuestionForm()
        {
            InitializeComponent();
            freshButton.Click += (sender, args) => { SelectedImportType = IMPORT_FRESH; DialogResult = DialogResult.OK; };
            asLayersButton.Click += (sender, args) => { SelectedImportType = IMPORT_AS_LAYERS; DialogResult = DialogResult.OK; };
            insertButton.Click += (sender, args) => { SelectedImportType = IMPORT_INSERT; DialogResult = DialogResult.OK; };
            DialogResult = DialogResult.Cancel;
        }
        public class ImportEventArgs : EventArgs
        {
            public int ImportType {  get; set; }
            public ImportEventArgs(int importType)
            {
                ImportType = importType;
            } 
        }
    }
}
