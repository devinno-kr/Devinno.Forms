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
    public partial class DvSerialPortSetting : DvForm
    {
        #region Constructor
        public DvSerialPortSetting()
        {
            InitializeComponent();

            Fixed = true;

            #region ComboBox
            cmbBaudrate.Items.AddRange(new int[] { 4800, 9600, 19200, 38400, 57600, 115200 }.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
            cmbDataBit.Items.AddRange(new int[] { 5, 6, 7, 8 }.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
            cmbParity.Items.AddRange(new Parity[] { Parity.None, Parity.Odd, Parity.Even }.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
            cmbStopBit.Items.AddRange(new StopBits[] { StopBits.None, StopBits.One, StopBits.Two }.Select(x => new ComboBoxItem(x.ToString()) { Tag = x }));
            #endregion
            #region Event
            btnOK.MouseUp += (o, s) => { if (ValidCheck()) DialogResult = System.Windows.Forms.DialogResult.OK; };
            btnCancel.MouseUp += (o, s) => { DialogResult = System.Windows.Forms.DialogResult.Cancel; };
            #endregion
        }
        #endregion

        #region Method
        #region ValidCheck
        bool ValidCheck()
        {
            if (layout.RowCount == 7)
                return cmbPort.SelectedIndex != -1 && cmbBaudrate.SelectedIndex != -1 && cmbDataBit.SelectedIndex != -1 && cmbParity.SelectedIndex != -1 && cmbStopBit.SelectedIndex != -1;
            else
                return cmbPort.SelectedIndex != -1 && cmbBaudrate.SelectedIndex != -1;
        }
        #endregion
        #region ShowSerialPortSetting
        public SerialPortSetting ShowSerialPortSetting(SerialPortSetting v = null)
        {
            Theme = GetCallerFormTheme() ?? Theme;
            SerialPortSetting ret = null;
            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(300 * f), Convert.ToInt32((40 + 10 + (36 * 6) + 10) * f) + 10);
            #endregion

            #region UI

            layout.SuspendLayout();
            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));

            layout.RowStyles.Clear();
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.65F));

            layout.Controls.Clear();
            layout.Controls.Add(this.cmbStopBit, 1, 4);
            layout.Controls.Add(this.cmbParity, 1, 3);
            layout.Controls.Add(this.cmbDataBit, 1, 2);
            layout.Controls.Add(this.cmbBaudrate, 1, 1);
            layout.Controls.Add(this.lblPort, 0, 0);
            layout.Controls.Add(this.lblBaudrate, 0, 1);
            layout.Controls.Add(this.lblDataBit, 0, 2);
            layout.Controls.Add(this.lblParity, 0, 3);
            layout.Controls.Add(this.lblStopBit, 0, 4);
            layout.Controls.Add(this.btnOK, 1, 6);
            layout.Controls.Add(this.btnCancel, 3, 6);
            layout.Controls.Add(this.cmbPort, 1, 0);
            layout.ResumeLayout();
            #endregion
            #region ComboBox : Port
            var lsPort = SerialPort.GetPortNames().Select(x => new ComboBoxItem(x.Split('\0').FirstOrDefault().ToString()) { Tag = x.Split('\0').FirstOrDefault() });
            cmbPort.Items.Clear();
            cmbPort.Items.AddRange(lsPort);
            #endregion
            #region Set
            if (v != null)
            {
                cmbPort.SelectedIndex = cmbPort.Items.IndexOf(cmbPort.Items.Where(x => (string)x.Tag == v.Port).FirstOrDefault());
                cmbBaudrate.SelectedIndex = cmbBaudrate.Items.IndexOf(cmbBaudrate.Items.Where(x => (int)x.Tag == v.Baudrate).FirstOrDefault());
                cmbDataBit.SelectedIndex = cmbDataBit.Items.IndexOf(cmbDataBit.Items.Where(x => (int)x.Tag == v.DataBit).FirstOrDefault());
                cmbParity.SelectedIndex = cmbParity.Items.IndexOf(cmbParity.Items.Where(x => (Parity)x.Tag == v.Parity).FirstOrDefault());
                cmbStopBit.SelectedIndex = cmbStopBit.Items.IndexOf(cmbStopBit.Items.Where(x => (StopBits)x.Tag == v.StopBit).FirstOrDefault());
            }
            else
            {
                cmbPort.SelectedIndex = 0;
                cmbBaudrate.SelectedIndex = cmbBaudrate.Items.Count - 1;
                cmbDataBit.SelectedIndex = cmbDataBit.Items.Count - 1;
                cmbParity.SelectedIndex = 0;
                cmbStopBit.SelectedIndex = 1;
            }

            cmbPort.ItemHeight = cmbBaudrate.ItemHeight = cmbDataBit.ItemHeight = cmbParity.ItemHeight = cmbStopBit.ItemHeight = cmbPort.Height;
            #endregion
            #region ShowDialog
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = new SerialPortSetting()
                {
                    Port = (string)cmbPort.Items[cmbPort.SelectedIndex].Tag,
                    Baudrate = (int)cmbBaudrate.Items[cmbBaudrate.SelectedIndex].Tag,
                    DataBit = (int)cmbDataBit.Items[cmbDataBit.SelectedIndex].Tag,
                    Parity = (Parity)cmbParity.Items[cmbParity.SelectedIndex].Tag,
                    StopBit = (StopBits)cmbStopBit.Items[cmbStopBit.SelectedIndex].Tag
                };
            }
            #endregion
            return ret;
        }
        #endregion
        #region ShowSimpleSerialPortSetting
        public SerialPortSetting ShowSimpleSerialPortSetting(SerialPortSetting v = null)
        {
            Theme = GetCallerFormTheme() ?? Theme;

            SerialPortSetting ret = null;
            #region DPI Size
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            foreach (var c in layout.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(300 * f), Convert.ToInt32((40 + 10 + (36 * 3) + 10) * f) + 10);
            #endregion
            #region UI
            layout.SuspendLayout();

            layout.ColumnStyles.Clear();
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));

            layout.RowStyles.Clear();
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));

            layout.Controls.Clear();
            layout.Controls.Add(this.cmbPort, 1, 0);
            layout.Controls.Add(this.lblPort, 0, 0);
            layout.Controls.Add(this.cmbBaudrate, 1, 1);
            layout.Controls.Add(this.lblBaudrate, 0, 1);
            layout.Controls.Add(this.btnOK, 1, 3);
            layout.Controls.Add(this.btnCancel, 3, 3);

            layout.ResumeLayout();
            #endregion
            #region ComboBox : Port
            var lsPort = SerialPort.GetPortNames().Select(x => new ComboBoxItem(x.Split('\0').FirstOrDefault().ToString()) { Tag = x.Split('\0').FirstOrDefault() });
            cmbPort.Items.Clear();
            cmbPort.Items.AddRange(lsPort);
            #endregion
            #region Set
            if (v != null)
            {
                cmbPort.SelectedIndex = cmbPort.Items.IndexOf(cmbPort.Items.Where(x => (string)x.Tag == v.Port).FirstOrDefault());
                cmbBaudrate.SelectedIndex = cmbBaudrate.Items.IndexOf(cmbBaudrate.Items.Where(x => (int)x.Tag == v.Baudrate).FirstOrDefault());
                cmbDataBit.SelectedIndex = cmbDataBit.Items.IndexOf(cmbDataBit.Items.Where(x => (int)x.Tag == v.DataBit).FirstOrDefault());
                cmbParity.SelectedIndex = cmbParity.Items.IndexOf(cmbParity.Items.Where(x => (Parity)x.Tag == v.Parity).FirstOrDefault());
                cmbStopBit.SelectedIndex = cmbStopBit.Items.IndexOf(cmbStopBit.Items.Where(x => (StopBits)x.Tag == v.StopBit).FirstOrDefault());
            }
            else
            {
                cmbPort.SelectedIndex = 0;
                cmbBaudrate.SelectedIndex = cmbBaudrate.Items.Count - 1;
                cmbDataBit.SelectedIndex = cmbDataBit.Items.Count - 1;
                cmbParity.SelectedIndex = 0;
                cmbStopBit.SelectedIndex = 1;
            }

            cmbPort.ItemHeight = cmbBaudrate.ItemHeight = cmbDataBit.ItemHeight = cmbParity.ItemHeight = cmbStopBit.ItemHeight = cmbPort.Height;
            #endregion
            #region ShowDialog
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = new SerialPortSetting()
                {
                    Port = (string)cmbPort.Items[cmbPort.SelectedIndex].Tag,
                    Baudrate = (int)cmbBaudrate.Items[cmbBaudrate.SelectedIndex].Tag,
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
