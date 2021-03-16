using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
        Timer tmr;

        #region Grid
        int GridMode = 1;
        List<GridItem2> Items2 = new List<GridItem2>();
        #endregion
        #region Image
        public static Bitmap[] pics = new Bitmap[] { new Bitmap(Properties.Resources._1), new Bitmap(Properties.Resources._2), new Bitmap(Properties.Resources._3) };
        #endregion
        #region Dialogs
        DvColorPickerDialog colorPicker = new DvColorPickerDialog();
        DvDateTimePickerDialog dateTimePicker = new DvDateTimePickerDialog();
        DvInputBox inputBox = new DvInputBox() { UseEnterKey = true };
        DvKeyboard keyboard = new DvKeyboard();
        DvKeypad keypad = new DvKeypad();
        DvKeypadH keypadH = new DvKeypadH();
        DvMessageBox messageBox = new DvMessageBox();
        DvSelectorBox selectorBox = new DvSelectorBox();
        DvSerialPortSetting serialPortSetting = new DvSerialPortSetting();
        #endregion

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
            dvTimeGraph1.XAxisGridDraw = true;
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
            dvTrendGraph1.MaximumXScale = TimeSpan.FromSeconds(5);
            dvTrendGraph1.XScale = TimeSpan.FromSeconds(1);
            dvTrendGraph1.XAxisGraduation = TimeSpan.FromSeconds(0.1);
            dvTrendGraph1.XAxisGridDraw = true;

            btnTrendStart.Enabled = !dvTrendGraph1.IsStart;
            btnTrendStop.Enabled = dvTrendGraph1.IsStart;

            sldCpp.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });
            sldJava.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });
            sldCSharp.ValueChanged += (o, s) => dvTrendGraph1.SetData<Data2>(new Data2() { Cpp = sldCpp.Value, Java = sldJava.Value, CSharp = sldCSharp.Value });

            #endregion
            #region Dialog
            var strbul = new StringBuilder();
            strbul.AppendLine("동해물과 백두산이 마르고 닳도록 하느님이 보우하사 우리나라 만세");
            strbul.AppendLine("무궁화 삼천리 화려강산 대한사람 대한으로 길이 보전하세");
            strbul.AppendLine("");
            strbul.AppendLine("남산 위에 저 소나무 철값을 두른듯 바람서리 불변함은 우리기상 일세");
            strbul.AppendLine("무궁화 삼천리 화려강산 대한사람 대한으로 길이 보전하세");
            strbul.AppendLine("");
            strbul.AppendLine("가을 하늘 공활한데 놃고 구름없이 밝은달은 우리 가슴 일편담심 일세");
            strbul.AppendLine("무궁화 삼천리 화려강산 대한사람 대한으로 길이 보전하세");
            strbul.AppendLine("");
            strbul.AppendLine("이 기상과 이 맘으로 충성을 다하여 괴로우나 즐거우나 나라 사랑하세");
            strbul.AppendLine("무궁화 삼천리 화려강산 대한사람 대한으로 길이 보전하세");

            btnColorPicker.ButtonClick += (o, s) => { Block = true; colorPicker.ShowColorPicker("색상 선택"); Block = false; };

            btnDateTimePicker_DateTime.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowDateTimePicker("날짜/시간 입력"); Block = false; };
            btnDateTimePicker_Date.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowDatePicker("날짜 입력"); Block = false; };
            btnDateTimePicker_Time.ButtonClick += (o, s) => { Block = true; dateTimePicker.ShowTimePicker("시간 입력"); Block = false; };

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
            btnMessageBox_YesNoCancel.ButtonClick += (o, s) => { Block = true; messageBox.ShowMessageBoxYesNoCancel("타이틀", strbul.ToString()); Block = false; };

            var lsSel = new List<TextIconItem>();
            for (int i = 1; i <= 8; i++) lsSel.Add(new TextIconItem() { Text = "Test " + i });
            btnSelectorBox_Selector.ButtonClick += (o, s) => { Block = true; selectorBox.ShowSelector("셀렉터", lsSel); Block = false; };
            btnSelectorBox_Combo.ButtonClick += (o, s) => { Block = true; selectorBox.ShowComboBox("셀렉터", lsSel); Block = false; };
            btnSelectorBox_Radio.ButtonClick += (o, s) => { Block = true; selectorBox.ShowRadioBox("셀렉터", lsSel, 2); Block = false; };
            btnSelectorBox_Check.ButtonClick += (o, s) => { Block = true; selectorBox.ShowCheckBox("셀렉터", lsSel, 2); Block = false; };

            btnSerialPortSetting_Normal.ButtonClick += (o, s) => { Block = true; serialPortSetting.ShowSerialPortSetting(); Block = false; };
            btnSerialPortSetting_Simple.ButtonClick += (o, s) => { Block = true; serialPortSetting.ShowSimpleSerialPortSetting(); Block = false; };
            #endregion
            #region ContentView / ContentGrid
            contentView.ScrollDirection = ScrollDirection.Vertical;
            contentView.ContentSize = new Size(100, 100);
            contentView.Gap = 6;
            contentView.AutoArrange = true;
            contentView.TouchAreaSize = 100;
            contentView.TouchMode = true;
            contentView.Selectable = true;
            for (int i = 0; i < 200; i++)
                contentView.Items.Add(new TContent(contentView) { Num = contentView.Items.Count + 1, RowSpan = (i % 10 == 0 || i % 10 == 5) ? 2 : 1, ColSpan = i % 10 == 0 ? 2 : 1 });



            contentGrid.ContentSize = new Size(100, 100);
            contentGrid.Gap = 6;
            contentGrid.AutoArrange = true;
            contentGrid.TouchAreaSize = 100;
            contentGrid.TouchMode = true;
            contentGrid.Selectable = false;
            for (int i = 1; i <= 7; i++) contentGrid.Pages.Add(new DvContentGridPage() { PageName = "Test" + i });
            foreach (var page in contentGrid.Pages)
            {
                var n = contentGrid.Pages.IndexOf(page);
                for (int i = 0; i < 5 * (contentGrid.Pages.IndexOf(page) + 1); i++)
                    page.Items.Add(new TContent(contentGrid) { Num = i + 1, ColSpan = 1, RowSpan = 1 });
            }
            contentGrid.CurrentPageIndex = 0;
            #endregion
            #region DataGrid
            #region actMonth
            var actMonth = new Action(() =>
            {
                GridMode = 1;
                var f = DpiRatio;
                dg.MovingStop();
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                dg.Font = new Font("나눔고딕", 7);
                dg.TextShadow = false;
                dg.RowBevel = true;
                dg.TouchMode = true;
                dg.ScrollMode = ScrollMode.Both;
                dg.RowHeight = dg.ColumnHeight = Convert.ToInt32(30 * f);
                dg.ColumnGroups.Add(new DvDataGridColumn(dg) { Name = "G1", HeaderText = "기본사항", Fixed = true });
                dg.ColumnGroups.Add(new DvDataGridColumn(dg) { Name = "G2", HeaderText = "일일 수집량" });
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", GroupName = "G1", HeaderText = "이름", SizeMode = SizeMode.Pixel, Width = Convert.ToInt32(150 * f), Fixed = true, UseFilter = true, CellType = typeof(DvDataGridLabelCell) });
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "State", GroupName = "G1", HeaderText = "상태", SizeMode = SizeMode.Pixel, Width = Convert.ToInt32(70 * f), Fixed = true, CellType = typeof(DvDataGridLabelCell) });
                for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
                    dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Day" + i, GroupName = "G2", HeaderText = i + "일", SizeMode = SizeMode.Pixel, Width = Convert.ToInt32(70 * f), CellType = typeof(DvDataGridLabelCell) });

                var srow = new DvDataGridSummaryRow(dg);
                var srow2 = new DvDataGridSummaryRow(dg);
                srow.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "합계", ColumnIndex = 0, ColumnSpan = 2 });
                srow.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "", ColumnIndex = 0, ColumnSpan = 1, Visible = false });
                srow2.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "평균", ColumnIndex = 0, ColumnSpan = 2 });
                srow2.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "", ColumnIndex = 0, ColumnSpan = 1, Visible = false });
                for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
                {
                    srow.Cells.Add(new DvDataGridSummarySumCell(dg, srow) { ColumnIndex = 1 + i, ColumnSpan = 1, Format = "N0" });
                    srow2.Cells.Add(new DvDataGridSummaryAverageCell(dg, srow) { ColumnIndex = 1 + i, ColumnSpan = 1, Format = "N0" });
                }
                dg.SummaryRows.Add(srow);
                dg.SummaryRows.Add(srow2);

                var Items = new List<GridItem>();
                for (int i = 0; i <= 100; i++)
                {
                    var lsv = new List<int>();
                    for (int j = 1; j <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); j++) lsv.Add(rnd.Next(0, 100));
                    Items.Add(new GridItem() { Name = "NM" + i, State = "NORMAL", Days = lsv.ToArray() });
                }
                dg.SetDataSource<GridItem>(Items);
            });
            #endregion
            #region actCells
            var actCells = new Action(() =>
            {
                GridMode = 2;
                var f = DpiRatio;
                dg.MovingStop();
                dg.SelectionMode = DvDataGridSelectionMode.SELECTOR;
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                dg.AutoSet = true;
                dg.Font = new Font("나눔고딕", 8);
                dg.TextShadow = false;
                dg.RowBevel = true;
                dg.TouchMode = false;
                dg.ScrollMode = ScrollMode.Vertical;
                dg.RowHeight = dg.ColumnHeight = Convert.ToInt32(30 * f);
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "DeviceName", HeaderText = "장치명", SizeMode = SizeMode.Percent, Width = 15M });
                dg.Columns.Add(new DvDataGridImageColumn(dg) { Name = "DeviceImage", HeaderText = "이미지", SizeMode = SizeMode.Percent, Width = 5M });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Time", HeaderText = "설치일", SizeMode = SizeMode.Percent, Width = 15M, Format = "yyyy.MM.dd" });
                dg.Columns.Add(new DvDataGridTextConverterColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = SizeMode.Percent, Width = 5M, Converter = GetDOW });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Temperature", HeaderText = "온도", SizeMode = SizeMode.Percent, Width =10M, Format = "0.0 ℃" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmT", HeaderText = "온도 알람", SizeMode = SizeMode.Percent, Width = 5M, Simple = false });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Humidity", HeaderText = "습도", SizeMode = SizeMode.Percent, Width = 10M, Format = "0 '%'" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmH", HeaderText = "습도 알람", SizeMode = SizeMode.Percent, Width = 5M, Simple = false });
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "RunText", HeaderText = "구동", SizeMode = SizeMode.Percent, Width = 15M, ButtonText = "구동", IconString = "fa-play" });
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "StopText", HeaderText = "정지", SizeMode = SizeMode.Percent, Width = 15M, ButtonText = "정지", IconString = "fa-stop" });

                var Items = new List<GridItem2>();
                for (int i = 1; i <= 100; i++)
                {
                    Items.Add(new GridItem2()
                    {
                        DeviceName = "DEV" + i,
                        DeviceImage = i % 3 == 0 ? pics[0] : (i % 3 == 1 ? pics[1] : pics[2]),
                        Time = DateTime.Now.Date + TimeSpan.FromDays(i),
                        Humidity = rnd.Next(0, 100),
                        Temperature = rnd.Next(0, 1000) / 10D,
                    });
                }
                Items2 = Items;
                dg.SetDataSource<GridItem2>(Items);
            });
            #endregion
            #region actCells2
            var actCells2 = new Action(() =>
            {
                GridMode = 3;
                var f = DpiRatio;
                dg.MovingStop();
                dg.SelectionMode = DvDataGridSelectionMode.SELECTOR;
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                dg.AutoSet = true;
                dg.Font = new Font("나눔고딕", 8);
                dg.TextShadow = false;
                dg.RowBevel = true;
                dg.TouchMode = false;
                dg.ScrollMode = ScrollMode.Vertical;
                dg.RowHeight = dg.ColumnHeight = Convert.ToInt32(30 * f);
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Text", HeaderText = "텍스트", SizeMode = SizeMode.Percent, Width = 16M, CellType = typeof(DvDataGridEditTextCell) });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Number1", HeaderText = "정수", SizeMode = SizeMode.Percent, Width = 16M, CellType = typeof(DvDataGridEditNumberCell), Format = "0" });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Number2", HeaderText = "소수", SizeMode = SizeMode.Percent, Width = 17M, CellType = typeof(DvDataGridEditNumberCell), Format = "0.00" });
                dg.Columns.Add(new DvDataGridDateTimePickerColumn(dg) { Name = "Time", HeaderText = "시간", SizeMode = SizeMode.Percent, Width = 17M, PickerMode = DvDateTimePickerStyle.DateTime });
                dg.Columns.Add(new DvDataGridColorPickerColumn(dg) { Name = "Color", HeaderText = "색상", SizeMode = SizeMode.Percent, Width = 17M });
                dg.Columns.Add(new DvDataGridComboBoxColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = SizeMode.Percent, Width = 17M, Items = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x=>new DvDataGridComboBoxItem(x.ToString()) { Source = x }).ToList()});

                var Items = new List<GridItem3>();
                for (int i = 1; i <= 100; i++)
                {
                    Items.Add(new GridItem3() { Text = "아이템" + i, Number1 = i, Number2 = i * 10D / 100D, DOW = (DayOfWeek)(i % 7), Time = new DateTime(2020, 1, 1) + TimeSpan.FromDays(i) });
                }
                dg.SetDataSource<GridItem3>(Items);
            });
            #endregion
            #region refresh
            tmr = new Timer() { Interval = 10 };
            tmr.Tick += (o, s) =>
              {
                  if (GridMode == 2)
                  {
                      var ar = Items2.ToArray();
                      foreach(var v in ar)
                      {
                          v.Humidity = Convert.ToInt32(MathTool.Constrain(v.Humidity + (rnd.Next() % 2 == 0 ? 1 : -1), 0, 100));
                          v.Temperature = Convert.ToDouble(MathTool.Constrain(v.Temperature + (rnd.Next() % 2 == 0 ? 0.1 : -0.1), 0, 100));
                      }
                      dg.RefreshValues();
                  }
              };
            tmr.Enabled = true;
            #endregion

            btnGridMonth.ButtonClick += (o, s) => actMonth();
            btnGridCells.ButtonClick += (o, s) => actCells();
            btnGridCells2.ButtonClick += (o, s) => actCells2();
            actMonth();
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
                    int n = 5;
                    cpp = MathTool.Constrain(cpp + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);
                    java = MathTool.Constrain(java + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);
                    csharp = MathTool.Constrain(csharp + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);

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
        #region GetDOW
        string GetDOW(object dow)
        {
            var s = "";
            if (dow is DayOfWeek)
            {
                switch (dow)
                {
                    case DayOfWeek.Monday: s = "월"; break;
                    case DayOfWeek.Tuesday: s = "화"; break;
                    case DayOfWeek.Wednesday: s = "수"; break;
                    case DayOfWeek.Thursday: s = "목"; break;
                    case DayOfWeek.Friday: s = "금"; break;
                    case DayOfWeek.Saturday: s = "토"; break;
                    case DayOfWeek.Sunday: s = "일"; break;
                }
            }
            return s;
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

        public DateTime Time { get; set; }
    }
    #endregion
    #region class : TContent
    public class TContent : DvContent
    {
        public int Num { get; set; }
        public bool OnOff { get; set; }
        public override bool SelectedDraw => true;
        
        public TContent(DvControl Control) : base(Control) { }

        public override void Draw(Graphics g, DvTheme Theme, Rectangle Bounds)
        {
            if (Control != null)
            {
                var rt = GetBounds(Bounds);

                Theme.DrawBox(g, (OnOff ? Theme.PointColor : Theme.Color2), Control.BackColor, rt, RoundType.ALL, BoxDrawOption.BORDER);
                Theme.DrawText(g, null, Num.ToString(), Control.Font, Color.White, rt);

                if (Selected) Theme.DrawBorder(g, Color.Goldenrod, Control.BackColor, 2, rt, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
            }
            base.Draw(g, Theme, Bounds);
        }

        public override void MouseDoubleClick(Rectangle Bounds, Point p)
        {
            if (Collision(Bounds, p)) { OnOff = !OnOff; Control?.Invalidate(); }
            base.MouseDoubleClick(Bounds, p);
        }

        public override bool Collision(Rectangle Bounds, Point p) => CollisionTool.Check(GetBounds(Bounds), p);
        public override Rectangle GetBounds(Rectangle Bounds) => Bounds;
    }
    #endregion
    #region class : GridItem
    public class GridItem
    {
        public string Name { get; set; }
        public string State { get; set; }

        public int Day1 { get => Days[0]; }
        public int Day2 { get => Days[1]; }
        public int Day3 { get => Days[2]; }
        public int Day4 { get => Days[3]; }
        public int Day5 { get => Days[4]; }
        public int Day6 { get => Days[5]; }
        public int Day7 { get => Days[6]; }
        public int Day8 { get => Days[7]; }
        public int Day9 { get => Days[8]; }
        public int Day10 { get => Days[9]; }
        public int Day11 { get => Days[10]; }
        public int Day12 { get => Days[11]; }
        public int Day13 { get => Days[12]; }
        public int Day14 { get => Days[13]; }
        public int Day15 { get => Days[14]; }
        public int Day16 { get => Days[15]; }
        public int Day17 { get => Days[16]; }
        public int Day18 { get => Days[17]; }
        public int Day19 { get => Days[18]; }
        public int Day20 { get => Days[19]; }
        public int Day21 { get => Days[20]; }
        public int Day22 { get => Days[21]; }
        public int Day23 { get => Days[22]; }
        public int Day24 { get => Days[23]; }
        public int Day25 { get => Days[24]; }
        public int Day26 { get => Days[25]; }
        public int Day27 { get => Days[26]; }
        public int Day28 { get => Days[27]; }
        public int Day29 { get => Days[28]; }
        public int Day30 { get => Days[29]; }
        public int Day31 { get => Days[30]; }

        public int[] Days { get; set; } = new int[31];
    }
    #endregion
    #region class : GridItem2
    public enum DeviceMode { A, B, C }
    public class GridItem2
    {
        public string DeviceName { get; set; }
        public Bitmap DeviceImage { get; set; }
        public DateTime Time { get; set; }
        public DayOfWeek DOW => Time.DayOfWeek;
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public bool AlarmH => Humidity < 15;
        public bool AlarmT => Temperature > 80;

        public string RunText => "RUN";
        public string StopText => "STOP";
    }
    #endregion
    #region class : GridItem3
    public class GridItem3
    {
        public string Text { get; set; }
        public int Number1 { get; set; }
        public double Number2 { get; set; }
        public Color Color { get; set; } = Color.White;
        public DateTime Time { get; set; } = new DateTime(2000, 1, 1);
        public DayOfWeek DOW { get; set; }
    }
    #endregion
}