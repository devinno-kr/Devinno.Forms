﻿using Devinno.Forms.Icons;
using Devinno.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms
{
    public class DVLIB
    {
        public static void Preload()
        {
            var v = ResourceTool.volumemask;
            var b = FA.Valid("fa-layer-group");
        }
    }
}


/*
    Container
        DvNavigation
        DvTabControl

    Control
        (DvContentView)
        (DvContentGrid)
       
        DvLineGraph
        DvTimeGraph
        DvTrendGraph

        DvTreeView
        DvToolBox

        DvDataGrid

    Dialog
        DvSelectorDialog
        DvPropertiesDialog
        DvInputBox
        DvKeyboard
        DvSerialPortSetting


*/