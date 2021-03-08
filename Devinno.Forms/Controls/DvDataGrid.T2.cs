using Devinno.Extensions;
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
        public override void CellPaint(DvTheme Theme, Graphics g, RectangleF CellBounds)
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
                Grid.Invalidate();
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
                Grid.Invalidate();
                if (CollisionTool.Check(CellBounds, x, y)) Grid.InvokeCellButtonClick(this);
            }
            base.CellMouseUp(CellBounds, x, y);
        }
        #endregion
        #endregion
    }
    #endregion
}
