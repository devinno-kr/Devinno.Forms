using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public class DvDialogs
    {
        public static DvColorPickerBox ColorBox { get; } = new DvColorPickerBox();
        public static DvDateTimePickerBox DateTimeBox { get; } = new DvDateTimePickerBox();
        public static DvInputBox InputBox { get; } = new DvInputBox();
        public static DvKeyboard Keyboard { get; } = new DvKeyboard();
        public static DvKeypad Keypad { get; } = new DvKeypad();
        public static DvMessageBox MessageBox { get; } = new DvMessageBox();
        public static DvSelectorBox SelectorBox { get; } = new DvSelectorBox();
        public static DvSerialPortSettingBox PortBox { get; } = new DvSerialPortSettingBox();
        public static DvWheelPickerBox WheelBox { get; } = new DvWheelPickerBox();

        public static void Set(bool blank, FormBorderStyle border)
        {
            ColorBox.BlankForm = blank;
            ColorBox.FormBorderStyle = border;

            DateTimeBox.BlankForm = blank;
            DateTimeBox.FormBorderStyle = border;

            InputBox.BlankForm = blank;
            InputBox.FormBorderStyle = border;

            Keyboard.BlankForm = blank;
            Keyboard.FormBorderStyle = border;

            Keypad.BlankForm = blank;
            Keypad.FormBorderStyle = border;

            MessageBox.BlankForm = blank;
            MessageBox.FormBorderStyle = border;

            SelectorBox.BlankForm = blank;
            SelectorBox.FormBorderStyle = border;

            PortBox.BlankForm = blank;
            PortBox.FormBorderStyle = border;

            WheelBox.BlankForm = blank;
            WheelBox.FormBorderStyle = border;
        }
    }
}
