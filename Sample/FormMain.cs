using Devinno.Communications.Modbus.TCP;
using Devinno.Data;
using Devinno.Forms;
using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Timers;
using Devinno.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thread = System.Threading.Thread;

namespace Sample
{
    public partial class FormMain : DvForm
    {
        #region Member Variable
        Random rnd = new Random();
        Data2 v = new Data2();
        DateTime prev = DateTime.Now;
        List<TextIcon> lsWP = new List<TextIcon>();

        Timer tmr;
        HiResTimer tmrHi;
      
        DvKeypad keypad;
        DvKeyboard keyboard;
        DvMessageBox MessageBox;
        DvInputBox InputBox;
        DvWheelPickerBox WheelBox;
        DvSelectorBox SelBox;
        DvSerialPortSettingBox portBox;
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            #region Set
            MenuGap = 10;
            MaximizeBox = false;
            MinimizeBox = false;
            Fixed = true;
            #endregion
            #region Menus
            Menus.Add(new DvFormMenuSelector("Control") { Text = "Control", Width = 70, NextSep = true, Selected = true });
            Menus.Add(new DvFormMenuSelector("Gauge") { Text = "Gauge", Width = 65, NextSep = true });
            Menus.Add(new DvFormMenuSelector("Graph") { Text = "Graph", Width = 65, NextSep = true });
            Menus.Add(new DvFormMenuSelector("Container") { Text = "Container", Width = 85, NextSep = true });
            Menus.Add(new DvFormMenuSelector("Dialog") { Text = "Dialog", Width = 65, NextSep = true });
            Menus.Add(new DvFormMenuSelector("Table") { Text = "Table", Width = 65 });
            #endregion

