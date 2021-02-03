using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
    public class DvCalendar : DvControl
    {
        #region Properties
        #region DaysBoxColor
        private Color cDaysBoxColor = DvTheme.DefaultTheme.Color2;
        public Color DaysBoxColor
        {
            get { return cDaysBoxColor; }
            set
            {
                if (cDaysBoxColor != value)
                {
                    cDaysBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region WeeklyBoxColor
        private Color cWeeklyBoxColor = DvTheme.DefaultTheme.Color1;
        public Color WeeklyBoxColor
        {
            get { return cWeeklyBoxColor; }
            set
            {
                if (cWeeklyBoxColor != value)
                {
                    cWeeklyBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region MonthlyBoxColor
        private Color cMonthlyBoxColor = DvTheme.DefaultTheme.Color2;
        public Color MonthlyBoxColor
        {
            get { return cMonthlyBoxColor; }
            set
            {
                if (cMonthlyBoxColor != value)
                {
                    cMonthlyBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectColor
        private Color cSelectColor = Color.Cyan;
        public Color SelectColor
        {
            get { return cSelectColor; }
            set
            {
                if (cSelectColor != value)
                {
                    cSelectColor = value; Invalidate();
                }
            }
        }
        #endregion

        #region MultiSelect
        private bool bMultiSelect = false;
        public bool MultiSelect
        {
            get { return bMultiSelect; }
            set
            {
                bMultiSelect = value;
            }
        }
        #endregion
        #region NoneSelect
        private bool bNoneSelect = false;
        public bool NoneSelect
        {
            get { return bNoneSelect; }
            set
            {
                bNoneSelect = value;
            }
        }
        #endregion

        #region CurrentMonth
        int nYear = DateTime.Now.Year;
        int nMonth = DateTime.Now.Month;
        public int CurrentYear { get { return nYear; } }
        public int CurrentMonth { get { return nMonth; } }
        public string CurrentMonthText
        {
            get { return nYear + "." + nMonth; }
        }
        #endregion
        #region SelectDays
        private List<DateTime> lstSel = new List<DateTime>();
        public event EventHandler SelectDaysChanged;
        public List<DateTime> SelectDays
        {
            get { return lstSel; }
        }
        #endregion
        #endregion

        #region Member Variable
        bool bMonthPrev = false, bMonthNext = false;
        #endregion

        #region Constructor
        public DvCalendar()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(250, 200);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            var f = DpiRatio;
            var rtContent = GetContentBounds();
            var nh = rtContent.Height / 8;
            var rtMonthly = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, nh);
            var rtWeekly = new Rectangle(rtContent.X, rtMonthly.Bottom, rtContent.Width, nh);
            var rtDays = new Rectangle(rtContent.X, rtWeekly.Bottom, rtContent.Width, rtContent.Height - (rtMonthly.Height + rtWeekly.Height));
            var rtMonthPrev = new Rectangle(rtMonthly.X, rtMonthly.Y, rtMonthly.Height, rtMonthly.Height);
            var rtMonthNext = new Rectangle(rtMonthly.Right - rtMonthly.Height, rtMonthly.Y, rtMonthly.Height, rtMonthly.Height);
            var rtMonthText = new Rectangle(rtMonthPrev.Right, rtMonthly.Top, rtMonthly.Width - (rtMonthPrev.Width + rtMonthNext.Width), rtMonthly.Height);

            var vw = (float)rtDays.Width / 7F;
            var vh = (float)rtDays.Height / 6F;

            var xs = new List<int>();
            var ys = new List<int>();

            for (int i = 0; i < 7; i++) xs.Add(rtDays.X + (int)Math.Round(vw * i));
            for (int i = 0; i < 6; i++) ys.Add(rtDays.Y + (int)Math.Round(vh * i));

            for (int iy = 0; iy < 6; iy++)
            {
                int y = ys[iy];
                int h = (iy == 5 ? rtDays.Bottom : ys[iy + 1]) - y;
                for (int ix = 0; ix < 7; ix++)
                {
                    int x = xs[ix];
                    int w = (ix == 6 ? rtDays.Right : xs[ix + 1]) - x;
                    var rt = new Rectangle(x, y, w, h);
                    SetArea("rtBox_" + iy + "_" + ix, rt);

                    if (iy == 0) SetArea("rtWeek_" + ix, new Rectangle(x, rtWeekly.Y, w, rtWeekly.Height));
                }
            }

            SetArea("rtMonthly", rtMonthly);
            SetArea("rtWeekly", rtWeekly);
            SetArea("rtDays", rtDays);
            SetArea("rtMonthPrev", rtMonthPrev);
            SetArea("rtMonthNext", rtMonthNext);
            SetArea("rtMonthText", rtMonthText);
            base.LoadAreas(g);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var DaysBoxColor = UseThemeColor ? Theme.Color2 : this.DaysBoxColor;
            var WeeklyBoxColor = UseThemeColor ? Theme.Color1 : this.WeeklyBoxColor;
            var MonthlyBoxColor = UseThemeColor ? Theme.Color2 : this.MonthlyBoxColor;
            #endregion
            #region Set
            var f = DpiRatio;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];

            var rtMonthly = Areas["rtMonthly"];
            var rtWeekly = Areas["rtWeekly"];
            var rtDays = Areas["rtDays"];
            var rtMonthPrev = Areas["rtMonthPrev"];
            var rtMonthNext = Areas["rtMonthNext"];
            var rtMonthText = Areas["rtMonthText"];
            #endregion
            #region DayList
            int Days = DateTime.DaysInMonth(CurrentYear, CurrentMonth);
            DateTime dt = new DateTime(CurrentYear, CurrentMonth, 1);
            int ndw = (int)dt.DayOfWeek;
            DateTime[] d = new DateTime[42];
            int startidx = ndw == 0 ? 7 : ndw;
            int endidx = startidx + Days;
            dt -= new TimeSpan(startidx, 0, 0, 0);
            for (int i = 0; i < 42; i++)
            {
                d[i] = dt;
                dt += new TimeSpan(1, 0, 0, 0);
            }
            #endregion
            #region Draw
            #region BG
            Theme.DrawBox(e.Graphics, DaysBoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.OUT_SHADOW);
            Theme.DrawBox(e.Graphics, MonthlyBoxColor, BackColor, rtMonthly,  RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.GRADIENT_V);
            Theme.DrawBox(e.Graphics, WeeklyBoxColor, BackColor, rtWeekly,  RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);
            #endregion
            #region Month Text / Prev / Next
            {
                var cp = bMonthPrev ? ForeColor.BrightnessTransmit(Theme.DownBright) : ForeColor;
                var cn = bMonthNext ? ForeColor.BrightnessTransmit(Theme.DownBright) : ForeColor;

                if (bMonthPrev) { rtMonthPrev.Offset(0, 1); }
                if (bMonthNext) { rtMonthNext.Offset(0, 1); }

                Theme.DrawTextShadow(e.Graphics, null, CurrentYear + "." + CurrentMonth, Font, ForeColor, BackColor, rtMonthText);
                br.Color = cp; e.Graphics.DrawIcon(new DvIcon("fas fa-chevron-left"), br, rtMonthPrev, DvContentAlignment.MiddleCenter);
                br.Color = cn; e.Graphics.DrawIcon(new DvIcon("fas fa-chevron-right"), br, rtMonthNext, DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Week
            for (int ix = 0; ix < 7; ix++)
            {
                var rt = Areas["rtWeek_" + ix];
                string s = "";
                Color c = ForeColor;
                switch ((DayOfWeek)ix)
                {
                    case DayOfWeek.Sunday: s = "SUN"; c = Color.Red; break;
                    case DayOfWeek.Monday: s = "MON"; break;
                    case DayOfWeek.Tuesday: s = "TUE"; break;
                    case DayOfWeek.Wednesday: s = "WED"; break;
                    case DayOfWeek.Thursday: s = "THR"; break;
                    case DayOfWeek.Friday: s = "FRI"; break;
                    case DayOfWeek.Saturday: s = "SAT"; c = Color.Blue; break;
                }

                Theme.DrawText(e.Graphics, null, s, Font, c, rt, DvContentAlignment.MiddleCenter);
            }
            #endregion
            #region Days
            {
                for (int iy = 0; iy < 6; iy++)
                {
                    for (int ix = 0; ix < 7; ix++)
                    {
                        #region Bounds
                        var rt = Areas["rtBox_" + iy + "_" + ix];
                        var idx = iy * 7 + ix;
                        var tm = d[idx];
                        #endregion
                        #region Box
                        {
                            var cBox = DaysBoxColor;

                            if (ix == 0 && iy == 5) Theme.DrawBox(e.Graphics, cBox, BackColor, rt, RoundType.LB, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);
                            else if (ix == 6 && iy == 5) Theme.DrawBox(e.Graphics, cBox, BackColor, rt, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);
                            else Theme.DrawBox(e.Graphics, cBox, BackColor, rt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT);
                        }
                        #endregion
                        #region Text
                        if (!SelectDays.Contains(tm))
                        {
                            var ctext = ForeColor;
                            var s = tm.Day.ToString();

                            if (idx >= startidx && idx < endidx)
                            {
                                ctext = (ix == 0 ? Color.Red : (ix == 6 ? Color.Blue : ForeColor));
                                Theme.DrawTextShadow(e.Graphics, null, s, Font, ctext, BackColor, rt, DvContentAlignment.MiddleCenter);
                            }
                            else
                            {
                                ctext = Color.FromArgb(120, Color.Black);
                                Theme.DrawText(e.Graphics, null, s, Font, ctext, rt, DvContentAlignment.MiddleCenter);
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion
            #region SelectDays
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var v in SelectDays)
            {
                var sidx = d.ToList().IndexOf(v);
                if (sidx >= 0)
                {
                    #region Bounds
                    var iy = sidx / 7;
                    var ix = sidx - (iy * 7);

                    var rt = Areas["rtBox_" + iy + "_" + ix];
                    var rtsh = new Rectangle(rt.Location, rt.Size); rtsh.Offset(0, 1);
                    var idx = iy * 7 + ix;
                    var tm = d[idx];
                    #endregion

                    var c = SelectColor;
                    #region Border
                    {
                        var rtv = new Rectangle(rt.X, rt.Y, rt.Width, rt.Height); rtv.Inflate(-1, -1);
                        p.Width = 2;
                        p.Color = c;
                        if (ix == 0 && iy == 5) Theme.DrawBorder(e.Graphics, c, DaysBoxColor, 2, rtv, RoundType.LB, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        else if (ix == 6 && iy == 5) Theme.DrawBorder(e.Graphics, c, DaysBoxColor, 2, rtv, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        else Theme.DrawBorder(e.Graphics, c, DaysBoxColor, 2, rtv, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                    }
                    #endregion
                    #region Text
                    {
                        var ctext = c;
                        var s = tm.Day.ToString();

                        Theme.DrawTextShadow(e.Graphics, null, s, Font, ctext, BackColor, rt, DvContentAlignment.MiddleCenter);
                    }
                    #endregion
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            #region DayList
            int Days = DateTime.DaysInMonth(CurrentYear, CurrentMonth);
            DateTime dt = new DateTime(CurrentYear, CurrentMonth, 1);
            int ndw = (int)dt.DayOfWeek;
            DateTime[] d = new DateTime[42];
            int startidx = ndw == 0 ? 7 : ndw;
            int endidx = startidx + Days;
            dt -= new TimeSpan(startidx, 0, 0, 0);
            for (int i = 0; i < 42; i++)
            {
                d[i] = dt;
                dt += new TimeSpan(1, 0, 0, 0);
            }
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];

            var rtMonthly = Areas["rtMonthly"];
            var rtWeekly = Areas["rtWeekly"];
            var rtDays = Areas["rtDays"];
            var rtMonthPrev = Areas["rtMonthPrev"];
            var rtMonthNext = Areas["rtMonthNext"];
            var rtMonthText = Areas["rtMonthText"];
            #endregion
            #region Month Prev / Next
            if (CollisionTool.Check(rtMonthPrev, e.X, e.Y)) bMonthPrev = true;
            if (CollisionTool.Check(rtMonthNext, e.X, e.Y)) bMonthNext = true;
            #endregion
            #region Days
            if (!NoneSelect)
            {
                for (int iy = 0; iy < 6; iy++)
                {
                    for (int ix = 0; ix < 7; ix++)
                    {

                        var rt = Areas["rtBox_" + iy + "_" + ix];
                        var idx = iy * 7 + ix;
                        var tm = d[idx];

                        if (CollisionTool.Check(rt, e.Location))
                        {
                            if (idx >= startidx && idx < endidx)
                            {
                                if (MultiSelect)
                                {
                                    if (ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift)
                                    {
                                        if (SelectDays.Contains(tm)) SelectDays.Remove(tm);
                                        else SelectDays.Add(tm);
                                        SelectDaysChanged?.Invoke(this, null);
                                    }
                                    else
                                    {
                                        SelectDays.Clear();
                                        SelectDays.Add(tm);
                                        SelectDaysChanged?.Invoke(this, null);
                                    }
                                }
                                else
                                {
                                    SelectDays.Clear();
                                    SelectDays.Add(tm);
                                    SelectDaysChanged?.Invoke(this, null);
                                }
                            }
                            else
                            {
                                nYear = tm.Year;
                                nMonth = tm.Month;
                            }
                        }
                    }
                }
            }
            #endregion
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            #region Month Prev
            if (bMonthPrev)
            {
                bMonthPrev = false;
                nMonth--;
                if (nMonth < 1)
                {
                    nYear--;
                    nMonth = 12;
                }
            }
            #endregion
            #region Month Next
            if (bMonthNext)
            {
                bMonthNext = false;
                nMonth++;
                if (nMonth > 12)
                {
                    nYear++;
                    nMonth = 1;
                }
            }
            #endregion
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion 
        #endregion

        #region Method
        #region SetCurrentDate
        public void SetCurrentDate(int Year, int Month)
        {
            nYear = Year; 
            nMonth = Month; 
            Invalidate();
        }
        #endregion
        #endregion

    }
}
