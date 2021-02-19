using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Tools;
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

namespace Sample
{
    public partial class FormMain : DvForm
    {
        Random rnd = new Random();

        DvColorPickerDialog colorPicker = new DvColorPickerDialog();
        DvDateTimePickerDialog dateTimePicker = new DvDateTimePickerDialog();
        DvInputBox inputBox = new DvInputBox();
        DvKeyboard keyboard = new DvKeyboard();
        DvKeypad keypad = new DvKeypad();
        DvKeypadH keypadH = new DvKeypadH();
        DvMessageBox messageBox = new DvMessageBox();
        DvSelectorBox selectorBox = new DvSelectorBox();
        DvSerialPortSetting serialPortSetting = new DvSerialPortSetting();

        public FormMain()
        {
            InitializeComponent();

            tab.TabIcons.Add("tpControl", new DvIcon("fa-cube", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpContainer", new DvIcon("fa-layer-group", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpGraph", new DvIcon("fa-chart-bar", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpTimeGraph", new DvIcon("fa-chart-line", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpDialog", new DvIcon("far fa-window-maximize", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpContents", new DvIcon("fa-cubes", 18, DvTextIconAlignment.TopBottom, 5));
            tab.TabIcons.Add("tpDataGrid", new DvIcon("fa-table", 18, DvTextIconAlignment.TopBottom, 5));

            #region Lamp / Switch
            dvSwitch1.OnOffChanged += (o, s) => dvLamp1.OnOff = dvLamp2.OnOff = dvLamp3.OnOff = dvLamp4.OnOff = dvSwitch1.OnOff;
            #endregion
            #region Knob / Meter / Gauge
            dvKnob1.ValueChanged += (o, s) => dvMeter1.Value = dvGauge1.Value = dvKnob1.Value;
            #endregion
            #region ComboBox / Selector / ListBox / ToolBox / TreeView
            var ls = new List<string>();
            for (int i = 1; i <= 30; i++) ls.Add("Test " + i);

            dvComboBox1.Items.AddRange(ls.Select(x => new ComboBoxItem(x))); dvComboBox1.SelectedIndex = 0;
            dvSelector1.Items.AddRange(ls.Select(x => new TextIconItem() { Text = x })); dvSelector1.SelectedIndex = 0;
            dvListBox1.Items.AddRange(ls.Select(x => new ListBoxItem(x)));

            for (int i = 1; i <= 3; i++)
            {
                var v = new ToolCategoryItem("Category " + i);
                dvToolBox1.Categories.Add(v);

                for (int j = 1; j <= 10; j++)
                    v.Items.Add(new ToolItem("Item " + i + "." + j));
            }

            for (int a = 1; a <= 3; a++)
            {
                var va = new TreeViewNode("Cat " + a);
                dvTreeView1.Nodes.Add(va);

                for (int b = 1; b <= 4; b++)
                {
                    var vb = new TreeViewNode("Sub " + a + "." + b);
                    va.Nodes.Add(vb);

                    for (int c = 1; c <= 4; c++)
                    {
                        var vc = new TreeViewNode("Item " + a + "." + b + "." + c);
                        vb.Nodes.Add(vc);
                    }
                }
            }
            #endregion
            #region ProgressH / ProgressV / SliderH / SliderV
            dvSliderh1.ValueChanged += (o, s) => dvProgressh1.Value = dvSliderh1.Value;
            dvSliderh2.ValueChanged += (o, s) => dvProgressh2.Value = dvSliderh2.Value;
            dvSliderv1.ValueChanged += (o, s) => dvProgressv1.Value = dvSliderv1.Value;
            dvSliderv2.ValueChanged += (o, s) => dvProgressv2.Value = dvSliderv2.Value;
            #endregion
            #region Tabless
            btnTab1.ButtonClick += (o, s) => tabless.SelectedTab = tlp1;
            btnTab2.ButtonClick += (o, s) => tabless.SelectedTab = tlp2;
            btnTab3.ButtonClick += (o, s) => tabless.SelectedTab = tlp3;
            #endregion
            #region BarGraph H / BarGraph V / Circle Graph / Line Graph
            btnGraphRefresh.ButtonClick += (o, s) => GraphSet();

            dvBarGraphh1.Series.Add(new GraphSeries() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson });
            dvBarGraphh1.Series.Add(new GraphSeries() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal });
            dvBarGraphh1.Series.Add(new GraphSeries() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue });

            dvBarGraphv1.Series.Add(new GraphSeries() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson });
            dvBarGraphv1.Series.Add(new GraphSeries() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal });
            dvBarGraphv1.Series.Add(new GraphSeries() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue });

