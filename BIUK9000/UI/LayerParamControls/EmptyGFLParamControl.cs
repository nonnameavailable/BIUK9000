using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.LayerParamControls
{
    public partial class EmptyGFLParamControl : UserControl, IGFLParamControl
    {
        public EmptyGFLParamControl()
        {
            InitializeComponent();
        }

        public event EventHandler ParamsChanged;

        public void LoadParams(GFL gfl)
        {
            
        }

        public void SaveParams(GFL gfl)
        {
            
        }
    }
}
