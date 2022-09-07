using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvNumberBox : DvControl
    {
        #region Const 
        const int CHKTM = 500;
        #endregion

        #region Properties
        #region ButtonColor
        private Color? cButtonColor = null;
        public Color? ButtonColor
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
        private Color? cValueBoxColor = null;
        public Color? ValueBoxColor
        {
            get => cValueBoxColor;
            set
            {
                if (cValueBoxColor != value)
                {
                    cValueBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Style
        private DvNumberBoxStyle eStyle = DvNumberBoxStyle.RightUpDown;
        public DvNumberBoxStyle Style
        {
            get => eStyle;
            set
            {
                if (eStyle != value)
                {
                    eStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ButtonSize
        private int nButtonSize = 30;
        public int ButtonSize
        {
            get => nButtonSize;
            set
            {
                if (nButtonSize != value)
                {
                    nButtonSize = value;
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
                if (nValue != value)
                {
                    nValue = value;
                    ValueChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
            }
        }
        #endregion
        #region Tick
        private double nTick = 1D;
        public double Tick
        {
            get => nTick;
            set
            {
                if (nTick != value)
                {
                    nTick = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region FormatString
        private string sFormatString = "0";
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

        #region Gradient
        private bool bGradient = true;
        public bool Gradient
        {
            get => bGradient;
            set
            {
                if (bGradient != value)
                {
                    bGradient = value;
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
        #endregion

        #region Member Variable
        int maxinterval = 250, mininterval = 10;

        bool bPlusDown = false;
        bool bMinusDown = false;
        bool bValueDown = false;
        DateTime dtMinusDown;
        DateTime dtPlusDown;
        #endregion

        #region Event
        public event EventHandler ValueChanged;
        public event EventHandler ValueClick;
        public event EventHandler ValueDoubleClick;
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
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var ValueBoxColor = this.ValueBoxColor ?? Theme.LabelColor;
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var BorderColor = Theme.GetBorderColor(ValueBoxColor, BackColor);

            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtPlus, rtMinus, rtValue) =>
            {
                #region Bounds
                var rtV = rtValue;
                var rtP = rtPlus;
                var rtM = rtMinus;
                var rtPI = Util.FromRect(rtPlus);
                var rtMI = Util.FromRect(rtMinus);

                if (bPlusDown) rtPI.Offset(0, 1);
                if (bMinusDown) rtMI.Offset(0, 1);
                #endregion
                #region Color
                var cvP = !bPlusDown ? ButtonColor : ButtonColor.BrightnessTransmit(Theme.DownBrightness);
                var ctP = !bPlusDown ? ForeColor : ForeColor.BrightnessTransmit(Theme.DownBrightness);
                var cvM = !bMinusDown ? ButtonColor : ButtonColor.BrightnessTransmit(Theme.DownBrightness);
                var ctM = !bMinusDown ? ForeColor : ForeColor.BrightnessTransmit(Theme.DownBrightness);
                #endregion
                #region Icon / Rounds
                var isz = Convert.ToInt32(Math.Max(Math.Min(rtM.Height, rtM.Width) / 3F, 5));
                var icoP = "";
                var icoM = "";

                var rndP = RoundType.Rect;
                var rndM = RoundType.Rect;
                var rndV = RoundType.Rect;
                switch (Style)
                {
                    case DvNumberBoxStyle.LeftRight:
                        {
                            icoP = "fa-plus";
                            icoM = "fa-minus";

                            switch (Round)
                            {
                                #region Ellipse / Rect
                                case RoundType.Ellipse:
                                case RoundType.Rect:
                                    rndV = rndP = rndM = RoundType.Rect;
                                    break;
                                #endregion

                                #region L / T / R / B
                                case RoundType.L:
                                    rndM = RoundType.L;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    break;
                                case RoundType.R:
                                    rndM = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.R;
                                    break;
                                case RoundType.T:
                                    rndM = RoundType.LT;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RT;
                                    break;
                                case RoundType.B:
                                    rndM = RoundType.LB;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RB;
                                    break;
                                #endregion

                                #region LT / RT / LB / RB
                                case RoundType.LT:
                                    rndM = RoundType.LT;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    break;
                                case RoundType.RT:
                                    rndM = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RT;
                                    break;
                                case RoundType.LB:
                                    rndM = RoundType.LB;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    break;
                                case RoundType.RB:
                                    rndM = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RB;
                                    break;
                                #endregion

                                #region All
                                case RoundType.All:
                                    rndM = RoundType.L;
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.R;
                                    break;
                                    #endregion
                            }
                        }
                        break;

                    case DvNumberBoxStyle.Right:
                        {
                            icoP = "fa-plus";
                            icoM = "fa-minus";

                            switch (Round)
                            {
                                #region Ellipse / Rect
                                case RoundType.Ellipse:
                                case RoundType.Rect:
                                    rndV = rndP = rndM = RoundType.Rect;
                                    break;
                                #endregion

                                #region L / T / R / B
                                case RoundType.L:
                                    rndV = RoundType.L;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.R:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.R;
                                    break;
                                case RoundType.T:
                                    rndV = RoundType.LT;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RT;
                                    break;
                                case RoundType.B:
                                    rndV = RoundType.LB;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                #endregion

                                #region LT / RT / LB / RB
                                case RoundType.LT:
                                    rndV = RoundType.LT;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.RT:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RT;
                                    break;
                                case RoundType.LB:
                                    rndV = RoundType.LB;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.RB:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                #endregion

                                #region All
                                case RoundType.All:
                                    rndV = RoundType.L;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.R;
                                    break;
                                    #endregion
                            }
                        }
                        break;

                    case DvNumberBoxStyle.RightUpDown:
                        {
                            icoP = "fa-chevron-up";
                            icoM = "fa-chevron-down";

                            switch (Round)
                            {
                                #region Ellipse / Rect
                                case RoundType.Ellipse:
                                case RoundType.Rect:
                                    rndV = rndP = rndM = RoundType.Rect;
                                    break;
                                #endregion

                                #region L / T / R / B
                                case RoundType.L:
                                    rndV = RoundType.L;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.R:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RT;
                                    rndM = RoundType.RB;
                                    break;
                                case RoundType.T:
                                    rndV = RoundType.LT;
                                    rndP = RoundType.RT;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.B:
                                    rndV = RoundType.LB;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                #endregion

                                #region LT / RT / LB / RB
                                case RoundType.LT:
                                    rndV = RoundType.LT;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.RT:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.RT;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.LB:
                                    rndV = RoundType.LB;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.RB:
                                    rndV = RoundType.Rect;
                                    rndP = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                #endregion

                                #region All
                                case RoundType.All:
                                    rndV = RoundType.L;
                                    rndP = RoundType.RT;
                                    rndM = RoundType.RB;
                                    break;
                                    #endregion
                            }
                        }
                        break;

                    case DvNumberBoxStyle.UpDown:
                        {
                            icoP = "fa-chevron-up";
                            icoM = "fa-chevron-down";

                            switch (Round)
                            {
                                #region Ellipse / Rect
                                case RoundType.Ellipse:
                                case RoundType.Rect:
                                    rndV = rndP = rndM = RoundType.Rect;
                                    break;
                                #endregion

                                #region L / T / R / B
                                case RoundType.L:
                                    rndP = RoundType.LT;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.LB;
                                    break;
                                case RoundType.R:
                                    rndP = RoundType.RT;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                case RoundType.T:
                                    rndP = RoundType.T;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.B:
                                    rndP = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.B;
                                    break;
                                #endregion

                                #region LT / RT / LB / RB
                                case RoundType.LT:
                                    rndP = RoundType.LT;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.RT:
                                    rndP = RoundType.RT;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.Rect;
                                    break;
                                case RoundType.LB:
                                    rndP = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.LB;
                                    break;
                                case RoundType.RB:
                                    rndP = RoundType.Rect;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.RB;
                                    break;
                                #endregion

                                #region All
                                case RoundType.All:
                                    rndP = RoundType.T;
                                    rndV = RoundType.Rect;
                                    rndM = RoundType.B;
                                    break;
                                    #endregion
                            }
                        }
                        break;

                }
                #endregion

                var s = string.IsNullOrWhiteSpace(FormatString) ? Value.ToString() : Value.ToString(FormatString);
                Theme.DrawBox(e.Graphics, rtV, ValueBoxColor, BorderColor, rndV, Box.LabelBox(Embossing.FlatConcave, ShadowGap));
                Theme.DrawText(e.Graphics, s, Font, ForeColor, rtV);

                Theme.DrawBox(e.Graphics, rtP, cvP, BorderColor, rndP, bPlusDown ? Box.ButtonDown(ShadowGap) : Box.ButtonUp_V(Gradient, ShadowGap));
                Theme.DrawIcon(e.Graphics, new Icons.DvIcon(icoP, isz), ctP, rtPI);

                Theme.DrawBox(e.Graphics, rtM, cvM, BorderColor, rndM, bMinusDown ? Box.ButtonDown(ShadowGap) : Box.ButtonUp_V(Gradient, ShadowGap));
                Theme.DrawIcon(e.Graphics, new Icons.DvIcon(icoM, isz), ctM, rtMI);

            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtPlus, rtMinus, rtValue) =>
            {
                if (CollisionTool.Check(rtMinus, e.X, e.Y))
                {
                    bMinusDown = true; dtMinusDown = DateTime.Now;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        while (bMinusDown)
                        {
                            var tmm = (DateTime.Now - dtMinusDown).TotalMilliseconds;

                            if (bMinusDown && tmm >= CHKTM)
                            {
                                var delay = (int)MathTool.Constrain(MathTool.Map(tmm, 2000, CHKTM, mininterval, maxinterval), mininterval, maxinterval);
                                this.Invoke(new Action(() => Value = MathTool.Constrain(Value - Tick, Minimum, Maximum)));
                                Thread.Sleep(delay);
                            }
                            else Thread.Sleep(100);
                        }
                    });
                }
                if (CollisionTool.Check(rtPlus, e.X, e.Y))
                {
                    bPlusDown = true; dtPlusDown = DateTime.Now;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        while (bPlusDown | bMinusDown)
                        {
                            var tmp = (DateTime.Now - dtPlusDown).TotalMilliseconds;

                            if (bPlusDown && tmp >= CHKTM)
                            {
                                var delay = (int)MathTool.Constrain(MathTool.Map(tmp, 2000, CHKTM, mininterval, maxinterval), mininterval, maxinterval);
                                this.Invoke(new Action(() => Value = MathTool.Constrain(Value + Tick, Minimum, Maximum)));
                                Thread.Sleep(delay);
                            }
                            else Thread.Sleep(100);
                        }
                    });
                }
                if (CollisionTool.Check(rtValue, e.X, e.Y)) { bValueDown = true; };
                
                Invalidate();
            });

            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtPlus, rtMinus, rtValue) =>
            {
                if (bMinusDown)
                {
                    bMinusDown = false;
                    if ((DateTime.Now - dtMinusDown).TotalMilliseconds < CHKTM) Value = MathTool.Constrain(Value - Tick, Minimum, Maximum);
                }
                if (bPlusDown)
                {
                    bPlusDown = false;
                    if ((DateTime.Now - dtPlusDown).TotalMilliseconds < CHKTM) Value = MathTool.Constrain(Value + Tick, Minimum, Maximum);
                }
                if (bValueDown)
                {
                    bValueDown = false;
                    if (CollisionTool.Check(rtValue, e.X, e.Y)) ValueClick?.Invoke(this, null);
                }

                Invalidate();
            });

            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            Areas((rtContent, rtPlus, rtMinus, rtValue) =>
            {
                if (CollisionTool.Check(rtValue, e.X, e.Y)) { ValueDoubleClick?.Invoke(this, new EventArgs()); }

            });
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtPlus = new RectangleF();
            var rtMinus = new RectangleF();
            var rtValue = new RectangleF();

            switch (Style)
            {
                case DvNumberBoxStyle.LeftRight:
                    {
                        var n = rtContent.Width / 3F;
                        var ButtonSize = Convert.ToInt32(Math.Min(n, this.ButtonSize));

                        rtMinus = Util.FromRect(rtContent.Left, rtContent.Top, ButtonSize, rtContent.Height);
                        rtPlus = Util.FromRect(rtContent.Right - ButtonSize, rtContent.Top, ButtonSize, rtContent.Height);
                        rtValue = Util.FromRect(rtContent.Left + ButtonSize, rtContent.Top, rtContent.Width - (ButtonSize * 2), rtContent.Height);
                    }
                    break;
                case DvNumberBoxStyle.RightUpDown:
                    {
                        var n = rtContent.Width / 2F;
                        var ButtonSize = Convert.ToInt32(Math.Min(n, this.ButtonSize));

                        var rtR = Util.FromRect(rtContent.Right - ButtonSize, rtContent.Top, ButtonSize, rtContent.Height);

                        rtPlus = Util.FromRect(rtR.Left, rtContent.Top, rtR.Width, rtR.Height / 2);
                        rtMinus = Util.FromRect(rtR.Left, rtPlus.Bottom, rtR.Width, rtContent.Bottom - rtPlus.Bottom);
                        rtValue = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - ButtonSize, rtContent.Height);
                    }
                    break;
                case DvNumberBoxStyle.UpDown:
                    {
                        var n = rtContent.Height / 3F;
                        var ButtonSize = Convert.ToInt32(Math.Min(n, this.ButtonSize));

                        rtPlus = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, ButtonSize);
                        rtMinus = Util.FromRect(rtContent.Left, rtContent.Bottom - ButtonSize, rtContent.Width, ButtonSize);
                        rtValue = Util.FromRect(rtContent.Left, rtPlus.Bottom, rtContent.Width, rtMinus.Top - rtPlus.Bottom);
                    }
                    break;
                case DvNumberBoxStyle.Right:
                    {
                        var n = rtContent.Width / 3F;
                        var ButtonSize = Convert.ToInt32(Math.Min(n, this.ButtonSize));

                        rtValue = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - (ButtonSize * 2), rtContent.Height);
                        rtPlus = Util.FromRect(rtValue.Right, rtContent.Top, ButtonSize, rtContent.Height);
                        rtMinus = Util.FromRect(rtPlus.Right, rtContent.Top, ButtonSize, rtContent.Height);
                    }
                    break;
            }

            act(rtContent, rtPlus, rtMinus, rtValue);
        }
        #endregion
        #endregion
    }
}
