using Devinno.Forms.Icons;
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
        DvSplitterTableLayout
        DvNavigation
        DvTabControl

    Control
        DvColorPicker
        DvDateTimePicker
        DvComboBox
        DvContentView
        DvContentGrid
        DvDataGrid
        DvGauge
        DvMeter
        DvInput
        DvLineGraph
        DvTimeGraph
        DvTrendGraph
        DvVolumeKnob
        DvOnOff
        DvSwitch
        DvListBox
        DvNumberBox
        DvToolBox
        DvPictureBox
        DvProgressH
        DvProgressV
        DvSliderH
        DvSliderV
        DvTreeView
        DvValueLabel

    Dialog
        DvColorPickerDialog
        DvDatetimePickerDialog
        DvSelectorDialog
        DvPropertiesDialog
        DvInputBox
        DvMessageBox
        DvKeyboard
        DvSerialPortSetting


*/