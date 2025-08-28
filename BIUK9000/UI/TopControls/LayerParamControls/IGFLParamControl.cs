using BIUK9000.GifferComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.UI.TopControls.LayerParamControls
{
    public interface IGFLParamControl
    {
        void LoadParams(GFL gfl);
        event EventHandler ParamsChanged;
        void SaveParams(GFL gfl);
    }
}
