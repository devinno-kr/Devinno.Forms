using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvButtons : DvControl
    {
        #region Properties
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
        #region BackgroundDraw
        private bool bBackgroundDraw = true;
        public bool BackgroundDraw
        {
            get => bBackgroundDraw;
            set
            {
                if (bBackgroundDraw != value)
                {
                    bBackgroundDraw = value;
                    Invalidate();
                }
            }
        }
        #endregion
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
        #region CheckdButtonColor
        private Color? cCheckdButtonColor = null;
        public Color? CheckdButtonColor
        {
            get => cCheckdButtonColor;
            set
            {
                if (cCheckdButtonColor != value)
                {
                    cCheckdButtonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Direction
        private DvDirectionHV eDirection = DvDirectionHV.Horizon;
        public DvDirectionHV Direction
        {
            get => eDirection;
            set
            {
                if(eDirection != value)
                {
                    eDirection = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectionMode
        public bool SelectionMode { get; set; } = false;
        #endregion

        #region Buttons
        public EventList<ButtonInfo> Buttons { get; private set; } = new EventList<ButtonInfo>();
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
        #region Clickable
        public bool Clickable { get; set; } = true;
        #endregion
        #endregion

        #region Member Variable

        #endregion

        #region Events
        public event EventHandler<ButtonsClickventArgs> ButtonClick;
        public event EventHandler<ButtonsSelectedventArgs> SelectedChanged;
        #endregion

        #region Constructor
        public DvButtons()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Buttons.Changed += (o, s) => { foreach (var v in Buttons) v.btns = this; };
            Size = new Size(150, 30);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var ButtonColor = this.ButtonColor ?? Theme.ButtonColor;
            var CheckButtonColor = this.CheckdButtonColor ?? Theme.PointColor;
            var BorderColor = Theme.GetBorderColor(ButtonColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            if (Buttons.Count > 0)
            {
                Areas((rtContent) =>
                {
                    Areas2(rtContent, (btns) =>
                    {
                        var ls = btns.ToList();
                        foreach (var btn in btns)
                        {
                            #region Var
                            var i = ls.IndexOf(btn);
                            var rtText = btn.Bounds;
                            var bDown = btn.Button.DownState;
                            var rnd = Round;
                            #endregion
                            #region Round
                            var first = i == 0;
                            var last = i == ls.Count - 1;
                            if (Direction == DvDirectionHV.Horizon)
                            {
                                switch (Round)
                                {
                                    #region Ellipse / Rect
                                    case RoundType.Ellipse:
                                    case RoundType.Rect:
                                        rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region L / T / R / B
                                    case RoundType.L:
                                        if (first) rnd = RoundType.L;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.R:
                                        if (last) rnd = RoundType.R;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.T:
                                        if (first) rnd = RoundType.LT;
                                        else if (last) rnd = RoundType.RT;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.B:
                                        if (first) rnd = RoundType.LB;
                                        else if (last) rnd = RoundType.RB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region LT / RT / LB / RB
                                    case RoundType.LT:
                                        if (first) rnd = RoundType.LT;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.RT:
                                        if (last) rnd = RoundType.RT;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.LB:
                                        if (first) rnd = RoundType.LB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.RB:
                                        if (last) rnd = RoundType.RB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region All
                                    case RoundType.All:
                                        if (first) rnd = RoundType.L;
                                        else if (last) rnd = RoundType.R;
                                        else rnd = RoundType.Rect;
                                        break;
                                        #endregion
                                }
                            }
                            else if (Direction == DvDirectionHV.Vertical)
                            {
                                switch (Round)
                                {
                                    #region Ellipse / Rect
                                    case RoundType.Ellipse:
                                    case RoundType.Rect:
                                        rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region L / T / R / B
                                    case RoundType.L:
                                        if (first) rnd = RoundType.LT;
                                        else if (last) rnd = RoundType.LB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.R:
                                        if (first) rnd = RoundType.RT;
                                        else if (last) rnd = RoundType.RB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.T:
                                        if (first) rnd = RoundType.T;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.B:
                                        if (last) rnd = RoundType.B;
                                        else rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region LT / RT / LB / RB
                                    case RoundType.LT:
                                        if (first) rnd = RoundType.LT;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.RT:
                                        if (first) rnd = RoundType.RT;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.LB:
                                        if (last) rnd = RoundType.LB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    case RoundType.RB:
                                        if (last) rnd = RoundType.RB;
                                        else rnd = RoundType.Rect;
                                        break;
                                    #endregion

                                    #region All
                                    case RoundType.All:
                                        if (first) rnd = RoundType.T;
                                        else if (last) rnd = RoundType.B;
                                        else rnd = RoundType.Rect;
                                        break;
                                        #endregion
                                }
                            }
                            #endregion
                            #region Color
                            var cV = btn.Button.Checked ? CheckButtonColor : ButtonColor;
                            var cB = bDown ? BorderColor.BrightnessTransmit(Theme.DownBrightness) : BorderColor;
                            var cF = bDown ? cV.BrightnessTransmit(Theme.DownBrightness) : cV;
                            var cT = bDown ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                            var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);
                            #endregion

                            #region DrawBox
                            if (BackgroundDraw)
                            {
                                var c = cF;

                                if (!bDown)
                                {
                                    if (Direction == DvDirectionHV.Horizon) Theme.DrawBox(e.Graphics, btn.Bounds, c, cB, rnd, Box.ButtonUp_V(Gradient, ShadowGap), Corner);
                                    else Theme.DrawBox(e.Graphics, btn.Bounds, c, cB, rnd, Box.ButtonUp_H(Gradient, ShadowGap), Corner);
                                }
                                else Theme.DrawBox(e.Graphics, btn.Bounds, c, cB, rnd, Box.ButtonDown(ShadowGap), Corner);
                            }
                            else
                            {
                                if (btn.Button.Checked)
                                {
                                    var c = CheckButtonColor;
                                    var c2 = BorderColor;

                                    var sz = e.Graphics.MeasureTextIcon(btn.Button.Icon, btn.Button.Text, Font);
                                    var rt = Util.MakeRectangleAlign(Util.FromRect(btn.Bounds, btn.Button.TextPadding), sz, ContentAlignment);
                                    rt.Inflate(7, 7);

                                    Theme.DrawBox(e.Graphics, rt, c, c2, RoundType.All, Box.FlatBox(true), Corner);
                                }
                            }
                            #endregion
                            #region DrawText
                            if (bDown) rtText.Offset(0, 1);
                            Theme.DrawTextIcon(e.Graphics, btn.Button, Font, cT, rtText, ContentAlignment);
                            #endregion
                        }
                    });
                });
            }
            else
            {
                Areas((rtContent) =>
                {
                    Theme.DrawBox(e.Graphics, rtContent, ButtonColor, BorderColor, Round, Box.ButtonUp_V(Gradient, ShadowGap), Corner);
                });
            }
            
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent) =>
            {
                Areas2(rtContent, (btns) =>
                {
                    var ls = btns.ToList();
                    foreach (var btn in btns)
                    {
                        if (CollisionTool.Check(btn.Bounds, e.Location)) btn.Button.DownState = true;
                    }
                });
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent) =>
            {
                Areas2(rtContent, (btns) =>
                {
                    var ls = btns.ToList();
                    foreach (var btn in btns)
                    {
                        if (btn.Button.DownState)
                        {
                            if (CollisionTool.Check(btn.Bounds, e.Location))
                            {
                                ButtonClick?.Invoke(this, new ButtonsClickventArgs(btn.Button));

                                if (SelectionMode)
                                {
                                    if (!btn.Button.Checked)
                                    {
                                        foreach (var v in Buttons.Where(x => x != btn.Button)) v.Checked = false;
                                        btn.Button.Checked = true;

                                        SelectedChanged?.Invoke(this, new ButtonsSelectedventArgs(btn.Button));
                                    }
                                }
                            }

                            btn.Button.DownState = false;
                        }
                    }
                });
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtContent )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF> act)
        {
            var rtContent = GetContentBounds();

            act(rtContent);
        }

        internal void Areas2(RectangleF rtContent, Action<ItemBTN[]> act)
        {
            if (Direction == DvDirectionHV.Horizon)
            {
                var rects = Util.DevideSizeH(rtContent, Buttons.Select(x => x.Size).ToList());
                var items = Buttons.Select(x => new ItemBTN { Button = x }).ToArray();
                for (int i = 0; i < Buttons.Count; i++)
                    items[i].Bounds = rects[i];

                act(items);
            }
            else if(Direction == DvDirectionHV.Vertical) 
            {
                var rects = Util.DevideSizeV(rtContent, Buttons.Select(x => x.Size).ToList());
                var items = Buttons.Select(x => new ItemBTN { Button = x }).ToArray();
                for (int i = 0; i < Buttons.Count; i++)
                    items[i].Bounds = rects[i];

                act(items);
            }
        }
        #endregion
        #endregion
    }

    #region class : ButtonInfo
    public class ButtonInfo : TextIcon
    {
        #region Properties
        #region Name
        public string Name { get; set; }
        #endregion
        #region Size
        public SizeInfo Size { get; set; } = new SizeInfo(DvSizeMode.Percent, 100);
        #endregion
        #region Checked
        private bool bCheck = false;
        public bool Checked
        {
            get => bCheck;
            set
            {
                if (bCheck != value)
                {
                    bCheck = value;
                    btns?.Invalidate();
                }
            }
        }
        #endregion
        #region DownState
        internal bool DownState { get; set; }
        #endregion
        #endregion

        #region Member Variable
        internal DvButtons btns;
        #endregion

        public ButtonInfo(string Name)
        {
            this.Name = Name;
        }
    }

    internal class ItemBTN
    {
        public ButtonInfo Button { get; set; }
        public RectangleF Bounds { get; set; }
    }
    #endregion
}
