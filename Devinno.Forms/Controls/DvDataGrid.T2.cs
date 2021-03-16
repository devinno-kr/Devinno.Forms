using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    #region class : DvDataGridTool
    internal class DvDataGridTool
    {
        #region Rect
        public static Rectangle RTI(RectangleF rt) => new Rectangle(Convert.ToInt32(rt.X), Convert.ToInt32(rt.Y), Convert.ToInt32(rt.Width), Convert.ToInt32(rt.Height));
        public static RectangleF RTF(RectangleF rt) => new RectangleF(Convert.ToSingle(rt.X), Convert.ToSingle(rt.Y), Convert.ToSingle(rt.Width), Convert.ToSingle(rt.Height));
        #endregion
        #region GetText
        public static string GetText(object Value)
        {
            var s = (string)null;
            if (Value is string) s = (string)Value;
            else if (Value != null) s = Value.ToString();
            return s;
        }
        #endregion
    }
    #endregion

    #region class : DvDataGridSummaryLabelCell
    public class DvDataGridSummaryLabelCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Text { get; set; }
        public Color CellTextColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryLabelCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var rt = DvDataGridTool.RTI(CellBounds);
            var s = !string.IsNullOrWhiteSpace(Text) ? Text : "";
            if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            else Theme.DrawText(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridSummarySumCell
    public class DvDataGridSummarySumCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Format { get; set; }
        public Color CellTextColor { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummarySumCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var rt = DvDataGridTool.RTI(CellBounds);
            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            else Theme.DrawText(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region Calc
        public override void Calc()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Sum();
            base.Calc();
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridSummaryAverageCell
    public class DvDataGridSummaryAverageCell : DvDataGridSummaryCell
    {
        #region Properties
        public string Format { get; set; }
        public Color CellTextColor { get; set; }
        public decimal Value { get; private set; }
        #endregion
        #region Constructor
        public DvDataGridSummaryAverageCell(DvDataGrid Grid, DvDataGridSummaryRow Row) : base(Grid, Row)
        {
            this.CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Virtual Method
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var rt = DvDataGridTool.RTI(CellBounds);
            var s = string.IsNullOrWhiteSpace(Format) ? Value.ToString() : Value.ToString(Format);
            Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, CellBackColor, rt);
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region Calc
        public override void Calc()
        {
            Value = Grid.GetRows().Select(x => Convert.ToDecimal(x.Cells[ColumnIndex].Value)).Average();
            base.Calc();
        }
        #endregion
        #endregion
    }
    #endregion

    #region class : DvDataGridTextFormatColumn
    public class DvDataGridTextFormatColumn : DvDataGridColumn
    {
        #region Properties
        public string Format { get; set; }
        #endregion
        #region Constructor
        public DvDataGridTextFormatColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion
        #region Method
        #region GetText
        public string GetText(object Value)
        {
            var s = (string)null;
            if (string.IsNullOrWhiteSpace(Format)) s = (Value != null) ? Value.ToString() : null;
            else
            {
                if (Value is string) s = (string)Value;
                else if (Value is byte) s = (!string.IsNullOrWhiteSpace(Format) ? ((byte)Value).ToString(Format) : ((byte)Value).ToString());
                else if (Value is short) s = (!string.IsNullOrWhiteSpace(Format) ? ((short)Value).ToString(Format) : ((short)Value).ToString());
                else if (Value is ushort) s = (!string.IsNullOrWhiteSpace(Format) ? ((ushort)Value).ToString(Format) : ((ushort)Value).ToString());
                else if (Value is int) s = (!string.IsNullOrWhiteSpace(Format) ? ((int)Value).ToString(Format) : ((int)Value).ToString());
                else if (Value is uint) s = (!string.IsNullOrWhiteSpace(Format) ? ((uint)Value).ToString(Format) : ((uint)Value).ToString());
                else if (Value is long) s = (!string.IsNullOrWhiteSpace(Format) ? ((long)Value).ToString(Format) : ((long)Value).ToString());
                else if (Value is ulong) s = (!string.IsNullOrWhiteSpace(Format) ? ((ulong)Value).ToString(Format) : ((ulong)Value).ToString());
                else if (Value is float) s = (!string.IsNullOrWhiteSpace(Format) ? ((float)Value).ToString(Format) : ((float)Value).ToString());
                else if (Value is double) s = (!string.IsNullOrWhiteSpace(Format) ? ((double)Value).ToString(Format) : ((double)Value).ToString());
                else if (Value is decimal) s = (!string.IsNullOrWhiteSpace(Format) ? ((decimal)Value).ToString(Format) : ((decimal)Value).ToString());
                else if (Value is DateTime) s = (!string.IsNullOrWhiteSpace(Format) ? ((DateTime)Value).ToString(Format) : ((DateTime)Value).ToString());
                else if (Value is Enum) s = Value.ToString();
            }
            return s;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridTextConverterColumn
    public class DvDataGridTextConverterColumn : DvDataGridColumn
    {
        #region Properties
        public Func<object, string> Converter { get; set; }
        #endregion
        #region Constructor
        public DvDataGridTextConverterColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLabelCell);
        }
        #endregion
        #region Method
        #region GetText
        public string GetText(object Value)
        {
            var s = (string)null;
            if (Converter != null) s = Converter(Value);
            else if (Value != null) s = Value.ToString();
            return s;
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridLampColumn
    public class DvDataGridLampColumn : DvDataGridColumn
    {
        #region Properties
        public Color OnColor { get; set; } = DvTheme.DefaultTheme.PointColor;
        public Color OffColor { get; set; } = DvTheme.DefaultTheme.Color3;
        public bool Simple { get; set; } = false;
        #endregion
        #region Constructor
        public DvDataGridLampColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridLampCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridButtonColumn
    public class DvDataGridButtonColumn : DvDataGridColumn
    {
        #region Properties
        public string ButtonText { get; set; }

        #region Icon
        private DvIcon ico = new DvIcon();
        internal DvIcon Icon => ico;
        public Bitmap IconImage
        {
            get => ico.IconImage;
            set { if (ico.IconImage != value) { ico.IconImage = value; } }
        }
        public string IconString
        {
            get => ico.IconString;
            set { if (ico.IconString != value) { ico.IconString = value; } }
        }
        public int IconGap
        {
            get => ico.Gap;
            set { if (ico.Gap != value) { ico.Gap = value; } }
        }
        public DvTextIconAlignment IconAlignment
        {
            get => ico.Alignment;
            set { if (ico.Alignment != value) { ico.Alignment = value; } }
        }
        public float IconSize
        {
            get => ico.IconSize;
            set { if (ico.IconSize != value) { ico.IconSize = value; } }
        }
        #endregion
        #endregion
        #region Constructor
        public DvDataGridButtonColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridButtonCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridImageColumn
    public class DvDataGridImageColumn : DvDataGridColumn
    {
        #region Properties
        public PictureScaleMode ImageSizeMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridImageColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            ImageSizeMode = PictureScaleMode.Strech;
            CellType = typeof(DvDataGridImageCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridCheckBoxColumn
    public class DvDataGridCheckBoxColumn : DvDataGridColumn
    {
        #region Properties
        public Color BoxColor { get; set; }
        public Color CheckColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridCheckBoxColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            var Theme = dataGrid.GetTheme();
            BoxColor = dataGrid.GetRowColor(Theme).BrightnessTransmit(DvDataGrid.BoxBright);
            CheckColor = dataGrid.ForeColor;
            CellType = typeof(DvDataGridCheckBoxCell);
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridComboBoxColumn
    #region class : DvDataGridComboBoxItem
    public class DvDataGridComboBoxItem : ListBoxItem
    {
        public object Source { get; set; }

        public DvDataGridComboBoxItem(string Text) : base(Text) { }
        public DvDataGridComboBoxItem(string Text, Bitmap Image) : base(Text, Image) { }
        public DvDataGridComboBoxItem(string Text, string IconString, float Size) : base(Text, IconString, Size) { }
        public DvDataGridComboBoxItem(string Text, string IconString, float size, int Gap) : base(Text, IconString, size, Gap) { }
    }
    #endregion

    public class DvDataGridComboBoxColumn : DvDataGridColumn
    {
        #region Properties
        public List<DvDataGridComboBoxItem> Items { get; set; }
        #endregion
        #region Constructor
        public DvDataGridComboBoxColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            Items = new List<DvDataGridComboBoxItem>();
            CellType = typeof(DvDataGridComboBoxCell);
        }
        #endregion
    }
    #endregion
    
    #region class : DvDataGridDateTimePickerColumn
    public class DvDataGridDateTimePickerColumn : DvDataGridColumn
    {
        #region Properties
        public string Format { get; set; }
        public DvDateTimePickerStyle PickerMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridDateTimePickerColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditDateTimeCell);
            PickerMode = DvDateTimePickerStyle.DateTime;
        }
        #endregion
    }
    #endregion
    #region class : DvDataGridColorPickerColumn
    public class DvDataGridColorPickerColumn : DvDataGridColumn
    {
        #region Properties
        #endregion
        #region Constructor
        public DvDataGridColorPickerColumn(DvDataGrid dataGrid) : base(dataGrid)
        {
            CellType = typeof(DvDataGridEditColorCell);
        }
        #endregion
    }
    #endregion
   
    #region class : DvDataGridLabelCell
    public class DvDataGridLabelCell : DvDataGridCell
    {
        #region Constructor
        public DvDataGridLabelCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion

        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var s = "";
            
            if (Column is DvDataGridTextFormatColumn) s = ((DvDataGridTextFormatColumn)Column).GetText(Value);
            else if (Column is DvDataGridTextConverterColumn) s = ((DvDataGridTextConverterColumn)Column).GetText(Value);
            else s = DvDataGridTool.GetText(Value);

            if (!string.IsNullOrWhiteSpace(s))
            {
                var rt = DvDataGridTool.RTI(CellBounds); 
                var c = CellTextColor;
                var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
            }
         
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridLampCell
    public class DvDataGridLampCell : DvDataGridCell
    {
        #region Properties
        public Color OnColor { get; set; }
        public Color OffColor { get; set; }
        #endregion

        #region Constructor
        public DvDataGridLampCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            CellTextColor = Grid.ForeColor;

            if (Column is DvDataGridLampColumn)
            {
                var c = Column as DvDataGridLampColumn;
                this.OnColor = c.OnColor;
                this.OffColor = c.OffColor;
            }
        }
        #endregion

        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            #region Init
            var br = new SolidBrush(Color.Black);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            #region Set
            var oldsm = g.SmoothingMode;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Var
            var Simple = Column is DvDataGridLampColumn ? ((DvDataGridLampColumn)Column).Simple : false; 
            var f = Grid.DpiRatio;
            var rt = DvDataGridTool.RTI(CellBounds);
            
            var LampBackColor = CellBackColor.BrightnessTransmit(-0.5);
            var OnLampColor = OnColor;
            var OffLampColor = OffColor;
            var OnOff = Value is bool ? (bool)Value : false;
            var LampLightBright = 0.5;
            var LampDarkBright = -0.5;
            #endregion

            if (Simple)
            {
                Theme.DrawBox(g, OnOff ? OnLampColor : OffLampColor, LampBackColor, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_LT | BoxDrawOption.IN_BEVEL_LT2);
            }
            else
            {
                #region Lamp
                if (OnOff)
                {
                    var c = OnLampColor;
                    var AON = Convert.ToInt32(OnLampColor.ToHSV().V * 180);
                    var AOFF = AON / 3;
                    var BA = AON;

                    var old = g.ClipBounds;
                    g.SetClip(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 1, rt.Height - 1), CombineMode.Intersect);

                    #region Fill
                    using (var pth = new GraphicsPath())
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(rt.Width / 3, rt.Height / 3);
                        pth.AddEllipse(rtv);
                        using (var pbr = new PathGradientBrush(pth))
                        {
                            pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rt.Left, rt.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rt.Top, rt.Bottom)));
                            pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                            pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright) };
                            g.FillEllipse(pbr, rtv);
                        }
                    }
                    #endregion
                    #region Gradient
                    using (var lgbr = new LinearGradientBrush(rt, Color.FromArgb(Convert.ToByte(MathTool.Constrain(Theme.BevelAlpha * 1.5, 0, 255)), Color.White), Color.FromArgb(0, Color.White), 90))
                    {
                        g.FillRectangle(lgbr, rt);
                    }
                    #endregion
                    #region InBevel
                    {
                        var rtv = new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 1, rt.Height - 1);
                        var rtex = new Rectangle(rtv.X, rtv.Y, rtv.Width - 1, rtv.Height - 1);
                        if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                        {
                            var vv = Convert.ToByte(MathTool.Constrain(c.GetBrightness() * 255, 0, 255));
                            var c1 = Color.FromArgb(vv, Color.White);
                            var c2 = Color.Transparent;

                            using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                            {
                                using (var p2 = new Pen(lgbr, 1F))
                                {
                                    g.DrawRectangle(p2, rtv);
                                }
                            }
                          
                        }
                    }
                    #endregion

                    g.ResetClip();
                    g.SetClip(old);
                }
                else
                {
                    var c = OffLampColor;
                    var AON = Convert.ToInt32(OnLampColor.ToHSV().V * 180);
                    var AOFF = AON / 3;
                    var BA = AOFF;

                    var old = g.ClipBounds;
                    g.SetClip(new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 1, rt.Height - 1), CombineMode.Intersect);

                    #region Fill
                    using (var pth = new GraphicsPath())
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(rt.Width / 3, rt.Height / 3);
                        pth.AddEllipse(rtv);
                        using (var pbr = new PathGradientBrush(pth))
                        {
                            pbr.CenterPoint = new Point(Convert.ToInt32(MathTool.Map(0.25, 0, 1, rt.Left, rt.Right)), Convert.ToInt32(MathTool.Map(0.25, 0, 1, rt.Top, rt.Bottom)));
                            pbr.CenterColor = c.BrightnessTransmit(LampLightBright);
                            pbr.SurroundColors = new Color[] { c.BrightnessTransmit(LampDarkBright / 2.0) };
                            g.FillEllipse(pbr, rtv);
                        }
                    }
                    #endregion
                    #region Gardient
                    using (var lgbr = new LinearGradientBrush(rt, Color.FromArgb(Convert.ToByte(MathTool.Constrain(Theme.BevelAlpha / 1.5, 0, 255)), Color.White), Color.FromArgb(0, Color.White), 90))
                    {
                        g.FillRectangle(lgbr, rt);
                    }
                    #endregion
                    #region InBevel
                    {
                        var rtv = new Rectangle(rt.X + 1, rt.Y + 1, rt.Width - 1, rt.Height - 1);
                        var rtex = new Rectangle(rtv.X, rtv.Y, rtv.Width - 1, rtv.Height - 1);
                        if (rtex.X > 0 && rtex.Y > 0 && rtex.Width > 0 && rtex.Height > 0)
                        {
                            var vv = Convert.ToByte(MathTool.Constrain(c.GetBrightness() * 255D / 1.5, 0, 255));
                            var c1 = Color.FromArgb(vv, Color.White);
                            var c2 = Color.Transparent;

                            using (var lgbr = new LinearGradientBrush(rtex, c1, c2, 75))
                            {
                                using (var p2 = new Pen(lgbr, 1F))
                                {
                                    g.DrawRectangle(p2, rtv);
                                }
                            }
                        }
                    }
                    #endregion

                    g.ResetClip();
                    g.SetClip(old);
                }
                #endregion
            }

            #region Reset
            g.SmoothingMode = oldsm;
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridButtonCell
    public class DvDataGridButtonCell : DvDataGridCell
    {
        #region Properties
        #endregion
        #region Member Variable
        private bool bDown = false;
        #endregion
        #region Constructor
        public DvDataGridButtonCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            CellTextColor = Grid.ForeColor;
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            #region Init
            var br = new SolidBrush(Color.Black);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            var c = Row.Selected ? SelectedCellBackColor : CellBackColor;
            var cU = c;
            var cD = c.BrightnessTransmit(Theme.DownBright);

            var rt = new Rectangle((int)CellBounds.X, (int)CellBounds.Y, (int)CellBounds.Width, (int)CellBounds.Height); rt.Inflate(-0, -0);
            if (!bDown) Theme.DrawBox(g, cU, c, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_V | BoxDrawOption.IN_BEVEL);
            else Theme.DrawBox(g, cD, c, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.GRADIENT_V_REVERSE | BoxDrawOption.IN_SHADOW);

            if (Column is DvDataGridButtonColumn)
            {
                var col = Column as DvDataGridButtonColumn;
                if (bDown) rt.Offset(0, 1);
                Theme.DrawTextShadow(g, col.Icon, col.ButtonText, Grid.Font, CellTextColor, c, rt);
            }
            else
            {
                if (Value != null && Value is string)
                {
                    if (bDown) rt.Offset(0, 1);
                    var s = (string)Value;
                    Theme.DrawTextShadow(g, null, s, Grid.Font, CellTextColor, c, rt);
                }
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseDown
        public override void CellMouseDown(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                bDown = true;
                Grid.InvalidateTH();
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (bDown)
            {
                bDown = false;
                Grid.InvalidateTH();
                if (CollisionTool.Check(CellBounds, x, y)) Grid.InvokeCellButtonClick(this);
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridImageCell
    public class DvDataGridImageCell : DvDataGridCell
    {
        #region Properties
        public PictureScaleMode ImageSizeMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridImageCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridImageColumn)
            {
                this.ImageSizeMode = ((DvDataGridImageColumn)Column).ImageSizeMode;
            }
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            #region Init
            var br = new SolidBrush(Color.Black);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            if (Value is Bitmap)
            {
                var rtv = DvDataGridTool.RTI(new Rectangle(CellBounds.X+1, CellBounds.Y+1, CellBounds.Width, CellBounds.Height));
                var old = g.ClipBounds;
                g.SetClip(rtv, CombineMode.Intersect);

                var Image = (Bitmap)Value;
                var rtContent = new Rectangle((int)CellBounds.X, (int)CellBounds.Y, (int)CellBounds.Width, (int)CellBounds.Height);
                int cx = rtContent.X + (rtContent.Width / 2);
                int cy = rtContent.Y + (rtContent.Height / 2);
                switch (ImageSizeMode)
                {
                    case PictureScaleMode.Real:
                        g.DrawImage(Image, new Rectangle(rtContent.X, rtContent.Y, Image.Width, Image.Height));
                        break;
                    case PictureScaleMode.CenterImage:
                        g.DrawImage(Image, new Rectangle(cx - (Image.Width / 2), cy - (Image.Height / 2), Image.Width, Image.Height));
                        break;
                    case PictureScaleMode.Strech:
                        g.DrawImage(Image, rtContent);
                        break;
                    case PictureScaleMode.Zoom:
                        double imgratio = 1D;
                        if ((Image.Width - rtContent.Width) > (Image.Height - rtContent.Height)) imgratio = (double)rtContent.Width / (double)Image.Width;
                        else imgratio = (double)rtContent.Height / (double)Image.Height;

                        int szw = Convert.ToInt32((double)Image.Width * imgratio);
                        int szh = Convert.ToInt32((double)Image.Height * imgratio);

                        g.DrawImage(Image, new Rectangle(rtContent.X + (rtContent.Width / 2) - (szw / 2), rtContent.Y + (rtContent.Height / 2) - (szh / 2), szw, szh));
                        break;
                }

                g.ResetClip();
                g.SetClip(old);
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridCheckBoxCell
    public class DvDataGridCheckBoxCell : DvDataGridCell
    {
        #region Properties
        public Color CheckBoxColor { get; set; }
        public Color CheckColor { get; set; }
        #endregion
        #region Constructor
        public DvDataGridCheckBoxCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridCheckBoxColumn)
            {
                CheckBoxColor = ((DvDataGridCheckBoxColumn)Column).BoxColor;
                CheckColor = ((DvDataGridCheckBoxColumn)Column).CheckColor;
            }
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            if (Grid != null)
            {
                var oldsm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;

                #region Var
                var f = Grid.DpiRatio;
                var sbw = Convert.ToInt32(f * DvDataGrid.SELECTOR_BOX_WIDTH);
                var BoxBright = DvDataGrid.BoxBright;
                var RowColor = Grid.GetRowColor(Theme);
                var SelectedRowColor = Grid.GetSelectedRowColor(Theme);
                #endregion
                #region Init
                var br = new SolidBrush(Color.Black);
                var p = new Pen(Color.Black);
                #endregion
                #region Draw
                var Value = this.Value is bool ? (bool)this.Value : false;
                {
                    var Checked = Value;
                    var c = Row.Selected ? SelectedRowColor : RowColor;
                    var rtSelectorBox = MathTool.MakeRectangle(DvDataGridTool.RTI(CellBounds), new Size(sbw, sbw));
                    Theme.DrawBox(g, RowColor.BrightnessTransmit(BoxBright), c, rtSelectorBox, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);

                    if (Checked)
                    {
                        Rectangle rtCheck = new Rectangle(rtSelectorBox.X, rtSelectorBox.Y - 0, rtSelectorBox.Width, rtSelectorBox.Height); rtCheck.Inflate(-Convert.ToInt32(4 * f), -Convert.ToInt32(4 * f));
                        Rectangle rtCheckSH = new Rectangle(rtCheck.X, rtCheck.Y + 1, rtCheck.Width, rtCheck.Height);

                        p.Width = Convert.ToInt32(3 * f);
                        p.Color = CheckColor;
                        g.DrawLine(p, rtCheck.X, rtCheck.Y + rtCheck.Height / 2, rtCheck.X + rtCheck.Width / 2, rtCheck.Y + rtCheck.Height);
                        g.DrawLine(p, rtCheck.X + rtCheck.Width / 2 - 1, rtCheck.Y + rtCheck.Height, rtCheck.X + rtCheck.Width, rtCheck.Y);
                        p.Width = 1;
                    }
                }
                #endregion
                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion

                g.SmoothingMode = oldsm;
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseDown
        public override void CellMouseDown(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var Value = this.Value is bool ? (bool)this.Value : false;
                var v = !((bool)Value);
                if (v != (bool)Value)
                {
                    #region Value Set
                    var old = Value;
                    this.Value = v;
                    Grid.InvokeValueChanged(this, old, v);
                    #endregion
                }
            }
            base.CellMouseDown(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridComboBoxCell
    public class DvDataGridComboBoxCell : DvDataGridCell
    {
        #region Properties
        public override object Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                var old = base.Value;
                if (!object.Equals(old, value))
                {
                    base.Value = value;
                    Grid.InvokeValueChanged(this, old, value);
                }
            }
        }
        #endregion

        #region Properties : Local
        #region MaximumViewCount
        int nMaximumViewCount = 10;
        public int MaximumViewCount
        {
            get { return nMaximumViewCount; }
            set { nMaximumViewCount = value; }
        }
        #endregion
        #region ItemHeight
        private int nItemHeight = 30;
        public int ItemHeight
        {
            get => nItemHeight;
            set
            {
                if (nItemHeight != value)
                {
                    nItemHeight = value;
                    Grid?.InvalidateTH();
                }
            }
        }
        #endregion
        #region Items
        public List<DvDataGridComboBoxItem> Items { get; } = new List<DvDataGridComboBoxItem>();
        #endregion
        #region ItemPadding
        private Padding padItem = new Padding(0, 0, 0, 0);
        public Padding ItemPadding
        {
            get => padItem;
            set
            {
                if (padItem != value)
                {
                    padItem = value;
                    Grid?.InvalidateTH();
                }
            }
        }
        #endregion
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set
            {
                if (eContentAlignment != value)
                {
                    eContentAlignment = value; 
                    Grid?.InvalidateTH();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvDataGridComboBoxCell(DvDataGrid Grid, DvDataGridRow Row, IDvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridComboBoxColumn)
            {
                Items.AddRange(((DvDataGridComboBoxColumn)Column).Items);
                ItemHeight = Grid.RowHeight;
            }
        }
        #endregion

        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            #region Init
            var br = new SolidBrush(Color.Black);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            {
                var rtContent = DvDataGridTool.RTI(CellBounds); rtContent.Inflate(-1, 0);
                var BoxColor = CellBackColor;
                var ForeColor = CellTextColor;
                var SelectedColor = Grid.GetSelectedRowColor(Theme);
                var c = (Row.Selected ? SelectedColor : CellBackColor);
                var ButtonWidth = 60;
                var rtIco = new Rectangle(rtContent.Right - ButtonWidth, rtContent.Y, ButtonWidth, rtContent.Height);
                var rtBox = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width - rtIco.Width, rtContent.Height);
                var rtText = new Rectangle(rtBox.X + ItemPadding.Left, rtBox.Y + ItemPadding.Top, rtBox.Width - (ItemPadding.Left + ItemPadding.Right), rtBox.Height - (ItemPadding.Top + ItemPadding.Bottom));

                p.Color = CellBackColor.BrightnessTransmit(Theme.BorderBright);
                g.DrawLine(p, rtContent.Left, rtContent.Top, rtContent.Left, rtContent.Bottom);
                g.DrawLine(p, rtContent.Right, rtContent.Top, rtContent.Right, rtContent.Bottom);

                p.Color = c.BrightnessTransmit(DvDataGrid.ColumnBevelBright);
                g.DrawLine(p, rtContent.Left + 1, rtContent.Top + 1, rtContent.Left + 1, rtContent.Bottom - 1);

                #region Text
                var ls = Items.Where(x => x.Source != null && x.Source.Equals(Value));
                if (ls.Count() > 0)
                {
                    var v = ls.FirstOrDefault();
                    if (v != null)
                    {
                        Theme.DrawTextShadow(g, v.Icon, v.Text, Grid.Font, ForeColor, c, rtText, ContentAlignment);
                    }
                }
                #endregion
                #region Seperate
                var szh = Convert.ToInt32(rtIco.Height / 2);

                p.Width = 1;

                p.Color = c.BrightnessTransmit(Theme.OutBevelBright);
                g.DrawLine(p, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

                p.Color = c.BrightnessTransmit(Theme.BorderBright);
                g.DrawLine(p, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
                #endregion
                #region Icon
                var nisz = rtIco.Height / 4;
                if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
                {
                    Theme.DrawTextShadow(g, new DvIcon("fa-chevron-up") { IconSize = nisz, Gap = 0 }, "", Grid.Font, ForeColor, BoxColor, rtIco);
                }
                else
                {
                    Theme.DrawTextShadow(g, new DvIcon("fa-chevron-down") { IconSize = nisz, Gap = 0 }, "", Grid.Font, ForeColor, BoxColor, rtIco);
                }
                #endregion
            }
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                if (Items != null && Items.Count > 0) OpenDropDown(CellBounds);
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion

        #region DropDown
        #region Member Variable
        private bool closedWhileInControl;
        private DropDownContainer dropContainer;
        #endregion

        #region Properties
        #region CanDrop
        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }
        #endregion
        #region DropState
        public DvDropState DropState { get; private set; }
        #endregion
        #endregion

        #region Method
        #region FreezeDropDown
        internal void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = true;
                if (!remainVisible)
                    dropContainer.Visible = false;
            }
        }
        #endregion
        #region UnFreezeDropDown
        internal void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = false;
                if (!dropContainer.Visible)
                    dropContainer.Visible = true;
            }
        }
        #endregion
        #region OpenDropDown
        private void OpenDropDown(Rectangle rt)
        {
            //Grid.Move += (o, s) => { if (dropContainer != null) dropContainer.Bounds = GetDropDownBounds(); };

            var vls = Items.Select(x => x.Source).ToList();
            var SelectedIndex = vls.IndexOf(Value);
            var vpos = SelectedIndex == -1 ? 0 : SelectedIndex * ItemHeight;
            vpos = (int)MathTool.Constrain(vpos - (ItemHeight * 2), 0, (Items.Count * ItemHeight));

            dropContainer = new DropDownContainer(this);
            dropContainer.Bounds = GetDropDownBounds(rt);
            dropContainer.DropStateChanged += (o, s) => { DropState = s.DropState; };
            dropContainer.FormClosed += (o, s) =>
            {
                if (!dropContainer.IsDisposed) dropContainer.Dispose();
                dropContainer = null;
                closedWhileInControl = (Grid.RectangleToScreen(Grid.ClientRectangle).Contains(Cursor.Position));
                DropState = DvDropState.Closed;
                Grid.InvalidateTH();
            };
            DropState = DvDropState.Dropping;
            dropContainer.VScrollPosition = vpos;
            dropContainer.Show(Grid);
            DropState = DvDropState.Dropped;
            Grid.InvalidateTH();
        }
        #endregion
        #region GetDropDownBounds
        private Rectangle GetDropDownBounds(Rectangle rt)
        {
            int n = Items.Count;
            Point ptu = Grid.PointToScreen(new Point(rt.Left, rt.Top));
            Point pt = Grid.PointToScreen(new Point(rt.Left, rt.Bottom));
            Point ptEnd = Grid.Parent.PointToScreen(new Point(0, Grid.Bounds.Bottom));

            if (pt.Y > ptEnd.Y - 2) pt.Y = ptEnd.Y - 2;

            if (MaximumViewCount != -1) n = Items.Count > MaximumViewCount ? MaximumViewCount : Items.Count;
            Size inflatedDropSize = new Size(rt.Width, n * ItemHeight + 2);
            Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = ptu.Y - screenBounds.Height + 1;
            return screenBounds;
        }
        #endregion
        #region CloseDropDown
        public void CloseDropDown()
        {
            if (dropContainer != null)
            {
                DropState = DvDropState.Closing;
                dropContainer.Freeze = false;
                dropContainer.Close();
            }
        }
        #endregion
        #region GetDropDownContainerDir
        internal int GetDropDownContainerDir()
        {
            int ret = -1;
            if (DropState == DvDropState.Dropping || DropState == DvDropState.Dropped)
            {
                var p1 = Grid.PointToScreen(new Point(0, 0));
                var p2 = dropContainer.Location;

                ret = p1.Y < p2.Y ? 1 : 2;
            }
            return ret;
        }
        #endregion
        #endregion

        #region Class
        #region DropWindowEventArgs
        internal class DropWindowEventArgs : EventArgs
        {
            internal DvDropState DropState { get; private set; }
            public DropWindowEventArgs(DvDropState DropState)
            {
                this.DropState = DropState;
            }
        }
        #endregion
        #region DropDownContainer
        public class DropDownContainer : DvForm, IMessageFilter
        {
            #region Properties
            internal bool Freeze { get; set; }
            public DvDataGridComboBoxCell ComboBox { get; private set; }
            public long VScrollPosition
            {
                get => ListBox.ScrollPosition;
                set
                {
                    if (ListBox.ScrollPosition != value)
                    {
                        ListBox.ScrollPosition = value;
                        ListBox.Invalidate();
                    }
                }
            }
            #endregion

            #region Member Variable
            private DvListBox ListBox = new DvListBox();
            #endregion

            #region Event
            internal event EventHandler<DropWindowEventArgs> DropStateChanged;
            #endregion

            public DropDownContainer(DvDataGridComboBoxCell c)
            {
                #region Init
                this.BlankForm = true;
                this.DoubleBuffered = true;
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.ControlBox = false;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.AutoSize = false;
                this.AutoScroll = false;
                this.MinimumSize = new Size(10, 10);
                this.Padding = new Padding(0, 0, 0, 0);
                #endregion
                #region Set
                var Theme = c.Grid.GetTheme();
                var RowColor = c.Grid.GetRowColor(Theme);
                var SelectedRowColor = c.Grid.GetSelectedRowColor(Theme);
                var cBack = RowColor;
                var cFore = c.Grid.ForeColor;
                var cBox = cBack;
                this.ComboBox = c;
                this.Font = c.Grid.Font;
                this.BackColor = cBack;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.ForeColor = cFore;
                ListBox.BackColor = cBack;
                ListBox.BoxColor = cBox;
                ListBox.RectMode = true;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.SINGLE;
                //ListBox.Corner = 0;
                ListBox.RowHeight = c.ItemHeight;
                ListBox.TouchMode = c.Grid.TouchMode;
                ListBox.ItemClicked += (o, s) =>
                {
                    if (s.Item != null)
                    {
                        if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                        c.Value = ((DvDataGridComboBoxItem)s.Item).Source;

                        this.Close();
                    }
                };

                if (c.Value != null && c.Value is DvDataGridComboBoxItem && ListBox.SelectedItems.Contains(c.Value)) ListBox.SelectedItems.Add((DvDataGridComboBoxItem)c.Value);

                this.Controls.Add(ListBox);
                #endregion

                this.BackColor = ListBox.BackColor = cBack;
                this.ForeColor = ListBox.ForeColor = cFore;
                ListBox.UseThemeColor = false;
                ListBox.BoxColor = cBox;
                ListBox.ItemColor = RowColor;
                ListBox.SelectedItemColor = SelectedRowColor;
                ListBox.RowHeight = c.Grid.RowHeight;
            }

            #region Implements
            #region PreFilterMessage
            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                    this.Close();
                }
                return false;
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditTextCell 
    public class DvDataGridEditTextCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditTextCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(DvDataGrid.InputBright);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null)
            {
                var s = "";

                if (Value is string) s = (string)Value;
                else s = Value.ToString();

                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var ret = Grid.InputBox.ShowString("입력 : " + Column.HeaderText, Value as string);
                if (ret != null)
                {
                    var v = ret;
                    if (v != Value as string)
                    {
                        var old = Value;
                        Value = v;
                        Grid.InvokeValueChanged(this, old, v);
                    }
                }
            }

            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditNumberCell 
    public class DvDataGridEditNumberCell : DvDataGridCell
    {
        #region Properties
        public string Format { get; set; }
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditNumberCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridTextFormatColumn)
            {
                this.Format = ((DvDataGridTextFormatColumn)Column).Format;
            }
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(DvDataGrid.InputBright);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null)
            {
                var s = "";

                if (Value is byte) s = (!string.IsNullOrWhiteSpace(Format) ? ((byte)Value).ToString(Format) : ((byte)Value).ToString());
                else if (Value is short) s = (!string.IsNullOrWhiteSpace(Format) ? ((short)Value).ToString(Format) : ((short)Value).ToString());
                else if (Value is ushort) s = (!string.IsNullOrWhiteSpace(Format) ? ((ushort)Value).ToString(Format) : ((ushort)Value).ToString());
                else if (Value is int) s = (!string.IsNullOrWhiteSpace(Format) ? ((int)Value).ToString(Format) : ((int)Value).ToString());
                else if (Value is uint) s = (!string.IsNullOrWhiteSpace(Format) ? ((uint)Value).ToString(Format) : ((uint)Value).ToString());
                else if (Value is long) s = (!string.IsNullOrWhiteSpace(Format) ? ((long)Value).ToString(Format) : ((long)Value).ToString());
                else if (Value is ulong) s = (!string.IsNullOrWhiteSpace(Format) ? ((ulong)Value).ToString(Format) : ((ulong)Value).ToString());
                else if (Value is float) s = (!string.IsNullOrWhiteSpace(Format) ? ((float)Value).ToString(Format) : ((float)Value).ToString());
                else if (Value is double) s = (!string.IsNullOrWhiteSpace(Format) ? ((double)Value).ToString(Format) : ((double)Value).ToString());
                else if (Value is decimal) s = (!string.IsNullOrWhiteSpace(Format) ? ((decimal)Value).ToString(Format) : ((decimal)Value).ToString());

                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                if (Value is byte)
                {
                    var ret = Grid.InputBox.ShowByte("입력 : " + Column.HeaderText, Value as byte?);
                    if (ret.HasValue && ret.Value != Value as byte?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is short)
                {
                    var ret = Grid.InputBox.ShowShort("입력 : " + Column.HeaderText, Value as short?);
                    if (ret.HasValue && ret.Value != Value as short?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is ushort)
                {
                    var ret = Grid.InputBox.ShowUShort("입력 : " + Column.HeaderText, Value as ushort?);
                    if (ret.HasValue && ret.Value != Value as ushort?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is int)
                {
                    var ret = Grid.InputBox.ShowInt("입력 : " + Column.HeaderText, Value as int?);
                    if (ret.HasValue && ret.Value != Value as int?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is uint)
                {
                    var ret = Grid.InputBox.ShowUInt("입력 : " + Column.HeaderText, Value as uint?);
                    if (ret.HasValue && ret.Value != Value as uint?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is long)
                {
                    var ret = Grid.InputBox.ShowLong("입력 : " + Column.HeaderText, Value as long?);
                    if (ret.HasValue && ret.Value != Value as long?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is ulong)
                {
                    var ret = Grid.InputBox.ShowULong("입력 : " + Column.HeaderText, Value as ulong?);
                    if (ret.HasValue && ret.Value != Value as ulong?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is float)
                {
                    var ret = Grid.InputBox.ShowFloat("입력 : " + Column.HeaderText, Value as float?);
                    if (ret.HasValue && ret.Value != Value as float?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is double)
                {
                    var ret = Grid.InputBox.ShowDouble("입력 : " + Column.HeaderText, Value as double?);
                    if (ret.HasValue && ret.Value != Value as double?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }
                else if (Value is decimal)
                {
                    var ret = Grid.InputBox.ShowDecimal("입력 : " + Column.HeaderText, Value as decimal?);
                    if (ret.HasValue && ret.Value != Value as decimal?) { var old = Value; Value = ret.Value; Grid.InvokeValueChanged(this, old, ret.Value); }
                }


            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditDateTimeCell 
    public class DvDataGridEditDateTimeCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        public string Format { get; set; }
        public DvDateTimePickerStyle PickerMode { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditDateTimeCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridDateTimePickerColumn)
            {
                this.Format = ((DvDataGridDateTimePickerColumn)Column).Format;
                this.PickerMode = ((DvDataGridDateTimePickerColumn)Column).PickerMode;
            }
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(DvDataGrid.InputBright);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null)
            {
                var s = "";

                if (Value is DateTime)
                {
                    if (Format == null)
                    {
                        switch (PickerMode)
                        {
                            case DvDateTimePickerStyle.DateTime: s = ((DateTime)Value).ToString("yyyy-MM-dd HH:mm:ss"); break;
                            case DvDateTimePickerStyle.Date: s = ((DateTime)Value).ToString("yyyy-MM-dd"); break;
                            case DvDateTimePickerStyle.Time: s = ((DateTime)Value).ToString("HH:mm:ss"); break;
                        }
                    }
                    else s = ((DateTime)Value).ToString(Format);
                }
                else s = Value.ToString();

                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rt);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rt);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                DateTime? ret = null;

                switch (PickerMode)
                {
                    case DvDateTimePickerStyle.DateTime: ret = Grid.DateTimePicker.ShowDateTimePicker("입력 : " + Column.HeaderText, (DateTime?)Value); break;
                    case DvDateTimePickerStyle.Date: ret = Grid.DateTimePicker.ShowDatePicker("입력 : " + Column.HeaderText, (DateTime?)Value); break;
                    case DvDateTimePickerStyle.Time: ret = Grid.DateTimePicker.ShowTimePicker("입력 : " + Column.HeaderText, (DateTime?)Value); break;
                }

                if (ret.HasValue)
                {
                    if (ret.Value != (DateTime?)Value)
                    {
                        var old = Value;
                        Value = ret.Value;
                        Grid.InvokeValueChanged(this, old, ret.Value);
                    }
                }
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvDataGridEditColorCell 
    public class DvDataGridEditColorCell : DvDataGridCell
    {
        #region Properties
        public bool ReadOnly { get; set; }
        #endregion
        #region Constructor
        public DvDataGridEditColorCell(DvDataGrid Grid, DvDataGridRow Row, DvDataGridColumn Column) : base(Grid, Row, Column)
        {
            if (Column is DvDataGridColorPickerColumn)
            {
            }
        }
        #endregion
        #region Override
        #region CellPaint
        public override void CellPaint(DvTheme Theme, Graphics g, Rectangle CellBounds)
        {
            var f = Grid.DpiRatio;
            var cTextBack = CellBackColor.BrightnessTransmit(DvDataGrid.InputBright);
            var rt = new Rectangle(CellBounds.X, CellBounds.Y, CellBounds.Width, CellBounds.Height); rt.Inflate(-Convert.ToInt32(2 * f), -Convert.ToInt32(2 * f));
            Theme.DrawBox(g, cTextBack, cTextBack, rt, RoundType.NONE, BoxDrawOption.BORDER);
            if (Value != null && Value is Color)
            {
                var vc = (Color)Value;
                var s = "#" + vc.R.ToString("X2") + vc.G.ToString("X2") + vc.B.ToString("X2");

                var vwh = Convert.ToInt32(f * 16);
                var gap = Convert.ToInt32(f * 5);

                var sz = g.MeasureString(s, Grid.Font);
                
                var rtw = DvDataGridTool.RTI(MathTool.MakeRectangle(rt, new SizeF((sz.Width + 4) + gap + (vwh), Math.Max(vwh, sz.Height + 4))));
                var rtBox = MathTool.MakeRectangle(rtw, new Size(vwh, vwh)); rtBox.X = rtw.X;
                var rtText = MathTool.MakeRectangle(rtw, new Size(Convert.ToInt32(sz.Width + 4), Convert.ToInt32(sz.Height + 4))); rtText.X = rtw.Right - Convert.ToInt32(sz.Width + 4);

                Theme.DrawBox(g, vc, cTextBack, rtBox, RoundType.NONE, BoxDrawOption.BORDER);
                if (!string.IsNullOrWhiteSpace(s))
                {
                    var c = CellTextColor;
                    var bg = (Row.Selected ? SelectedCellBackColor : CellBackColor);
                    if (Grid.TextShadow) Theme.DrawTextShadow(g, null, s, Grid.Font, c, bg, rtText);
                    else Theme.DrawText(g, null, s, Grid.Font, c, bg, rtText);
                }
            }
            base.CellPaint(Theme, g, CellBounds);
        }
        #endregion
        #region CellMouseUp
        public override void CellMouseUp(Rectangle CellBounds, int x, int y)
        {
            if (CollisionTool.Check(CellBounds, x, y))
            {
                var ret = Grid.ColorPicker.ShowColorPicker("입력 : " + Column.HeaderText, (Color?)Value);
                if (ret.HasValue)
                {
                    if (ret.Value != (Color?)Value)
                    {
                        #region Value Set
                        var old = Value;
                        Value = ret.Value;
                        Grid.InvokeValueChanged(this, old, ret.Value);
                        #endregion
                    }
                }
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
}
