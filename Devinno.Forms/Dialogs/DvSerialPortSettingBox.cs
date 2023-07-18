using Devinno.Communications.Setting;
using Devinno.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvSerialPortSettingBox : DvForm
    {
        #region Properties
        public DvButton ButtonOK => btnOk;
        public DvButton ButtonCancel => btnCancel;
        public DvValueInputCombo InputPort => inPort;
        public DvValueInputCombo InputBaudrate => inBaudrate;
        public DvValueInputCombo InputData => inDataBit;
        public DvValueInputCombo InputParity => inParityBit;
        public DvValueInputCombo InputStop => inStopBit;
        #endregion

        #region Constructor
        public DvSerialPortSettingBox()
        {
            InitializeComponent();

            #region ComboBox
            inBaudrate.Items.AddRange(new int[] { 4800, 9600, 19200, 38400, 57600, 115200 }.Select(x => new TextIcon { Text = x.ToString(), Tag = x }));
            inDataBit.Items.AddRange(new int[] { 5, 6, 7, 8 }.Select(x => new TextIcon { Text = x.ToString(), Tag = x }));
            inParityBit.Items.AddRange(new Parity[] { Parity.None, Parity.Odd, Parity.Even }.Select(x => new TextIcon { Text = x.ToString(), Tag = x }));
            inStopBit.Items.AddRange(new StopBits[] { StopBits.None, StopBits.One, StopBits.Two }.Select(x => new TextIcon { Text = x.ToString(), Tag = x }));
            #endregion
            #region Event
            btnOk.MouseUp += (o, s) => { if (ValidCheck()) DialogResult = System.Windows.Forms.DialogResult.OK; };
            btnCancel.MouseUp += (o, s) => { DialogResult = System.Windows.Forms.DialogResult.Cancel; };
            #endregion
        }
        #endregion

        #region Method
        #region ValidCheck
        bool ValidCheck()
        {
            if (tpnl.RowCount == 6)
                return inPort.SelectedIndex != -1 && inBaudrate.SelectedIndex != -1 && inDataBit.SelectedIndex != -1 && inParityBit.SelectedIndex != -1 && inStopBit.SelectedIndex != -1;
            else
                return inPort.SelectedIndex != -1 && inBaudrate.SelectedIndex != -1;
        }
        #endregion
        #region ShowSerialPortSetting
        public SerialPortSetting ShowSerialPortSetting(SerialPortSetting v = null)
        {
            Theme = GetCallerFormTheme();

            SerialPortSetting ret = null;

            #region Size
            this.Size = new Size(300, 40 + 10 + (39 * 5 + 10) + 36 + 10);
            #endregion
            #region UI
            tpnl.SuspendLayout();
           
            tpnl.RowStyles.Clear();
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            tpnl.Controls.Clear();
            tpnl.Controls.Add(this.inPort, 0, 0);
            tpnl.Controls.Add(this.inBaudrate, 0, 1);
            tpnl.Controls.Add(this.inDataBit, 0, 2);
            tpnl.Controls.Add(this.inParityBit, 0, 3);
            tpnl.Controls.Add(this.inStopBit, 0, 4);
            tpnl.ResumeLayout();
            #endregion
            #region ComboBox : Port
            var lsPort = SerialPort.GetPortNames().Select(x => new TextIcon { Text = x.Split('\0').FirstOrDefault().ToString(), Tag = x.Split('\0').FirstOrDefault() });
            inPort.Items.Clear();
            inPort.Items.AddRange(lsPort);
            #endregion
            #region Set
            if (v != null)
            {
                inPort.SelectedIndex = inPort.Items.IndexOf(inPort.Items.Where(x => (string)x.Tag == v.Port).FirstOrDefault());
                inBaudrate.SelectedIndex = inBaudrate.Items.IndexOf(inBaudrate.Items.Where(x => (int)x.Tag == v.Baudrate).FirstOrDefault());
                inDataBit.SelectedIndex = inDataBit.Items.IndexOf(inDataBit.Items.Where(x => (int)x.Tag == v.DataBit).FirstOrDefault());
                inParityBit.SelectedIndex = inParityBit.Items.IndexOf(inParityBit.Items.Where(x => (Parity)x.Tag == v.Parity).FirstOrDefault());
                inStopBit.SelectedIndex = inStopBit.Items.IndexOf(inStopBit.Items.Where(x => (StopBits)x.Tag == v.StopBit).FirstOrDefault());
            }
            else
            {
                inPort.SelectedIndex = 0;
                inBaudrate.SelectedIndex = inBaudrate.Items.Count - 1;
                inDataBit.SelectedIndex = inDataBit.Items.Count - 1;
                inParityBit.SelectedIndex = 0;
                inStopBit.SelectedIndex = 1;
            }

            inPort.ItemHeight = inBaudrate.ItemHeight = inDataBit.ItemHeight = inParityBit.ItemHeight = inStopBit.ItemHeight = inPort.Height;
            #endregion
            #region ShowDialog
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = new SerialPortSetting()
                {
                    Port = (string)inPort.Items[inPort.SelectedIndex].Tag,
                    Baudrate = (int)inBaudrate.Items[inBaudrate.SelectedIndex].Tag,
                    DataBit = (int)inDataBit.Items[inDataBit.SelectedIndex].Tag,
                    Parity = (Parity)inParityBit.Items[inParityBit.SelectedIndex].Tag,
                    StopBit = (StopBits)inStopBit.Items[inStopBit.SelectedIndex].Tag
                };
            }
            #endregion
            return ret;
        }
        #endregion
        #region ShowSimpleSerialPortSetting
        public SerialPortSetting ShowSimpleSerialPortSetting(SerialPortSetting v = null)
        {
            Theme = GetCallerFormTheme();

            SerialPortSetting ret = null;
            #region Size
            this.Size = new Size(300, 40 + 10 + (39 * 2 + 10) + 36 + 10);
            #endregion
            #region UI
            tpnl.SuspendLayout();

            tpnl.RowStyles.Clear();
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 39));
            tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            tpnl.Controls.Clear();
            tpnl.Controls.Add(this.inPort, 0, 0);
            tpnl.Controls.Add(this.inBaudrate, 0, 1);

            tpnl.ResumeLayout();
            #endregion
            #region ComboBox : Port
            var lsPort = SerialPort.GetPortNames().Select(x => new TextIcon { Text = x.Split('\0').FirstOrDefault().ToString(), Tag = x.Split('\0').FirstOrDefault() });
            inPort.Items.Clear();
            inPort.Items.AddRange(lsPort);
            #endregion
            #region Set
            if (v != null)
            {
                inPort.SelectedIndex = inPort.Items.IndexOf(inPort.Items.Where(x => (string)x.Tag == v.Port).FirstOrDefault());
                inBaudrate.SelectedIndex = inBaudrate.Items.IndexOf(inBaudrate.Items.Where(x => (int)x.Tag == v.Baudrate).FirstOrDefault());
                inDataBit.SelectedIndex = inDataBit.Items.IndexOf(inDataBit.Items.Where(x => (int)x.Tag == v.DataBit).FirstOrDefault());
                inParityBit.SelectedIndex = inParityBit.Items.IndexOf(inParityBit.Items.Where(x => (Parity)x.Tag == v.Parity).FirstOrDefault());
                inStopBit.SelectedIndex = inStopBit.Items.IndexOf(inStopBit.Items.Where(x => (StopBits)x.Tag == v.StopBit).FirstOrDefault());
            }
            else
            {
                inPort.SelectedIndex = 0;
                inBaudrate.SelectedIndex = inBaudrate.Items.Count - 1;
                inDataBit.SelectedIndex = inDataBit.Items.Count - 1;
                inParityBit.SelectedIndex = 0;
                inStopBit.SelectedIndex = 1;
            }

            inPort.ItemHeight = inBaudrate.ItemHeight = inDataBit.ItemHeight = inParityBit.ItemHeight = inStopBit.ItemHeight = inPort.Height;
            #endregion
            #region ShowDialog
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = new SerialPortSetting()
                {
                    Port = (string)inPort.Items[inPort.SelectedIndex].Tag,
                    Baudrate = (int)inBaudrate.Items[inBaudrate.SelectedIndex].Tag,
                    DataBit = 8,
                    Parity = Parity.None,
                    StopBit = StopBits.One
                };
            }
            #endregion
            return ret;
        }
        #endregion
        #endregion
    }
}
