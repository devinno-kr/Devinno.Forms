using Devinno.Communications.Modbus.RTU;
using Devinno.Communications.Modbus.TCP;
using Devinno.Data;
using Devinno.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormTest : DvForm
    {
        //ModbusRTUMaster mb;
        //ModbusTCPMaster mb;
        //bool[] M = new bool[40];
        //int[] D = new int[10];

        Timer tmr;

        //ModbusRTUSlave mb;
        ModbusTCPSlave mb;
        BitMemories P = new BitMemories("P", new byte[512]);
        BitMemories M = new BitMemories("M", new byte[512]);
        WordMemories C = new WordMemories("C", new byte[8192]);
        WordMemories D = new WordMemories("D", new byte[8192]);

        public FormTest()
        {
            InitializeComponent();

            #region Remark : master
            /*
            //mb = new ModbusRTUMaster { Port = "COM13", Baudrate = 9600 };
            mb = new ModbusTCPMaster { RemoteIP = "172.30.1.123" };
            mb.AutoBitRead_FC1(1, 1, 0x1000, 40);
            mb.AutoWordRead_FC3(2, 1, 0x7000, 10);
            mb.BitReadReceived += (o, s) => { for (int i = 0; i < s.Length; i++) M[i] = s.ReceiveData[i]; };
            mb.WordReadReceived += (o, s) => { for (int i = 0; i < s.Length; i++) D[i] = s.ReceiveData[i]; };
            mb.AutoStart = true;

            btn1.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1000, !M[0]);
            btn2.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1001, !M[1]);
            btn3.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1002, !M[2]);
            btn4.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1003, !M[3]);
            btn5.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1004, !M[4]);
            btn6.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1005, !M[5]);
            btn7.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1006, !M[6]);
            btn8.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1007, !M[7]);

            btnBitWrite.ButtonClick += (o, s) => mb.ManualBitWrite_FC5(10, 1, 0x1000, true);
            btnMultiBitWrite.ButtonClick += (o, s) => mb.ManualMultiBitWrite_FC15(10, 1, 0x1000, new bool[16]);

            btnWordWrite.ButtonClick += (o, s) =>
            {
                mb.ManualWordWrite_FC6(10, 1, 0x7000, 1);
                mb.ManualWordWrite_FC6(10, 1, 0x7001, 2);
                mb.ManualWordWrite_FC6(10, 1, 0x7002, 3);
                mb.ManualWordWrite_FC6(10, 1, 0x7003, 4);
            };
            btnMultiWordWrite.ButtonClick += (o, s) => mb.ManualMultiWordWrite_FC16(10, 1, 0x7000, new int[4]);
            btnWordBitSet.ButtonClick += (o, s) => mb.ManualWordBitSet_FC26(10, 1, 0x7000, 3, true);

            tmr = new Timer { Interval = 10, Enabled = true };
            tmr.Tick += (o, s) =>
            {
                lmp1.OnOff = M[0];
                lmp2.OnOff = M[1];
                lmp3.OnOff = M[2];
                lmp4.OnOff = M[3];
                lmp5.OnOff = M[4];
                lmp6.OnOff = M[5];
                lmp7.OnOff = M[6];
                lmp8.OnOff = M[7];

                lblD0.Text = $"{D[0]}  {D[1]}  {D[2]}  {D[3]}";
            };
            */
            #endregion

            #region Remark : slave
            //mb = new ModbusRTUSlave { Port = "COM13", Baudrate = 115200, Slave = 1 };
            mb = new ModbusTCPSlave { Slave = 1 };
            mb.SocketConnected += (o, s) => Debug.WriteLine("Connected");
            mb.SocketDisconnected += (o, s) => Debug.WriteLine("Disconnected");

            mb.BitAreas.Add(0x0000, P);
            mb.BitAreas.Add(0x1000, M);
            mb.WordAreas.Add(0x6000, C);
            mb.WordAreas.Add(0x7000, D);
            mb.Start();

            btn1.ButtonColor = Color.FromArgb(90, 90, 90);
            btn2.ButtonColor = Color.FromArgb(90, 90, 90);
            btn3.ButtonColor = Color.FromArgb(90, 90, 90);
            btn4.ButtonColor = Color.FromArgb(90, 90, 90);
            btn5.ButtonColor = Color.FromArgb(90, 90, 90);
            btn6.ButtonColor = Color.FromArgb(90, 90, 90);
            btn7.ButtonColor = Color.FromArgb(90, 90, 90);
            btn8.ButtonColor = Color.FromArgb(90, 90, 90);

            btn1.ButtonClick += (o, s) => btn1.ButtonColor = btn1.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn2.ButtonClick += (o, s) => btn2.ButtonColor = btn2.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn3.ButtonClick += (o, s) => btn3.ButtonColor = btn3.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn4.ButtonClick += (o, s) => btn4.ButtonColor = btn4.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn5.ButtonClick += (o, s) => btn5.ButtonColor = btn5.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn6.ButtonClick += (o, s) => btn6.ButtonColor = btn6.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn7.ButtonClick += (o, s) => btn7.ButtonColor = btn7.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);
            btn8.ButtonClick += (o, s) => btn8.ButtonColor = btn8.ButtonColor == Color.FromArgb(90, 90, 90) ? Color.DarkRed : Color.FromArgb(90, 90, 90);

            btnBitWrite.ButtonClick += (o, s) => { P[0] = true; };
            btnMultiBitWrite.ButtonClick += (o, s) => { for (int i = 0; i < 16; i++) P[i] = false; };

            btnWordWrite.ButtonClick += (o, s) => { D[0] = 1; D[1] = 2; D[2] = 3; D[3] = 4; };
            btnMultiWordWrite.ButtonClick += (o, s) => { for (int i = 0; i < 4; i++) D[i] = 0; }; 

            tmr = new Timer { Interval = 10, Enabled = true };
            tmr.Tick += (o, s) =>
            {
                M[0] = btn1.ButtonColor == Color.DarkRed;
                M[1] = btn2.ButtonColor == Color.DarkRed;
                M[2] = btn3.ButtonColor == Color.DarkRed;
                M[3] = btn4.ButtonColor == Color.DarkRed;
                M[4] = btn5.ButtonColor == Color.DarkRed;
                M[5] = btn6.ButtonColor == Color.DarkRed;
                M[6] = btn7.ButtonColor == Color.DarkRed;
                M[7] = btn8.ButtonColor == Color.DarkRed;

                lmp1.OnOff = P[0];
                lmp2.OnOff = P[1];
                lmp3.OnOff = P[2];
                lmp4.OnOff = P[3];
                lmp5.OnOff = P[4];
                lmp6.OnOff = P[5];
                lmp7.OnOff = P[6];
                lmp8.OnOff = P[7];

                lblD0.Text = $"{C[0]}  {C[1]}  {C[2]}  {C[3]}";
            };
            #endregion
        }


    }
}
