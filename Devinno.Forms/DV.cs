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
        DvGroupBox
        DvTab
        DvPanel
        DvSplitterTableLayout
        DvNavigation
        DvTabControl

    Control
        DvColorPicker
        DvComboBox
        DvContentView
        DvContentGrid
        DvDataGrid
        DvDateTimePicker
        DvGauge
        DvInput
        DvLineGraph
        DvListBox
        DvMeter
        DvNumberBox
        DvOnOff
        DvPictureBox
        DvProgressH
        DvProgressV
        DvSliderH
        DvSliderV
        DvSwitch
        DvTimeGraph
        DvToolBox
        DvTreeView
        DvTrendGraph
        DvValueLabel
        DvVolumeKnob

    Dialog
        DvColorPickerDialog
        DvDatetimePickerDialog
        DvSelectorDialog
        DvInputBox
        DvMessageBox
        DvKeyboard
        DvKeypad
        DvSerialPortSetting


*/