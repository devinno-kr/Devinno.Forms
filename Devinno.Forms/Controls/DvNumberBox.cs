using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.Forms.Controls
{
    public class DvNumberBox : DvControl
    {
        #region Const 
        const int CHKTM = 500;
        #endregion

        #region Properties
        #region ButtonColor
        private Color cButtonColor = DvTheme.DefaultTheme.Color3;
        public Color ButtonColor
        {
            get => cButtonColor;
            set
            {
                if (cButtonColor != value)
                {
                    cButtonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ValueBoxColor
        private Color cValueColor = DvTheme.DefaultTheme.Color2;
        public Color ValueBoxColor
        {
            get => cValueColor;
            set
            {
                if (cValueColor != value)
                {
                    cValueColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Style
        private DvNumberBoxStyle eStyle = DvNumberBoxStyle.Right;
        public DvNumberBoxStyle Style
        {
            get => eStyle;
            set
            {
                if(eStyle != value)
                {
                    eStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Minimum
        private double nMinimum = 0D;
        public double Minimum
        {
            get => nMinimum;
            set
            {
                if (nMinimum != value)
                {
                    nMinimum = value;
                    if (nValue < nMinimum) nValue = nMinimum;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Maximum
        private double nMaximum = 100D;
        public double Maximum
        {
            get => nMaximum;
            set
            {
                if (nMaximum != value)
                {
                    nMaximum = value;
                    if (nValue > nMaximum) nValue = nMaximum;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Value
        private double nValue = 0D;
        public double Value
        {
            get => nValue;
            set
            {
                var v = MathTool.Constrain(value, Minimum, Maximum);
                if (nValue != v)
                {
                    nValue = v;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Tick
        public double Tick { get; set; } = 1D;
        #endregion
        #region FormatString
        private string sFormatString = null;
        public string FormatString
        {
            get => sFormatString;
            set
            {
                if (sFormatString != value)
                {
                    sFormatString = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member variable
        bool bMinusDown = false;
        bool bPlusDown = false;
        bool bValueDown = false;

        DateTime dtMinusDown = DateTime.Now;
        DateTime dtPlusDown = DateTime.Now;

        Thread th;

        int maxinterval = 250, mininterval = 10;
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        public event EventHandler ValueDoubleClick;
        public event EventHandler ValueClick;
        #endregion

        #region Constructor
        public DvNumberBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);

            #region Thread
            th = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    var tmm = (DateTime.Now - dtMinusDown).TotalMilliseconds;
                    var tmp = (DateTime.Now - dtPlusDown).TotalMilliseconds;

                    if (bMinusDown && tmm >= CHKTM)
                    {

                        var delay = (int)MathTool.Constrain(MathTool.Map(tmm, 2000, CHKTM, mininterval, maxinterval), mininterval, maxinterval);
                        this.Invoke(new Action(() => { Value = MathTool.Constrain(Value - Tick, Minimum, Maximum); }));
                        Thread.Sleep(delay);
                    }
                    else if (bPlusDown && tmp >= CHKTM)
                    {
                    
                        var delay = (int)MathTool.Constrain(MathTool.Map(tmp, 2000, CHKTM, mininterval, maxinterval), mininterval, maxinterval);
                        this.Invoke(new Action(() => { Value = MathTool.Constrain(Value + Tick, Minimum, Maximum); }));
                        Thread.Sleep(delay);
                    }
                    else Thread.Sleep(100);
                }
            }))
            { IsBackground = true };
            th.Start();
            #endregion
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            switch (Style)
            {
                case DvNumberBoxStyle.LeftRight:
                    {
                        var rtMinus = new Rectangle(rtContent.X, rtContent.Y, rtContent.Height, rtContent.Height);
                        var rtPlus = new Rectangle(rtContent.Right - rtContent.Height, rtContent.Y, rtContent.Height, rtContent.Height);
                        var rtValue = new Rectangle(rtContent.X + rtContent.Height, rtContent.Y, rtContent.Width - (rtContent.Height * 2), rtContent.Height);
                        SetArea("rtMinus", rtMinus);
                        SetArea("rtPlus", rtPlus);
                        SetArea("rtValue", rtValue);
                    } 
                    break;
                case DvNumberBoxStyle.Right:
                    {
                        var rtR = new Rectangle(rtContent.Right - rtContent.Height, rtContent.Y, rtContent.Height, rtContent.Height);

                        var rtPlus = new Rectangle(rtR.X, rtContent.Y, rtR.Width, rtR.Height / 2);
                        var rtMinus = new Rectangle(rtR.X, rtPlus.Bottom, rtR.Width, rtContent.Bottom - rtPlus.Bottom);
                        var rtValue = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width - rtContent.Height, rtContent.Height);
                        SetArea("rtMinus", rtMinus);
                        SetArea("rtPlus", rtPlus);
                        SetArea("rtValue", rtValue);
                    }
                    break;
                case DvNumberBoxStyle.UpDown:
                    {
                        int h = rtContent.Height / 3;
                        var rtPlus = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, h);
                        var rtMinus = new Rectangle(rtContent.X, rtContent.Bottom - h, rtContent.Width, h);
                        var rtValue = new Rectangle(rtContent.X, rtPlus.Bottom, rtContent.Width, rtMinus.Top - rtPlus.Bottom);
                        SetArea("rtMinus", rtMinus);
                        SetArea("rtPlus", rtPlus);
                        SetArea("rtValue", rtValue);
                    }
                    break;
            }
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var ButtonColor = UseThemeColor ? Theme.Color3 : this.ButtonColor;
            var ValueBoxColor = UseThemeColor ? Theme.Color2 : this.ButtonColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtTxt = Areas["rtValue"];
            var rtP = Areas["rtPlus"];
            var rtM = Areas["rtMinus"];
            #endregion
            #region Init
            var p = new Pen(ButtonColor, 1);
            var br = new SolidBrush(ButtonColor);
            #endregion
            #region Draw
            
            switch(Style)
            {
                case DvNumberBoxStyle.LeftRight:
                    {
                        int isz = rtM.Height / 4;
                        #region Back
                        Theme.DrawBox(e.Graphics, ValueBoxColor, BackColor, rtTxt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        #endregion
                        #region Plus
                        if (!bPlusDown)
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 0, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.R, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 1, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                        #region Minus
                        if (!bMinusDown)
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 0, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 1, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                    }
                    break;

                case DvNumberBoxStyle.Right:
                    {
                        int isz = rtM.Height / 4;
                        #region Back
                        Theme.DrawBox(e.Graphics, ValueBoxColor, BackColor, rtTxt, RoundType.L, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        #endregion
                        #region Plus
                        if (!bPlusDown)
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.RT, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 0, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.RT, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 1, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                        #region Minus
                        if (!bMinusDown)
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 0, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.RB, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 1, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                    }
                    break;

                case DvNumberBoxStyle.UpDown:
                    {
                        int isz = rtM.Height / 4;
                        #region Back
                        Theme.DrawBox(e.Graphics, ValueBoxColor, BackColor, rtTxt, RoundType.NONE, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        #endregion
                        #region Plus
                        if (!bPlusDown)
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 0, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-plus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtP, RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtP.X + 1, rtP.Y + 1, rtP.Width, rtP.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                        #region Minus
                        if (!bMinusDown)
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor;
                            var ct = ForeColor;
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.B, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 0, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var ico = new DvIcon("fa-minus") { IconSize = isz };
                            var cv = ButtonColor.BrightnessTransmit(Theme.DownBright);
                            var ct = ForeColor.BrightnessTransmit(Theme.DownBright);
                            Theme.DrawBox(e.Graphics, cv, BackColor, rtM, RoundType.B, BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW | BoxDrawOption.GRADIENT_V_REVERSE);
                            Theme.DrawTextShadow(e.Graphics, ico, null, Font, ct, cv, new Rectangle(rtM.X + 1, rtM.Y + 1, rtM.Width, rtM.Height), DvContentAlignment.MiddleCenter);
                        }
                        #endregion
                    }
                    break;

            }

            var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);
            Theme.DrawTextShadow(e.Graphics, null, s, Font, ForeColor, ValueBoxColor, rtTxt, DvContentAlignment.MiddleCenter);

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
            if (Areas.ContainsKey("rtMinus") && CollisionTool.Check(Areas["rtMinus"], e.Location)) { bMinusDown = true; dtMinusDown = DateTime.Now; Invalidate(); }
            if (Areas.ContainsKey("rtPlus") && CollisionTool.Check(Areas["rtPlus"], e.Location)) { bPlusDown = true; dtPlusDown = DateTime.Now; Invalidate(); }
            if (Areas.ContainsKey("rtValue") && CollisionTool.Check(Areas["rtValue"], e.Location)) {bValueDown = true; Invalidate(); }

            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (bMinusDown)
            {
                bMinusDown = false;
                if ((DateTime.Now - dtMinusDown).TotalMilliseconds < CHKTM) Value = MathTool.Constrain(Value - Tick, Minimum, Maximum);
                Invalidate();
            }
            if (bPlusDown)
            {
                bPlusDown = false;
                if ((DateTime.Now - dtPlusDown).TotalMilliseconds < CHKTM) Value = MathTool.Constrain(Value + Tick, Minimum, Maximum);
                Invalidate();
            }
            if(bValueDown)
            {
                bValueDown = false;
                if (Areas.ContainsKey("rtValue") && CollisionTool.Check(Areas["rtValue"], e.Location)) ValueClick?.Invoke(this, null);
            }
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtValue") && CollisionTool.Check(Areas["rtValue"], e.Location)) ValueDoubleClick?.Invoke(this, null);
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion
    }

    #region enum : DvNumberBoxStyle
    public enum DvNumberBoxStyle { UpDown, LeftRight, Right};
    #endregion
}
