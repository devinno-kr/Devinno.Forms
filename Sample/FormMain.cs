using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMain : DvForm
    {
        DvMessageBox msg = new DvMessageBox();
        DvKeypadH keypad = new DvKeypadH();
        public FormMain()
        {
            InitializeComponent();

            btnOK.ButtonClick += (o, s) => { Block = true; msg.ShowMessageBoxOk("테스트", "테스트입니다."); Block = false; };
            btnOkCancel.ButtonClick += (o, s) => { Block = true; msg.ShowMessageBoxOkCancel("테스트", "테스트입니다."); Block = false; };
            btnYesNo.ButtonClick += (o, s) => { Block = true; msg.ShowMessageBoxYesNo("테스트", "테스트입니다."); Block = false; };
            btnYesNoCancel.ButtonClick += (o, s) => { Block = true; msg.ShowMessageBoxYesNoCancel("테스트", "테스트입니다."); Block = false; };

            btnKeypad.ButtonClick += (o, s) => { Block = true; int? ret = keypad.ShowKeypad((int?)null); if (ret.HasValue) dvLabel1.Text = ret.Value.ToString(); Block = false; };
            btnKeypadEx.ButtonClick += (o, s) => { Block = true; decimal? ret = keypad.ShowKeypadEx((decimal?)null); if (ret.HasValue) dvLabel1.Text = ret.Value.ToString(); Block = false; };
            btnPassword.ButtonClick += (o, s) => { Block = true; string ret = keypad.ShowPassword(); if (ret != null) dvLabel1.Text = ret; Block = false; };
        }
    }
}
