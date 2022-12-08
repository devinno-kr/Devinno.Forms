
namespace Sample
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab = new Devinno.Forms.Containers.DvTablessControl();
            this.tpControl = new System.Windows.Forms.TabPage();
            this.inOnOff = new Devinno.Forms.Controls.DvValueInputBool();
            this.inTemp = new Devinno.Forms.Controls.DvValueInputFloat();
            this.inPos = new Devinno.Forms.Controls.DvValueInputInt();
            this.inName = new Devinno.Forms.Controls.DvValueInputText();
            this.vlblName = new Devinno.Forms.Controls.DvValueLabelText();
            this.vlblPos = new Devinno.Forms.Controls.DvValueLabelInt();
            this.vlblTemp = new Devinno.Forms.Controls.DvValueLabelFloat();
            this.vlblOnOff = new Devinno.Forms.Controls.DvValueLabelBoolean();
            this.ani = new Devinno.Forms.Controls.DvAnimate();
            this.calendar = new Devinno.Forms.Controls.DvCalendar();
            this.sels = new Devinno.Forms.Controls.DvSelector();
            this.pic = new Devinno.Forms.Controls.DvPictureBox();
            this.nbR = new Devinno.Forms.Controls.DvNumberBox();
            this.nbUD = new Devinno.Forms.Controls.DvNumberBox();
            this.nbLR = new Devinno.Forms.Controls.DvNumberBox();
            this.nbRUD = new Devinno.Forms.Controls.DvNumberBox();
            this.sw = new Devinno.Forms.Controls.DvSwitch();
            this.onoff = new Devinno.Forms.Controls.DvOnOff();
            this.lmp3 = new Devinno.Forms.Controls.DvLamp();
            this.lmp2 = new Devinno.Forms.Controls.DvLamp();
            this.lmp1 = new Devinno.Forms.Controls.DvLamp();
            this.btnLampPump = new Devinno.Forms.Controls.DvLampButton();
            this.btnLampMotor = new Devinno.Forms.Controls.DvLampButton();
            this.tblTriButton = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnTriangleLeft = new Devinno.Forms.Controls.DvTriangleButton();
            this.btnTriangleRight = new Devinno.Forms.Controls.DvTriangleButton();
            this.btnTriangleDown = new Devinno.Forms.Controls.DvTriangleButton();
            this.btnTriableUp = new Devinno.Forms.Controls.DvTriangleButton();
            this.btnCircleUp = new Devinno.Forms.Controls.DvCircleButton();
            this.btnCircleDown = new Devinno.Forms.Controls.DvCircleButton();
            this.btnRadio2 = new Devinno.Forms.Controls.DvRadioButton();
            this.btnRadio1 = new Devinno.Forms.Controls.DvRadioButton();
            this.btnToggle2 = new Devinno.Forms.Controls.DvToggleButton();
            this.btnToggle1 = new Devinno.Forms.Controls.DvToggleButton();
            this.rad3 = new Devinno.Forms.Controls.DvRadioBox();
            this.rad2 = new Devinno.Forms.Controls.DvRadioBox();
            this.rad1 = new Devinno.Forms.Controls.DvRadioBox();
            this.chk3 = new Devinno.Forms.Controls.DvCheckBox();
            this.chk2 = new Devinno.Forms.Controls.DvCheckBox();
            this.chk1 = new Devinno.Forms.Controls.DvCheckBox();
            this.btnsType = new Devinno.Forms.Controls.DvButtons();
            this.btnTextAlign = new Devinno.Forms.Controls.DvButton();
            this.txtMultiLine = new Devinno.Forms.Controls.DvTextBox();
            this.txtNumber = new Devinno.Forms.Controls.DvTextBox();
            this.btnBlank = new Devinno.Forms.Controls.DvButton();
            this.lblNone = new Devinno.Forms.Controls.DvLabel();
            this.btnConvex = new Devinno.Forms.Controls.DvLabel();
            this.lblConcave = new Devinno.Forms.Controls.DvLabel();
            this.lblFlatConvex = new Devinno.Forms.Controls.DvLabel();
            this.lblFlatConcave = new Devinno.Forms.Controls.DvLabel();
            this.lblFlat = new Devinno.Forms.Controls.DvLabel();
            this.btnIcon = new Devinno.Forms.Controls.DvButton();
            this.btnGradient = new Devinno.Forms.Controls.DvButton();
            this.btnFlat = new Devinno.Forms.Controls.DvButton();
            this.tpGauge = new System.Windows.Forms.TabPage();
            this.tblGauge1 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.dvContainer6 = new Devinno.Forms.Containers.DvContainer();
            this.rangeV_T = new Devinno.Forms.Controls.DvRangeSliderV();
            this.rangeV_B = new Devinno.Forms.Controls.DvRangeSliderV();
            this.rangeV_G = new Devinno.Forms.Controls.DvRangeSliderV();
            this.rangeV_R = new Devinno.Forms.Controls.DvRangeSliderV();
            this.rangeV_N = new Devinno.Forms.Controls.DvRangeSliderV();
            this.dvContainer4 = new Devinno.Forms.Containers.DvContainer();
            this.sldV_T = new Devinno.Forms.Controls.DvSliderV();
            this.sldV_N = new Devinno.Forms.Controls.DvSliderV();
            this.sldV_R = new Devinno.Forms.Controls.DvSliderV();
            this.sldV_G = new Devinno.Forms.Controls.DvSliderV();
            this.sldV_B = new Devinno.Forms.Controls.DvSliderV();
            this.dvContainer3 = new Devinno.Forms.Containers.DvContainer();
            this.pgsV_T = new Devinno.Forms.Controls.DvProgressV();
            this.pgsV_N = new Devinno.Forms.Controls.DvProgressV();
            this.pgsV_R = new Devinno.Forms.Controls.DvProgressV();
            this.pgsV_G = new Devinno.Forms.Controls.DvProgressV();
            this.pgsV_B = new Devinno.Forms.Controls.DvProgressV();
            this.dvContainer2 = new Devinno.Forms.Containers.DvContainer();
            this.sldH_T = new Devinno.Forms.Controls.DvSliderH();
            this.sldH_N = new Devinno.Forms.Controls.DvSliderH();
            this.sldH_R = new Devinno.Forms.Controls.DvSliderH();
            this.sldH_B = new Devinno.Forms.Controls.DvSliderH();
            this.sldH_G = new Devinno.Forms.Controls.DvSliderH();
            this.sg = new Devinno.Forms.Controls.DvStepGauge();
            this.dvContainer1 = new Devinno.Forms.Containers.DvContainer();
            this.pgsH_T = new Devinno.Forms.Controls.DvProgressH();
            this.pgsH_N = new Devinno.Forms.Controls.DvProgressH();
            this.pgsH_R = new Devinno.Forms.Controls.DvProgressH();
            this.pgsH_G = new Devinno.Forms.Controls.DvProgressH();
            this.pgsH_B = new Devinno.Forms.Controls.DvProgressH();
            this.dvContainer5 = new Devinno.Forms.Containers.DvContainer();
            this.rangeH_T = new Devinno.Forms.Controls.DvRangeSliderH();
            this.rangeH_B = new Devinno.Forms.Controls.DvRangeSliderH();
            this.rangeH_G = new Devinno.Forms.Controls.DvRangeSliderH();
            this.rangeH_R = new Devinno.Forms.Controls.DvRangeSliderH();
            this.rangeH_N = new Devinno.Forms.Controls.DvRangeSliderH();
            this.tblGauge2 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.gauge = new Devinno.Forms.Controls.DvGauge();
            this.meter = new Devinno.Forms.Controls.DvMeter();
            this.knob = new Devinno.Forms.Controls.DvKnob();
            this.tpGraph = new System.Windows.Forms.TabPage();
            this.dvTableLayoutPanel1 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.btnPause = new Devinno.Forms.Controls.DvButton();
            this.tabGraph = new Devinno.Forms.Containers.DvTablessControl();
            this.tpBarGraphH = new System.Windows.Forms.TabPage();
            this.barGraphH = new Devinno.Forms.Controls.DvBarGraphH();
            this.tpBarGraphV = new System.Windows.Forms.TabPage();
            this.barGraphV = new Devinno.Forms.Controls.DvBarGraphV();
            this.tpCircleGraph = new System.Windows.Forms.TabPage();
            this.circleGraph = new Devinno.Forms.Controls.DvCircleGraph();
            this.tpLineGraph = new System.Windows.Forms.TabPage();
            this.lineGraph = new Devinno.Forms.Controls.DvLineGraph();
            this.tpTrendGraph = new System.Windows.Forms.TabPage();
            this.trendGraph = new Devinno.Forms.Controls.DvTrendGraph();
            this.tpTimeGraph = new System.Windows.Forms.TabPage();
            this.timeGraph = new Devinno.Forms.Controls.DvTimeGraph();
            this.menuGraph = new Devinno.Forms.Controls.DvButtons();
            this.btnGraphRefresh = new Devinno.Forms.Controls.DvButton();
            this.tpContainer = new System.Windows.Forms.TabPage();
            this.dvScrollablePanel1 = new Devinno.Forms.Containers.DvScrollablePanel();
            this.dvTableLayoutPanel2 = new Devinno.Forms.Containers.DvTableLayoutPanel();
            this.panel = new Devinno.Forms.Containers.DvPanel();
            this.pnlBox = new Devinno.Forms.Containers.DvBoxPanel();
            this.borderPanel = new Devinno.Forms.Containers.DvBorderPanel();
            this.grpBox = new Devinno.Forms.Containers.DvGroupBox();
            this.tab2 = new Devinno.Forms.Containers.DvTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tpDialog = new System.Windows.Forms.TabPage();
            this.btnPortSettingSimple = new Devinno.Forms.Controls.DvButton();
            this.btnPortSetting = new Devinno.Forms.Controls.DvButton();
            this.dvLabel6 = new Devinno.Forms.Controls.DvLabel();
            this.inCombo = new Devinno.Forms.Controls.DvValueInputCombo();
            this.btnSelCheck = new Devinno.Forms.Controls.DvButton();
            this.btnSelCombo = new Devinno.Forms.Controls.DvButton();
            this.btnSelRadio = new Devinno.Forms.Controls.DvButton();
            this.btnSelSelector = new Devinno.Forms.Controls.DvButton();
            this.btnSelWheel = new Devinno.Forms.Controls.DvButton();
            this.dvLabel5 = new Devinno.Forms.Controls.DvLabel();
            this.btnInputInt = new Devinno.Forms.Controls.DvButton();
            this.btnInputFloat = new Devinno.Forms.Controls.DvButton();
            this.btnInputString = new Devinno.Forms.Controls.DvButton();
            this.btnInputClass = new Devinno.Forms.Controls.DvButton();
            this.dvLabel4 = new Devinno.Forms.Controls.DvLabel();
            this.inDoW = new Devinno.Forms.Controls.DvValueInputWheel();
            this.datePicker = new Devinno.Forms.Controls.DvDateTimePicker();
            this.dvLabel3 = new Devinno.Forms.Controls.DvLabel();
            this.colorPicker = new Devinno.Forms.Controls.DvColorPicker();
            this.lblMb = new Devinno.Forms.Controls.DvLabel();
            this.btnMbYesNoCancel = new Devinno.Forms.Controls.DvButton();
            this.btnMbOkCancel = new Devinno.Forms.Controls.DvButton();
            this.btnMbYesNo = new Devinno.Forms.Controls.DvButton();
            this.btnMbOK = new Devinno.Forms.Controls.DvButton();
            this.dvLabel2 = new Devinno.Forms.Controls.DvLabel();
            this.lblKeyResult = new Devinno.Forms.Controls.DvLabel();
            this.btnKeyboardEng = new Devinno.Forms.Controls.DvButton();
            this.btnKeyboardHan = new Devinno.Forms.Controls.DvButton();
            this.btnKeypadPassword = new Devinno.Forms.Controls.DvButton();
            this.btnKeypadDecimal = new Devinno.Forms.Controls.DvButton();
            this.dvLabel1 = new Devinno.Forms.Controls.DvLabel();
            this.btnKeypadInt = new Devinno.Forms.Controls.DvButton();
            this.tpTable = new System.Windows.Forms.TabPage();
            this.dataGrid = new Devinno.Forms.Controls.DvDataGrid();
            this.btnTreeRemove = new Devinno.Forms.Controls.DvButton();
            this.btnTreeAdd = new Devinno.Forms.Controls.DvButton();
            this.treeView = new Devinno.Forms.Controls.DvTreeView();
            this.lblToolDrag = new Devinno.Forms.Controls.DvLabel();
            this.toolBox = new Devinno.Forms.Controls.DvToolBox();
            this.comboBox = new Devinno.Forms.Controls.DvComboBox();
            this.listBox = new Devinno.Forms.Controls.DvListBox();
            this.dvButton2 = new Devinno.Forms.Controls.DvButton();
            this.dvButton1 = new Devinno.Forms.Controls.DvButton();
            this.tab.SuspendLayout();
            this.tpControl.SuspendLayout();
            this.tblTriButton.SuspendLayout();
            this.tpGauge.SuspendLayout();
            this.tblGauge1.SuspendLayout();
            this.dvContainer6.SuspendLayout();
            this.dvContainer4.SuspendLayout();
            this.dvContainer3.SuspendLayout();
            this.dvContainer2.SuspendLayout();
            this.dvContainer1.SuspendLayout();
            this.dvContainer5.SuspendLayout();
            this.tblGauge2.SuspendLayout();
            this.tpGraph.SuspendLayout();
            this.dvTableLayoutPanel1.SuspendLayout();
            this.tabGraph.SuspendLayout();
            this.tpBarGraphH.SuspendLayout();
            this.tpBarGraphV.SuspendLayout();
            this.tpCircleGraph.SuspendLayout();
            this.tpLineGraph.SuspendLayout();
            this.tpTrendGraph.SuspendLayout();
            this.tpTimeGraph.SuspendLayout();
            this.tpContainer.SuspendLayout();
            this.dvScrollablePanel1.SuspendLayout();
            this.dvTableLayoutPanel2.SuspendLayout();
            this.tab2.SuspendLayout();
            this.tpDialog.SuspendLayout();
            this.tpTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tab.Controls.Add(this.tpControl);
            this.tab.Controls.Add(this.tpGauge);
            this.tab.Controls.Add(this.tpGraph);
            this.tab.Controls.Add(this.tpContainer);
            this.tab.Controls.Add(this.tpDialog);
            this.tab.Controls.Add(this.tpTable);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.ItemSize = new System.Drawing.Size(0, 1);
            this.tab.Location = new System.Drawing.Point(7, 40);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(726, 723);
            this.tab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab.TabIndex = 0;
            // 
            // tpControl
            // 
            this.tpControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpControl.Controls.Add(this.inOnOff);
            this.tpControl.Controls.Add(this.inTemp);
            this.tpControl.Controls.Add(this.inPos);
            this.tpControl.Controls.Add(this.inName);
            this.tpControl.Controls.Add(this.vlblName);
            this.tpControl.Controls.Add(this.vlblPos);
            this.tpControl.Controls.Add(this.vlblTemp);
            this.tpControl.Controls.Add(this.vlblOnOff);
            this.tpControl.Controls.Add(this.ani);
            this.tpControl.Controls.Add(this.calendar);
            this.tpControl.Controls.Add(this.sels);
            this.tpControl.Controls.Add(this.pic);
            this.tpControl.Controls.Add(this.nbR);
            this.tpControl.Controls.Add(this.nbUD);
            this.tpControl.Controls.Add(this.nbLR);
            this.tpControl.Controls.Add(this.nbRUD);
            this.tpControl.Controls.Add(this.sw);
            this.tpControl.Controls.Add(this.onoff);
            this.tpControl.Controls.Add(this.lmp3);
            this.tpControl.Controls.Add(this.lmp2);
            this.tpControl.Controls.Add(this.lmp1);
            this.tpControl.Controls.Add(this.btnLampPump);
            this.tpControl.Controls.Add(this.btnLampMotor);
            this.tpControl.Controls.Add(this.tblTriButton);
            this.tpControl.Controls.Add(this.btnCircleUp);
            this.tpControl.Controls.Add(this.btnCircleDown);
            this.tpControl.Controls.Add(this.btnRadio2);
            this.tpControl.Controls.Add(this.btnRadio1);
            this.tpControl.Controls.Add(this.btnToggle2);
            this.tpControl.Controls.Add(this.btnToggle1);
            this.tpControl.Controls.Add(this.rad3);
            this.tpControl.Controls.Add(this.rad2);
            this.tpControl.Controls.Add(this.rad1);
            this.tpControl.Controls.Add(this.chk3);
            this.tpControl.Controls.Add(this.chk2);
            this.tpControl.Controls.Add(this.chk1);
            this.tpControl.Controls.Add(this.btnsType);
            this.tpControl.Controls.Add(this.btnTextAlign);
            this.tpControl.Controls.Add(this.txtMultiLine);
            this.tpControl.Controls.Add(this.txtNumber);
            this.tpControl.Controls.Add(this.btnBlank);
            this.tpControl.Controls.Add(this.lblNone);
            this.tpControl.Controls.Add(this.btnConvex);
            this.tpControl.Controls.Add(this.lblConcave);
            this.tpControl.Controls.Add(this.lblFlatConvex);
            this.tpControl.Controls.Add(this.lblFlatConcave);
            this.tpControl.Controls.Add(this.lblFlat);
            this.tpControl.Controls.Add(this.btnIcon);
            this.tpControl.Controls.Add(this.btnGradient);
            this.tpControl.Controls.Add(this.btnFlat);
            this.tpControl.Location = new System.Drawing.Point(4, 5);
            this.tpControl.Name = "tpControl";
            this.tpControl.Padding = new System.Windows.Forms.Padding(10);
            this.tpControl.Size = new System.Drawing.Size(718, 714);
            this.tpControl.TabIndex = 0;
            this.tpControl.Text = "tabPage1";
            // 
            // inOnOff
            // 
            this.inOnOff.Button = null;
            this.inOnOff.ButtonColor = null;
            this.inOnOff.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inOnOff.ButtonIconGap = 0;
            this.inOnOff.ButtonIconImage = null;
            this.inOnOff.ButtonIconSize = 10F;
            this.inOnOff.ButtonIconString = null;
            this.inOnOff.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inOnOff.ButtonWidth = null;
            this.inOnOff.Location = new System.Drawing.Point(179, 671);
            this.inOnOff.Name = "inOnOff";
            this.inOnOff.Off = "OFF";
            this.inOnOff.On = "ON";
            this.inOnOff.Round = null;
            this.inOnOff.ShadowGap = 1;
            this.inOnOff.Size = new System.Drawing.Size(150, 30);
            this.inOnOff.TabIndex = 54;
            this.inOnOff.Text = "dvValueInputBoolean1";
            this.inOnOff.Title = null;
            this.inOnOff.TitleColor = null;
            this.inOnOff.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inOnOff.TitleIconGap = 0;
            this.inOnOff.TitleIconImage = null;
            this.inOnOff.TitleIconSize = 10F;
            this.inOnOff.TitleIconString = null;
            this.inOnOff.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inOnOff.TitleWidth = null;
            this.inOnOff.Unit = "";
            this.inOnOff.UnitWidth = null;
            this.inOnOff.Value = false;
            this.inOnOff.ValueColor = null;
            // 
            // inTemp
            // 
            this.inTemp.Button = null;
            this.inTemp.ButtonColor = null;
            this.inTemp.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inTemp.ButtonIconGap = 0;
            this.inTemp.ButtonIconImage = null;
            this.inTemp.ButtonIconSize = 10F;
            this.inTemp.ButtonIconString = "fa-pencil";
            this.inTemp.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inTemp.ButtonWidth = 30;
            this.inTemp.ErrorColor = null;
            this.inTemp.Location = new System.Drawing.Point(179, 635);
            this.inTemp.Maximum = 300F;
            this.inTemp.Minimum = -20F;
            this.inTemp.Name = "inTemp";
            this.inTemp.Round = null;
            this.inTemp.ShadowGap = 1;
            this.inTemp.Size = new System.Drawing.Size(150, 30);
            this.inTemp.TabIndex = 53;
            this.inTemp.Text = "온도";
            this.inTemp.Title = "온도";
            this.inTemp.TitleColor = null;
            this.inTemp.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inTemp.TitleIconGap = 0;
            this.inTemp.TitleIconImage = null;
            this.inTemp.TitleIconSize = 10F;
            this.inTemp.TitleIconString = null;
            this.inTemp.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inTemp.TitleWidth = 40;
            this.inTemp.Unit = "℃";
            this.inTemp.UnitWidth = 30;
            this.inTemp.Value = 0F;
            this.inTemp.ValueColor = null;
            // 
            // inPos
            // 
            this.inPos.Button = null;
            this.inPos.ButtonColor = null;
            this.inPos.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inPos.ButtonIconGap = 0;
            this.inPos.ButtonIconImage = null;
            this.inPos.ButtonIconSize = 10F;
            this.inPos.ButtonIconString = null;
            this.inPos.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inPos.ButtonWidth = null;
            this.inPos.ErrorColor = null;
            this.inPos.Location = new System.Drawing.Point(179, 599);
            this.inPos.Maximum = 1000;
            this.inPos.Minimum = 0;
            this.inPos.Name = "inPos";
            this.inPos.Round = null;
            this.inPos.ShadowGap = 1;
            this.inPos.Size = new System.Drawing.Size(150, 30);
            this.inPos.TabIndex = 52;
            this.inPos.Text = "위치";
            this.inPos.Title = "위치";
            this.inPos.TitleColor = null;
            this.inPos.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inPos.TitleIconGap = 0;
            this.inPos.TitleIconImage = null;
            this.inPos.TitleIconSize = 10F;
            this.inPos.TitleIconString = null;
            this.inPos.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inPos.TitleWidth = 40;
            this.inPos.Unit = "m";
            this.inPos.UnitWidth = 30;
            this.inPos.Value = 0;
            this.inPos.ValueColor = null;
            // 
            // inName
            // 
            this.inName.Button = null;
            this.inName.ButtonColor = null;
            this.inName.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inName.ButtonIconGap = 0;
            this.inName.ButtonIconImage = null;
            this.inName.ButtonIconSize = 10F;
            this.inName.ButtonIconString = null;
            this.inName.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inName.ButtonWidth = null;
            this.inName.Location = new System.Drawing.Point(179, 563);
            this.inName.Name = "inName";
            this.inName.Round = null;
            this.inName.ShadowGap = 1;
            this.inName.Size = new System.Drawing.Size(150, 30);
            this.inName.TabIndex = 51;
            this.inName.Text = "명칭";
            this.inName.Title = "명칭";
            this.inName.TitleColor = null;
            this.inName.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inName.TitleIconGap = 0;
            this.inName.TitleIconImage = null;
            this.inName.TitleIconSize = 10F;
            this.inName.TitleIconString = null;
            this.inName.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inName.TitleWidth = 40;
            this.inName.Unit = "";
            this.inName.UnitWidth = null;
            this.inName.Value = "";
            this.inName.ValueColor = null;
            // 
            // vlblName
            // 
            this.vlblName.Button = null;
            this.vlblName.ButtonColor = null;
            this.vlblName.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblName.ButtonIconGap = 0;
            this.vlblName.ButtonIconImage = null;
            this.vlblName.ButtonIconSize = 10F;
            this.vlblName.ButtonIconString = null;
            this.vlblName.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblName.ButtonWidth = null;
            this.vlblName.Location = new System.Drawing.Point(13, 563);
            this.vlblName.Name = "vlblName";
            this.vlblName.Round = null;
            this.vlblName.ShadowGap = 1;
            this.vlblName.Size = new System.Drawing.Size(150, 30);
            this.vlblName.TabIndex = 50;
            this.vlblName.Text = "명칭";
            this.vlblName.Title = "명칭";
            this.vlblName.TitleColor = null;
            this.vlblName.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblName.TitleIconGap = 0;
            this.vlblName.TitleIconImage = null;
            this.vlblName.TitleIconSize = 10F;
            this.vlblName.TitleIconString = null;
            this.vlblName.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblName.TitleWidth = 40;
            this.vlblName.Unit = "";
            this.vlblName.UnitWidth = null;
            this.vlblName.Value = "";
            this.vlblName.ValueColor = null;
            // 
            // vlblPos
            // 
            this.vlblPos.Button = null;
            this.vlblPos.ButtonColor = null;
            this.vlblPos.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblPos.ButtonIconGap = 0;
            this.vlblPos.ButtonIconImage = null;
            this.vlblPos.ButtonIconSize = 10F;
            this.vlblPos.ButtonIconString = null;
            this.vlblPos.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblPos.ButtonWidth = null;
            this.vlblPos.FormatString = null;
            this.vlblPos.Location = new System.Drawing.Point(13, 599);
            this.vlblPos.Name = "vlblPos";
            this.vlblPos.Round = null;
            this.vlblPos.ShadowGap = 1;
            this.vlblPos.Size = new System.Drawing.Size(150, 30);
            this.vlblPos.TabIndex = 49;
            this.vlblPos.Text = "위치";
            this.vlblPos.Title = "위치";
            this.vlblPos.TitleColor = null;
            this.vlblPos.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblPos.TitleIconGap = 0;
            this.vlblPos.TitleIconImage = null;
            this.vlblPos.TitleIconSize = 10F;
            this.vlblPos.TitleIconString = null;
            this.vlblPos.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblPos.TitleWidth = 40;
            this.vlblPos.Unit = "m";
            this.vlblPos.UnitWidth = 30;
            this.vlblPos.Value = 0;
            this.vlblPos.ValueColor = null;
            // 
            // vlblTemp
            // 
            this.vlblTemp.Button = null;
            this.vlblTemp.ButtonColor = null;
            this.vlblTemp.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblTemp.ButtonIconGap = 0;
            this.vlblTemp.ButtonIconImage = null;
            this.vlblTemp.ButtonIconSize = 10F;
            this.vlblTemp.ButtonIconString = "fa-pencil";
            this.vlblTemp.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblTemp.ButtonWidth = 30;
            this.vlblTemp.FormatString = "0.0";
            this.vlblTemp.Location = new System.Drawing.Point(13, 635);
            this.vlblTemp.Name = "vlblTemp";
            this.vlblTemp.Round = null;
            this.vlblTemp.ShadowGap = 1;
            this.vlblTemp.Size = new System.Drawing.Size(150, 30);
            this.vlblTemp.TabIndex = 48;
            this.vlblTemp.Text = "온도";
            this.vlblTemp.Title = "온도";
            this.vlblTemp.TitleColor = null;
            this.vlblTemp.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblTemp.TitleIconGap = 0;
            this.vlblTemp.TitleIconImage = null;
            this.vlblTemp.TitleIconSize = 10F;
            this.vlblTemp.TitleIconString = null;
            this.vlblTemp.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblTemp.TitleWidth = 40;
            this.vlblTemp.Unit = "℃";
            this.vlblTemp.UnitWidth = 30;
            this.vlblTemp.Value = 0F;
            this.vlblTemp.ValueColor = null;
            // 
            // vlblOnOff
            // 
            this.vlblOnOff.Button = null;
            this.vlblOnOff.ButtonColor = null;
            this.vlblOnOff.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblOnOff.ButtonIconGap = 0;
            this.vlblOnOff.ButtonIconImage = null;
            this.vlblOnOff.ButtonIconSize = 10F;
            this.vlblOnOff.ButtonIconString = null;
            this.vlblOnOff.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblOnOff.ButtonWidth = null;
            this.vlblOnOff.Location = new System.Drawing.Point(13, 671);
            this.vlblOnOff.Name = "vlblOnOff";
            this.vlblOnOff.Off = "OFF";
            this.vlblOnOff.On = "ON";
            this.vlblOnOff.Round = null;
            this.vlblOnOff.ShadowGap = 1;
            this.vlblOnOff.Size = new System.Drawing.Size(150, 30);
            this.vlblOnOff.TabIndex = 47;
            this.vlblOnOff.Title = "";
            this.vlblOnOff.TitleColor = null;
            this.vlblOnOff.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.vlblOnOff.TitleIconGap = 0;
            this.vlblOnOff.TitleIconImage = null;
            this.vlblOnOff.TitleIconSize = 10F;
            this.vlblOnOff.TitleIconString = null;
            this.vlblOnOff.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.vlblOnOff.TitleWidth = null;
            this.vlblOnOff.Unit = "";
            this.vlblOnOff.UnitWidth = null;
            this.vlblOnOff.Value = false;
            this.vlblOnOff.ValueColor = null;
            // 
            // ani
            // 
            this.ani.Interval = 10;
            this.ani.Location = new System.Drawing.Point(345, 563);
            this.ani.Name = "ani";
            this.ani.OffImage = null;
            this.ani.OnOff = false;
            this.ani.ShadowGap = 1;
            this.ani.Size = new System.Drawing.Size(138, 138);
            this.ani.TabIndex = 46;
            this.ani.TabStop = false;
            this.ani.Text = "dvAnimate1";
            // 
            // calendar
            // 
            this.calendar.DaysBoxColor = null;
            this.calendar.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calendar.Location = new System.Drawing.Point(499, 563);
            this.calendar.MonthlyBoxColor = null;
            this.calendar.MultiSelect = false;
            this.calendar.Name = "calendar";
            this.calendar.NoneSelect = false;
            this.calendar.Round = null;
            this.calendar.SelectColor = null;
            this.calendar.ShadowGap = 1;
            this.calendar.Size = new System.Drawing.Size(206, 138);
            this.calendar.TabIndex = 45;
            this.calendar.Text = "dvCalendar1";
            this.calendar.WeeklyBoxColor = null;
            this.calendar.WeeklyFont = new System.Drawing.Font("나눔고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // sels
            // 
            this.sels.BackgroundDraw = true;
            this.sels.ButtonWidth = null;
            this.sels.Location = new System.Drawing.Point(499, 361);
            this.sels.Name = "sels";
            this.sels.Round = null;
            this.sels.SelectedIndex = -1;
            this.sels.SelectorColor = null;
            this.sels.ShadowGap = 1;
            this.sels.Size = new System.Drawing.Size(206, 30);
            this.sels.Style = Devinno.Forms.Embossing.Convex;
            this.sels.TabIndex = 44;
            this.sels.Text = "dvSelector1";
            // 
            // pic
            // 
            this.pic.BoxColor = null;
            this.pic.BoxDraw = true;
            this.pic.Image = global::Sample.Properties.Resources.logo3;
            this.pic.Location = new System.Drawing.Point(345, 443);
            this.pic.Name = "pic";
            this.pic.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pic.ScaleMode = Devinno.Forms.DvPictureScaleMode.CenterImage;
            this.pic.ShadowGap = 1;
            this.pic.Size = new System.Drawing.Size(138, 102);
            this.pic.TabIndex = 42;
            this.pic.TabStop = false;
            this.pic.Text = "dvPictureBox1";
            // 
            // nbR
            // 
            this.nbR.ButtonColor = null;
            this.nbR.ButtonSize = 30;
            this.nbR.FormatString = "0";
            this.nbR.Gradient = true;
            this.nbR.Location = new System.Drawing.Point(13, 479);
            this.nbR.Maximum = 100D;
            this.nbR.Minimum = 0D;
            this.nbR.Name = "nbR";
            this.nbR.Round = null;
            this.nbR.ShadowGap = 1;
            this.nbR.Size = new System.Drawing.Size(150, 30);
            this.nbR.Style = Devinno.Forms.DvNumberBoxStyle.Right;
            this.nbR.TabIndex = 41;
            this.nbR.Text = "dvNumberBox4";
            this.nbR.Tick = 1D;
            this.nbR.Value = 0D;
            this.nbR.ValueBoxColor = null;
            // 
            // nbUD
            // 
            this.nbUD.ButtonColor = null;
            this.nbUD.ButtonSize = 30;
            this.nbUD.FormatString = "0";
            this.nbUD.Gradient = true;
            this.nbUD.Location = new System.Drawing.Point(179, 443);
            this.nbUD.Maximum = 100D;
            this.nbUD.Minimum = 0D;
            this.nbUD.Name = "nbUD";
            this.nbUD.Round = null;
            this.nbUD.ShadowGap = 1;
            this.nbUD.Size = new System.Drawing.Size(150, 102);
            this.nbUD.Style = Devinno.Forms.DvNumberBoxStyle.UpDown;
            this.nbUD.TabIndex = 40;
            this.nbUD.Text = "dvNumberBox3";
            this.nbUD.Tick = 1D;
            this.nbUD.Value = 0D;
            this.nbUD.ValueBoxColor = null;
            // 
            // nbLR
            // 
            this.nbLR.ButtonColor = null;
            this.nbLR.ButtonSize = 30;
            this.nbLR.FormatString = "0";
            this.nbLR.Gradient = true;
            this.nbLR.Location = new System.Drawing.Point(13, 515);
            this.nbLR.Maximum = 100D;
            this.nbLR.Minimum = 0D;
            this.nbLR.Name = "nbLR";
            this.nbLR.Round = null;
            this.nbLR.ShadowGap = 1;
            this.nbLR.Size = new System.Drawing.Size(150, 30);
            this.nbLR.Style = Devinno.Forms.DvNumberBoxStyle.LeftRight;
            this.nbLR.TabIndex = 39;
            this.nbLR.Text = "dvNumberBox2";
            this.nbLR.Tick = 1D;
            this.nbLR.Value = 0D;
            this.nbLR.ValueBoxColor = null;
            // 
            // nbRUD
            // 
            this.nbRUD.ButtonColor = null;
            this.nbRUD.ButtonSize = 30;
            this.nbRUD.FormatString = "0";
            this.nbRUD.Gradient = true;
            this.nbRUD.Location = new System.Drawing.Point(13, 443);
            this.nbRUD.Maximum = 100D;
            this.nbRUD.Minimum = 0D;
            this.nbRUD.Name = "nbRUD";
            this.nbRUD.Round = null;
            this.nbRUD.ShadowGap = 1;
            this.nbRUD.Size = new System.Drawing.Size(150, 30);
            this.nbRUD.Style = Devinno.Forms.DvNumberBoxStyle.RightUpDown;
            this.nbRUD.TabIndex = 1;
            this.nbRUD.Text = "dvNumberBox1";
            this.nbRUD.Tick = 1D;
            this.nbRUD.Value = 0D;
            this.nbRUD.ValueBoxColor = null;
            // 
            // sw
            // 
            this.sw.BoxColor = null;
            this.sw.Location = new System.Drawing.Point(499, 277);
            this.sw.Name = "sw";
            this.sw.OffLampColor = null;
            this.sw.OffText = "OFF";
            this.sw.OnLampColor = null;
            this.sw.OnOff = false;
            this.sw.OnText = "ON";
            this.sw.ShadowGap = 1;
            this.sw.Size = new System.Drawing.Size(206, 66);
            this.sw.SwitchColor = null;
            this.sw.TabIndex = 38;
            this.sw.Text = "dvSwitch1";
            // 
            // onoff
            // 
            this.onoff.CursorColor = null;
            this.onoff.Location = new System.Drawing.Point(499, 193);
            this.onoff.Name = "onoff";
            this.onoff.OffBoxColor = null;
            this.onoff.OffText = "OFF";
            this.onoff.OffTextColor = null;
            this.onoff.OnBoxColor = null;
            this.onoff.OnOff = false;
            this.onoff.OnText = "ON";
            this.onoff.OnTextColor = null;
            this.onoff.ShadowGap = 1;
            this.onoff.Size = new System.Drawing.Size(206, 66);
            this.onoff.TabIndex = 37;
            this.onoff.Text = "dvOnOff1";
            // 
            // lmp3
            // 
            this.lmp3.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lmp3.LampAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lmp3.LampGap = 5;
            this.lmp3.LampSize = 26;
            this.lmp3.Location = new System.Drawing.Point(345, 313);
            this.lmp3.Name = "lmp3";
            this.lmp3.OffLampColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lmp3.OnLampColor = System.Drawing.Color.Blue;
            this.lmp3.OnOff = false;
            this.lmp3.ShadowGap = 1;
            this.lmp3.Size = new System.Drawing.Size(138, 30);
            this.lmp3.TabIndex = 36;
            this.lmp3.TabStop = false;
            this.lmp3.Text = "Lamp 3";
            this.lmp3.TextPadding = new System.Windows.Forms.Padding(0);
            // 
            // lmp2
            // 
            this.lmp2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lmp2.LampAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lmp2.LampGap = 5;
            this.lmp2.LampSize = 26;
            this.lmp2.Location = new System.Drawing.Point(345, 277);
            this.lmp2.Name = "lmp2";
            this.lmp2.OffLampColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lmp2.OnLampColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(0)))));
            this.lmp2.OnOff = false;
            this.lmp2.ShadowGap = 1;
            this.lmp2.Size = new System.Drawing.Size(138, 30);
            this.lmp2.TabIndex = 35;
            this.lmp2.TabStop = false;
            this.lmp2.Text = "Lamp 2";
            this.lmp2.TextPadding = new System.Windows.Forms.Padding(0);
            // 
            // lmp1
            // 
            this.lmp1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleLeft;
            this.lmp1.LampAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lmp1.LampGap = 5;
            this.lmp1.LampSize = 26;
            this.lmp1.Location = new System.Drawing.Point(345, 241);
            this.lmp1.Name = "lmp1";
            this.lmp1.OffLampColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lmp1.OnLampColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lmp1.OnOff = false;
            this.lmp1.ShadowGap = 1;
            this.lmp1.Size = new System.Drawing.Size(138, 30);
            this.lmp1.TabIndex = 34;
            this.lmp1.TabStop = false;
            this.lmp1.Text = "Lamp 1";
            this.lmp1.TextPadding = new System.Windows.Forms.Padding(0);
            // 
            // btnLampPump
            // 
            this.btnLampPump.ButtonColor = null;
            this.btnLampPump.Clickable = true;
            this.btnLampPump.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnLampPump.Gradient = true;
            this.btnLampPump.LampAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnLampPump.LampGap = 3;
            this.btnLampPump.LampSize = 20;
            this.btnLampPump.Location = new System.Drawing.Point(345, 397);
            this.btnLampPump.Name = "btnLampPump";
            this.btnLampPump.OffLampColor = null;
            this.btnLampPump.OnLampColor = System.Drawing.Color.LimeGreen;
            this.btnLampPump.OnOff = false;
            this.btnLampPump.Round = null;
            this.btnLampPump.ShadowGap = 1;
            this.btnLampPump.Size = new System.Drawing.Size(138, 30);
            this.btnLampPump.TabIndex = 33;
            this.btnLampPump.Text = "Pump";
            this.btnLampPump.TextPadding = new System.Windows.Forms.Padding(5);
            this.btnLampPump.UseKey = false;
            // 
            // btnLampMotor
            // 
            this.btnLampMotor.ButtonColor = null;
            this.btnLampMotor.Clickable = true;
            this.btnLampMotor.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnLampMotor.Gradient = true;
            this.btnLampMotor.LampAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnLampMotor.LampGap = 3;
            this.btnLampMotor.LampSize = 20;
            this.btnLampMotor.Location = new System.Drawing.Point(345, 361);
            this.btnLampMotor.Name = "btnLampMotor";
            this.btnLampMotor.OffLampColor = null;
            this.btnLampMotor.OnLampColor = null;
            this.btnLampMotor.OnOff = false;
            this.btnLampMotor.Round = null;
            this.btnLampMotor.ShadowGap = 1;
            this.btnLampMotor.Size = new System.Drawing.Size(138, 30);
            this.btnLampMotor.TabIndex = 32;
            this.btnLampMotor.Text = "Motor";
            this.btnLampMotor.TextPadding = new System.Windows.Forms.Padding(5);
            this.btnLampMotor.UseKey = false;
            // 
            // tblTriButton
            // 
            this.tblTriButton.ColumnCount = 3;
            this.tblTriButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblTriButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblTriButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblTriButton.Controls.Add(this.btnTriangleLeft, 0, 0);
            this.tblTriButton.Controls.Add(this.btnTriangleRight, 2, 0);
            this.tblTriButton.Controls.Add(this.btnTriangleDown, 1, 1);
            this.tblTriButton.Controls.Add(this.btnTriableUp, 1, 0);
            this.tblTriButton.Location = new System.Drawing.Point(499, 13);
            this.tblTriButton.Name = "tblTriButton";
            this.tblTriButton.RowCount = 2;
            this.tblTriButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTriButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblTriButton.Size = new System.Drawing.Size(206, 174);
            this.tblTriButton.TabIndex = 31;
            // 
            // btnTriangleLeft
            // 
            this.btnTriangleLeft.BoxColor = null;
            this.btnTriangleLeft.ButtonColor = null;
            this.btnTriangleLeft.Clickable = true;
            this.btnTriangleLeft.Corner = null;
            this.btnTriangleLeft.Direction = Devinno.Forms.DvDirection.Left;
            this.btnTriangleLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriangleLeft.Gradient = true;
            this.btnTriangleLeft.Location = new System.Drawing.Point(0, 0);
            this.btnTriangleLeft.Margin = new System.Windows.Forms.Padding(0);
            this.btnTriangleLeft.Name = "btnTriangleLeft";
            this.tblTriButton.SetRowSpan(this.btnTriangleLeft, 2);
            this.btnTriangleLeft.ShadowGap = 1;
            this.btnTriangleLeft.Size = new System.Drawing.Size(67, 174);
            this.btnTriangleLeft.TabIndex = 27;
            this.btnTriangleLeft.Text = "dvTriangleButton1";
            this.btnTriangleLeft.UseKey = false;
            // 
            // btnTriangleRight
            // 
            this.btnTriangleRight.BoxColor = null;
            this.btnTriangleRight.ButtonColor = null;
            this.btnTriangleRight.Clickable = true;
            this.btnTriangleRight.Corner = null;
            this.btnTriangleRight.Direction = Devinno.Forms.DvDirection.Right;
            this.btnTriangleRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriangleRight.Gradient = true;
            this.btnTriangleRight.Location = new System.Drawing.Point(137, 0);
            this.btnTriangleRight.Margin = new System.Windows.Forms.Padding(0);
            this.btnTriangleRight.Name = "btnTriangleRight";
            this.tblTriButton.SetRowSpan(this.btnTriangleRight, 2);
            this.btnTriangleRight.ShadowGap = 1;
            this.btnTriangleRight.Size = new System.Drawing.Size(69, 174);
            this.btnTriangleRight.TabIndex = 28;
            this.btnTriangleRight.Text = "dvTriangleButton2";
            this.btnTriangleRight.UseKey = false;
            // 
            // btnTriangleDown
            // 
            this.btnTriangleDown.BoxColor = null;
            this.btnTriangleDown.ButtonColor = null;
            this.btnTriangleDown.Clickable = true;
            this.btnTriangleDown.Corner = null;
            this.btnTriangleDown.Direction = Devinno.Forms.DvDirection.Down;
            this.btnTriangleDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriangleDown.Gradient = true;
            this.btnTriangleDown.Location = new System.Drawing.Point(67, 87);
            this.btnTriangleDown.Margin = new System.Windows.Forms.Padding(0);
            this.btnTriangleDown.Name = "btnTriangleDown";
            this.btnTriangleDown.ShadowGap = 1;
            this.btnTriangleDown.Size = new System.Drawing.Size(70, 87);
            this.btnTriangleDown.TabIndex = 30;
            this.btnTriangleDown.Text = "dvTriangleButton4";
            this.btnTriangleDown.UseKey = false;
            // 
            // btnTriableUp
            // 
            this.btnTriableUp.BoxColor = null;
            this.btnTriableUp.ButtonColor = null;
            this.btnTriableUp.Clickable = true;
            this.btnTriableUp.Corner = null;
            this.btnTriableUp.Direction = Devinno.Forms.DvDirection.Up;
            this.btnTriableUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTriableUp.Gradient = true;
            this.btnTriableUp.Location = new System.Drawing.Point(67, 0);
            this.btnTriableUp.Margin = new System.Windows.Forms.Padding(0);
            this.btnTriableUp.Name = "btnTriableUp";
            this.btnTriableUp.ShadowGap = 1;
            this.btnTriableUp.Size = new System.Drawing.Size(70, 87);
            this.btnTriableUp.TabIndex = 29;
            this.btnTriableUp.Text = "dvTriangleButton3";
            this.btnTriableUp.UseKey = false;
            // 
            // btnCircleUp
            // 
            this.btnCircleUp.BackgroundDraw = true;
            this.btnCircleUp.ButtonBackColor = null;
            this.btnCircleUp.ButtonColor = null;
            this.btnCircleUp.Clickable = true;
            this.btnCircleUp.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCircleUp.Gradient = true;
            this.btnCircleUp.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCircleUp.IconGap = 0;
            this.btnCircleUp.IconImage = null;
            this.btnCircleUp.IconSize = 18F;
            this.btnCircleUp.IconString = "fa-chevron-Left";
            this.btnCircleUp.Location = new System.Drawing.Point(499, 443);
            this.btnCircleUp.Name = "btnCircleUp";
            this.btnCircleUp.ShadowGap = 1;
            this.btnCircleUp.Size = new System.Drawing.Size(100, 102);
            this.btnCircleUp.TabIndex = 26;
            this.btnCircleUp.Text = null;
            this.btnCircleUp.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCircleUp.UseKey = false;
            // 
            // btnCircleDown
            // 
            this.btnCircleDown.BackgroundDraw = true;
            this.btnCircleDown.ButtonBackColor = null;
            this.btnCircleDown.ButtonColor = null;
            this.btnCircleDown.Clickable = true;
            this.btnCircleDown.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnCircleDown.Gradient = true;
            this.btnCircleDown.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnCircleDown.IconGap = 0;
            this.btnCircleDown.IconImage = null;
            this.btnCircleDown.IconSize = 18F;
            this.btnCircleDown.IconString = "fa-chevron-right";
            this.btnCircleDown.Location = new System.Drawing.Point(605, 443);
            this.btnCircleDown.Name = "btnCircleDown";
            this.btnCircleDown.ShadowGap = 1;
            this.btnCircleDown.Size = new System.Drawing.Size(100, 102);
            this.btnCircleDown.TabIndex = 25;
            this.btnCircleDown.Text = null;
            this.btnCircleDown.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnCircleDown.UseKey = false;
            // 
            // btnRadio2
            // 
            this.btnRadio2.ButtonColor = null;
            this.btnRadio2.Checked = false;
            this.btnRadio2.CheckedButtonColor = null;
            this.btnRadio2.Clickable = true;
            this.btnRadio2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnRadio2.Gradient = true;
            this.btnRadio2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnRadio2.IconGap = 0;
            this.btnRadio2.IconImage = null;
            this.btnRadio2.IconSize = 10F;
            this.btnRadio2.IconString = null;
            this.btnRadio2.Location = new System.Drawing.Point(179, 397);
            this.btnRadio2.Name = "btnRadio2";
            this.btnRadio2.Round = null;
            this.btnRadio2.ShadowGap = 1;
            this.btnRadio2.Size = new System.Drawing.Size(150, 30);
            this.btnRadio2.TabIndex = 24;
            this.btnRadio2.Text = "Radio 2";
            this.btnRadio2.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnRadio2.UseKey = false;
            // 
            // btnRadio1
            // 
            this.btnRadio1.ButtonColor = null;
            this.btnRadio1.Checked = false;
            this.btnRadio1.CheckedButtonColor = null;
            this.btnRadio1.Clickable = true;
            this.btnRadio1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnRadio1.Gradient = true;
            this.btnRadio1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnRadio1.IconGap = 0;
            this.btnRadio1.IconImage = null;
            this.btnRadio1.IconSize = 10F;
            this.btnRadio1.IconString = null;
            this.btnRadio1.Location = new System.Drawing.Point(179, 361);
            this.btnRadio1.Name = "btnRadio1";
            this.btnRadio1.Round = null;
            this.btnRadio1.ShadowGap = 1;
            this.btnRadio1.Size = new System.Drawing.Size(150, 30);
            this.btnRadio1.TabIndex = 23;
            this.btnRadio1.Text = "Radio 1";
            this.btnRadio1.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnRadio1.UseKey = false;
            // 
            // btnToggle2
            // 
            this.btnToggle2.ButtonColor = null;
            this.btnToggle2.Checked = false;
            this.btnToggle2.CheckedButtonColor = null;
            this.btnToggle2.Clickable = true;
            this.btnToggle2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnToggle2.Gradient = true;
            this.btnToggle2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnToggle2.IconGap = 0;
            this.btnToggle2.IconImage = null;
            this.btnToggle2.IconSize = 10F;
            this.btnToggle2.IconString = null;
            this.btnToggle2.Location = new System.Drawing.Point(13, 397);
            this.btnToggle2.Name = "btnToggle2";
            this.btnToggle2.Round = null;
            this.btnToggle2.ShadowGap = 1;
            this.btnToggle2.Size = new System.Drawing.Size(150, 30);
            this.btnToggle2.TabIndex = 22;
            this.btnToggle2.Text = "Toggle 2";
            this.btnToggle2.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnToggle2.UseKey = false;
            // 
            // btnToggle1
            // 
            this.btnToggle1.ButtonColor = null;
            this.btnToggle1.Checked = false;
            this.btnToggle1.CheckedButtonColor = null;
            this.btnToggle1.Clickable = true;
            this.btnToggle1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnToggle1.Gradient = true;
            this.btnToggle1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnToggle1.IconGap = 0;
            this.btnToggle1.IconImage = null;
            this.btnToggle1.IconSize = 10F;
            this.btnToggle1.IconString = null;
            this.btnToggle1.Location = new System.Drawing.Point(13, 361);
            this.btnToggle1.Name = "btnToggle1";
            this.btnToggle1.Round = null;
            this.btnToggle1.ShadowGap = 1;
            this.btnToggle1.Size = new System.Drawing.Size(150, 30);
            this.btnToggle1.TabIndex = 21;
            this.btnToggle1.Text = "Toggle 1";
            this.btnToggle1.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnToggle1.UseKey = false;
            // 
            // rad3
            // 
            this.rad3.BoxColor = null;
            this.rad3.BoxSize = 20;
            this.rad3.Checked = false;
            this.rad3.Location = new System.Drawing.Point(179, 313);
            this.rad3.Name = "rad3";
            this.rad3.RadioColor = System.Drawing.Color.Yellow;
            this.rad3.ShadowGap = 1;
            this.rad3.Size = new System.Drawing.Size(150, 30);
            this.rad3.TabIndex = 20;
            this.rad3.Text = "RadioBox 3";
            // 
            // rad2
            // 
            this.rad2.BoxColor = null;
            this.rad2.BoxSize = 20;
            this.rad2.Checked = false;
            this.rad2.Location = new System.Drawing.Point(179, 277);
            this.rad2.Name = "rad2";
            this.rad2.RadioColor = System.Drawing.Color.Lime;
            this.rad2.ShadowGap = 1;
            this.rad2.Size = new System.Drawing.Size(150, 30);
            this.rad2.TabIndex = 19;
            this.rad2.Text = "RadioBox 2";
            // 
            // rad1
            // 
            this.rad1.BoxColor = null;
            this.rad1.BoxSize = 20;
            this.rad1.Checked = false;
            this.rad1.Location = new System.Drawing.Point(179, 241);
            this.rad1.Name = "rad1";
            this.rad1.RadioColor = null;
            this.rad1.ShadowGap = 1;
            this.rad1.Size = new System.Drawing.Size(150, 30);
            this.rad1.TabIndex = 18;
            this.rad1.Text = "RadioBox 1";
            // 
            // chk3
            // 
            this.chk3.BoxColor = null;
            this.chk3.BoxSize = 20;
            this.chk3.CheckColor = System.Drawing.Color.Yellow;
            this.chk3.Checked = false;
            this.chk3.Location = new System.Drawing.Point(13, 313);
            this.chk3.Name = "chk3";
            this.chk3.ShadowGap = 1;
            this.chk3.Size = new System.Drawing.Size(150, 30);
            this.chk3.TabIndex = 17;
            this.chk3.Text = "CheckBox 3";
            // 
            // chk2
            // 
            this.chk2.BoxColor = null;
            this.chk2.BoxSize = 20;
            this.chk2.CheckColor = System.Drawing.Color.Lime;
            this.chk2.Checked = false;
            this.chk2.Location = new System.Drawing.Point(13, 277);
            this.chk2.Name = "chk2";
            this.chk2.ShadowGap = 1;
            this.chk2.Size = new System.Drawing.Size(150, 30);
            this.chk2.TabIndex = 16;
            this.chk2.Text = "CheckBox 2";
            // 
            // chk1
            // 
            this.chk1.BoxColor = null;
            this.chk1.BoxSize = 20;
            this.chk1.CheckColor = null;
            this.chk1.Checked = false;
            this.chk1.Location = new System.Drawing.Point(13, 241);
            this.chk1.Name = "chk1";
            this.chk1.ShadowGap = 1;
            this.chk1.Size = new System.Drawing.Size(150, 30);
            this.chk1.TabIndex = 15;
            this.chk1.Text = "CheckBox 1";
            // 
            // btnsType
            // 
            this.btnsType.BackgroundDraw = true;
            this.btnsType.ButtonColor = null;
            this.btnsType.CheckdButtonColor = null;
            this.btnsType.Clickable = true;
            this.btnsType.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnsType.Direction = Devinno.Forms.DvDirectionHV.Horizon;
            this.btnsType.Gradient = true;
            this.btnsType.Location = new System.Drawing.Point(499, 397);
            this.btnsType.Name = "btnsType";
            this.btnsType.Round = null;
            this.btnsType.SelectionMode = false;
            this.btnsType.ShadowGap = 1;
            this.btnsType.Size = new System.Drawing.Size(206, 30);
            this.btnsType.TabIndex = 13;
            this.btnsType.Text = "dvButtons1";
            // 
            // btnTextAlign
            // 
            this.btnTextAlign.BackgroundDraw = true;
            this.btnTextAlign.ButtonColor = null;
            this.btnTextAlign.Clickable = true;
            this.btnTextAlign.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnTextAlign.Gradient = true;
            this.btnTextAlign.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnTextAlign.IconGap = 0;
            this.btnTextAlign.IconImage = null;
            this.btnTextAlign.IconSize = 10F;
            this.btnTextAlign.IconString = null;
            this.btnTextAlign.Location = new System.Drawing.Point(345, 193);
            this.btnTextAlign.Name = "btnTextAlign";
            this.btnTextAlign.Round = null;
            this.btnTextAlign.ShadowGap = 1;
            this.btnTextAlign.Size = new System.Drawing.Size(138, 30);
            this.btnTextAlign.TabIndex = 12;
            this.btnTextAlign.Text = "정렬 변경";
            this.btnTextAlign.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnTextAlign.UseKey = false;
            // 
            // txtMultiLine
            // 
            this.txtMultiLine.BorderColor = null;
            this.txtMultiLine.ContentAlignment = Devinno.Forms.DvContentAlignment.TopLeft;
            this.txtMultiLine.FullMode = false;
            this.txtMultiLine.InputType = Devinno.Forms.DvTextBoxType.Text;
            this.txtMultiLine.Location = new System.Drawing.Point(345, 49);
            this.txtMultiLine.MaxLength = 32767;
            this.txtMultiLine.MinusInput = false;
            this.txtMultiLine.MultiLine = true;
            this.txtMultiLine.Name = "txtMultiLine";
            this.txtMultiLine.Round = null;
            this.txtMultiLine.ShadowGap = 1;
            this.txtMultiLine.Size = new System.Drawing.Size(138, 138);
            this.txtMultiLine.Style = Devinno.Forms.Embossing.FlatConcave;
            this.txtMultiLine.TabIndex = 11;
            this.txtMultiLine.TabStop = false;
            this.txtMultiLine.Text = "동해물과 백두산이\r\n마르고 닳도록\r\n하느님이 보우하사\r\n우리 나라 만세";
            this.txtMultiLine.TextBoxColor = null;
            this.txtMultiLine.TextPadding = new System.Windows.Forms.Padding(5);
            this.txtMultiLine.Unit = "";
            this.txtMultiLine.UnitWidth = null;
            // 
            // txtNumber
            // 
            this.txtNumber.BorderColor = null;
            this.txtNumber.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.txtNumber.FullMode = false;
            this.txtNumber.InputType = Devinno.Forms.DvTextBoxType.Number;
            this.txtNumber.Location = new System.Drawing.Point(345, 13);
            this.txtNumber.MaxLength = 32767;
            this.txtNumber.MinusInput = false;
            this.txtNumber.MultiLine = false;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Round = null;
            this.txtNumber.ShadowGap = 1;
            this.txtNumber.Size = new System.Drawing.Size(138, 30);
            this.txtNumber.Style = Devinno.Forms.Embossing.FlatConcave;
            this.txtNumber.TabIndex = 10;
            this.txtNumber.TabStop = false;
            this.txtNumber.Text = "0";
            this.txtNumber.TextBoxColor = null;
            this.txtNumber.TextPadding = new System.Windows.Forms.Padding(5);
            this.txtNumber.Unit = "℃";
            this.txtNumber.UnitWidth = 30;
            // 
            // btnBlank
            // 
            this.btnBlank.BackgroundDraw = false;
            this.btnBlank.ButtonColor = null;
            this.btnBlank.Clickable = true;
            this.btnBlank.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnBlank.Gradient = true;
            this.btnBlank.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnBlank.IconGap = 3;
            this.btnBlank.IconImage = null;
            this.btnBlank.IconSize = 12F;
            this.btnBlank.IconString = "";
            this.btnBlank.Location = new System.Drawing.Point(13, 193);
            this.btnBlank.Name = "btnBlank";
            this.btnBlank.Round = null;
            this.btnBlank.ShadowGap = 1;
            this.btnBlank.Size = new System.Drawing.Size(150, 30);
            this.btnBlank.TabIndex = 9;
            this.btnBlank.Text = "Blank Button";
            this.btnBlank.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnBlank.UseKey = false;
            // 
            // lblNone
            // 
            this.lblNone.BackgroundDraw = false;
            this.lblNone.BorderColor = null;
            this.lblNone.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblNone.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblNone.IconGap = 0;
            this.lblNone.IconImage = null;
            this.lblNone.IconSize = 10F;
            this.lblNone.IconString = null;
            this.lblNone.LabelColor = null;
            this.lblNone.Location = new System.Drawing.Point(179, 193);
            this.lblNone.Name = "lblNone";
            this.lblNone.Round = null;
            this.lblNone.ShadowGap = 1;
            this.lblNone.Size = new System.Drawing.Size(150, 30);
            this.lblNone.Style = Devinno.Forms.Embossing.Flat;
            this.lblNone.TabIndex = 8;
            this.lblNone.TabStop = false;
            this.lblNone.Text = "None";
            this.lblNone.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblNone.Unit = "";
            this.lblNone.UnitWidth = null;
            // 
            // btnConvex
            // 
            this.btnConvex.BackgroundDraw = true;
            this.btnConvex.BorderColor = null;
            this.btnConvex.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnConvex.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnConvex.IconGap = 0;
            this.btnConvex.IconImage = null;
            this.btnConvex.IconSize = 10F;
            this.btnConvex.IconString = null;
            this.btnConvex.LabelColor = null;
            this.btnConvex.Location = new System.Drawing.Point(179, 157);
            this.btnConvex.Name = "btnConvex";
            this.btnConvex.Round = null;
            this.btnConvex.ShadowGap = 1;
            this.btnConvex.Size = new System.Drawing.Size(150, 30);
            this.btnConvex.Style = Devinno.Forms.Embossing.Convex;
            this.btnConvex.TabIndex = 7;
            this.btnConvex.TabStop = false;
            this.btnConvex.Text = "Convex";
            this.btnConvex.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnConvex.Unit = "";
            this.btnConvex.UnitWidth = null;
            // 
            // lblConcave
            // 
            this.lblConcave.BackgroundDraw = true;
            this.lblConcave.BorderColor = null;
            this.lblConcave.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblConcave.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblConcave.IconGap = 0;
            this.lblConcave.IconImage = null;
            this.lblConcave.IconSize = 10F;
            this.lblConcave.IconString = null;
            this.lblConcave.LabelColor = null;
            this.lblConcave.Location = new System.Drawing.Point(179, 121);
            this.lblConcave.Name = "lblConcave";
            this.lblConcave.Round = null;
            this.lblConcave.ShadowGap = 1;
            this.lblConcave.Size = new System.Drawing.Size(150, 30);
            this.lblConcave.Style = Devinno.Forms.Embossing.Concave;
            this.lblConcave.TabIndex = 6;
            this.lblConcave.TabStop = false;
            this.lblConcave.Text = "Concave";
            this.lblConcave.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblConcave.Unit = "";
            this.lblConcave.UnitWidth = null;
            // 
            // lblFlatConvex
            // 
            this.lblFlatConvex.BackgroundDraw = true;
            this.lblFlatConvex.BorderColor = null;
            this.lblFlatConvex.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblFlatConvex.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblFlatConvex.IconGap = 0;
            this.lblFlatConvex.IconImage = null;
            this.lblFlatConvex.IconSize = 10F;
            this.lblFlatConvex.IconString = null;
            this.lblFlatConvex.LabelColor = null;
            this.lblFlatConvex.Location = new System.Drawing.Point(179, 85);
            this.lblFlatConvex.Name = "lblFlatConvex";
            this.lblFlatConvex.Round = null;
            this.lblFlatConvex.ShadowGap = 1;
            this.lblFlatConvex.Size = new System.Drawing.Size(150, 30);
            this.lblFlatConvex.Style = Devinno.Forms.Embossing.FlatConvex;
            this.lblFlatConvex.TabIndex = 5;
            this.lblFlatConvex.TabStop = false;
            this.lblFlatConvex.Text = "Flat Convex";
            this.lblFlatConvex.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblFlatConvex.Unit = "";
            this.lblFlatConvex.UnitWidth = null;
            // 
            // lblFlatConcave
            // 
            this.lblFlatConcave.BackgroundDraw = true;
            this.lblFlatConcave.BorderColor = null;
            this.lblFlatConcave.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblFlatConcave.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblFlatConcave.IconGap = 0;
            this.lblFlatConcave.IconImage = null;
            this.lblFlatConcave.IconSize = 10F;
            this.lblFlatConcave.IconString = null;
            this.lblFlatConcave.LabelColor = null;
            this.lblFlatConcave.Location = new System.Drawing.Point(179, 49);
            this.lblFlatConcave.Name = "lblFlatConcave";
            this.lblFlatConcave.Round = null;
            this.lblFlatConcave.ShadowGap = 1;
            this.lblFlatConcave.Size = new System.Drawing.Size(150, 30);
            this.lblFlatConcave.Style = Devinno.Forms.Embossing.FlatConcave;
            this.lblFlatConcave.TabIndex = 4;
            this.lblFlatConcave.TabStop = false;
            this.lblFlatConcave.Text = "Flat Concave";
            this.lblFlatConcave.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblFlatConcave.Unit = "";
            this.lblFlatConcave.UnitWidth = null;
            // 
            // lblFlat
            // 
            this.lblFlat.BackgroundDraw = true;
            this.lblFlat.BorderColor = null;
            this.lblFlat.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblFlat.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblFlat.IconGap = 0;
            this.lblFlat.IconImage = null;
            this.lblFlat.IconSize = 10F;
            this.lblFlat.IconString = null;
            this.lblFlat.LabelColor = null;
            this.lblFlat.Location = new System.Drawing.Point(179, 13);
            this.lblFlat.Name = "lblFlat";
            this.lblFlat.Round = null;
            this.lblFlat.ShadowGap = 1;
            this.lblFlat.Size = new System.Drawing.Size(150, 30);
            this.lblFlat.Style = Devinno.Forms.Embossing.Flat;
            this.lblFlat.TabIndex = 3;
            this.lblFlat.TabStop = false;
            this.lblFlat.Text = "Flat";
            this.lblFlat.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblFlat.Unit = "";
            this.lblFlat.UnitWidth = null;
            // 
            // btnIcon
            // 
            this.btnIcon.BackgroundDraw = true;
            this.btnIcon.ButtonColor = null;
            this.btnIcon.Clickable = true;
            this.btnIcon.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnIcon.Gradient = true;
            this.btnIcon.IconAlignment = Devinno.Forms.DvTextIconAlignment.TopBottom;
            this.btnIcon.IconGap = 3;
            this.btnIcon.IconImage = null;
            this.btnIcon.IconSize = 18F;
            this.btnIcon.IconString = "fa-cube";
            this.btnIcon.Location = new System.Drawing.Point(13, 85);
            this.btnIcon.Name = "btnIcon";
            this.btnIcon.Round = null;
            this.btnIcon.ShadowGap = 1;
            this.btnIcon.Size = new System.Drawing.Size(150, 102);
            this.btnIcon.TabIndex = 2;
            this.btnIcon.Text = "Icon Button";
            this.btnIcon.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnIcon.UseKey = false;
            // 
            // btnGradient
            // 
            this.btnGradient.BackgroundDraw = true;
            this.btnGradient.ButtonColor = null;
            this.btnGradient.Clickable = true;
            this.btnGradient.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnGradient.Gradient = true;
            this.btnGradient.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnGradient.IconGap = 0;
            this.btnGradient.IconImage = null;
            this.btnGradient.IconSize = 10F;
            this.btnGradient.IconString = null;
            this.btnGradient.Location = new System.Drawing.Point(13, 49);
            this.btnGradient.Name = "btnGradient";
            this.btnGradient.Round = null;
            this.btnGradient.ShadowGap = 1;
            this.btnGradient.Size = new System.Drawing.Size(150, 30);
            this.btnGradient.TabIndex = 1;
            this.btnGradient.Text = "Gradient Button";
            this.btnGradient.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnGradient.UseKey = false;
            // 
            // btnFlat
            // 
            this.btnFlat.BackgroundDraw = true;
            this.btnFlat.ButtonColor = null;
            this.btnFlat.Clickable = true;
            this.btnFlat.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnFlat.Gradient = false;
            this.btnFlat.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnFlat.IconGap = 0;
            this.btnFlat.IconImage = null;
            this.btnFlat.IconSize = 10F;
            this.btnFlat.IconString = null;
            this.btnFlat.Location = new System.Drawing.Point(13, 13);
            this.btnFlat.Name = "btnFlat";
            this.btnFlat.Round = null;
            this.btnFlat.ShadowGap = 1;
            this.btnFlat.Size = new System.Drawing.Size(150, 30);
            this.btnFlat.TabIndex = 0;
            this.btnFlat.Text = "Flat Button";
            this.btnFlat.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnFlat.UseKey = false;
            // 
            // tpGauge
            // 
            this.tpGauge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpGauge.Controls.Add(this.tblGauge1);
            this.tpGauge.Controls.Add(this.tblGauge2);
            this.tpGauge.Location = new System.Drawing.Point(4, 5);
            this.tpGauge.Name = "tpGauge";
            this.tpGauge.Padding = new System.Windows.Forms.Padding(10);
            this.tpGauge.Size = new System.Drawing.Size(718, 690);
            this.tpGauge.TabIndex = 1;
            this.tpGauge.Text = "tabPage2";
            // 
            // tblGauge1
            // 
            this.tblGauge1.ColumnCount = 3;
            this.tblGauge1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tblGauge1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tblGauge1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tblGauge1.Controls.Add(this.dvContainer6, 2, 1);
            this.tblGauge1.Controls.Add(this.dvContainer4, 1, 1);
            this.tblGauge1.Controls.Add(this.dvContainer3, 0, 1);
            this.tblGauge1.Controls.Add(this.dvContainer2, 1, 0);
            this.tblGauge1.Controls.Add(this.sg, 1, 2);
            this.tblGauge1.Controls.Add(this.dvContainer1, 0, 0);
            this.tblGauge1.Controls.Add(this.dvContainer5, 2, 0);
            this.tblGauge1.Location = new System.Drawing.Point(13, 13);
            this.tblGauge1.Name = "tblGauge1";
            this.tblGauge1.RowCount = 3;
            this.tblGauge1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGauge1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGauge1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblGauge1.Size = new System.Drawing.Size(692, 461);
            this.tblGauge1.TabIndex = 23;
            // 
            // dvContainer6
            // 
            this.dvContainer6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer6.Controls.Add(this.rangeV_T);
            this.dvContainer6.Controls.Add(this.rangeV_B);
            this.dvContainer6.Controls.Add(this.rangeV_G);
            this.dvContainer6.Controls.Add(this.rangeV_R);
            this.dvContainer6.Controls.Add(this.rangeV_N);
            this.dvContainer6.Location = new System.Drawing.Point(479, 221);
            this.dvContainer6.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer6.Name = "dvContainer6";
            this.dvContainer6.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer6.ShadowGap = 1;
            this.dvContainer6.Size = new System.Drawing.Size(194, 194);
            this.dvContainer6.TabIndex = 5;
            this.dvContainer6.TabStop = false;
            this.dvContainer6.Text = "dvContainer6";
            // 
            // rangeV_T
            // 
            this.rangeV_T.BarColor = System.Drawing.Color.Teal;
            this.rangeV_T.BoxColor = null;
            this.rangeV_T.CursorColor = null;
            this.rangeV_T.DrawText = false;
            this.rangeV_T.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeV_T.FormatString = "0";
            this.rangeV_T.Location = new System.Drawing.Point(154, 10);
            this.rangeV_T.Maximum = 100D;
            this.rangeV_T.Minimum = 0D;
            this.rangeV_T.Name = "rangeV_T";
            this.rangeV_T.RangeEnd = 50D;
            this.rangeV_T.RangeStart = 50D;
            this.rangeV_T.Round = null;
            this.rangeV_T.ShadowGap = 1;
            this.rangeV_T.Size = new System.Drawing.Size(30, 174);
            this.rangeV_T.TabIndex = 25;
            this.rangeV_T.Text = "dvRangeSliderv1";
            this.rangeV_T.Tick = 10D;
            // 
            // rangeV_B
            // 
            this.rangeV_B.BarColor = System.Drawing.Color.Navy;
            this.rangeV_B.BoxColor = null;
            this.rangeV_B.CursorColor = null;
            this.rangeV_B.DrawText = false;
            this.rangeV_B.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeV_B.FormatString = "0";
            this.rangeV_B.Location = new System.Drawing.Point(118, 10);
            this.rangeV_B.Maximum = 100D;
            this.rangeV_B.Minimum = 0D;
            this.rangeV_B.Name = "rangeV_B";
            this.rangeV_B.RangeEnd = 50D;
            this.rangeV_B.RangeStart = 50D;
            this.rangeV_B.Round = null;
            this.rangeV_B.ShadowGap = 1;
            this.rangeV_B.Size = new System.Drawing.Size(30, 174);
            this.rangeV_B.TabIndex = 24;
            this.rangeV_B.Text = "dvRangeSliderv1";
            this.rangeV_B.Tick = 10D;
            // 
            // rangeV_G
            // 
            this.rangeV_G.BarColor = System.Drawing.Color.Green;
            this.rangeV_G.BoxColor = null;
            this.rangeV_G.CursorColor = null;
            this.rangeV_G.DrawText = false;
            this.rangeV_G.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeV_G.FormatString = "0";
            this.rangeV_G.Location = new System.Drawing.Point(82, 10);
            this.rangeV_G.Maximum = 100D;
            this.rangeV_G.Minimum = 0D;
            this.rangeV_G.Name = "rangeV_G";
            this.rangeV_G.RangeEnd = 50D;
            this.rangeV_G.RangeStart = 50D;
            this.rangeV_G.Round = null;
            this.rangeV_G.ShadowGap = 1;
            this.rangeV_G.Size = new System.Drawing.Size(30, 174);
            this.rangeV_G.TabIndex = 23;
            this.rangeV_G.Text = "dvRangeSliderv1";
            this.rangeV_G.Tick = 10D;
            // 
            // rangeV_R
            // 
            this.rangeV_R.BarColor = System.Drawing.Color.DarkRed;
            this.rangeV_R.BoxColor = null;
            this.rangeV_R.CursorColor = null;
            this.rangeV_R.DrawText = false;
            this.rangeV_R.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeV_R.FormatString = "0";
            this.rangeV_R.Location = new System.Drawing.Point(46, 10);
            this.rangeV_R.Maximum = 100D;
            this.rangeV_R.Minimum = 0D;
            this.rangeV_R.Name = "rangeV_R";
            this.rangeV_R.RangeEnd = 50D;
            this.rangeV_R.RangeStart = 50D;
            this.rangeV_R.Round = null;
            this.rangeV_R.ShadowGap = 1;
            this.rangeV_R.Size = new System.Drawing.Size(30, 174);
            this.rangeV_R.TabIndex = 22;
            this.rangeV_R.Text = "dvRangeSliderv1";
            this.rangeV_R.Tick = null;
            // 
            // rangeV_N
            // 
            this.rangeV_N.BarColor = System.Drawing.Color.DarkGray;
            this.rangeV_N.BoxColor = null;
            this.rangeV_N.CursorColor = null;
            this.rangeV_N.DrawText = true;
            this.rangeV_N.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeV_N.FormatString = "0";
            this.rangeV_N.Location = new System.Drawing.Point(10, 10);
            this.rangeV_N.Maximum = 100D;
            this.rangeV_N.Minimum = 0D;
            this.rangeV_N.Name = "rangeV_N";
            this.rangeV_N.RangeEnd = 50D;
            this.rangeV_N.RangeStart = 50D;
            this.rangeV_N.Round = null;
            this.rangeV_N.ShadowGap = 1;
            this.rangeV_N.Size = new System.Drawing.Size(30, 174);
            this.rangeV_N.TabIndex = 21;
            this.rangeV_N.Text = "dvRangeSliderv1";
            this.rangeV_N.Tick = null;
            // 
            // dvContainer4
            // 
            this.dvContainer4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer4.Controls.Add(this.sldV_T);
            this.dvContainer4.Controls.Add(this.sldV_N);
            this.dvContainer4.Controls.Add(this.sldV_R);
            this.dvContainer4.Controls.Add(this.sldV_G);
            this.dvContainer4.Controls.Add(this.sldV_B);
            this.dvContainer4.Location = new System.Drawing.Point(248, 221);
            this.dvContainer4.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer4.Name = "dvContainer4";
            this.dvContainer4.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer4.ShadowGap = 1;
            this.dvContainer4.Size = new System.Drawing.Size(194, 194);
            this.dvContainer4.TabIndex = 3;
            this.dvContainer4.TabStop = false;
            this.dvContainer4.Text = "dvContainer4";
            // 
            // sldV_T
            // 
            this.sldV_T.BarColor = System.Drawing.Color.Teal;
            this.sldV_T.BoxColor = null;
            this.sldV_T.CursorColor = null;
            this.sldV_T.DrawText = false;
            this.sldV_T.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldV_T.FormatString = "0";
            this.sldV_T.Location = new System.Drawing.Point(154, 10);
            this.sldV_T.Maximum = 100D;
            this.sldV_T.Minimum = 0D;
            this.sldV_T.Name = "sldV_T";
            this.sldV_T.Reverse = false;
            this.sldV_T.Round = null;
            this.sldV_T.ShadowGap = 1;
            this.sldV_T.Size = new System.Drawing.Size(30, 174);
            this.sldV_T.TabIndex = 16;
            this.sldV_T.Text = "dvSliderv4";
            this.sldV_T.Tick = 10D;
            this.sldV_T.Value = 0D;
            // 
            // sldV_N
            // 
            this.sldV_N.BarColor = System.Drawing.Color.DarkGray;
            this.sldV_N.BoxColor = null;
            this.sldV_N.CursorColor = null;
            this.sldV_N.DrawText = true;
            this.sldV_N.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldV_N.FormatString = "0";
            this.sldV_N.Location = new System.Drawing.Point(10, 10);
            this.sldV_N.Maximum = 100D;
            this.sldV_N.Minimum = 0D;
            this.sldV_N.Name = "sldV_N";
            this.sldV_N.Reverse = false;
            this.sldV_N.Round = null;
            this.sldV_N.ShadowGap = 1;
            this.sldV_N.Size = new System.Drawing.Size(30, 174);
            this.sldV_N.TabIndex = 12;
            this.sldV_N.Text = "dvSliderv1";
            this.sldV_N.Tick = null;
            this.sldV_N.Value = 0D;
            // 
            // sldV_R
            // 
            this.sldV_R.BarColor = System.Drawing.Color.DarkRed;
            this.sldV_R.BoxColor = null;
            this.sldV_R.CursorColor = null;
            this.sldV_R.DrawText = false;
            this.sldV_R.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldV_R.FormatString = "0";
            this.sldV_R.Location = new System.Drawing.Point(46, 10);
            this.sldV_R.Maximum = 100D;
            this.sldV_R.Minimum = 0D;
            this.sldV_R.Name = "sldV_R";
            this.sldV_R.Reverse = false;
            this.sldV_R.Round = null;
            this.sldV_R.ShadowGap = 1;
            this.sldV_R.Size = new System.Drawing.Size(30, 174);
            this.sldV_R.TabIndex = 13;
            this.sldV_R.Text = "dvSliderv2";
            this.sldV_R.Tick = null;
            this.sldV_R.Value = 0D;
            // 
            // sldV_G
            // 
            this.sldV_G.BarColor = System.Drawing.Color.Green;
            this.sldV_G.BoxColor = null;
            this.sldV_G.CursorColor = null;
            this.sldV_G.DrawText = false;
            this.sldV_G.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldV_G.FormatString = "0";
            this.sldV_G.Location = new System.Drawing.Point(82, 10);
            this.sldV_G.Maximum = 100D;
            this.sldV_G.Minimum = 0D;
            this.sldV_G.Name = "sldV_G";
            this.sldV_G.Reverse = false;
            this.sldV_G.Round = null;
            this.sldV_G.ShadowGap = 1;
            this.sldV_G.Size = new System.Drawing.Size(30, 174);
            this.sldV_G.TabIndex = 14;
            this.sldV_G.Text = "dvSliderv3";
            this.sldV_G.Tick = 10D;
            this.sldV_G.Value = 0D;
            // 
            // sldV_B
            // 
            this.sldV_B.BarColor = System.Drawing.Color.Navy;
            this.sldV_B.BoxColor = null;
            this.sldV_B.CursorColor = null;
            this.sldV_B.DrawText = false;
            this.sldV_B.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldV_B.FormatString = "0";
            this.sldV_B.Location = new System.Drawing.Point(118, 10);
            this.sldV_B.Maximum = 100D;
            this.sldV_B.Minimum = 0D;
            this.sldV_B.Name = "sldV_B";
            this.sldV_B.Reverse = false;
            this.sldV_B.Round = null;
            this.sldV_B.ShadowGap = 1;
            this.sldV_B.Size = new System.Drawing.Size(30, 174);
            this.sldV_B.TabIndex = 15;
            this.sldV_B.Text = "dvSliderv4";
            this.sldV_B.Tick = 10D;
            this.sldV_B.Value = 0D;
            // 
            // dvContainer3
            // 
            this.dvContainer3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer3.Controls.Add(this.pgsV_T);
            this.dvContainer3.Controls.Add(this.pgsV_N);
            this.dvContainer3.Controls.Add(this.pgsV_R);
            this.dvContainer3.Controls.Add(this.pgsV_G);
            this.dvContainer3.Controls.Add(this.pgsV_B);
            this.dvContainer3.Location = new System.Drawing.Point(18, 221);
            this.dvContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer3.Name = "dvContainer3";
            this.dvContainer3.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer3.ShadowGap = 1;
            this.dvContainer3.Size = new System.Drawing.Size(194, 194);
            this.dvContainer3.TabIndex = 2;
            this.dvContainer3.TabStop = false;
            this.dvContainer3.Text = "dvContainer3";
            // 
            // pgsV_T
            // 
            this.pgsV_T.BarColor = System.Drawing.Color.Teal;
            this.pgsV_T.BoxColor = null;
            this.pgsV_T.DrawText = true;
            this.pgsV_T.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsV_T.FormatString = "0";
            this.pgsV_T.InnerBar = true;
            this.pgsV_T.Location = new System.Drawing.Point(154, 10);
            this.pgsV_T.Maximum = 100D;
            this.pgsV_T.Minimum = 0D;
            this.pgsV_T.Name = "pgsV_T";
            this.pgsV_T.Reverse = false;
            this.pgsV_T.Round = null;
            this.pgsV_T.ShadowGap = 1;
            this.pgsV_T.Size = new System.Drawing.Size(30, 174);
            this.pgsV_T.TabIndex = 8;
            this.pgsV_T.TabStop = false;
            this.pgsV_T.Text = "dvProgressv4";
            this.pgsV_T.Value = 0D;
            // 
            // pgsV_N
            // 
            this.pgsV_N.BarColor = System.Drawing.Color.DarkGray;
            this.pgsV_N.BoxColor = null;
            this.pgsV_N.DrawText = false;
            this.pgsV_N.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsV_N.FormatString = "0";
            this.pgsV_N.InnerBar = true;
            this.pgsV_N.Location = new System.Drawing.Point(10, 10);
            this.pgsV_N.Maximum = 100D;
            this.pgsV_N.Minimum = 0D;
            this.pgsV_N.Name = "pgsV_N";
            this.pgsV_N.Reverse = false;
            this.pgsV_N.Round = null;
            this.pgsV_N.ShadowGap = 1;
            this.pgsV_N.Size = new System.Drawing.Size(30, 174);
            this.pgsV_N.TabIndex = 4;
            this.pgsV_N.TabStop = false;
            this.pgsV_N.Text = "dvProgressv1";
            this.pgsV_N.Value = 0D;
            // 
            // pgsV_R
            // 
            this.pgsV_R.BarColor = System.Drawing.Color.DarkRed;
            this.pgsV_R.BoxColor = null;
            this.pgsV_R.DrawText = true;
            this.pgsV_R.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsV_R.FormatString = "0";
            this.pgsV_R.InnerBar = true;
            this.pgsV_R.Location = new System.Drawing.Point(46, 10);
            this.pgsV_R.Maximum = 100D;
            this.pgsV_R.Minimum = 0D;
            this.pgsV_R.Name = "pgsV_R";
            this.pgsV_R.Reverse = false;
            this.pgsV_R.Round = null;
            this.pgsV_R.ShadowGap = 1;
            this.pgsV_R.Size = new System.Drawing.Size(30, 174);
            this.pgsV_R.TabIndex = 5;
            this.pgsV_R.TabStop = false;
            this.pgsV_R.Text = "dvProgressv2";
            this.pgsV_R.Value = 0D;
            // 
            // pgsV_G
            // 
            this.pgsV_G.BarColor = System.Drawing.Color.Green;
            this.pgsV_G.BoxColor = null;
            this.pgsV_G.DrawText = true;
            this.pgsV_G.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsV_G.FormatString = "0";
            this.pgsV_G.InnerBar = true;
            this.pgsV_G.Location = new System.Drawing.Point(82, 10);
            this.pgsV_G.Maximum = 100D;
            this.pgsV_G.Minimum = 0D;
            this.pgsV_G.Name = "pgsV_G";
            this.pgsV_G.Reverse = false;
            this.pgsV_G.Round = null;
            this.pgsV_G.ShadowGap = 1;
            this.pgsV_G.Size = new System.Drawing.Size(30, 174);
            this.pgsV_G.TabIndex = 6;
            this.pgsV_G.TabStop = false;
            this.pgsV_G.Text = "dvProgressv3";
            this.pgsV_G.Value = 0D;
            // 
            // pgsV_B
            // 
            this.pgsV_B.BarColor = System.Drawing.Color.Navy;
            this.pgsV_B.BoxColor = null;
            this.pgsV_B.DrawText = true;
            this.pgsV_B.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsV_B.FormatString = "0";
            this.pgsV_B.InnerBar = true;
            this.pgsV_B.Location = new System.Drawing.Point(118, 10);
            this.pgsV_B.Maximum = 100D;
            this.pgsV_B.Minimum = 0D;
            this.pgsV_B.Name = "pgsV_B";
            this.pgsV_B.Reverse = false;
            this.pgsV_B.Round = null;
            this.pgsV_B.ShadowGap = 1;
            this.pgsV_B.Size = new System.Drawing.Size(30, 174);
            this.pgsV_B.TabIndex = 7;
            this.pgsV_B.TabStop = false;
            this.pgsV_B.Text = "dvProgressv4";
            this.pgsV_B.Value = 0D;
            // 
            // dvContainer2
            // 
            this.dvContainer2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer2.Controls.Add(this.sldH_T);
            this.dvContainer2.Controls.Add(this.sldH_N);
            this.dvContainer2.Controls.Add(this.sldH_R);
            this.dvContainer2.Controls.Add(this.sldH_B);
            this.dvContainer2.Controls.Add(this.sldH_G);
            this.dvContainer2.Location = new System.Drawing.Point(248, 9);
            this.dvContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer2.Name = "dvContainer2";
            this.dvContainer2.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer2.ShadowGap = 1;
            this.dvContainer2.Size = new System.Drawing.Size(194, 194);
            this.dvContainer2.TabIndex = 1;
            this.dvContainer2.TabStop = false;
            this.dvContainer2.Text = "dvContainer2";
            // 
            // sldH_T
            // 
            this.sldH_T.BarColor = System.Drawing.Color.Teal;
            this.sldH_T.BoxColor = null;
            this.sldH_T.CursorColor = null;
            this.sldH_T.DrawText = false;
            this.sldH_T.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldH_T.FormatString = "0";
            this.sldH_T.Location = new System.Drawing.Point(10, 154);
            this.sldH_T.Maximum = 100D;
            this.sldH_T.Minimum = 0D;
            this.sldH_T.Name = "sldH_T";
            this.sldH_T.Reverse = false;
            this.sldH_T.Round = null;
            this.sldH_T.ShadowGap = 1;
            this.sldH_T.Size = new System.Drawing.Size(174, 30);
            this.sldH_T.TabIndex = 12;
            this.sldH_T.Text = "dvSliderh3";
            this.sldH_T.Tick = 10D;
            this.sldH_T.Value = 0D;
            // 
            // sldH_N
            // 
            this.sldH_N.BarColor = System.Drawing.Color.DarkGray;
            this.sldH_N.BoxColor = null;
            this.sldH_N.CursorColor = null;
            this.sldH_N.DrawText = true;
            this.sldH_N.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldH_N.FormatString = "0";
            this.sldH_N.Location = new System.Drawing.Point(10, 10);
            this.sldH_N.Maximum = 100D;
            this.sldH_N.Minimum = 0D;
            this.sldH_N.Name = "sldH_N";
            this.sldH_N.Reverse = false;
            this.sldH_N.Round = null;
            this.sldH_N.ShadowGap = 1;
            this.sldH_N.Size = new System.Drawing.Size(174, 30);
            this.sldH_N.TabIndex = 8;
            this.sldH_N.Text = "dvSliderh1";
            this.sldH_N.Tick = null;
            this.sldH_N.Value = 0D;
            // 
            // sldH_R
            // 
            this.sldH_R.BarColor = System.Drawing.Color.DarkRed;
            this.sldH_R.BoxColor = null;
            this.sldH_R.CursorColor = null;
            this.sldH_R.DrawText = false;
            this.sldH_R.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldH_R.FormatString = "0";
            this.sldH_R.Location = new System.Drawing.Point(10, 46);
            this.sldH_R.Maximum = 100D;
            this.sldH_R.Minimum = 0D;
            this.sldH_R.Name = "sldH_R";
            this.sldH_R.Reverse = false;
            this.sldH_R.Round = null;
            this.sldH_R.ShadowGap = 1;
            this.sldH_R.Size = new System.Drawing.Size(174, 30);
            this.sldH_R.TabIndex = 9;
            this.sldH_R.Text = "dvSliderh2";
            this.sldH_R.Tick = null;
            this.sldH_R.Value = 0D;
            // 
            // sldH_B
            // 
            this.sldH_B.BarColor = System.Drawing.Color.Navy;
            this.sldH_B.BoxColor = null;
            this.sldH_B.CursorColor = null;
            this.sldH_B.DrawText = false;
            this.sldH_B.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldH_B.FormatString = "0";
            this.sldH_B.Location = new System.Drawing.Point(10, 118);
            this.sldH_B.Maximum = 100D;
            this.sldH_B.Minimum = 0D;
            this.sldH_B.Name = "sldH_B";
            this.sldH_B.Reverse = false;
            this.sldH_B.Round = null;
            this.sldH_B.ShadowGap = 1;
            this.sldH_B.Size = new System.Drawing.Size(174, 30);
            this.sldH_B.TabIndex = 10;
            this.sldH_B.Text = "dvSliderh3";
            this.sldH_B.Tick = 10D;
            this.sldH_B.Value = 0D;
            // 
            // sldH_G
            // 
            this.sldH_G.BarColor = System.Drawing.Color.Green;
            this.sldH_G.BoxColor = null;
            this.sldH_G.CursorColor = null;
            this.sldH_G.DrawText = false;
            this.sldH_G.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sldH_G.FormatString = "0";
            this.sldH_G.Location = new System.Drawing.Point(10, 82);
            this.sldH_G.Maximum = 100D;
            this.sldH_G.Minimum = 0D;
            this.sldH_G.Name = "sldH_G";
            this.sldH_G.Reverse = false;
            this.sldH_G.Round = null;
            this.sldH_G.ShadowGap = 1;
            this.sldH_G.Size = new System.Drawing.Size(174, 30);
            this.sldH_G.TabIndex = 11;
            this.sldH_G.Text = "dvSliderh4";
            this.sldH_G.Tick = 10D;
            this.sldH_G.Value = 0D;
            // 
            // sg
            // 
            this.sg.ButtonColor = null;
            this.sg.ButtonStyle = Devinno.Forms.DvStepButtonStyle.LeftRight;
            this.sg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sg.Gap = 5;
            this.sg.Location = new System.Drawing.Point(233, 427);
            this.sg.Name = "sg";
            this.sg.OffColor = null;
            this.sg.OnColor = System.Drawing.Color.DarkRed;
            this.sg.ShadowGap = 1;
            this.sg.Size = new System.Drawing.Size(224, 31);
            this.sg.Step = 0;
            this.sg.StepCount = 7;
            this.sg.TabIndex = 19;
            this.sg.Text = "dvStepGauge1";
            this.sg.UseButton = true;
            // 
            // dvContainer1
            // 
            this.dvContainer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer1.Controls.Add(this.pgsH_T);
            this.dvContainer1.Controls.Add(this.pgsH_N);
            this.dvContainer1.Controls.Add(this.pgsH_R);
            this.dvContainer1.Controls.Add(this.pgsH_G);
            this.dvContainer1.Controls.Add(this.pgsH_B);
            this.dvContainer1.Location = new System.Drawing.Point(18, 9);
            this.dvContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer1.Name = "dvContainer1";
            this.dvContainer1.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer1.ShadowGap = 1;
            this.dvContainer1.Size = new System.Drawing.Size(194, 194);
            this.dvContainer1.TabIndex = 0;
            this.dvContainer1.TabStop = false;
            this.dvContainer1.Text = "dvContainer1";
            // 
            // pgsH_T
            // 
            this.pgsH_T.BarColor = System.Drawing.Color.Teal;
            this.pgsH_T.BoxColor = null;
            this.pgsH_T.DrawText = true;
            this.pgsH_T.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsH_T.FormatString = "0";
            this.pgsH_T.InnerBar = true;
            this.pgsH_T.Location = new System.Drawing.Point(10, 154);
            this.pgsH_T.Maximum = 100D;
            this.pgsH_T.Minimum = 0D;
            this.pgsH_T.Name = "pgsH_T";
            this.pgsH_T.Reverse = false;
            this.pgsH_T.Round = null;
            this.pgsH_T.ShadowGap = 1;
            this.pgsH_T.Size = new System.Drawing.Size(174, 30);
            this.pgsH_T.TabIndex = 4;
            this.pgsH_T.TabStop = false;
            this.pgsH_T.Text = "dvProgressh4";
            this.pgsH_T.Value = 0D;
            // 
            // pgsH_N
            // 
            this.pgsH_N.BarColor = System.Drawing.Color.DarkGray;
            this.pgsH_N.BoxColor = null;
            this.pgsH_N.DrawText = false;
            this.pgsH_N.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsH_N.FormatString = "0";
            this.pgsH_N.InnerBar = true;
            this.pgsH_N.Location = new System.Drawing.Point(10, 10);
            this.pgsH_N.Maximum = 100D;
            this.pgsH_N.Minimum = 0D;
            this.pgsH_N.Name = "pgsH_N";
            this.pgsH_N.Reverse = false;
            this.pgsH_N.Round = null;
            this.pgsH_N.ShadowGap = 1;
            this.pgsH_N.Size = new System.Drawing.Size(174, 30);
            this.pgsH_N.TabIndex = 0;
            this.pgsH_N.TabStop = false;
            this.pgsH_N.Text = "dvProgressh1";
            this.pgsH_N.Value = 0D;
            // 
            // pgsH_R
            // 
            this.pgsH_R.BarColor = System.Drawing.Color.DarkRed;
            this.pgsH_R.BoxColor = null;
            this.pgsH_R.DrawText = true;
            this.pgsH_R.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsH_R.FormatString = "0";
            this.pgsH_R.InnerBar = true;
            this.pgsH_R.Location = new System.Drawing.Point(10, 46);
            this.pgsH_R.Maximum = 100D;
            this.pgsH_R.Minimum = 0D;
            this.pgsH_R.Name = "pgsH_R";
            this.pgsH_R.Reverse = false;
            this.pgsH_R.Round = null;
            this.pgsH_R.ShadowGap = 1;
            this.pgsH_R.Size = new System.Drawing.Size(174, 30);
            this.pgsH_R.TabIndex = 1;
            this.pgsH_R.TabStop = false;
            this.pgsH_R.Text = "dvProgressh2";
            this.pgsH_R.Value = 0D;
            // 
            // pgsH_G
            // 
            this.pgsH_G.BarColor = System.Drawing.Color.Green;
            this.pgsH_G.BoxColor = null;
            this.pgsH_G.DrawText = true;
            this.pgsH_G.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsH_G.FormatString = "0";
            this.pgsH_G.InnerBar = true;
            this.pgsH_G.Location = new System.Drawing.Point(10, 82);
            this.pgsH_G.Maximum = 100D;
            this.pgsH_G.Minimum = 0D;
            this.pgsH_G.Name = "pgsH_G";
            this.pgsH_G.Reverse = false;
            this.pgsH_G.Round = null;
            this.pgsH_G.ShadowGap = 1;
            this.pgsH_G.Size = new System.Drawing.Size(174, 30);
            this.pgsH_G.TabIndex = 2;
            this.pgsH_G.TabStop = false;
            this.pgsH_G.Text = "dvProgressh3";
            this.pgsH_G.Value = 0D;
            // 
            // pgsH_B
            // 
            this.pgsH_B.BarColor = System.Drawing.Color.Navy;
            this.pgsH_B.BoxColor = null;
            this.pgsH_B.DrawText = true;
            this.pgsH_B.Font = new System.Drawing.Font("나눔고딕", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pgsH_B.FormatString = "0";
            this.pgsH_B.InnerBar = true;
            this.pgsH_B.Location = new System.Drawing.Point(10, 118);
            this.pgsH_B.Maximum = 100D;
            this.pgsH_B.Minimum = 0D;
            this.pgsH_B.Name = "pgsH_B";
            this.pgsH_B.Reverse = false;
            this.pgsH_B.Round = null;
            this.pgsH_B.ShadowGap = 1;
            this.pgsH_B.Size = new System.Drawing.Size(174, 30);
            this.pgsH_B.TabIndex = 3;
            this.pgsH_B.TabStop = false;
            this.pgsH_B.Text = "dvProgressh4";
            this.pgsH_B.Value = 0D;
            // 
            // dvContainer5
            // 
            this.dvContainer5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dvContainer5.Controls.Add(this.rangeH_T);
            this.dvContainer5.Controls.Add(this.rangeH_B);
            this.dvContainer5.Controls.Add(this.rangeH_G);
            this.dvContainer5.Controls.Add(this.rangeH_R);
            this.dvContainer5.Controls.Add(this.rangeH_N);
            this.dvContainer5.Location = new System.Drawing.Point(479, 9);
            this.dvContainer5.Margin = new System.Windows.Forms.Padding(0);
            this.dvContainer5.Name = "dvContainer5";
            this.dvContainer5.Padding = new System.Windows.Forms.Padding(7);
            this.dvContainer5.ShadowGap = 1;
            this.dvContainer5.Size = new System.Drawing.Size(194, 194);
            this.dvContainer5.TabIndex = 4;
            this.dvContainer5.TabStop = false;
            this.dvContainer5.Text = "dvContainer5";
            // 
            // rangeH_T
            // 
            this.rangeH_T.BarColor = System.Drawing.Color.Teal;
            this.rangeH_T.BoxColor = null;
            this.rangeH_T.CursorColor = null;
            this.rangeH_T.DrawText = false;
            this.rangeH_T.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeH_T.FormatString = "0";
            this.rangeH_T.Location = new System.Drawing.Point(10, 154);
            this.rangeH_T.Maximum = 100D;
            this.rangeH_T.Minimum = 0D;
            this.rangeH_T.Name = "rangeH_T";
            this.rangeH_T.RangeEnd = 50D;
            this.rangeH_T.RangeStart = 50D;
            this.rangeH_T.Round = null;
            this.rangeH_T.ShadowGap = 1;
            this.rangeH_T.Size = new System.Drawing.Size(174, 30);
            this.rangeH_T.TabIndex = 24;
            this.rangeH_T.Text = "dvRangeSliderh1";
            this.rangeH_T.Tick = 10D;
            // 
            // rangeH_B
            // 
            this.rangeH_B.BarColor = System.Drawing.Color.Navy;
            this.rangeH_B.BoxColor = null;
            this.rangeH_B.CursorColor = null;
            this.rangeH_B.DrawText = false;
            this.rangeH_B.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeH_B.FormatString = "0";
            this.rangeH_B.Location = new System.Drawing.Point(10, 118);
            this.rangeH_B.Maximum = 100D;
            this.rangeH_B.Minimum = 0D;
            this.rangeH_B.Name = "rangeH_B";
            this.rangeH_B.RangeEnd = 50D;
            this.rangeH_B.RangeStart = 50D;
            this.rangeH_B.Round = null;
            this.rangeH_B.ShadowGap = 1;
            this.rangeH_B.Size = new System.Drawing.Size(174, 30);
            this.rangeH_B.TabIndex = 23;
            this.rangeH_B.Text = "dvRangeSliderh1";
            this.rangeH_B.Tick = 10D;
            // 
            // rangeH_G
            // 
            this.rangeH_G.BarColor = System.Drawing.Color.Green;
            this.rangeH_G.BoxColor = null;
            this.rangeH_G.CursorColor = null;
            this.rangeH_G.DrawText = false;
            this.rangeH_G.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeH_G.FormatString = "0";
            this.rangeH_G.Location = new System.Drawing.Point(10, 82);
            this.rangeH_G.Maximum = 100D;
            this.rangeH_G.Minimum = 0D;
            this.rangeH_G.Name = "rangeH_G";
            this.rangeH_G.RangeEnd = 50D;
            this.rangeH_G.RangeStart = 50D;
            this.rangeH_G.Round = null;
            this.rangeH_G.ShadowGap = 1;
            this.rangeH_G.Size = new System.Drawing.Size(174, 30);
            this.rangeH_G.TabIndex = 22;
            this.rangeH_G.Text = "dvRangeSliderh1";
            this.rangeH_G.Tick = 10D;
            // 
            // rangeH_R
            // 
            this.rangeH_R.BarColor = System.Drawing.Color.DarkRed;
            this.rangeH_R.BoxColor = null;
            this.rangeH_R.CursorColor = null;
            this.rangeH_R.DrawText = false;
            this.rangeH_R.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeH_R.FormatString = "0";
            this.rangeH_R.Location = new System.Drawing.Point(10, 46);
            this.rangeH_R.Maximum = 100D;
            this.rangeH_R.Minimum = 0D;
            this.rangeH_R.Name = "rangeH_R";
            this.rangeH_R.RangeEnd = 50D;
            this.rangeH_R.RangeStart = 50D;
            this.rangeH_R.Round = null;
            this.rangeH_R.ShadowGap = 1;
            this.rangeH_R.Size = new System.Drawing.Size(174, 30);
            this.rangeH_R.TabIndex = 21;
            this.rangeH_R.Text = "dvRangeSliderh1";
            this.rangeH_R.Tick = null;
            // 
            // rangeH_N
            // 
            this.rangeH_N.BarColor = System.Drawing.Color.DarkGray;
            this.rangeH_N.BoxColor = null;
            this.rangeH_N.CursorColor = null;
            this.rangeH_N.DrawText = true;
            this.rangeH_N.Font = new System.Drawing.Font("나눔고딕", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rangeH_N.FormatString = "0";
            this.rangeH_N.Location = new System.Drawing.Point(10, 10);
            this.rangeH_N.Maximum = 100D;
            this.rangeH_N.Minimum = 0D;
            this.rangeH_N.Name = "rangeH_N";
            this.rangeH_N.RangeEnd = 50D;
            this.rangeH_N.RangeStart = 50D;
            this.rangeH_N.Round = null;
            this.rangeH_N.ShadowGap = 1;
            this.rangeH_N.Size = new System.Drawing.Size(174, 30);
            this.rangeH_N.TabIndex = 20;
            this.rangeH_N.Text = "dvRangeSliderh1";
            this.rangeH_N.Tick = null;
            // 
            // tblGauge2
            // 
            this.tblGauge2.ColumnCount = 3;
            this.tblGauge2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblGauge2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tblGauge2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tblGauge2.Controls.Add(this.gauge, 0, 0);
            this.tblGauge2.Controls.Add(this.meter, 1, 0);
            this.tblGauge2.Controls.Add(this.knob, 2, 0);
            this.tblGauge2.Location = new System.Drawing.Point(13, 480);
            this.tblGauge2.Name = "tblGauge2";
            this.tblGauge2.RowCount = 1;
            this.tblGauge2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGauge2.Size = new System.Drawing.Size(692, 221);
            this.tblGauge2.TabIndex = 22;
            // 
            // gauge
            // 
            this.gauge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gauge.EmptyColor = null;
            this.gauge.FillColor = null;
            this.gauge.FormatString = "0";
            this.gauge.Gradient = true;
            this.gauge.GraduationLarge = 10D;
            this.gauge.GraduationSmall = 2D;
            this.gauge.Location = new System.Drawing.Point(3, 3);
            this.gauge.Maximum = 100D;
            this.gauge.Minimum = 0D;
            this.gauge.Name = "gauge";
            this.gauge.RemarkFont = new System.Drawing.Font("나눔고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gauge.ShadowGap = 1;
            this.gauge.Size = new System.Drawing.Size(224, 215);
            this.gauge.StartAngle = 135;
            this.gauge.SweepAngle = 270;
            this.gauge.TabIndex = 16;
            this.gauge.TabStop = false;
            this.gauge.Text = "dvGauge1";
            this.gauge.TextDistance = 0.5F;
            this.gauge.Unit = "PERCENT";
            this.gauge.UnitDistance = 0.7F;
            this.gauge.UnitFont = new System.Drawing.Font("나눔고딕", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gauge.Value = 0D;
            this.gauge.ValueDraw = true;
            this.gauge.ValueFont = new System.Drawing.Font("나눔고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // meter
            // 
            this.meter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meter.FillColor = null;
            this.meter.FormatString = "0";
            this.meter.GraduationLarge = 10D;
            this.meter.GraduationSmall = 2D;
            this.meter.Location = new System.Drawing.Point(233, 3);
            this.meter.Maximum = 100D;
            this.meter.Minimum = 0D;
            this.meter.Name = "meter";
            this.meter.NeedleColor = null;
            this.meter.NeedlePointColor = null;
            this.meter.RemarkFont = new System.Drawing.Font("나눔고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.meter.ShadowGap = 1;
            this.meter.Size = new System.Drawing.Size(224, 215);
            this.meter.StartAngle = 135;
            this.meter.SweepAngle = 270;
            this.meter.TabIndex = 17;
            this.meter.TabStop = false;
            this.meter.Text = "dvMeter1";
            this.meter.TextDistance = 0.5F;
            this.meter.Unit = "PERCENT";
            this.meter.UnitDistance = 0.7F;
            this.meter.UnitFont = new System.Drawing.Font("나눔고딕", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.meter.Value = 0D;
            this.meter.ValueDraw = true;
            this.meter.ValueFont = new System.Drawing.Font("나눔고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // knob
            // 
            this.knob.CursorColor = null;
            this.knob.CursorDownColor = null;
            this.knob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.knob.EmptyColor = null;
            this.knob.FillColor = null;
            this.knob.FormatString = "0";
            this.knob.GraduationLarge = 10D;
            this.knob.GraduationSmall = 2D;
            this.knob.KnobColor = null;
            this.knob.Location = new System.Drawing.Point(463, 3);
            this.knob.Maximum = 100D;
            this.knob.Minimum = 0D;
            this.knob.Name = "knob";
            this.knob.RemarkFont = new System.Drawing.Font("나눔고딕", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.knob.ShadowGap = 1;
            this.knob.Size = new System.Drawing.Size(226, 215);
            this.knob.StartAngle = 135;
            this.knob.SweepAngle = 270;
            this.knob.TabIndex = 18;
            this.knob.Text = "dvKnob1";
            this.knob.TextDistance = 0.5F;
            this.knob.Unit = "PERCENT";
            this.knob.UnitDistance = 0.7F;
            this.knob.UnitFont = new System.Drawing.Font("나눔고딕", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.knob.Value = 0D;
            this.knob.ValueDraw = true;
            this.knob.ValueFont = new System.Drawing.Font("나눔고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // tpGraph
            // 
            this.tpGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpGraph.Controls.Add(this.dvTableLayoutPanel1);
            this.tpGraph.Location = new System.Drawing.Point(4, 5);
            this.tpGraph.Name = "tpGraph";
            this.tpGraph.Padding = new System.Windows.Forms.Padding(10);
            this.tpGraph.Size = new System.Drawing.Size(718, 690);
            this.tpGraph.TabIndex = 2;
            this.tpGraph.Text = "tabPage3";
            // 
            // dvTableLayoutPanel1
            // 
            this.dvTableLayoutPanel1.ColumnCount = 3;
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.Controls.Add(this.btnPause, 0, 1);
            this.dvTableLayoutPanel1.Controls.Add(this.tabGraph, 0, 0);
            this.dvTableLayoutPanel1.Controls.Add(this.menuGraph, 1, 1);
            this.dvTableLayoutPanel1.Controls.Add(this.btnGraphRefresh, 2, 1);
            this.dvTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvTableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
            this.dvTableLayoutPanel1.Name = "dvTableLayoutPanel1";
            this.dvTableLayoutPanel1.RowCount = 2;
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.dvTableLayoutPanel1.Size = new System.Drawing.Size(698, 670);
            this.dvTableLayoutPanel1.TabIndex = 1;
            // 
            // btnPause
            // 
            this.btnPause.BackgroundDraw = true;
            this.btnPause.ButtonColor = null;
            this.btnPause.Clickable = true;
            this.btnPause.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnPause.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPause.Gradient = true;
            this.btnPause.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnPause.IconGap = 0;
            this.btnPause.IconImage = null;
            this.btnPause.IconSize = 12F;
            this.btnPause.IconString = "fa-pause";
            this.btnPause.Location = new System.Drawing.Point(3, 637);
            this.btnPause.Name = "btnPause";
            this.btnPause.Round = null;
            this.btnPause.ShadowGap = 1;
            this.btnPause.Size = new System.Drawing.Size(30, 30);
            this.btnPause.TabIndex = 1;
            this.btnPause.Text = null;
            this.btnPause.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnPause.UseKey = false;
            // 
            // tabGraph
            // 
            this.tabGraph.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.dvTableLayoutPanel1.SetColumnSpan(this.tabGraph, 3);
            this.tabGraph.Controls.Add(this.tpBarGraphH);
            this.tabGraph.Controls.Add(this.tpBarGraphV);
            this.tabGraph.Controls.Add(this.tpCircleGraph);
            this.tabGraph.Controls.Add(this.tpLineGraph);
            this.tabGraph.Controls.Add(this.tpTrendGraph);
            this.tabGraph.Controls.Add(this.tpTimeGraph);
            this.tabGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGraph.ItemSize = new System.Drawing.Size(0, 1);
            this.tabGraph.Location = new System.Drawing.Point(3, 3);
            this.tabGraph.Multiline = true;
            this.tabGraph.Name = "tabGraph";
            this.tabGraph.SelectedIndex = 0;
            this.tabGraph.Size = new System.Drawing.Size(692, 628);
            this.tabGraph.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabGraph.TabIndex = 0;
            // 
            // tpBarGraphH
            // 
            this.tpBarGraphH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpBarGraphH.Controls.Add(this.barGraphH);
            this.tpBarGraphH.Location = new System.Drawing.Point(4, 5);
            this.tpBarGraphH.Name = "tpBarGraphH";
            this.tpBarGraphH.Padding = new System.Windows.Forms.Padding(3);
            this.tpBarGraphH.Size = new System.Drawing.Size(684, 619);
            this.tpBarGraphH.TabIndex = 0;
            this.tpBarGraphH.Text = "tabPage1";
            // 
            // barGraphH
            // 
            this.barGraphH.BarGap = 8;
            this.barGraphH.BarSize = 24;
            this.barGraphH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barGraphH.FormatString = "0";
            this.barGraphH.Graduation = 10D;
            this.barGraphH.GraphBackColor = null;
            this.barGraphH.GraphMode = Devinno.Forms.DvBarGraphMode.List;
            this.barGraphH.GridColor = null;
            this.barGraphH.Location = new System.Drawing.Point(3, 3);
            this.barGraphH.Maximum = 100D;
            this.barGraphH.Minimum = 0D;
            this.barGraphH.Name = "barGraphH";
            this.barGraphH.RemarkColor = null;
            this.barGraphH.Scrollable = true;
            this.barGraphH.ShadowGap = 1;
            this.barGraphH.Size = new System.Drawing.Size(678, 613);
            this.barGraphH.TabIndex = 0;
            this.barGraphH.Text = "dvBarGraphh1";
            this.barGraphH.ValueDraw = true;
            // 
            // tpBarGraphV
            // 
            this.tpBarGraphV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpBarGraphV.Controls.Add(this.barGraphV);
            this.tpBarGraphV.Location = new System.Drawing.Point(4, 5);
            this.tpBarGraphV.Name = "tpBarGraphV";
            this.tpBarGraphV.Padding = new System.Windows.Forms.Padding(3);
            this.tpBarGraphV.Size = new System.Drawing.Size(684, 643);
            this.tpBarGraphV.TabIndex = 1;
            this.tpBarGraphV.Text = "tabPage2";
            // 
            // barGraphV
            // 
            this.barGraphV.BarGap = 8;
            this.barGraphV.BarSize = 24;
            this.barGraphV.Cursor = System.Windows.Forms.Cursors.Default;
            this.barGraphV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barGraphV.FormatString = "0";
            this.barGraphV.Graduation = 10D;
            this.barGraphV.GraphBackColor = null;
            this.barGraphV.GraphMode = Devinno.Forms.DvBarGraphMode.List;
            this.barGraphV.GridColor = null;
            this.barGraphV.Location = new System.Drawing.Point(3, 3);
            this.barGraphV.Maximum = 100D;
            this.barGraphV.Minimum = 0D;
            this.barGraphV.Name = "barGraphV";
            this.barGraphV.RemarkColor = null;
            this.barGraphV.Scrollable = true;
            this.barGraphV.ShadowGap = 1;
            this.barGraphV.Size = new System.Drawing.Size(678, 637);
            this.barGraphV.TabIndex = 0;
            this.barGraphV.Text = "dvBarGraphv1";
            this.barGraphV.ValueDraw = true;
            // 
            // tpCircleGraph
            // 
            this.tpCircleGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpCircleGraph.Controls.Add(this.circleGraph);
            this.tpCircleGraph.Location = new System.Drawing.Point(4, 5);
            this.tpCircleGraph.Name = "tpCircleGraph";
            this.tpCircleGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpCircleGraph.Size = new System.Drawing.Size(684, 643);
            this.tpCircleGraph.TabIndex = 2;
            this.tpCircleGraph.Text = "tabPage3";
            // 
            // circleGraph
            // 
            this.circleGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.circleGraph.Gradient = true;
            this.circleGraph.IconSize = 12;
            this.circleGraph.Location = new System.Drawing.Point(3, 3);
            this.circleGraph.Name = "circleGraph";
            this.circleGraph.ShadowGap = 1;
            this.circleGraph.Size = new System.Drawing.Size(678, 637);
            this.circleGraph.TabIndex = 0;
            this.circleGraph.Text = "dvCircleGraph1";
            this.circleGraph.ValueFont = new System.Drawing.Font("나눔고딕", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // tpLineGraph
            // 
            this.tpLineGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpLineGraph.Controls.Add(this.lineGraph);
            this.tpLineGraph.Location = new System.Drawing.Point(4, 5);
            this.tpLineGraph.Name = "tpLineGraph";
            this.tpLineGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpLineGraph.Size = new System.Drawing.Size(684, 643);
            this.tpLineGraph.TabIndex = 3;
            this.tpLineGraph.Text = "tabPage4";
            // 
            // lineGraph
            // 
            this.lineGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineGraph.FormatString = "0";
            this.lineGraph.Graduation = 10D;
            this.lineGraph.GraphBackColor = null;
            this.lineGraph.GridColor = null;
            this.lineGraph.Location = new System.Drawing.Point(3, 3);
            this.lineGraph.Maximum = 100D;
            this.lineGraph.Minimum = 0D;
            this.lineGraph.Name = "lineGraph";
            this.lineGraph.PointDraw = true;
            this.lineGraph.RemarkColor = null;
            this.lineGraph.Scrollable = true;
            this.lineGraph.SectionWidth = 80;
            this.lineGraph.ShadowGap = 1;
            this.lineGraph.Size = new System.Drawing.Size(678, 637);
            this.lineGraph.TabIndex = 0;
            this.lineGraph.Text = "dvLineGraph1";
            this.lineGraph.ValueDraw = true;
            // 
            // tpTrendGraph
            // 
            this.tpTrendGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpTrendGraph.Controls.Add(this.trendGraph);
            this.tpTrendGraph.Location = new System.Drawing.Point(4, 5);
            this.tpTrendGraph.Name = "tpTrendGraph";
            this.tpTrendGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpTrendGraph.Size = new System.Drawing.Size(684, 643);
            this.tpTrendGraph.TabIndex = 4;
            this.tpTrendGraph.Text = "tabPage5";
            // 
            // trendGraph
            // 
            this.trendGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trendGraph.GraphBackColor = null;
            this.trendGraph.GridColor = null;
            this.trendGraph.Interval = 1000;
            this.trendGraph.Location = new System.Drawing.Point(3, 3);
            this.trendGraph.MaximumXScale = System.TimeSpan.Parse("01:00:00");
            this.trendGraph.Name = "trendGraph";
            this.trendGraph.Pause = false;
            this.trendGraph.RemarkColor = null;
            this.trendGraph.ShadowGap = 1;
            this.trendGraph.Size = new System.Drawing.Size(678, 637);
            this.trendGraph.TabIndex = 0;
            this.trendGraph.Text = "dvTrendGraph1";
            this.trendGraph.TimeFormatString = null;
            this.trendGraph.ValueFormatString = null;
            this.trendGraph.XAxisGraduation = System.TimeSpan.Parse("00:10:00");
            this.trendGraph.XAxisGridDraw = true;
            this.trendGraph.XScale = System.TimeSpan.Parse("01:00:00");
            this.trendGraph.YAxisGraduationCount = 10;
            this.trendGraph.YAxisGridDraw = true;
            // 
            // tpTimeGraph
            // 
            this.tpTimeGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpTimeGraph.Controls.Add(this.timeGraph);
            this.tpTimeGraph.Location = new System.Drawing.Point(4, 5);
            this.tpTimeGraph.Name = "tpTimeGraph";
            this.tpTimeGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tpTimeGraph.Size = new System.Drawing.Size(684, 643);
            this.tpTimeGraph.TabIndex = 5;
            this.tpTimeGraph.Text = "tabPage6";
            // 
            // timeGraph
            // 
            this.timeGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeGraph.GraphBackColor = null;
            this.timeGraph.GridColor = null;
            this.timeGraph.Location = new System.Drawing.Point(3, 3);
            this.timeGraph.Name = "timeGraph";
            this.timeGraph.RemarkColor = null;
            this.timeGraph.ShadowGap = 1;
            this.timeGraph.Size = new System.Drawing.Size(678, 637);
            this.timeGraph.TabIndex = 0;
            this.timeGraph.Text = "dvTimeGraph1";
            this.timeGraph.TimeFormatString = null;
            this.timeGraph.ValueFormatString = null;
            this.timeGraph.XAxisGraduation = System.TimeSpan.Parse("00:10:00");
            this.timeGraph.XAxisGridDraw = true;
            this.timeGraph.XScale = System.TimeSpan.Parse("01:00:00");
            this.timeGraph.YAxisGraduationCount = 10;
            this.timeGraph.YAxisGridDraw = true;
            // 
            // menuGraph
            // 
            this.menuGraph.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.menuGraph.BackgroundDraw = true;
            this.menuGraph.ButtonColor = null;
            this.menuGraph.CheckdButtonColor = null;
            this.menuGraph.Clickable = true;
            this.menuGraph.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.menuGraph.Direction = Devinno.Forms.DvDirectionHV.Horizon;
            this.menuGraph.Gradient = true;
            this.menuGraph.Location = new System.Drawing.Point(104, 637);
            this.menuGraph.Name = "menuGraph";
            this.menuGraph.Round = null;
            this.menuGraph.SelectionMode = false;
            this.menuGraph.ShadowGap = 1;
            this.menuGraph.Size = new System.Drawing.Size(490, 30);
            this.menuGraph.TabIndex = 1;
            this.menuGraph.Text = "dvButtons1";
            // 
            // btnGraphRefresh
            // 
            this.btnGraphRefresh.BackgroundDraw = true;
            this.btnGraphRefresh.ButtonColor = null;
            this.btnGraphRefresh.Clickable = true;
            this.btnGraphRefresh.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnGraphRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGraphRefresh.Gradient = true;
            this.btnGraphRefresh.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnGraphRefresh.IconGap = 0;
            this.btnGraphRefresh.IconImage = null;
            this.btnGraphRefresh.IconSize = 12F;
            this.btnGraphRefresh.IconString = "fa-rotate";
            this.btnGraphRefresh.Location = new System.Drawing.Point(665, 637);
            this.btnGraphRefresh.Name = "btnGraphRefresh";
            this.btnGraphRefresh.Round = null;
            this.btnGraphRefresh.ShadowGap = 1;
            this.btnGraphRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnGraphRefresh.TabIndex = 2;
            this.btnGraphRefresh.Text = null;
            this.btnGraphRefresh.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnGraphRefresh.UseKey = false;
            // 
            // tpContainer
            // 
            this.tpContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpContainer.Controls.Add(this.dvScrollablePanel1);
            this.tpContainer.Location = new System.Drawing.Point(4, 5);
            this.tpContainer.Name = "tpContainer";
            this.tpContainer.Padding = new System.Windows.Forms.Padding(10);
            this.tpContainer.Size = new System.Drawing.Size(718, 690);
            this.tpContainer.TabIndex = 4;
            this.tpContainer.Text = "tabPage5";
            // 
            // dvScrollablePanel1
            // 
            this.dvScrollablePanel1.AutoScroll = true;
            this.dvScrollablePanel1.Controls.Add(this.dvTableLayoutPanel2);
            this.dvScrollablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvScrollablePanel1.Location = new System.Drawing.Point(10, 10);
            this.dvScrollablePanel1.Name = "dvScrollablePanel1";
            this.dvScrollablePanel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dvScrollablePanel1.ShadowGap = 1;
            this.dvScrollablePanel1.Size = new System.Drawing.Size(698, 670);
            this.dvScrollablePanel1.TabIndex = 5;
            this.dvScrollablePanel1.TabStop = false;
            this.dvScrollablePanel1.Text = "dvScrollablePanel1";
            // 
            // dvTableLayoutPanel2
            // 
            this.dvTableLayoutPanel2.ColumnCount = 2;
            this.dvTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dvTableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dvTableLayoutPanel2.Controls.Add(this.panel, 0, 0);
            this.dvTableLayoutPanel2.Controls.Add(this.pnlBox, 1, 1);
            this.dvTableLayoutPanel2.Controls.Add(this.borderPanel, 1, 0);
            this.dvTableLayoutPanel2.Controls.Add(this.grpBox, 0, 1);
            this.dvTableLayoutPanel2.Controls.Add(this.tab2, 0, 2);
            this.dvTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.dvTableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.dvTableLayoutPanel2.Name = "dvTableLayoutPanel2";
            this.dvTableLayoutPanel2.RowCount = 3;
            this.dvTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.dvTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.dvTableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dvTableLayoutPanel2.Size = new System.Drawing.Size(671, 1041);
            this.dvTableLayoutPanel2.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.DrawTitle = true;
            this.panel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.panel.IconGap = 3;
            this.panel.IconImage = null;
            this.panel.IconSize = 12F;
            this.panel.IconString = "fa-chalkboard";
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.panel.PanelColor = null;
            this.panel.Round = null;
            this.panel.ShadowGap = 1;
            this.panel.Size = new System.Drawing.Size(329, 194);
            this.panel.TabIndex = 1;
            this.panel.TabStop = false;
            this.panel.Text = "Panel";
            this.panel.TextPadding = new System.Windows.Forms.Padding(0);
            this.panel.TitleHeight = 30;
            // 
            // pnlBox
            // 
            this.pnlBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlBox.Border = true;
            this.pnlBox.Corner = null;
            this.pnlBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBox.DrawTitle = true;
            this.pnlBox.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.pnlBox.IconGap = 3;
            this.pnlBox.IconImage = null;
            this.pnlBox.IconSize = 12F;
            this.pnlBox.IconString = "fa-box";
            this.pnlBox.Location = new System.Drawing.Point(338, 203);
            this.pnlBox.Name = "pnlBox";
            this.pnlBox.PanelColor = null;
            this.pnlBox.Round = null;
            this.pnlBox.ShadowGap = 1;
            this.pnlBox.Size = new System.Drawing.Size(330, 194);
            this.pnlBox.TabIndex = 4;
            this.pnlBox.TabStop = false;
            this.pnlBox.Text = "Box Panel";
            this.pnlBox.TextPadding = new System.Windows.Forms.Padding(0);
            // 
            // borderPanel
            // 
            this.borderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.borderPanel.BorderColor = null;
            this.borderPanel.BorderWidth = 5;
            this.borderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderPanel.DrawTitle = true;
            this.borderPanel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.borderPanel.IconGap = 3;
            this.borderPanel.IconImage = null;
            this.borderPanel.IconSize = 12F;
            this.borderPanel.IconString = "fa-border-top-left";
            this.borderPanel.Location = new System.Drawing.Point(338, 3);
            this.borderPanel.Name = "borderPanel";
            this.borderPanel.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.borderPanel.ShadowGap = 1;
            this.borderPanel.Size = new System.Drawing.Size(330, 194);
            this.borderPanel.TabIndex = 2;
            this.borderPanel.TabStop = false;
            this.borderPanel.Text = "Border Panel";
            this.borderPanel.TextPadding = new System.Windows.Forms.Padding(0);
            this.borderPanel.TitleHeight = 30;
            // 
            // grpBox
            // 
            this.grpBox.BorderColor = null;
            this.grpBox.BorderThickness = Devinno.Forms.BorderThickness.Thin;
            this.grpBox.Corner = null;
            this.grpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBox.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.grpBox.IconGap = 3;
            this.grpBox.IconImage = null;
            this.grpBox.IconSize = 12F;
            this.grpBox.IconString = "fa-object-group";
            this.grpBox.Location = new System.Drawing.Point(3, 203);
            this.grpBox.Name = "grpBox";
            this.grpBox.ShadowGap = 1;
            this.grpBox.Size = new System.Drawing.Size(329, 194);
            this.grpBox.TabIndex = 3;
            this.grpBox.TabStop = false;
            this.grpBox.Text = "GroupBox";
            this.grpBox.TextPadding = new System.Windows.Forms.Padding(0);
            // 
            // tab2
            // 
            this.tab2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.dvTableLayoutPanel2.SetColumnSpan(this.tab2, 2);
            this.tab2.Controls.Add(this.tabPage1);
            this.tab2.Controls.Add(this.tabPage2);
            this.tab2.Controls.Add(this.tabPage3);
            this.tab2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab2.DrawBoarder = true;
            this.tab2.ItemSize = new System.Drawing.Size(40, 120);
            this.tab2.Location = new System.Drawing.Point(3, 403);
            this.tab2.Multiline = true;
            this.tab2.Name = "tab2";
            this.tab2.PointColor = null;
            this.tab2.Round = null;
            this.tab2.SelectedIndex = 0;
            this.tab2.Size = new System.Drawing.Size(665, 635);
            this.tab2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab2.TabBackColor = null;
            this.tab2.TabColor = null;
            this.tab2.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tabPage1.Location = new System.Drawing.Point(124, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(10);
            this.tabPage1.Size = new System.Drawing.Size(537, 627);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "테스트 1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tabPage2.Location = new System.Drawing.Point(124, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(537, 627);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "테스트 2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.tabPage3.Location = new System.Drawing.Point(124, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(537, 627);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "테스트 3";
            // 
            // tpDialog
            // 
            this.tpDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpDialog.Controls.Add(this.btnPortSettingSimple);
            this.tpDialog.Controls.Add(this.btnPortSetting);
            this.tpDialog.Controls.Add(this.dvLabel6);
            this.tpDialog.Controls.Add(this.inCombo);
            this.tpDialog.Controls.Add(this.btnSelCheck);
            this.tpDialog.Controls.Add(this.btnSelCombo);
            this.tpDialog.Controls.Add(this.btnSelRadio);
            this.tpDialog.Controls.Add(this.btnSelSelector);
            this.tpDialog.Controls.Add(this.btnSelWheel);
            this.tpDialog.Controls.Add(this.dvLabel5);
            this.tpDialog.Controls.Add(this.btnInputInt);
            this.tpDialog.Controls.Add(this.btnInputFloat);
            this.tpDialog.Controls.Add(this.btnInputString);
            this.tpDialog.Controls.Add(this.btnInputClass);
            this.tpDialog.Controls.Add(this.dvLabel4);
            this.tpDialog.Controls.Add(this.inDoW);
            this.tpDialog.Controls.Add(this.datePicker);
            this.tpDialog.Controls.Add(this.dvLabel3);
            this.tpDialog.Controls.Add(this.colorPicker);
            this.tpDialog.Controls.Add(this.lblMb);
            this.tpDialog.Controls.Add(this.btnMbYesNoCancel);
            this.tpDialog.Controls.Add(this.btnMbOkCancel);
            this.tpDialog.Controls.Add(this.btnMbYesNo);
            this.tpDialog.Controls.Add(this.btnMbOK);
            this.tpDialog.Controls.Add(this.dvLabel2);
            this.tpDialog.Controls.Add(this.lblKeyResult);
            this.tpDialog.Controls.Add(this.btnKeyboardEng);
            this.tpDialog.Controls.Add(this.btnKeyboardHan);
            this.tpDialog.Controls.Add(this.btnKeypadPassword);
            this.tpDialog.Controls.Add(this.btnKeypadDecimal);
            this.tpDialog.Controls.Add(this.dvLabel1);
            this.tpDialog.Controls.Add(this.btnKeypadInt);
            this.tpDialog.Location = new System.Drawing.Point(4, 5);
            this.tpDialog.Name = "tpDialog";
            this.tpDialog.Padding = new System.Windows.Forms.Padding(10);
            this.tpDialog.Size = new System.Drawing.Size(718, 690);
            this.tpDialog.TabIndex = 6;
            // 
            // btnPortSettingSimple
            // 
            this.btnPortSettingSimple.BackgroundDraw = true;
            this.btnPortSettingSimple.ButtonColor = null;
            this.btnPortSettingSimple.Clickable = true;
            this.btnPortSettingSimple.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnPortSettingSimple.Gradient = true;
            this.btnPortSettingSimple.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnPortSettingSimple.IconGap = 0;
            this.btnPortSettingSimple.IconImage = null;
            this.btnPortSettingSimple.IconSize = 12F;
            this.btnPortSettingSimple.IconString = null;
            this.btnPortSettingSimple.Location = new System.Drawing.Point(169, 367);
            this.btnPortSettingSimple.Name = "btnPortSettingSimple";
            this.btnPortSettingSimple.Round = null;
            this.btnPortSettingSimple.ShadowGap = 1;
            this.btnPortSettingSimple.Size = new System.Drawing.Size(150, 30);
            this.btnPortSettingSimple.TabIndex = 31;
            this.btnPortSettingSimple.Text = "포트 설정 ( 단순 )";
            this.btnPortSettingSimple.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnPortSettingSimple.UseKey = false;
            // 
            // btnPortSetting
            // 
            this.btnPortSetting.BackgroundDraw = true;
            this.btnPortSetting.ButtonColor = null;
            this.btnPortSetting.Clickable = true;
            this.btnPortSetting.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnPortSetting.Gradient = true;
            this.btnPortSetting.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnPortSetting.IconGap = 0;
            this.btnPortSetting.IconImage = null;
            this.btnPortSetting.IconSize = 12F;
            this.btnPortSetting.IconString = null;
            this.btnPortSetting.Location = new System.Drawing.Point(169, 331);
            this.btnPortSetting.Name = "btnPortSetting";
            this.btnPortSetting.Round = null;
            this.btnPortSetting.ShadowGap = 1;
            this.btnPortSetting.Size = new System.Drawing.Size(150, 30);
            this.btnPortSetting.TabIndex = 30;
            this.btnPortSetting.Text = "포트 설정";
            this.btnPortSetting.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnPortSetting.UseKey = false;
            // 
            // dvLabel6
            // 
            this.dvLabel6.BackgroundDraw = false;
            this.dvLabel6.BorderColor = null;
            this.dvLabel6.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel6.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel6.IconGap = 0;
            this.dvLabel6.IconImage = null;
            this.dvLabel6.IconSize = 12F;
            this.dvLabel6.IconString = null;
            this.dvLabel6.LabelColor = null;
            this.dvLabel6.Location = new System.Drawing.Point(169, 295);
            this.dvLabel6.Name = "dvLabel6";
            this.dvLabel6.Round = null;
            this.dvLabel6.ShadowGap = 1;
            this.dvLabel6.Size = new System.Drawing.Size(150, 30);
            this.dvLabel6.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel6.TabIndex = 29;
            this.dvLabel6.TabStop = false;
            this.dvLabel6.Text = "시리얼 포트";
            this.dvLabel6.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel6.Unit = "";
            this.dvLabel6.UnitWidth = null;
            // 
            // inCombo
            // 
            this.inCombo.Button = null;
            this.inCombo.ButtonColor = null;
            this.inCombo.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inCombo.ButtonIconGap = 0;
            this.inCombo.ButtonIconImage = null;
            this.inCombo.ButtonIconSize = 12F;
            this.inCombo.ButtonIconString = null;
            this.inCombo.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inCombo.ButtonWidth = null;
            this.inCombo.ItemHeight = 30;
            this.inCombo.ItemViewCount = 8;
            this.inCombo.Location = new System.Drawing.Point(13, 439);
            this.inCombo.Name = "inCombo";
            this.inCombo.Round = null;
            this.inCombo.SelectedIndex = -1;
            this.inCombo.ShadowGap = 1;
            this.inCombo.Size = new System.Drawing.Size(150, 30);
            this.inCombo.TabIndex = 28;
            this.inCombo.Text = "dvValueInputCombo1";
            this.inCombo.Title = null;
            this.inCombo.TitleColor = null;
            this.inCombo.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inCombo.TitleIconGap = 0;
            this.inCombo.TitleIconImage = null;
            this.inCombo.TitleIconSize = 12F;
            this.inCombo.TitleIconString = null;
            this.inCombo.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inCombo.TitleWidth = null;
            this.inCombo.Unit = "";
            this.inCombo.UnitWidth = null;
            this.inCombo.ValueColor = null;
            // 
            // btnSelCheck
            // 
            this.btnSelCheck.BackgroundDraw = true;
            this.btnSelCheck.ButtonColor = null;
            this.btnSelCheck.Clickable = true;
            this.btnSelCheck.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSelCheck.Gradient = true;
            this.btnSelCheck.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSelCheck.IconGap = 0;
            this.btnSelCheck.IconImage = null;
            this.btnSelCheck.IconSize = 12F;
            this.btnSelCheck.IconString = null;
            this.btnSelCheck.Location = new System.Drawing.Point(481, 193);
            this.btnSelCheck.Name = "btnSelCheck";
            this.btnSelCheck.Round = null;
            this.btnSelCheck.ShadowGap = 1;
            this.btnSelCheck.Size = new System.Drawing.Size(150, 30);
            this.btnSelCheck.TabIndex = 27;
            this.btnSelCheck.Text = "Check";
            this.btnSelCheck.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSelCheck.UseKey = false;
            // 
            // btnSelCombo
            // 
            this.btnSelCombo.BackgroundDraw = true;
            this.btnSelCombo.ButtonColor = null;
            this.btnSelCombo.Clickable = true;
            this.btnSelCombo.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSelCombo.Gradient = true;
            this.btnSelCombo.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSelCombo.IconGap = 0;
            this.btnSelCombo.IconImage = null;
            this.btnSelCombo.IconSize = 12F;
            this.btnSelCombo.IconString = null;
            this.btnSelCombo.Location = new System.Drawing.Point(481, 121);
            this.btnSelCombo.Name = "btnSelCombo";
            this.btnSelCombo.Round = null;
            this.btnSelCombo.ShadowGap = 1;
            this.btnSelCombo.Size = new System.Drawing.Size(150, 30);
            this.btnSelCombo.TabIndex = 26;
            this.btnSelCombo.Text = "Combo";
            this.btnSelCombo.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSelCombo.UseKey = false;
            // 
            // btnSelRadio
            // 
            this.btnSelRadio.BackgroundDraw = true;
            this.btnSelRadio.ButtonColor = null;
            this.btnSelRadio.Clickable = true;
            this.btnSelRadio.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSelRadio.Gradient = true;
            this.btnSelRadio.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSelRadio.IconGap = 0;
            this.btnSelRadio.IconImage = null;
            this.btnSelRadio.IconSize = 12F;
            this.btnSelRadio.IconString = null;
            this.btnSelRadio.Location = new System.Drawing.Point(481, 157);
            this.btnSelRadio.Name = "btnSelRadio";
            this.btnSelRadio.Round = null;
            this.btnSelRadio.ShadowGap = 1;
            this.btnSelRadio.Size = new System.Drawing.Size(150, 30);
            this.btnSelRadio.TabIndex = 25;
            this.btnSelRadio.Text = "Radio";
            this.btnSelRadio.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSelRadio.UseKey = false;
            // 
            // btnSelSelector
            // 
            this.btnSelSelector.BackgroundDraw = true;
            this.btnSelSelector.ButtonColor = null;
            this.btnSelSelector.Clickable = true;
            this.btnSelSelector.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSelSelector.Gradient = true;
            this.btnSelSelector.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSelSelector.IconGap = 0;
            this.btnSelSelector.IconImage = null;
            this.btnSelSelector.IconSize = 12F;
            this.btnSelSelector.IconString = null;
            this.btnSelSelector.Location = new System.Drawing.Point(481, 85);
            this.btnSelSelector.Name = "btnSelSelector";
            this.btnSelSelector.Round = null;
            this.btnSelSelector.ShadowGap = 1;
            this.btnSelSelector.Size = new System.Drawing.Size(150, 30);
            this.btnSelSelector.TabIndex = 24;
            this.btnSelSelector.Text = "Selector";
            this.btnSelSelector.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSelSelector.UseKey = false;
            // 
            // btnSelWheel
            // 
            this.btnSelWheel.BackgroundDraw = true;
            this.btnSelWheel.ButtonColor = null;
            this.btnSelWheel.Clickable = true;
            this.btnSelWheel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnSelWheel.Gradient = true;
            this.btnSelWheel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnSelWheel.IconGap = 0;
            this.btnSelWheel.IconImage = null;
            this.btnSelWheel.IconSize = 12F;
            this.btnSelWheel.IconString = null;
            this.btnSelWheel.Location = new System.Drawing.Point(481, 49);
            this.btnSelWheel.Name = "btnSelWheel";
            this.btnSelWheel.Round = null;
            this.btnSelWheel.ShadowGap = 1;
            this.btnSelWheel.Size = new System.Drawing.Size(150, 30);
            this.btnSelWheel.TabIndex = 23;
            this.btnSelWheel.Text = "Wheel";
            this.btnSelWheel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnSelWheel.UseKey = false;
            // 
            // dvLabel5
            // 
            this.dvLabel5.BackgroundDraw = false;
            this.dvLabel5.BorderColor = null;
            this.dvLabel5.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel5.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel5.IconGap = 0;
            this.dvLabel5.IconImage = null;
            this.dvLabel5.IconSize = 12F;
            this.dvLabel5.IconString = null;
            this.dvLabel5.LabelColor = null;
            this.dvLabel5.Location = new System.Drawing.Point(481, 13);
            this.dvLabel5.Name = "dvLabel5";
            this.dvLabel5.Round = null;
            this.dvLabel5.ShadowGap = 1;
            this.dvLabel5.Size = new System.Drawing.Size(150, 30);
            this.dvLabel5.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel5.TabIndex = 22;
            this.dvLabel5.TabStop = false;
            this.dvLabel5.Text = "선택";
            this.dvLabel5.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel5.Unit = "";
            this.dvLabel5.UnitWidth = null;
            // 
            // btnInputInt
            // 
            this.btnInputInt.BackgroundDraw = true;
            this.btnInputInt.ButtonColor = null;
            this.btnInputInt.Clickable = true;
            this.btnInputInt.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnInputInt.Gradient = true;
            this.btnInputInt.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnInputInt.IconGap = 0;
            this.btnInputInt.IconImage = null;
            this.btnInputInt.IconSize = 12F;
            this.btnInputInt.IconString = null;
            this.btnInputInt.Location = new System.Drawing.Point(325, 121);
            this.btnInputInt.Name = "btnInputInt";
            this.btnInputInt.Round = null;
            this.btnInputInt.ShadowGap = 1;
            this.btnInputInt.Size = new System.Drawing.Size(150, 30);
            this.btnInputInt.TabIndex = 21;
            this.btnInputInt.Text = "Integer";
            this.btnInputInt.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnInputInt.UseKey = false;
            // 
            // btnInputFloat
            // 
            this.btnInputFloat.BackgroundDraw = true;
            this.btnInputFloat.ButtonColor = null;
            this.btnInputFloat.Clickable = true;
            this.btnInputFloat.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnInputFloat.Gradient = true;
            this.btnInputFloat.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnInputFloat.IconGap = 0;
            this.btnInputFloat.IconImage = null;
            this.btnInputFloat.IconSize = 12F;
            this.btnInputFloat.IconString = null;
            this.btnInputFloat.Location = new System.Drawing.Point(325, 157);
            this.btnInputFloat.Name = "btnInputFloat";
            this.btnInputFloat.Round = null;
            this.btnInputFloat.ShadowGap = 1;
            this.btnInputFloat.Size = new System.Drawing.Size(150, 30);
            this.btnInputFloat.TabIndex = 20;
            this.btnInputFloat.Text = "Float";
            this.btnInputFloat.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnInputFloat.UseKey = false;
            // 
            // btnInputString
            // 
            this.btnInputString.BackgroundDraw = true;
            this.btnInputString.ButtonColor = null;
            this.btnInputString.Clickable = true;
            this.btnInputString.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnInputString.Gradient = true;
            this.btnInputString.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnInputString.IconGap = 0;
            this.btnInputString.IconImage = null;
            this.btnInputString.IconSize = 12F;
            this.btnInputString.IconString = null;
            this.btnInputString.Location = new System.Drawing.Point(325, 85);
            this.btnInputString.Name = "btnInputString";
            this.btnInputString.Round = null;
            this.btnInputString.ShadowGap = 1;
            this.btnInputString.Size = new System.Drawing.Size(150, 30);
            this.btnInputString.TabIndex = 19;
            this.btnInputString.Text = "String";
            this.btnInputString.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnInputString.UseKey = false;
            // 
            // btnInputClass
            // 
            this.btnInputClass.BackgroundDraw = true;
            this.btnInputClass.ButtonColor = null;
            this.btnInputClass.Clickable = true;
            this.btnInputClass.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnInputClass.Gradient = true;
            this.btnInputClass.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnInputClass.IconGap = 0;
            this.btnInputClass.IconImage = null;
            this.btnInputClass.IconSize = 12F;
            this.btnInputClass.IconString = null;
            this.btnInputClass.Location = new System.Drawing.Point(325, 49);
            this.btnInputClass.Name = "btnInputClass";
            this.btnInputClass.Round = null;
            this.btnInputClass.ShadowGap = 1;
            this.btnInputClass.Size = new System.Drawing.Size(150, 30);
            this.btnInputClass.TabIndex = 18;
            this.btnInputClass.Text = "Class";
            this.btnInputClass.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnInputClass.UseKey = false;
            // 
            // dvLabel4
            // 
            this.dvLabel4.BackgroundDraw = false;
            this.dvLabel4.BorderColor = null;
            this.dvLabel4.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel4.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel4.IconGap = 0;
            this.dvLabel4.IconImage = null;
            this.dvLabel4.IconSize = 12F;
            this.dvLabel4.IconString = null;
            this.dvLabel4.LabelColor = null;
            this.dvLabel4.Location = new System.Drawing.Point(325, 13);
            this.dvLabel4.Name = "dvLabel4";
            this.dvLabel4.Round = null;
            this.dvLabel4.ShadowGap = 1;
            this.dvLabel4.Size = new System.Drawing.Size(150, 30);
            this.dvLabel4.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel4.TabIndex = 17;
            this.dvLabel4.TabStop = false;
            this.dvLabel4.Text = "입력";
            this.dvLabel4.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel4.Unit = "";
            this.dvLabel4.UnitWidth = null;
            // 
            // inDoW
            // 
            this.inDoW.Button = null;
            this.inDoW.ButtonColor = null;
            this.inDoW.ButtonIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inDoW.ButtonIconGap = 0;
            this.inDoW.ButtonIconImage = null;
            this.inDoW.ButtonIconSize = 12F;
            this.inDoW.ButtonIconString = null;
            this.inDoW.ButtonTextPadding = new System.Windows.Forms.Padding(0);
            this.inDoW.ButtonWidth = null;
            this.inDoW.ItemHeight = 30;
            this.inDoW.ItemViewCount = 5;
            this.inDoW.Location = new System.Drawing.Point(13, 403);
            this.inDoW.Name = "inDoW";
            this.inDoW.Round = null;
            this.inDoW.SelectedIndex = -1;
            this.inDoW.ShadowGap = 1;
            this.inDoW.Size = new System.Drawing.Size(150, 30);
            this.inDoW.TabIndex = 16;
            this.inDoW.Text = "dvValueInputSelector1";
            this.inDoW.Title = null;
            this.inDoW.TitleColor = null;
            this.inDoW.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.inDoW.TitleIconGap = 0;
            this.inDoW.TitleIconImage = null;
            this.inDoW.TitleIconSize = 12F;
            this.inDoW.TitleIconString = null;
            this.inDoW.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.inDoW.TitleWidth = null;
            this.inDoW.Unit = "";
            this.inDoW.UnitWidth = null;
            this.inDoW.ValueColor = null;
            // 
            // datePicker
            // 
            this.datePicker.ButtonColor = null;
            this.datePicker.ButtonWidth = 30;
            this.datePicker.DateTimeType = Devinno.Forms.DateTimePickerType.Time;
            this.datePicker.Location = new System.Drawing.Point(13, 367);
            this.datePicker.Name = "datePicker";
            this.datePicker.Round = null;
            this.datePicker.SelectedValue = new System.DateTime(2022, 8, 29, 18, 30, 52, 248);
            this.datePicker.ShadowGap = 1;
            this.datePicker.Size = new System.Drawing.Size(150, 30);
            this.datePicker.TabIndex = 15;
            this.datePicker.Text = "dvDateTimePicker1";
            this.datePicker.Title = null;
            this.datePicker.TitleColor = null;
            this.datePicker.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.datePicker.TitleIconGap = 0;
            this.datePicker.TitleIconImage = null;
            this.datePicker.TitleIconSize = 12F;
            this.datePicker.TitleIconString = null;
            this.datePicker.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.datePicker.TitleWidth = null;
            this.datePicker.ValueColor = null;
            // 
            // dvLabel3
            // 
            this.dvLabel3.BackgroundDraw = false;
            this.dvLabel3.BorderColor = null;
            this.dvLabel3.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel3.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel3.IconGap = 0;
            this.dvLabel3.IconImage = null;
            this.dvLabel3.IconSize = 12F;
            this.dvLabel3.IconString = null;
            this.dvLabel3.LabelColor = null;
            this.dvLabel3.Location = new System.Drawing.Point(13, 295);
            this.dvLabel3.Name = "dvLabel3";
            this.dvLabel3.Round = null;
            this.dvLabel3.ShadowGap = 1;
            this.dvLabel3.Size = new System.Drawing.Size(150, 30);
            this.dvLabel3.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel3.TabIndex = 14;
            this.dvLabel3.TabStop = false;
            this.dvLabel3.Text = "기타";
            this.dvLabel3.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel3.Unit = "";
            this.dvLabel3.UnitWidth = null;
            // 
            // colorPicker
            // 
            this.colorPicker.ButtonColor = null;
            this.colorPicker.ButtonWidth = 30;
            this.colorPicker.CodeType = Devinno.Tools.ColorCodeType.CodeRGB;
            this.colorPicker.Location = new System.Drawing.Point(13, 331);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Round = null;
            this.colorPicker.SelectedColor = System.Drawing.Color.White;
            this.colorPicker.ShadowGap = 1;
            this.colorPicker.Size = new System.Drawing.Size(150, 30);
            this.colorPicker.TabIndex = 13;
            this.colorPicker.Title = "";
            this.colorPicker.TitleColor = null;
            this.colorPicker.TitleIconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.colorPicker.TitleIconGap = 0;
            this.colorPicker.TitleIconImage = null;
            this.colorPicker.TitleIconSize = 12F;
            this.colorPicker.TitleIconString = null;
            this.colorPicker.TitleTextPadding = new System.Windows.Forms.Padding(0);
            this.colorPicker.TitleWidth = null;
            this.colorPicker.ValueColor = null;
            // 
            // lblMb
            // 
            this.lblMb.BackgroundDraw = true;
            this.lblMb.BorderColor = null;
            this.lblMb.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblMb.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblMb.IconGap = 0;
            this.lblMb.IconImage = null;
            this.lblMb.IconSize = 12F;
            this.lblMb.IconString = null;
            this.lblMb.LabelColor = null;
            this.lblMb.Location = new System.Drawing.Point(169, 193);
            this.lblMb.Name = "lblMb";
            this.lblMb.Round = null;
            this.lblMb.ShadowGap = 1;
            this.lblMb.Size = new System.Drawing.Size(150, 30);
            this.lblMb.Style = Devinno.Forms.Embossing.FlatConcave;
            this.lblMb.TabIndex = 12;
            this.lblMb.TabStop = false;
            this.lblMb.Text = null;
            this.lblMb.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblMb.Unit = "";
            this.lblMb.UnitWidth = null;
            // 
            // btnMbYesNoCancel
            // 
            this.btnMbYesNoCancel.BackgroundDraw = true;
            this.btnMbYesNoCancel.ButtonColor = null;
            this.btnMbYesNoCancel.Clickable = true;
            this.btnMbYesNoCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMbYesNoCancel.Gradient = true;
            this.btnMbYesNoCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMbYesNoCancel.IconGap = 0;
            this.btnMbYesNoCancel.IconImage = null;
            this.btnMbYesNoCancel.IconSize = 12F;
            this.btnMbYesNoCancel.IconString = null;
            this.btnMbYesNoCancel.Location = new System.Drawing.Point(169, 157);
            this.btnMbYesNoCancel.Name = "btnMbYesNoCancel";
            this.btnMbYesNoCancel.Round = null;
            this.btnMbYesNoCancel.ShadowGap = 1;
            this.btnMbYesNoCancel.Size = new System.Drawing.Size(150, 30);
            this.btnMbYesNoCancel.TabIndex = 11;
            this.btnMbYesNoCancel.Text = "Yes / No / Cancel";
            this.btnMbYesNoCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMbYesNoCancel.UseKey = false;
            // 
            // btnMbOkCancel
            // 
            this.btnMbOkCancel.BackgroundDraw = true;
            this.btnMbOkCancel.ButtonColor = null;
            this.btnMbOkCancel.Clickable = true;
            this.btnMbOkCancel.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMbOkCancel.Gradient = true;
            this.btnMbOkCancel.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMbOkCancel.IconGap = 0;
            this.btnMbOkCancel.IconImage = null;
            this.btnMbOkCancel.IconSize = 12F;
            this.btnMbOkCancel.IconString = null;
            this.btnMbOkCancel.Location = new System.Drawing.Point(169, 121);
            this.btnMbOkCancel.Name = "btnMbOkCancel";
            this.btnMbOkCancel.Round = null;
            this.btnMbOkCancel.ShadowGap = 1;
            this.btnMbOkCancel.Size = new System.Drawing.Size(150, 30);
            this.btnMbOkCancel.TabIndex = 10;
            this.btnMbOkCancel.Text = "Ok / Cancel";
            this.btnMbOkCancel.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMbOkCancel.UseKey = false;
            // 
            // btnMbYesNo
            // 
            this.btnMbYesNo.BackgroundDraw = true;
            this.btnMbYesNo.ButtonColor = null;
            this.btnMbYesNo.Clickable = true;
            this.btnMbYesNo.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMbYesNo.Gradient = true;
            this.btnMbYesNo.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMbYesNo.IconGap = 0;
            this.btnMbYesNo.IconImage = null;
            this.btnMbYesNo.IconSize = 12F;
            this.btnMbYesNo.IconString = null;
            this.btnMbYesNo.Location = new System.Drawing.Point(169, 85);
            this.btnMbYesNo.Name = "btnMbYesNo";
            this.btnMbYesNo.Round = null;
            this.btnMbYesNo.ShadowGap = 1;
            this.btnMbYesNo.Size = new System.Drawing.Size(150, 30);
            this.btnMbYesNo.TabIndex = 9;
            this.btnMbYesNo.Text = "Yes / No";
            this.btnMbYesNo.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMbYesNo.UseKey = false;
            // 
            // btnMbOK
            // 
            this.btnMbOK.BackgroundDraw = true;
            this.btnMbOK.ButtonColor = null;
            this.btnMbOK.Clickable = true;
            this.btnMbOK.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnMbOK.Gradient = true;
            this.btnMbOK.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnMbOK.IconGap = 0;
            this.btnMbOK.IconImage = null;
            this.btnMbOK.IconSize = 12F;
            this.btnMbOK.IconString = null;
            this.btnMbOK.Location = new System.Drawing.Point(169, 49);
            this.btnMbOK.Name = "btnMbOK";
            this.btnMbOK.Round = null;
            this.btnMbOK.ShadowGap = 1;
            this.btnMbOK.Size = new System.Drawing.Size(150, 30);
            this.btnMbOK.TabIndex = 8;
            this.btnMbOK.Text = "OK";
            this.btnMbOK.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnMbOK.UseKey = false;
            // 
            // dvLabel2
            // 
            this.dvLabel2.BackgroundDraw = false;
            this.dvLabel2.BorderColor = null;
            this.dvLabel2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel2.IconGap = 0;
            this.dvLabel2.IconImage = null;
            this.dvLabel2.IconSize = 12F;
            this.dvLabel2.IconString = null;
            this.dvLabel2.LabelColor = null;
            this.dvLabel2.Location = new System.Drawing.Point(169, 13);
            this.dvLabel2.Name = "dvLabel2";
            this.dvLabel2.Round = null;
            this.dvLabel2.ShadowGap = 1;
            this.dvLabel2.Size = new System.Drawing.Size(150, 30);
            this.dvLabel2.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel2.TabIndex = 7;
            this.dvLabel2.TabStop = false;
            this.dvLabel2.Text = "메시지박스";
            this.dvLabel2.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel2.Unit = "";
            this.dvLabel2.UnitWidth = null;
            // 
            // lblKeyResult
            // 
            this.lblKeyResult.BackgroundDraw = true;
            this.lblKeyResult.BorderColor = null;
            this.lblKeyResult.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblKeyResult.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblKeyResult.IconGap = 0;
            this.lblKeyResult.IconImage = null;
            this.lblKeyResult.IconSize = 12F;
            this.lblKeyResult.IconString = null;
            this.lblKeyResult.LabelColor = null;
            this.lblKeyResult.Location = new System.Drawing.Point(13, 229);
            this.lblKeyResult.Name = "lblKeyResult";
            this.lblKeyResult.Round = null;
            this.lblKeyResult.ShadowGap = 1;
            this.lblKeyResult.Size = new System.Drawing.Size(150, 30);
            this.lblKeyResult.Style = Devinno.Forms.Embossing.FlatConcave;
            this.lblKeyResult.TabIndex = 6;
            this.lblKeyResult.TabStop = false;
            this.lblKeyResult.Text = null;
            this.lblKeyResult.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblKeyResult.Unit = "";
            this.lblKeyResult.UnitWidth = null;
            // 
            // btnKeyboardEng
            // 
            this.btnKeyboardEng.BackgroundDraw = true;
            this.btnKeyboardEng.ButtonColor = null;
            this.btnKeyboardEng.Clickable = true;
            this.btnKeyboardEng.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeyboardEng.Gradient = true;
            this.btnKeyboardEng.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeyboardEng.IconGap = 0;
            this.btnKeyboardEng.IconImage = null;
            this.btnKeyboardEng.IconSize = 12F;
            this.btnKeyboardEng.IconString = null;
            this.btnKeyboardEng.Location = new System.Drawing.Point(13, 193);
            this.btnKeyboardEng.Name = "btnKeyboardEng";
            this.btnKeyboardEng.Round = null;
            this.btnKeyboardEng.ShadowGap = 1;
            this.btnKeyboardEng.Size = new System.Drawing.Size(150, 30);
            this.btnKeyboardEng.TabIndex = 5;
            this.btnKeyboardEng.Text = "키보드 영문";
            this.btnKeyboardEng.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeyboardEng.UseKey = false;
            // 
            // btnKeyboardHan
            // 
            this.btnKeyboardHan.BackgroundDraw = true;
            this.btnKeyboardHan.ButtonColor = null;
            this.btnKeyboardHan.Clickable = true;
            this.btnKeyboardHan.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeyboardHan.Gradient = true;
            this.btnKeyboardHan.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeyboardHan.IconGap = 0;
            this.btnKeyboardHan.IconImage = null;
            this.btnKeyboardHan.IconSize = 12F;
            this.btnKeyboardHan.IconString = null;
            this.btnKeyboardHan.Location = new System.Drawing.Point(13, 157);
            this.btnKeyboardHan.Name = "btnKeyboardHan";
            this.btnKeyboardHan.Round = null;
            this.btnKeyboardHan.ShadowGap = 1;
            this.btnKeyboardHan.Size = new System.Drawing.Size(150, 30);
            this.btnKeyboardHan.TabIndex = 4;
            this.btnKeyboardHan.Text = "키보드 한글";
            this.btnKeyboardHan.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeyboardHan.UseKey = false;
            // 
            // btnKeypadPassword
            // 
            this.btnKeypadPassword.BackgroundDraw = true;
            this.btnKeypadPassword.ButtonColor = null;
            this.btnKeypadPassword.Clickable = true;
            this.btnKeypadPassword.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeypadPassword.Gradient = true;
            this.btnKeypadPassword.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeypadPassword.IconGap = 0;
            this.btnKeypadPassword.IconImage = null;
            this.btnKeypadPassword.IconSize = 12F;
            this.btnKeypadPassword.IconString = null;
            this.btnKeypadPassword.Location = new System.Drawing.Point(13, 121);
            this.btnKeypadPassword.Name = "btnKeypadPassword";
            this.btnKeypadPassword.Round = null;
            this.btnKeypadPassword.ShadowGap = 1;
            this.btnKeypadPassword.Size = new System.Drawing.Size(150, 30);
            this.btnKeypadPassword.TabIndex = 3;
            this.btnKeypadPassword.Text = "패스워드";
            this.btnKeypadPassword.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeypadPassword.UseKey = false;
            // 
            // btnKeypadDecimal
            // 
            this.btnKeypadDecimal.BackgroundDraw = true;
            this.btnKeypadDecimal.ButtonColor = null;
            this.btnKeypadDecimal.Clickable = true;
            this.btnKeypadDecimal.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeypadDecimal.Gradient = true;
            this.btnKeypadDecimal.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeypadDecimal.IconGap = 0;
            this.btnKeypadDecimal.IconImage = null;
            this.btnKeypadDecimal.IconSize = 12F;
            this.btnKeypadDecimal.IconString = null;
            this.btnKeypadDecimal.Location = new System.Drawing.Point(13, 85);
            this.btnKeypadDecimal.Name = "btnKeypadDecimal";
            this.btnKeypadDecimal.Round = null;
            this.btnKeypadDecimal.ShadowGap = 1;
            this.btnKeypadDecimal.Size = new System.Drawing.Size(150, 30);
            this.btnKeypadDecimal.TabIndex = 2;
            this.btnKeypadDecimal.Text = "키패드 실수";
            this.btnKeypadDecimal.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeypadDecimal.UseKey = false;
            // 
            // dvLabel1
            // 
            this.dvLabel1.BackgroundDraw = false;
            this.dvLabel1.BorderColor = null;
            this.dvLabel1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvLabel1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvLabel1.IconGap = 0;
            this.dvLabel1.IconImage = null;
            this.dvLabel1.IconSize = 12F;
            this.dvLabel1.IconString = null;
            this.dvLabel1.LabelColor = null;
            this.dvLabel1.Location = new System.Drawing.Point(13, 13);
            this.dvLabel1.Name = "dvLabel1";
            this.dvLabel1.Round = null;
            this.dvLabel1.ShadowGap = 1;
            this.dvLabel1.Size = new System.Drawing.Size(150, 30);
            this.dvLabel1.Style = Devinno.Forms.Embossing.FlatConcave;
            this.dvLabel1.TabIndex = 1;
            this.dvLabel1.TabStop = false;
            this.dvLabel1.Text = "키패드 / 키보드";
            this.dvLabel1.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvLabel1.Unit = "";
            this.dvLabel1.UnitWidth = null;
            // 
            // btnKeypadInt
            // 
            this.btnKeypadInt.BackgroundDraw = true;
            this.btnKeypadInt.ButtonColor = null;
            this.btnKeypadInt.Clickable = true;
            this.btnKeypadInt.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnKeypadInt.Gradient = true;
            this.btnKeypadInt.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnKeypadInt.IconGap = 0;
            this.btnKeypadInt.IconImage = null;
            this.btnKeypadInt.IconSize = 12F;
            this.btnKeypadInt.IconString = null;
            this.btnKeypadInt.Location = new System.Drawing.Point(13, 49);
            this.btnKeypadInt.Name = "btnKeypadInt";
            this.btnKeypadInt.Round = null;
            this.btnKeypadInt.ShadowGap = 1;
            this.btnKeypadInt.Size = new System.Drawing.Size(150, 30);
            this.btnKeypadInt.TabIndex = 0;
            this.btnKeypadInt.Text = "키패드 정수";
            this.btnKeypadInt.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnKeypadInt.UseKey = false;
            // 
            // tpTable
            // 
            this.tpTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tpTable.Controls.Add(this.dataGrid);
            this.tpTable.Controls.Add(this.btnTreeRemove);
            this.tpTable.Controls.Add(this.btnTreeAdd);
            this.tpTable.Controls.Add(this.treeView);
            this.tpTable.Controls.Add(this.lblToolDrag);
            this.tpTable.Controls.Add(this.toolBox);
            this.tpTable.Controls.Add(this.comboBox);
            this.tpTable.Controls.Add(this.listBox);
            this.tpTable.Location = new System.Drawing.Point(4, 5);
            this.tpTable.Name = "tpTable";
            this.tpTable.Padding = new System.Windows.Forms.Padding(10);
            this.tpTable.Size = new System.Drawing.Size(718, 690);
            this.tpTable.TabIndex = 7;
            this.tpTable.Text = "tabPage2";
            // 
            // dataGrid
            // 
            this.dataGrid.Bevel = true;
            this.dataGrid.BoxColor = null;
            this.dataGrid.ColumnColor = null;
            this.dataGrid.ColumnHeight = 30;
            this.dataGrid.HScrollPosition = 0D;
            this.dataGrid.InputColor = null;
            this.dataGrid.Location = new System.Drawing.Point(13, 318);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowColor = null;
            this.dataGrid.RowHeight = 30;
            this.dataGrid.ScrollMode = Devinno.Forms.Utils.ScrollMode.Vertical;
            this.dataGrid.SelectedRowColor = null;
            this.dataGrid.SelectionMode = Devinno.Forms.Controls.DvDataGridSelectionMode.Single;
            this.dataGrid.ShadowGap = 1;
            this.dataGrid.Size = new System.Drawing.Size(692, 383);
            this.dataGrid.SummaryRowColor = null;
            this.dataGrid.TabIndex = 7;
            this.dataGrid.Text = "dvDataGrid1";
            this.dataGrid.VScrollPosition = 0D;
            // 
            // btnTreeRemove
            // 
            this.btnTreeRemove.BackgroundDraw = true;
            this.btnTreeRemove.ButtonColor = null;
            this.btnTreeRemove.Clickable = true;
            this.btnTreeRemove.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnTreeRemove.Gradient = true;
            this.btnTreeRemove.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnTreeRemove.IconGap = 0;
            this.btnTreeRemove.IconImage = null;
            this.btnTreeRemove.IconSize = 12F;
            this.btnTreeRemove.IconString = "fa-minus";
            this.btnTreeRemove.Location = new System.Drawing.Point(471, 282);
            this.btnTreeRemove.Name = "btnTreeRemove";
            this.btnTreeRemove.Round = null;
            this.btnTreeRemove.ShadowGap = 1;
            this.btnTreeRemove.Size = new System.Drawing.Size(40, 30);
            this.btnTreeRemove.TabIndex = 6;
            this.btnTreeRemove.Text = null;
            this.btnTreeRemove.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnTreeRemove.UseKey = false;
            // 
            // btnTreeAdd
            // 
            this.btnTreeAdd.BackgroundDraw = true;
            this.btnTreeAdd.ButtonColor = null;
            this.btnTreeAdd.Clickable = true;
            this.btnTreeAdd.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.btnTreeAdd.Gradient = true;
            this.btnTreeAdd.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.btnTreeAdd.IconGap = 0;
            this.btnTreeAdd.IconImage = null;
            this.btnTreeAdd.IconSize = 12F;
            this.btnTreeAdd.IconString = "fa-plus";
            this.btnTreeAdd.Location = new System.Drawing.Point(425, 282);
            this.btnTreeAdd.Name = "btnTreeAdd";
            this.btnTreeAdd.Round = null;
            this.btnTreeAdd.ShadowGap = 1;
            this.btnTreeAdd.Size = new System.Drawing.Size(40, 30);
            this.btnTreeAdd.TabIndex = 5;
            this.btnTreeAdd.Text = null;
            this.btnTreeAdd.TextPadding = new System.Windows.Forms.Padding(0);
            this.btnTreeAdd.UseKey = false;
            // 
            // treeView
            // 
            this.treeView.BackgroundDraw = true;
            this.treeView.BoxColor = null;
            this.treeView.ItemHeight = 30;
            this.treeView.Location = new System.Drawing.Point(425, 13);
            this.treeView.Name = "treeView";
            this.treeView.RadioColor = null;
            this.treeView.RadioSize = 16;
            this.treeView.Round = null;
            this.treeView.SelectedColor = null;
            this.treeView.SelectionMode = Devinno.Forms.ItemSelectionMode.Single;
            this.treeView.ShadowGap = 1;
            this.treeView.Size = new System.Drawing.Size(280, 263);
            this.treeView.TabIndex = 4;
            this.treeView.TabStop = false;
            this.treeView.Text = "dvTreeView1";
            // 
            // lblToolDrag
            // 
            this.lblToolDrag.BackgroundDraw = true;
            this.lblToolDrag.BorderColor = null;
            this.lblToolDrag.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.lblToolDrag.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.lblToolDrag.IconGap = 0;
            this.lblToolDrag.IconImage = null;
            this.lblToolDrag.IconSize = 12F;
            this.lblToolDrag.IconString = null;
            this.lblToolDrag.LabelColor = null;
            this.lblToolDrag.Location = new System.Drawing.Point(219, 282);
            this.lblToolDrag.Name = "lblToolDrag";
            this.lblToolDrag.Round = null;
            this.lblToolDrag.ShadowGap = 1;
            this.lblToolDrag.Size = new System.Drawing.Size(200, 30);
            this.lblToolDrag.Style = Devinno.Forms.Embossing.FlatConcave;
            this.lblToolDrag.TabIndex = 3;
            this.lblToolDrag.TabStop = false;
            this.lblToolDrag.Text = null;
            this.lblToolDrag.TextPadding = new System.Windows.Forms.Padding(0);
            this.lblToolDrag.Unit = "";
            this.lblToolDrag.UnitWidth = null;
            // 
            // toolBox
            // 
            this.toolBox.BackgroundDraw = true;
            this.toolBox.BoxColor = null;
            this.toolBox.CategoryColor = null;
            this.toolBox.ItemHeight = 30;
            this.toolBox.Location = new System.Drawing.Point(219, 13);
            this.toolBox.Name = "toolBox";
            this.toolBox.RadioSize = 16;
            this.toolBox.Round = null;
            this.toolBox.ShadowGap = 1;
            this.toolBox.Size = new System.Drawing.Size(200, 263);
            this.toolBox.TabIndex = 2;
            this.toolBox.TabStop = false;
            this.toolBox.Text = "dvToolBox1";
            // 
            // comboBox
            // 
            this.comboBox.BoxColor = null;
            this.comboBox.ButtonWidth = 40;
            this.comboBox.ItemColor = null;
            this.comboBox.ItemHeight = 30;
            this.comboBox.Location = new System.Drawing.Point(13, 282);
            this.comboBox.MaximumViewCount = 8;
            this.comboBox.Name = "comboBox";
            this.comboBox.Round = null;
            this.comboBox.SelectedIndex = -1;
            this.comboBox.SelectedItemColor = null;
            this.comboBox.ShadowGap = 1;
            this.comboBox.Size = new System.Drawing.Size(200, 30);
            this.comboBox.TabIndex = 1;
            this.comboBox.Text = "dvComboBox1";
            // 
            // listBox
            // 
            this.listBox.BackgroundDraw = true;
            this.listBox.BoxColor = null;
            this.listBox.ItemAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.listBox.ItemHeight = 30;
            this.listBox.ItemPadding = new System.Windows.Forms.Padding(0);
            this.listBox.Location = new System.Drawing.Point(13, 13);
            this.listBox.Name = "listBox";
            this.listBox.Round = null;
            this.listBox.RowColor = null;
            this.listBox.SelectedColor = null;
            this.listBox.SelectionMode = Devinno.Forms.ItemSelectionMode.Single;
            this.listBox.ShadowGap = 1;
            this.listBox.Size = new System.Drawing.Size(200, 263);
            this.listBox.TabIndex = 0;
            this.listBox.Text = "dvListBox1";
            // 
            // dvButton2
            // 
            this.dvButton2.BackgroundDraw = true;
            this.dvButton2.ButtonColor = null;
            this.dvButton2.Clickable = true;
            this.dvButton2.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton2.Gradient = true;
            this.dvButton2.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvButton2.IconGap = 0;
            this.dvButton2.IconImage = null;
            this.dvButton2.IconSize = 12F;
            this.dvButton2.IconString = null;
            this.dvButton2.Location = new System.Drawing.Point(28, 95);
            this.dvButton2.Name = "dvButton2";
            this.dvButton2.Round = null;
            this.dvButton2.ShadowGap = 1;
            this.dvButton2.Size = new System.Drawing.Size(248, 173);
            this.dvButton2.TabIndex = 1;
            this.dvButton2.Text = "dvButton2";
            this.dvButton2.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton2.UseKey = false;
            // 
            // dvButton1
            // 
            this.dvButton1.BackgroundDraw = true;
            this.dvButton1.ButtonColor = null;
            this.dvButton1.Clickable = true;
            this.dvButton1.ContentAlignment = Devinno.Forms.DvContentAlignment.MiddleCenter;
            this.dvButton1.Gradient = true;
            this.dvButton1.IconAlignment = Devinno.Forms.DvTextIconAlignment.LeftRight;
            this.dvButton1.IconGap = 0;
            this.dvButton1.IconImage = null;
            this.dvButton1.IconSize = 12F;
            this.dvButton1.IconString = null;
            this.dvButton1.Location = new System.Drawing.Point(184, 32);
            this.dvButton1.Name = "dvButton1";
            this.dvButton1.Round = null;
            this.dvButton1.ShadowGap = 1;
            this.dvButton1.Size = new System.Drawing.Size(225, 230);
            this.dvButton1.TabIndex = 0;
            this.dvButton1.Text = "dvButton1";
            this.dvButton1.TextPadding = new System.Windows.Forms.Padding(0);
            this.dvButton1.UseKey = false;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(740, 770);
            this.Controls.Add(this.tab);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(740, 770);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(7, 40, 7, 7);
            this.Text = "DEVINNO";
            this.Title = "DEVINNO";
            this.TitleFont = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleIconBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.TitleIconImage = global::Sample.Properties.Resources.logo2;
            this.TitleIconSize = 14F;
            this.TitleIconString = "fa-image";
            this.tab.ResumeLayout(false);
            this.tpControl.ResumeLayout(false);
            this.tblTriButton.ResumeLayout(false);
            this.tpGauge.ResumeLayout(false);
            this.tblGauge1.ResumeLayout(false);
            this.dvContainer6.ResumeLayout(false);
            this.dvContainer4.ResumeLayout(false);
            this.dvContainer3.ResumeLayout(false);
            this.dvContainer2.ResumeLayout(false);
            this.dvContainer1.ResumeLayout(false);
            this.dvContainer5.ResumeLayout(false);
            this.tblGauge2.ResumeLayout(false);
            this.tpGraph.ResumeLayout(false);
            this.dvTableLayoutPanel1.ResumeLayout(false);
            this.tabGraph.ResumeLayout(false);
            this.tpBarGraphH.ResumeLayout(false);
            this.tpBarGraphV.ResumeLayout(false);
            this.tpCircleGraph.ResumeLayout(false);
            this.tpLineGraph.ResumeLayout(false);
            this.tpTrendGraph.ResumeLayout(false);
            this.tpTimeGraph.ResumeLayout(false);
            this.tpContainer.ResumeLayout(false);
            this.dvScrollablePanel1.ResumeLayout(false);
            this.dvTableLayoutPanel2.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.tpDialog.ResumeLayout(false);
            this.tpTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Devinno.Forms.Containers.DvTablessControl tab;
        private System.Windows.Forms.TabPage tpControl;
        private System.Windows.Forms.TabPage tpGauge;
        private System.Windows.Forms.TabPage tpGraph;
        private System.Windows.Forms.TabPage tpContainer;
        private System.Windows.Forms.TabPage tpDialog;
        private System.Windows.Forms.TabPage tpTable;
        private Devinno.Forms.Controls.DvButton btnIcon;
        private Devinno.Forms.Controls.DvButton btnGradient;
        private Devinno.Forms.Controls.DvButton btnFlat;
        private Devinno.Forms.Controls.DvLabel lblNone;
        private Devinno.Forms.Controls.DvLabel btnConvex;
        private Devinno.Forms.Controls.DvLabel lblConcave;
        private Devinno.Forms.Controls.DvLabel lblFlatConvex;
        private Devinno.Forms.Controls.DvLabel lblFlatConcave;
        private Devinno.Forms.Controls.DvLabel lblFlat;
        private Devinno.Forms.Controls.DvButton btnBlank;
        private Devinno.Forms.Controls.DvTextBox txtMultiLine;
        private Devinno.Forms.Controls.DvTextBox txtNumber;
        private Devinno.Forms.Controls.DvButton btnTextAlign;
        private Devinno.Forms.Controls.DvButtons btnsType;
        private Devinno.Forms.Controls.DvCheckBox chk1;
        private Devinno.Forms.Controls.DvCheckBox chk3;
        private Devinno.Forms.Controls.DvCheckBox chk2;
        private Devinno.Forms.Controls.DvRadioBox rad3;
        private Devinno.Forms.Controls.DvRadioBox rad2;
        private Devinno.Forms.Controls.DvRadioBox rad1;
        private Devinno.Forms.Controls.DvToggleButton btnToggle2;
        private Devinno.Forms.Controls.DvToggleButton btnToggle1;
        private Devinno.Forms.Controls.DvRadioButton btnRadio1;
        private Devinno.Forms.Controls.DvRadioButton btnRadio2;
        private Devinno.Forms.Controls.DvCircleButton btnCircleDown;
        private Devinno.Forms.Controls.DvCircleButton btnCircleUp;
        private Devinno.Forms.Controls.DvTriangleButton btnTriangleDown;
        private Devinno.Forms.Controls.DvTriangleButton btnTriableUp;
        private Devinno.Forms.Controls.DvTriangleButton btnTriangleRight;
        private Devinno.Forms.Controls.DvTriangleButton btnTriangleLeft;
        private Devinno.Forms.Containers.DvTableLayoutPanel tblTriButton;
        private Devinno.Forms.Controls.DvLampButton btnLampMotor;
        private Devinno.Forms.Controls.DvLampButton btnLampPump;
        private Devinno.Forms.Controls.DvLamp lmp1;
        private Devinno.Forms.Controls.DvLamp lmp3;
        private Devinno.Forms.Controls.DvLamp lmp2;
        private Devinno.Forms.Controls.DvOnOff onoff;
        private Devinno.Forms.Controls.DvSwitch sw;
        private Devinno.Forms.Controls.DvNumberBox nbLR;
        private Devinno.Forms.Controls.DvNumberBox nbRUD;
        private Devinno.Forms.Controls.DvNumberBox nbUD;
        private Devinno.Forms.Controls.DvNumberBox nbR;
        private Devinno.Forms.Controls.DvPictureBox pic;
        private Devinno.Forms.Controls.DvSelector sels;
        private Devinno.Forms.Controls.DvCalendar calendar;
        private Devinno.Forms.Controls.DvAnimate ani;
        private Devinno.Forms.Controls.DvValueLabelBoolean vlblOnOff;
        private Devinno.Forms.Controls.DvValueLabelText vlblName;
        private Devinno.Forms.Controls.DvValueLabelInt vlblPos;
        private Devinno.Forms.Controls.DvValueLabelFloat vlblTemp;
        private Devinno.Forms.Controls.DvValueInputText inName;
        private Devinno.Forms.Controls.DvValueInputInt inPos;
        private Devinno.Forms.Controls.DvValueInputFloat inTemp;
        private Devinno.Forms.Controls.DvValueInputBool inOnOff;
        private Devinno.Forms.Controls.DvProgressH pgsH_N;
        private Devinno.Forms.Controls.DvProgressV pgsV_N;
        private Devinno.Forms.Controls.DvProgressH pgsH_B;
        private Devinno.Forms.Controls.DvProgressH pgsH_G;
        private Devinno.Forms.Controls.DvProgressH pgsH_R;
        private Devinno.Forms.Controls.DvProgressV pgsV_B;
        private Devinno.Forms.Controls.DvProgressV pgsV_G;
        private Devinno.Forms.Controls.DvProgressV pgsV_R;
        private Devinno.Forms.Controls.DvSliderH sldH_N;
        private Devinno.Forms.Controls.DvSliderH sldH_G;
        private Devinno.Forms.Controls.DvSliderH sldH_R;
        private Devinno.Forms.Controls.DvSliderV sldV_N;
        private Devinno.Forms.Controls.DvSliderV sldV_G;
        private Devinno.Forms.Controls.DvSliderV sldV_R;
        private Devinno.Forms.Controls.DvSliderV sldV_B;
        private Devinno.Forms.Controls.DvSliderH sldH_B;
        private Devinno.Forms.Controls.DvGauge gauge;
        private Devinno.Forms.Controls.DvMeter meter;
        private Devinno.Forms.Controls.DvKnob knob;
        private Devinno.Forms.Controls.DvStepGauge sg;
        private Devinno.Forms.Controls.DvRangeSliderV rangeV_N;
        private Devinno.Forms.Controls.DvRangeSliderH rangeH_N;
        private Devinno.Forms.Containers.DvTableLayoutPanel tblGauge2;
        private Devinno.Forms.Containers.DvPanel panel;
        private Devinno.Forms.Containers.DvBorderPanel borderPanel;
        private Devinno.Forms.Containers.DvGroupBox grpBox;
        private Devinno.Forms.Containers.DvBoxPanel pnlBox;
        private Devinno.Forms.Controls.DvBarGraphH barGraphH;
        private Devinno.Forms.Containers.DvTableLayoutPanel dvTableLayoutPanel1;
        private Devinno.Forms.Containers.DvTablessControl tabGraph;
        private System.Windows.Forms.TabPage tpBarGraphH;
        private System.Windows.Forms.TabPage tpBarGraphV;
        private System.Windows.Forms.TabPage tpCircleGraph;
        private System.Windows.Forms.TabPage tpLineGraph;
        private System.Windows.Forms.TabPage tpTrendGraph;
        private System.Windows.Forms.TabPage tpTimeGraph;
        private Devinno.Forms.Controls.DvButtons menuGraph;
        private Devinno.Forms.Containers.DvTableLayoutPanel tblGauge1;
        private Devinno.Forms.Containers.DvContainer dvContainer4;
        private Devinno.Forms.Containers.DvContainer dvContainer3;
        private Devinno.Forms.Containers.DvContainer dvContainer2;
        private Devinno.Forms.Containers.DvContainer dvContainer1;
        private Devinno.Forms.Controls.DvBarGraphV barGraphV;
        private Devinno.Forms.Controls.DvLineGraph lineGraph;
        private Devinno.Forms.Controls.DvCircleGraph circleGraph;
        private Devinno.Forms.Controls.DvButton btnGraphRefresh;
        private Devinno.Forms.Containers.DvContainer dvContainer6;
        private Devinno.Forms.Controls.DvRangeSliderV rangeV_T;
        private Devinno.Forms.Controls.DvRangeSliderV rangeV_B;
        private Devinno.Forms.Controls.DvRangeSliderV rangeV_G;
        private Devinno.Forms.Controls.DvRangeSliderV rangeV_R;
        private Devinno.Forms.Controls.DvSliderV sldV_T;
        private Devinno.Forms.Controls.DvProgressV pgsV_T;
        private Devinno.Forms.Controls.DvSliderH sldH_T;
        private Devinno.Forms.Controls.DvProgressH pgsH_T;
        private Devinno.Forms.Containers.DvContainer dvContainer5;
        private Devinno.Forms.Controls.DvRangeSliderH rangeH_T;
        private Devinno.Forms.Controls.DvRangeSliderH rangeH_B;
        private Devinno.Forms.Controls.DvRangeSliderH rangeH_G;
        private Devinno.Forms.Controls.DvRangeSliderH rangeH_R;
        private Devinno.Forms.Controls.DvTimeGraph timeGraph;
        private Devinno.Forms.Controls.DvTrendGraph trendGraph;
        private Devinno.Forms.Controls.DvButton btnPause;
        private Devinno.Forms.Controls.DvListBox dvListBox1;
        private Devinno.Forms.Controls.DvListBox listBox;
        private Devinno.Forms.Controls.DvComboBox comboBox;
        private Devinno.Forms.Controls.DvButton dvButton1;
        private Devinno.Forms.Controls.DvButton dvButton2;
        private Devinno.Forms.Controls.DvButton btnKeypadInt;
        private Devinno.Forms.Controls.DvButton btnKeypadDecimal;
        private Devinno.Forms.Controls.DvLabel dvLabel1;
        private Devinno.Forms.Controls.DvButton btnKeypadPassword;
        private Devinno.Forms.Containers.DvScrollablePanel dvScrollablePanel1;
        private Devinno.Forms.Containers.DvTableLayoutPanel dvTableLayoutPanel2;
        private Devinno.Forms.Controls.DvButton btnKeyboardEng;
        private Devinno.Forms.Controls.DvButton btnKeyboardHan;
        private Devinno.Forms.Controls.DvLabel lblKeyResult;
        private Devinno.Forms.Controls.DvLabel lblMb;
        private Devinno.Forms.Controls.DvButton btnMbYesNoCancel;
        private Devinno.Forms.Controls.DvButton btnMbOkCancel;
        private Devinno.Forms.Controls.DvButton btnMbYesNo;
        private Devinno.Forms.Controls.DvButton btnMbOK;
        private Devinno.Forms.Controls.DvLabel dvLabel2;
        private Devinno.Forms.Controls.DvColorPicker colorPicker;
        private Devinno.Forms.Controls.DvLabel dvLabel3;
        private Devinno.Forms.Controls.DvDateTimePicker datePicker;
        private Devinno.Forms.Controls.DvValueInputWheel inDoW;
        private Devinno.Forms.Controls.DvLabel dvLabel4;
        private Devinno.Forms.Controls.DvButton btnInputClass;
        private Devinno.Forms.Controls.DvButton btnInputInt;
        private Devinno.Forms.Controls.DvButton btnInputFloat;
        private Devinno.Forms.Controls.DvButton btnInputString;
        private Devinno.Forms.Controls.DvButton btnSelCombo;
        private Devinno.Forms.Controls.DvButton btnSelRadio;
        private Devinno.Forms.Controls.DvButton btnSelSelector;
        private Devinno.Forms.Controls.DvButton btnSelWheel;
        private Devinno.Forms.Controls.DvLabel dvLabel5;
        private Devinno.Forms.Controls.DvButton btnSelCheck;
        private Devinno.Forms.Controls.DvValueInputCombo inCombo;
        private Devinno.Forms.Controls.DvButton btnPortSettingSimple;
        private Devinno.Forms.Controls.DvButton btnPortSetting;
        private Devinno.Forms.Controls.DvLabel dvLabel6;
        private Devinno.Forms.Containers.DvTabControl tab2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private Devinno.Forms.Controls.DvToolBox toolBox;
        private Devinno.Forms.Controls.DvLabel lblToolDrag;
        private Devinno.Forms.Controls.DvTreeView treeView;
        private Devinno.Forms.Controls.DvButton btnTreeRemove;
        private Devinno.Forms.Controls.DvButton btnTreeAdd;
        private Devinno.Forms.Controls.DvDataGrid dataGrid;
    }
}