            dvLineGraph1.Series.Add(new GraphSeries() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson });
            dvLineGraph1.Series.Add(new GraphSeries() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal });
            dvLineGraph1.Series.Add(new GraphSeries() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue });

            dvCircleGraph1.Series.Add(new GraphSeries() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson });
            dvCircleGraph1.Series.Add(new GraphSeries() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal });
            dvCircleGraph1.Series.Add(new GraphSeries() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue });

            dvLineGraph1.Scrollable = dvBarGraphv1.Scrollable = dvBarGraphh1.Scrollable = true;
            dvLineGraph1.TouchMode = dvBarGraphv1.TouchMode = dvBarGraphh1.TouchMode = true;
            dvLineGraph1.ValueDraw = dvBarGraphv1.ValueDraw = dvBarGraphh1.ValueDraw = true;
            dvBarGraphv1.GraphMode = dvBarGraphh1.GraphMode = BarGraphMode.LIST;
            dvBarGraphv1.BarSize = dvBarGraphh1.BarSize = 30;
            dvBarGraphv1.BarGap = dvBarGraphh1.BarGap = 20;
            dvBarGraphv1.Gradient = dvBarGraphh1.Gradient = true;
            dvLineGraph1.PointDraw = true;

            GraphSet();
            #endregion
            #region Time Graph / Trend Graph 
            btnTimeGraphRefresh.ButtonClick += (o, s) => TimeGraphSet();

            dvTimeGraph1.Series.Add(new GraphSeries2() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson, Minimum = 0, Maximum = 100 });
            dvTimeGraph1.Series.Add(new GraphSeries2() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal, Minimum = 0, Maximum = 100 });
            dvTimeGraph1.Series.Add(new GraphSeries2() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue, Minimum = 0, Maximum = 100 });
            dvTimeGraph1.TouchMode = true;
            TimeGraphSet();


            btnTrendStart.ButtonClick += (o, s) =>
            {
                dvTrendGraph1.Start<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });
                btnTrendStart.Enabled = !dvTrendGraph1.IsStart;
                btnTrendStop.Enabled = dvTrendGraph1.IsStart;
            };
            btnTrendStop.ButtonClick += (o, s) =>
            {
                dvTrendGraph1.Stop();
                btnTrendStart.Enabled = !dvTrendGraph1.IsStart;
                btnTrendStop.Enabled = dvTrendGraph1.IsStart;
            };

            dvTrendGraph1.Series.Add(new GraphSeries2() { Name = "Cpp", Alias = "C++", SeriesColor = Color.Crimson, Minimum = 0, Maximum = 100 });
            dvTrendGraph1.Series.Add(new GraphSeries2() { Name = "Java", Alias = "Java", SeriesColor = Color.Teal, Minimum = 0, Maximum = 100 });
            dvTrendGraph1.Series.Add(new GraphSeries2() { Name = "CSharp", Alias = "C#", SeriesColor = Color.DodgerBlue, Minimum = 0, Maximum = 100 });
            dvTrendGraph1.TouchMode = true;
            dvTrendGraph1.Interval = 10;
            dvTrendGraph1.MaximumXScale = TimeSpan.FromSeconds(10);
            dvTrendGraph1.XScale = TimeSpan.FromSeconds(3);
            dvTrendGraph1.XAxisGraduation = TimeSpan.FromSeconds(0.5);

            btnTrendStart.Enabled = !dvTrendGraph1.IsStart;
            btnTrendStop.Enabled = dvTrendGraph1.IsStart;

            sldCpp.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });
            sldJava.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });
            sldCSharp.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });

            #endregion
            #region Dialog
            btnColorPicker.ButtonClick += (o, s) => { Block = true; colorPicker.ShowColorPicker(); Block = false; };

            btnDateTimePicker_DateTime.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowDateTimePicker(); Block = false; };
            btnDateTimePicker_Date.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowDatePicker(); Block = false; };
            btnDateTimePicker_Time.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowTimePicker(); Block = false; };

            btnInputBox.ButtonClick += (o, s) =>
            {
                Block = true;
                var dic = new Dictionary<string, InputBoxInfo>();
                dic.Add("PortName", new InputBoxInfo() { Items = SerialPort.GetPortNames().Select(x => new ComboBoxItem(x)).ToList() });
                dic.Add("Baudrate", new InputBoxInfo() { Items = new int[] { 4800, 9600, 19200, 38400, 57600, 115200 }.Select(x => new ComboBoxItem(x.ToString())).ToList() });
                inputBox.ShowInputBox<Data3>("입력", null, dic);  
                Block = false;
            };

            btnKeyboard.ButtonClick += (o, s) => { Block = true; keyboard.ShowKeyboard("키보드"); Block = false; };

            btnKeypad_Int.ButtonClick += (o, s) => { Block = true; keypad.ShowKeypad("키패드"); Block = false; };
            btnKeypad_Float.ButtonClick += (o, s) => { Block = true; keypad.ShowKeypadEx("키패드"); Block = false; };
            btnKeypad_Password.ButtonClick += (o, s) => { Block = true; keypad.ShowPassword("키패드"); Block = false; };

            btnKeypadH_Int.ButtonClick += (o, s) => { Block = true; keypadH.ShowKeypad("키패드"); Block = false; };
            btnKeypadH_Float.ButtonClick += (o, s) => { Block = true; keypadH.ShowKeypadEx("키패드"); Block = false; };
            btnKeypadH_Password.ButtonClick += (o, s) => { Block = true; keypadH.ShowPassword("키패드"); Block = false; };

            btnMessageBox_Ok.ButtonClick += (o, s) => { Block = true; messageBox.ShowMessageBoxOk("타이틀", "메시지"); Block = false; };
            btnMessageBox_OkCancel.ButtonClick += (o, s) => { Block = true; messageBox.ShowMessageBoxOkCancel("타이틀", "메시지"); Block = false; };
            btnMessageBox_YesNo.ButtonClick += (o, s) => { Block = true; messageBox.ShowMessageBoxYesNo("타이틀", "메시지"); Block = false; };
            btnMessageBox_YesNoCancel.ButtonClick += (o, s) => { Block = true; messageBox.ShowMessageBoxYesNoCancel("타이틀", "메시지"); Block = false; };

            var lsSel = new List<TextIconItem>();
            for (int i = 1; i <= 8; i++) lsSel.Add(new TextIconItem() { Text = "Test" + i });
            btnSelectorBox_Selector.ButtonClick += (o, s) => { Block = true; selectorBox.ShowSelector("셀렉터", lsSel); Block = false; };
            btnSelectorBox_Combo.ButtonClick += (o, s) => { Block = true; selectorBox.ShowComboBox("셀렉터", lsSel); Block = false; };
            btnSelectorBox_Radio.ButtonClick += (o, s) => { Block = true; selectorBox.ShowRadioBox("셀렉터", lsSel, 2); Block = false; };
            btnSelectorBox_Check.ButtonClick += (o, s) => { Block = true; selectorBox.ShowCheckBox("셀렉터", lsSel, 2); Block = false; };

            btnSerialPortSetting_Normal.ButtonClick += (o, s) => { Block = true; serialPortSetting.ShowSerialPortSetting(); Block = false; };
            btnSerialPortSetting_Simple.ButtonClick += (o, s) => { Block = true; serialPortSetting.ShowSimpleSerialPortSetting(); Block = false; };
            #endregion
        }

        #region GraphSet
        void GraphSet()
        {
            var ls1 = new List<Data1>();
            var java = 70D;
            var cpp = 50D;
            var csharp = 30D;
            var vcs = new Color[] { Color.Red, Color.DarkOrange, Color.Goldenrod, Color.Green, Color.DodgerBlue, Color.Blue, Color.Violet, Color.Teal, Color.SteelBlue, Color.Crimson, Color.Lime, Color.Khaki };
            for (int y = 2018; y <= 2021; y++)
                for (int m = 1; m <= 12; m++)
                {
                    cpp = MathTool.Constrain(cpp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                    java = MathTool.Constrain(java + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                    csharp = MathTool.Constrain(csharp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);

                    ls1.Add(new Data1() { Name = y + "." + m, Cpp = cpp, CSharp = csharp, Java = java, Color = vcs[m - 1] });
                }

            dvBarGraphh1.SetDataSource<Data1>(ls1);
            dvBarGraphv1.SetDataSource<Data1>(ls1);
            dvLineGraph1.SetDataSource<Data1>(ls1);
            dvCircleGraph1.SetDataSource<Data1>(ls1.GetRange(ls1.Count - 12, 12));
        }
        #endregion
        #region TimeGraphSet
        void TimeGraphSet()
        {
            var ls1 = new List<Data2>();
            var java = 70D;
            var cpp = 50D;
            var csharp = 30D;
            for (var dt = DateTime.Now.Date; dt <= DateTime.Now.Date + TimeSpan.FromHours(5); dt += TimeSpan.FromSeconds(5))
            {
                cpp = MathTool.Constrain(cpp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                java = MathTool.Constrain(java + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                csharp = MathTool.Constrain(csharp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);

                ls1.Add(new Data2() { Time = dt, Cpp = cpp, CSharp = csharp, Java = java });
            }

            dvTimeGraph1.SetDataSource<Data2>(ls1);
        }
        #endregion
    }

    #region class : Data1 
    class Data1 : GraphData
    {
        public override string Name { get; set; }
        public double CSharp { get; set; }
        public double Cpp { get; set; }
        public double Java { get; set; }
    }
    #endregion
    #region class : Data2
    class Data2 : TimeGraphData
    {
        public override DateTime Time { get; set; }
        public double CSharp { get; set; }
        public double Cpp { get; set; }
        public double Java { get; set; }
    }
    #endregion
    #region class : Data3
    class Data3
    {
        public string PortName { get; set; }
        public int Baudrate { get; set; }

        [InputBoxIgnore]
        public int Ignore { get; set; }

        public int Number1 { get; set; }
        public double Number2 { get; set; }
        public string Text { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool OnOff { get; set; }
    }
    #endregion

}