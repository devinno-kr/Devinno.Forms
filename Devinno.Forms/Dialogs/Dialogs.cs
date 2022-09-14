using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.Forms.Dialogs
{
    internal class DvDialogs
    {
        public static DvColorPickerBox ColorBox { get; } = new DvColorPickerBox();
        public static DvDateTimePickerBox DateTimeBox { get; } = new DvDateTimePickerBox();
        public static DvInputBox InputBox { get; } = new DvInputBox();
        public static DvKeyboard Keyboard { get; } = new DvKeyboard();
        public static DvKeypad Keypad { get; } = new DvKeypad();
        public static DvSelectorBox SelectorBox { get; } = new DvSelectorBox();
    }
}
