﻿using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIUK9000.UI.LayerParamControls
{
    public static class GFLParamsControlFactory
    {
        public static IGFLParamControl GFLParamControl(GFL gfl)
        {
            if(gfl is TextGFL)
            {
                return new TextGFLParamControl();
            }
            else if (gfl is PlainGFL)
            {
                return new PlainGFLParamControl();
            }
            else
            {
                return null;
            } 
        }
    }
}
