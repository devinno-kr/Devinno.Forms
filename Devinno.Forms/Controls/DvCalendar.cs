using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Color? cDaysBoxColor = null;
        public Color? DaysBoxColor
        {
            get => cDaysBoxColor;
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
        private Color? cWeeklyBoxColor = null;
        public Color? WeeklyBoxColor
        {
            get => cWeeklyBoxColor;
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
        private Color? cMonthlyBoxColor = null;
        public Color? MonthlyBoxColor
        {
            get => cMonthlyBoxColor;
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
        private Color? cSelectColor = null;
        public Color? SelectColor
        {
            get => cSelectColor;
            set
            {
                if (cSelectColor != value)
                {
                    cSelectColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region CurrentYear
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        #endregion
        #region CurrentMonth
        public int CurrentMonth { get; set; } = DateTime.Now.Month;
        #endregion
        #region CurrentMonthText
        public string CurrentMonthText => CurrentYear + "." + CurrentMonth;
        #endregion

        #region MultiSelect
        public bool MultiSelect { get; set; } = false;
        #endregion
        #region NoneSelect
        public bool NoneSelect { get; set; } = false;
        #endregion

        #region WeeklyFont
        private Font ftWeek = new Font("나눔고딕", 8, FontStyle.Regular);
        public Font WeeklyFont
        {
            get => ftWeek;
            set
            {
                if (ftWeek != value)
                {
                    ftWeek = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region SelectedDays 
        public List<DateTime> SelectedDays { get; } = new List<DateTime>();
        #endregion
        #endregion

        #region Member Variable
        bool bMonthPrev = false, bMonthNext = false;
        #endregion

        #region Event
        public event EventHandler SelectedDaysChanged;
        #endregion

        #region Constructor
        public DvCalendar()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var DaysBoxColor = this.DaysBoxColor ?? Theme.CalendarDaysColor;
            var WeeklyBoxColor = this.WeeklyBoxColor ?? Theme.CalendarWeeksColor;
            var MonthlyBoxColor = this.MonthlyBoxColor ?? Theme.CalendarMonthColor;
            var SelectColor = this.SelectColor ?? Theme.CalendarSelectColor;
            var BorderColor = Theme.GetBorderColor(WeeklyBoxColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            #endregion

            Areas((rtContent, rtMonthly, rtWeekly, rtDays, rtMonthPrev, rtMonthNext, rtMonthText, dicBoxes, dicWeeks) =>
            {
                #region DayList
                int Days = DateTime.DaysInMonth(CurrentYear, CurrentMonth);
                DateTime dt = new DateTime(CurrentYear, CurrentMonth, 1);
                int ndw = (int)dt.DayOfWeek;
                DateTime[] d = new DateTime[42];
                int startidx = ndw == 0 ? 7 : ndw;
                int endidx = startidx + Days;
                if (dt.Date.Year == 1 && dt.Date.Month == 1 && dt.Date.Day == 1) { }
                else dt -= new TimeSpan(startidx, 0, 0, 0);

                for (int i = 0; i < 42; i++)
                {
                    d[i] = dt;
                    dt += new TimeSpan(1, 0, 0, 0);
                }
                #endregion
                #region Draw
                #region BG
                Theme.DrawBox(e.Graphics, rtContent, DaysBoxColor, BorderColor, RoundType.All, BoxStyle.Border | BoxStyle.Fill | BoxStyle.OutShadow);
                Theme.DrawBox(e.Graphics, rtMonthly, MonthlyBoxColor, BorderColor, RoundType.T, BoxStyle.Border | BoxStyle.GradientV | BoxStyle.InBevel);
                Theme.DrawBox(e.Graphics, rtWeekly, WeeklyBoxColor, BorderColor, RoundType.Rect, BoxStyle.Border | BoxStyle.Fill | BoxStyle.InShadow);
                #endregion
                #region Month Text / Prev / Next
                {
                    var cp = bMonthPrev ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                    var cn = bMonthNext ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;

                    if (bMonthPrev) { rtMonthPrev.Offset(0, 1); }
                    if (bMonthNext) { rtMonthNext.Offset(0, 1); }

                    var isz = (int)rtMonthPrev.Height / 3;

                    Theme.DrawText(e.Graphics, CurrentMonthText, Font, ForeColor, rtMonthText);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fas fa-chevron-left", isz), cp, rtMonthPrev);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fas fa-chevron-right", isz), cn, rtMonthNext);
                }
                #endregion
                #region Week
                for (int ix = 0; ix < 7; ix++)
                {
                    var rt = dicWeeks["rtWeek_" + ix];
                    string s = "";
                    var c = ForeColor;
                    switch ((DayOfWeek)ix)
                    {
                        case DayOfWeek.Sunday: s = "SUN"; c =  Color.Red; break;
                        case DayOfWeek.Monday: s = "MON"; break;
                        case DayOfWeek.Tuesday: s = "TUE"; break;
                        case DayOfWeek.Wednesday: s = "WED"; break;
                        case DayOfWeek.Thursday: s = "THR"; break;
                        case DayOfWeek.Friday: s = "FRI"; break;
                        case DayOfWeek.Saturday: s = "SAT"; c = Color.DodgerBlue; break;
                    }

                    Theme.DrawText(e.Graphics, s, WeeklyFont, c, rt);
                }
                #endregion
                #region Days
                {
                    p.Width = 1;
                    p.Color = Theme.GetInBevelColor(DaysBoxColor);
                    e.Graphics.DrawLine(p, rtDays.Left + 1, rtDays.Top + 0.5F, rtDays.Right - 1, rtDays.Top + 0.5F);

                    for (int iy = 0; iy < 6; iy++)
                    {
                        for (int ix = 0; ix < 7; ix++)
                        {
                            #region Bounds
                            var rt = dicBoxes["rtBox_" + iy + "_" + ix];
                            var idx = iy * 7 + ix;
                            var tm = d[idx];
                            #endregion
                            #region Text
                            if (!SelectedDays.Contains(tm))
                            {
                                var ctext = ForeColor;
                                var s = tm.Day.ToString();

                                if (idx >= startidx && idx < endidx)
                                {
                                    ctext = (ix == 0 ? Color.Red : (ix == 6 ? Color.DodgerBlue : ForeColor));
                                    Theme.DrawText(e.Graphics, s, Font, ctext, rt);

                                }
                                else
                                {
                                    ctext = Util.FromArgb(120, Color.Black);
                                    Theme.DrawText(e.Graphics, s, Font, ctext, rt, DvContentAlignment.MiddleCenter, false);
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion
                #region SelectDays
                foreach (var v in SelectedDays)
                {
                    var sidx = d.ToList().IndexOf(v.Date);
                    if (sidx >= 0)
                    {
                        #region Bounds
                        var iy = sidx / 7;
                        var ix = sidx - (iy * 7);

                        var rt = dicBoxes["rtBox_" + iy + "_" + ix];
                        var rtsh = rt; rtsh.Offset(0, 1);
                        var idx = iy * 7 + ix;
                        var tm = d[idx];
                        #endregion

                        var c = SelectColor;
                        #region Border
                        {
                            var rtv = rt; rtv.Inflate(-1, -1);

                            if (ix == 0 && iy == 5)
                                Theme.DrawBox(e.Graphics, rtv, DaysBoxColor, c, RoundType.LB, BoxStyle.Border);
                            else if (ix == 6 && iy == 5)
                                Theme.DrawBox(e.Graphics, rtv, DaysBoxColor, c, RoundType.RB, BoxStyle.Border);
                            else
                                Theme.DrawBox(e.Graphics, rtv, DaysBoxColor, c, RoundType.Rect, BoxStyle.Border);
                        }
                        #endregion
                        #region Text
                        {
                            var ctext = c;
                            var s = tm.Day.ToString();

                            Theme.DrawText(e.Graphics, s, Font, ctext, rt);
                        }
                        #endregion
                    }
                }
                #endregion
                #endregion
            });

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;

            Areas((rtContent, rtMonthly, rtWeekly, rtDays, rtMonthPrev, rtMonthNext, rtMonthText, dicBoxes, dicWeeks) =>
            {
                #region DayList
                int Days = DateTime.DaysInMonth(CurrentYear, CurrentMonth);
                DateTime dt = new DateTime(CurrentYear, CurrentMonth, 1);
                int ndw = (int)dt.DayOfWeek;
                DateTime[] d = new DateTime[42];
                int startidx = ndw == 0 ? 7 : ndw;
                int endidx = startidx + Days;
                if (dt.Date.Year == 1 && dt.Date.Month == 1 && dt.Date.Day == 1) { }
                else dt -= new TimeSpan(startidx, 0, 0, 0);

                for (int i = 0; i < 42; i++)
                {
                    d[i] = dt;
                    dt += new TimeSpan(1, 0, 0, 0);
                }
                #endregion
                #region Month Prev / Next
                if (CollisionTool.Check(rtMonthPrev, x, y)) bMonthPrev = true;
                if (CollisionTool.Check(rtMonthNext, x, y)) bMonthNext = true;
                #endregion
                #region Days
                if (!NoneSelect)
                {
                    for (int iy = 0; iy < 6; iy++)
                    {
                        for (int ix = 0; ix < 7; ix++)
                        {
                            var rt = dicBoxes["rtBox_" + iy + "_" + ix];
                            var idx = iy * 7 + ix;
                            var tm = d[idx];

                            if (CollisionTool.Check(rt, x, y))
                            {
                                if (idx >= startidx && idx < endidx)
                                {
                                    if (MultiSelect)
                                    {
                                        if (SelectedDays.Contains(tm)) SelectedDays.Remove(tm);
                                        else SelectedDays.Add(tm);
                                        SelectedDaysChanged?.Invoke(this, null);
                                    }
                                    else
                                    {
                                        SelectedDays.Clear();
                                        SelectedDays.Add(tm);
                                        SelectedDaysChanged?.Invoke(this, null);
                                    }
                                }
                                else
                                {
                                    CurrentYear = tm.Year;
                                    CurrentMonth = tm.Month;
                                }
                            }
                        }
                    }
                }
                #endregion
            });

            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;

            #region Month Prev
            if (bMonthPrev)
            {
                bMonthPrev = false;
                CurrentMonth--;
                if (CurrentMonth < 1)
                {
                    CurrentYear--;
                    CurrentMonth = 12;
                }
            }
            #endregion
            #region Month Next
            if (bMonthNext)
            {
                bMonthNext = false;
                CurrentMonth++;
                if (CurrentMonth > 12)
                {
                    CurrentYear++;
                    CurrentMonth = 1;
                }
            }
            #endregion

            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion 
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, Dictionary<string, RectangleF>, Dictionary<string, RectangleF>> act)
        {
            var rtContent = GetContentBounds();

            var nh = rtContent.Height / 8;
            var rtMonthly = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, nh + 1);
            var rtWeekly = Util.FromRect(rtContent.Left, rtContent.Top + nh, rtContent.Width, nh);
            var rtDays = Util.FromRect(rtContent.Left, rtWeekly.Bottom, rtContent.Width, rtContent.Height - (rtMonthly.Height + rtWeekly.Height));
            var rtMonthPrev = Util.FromRect(rtMonthly.Left, rtMonthly.Top, rtMonthly.Height, rtMonthly.Height);
            var rtMonthNext = Util.FromRect(rtMonthly.Right - rtMonthly.Height, rtMonthly.Top, rtMonthly.Height, rtMonthly.Height);
            var rtMonthText = Util.FromRect(rtMonthPrev.Right, rtMonthly.Top, rtMonthly.Width - (rtMonthPrev.Width + rtMonthNext.Width), rtMonthly.Height);

            var vw = (float)rtDays.Width / 7F;
            var vh = (float)rtDays.Height / 6F;

            var xs = new List<int>();
            var ys = new List<int>();

            for (int i = 0; i < 7; i++) xs.Add(Convert.ToInt32(rtDays.Left + Math.Round(vw * i)));
            for (int i = 0; i < 6; i++) ys.Add(Convert.ToInt32(rtDays.Top + Math.Round(vh * i)));

            var dicBoxes = new Dictionary<string, RectangleF>();
            var dicWeeks = new Dictionary<string, RectangleF>();
            for (int iy = 0; iy < 6; iy++)
            {
                int y = ys[iy];
                int h = Convert.ToInt32(iy == 5 ? rtDays.Bottom : ys[iy + 1]) - y;
                for (int ix = 0; ix < 7; ix++)
                {
                    int x = xs[ix];
                    int w = Convert.ToInt32(ix == 6 ? rtDays.Right : xs[ix + 1]) - x;
                    var rt = Util.FromRect(x, y, w, h);
                    dicBoxes.Add("rtBox_" + iy + "_" + ix, rt);

                    if (iy == 0) dicWeeks.Add("rtWeek_" + ix, Util.FromRect(x, rtWeekly.Top, w, rtWeekly.Height));
                }
            }

            act(rtContent, rtMonthly, rtWeekly, rtDays, rtMonthPrev, rtMonthNext, rtMonthText, dicBoxes, dicWeeks);
        }
        #endregion
        #endregion
    }
}