            #region New
            #region btnsType
            btnsType.Buttons.Add(new ButtonInfo("A") { Text = "A", Size = new SizeInfo(DvSizeMode.Percent, 33.3F) });
            btnsType.Buttons.Add(new ButtonInfo("B") { Text = "B", Size = new SizeInfo(DvSizeMode.Percent, 33.4F) });
            btnsType.Buttons.Add(new ButtonInfo("C") { Text = "C", Size = new SizeInfo(DvSizeMode.Percent, 33.3F) });
            #endregion
            #region sels
            for (int i = 1; i <= 10; i++) sels.Items.Add(new TextIcon() { Text = "Item " + i, IconString = "fa-cube", IconSize = 10 });
            sels.SelectedIndex = 0;
            #endregion
            #region ani
            ani.OffImage = new Bitmap(Properties.Resources.Fan0);
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan1));
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan2));
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan3));
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan4));
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan5));
            ani.OnImages.Add(new Bitmap(Properties.Resources.Fan6));
            ani.Interval = 25;
            ani.MouseDown += (o, s) => ani.OnOff = !ani.OnOff;
            #endregion
            #region meter
            meter.Bars.Add(new MeterBar(0, 70, Color.Green));
            meter.Bars.Add(new MeterBar(70, 90, Color.Orange));
            meter.Bars.Add(new MeterBar(90, 100, Color.Red));
            #endregion
            #region barGraphH
            barGraphH.Series.Add(new GraphSeries { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0) });
            barGraphH.Series.Add(new GraphSeries { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0) });
            barGraphH.Series.Add(new GraphSeries { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200) });
            #endregion
            #region barGraphV
            barGraphV.Series.Add(new GraphSeries { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0) });
            barGraphV.Series.Add(new GraphSeries { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0) });
            barGraphV.Series.Add(new GraphSeries { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200) });
            #endregion
            #region lineGraph
            lineGraph.Series.Add(new GraphSeries { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0) });
            lineGraph.Series.Add(new GraphSeries { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0) });
            lineGraph.Series.Add(new GraphSeries { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200) });
            #endregion
            #region circleGraph
            circleGraph.Series.Add(new GraphSeries { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0) });
            circleGraph.Series.Add(new GraphSeries { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0) });
            circleGraph.Series.Add(new GraphSeries { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200) });
            #endregion
            #region timeGraph
            timeGraph.XAxisGridDraw = true;
            timeGraph.YAxisGridDraw = true;
            timeGraph.Series.Add(new GraphSeries2() { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0), Minimum = 0, Maximum = 100 });
            timeGraph.Series.Add(new GraphSeries2() { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0), Minimum = 0, Maximum = 100 });
            timeGraph.Series.Add(new GraphSeries2() { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200), Minimum = 0, Maximum = 100 });
            #endregion
            #region TrendGraph
            trendGraph.Series.Add(new GraphSeries2() { Name = "CSharp", Alias = "C#", SeriesColor = Color.FromArgb(220, 0, 0), Minimum = 0, Maximum = 100 });
            trendGraph.Series.Add(new GraphSeries2() { Name = "Cpp", Alias = "C++", SeriesColor = Color.FromArgb(0, 150, 0), Minimum = 0, Maximum = 100 });
            trendGraph.Series.Add(new GraphSeries2() { Name = "Java", Alias = "Java", SeriesColor = Color.FromArgb(0, 0, 200), Minimum = 0, Maximum = 100 });
            trendGraph.MaximumXScale = TimeSpan.FromSeconds(10);
            trendGraph.XScale = TimeSpan.FromSeconds(1);
            trendGraph.XAxisGraduation = TimeSpan.FromSeconds(0.2);
            trendGraph.Interval = 10;
            #endregion
            #region menuGraph
            menuGraph.SelectionMode = true;
            menuGraph.Buttons.Add(new ButtonInfo("BarGraphH") { Text = "Bar H", IconString = "fa-chart-bar", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 17), Checked = true });
            menuGraph.Buttons.Add(new ButtonInfo("BarGraphV") { Text = "Bar V", IconString = "fa-chart-column", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 17) });
            menuGraph.Buttons.Add(new ButtonInfo("LineGraph") { Text = "Line", IconString = "fa-chart-line", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 16) });
            menuGraph.Buttons.Add(new ButtonInfo("CircleGraph") { Text = "Circle", IconString = "fa-chart-pie", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 17) });
            menuGraph.Buttons.Add(new ButtonInfo("TimeGraph") { Text = "Time", IconString = "fa-clock", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 16) });
            menuGraph.Buttons.Add(new ButtonInfo("TrendGraph") { Text = "Trend", IconString = "fa-arrow-trend-up", IconSize = 12, IconGap = 3, Size = new SizeInfo(DvSizeMode.Percent, 17) });
            #endregion
            #region listBox
            for (int i = 1; i <= 30; i++)
                listBox.Items.Add(new TextIcon() { Text = "Item " + i, IconString = "fa-cube", IconSize = 10 });

            listBox.SelectionMode = ItemSelectionMode.Multi;
            #endregion
            #region inCombo
            for (int i = 1; i <= 30; i++)
                inCombo.Items.Add(new TextIcon() { Text = "Item " + i, IconString = "fa-cube", IconSize = 10 });

            inCombo.SelectedIndex = 0;
            #endregion
            #region comboBox
            for (int i = 1; i <= 30; i++)
                comboBox.Items.Add(new TextIcon() { Text = "Item " + i, IconString = "fa-cube", IconSize = 10 });

            comboBox.SelectedIndex = 0;
            #endregion
            #region Keypad
            keypad = new DvKeypad();
            #endregion
            #region keyboard
            keyboard = new DvKeyboard();
            #endregion
            #region MessageBox
            MessageBox = new DvMessageBox();
            #endregion
            #region wheelPicker
            foreach (var v in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
                inDoW.Items.Add(new TextIcon { Text = v.ToString() });
            
            inDoW.SelectedIndex = 0;
            #endregion
            #region tab2
            tab2.ItemSize = new Size(40, 120);
            tab2.SizeMode = TabSizeMode.Fixed;
            tab2.TabIcons.Add("tabPage1", new DvIcon("fa-tablet", 12) { Gap = 5 });
            tab2.TabIcons.Add("tabPage2", new DvIcon("fa-cube", 12) { Gap = 5 });
            tab2.TabIcons.Add("tabPage3", new DvIcon("fa-bell", 12) { Gap = 5 });
            //tab2.SizeMode = TabSizeMode.Normal;
            #endregion
            #region treeView
            for (int a = 1; a <= 3; a++)
            {
                var va = new DvTreeViewLabelNode("Cat " + a);
                treeView.Nodes.Add(va);

                var ls = Enum.GetValues<DayOfWeek>().Select(x => new TextIcon { Text = x.ToString(), Value = x }).ToList();

                for (int b = 1; b <= 2; b++)
                {
                    var vb = new DvTreeViewLabelNode("Sub " + a + "." + b);
                    va.Nodes.Add(vb);

                    for (int c = 1; c <= 4; c++)
                    {
                        if (c == 1)
                        {
                            var vc = new DvTreeViewValueLabelNode("Name") { Value = "Item " + a + "." + b + "." + c, ValueColor = Color.FromArgb(50, 50, 50), TitleWidth = 60, ValueWidth = 120 };
                            vb.Nodes.Add(vc);
                        }
                        else if (c == 2)
                        {
                            var vc = new DvTreeViewInputTextNode("Title") { TitleWidth = 60, ValueWidth = 120 };
                            vb.Nodes.Add(vc);
                        }
                        else if (c == 3)
                        {
                            var vc = new DvTreeViewInputNumberNode<float>("Temp") { Minimum = -20, Maximum = 200, Value = 0, TitleWidth = 60, ButtonWidth = 30, ValueWidth = 90, UnitWidth = 24, Unit = "℃", ButtonIconString = "fa-check" };
                            vb.Nodes.Add(vc);
                        }
                        else if (c == 4)
                        {
                            var vc = new DvTreeViewInputComboNode("DOW") { TitleWidth = 60, ValueWidth = 120 };
                            vc.Items.AddRange(ls);
                            vc.SelectedIndex = 0;
                            vb.Nodes.Add(vc);
                        }
                    }
                }
            }
            #endregion
            #region toolBox
            for (int i = 1; i <= 3; i++)
            {
                var v = new ToolCategoryItem("Category " + i);
                toolBox.Categories.Add(v);

                for (int j = 1; j <= 10; j++)
                    v.Items.Add(new ToolItem("Item " + i + "." + j));
            }
            toolBox.ItemDown += (o, s) => s.Drag = true;

            lblToolDrag.AllowDrop = true;
            lblToolDrag.DragEnter += (o, s) =>
            {
                if (s.Data.GetDataPresent(typeof(ToolItem))) s.Effect = DragDropEffects.Copy;
                else s.Effect = DragDropEffects.None;
            };
            lblToolDrag.DragDrop += (o, s) =>
            {
                if (s.Data.GetDataPresent(typeof(ToolItem)))
                {
                    var itm = s.Data.GetData(typeof(ToolItem)) as ToolItem;
                    if (itm != null)
                    {
                        lblToolDrag.Text = itm.Text;
                    }
                }
            };
            #endregion
            #region dataGrid
            #region actMonth
            var actMonth = new Action(() =>
            {
                var nds = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                var dg = dataGrid;
                dg.SelectionMode = DvDataGridSelectionMode.Selector;
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                dg.ScrollMode = ScrollMode.Both;
                dg.RowHeight = dg.ColumnHeight = Convert.ToInt32(30);
                dg.ColumnGroups.Add(new DvDataGridColumn(dg) { Name = "G1", HeaderText = "기본사항", Fixed = true });
                dg.ColumnGroups.Add(new DvDataGridColumn(dg) { Name = "G2", HeaderText = "일일 수집량" });
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", GroupName = "G1", HeaderText = "이름", SizeMode = DvSizeMode.Pixel, Width = Convert.ToInt32(100), Fixed = true, UseFilter = true, CellType = typeof(DvDataGridLabelCell) });
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "State", GroupName = "G1", HeaderText = "상태", SizeMode = DvSizeMode.Pixel, Width = Convert.ToInt32(70), Fixed = true, CellType = typeof(DvDataGridLabelCell) });
                for (int i = 1; i <= nds; i++)
                    dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Day" + i, GroupName = "G2", HeaderText = i + "일", SizeMode = DvSizeMode.Pixel, Width = Convert.ToInt32(70), CellType = typeof(DvDataGridLabelCell) });

                var srow = new DvDataGridSummaryRow(dg);
                var srow2 = new DvDataGridSummaryRow(dg);
                srow.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "합계", ColumnIndex = 0, ColumnSpan = 2 });
                srow.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "", ColumnIndex = 0, ColumnSpan = 1, Visible = false });
                srow2.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "평균", ColumnIndex = 0, ColumnSpan = 2 });
                srow2.Cells.Add(new DvDataGridSummaryLabelCell(dg, srow) { Text = "", ColumnIndex = 0, ColumnSpan = 1, Visible = false });
                for (int i = 1; i <= nds; i++)
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
            #region actMonitor
            var actMonitor = new Action(() =>
            {
                var pics = new List<Bitmap>();
                pics.Add(new Bitmap(Properties.Resources._1));
                pics.Add(new Bitmap(Properties.Resources._2));
                pics.Add(new Bitmap(Properties.Resources._3));

                var dg = dataGrid;
                dg.SelectionMode = DvDataGridSelectionMode.Selector;
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                /*
                dg.RowHeight = 43;
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "DeviceName", HeaderText = "장치명", SizeMode = SizeMode.Percent, Width = 10M });
                dg.Columns.Add(new DvDataGridImageColumn(dg) { Name = "DeviceImage", HeaderText = "이미지", SizeMode = SizeMode.Percent, Width = 11M });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Time", HeaderText = "설치일", SizeMode = SizeMode.Percent, Width = 15M, Format = "yyyy.MM.dd", UseSort = true });
                dg.Columns.Add(new DvDataGridTextConverterColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = SizeMode.Percent, Width = 8M, Converter = GetDOW });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Temperature", HeaderText = "온도", SizeMode = SizeMode.Percent, Width = 10M, Format = "0.0 ℃" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmT", HeaderText = "온도 알람", SizeMode = SizeMode.Percent, Width = 10M});
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Humidity", HeaderText = "습도", SizeMode = SizeMode.Percent, Width = 10M, Format = "0 '%'" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmH", HeaderText = "습도 알람", SizeMode = SizeMode.Percent, Width = 10M});
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "Play", HeaderText = "동작", Text = "", IconString = "fa-play", IconSize = 11, SizeMode = SizeMode.Percent, Width = 8M });
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "Stop", HeaderText = "정지", Text = "", IconString = "fa-stop", IconSize = 11, SizeMode = SizeMode.Percent, Width = 8M });
                */

                dg.RowHeight = 30;
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "DeviceName", HeaderText = "장치명", SizeMode = DvSizeMode.Percent, Width = 15M });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Time", HeaderText = "설치일", SizeMode = DvSizeMode.Percent, Width = 15M, Format = "yyyy.MM.dd", UseSort = true });
                dg.Columns.Add(new DvDataGridTextConverterColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = DvSizeMode.Percent, Width = 10M, Converter = GetDOW });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Temperature", HeaderText = "온도", SizeMode = DvSizeMode.Percent, Width = 10M, Format = "0.0 ℃" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmT", HeaderText = "온도 알람", SizeMode = DvSizeMode.Percent, Width = 10M });
                dg.Columns.Add(new DvDataGridTextFormatColumn(dg) { Name = "Humidity", HeaderText = "습도", SizeMode = DvSizeMode.Percent, Width = 10M, Format = "0 '%'" });
                dg.Columns.Add(new DvDataGridLampColumn(dg) { Name = "AlarmH", HeaderText = "습도 알람", SizeMode = DvSizeMode.Percent, Width = 10M });
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "Play", HeaderText = "동작", Text = "", IconString = "fa-play", IconSize = 12, SizeMode = DvSizeMode.Percent, Width = 10M });
                dg.Columns.Add(new DvDataGridButtonColumn(dg) { Name = "Stop", HeaderText = "정지", Text = "", IconString = "fa-stop", IconSize = 12, SizeMode = DvSizeMode.Percent, Width = 10M });

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
                dg.SetDataSource<GridItem2>(Items);

                new Thread((o) =>
                {
                    var ls = o as List<GridItem2>;
                    while (true)
                    {
                        foreach (var v in ls)
                        {
                            v.Humidity = Convert.ToInt32(MathTool.Constrain(v.Humidity + (rnd.Next() % 2 == 0 ? 1 : -1), 0, 100));
                            v.Temperature = Convert.ToDouble(MathTool.Constrain(v.Temperature + (rnd.Next() % 2 == 0 ? 0.1 : -0.1), 0, 100));
                        }
                        Thread.Sleep(10);
                    }
                })
                { IsBackground = true }.Start(Items);
            });
            #endregion
            #region actInput
            var actInput = new Action(() =>
            {
                var dg = dataGrid;
                dg.SelectionMode = DvDataGridSelectionMode.Selector;
                dg.ColumnGroups.Clear();
                dg.Columns.Clear();
                dg.Rows.Clear();
                dg.SummaryRows.Clear();

                var ls = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => new TextIcon { Text = GetDOW(x), Value = x }).ToList();

                /*
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", HeaderText = "명칭", SizeMode = DvSizeMode.Percent, Width = 10M });
                dg.Columns.Add(new DvDataGridCheckBoxColumn(dg) { Name = "Used", HeaderText = "사용", SizeMode = DvSizeMode.Percent, Width = 5M });
                dg.Columns.Add(new DvDataGridComboBoxColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = DvSizeMode.Percent, Width = 10M, Items = ls, MaximumViewCount = 5, ButtonWidth = 30, });
                dg.Columns.Add(new DvDataGridEditTextColumn(dg) { Name = "Message", HeaderText = "메시지", SizeMode = DvSizeMode.Percent, Width = 12M });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer", HeaderText = "정수", SizeMode = DvSizeMode.Percent, Width = 10M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<double>(dg) { Name = "Double", HeaderText = "실수", SizeMode = DvSizeMode.Percent, Width = 10M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditBoolColumn(dg) { Name = "OnOff", HeaderText = "ON/OFF", SizeMode = DvSizeMode.Percent, Width = 18M, });
                dg.Columns.Add(new DvDataGridDateTimePickerColumn(dg) { Name = "Time", HeaderText = "날짜", SizeMode = DvSizeMode.Percent, Width = 13M, PickerMode = DateTimePickerType.Date });
                dg.Columns.Add(new DvDataGridColorPickerColumn(dg) { Name = "Color", HeaderText = "색상", SizeMode = DvSizeMode.Percent, Width = 12M });
                */
                /*
                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", HeaderText = "명칭", SizeMode = DvSizeMode.Percent, Width = 12M });
                dg.Columns.Add(new DvDataGridComboBoxColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = DvSizeMode.Percent, Width = 13M, Items = ls, MaximumViewCount = 5, ButtonWidth = 40, });
                dg.Columns.Add(new DvDataGridEditStringColumn(dg) { Name = "Message", HeaderText = "메시지", SizeMode = DvSizeMode.Percent, Width = 15M });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer", HeaderText = "정수", SizeMode = DvSizeMode.Percent, Width = 10M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<double>(dg) { Name = "Double", HeaderText = "실수", SizeMode = DvSizeMode.Percent, Width = 12M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditBoolColumn(dg) { Name = "OnOff", HeaderText = "ON/OFF", SizeMode = DvSizeMode.Percent, Width = 22M, });
                dg.Columns.Add(new DvDataGridDateTimePickerColumn(dg) { Name = "Time", HeaderText = "날짜", SizeMode = DvSizeMode.Percent, Width = 16M, PickerMode = DateTimePickerType.Date });
                */

                dg.Columns.Add(new DvDataGridColumn(dg) { Name = "Name", HeaderText = "명칭", SizeMode = DvSizeMode.Pixel, Width = 80M });
                dg.Columns.Add(new DvDataGridComboBoxColumn(dg) { Name = "DOW", HeaderText = "요일", SizeMode = DvSizeMode.Pixel, Width = 80M, Items = ls, MaximumViewCount = 5, ButtonWidth = 40, });
                dg.Columns.Add(new DvDataGridEditTextColumn(dg) { Name = "Message", HeaderText = "메시지", SizeMode = DvSizeMode.Pixel, Width = 80M });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer", HeaderText = "정수", SizeMode = DvSizeMode.Pixel, Width = 80M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer2", HeaderText = "정수2", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer3", HeaderText = "정수3", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer4", HeaderText = "정수4", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer5", HeaderText = "정수5", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer6", HeaderText = "정수6", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer7", HeaderText = "정수7", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer8", HeaderText = "정수8", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<int>(dg) { Name = "Integer9", HeaderText = "정수9", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditNumberColumn<double>(dg) { Name = "Double", HeaderText = "실수", SizeMode = DvSizeMode.Pixel, Width = 70M, Minimum = 0, Maximum = 100 });
                dg.Columns.Add(new DvDataGridEditBoolColumn(dg) { Name = "OnOff", HeaderText = "ON/OFF", SizeMode = DvSizeMode.Pixel, Width = 160M, });
                dg.Columns.Add(new DvDataGridDateTimePickerColumn(dg) { Name = "Time", HeaderText = "날짜", SizeMode = DvSizeMode.Pixel, Width = 120M, PickerMode = DateTimePickerType.Date });
                dg.ScrollMode = ScrollMode.Both;

                var Items = new List<GridItem3>();
                for (int i = 1; i <= 30; i++)
                {
                    Items.Add(new GridItem3()
                    {
                        Name = "아이템" + i,
                        DOW = (DayOfWeek)(i % 7),
                        Time = DateTime.Now.Date,
                        Color = Color.Black,
                        Message = "Comment",
                    });
                }
                dg.SetDataSource<GridItem3>(Items);
            });
            #endregion

            actMonth();
            //actMonitor();
            //actInput();
            #endregion
            #region Boxes
            InputBox = new DvInputBox { MinWidth = 220 };
            WheelBox = new DvWheelPickerBox { };
            SelBox = new DvSelectorBox { ColumnCount = 2, ItemWidth = 100 };
            portBox = new DvSerialPortSettingBox { };

            for (int i = 1; i <= 10; i++) lsWP.Add(new TextIcon() { Text = "Item " + i });
            #endregion
            #endregion

            #region Event
            #region this.MenuSelected
            this.MenuSelected += (o, s) =>
            {
                switch (s.Menu.Name)
                {
                    case "Control": tab.SelectedTab = tpControl; break;
                    case "Gauge": tab.SelectedTab = tpGauge; break;
                    case "Graph": tab.SelectedTab = tpGraph; break;
                    case "Container": tab.SelectedTab = tpContainer; break;
                    case "Dialog": tab.SelectedTab = tpDialog; break;
                    case "Table": tab.SelectedTab = tpTable; break;

                }
            };

            tab.SelectedTab = tpControl;
            #endregion
            #region btnTextAlign.ButtonClick 
            btnTextAlign.ButtonClick += (o, s) =>
            {
                switch (txtMultiLine.ContentAlignment)
                {
                    case DvContentAlignment.TopLeft: txtMultiLine.ContentAlignment = DvContentAlignment.TopCenter; break;
                    case DvContentAlignment.TopCenter: txtMultiLine.ContentAlignment = DvContentAlignment.TopRight; break;
                    case DvContentAlignment.TopRight: txtMultiLine.ContentAlignment = DvContentAlignment.MiddleLeft; break;
                    case DvContentAlignment.MiddleLeft: txtMultiLine.ContentAlignment = DvContentAlignment.MiddleCenter; break;
                    case DvContentAlignment.MiddleCenter: txtMultiLine.ContentAlignment = DvContentAlignment.MiddleRight; break;
                    case DvContentAlignment.MiddleRight: txtMultiLine.ContentAlignment = DvContentAlignment.BottomLeft; break;
                    case DvContentAlignment.BottomLeft: txtMultiLine.ContentAlignment = DvContentAlignment.BottomCenter; break;
                    case DvContentAlignment.BottomCenter: txtMultiLine.ContentAlignment = DvContentAlignment.BottomRight; break;
                    case DvContentAlignment.BottomRight: txtMultiLine.ContentAlignment = DvContentAlignment.TopLeft; break;
                }
            };
            #endregion
            #region btnLamp.ButtonClick
            btnLampMotor.ButtonClick += (o, s) => btnLampMotor.OnOff = !btnLampMotor.OnOff;
            btnLampPump.ButtonClick += (o, s) => btnLampPump.OnOff = !btnLampPump.OnOff;
            #endregion
            #region lmp.MouseClick
            lmp1.MouseDown += (o, s) => lmp1.OnOff = !lmp1.OnOff;
            lmp2.MouseDown += (o, s) => lmp2.OnOff = !lmp2.OnOff;
            lmp3.MouseDown += (o, s) => lmp3.OnOff = !lmp3.OnOff;
            #endregion
            #region in.ValueChanged
            inOnOff.ValueChanged += (o, s) => vlblOnOff.Value = inOnOff.Value;
            inName.ValueChanged += (o, s) => vlblName.Value = inName.Value;
            inPos.ValueChanged += (o, s) => vlblPos.Value = inPos.Value ?? vlblPos.Value;
            inTemp.ValueChanged += (o, s) => vlblTemp.Value = inTemp.Value ?? vlblTemp.Value;
            #endregion
            #region sld.ValueChanged
            sldH_N.ValueChanged += (o, s) => pgsH_N.Value = sldH_N.Value;
            sldH_R.ValueChanged += (o, s) => pgsH_R.Value = sldH_R.Value;
            sldH_G.ValueChanged += (o, s) => pgsH_G.Value = sldH_G.Value;
            sldH_B.ValueChanged += (o, s) => pgsH_B.Value = sldH_B.Value;

            sldV_N.ValueChanged += (o, s) => pgsV_N.Value = sldV_N.Value;
            sldV_R.ValueChanged += (o, s) => pgsV_R.Value = sldV_R.Value;
            sldV_G.ValueChanged += (o, s) => pgsV_G.Value = sldV_G.Value;
            sldV_B.ValueChanged += (o, s) => pgsV_B.Value = sldV_B.Value;
            #endregion
            #region knob.ValueChanged
            knob.ValueChanged += (o, s) => gauge.Value = meter.Value = knob.Value;
            #endregion

            #region menuGraph.SelectedChanged
            menuGraph.SelectedChanged += (o, s) =>
            {
                switch (s.Button.Name)
                {
                    case "BarGraphH": tabGraph.SelectedTab = tpBarGraphH; break;
                    case "BarGraphV": tabGraph.SelectedTab = tpBarGraphV; break;
                    case "CircleGraph": tabGraph.SelectedTab = tpCircleGraph; break;
                    case "LineGraph": tabGraph.SelectedTab = tpLineGraph; break;
                    case "TrendGraph": tabGraph.SelectedTab = tpTrendGraph; break;
                    case "TimeGraph": tabGraph.SelectedTab = tpTimeGraph; break;
                }
            };
            #endregion
            #region btnGraphRefresh.ButtonClick
            btnGraphRefresh.ButtonClick += (o, s) =>
            {
                GraphSet();
                TimeGraphSet();
            };
            #endregion
            #region btnPause.ButtonClick
            btnPause.ButtonClick += (o, s) =>
            {
                trendGraph.Pause = !trendGraph.Pause;
                btnPause.IconString = trendGraph.Pause ? "fa-play" : "fa-pause";
            };
            #endregion
       
            #region tmrHi.Tick
            tmrHi = new HiResTimer(5);
            tmrHi.Elapsed += (o, s) => TrendGraphSet();
            tmrHi.Start();
            #endregion
            #region tmr
            tmr = new Timer { Interval = 10, Enabled = true };
            tmr.Tick += (o, s) => dataGrid.Invalidate();
            #endregion

            #region btnKeypadInt.ButtonClick
            btnKeypadInt.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = keypad.ShowKeypad<int>("키패드");
                if (ret.HasValue) lblKeyResult.Text = ret.Value.ToString();
                Block = false;
            };
            #endregion
            #region btnKeypadDecimal.ButtonClick
            btnKeypadDecimal.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = keypad.ShowKeypad<decimal>("키패드");
                if (ret.HasValue) lblKeyResult.Text = ret.Value.ToString();
                Block = false;
            };
            #endregion
            #region btnKeypadPassword.ButtonClick
            btnKeypadPassword.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = keypad.ShowPassword("패스워드");
                if (ret != null) lblKeyResult.Text = ret;
                Block = false;
            };
            #endregion
            #region btnKeyboardHan.ButtonClick
            btnKeyboardHan.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = keyboard.ShowKeyboard("키보드", KeyboardMode.Korea, "");
                if (ret != null) lblKeyResult.Text = ret;
                Block = false;
            };
            #endregion
            #region btnKeyboardEng.ButtonClick
            btnKeyboardEng.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = keyboard.ShowKeyboard("키보드", KeyboardMode.EnglishOnly, "");
                if (ret != null) lblKeyResult.Text = ret;
                Block = false;
            };
            #endregion

            #region btnMbOK.ButtonClick
            btnMbOK.ButtonClick += (o, s) =>
            {
                Block = true;
                lblMb.Text = MessageBox.ShowMessageBoxOk("애국가", "동해물과 백두산이 마르고 닳도록\r\n하나님이 보우하사 우리나라 만세\r\n무궁화 삼천리 화려강산\r\n대한사람 대한으로 길이 보전하세").ToString();
                Block = false;
            };
            #endregion
            #region btnMbYesNo.ButtonClick
            btnMbYesNo.ButtonClick += (o, s) =>
            {
                Block = true;
                lblMb.Text = MessageBox.ShowMessageBoxYesNo("진행", "진행 하시겠습니까?").ToString();
                Block = false;
            };
            #endregion
            #region btnMbOkCancel.ButtonClick
            btnMbOkCancel.ButtonClick += (o, s) =>
            {
                Block = true;
                lblMb.Text = MessageBox.ShowMessageBoxOkCancel("확인", "확인 하시겠습니까?").ToString();
                Block = false;
            };
            #endregion
            #region btnMbYesNoCancel.ButtonClick
            btnMbYesNoCancel.ButtonClick += (o, s) =>
            {
                Block = true;
                lblMb.Text = MessageBox.ShowMessageBoxYesNoCancel("저장", "저장 하시겠습니까?").ToString();
                Block = false;
            };
            #endregion

            #region btnInputClass.ButtonClick
            btnInputClass.ButtonClick += (o, s) =>
            {
                Dictionary<string, InputBoxInfo> infos = new Dictionary<string, InputBoxInfo>();
                infos.Add("Name", new InputBoxInfo { Title = "명칭" });
                infos.Add("Distance", new InputBoxInfo { Title = "길이", Unit = "㎜" });
                infos.Add("Temperature", new InputBoxInfo { Title = "온도", Unit = "℃" });
                infos.Add("DayOfWeek", new InputBoxInfo { Title = "요일" });
                infos.Add("Step", new InputBoxInfo { Title = "단계", Items = new int[] { 0, 10, 20, 30, 40, 50 }.Select(x => new TextIcon() { Text = x.ToString(), Value = x }).ToList() });
                infos.Add("Use", new InputBoxInfo { Title = "사용여부" });
                
                Block = true;
                InputBox.ItemWidth = 200;
                var ret = InputBox.ShowInputBox<Data3>("입력", new Data3 { Name = "테스트", Distance = 100, Temperature = 36.5, DayOfWeek = DayOfWeek.Monday }, infos);

                if (ret != null)
                {
                    var set = new JsonSerializerSettings() { Formatting = Formatting.Indented };
                    var ss = Serialize.JsonSerialize(ret, set);

                   MessageBox.ShowMessageBoxOk("JSON", ss);
                }
                Block = false;
            };
            #endregion
            #region btnInputString.ButtonClick
            btnInputString.ButtonClick += (o, s) =>
            {
                Block = true;
                InputBox.UseEnterKey = true;
                var ret = InputBox.ShowString("명칭", "테스트");
                if (ret != null) MessageBox.ShowMessageBoxOk("결과", ret);
                InputBox.UseEnterKey = false;
                Block = false;
            };
            #endregion
            #region btnInputInt.ButtonClick
            btnInputInt.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = InputBox.ShowInt("속도", 0, 0, 100, "%");
                if (ret.HasValue) MessageBox.ShowMessageBoxOk("결과", ret.Value.ToString());
                Block = false;
            };
            #endregion
            #region btnInputFloat.ButtonClick
            btnInputFloat.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = InputBox.ShowFloat("온도", 0F, -50F, 200F, "℃");
                if (ret.HasValue) MessageBox.ShowMessageBoxOk("결과", ret.Value.ToString());
                Block = false;
            };
            #endregion

            #region btnSelWheel.ButtonClick
            btnSelWheel.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = WheelBox.ShowWheelPickerBox("선택", 0, lsWP);
                Block = false;
            };
            #endregion
            #region btnSelSelector.ButtonClick
            btnSelSelector.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = SelBox.ShowSelector("선택", lsWP, lsWP.FirstOrDefault());
                Block = false;
            };
            #endregion
            #region btnSelCombo.ButtonClick
            btnSelCombo.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = SelBox.ShowComboBox("선택", lsWP, lsWP.FirstOrDefault());
                Block = false;
            };
            #endregion
            #region btnSelRadio.ButtonClick
            btnSelRadio.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = SelBox.ShowRadioBox("선택", lsWP, lsWP.FirstOrDefault());
                Block = false;
            };
            #endregion
            #region btnSelCheck.ButtonClick
            btnSelCheck.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = SelBox.ShowCheckBox("선택", lsWP, new List<TextIcon>());
                Block = false;
            };
            #endregion

            #region btnPortSetting.ButtonClick
            btnPortSetting.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = portBox.ShowSerialPortSetting();
                if(ret != null)
                {
                    var set = new JsonSerializerSettings() { Formatting = Formatting.Indented };
                    var ss = Serialize.JsonSerialize(ret, set);
                    MessageBox.ShowMessageBoxOk("JSON", ss);
                }
                Block = false;
            };
            #endregion
            #region btnPortSettingSimple.ButtonClick
            btnPortSettingSimple.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = portBox.ShowSimpleSerialPortSetting();
                if (ret != null)
                {
                    var set = new JsonSerializerSettings() { Formatting = Formatting.Indented };
                    var ss = Serialize.JsonSerialize(ret, set);
                    MessageBox.ShowMessageBoxOk("JSON", ss);
                }
                Block = false;
            };
            #endregion

            #region btnTreeAdd.ButtonClick
            btnTreeAdd.ButtonClick += (o, s) =>
            {
                Block = true;
                var ret = InputBox.ShowString("입력");
                if (ret != null)
                {
                    if (treeView.SelectedNodes.Count > 0) treeView.SelectedNodes.First().Nodes.Add(new DvTreeViewLabelNode(ret));
                    else treeView.Nodes.Add(new DvTreeViewLabelNode(ret));
                    treeView.Invalidate();
                }
                Block = false;
            };
            #endregion
            #region btnTreeRemove.ButtonClick 
            btnTreeRemove.ButtonClick += (o, s) =>
            {
                if (treeView.SelectedNodes.Count > 0)
                {
                    var v = treeView.SelectedNodes.First();
                    if (v.Parents != null) v.Parents.Nodes.Remove(v);
                    else treeView.Nodes.Remove(v);   
                }
                treeView.Invalidate();
            };
            #endregion
            #endregion

            Theme.KeyboardInput = true;

            SetExComposited();
            GraphSet();
            TimeGraphSet();
            trendGraph.Start<Data2>(v);
            DwmTool.SetTheme(this, true);
        }
        #endregion

        #region Override
        #region OnClosed
        protected override void OnClosed(EventArgs e)
        {
            tmrHi.Stop();
            base.OnClosed(e);
        }
        #endregion
        #endregion

        #region Method
        #region GraphSet
        void GraphSet()
        {
            var ls1 = new List<Data1>();
            var java = 70D;
            var cpp = 50D;
            var csharp = 30D;
            var vcs = new Color[] { Color.Red, Color.DarkOrange, Color.LimeGreen, Color.Green, Color.Teal, Color.SteelBlue, Color.DodgerBlue, Color.DeepSkyBlue, Color.Violet, Color.Crimson, Color.Brown, Color.Maroon };
            for (int y = 2018; y <= 2021; y++)
                for (int m = 1; m <= 12; m++)
                {
                    int n = 5;
                    cpp = MathTool.Constrain(cpp + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);
                    java = MathTool.Constrain(java + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);
                    csharp = MathTool.Constrain(csharp + (rnd.Next() % 2 == 0 ? n : -n), 0, 100);

                    ls1.Add(new Data1() { Name = (y) + "." + m, Cpp = cpp, CSharp = csharp, Java = java, Color = vcs[m - 1] });
                }

            lineGraph.SetDataSource<Data1>(ls1);
            barGraphV.SetDataSource<Data1>(ls1);
            barGraphH.SetDataSource<Data1>(ls1);
            circleGraph.SetDataSource<Data1>(ls1.GetRange(ls1.Count - 12, 12));
        }
        #endregion
        #region TimeGraphSet
        void TimeGraphSet()
        {
            var ls1 = new List<Data2>();
            var java = 70D;
            var cpp = 50D;
            var csharp = 30D;
            for (var dt = DateTime.Now.Date; dt <= DateTime.Now.Date + TimeSpan.FromHours(5); dt += TimeSpan.FromSeconds(5 * 2))
            {
                cpp = MathTool.Constrain(cpp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                java = MathTool.Constrain(java + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);
                csharp = MathTool.Constrain(csharp + (rnd.Next() % 2 == 0 ? 3 : -3), 0, 100);

                ls1.Add(new Data2() { Time = dt, Cpp = cpp, CSharp = csharp, Java = java });
            }

            timeGraph.SetDataSource<Data2>(ls1);
        }
        #endregion
        #region TrendGraphSet
        public void TrendGraphSet()
        {
            if (trendGraph.IsStart)
            {
                int n = 1;
                v.Cpp = MathTool.Constrain(v.Cpp + rnd.Next(-n, n + 1), 0, 100);
                v.CSharp = MathTool.Constrain(v.CSharp + rnd.Next(-n, n + 1), 0, 100);
                v.Java = MathTool.Constrain(v.Java + rnd.Next(-n, n + 1), 0, 100);

                trendGraph.SetData<Data2>(v);
            }
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
    public class Data3
    {
        public string? Name { get; set; }
        public int Distance { get; set; }
        public double Temperature { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int Step { get; set; }
        public bool Use { get; set; }

        [InputBoxIgnore]
        [JsonIgnore]
        public string? Ext { get; set; }

        [JsonIgnore]
        public object? Tag { get; set; }
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
    }
    #endregion
    #region class : GridItem3
    public class GridItem3
    {
        public string Name { get; set; }
        public bool Used { get; set; }
        public DayOfWeek DOW { get; set; }
        public string Message { get; set; }
        public int Integer { get; set; }
        public double Double { get; set; }
        public bool OnOff { get; set; }
        public DateTime Time { get; set; }
        public Color Color { get; set; }

        public int Integer2 { get; set; }
        public int Integer3 { get; set; }
        public int Integer4 { get; set; }
        public int Integer5 { get; set; }
        public int Integer6 { get; set; }
        public int Integer7 { get; set; }
        public int Integer8 { get; set; }
        public int Integer9 { get; set; }

    }
    #endregion

}
