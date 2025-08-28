using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI.CustomControls;
using BIUK9000.UI.LayerParamControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000.UI.TopControls.LayerParamControls
{
    public class UpperControlManager
    {
        private RecordControl _recordC;
        private PaintControl _paintC;
        public UpperControlManager(PaintControl pc, RecordControl rc)
        {
            _recordC = rc;
            _paintC = pc;
        }
        private static IGFLParamControl GFLParamControl(GFL gfl)
        {
            if(gfl is TextGFL)
            {
                return new TextGFLParamControl();
            }
            else if (gfl is ShapeGFL)
            {
                return new ShapeGFLParamControl();
            }
            else
            {
                return null;
            } 
        }
        public void UpdateUpperControl(MainForm mf)
        {
            if(mf.Mode == Mode.Move)
            {
                Control test = mf.UpperControl;
                if (test != null && (test is TextGFLParamControl || test is ShapeGFLParamControl))
                {
                    test.Dispose();
                }
                mf.UpperControl = null;
                GFL sl = mf.SelectedLayer;
                if (sl == null || sl is BitmapGFL) return;
                IGFLParamControl gflpc = GFLParamControl(sl);
                gflpc.LoadParams(sl);
                gflpc.ParamsChanged += (sender, args) =>
                {
                    gflpc.SaveParams(sl);
                    mf.UpdateMainPictureBox();
                    mf.ApplyLayerParams();
                };
                mf.UpperControl = (Control)gflpc;
            } else if(mf.Mode == Mode.Paint)
            {
                mf.UpperControl = _paintC;
            } else if(mf.Mode == Mode.Record)
            {
                mf.UpperControl = _recordC;
            }
        }
    }
}
