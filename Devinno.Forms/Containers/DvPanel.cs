using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Controls;
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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Containers
{
    public class DvPanel : DvContainer
    {
        #region Properties
        #region Text / Icon
        private TextIcon texticon = new TextIcon();

        public DvIcon Icon => texticon.Icon;
        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap IconImage
        {
            get => texticon.IconImage;
            set { if (texticon.IconImage != value) { texticon.IconImage = value; Invalidate(); } }
        }
        public string IconString
        {
            get => texticon.IconString;
            set { if (texticon.IconString != value) { texticon.IconString = value; Invalidate(); } }
        }
        public float IconSize
        {
            get => texticon.IconSize;
            set { if (texticon.IconSize != value) { texticon.IconSize = value; Invalidate(); } }
        }
        public int IconGap
        {
            get => texticon.IconGap;
            set { if (texticon.IconGap != value) { texticon.IconGap = value; Invalidate(); } }
        }
        public DvTextIconAlignment IconAlignment
        {
            get => texticon.IconAlignment;
            set { if (texticon.IconAlignment != value) { texticon.IconAlignment = value; Invalidate(); } }
        }

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public override string Text
        {
            get => texticon.Text;
            set { if (texticon.Text != value) { base.Text = texticon.Text = value; Invalidate(); } }
        }

        public Padding TextPadding
        {
            get => texticon.TextPadding;
            set { if (texticon.TextPadding != value) { texticon.TextPadding = value; Invalidate(); } }
        }
        #endregion
        #region DrawTitle
        private bool bDrawTitle = true;
        public bool DrawTitle
        {
            get => bDrawTitle;
            set
            {
                if (bDrawTitle != value)
                {
                    bDrawTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleHeight
        private int nTitleHeight = 30;
        public int TitleHeight
        {
            get => nTitleHeight;
            set
            {
                if (nTitleHeight != value)
                {
                    nTitleHeight = value;
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
        #region PanelColor
        private Color? cPanelColor = null;
        public Color? PanelColor
        {
            get => cPanelColor;
            set
            {
                if (cPanelColor != value)
                {
                    cPanelColor = value;
                    if (cPanelColor.HasValue) BackColor = cPanelColor.Value;
                    else BackColor = cPanelColor ?? GetTheme().PanelColor;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Buttons
        public EventList<ButtonInfo> Buttons { get; private set; } = new EventList<ButtonInfo>();
        public int? ButtonsWidth { get; set; } = null;
        #endregion
        #region Events
        public event EventHandler<ButtonsClickventArgs> ButtonClick;
        #endregion
        #endregion

        #region Constructor
        public DvPanel()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Padding = new Padding(0, nTitleHeight, 0, 0);
            Size = new Size(150, 100);

            Buttons.Changed += (o, s) => { foreach (var v in Buttons) v.control = this; };
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BackColor = Parent.BackColor;
            var PanelColor = this.PanelColor ?? Theme.PanelColor;
            var BorderColor = Theme.GetBorderColor(PanelColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            this.BackColor = PanelColor;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            #endregion

            e.Graphics.Clear(BackColor);

            Areas((rtContent, rtPanel, rtTitle, rtText, rtButtons) => {

                Theme.DrawBox(e.Graphics, rtContent, PanelColor, BorderColor, Round, Box.Style(Fill.Fill, Embossing.Convex, ShadowGap, true));

                if (DrawTitle)
                {
                    #region Gradient
                    var rte = rtTitle;
                    rte.Inflate(1, 1);
                    using (var lgbr = new LinearGradientBrush(rte, Color.FromArgb(Theme.GradientLightAlpha, Color.White), Color.FromArgb(0, Color.White), 90))
                    {
                        e.Graphics.FillRoundRectangleT(lgbr, rtTitle, Corner);
                    }
                    #endregion
                    #region Line
                    p.Width = 1;
                    p.Color = Color.FromArgb(Theme.OutBevelAlpha, Color.White);
                    e.Graphics.DrawLine(p, rtContent.Left + 5, rtTitle.Bottom + 1, rtContent.Right - 5, rtTitle.Bottom + 1);

                    p.Color = BorderColor;
                    e.Graphics.DrawLine(p, rtContent.Left + 5, rtTitle.Bottom, rtContent.Right - 5, rtTitle.Bottom);
                    #endregion
                    #region Text
                    Theme.DrawTextIcon(e.Graphics, texticon, Font, ForeColor, rtText, DvContentAlignment.MiddleLeft);
                    #endregion
                    #region Buttons
                    if (Buttons.Count > 0 && ButtonsWidth.HasValue && DrawTitle)
                    {
                        Areas2(rtButtons, (btns) =>
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
                                #endregion
                                #region Color
                                var cV = btn.Button.Checked ? Theme.PointColor : Theme.ButtonColor;
                                var cB = bDown ? BorderColor.BrightnessTransmit(Theme.DownBrightness) : BorderColor;
                                var cF = bDown ? cV.BrightnessTransmit(Theme.DownBrightness) : cV;
                                var cT = bDown ? ForeColor.BrightnessTransmit(Theme.DownBrightness) : ForeColor;
                                var cL = Util.FromArgb(Theme.OutBevelAlpha, Color.White);
                                #endregion

                                #region DrawBox
                                if (!bDown) Theme.DrawBox(e.Graphics, btn.Bounds, cF, cB, rnd, Box.ButtonUp_V(true, ShadowGap), Corner);
                                else Theme.DrawBox(e.Graphics, btn.Bounds, cF, cB, rnd, Box.ButtonDown(ShadowGap), Corner);
                                #endregion
                                #region DrawText
                                if (bDown) rtText.Offset(0, 1);
                                Theme.DrawTextIcon(e.Graphics, btn.Button, Font, cT, rtText, DvContentAlignment.MiddleCenter);
                                #endregion
                            }
                        });
                    }
                    #endregion
                }

            });

            #region Dispose
            p.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtPanel, rtTitle, rtText, rtButtons) =>
            {
                if (Buttons.Count > 0 && ButtonsWidth.HasValue && DrawTitle)
                {
                    Areas2(rtButtons, (btns) =>
                    {
                        var ls = btns.ToList();
                        foreach (var btn in btns)
                        {
                            if (CollisionTool.Check(btn.Bounds, e.Location)) 
                                btn.Button.DownState = true;
                        }
                    });
                }
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtPanel, rtTitle, rtText, rtButtons) =>
            {
                if (Buttons.Count > 0 && ButtonsWidth.HasValue && DrawTitle)
                {
                    Areas2(rtButtons, (btns) =>
                    {
                        var ls = btns.ToList();
                        foreach (var btn in btns)
                        {
                            if (btn.Button.DownState)
                            {
                                if (CollisionTool.Check(btn.Bounds, e.Location))
                                {
                                    ButtonClick?.Invoke(this, new ButtonsClickventArgs(btn.Button));
                                }

                                btn.Button.DownState = false;
                            }
                        }
                    });
                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtTitle = Util.FromRect(rtContent.Left + 1, rtContent.Top, rtContent.Width - 2, TitleHeight);
            var rtPanel = rtContent;
            var rtText = Util.FromRect(rtTitle.Left + TextPadding.Left, rtTitle.Top + TextPadding.Top, rtTitle.Width - (TextPadding.Left + TextPadding.Right), rtTitle.Height - (TextPadding.Top + TextPadding.Bottom));
            var rtButtons = Util.FromRect(rtTitle.Right - 5 - (ButtonsWidth ?? 0), rtTitle.Top + 5, (ButtonsWidth ?? 0), rtTitle.Height - 10);

            if (DrawTitle)
            {
                rtTitle = Util.FromRect(rtContent.Left + 1, rtContent.Top, rtContent.Width - 2, TitleHeight);
                rtPanel = Util.FromRect(rtContent.Left, rtTitle.Bottom, rtContent.Width, rtContent.Height - rtTitle.Bottom);
                rtText = Util.FromRect(rtTitle.Left + 5, rtTitle.Top, rtTitle.Width - 5, rtTitle.Height);
            }

            act(rtContent, rtPanel, rtTitle, rtText, rtButtons);
        }

        internal void Areas2(RectangleF rtButtons, Action<ItemBTN[]> act)
        {
            var rects = Util.DevideSizeH(rtButtons, Buttons.Select(x => x.Size).ToList());
            var items = Buttons.Select(x => new ItemBTN { Button = x }).ToArray();
            for (int i = 0; i < Buttons.Count; i++) items[i].Bounds = rects[i];

            act(items);
        }
        #endregion
        #endregion
    }
}
