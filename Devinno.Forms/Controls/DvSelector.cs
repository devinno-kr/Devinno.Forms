using Devinno.Extensions;
using Devinno.Forms.Dialogs;
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
    public class DvSelector : DvControl
    {
        #region Properties
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
        #region SelectorColor
        private Color? cSelectorColor = null;
        public Color? SelectorColor
        {
            get => cSelectorColor;
            set
            {
                if (cSelectorColor != value)
                {
                    cSelectorColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Items
        public List<TextIcon> Items { get; } = new List<TextIcon>();
        #endregion
        #region SelectedIndex
        private int nSelectedIndex = -1;
        public int SelectedIndex
        {
            get => nSelectedIndex;
            set
            {
                if (nSelectedIndex != value)
                {
                    nSelectedIndex = value;
                    SelectedIndexChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #region ButtonWidth
        private int? nButtonWidth = null;
        public int? ButtonWidth
        {
            get => nButtonWidth;
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Style(Emboss)
        private Embossing eEmboss = Embossing.Convex;
        public Embossing Style
        {
            get => eEmboss;
            set
            {
                if (eEmboss != value)
                {
                    eEmboss = value;
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

        #region Animation
        private bool Animation => GetTheme()?.Animation ?? false;
        #endregion
        #endregion

        #region Member Variable
        private Animation ani = new Animation();

        private bool bLeft = false;
        private bool bRight = false;
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Constructor
        public DvSelector()
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
            var SelectorColor = this.SelectorColor ?? Theme.ButtonColor;
            var BorderColor = Theme.GetBorderColor(SelectorColor, BackColor);
            var Corner = Theme.Corner;
            var Round = this.Round ?? RoundType.All;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion

            Areas((rtContent, rtLeft, rtRight, rtText, rtTextP, rtTextN) =>
            {
                #region Init
                var p = new Pen(Color.Black);
                var br = new SolidBrush(Color.Black);
                #endregion
                #region Background
                if (BackgroundDraw) Theme.DrawBox(e.Graphics, rtContent, SelectorColor, BorderColor, Round, Box.LabelBox(Style, ShadowGap));
                #endregion
                #region Button
                {
                    var rtL = rtLeft;
                    var rtR = rtRight;
                    if (bLeft) rtL.Offset(0, 1);
                    if (bRight) rtR.Offset(0, 1);

                    var isz = Convert.ToInt32(rtContent.Height / 3F);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-left", isz), ForeColor, rtL);
                    Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-right", isz), ForeColor, rtR);

                    var cp = MathTool.CenterPoint(rtContent);
                    var c1 = BorderColor;
                    var c2 = SelectorColor.BrightnessTransmit(0.2F);
                    var gny = Convert.ToInt32(rtContent.Height / 4);
                    var cy = Convert.ToInt32(cp.Y);

                    p.Width = 1;
                    p.Color = c1;
                    e.Graphics.DrawLine(p, rtL.Right, cy - gny, rtL.Right, cy + gny);
                    e.Graphics.DrawLine(p, rtR.Left, cy - gny, rtR.Left, cy + gny);

                    p.Width = 1;
                    p.Color = c2;
                    e.Graphics.DrawLine(p, rtL.Right + 0.5F, cy - gny, rtL.Right + 0.5F, cy + gny);
                    e.Graphics.DrawLine(p, rtR.Left + 0.5F, cy - gny, rtR.Left + 0.5F, cy + gny);
                }
                #endregion
                #region Text
                {
                    if (Items.Count > 0)
                    {
                        if (Animation && ani.IsPlaying)
                        {
                            e.Graphics.SetClip(rtText);
                           
                            #region Item
                            var SelectedIndex = ani.Variable == "Left" ? this.SelectedIndex + 1 : this.SelectedIndex - 1;
                            if (SelectedIndex == -1) SelectedIndex = Items.Count - 1;
                            if (SelectedIndex >= Items.Count) SelectedIndex = 0;

                            var itm = SelectedIndex == -1 ? null : Items[SelectedIndex];
                            var itmN = Items[SelectedIndex + 1 >= Items.Count ? 0 : SelectedIndex + 1];
                            var itmP = Items[SelectedIndex - 1 <= -1 ? Items.Count - 1 : SelectedIndex - 1];
                            #endregion
                            #region Bounds / Color
                            var w = rtText.Width;

                            var rt = rtText;
                            var rtP = rtTextP;
                            var rtN = rtTextN;

                            var c = ForeColor;
                            var cP = ForeColor;
                            var cN = ForeColor;

                            var eR = AnimationAccel.DCL;
                            var eA = AnimationAccel.Linear;
                            if (ani.Variable == "Left")
                            {
                                rt.Offset(ani.Value(eR, 0, w), 0);
                                rtP.Offset(ani.Value(eR, 0, w), 0);
                                rtN.Offset(ani.Value(eR, 0, w), 0);

                                cP = Color.FromArgb(Convert.ToByte(ani.Value(eA, 0, 150)), cP);
                                c = Color.FromArgb(Convert.ToByte(ani.Value(eA, 255, 0)), c);
                                cN = Color.FromArgb(Convert.ToByte(ani.Value(eA, 0, 0)), cN);
                            }
                            else if (ani.Variable == "Right")
                            {
                                rt.Offset(ani.Value(eR, 0, -w), 0);
                                rtP.Offset(ani.Value(eR, 0, -w), 0);
                                rtN.Offset(ani.Value(eR, 0, -w), 0);

                                cP = Color.FromArgb(Convert.ToByte(ani.Value(eA, 0, 0)), cP);
                                c = Color.FromArgb(Convert.ToByte(ani.Value(eA, 150, 0)), c);
                                cN = Color.FromArgb(Convert.ToByte(ani.Value(eA, 0, 255)), cN);
                            }
                            #endregion
                            #region Draw
                            if (itm != null) Theme.DrawTextIcon(e.Graphics, itm, Font, c, rt);

                            Theme.DrawTextIcon(e.Graphics, itmP, Font, cP, rtP);
                            Theme.DrawTextIcon(e.Graphics, itmN, Font, cN, rtN);
                            #endregion

                            e.Graphics.ResetClip();
                        }
                        else
                        {
                            #region TextIcon
                            var itm = SelectedIndex == -1 ? null : Items[SelectedIndex];
                            if (itm != null) Theme.DrawTextIcon(e.Graphics, itm, Font, ForeColor, rtText);
                            #endregion
                        }
                    }
                }
                #endregion
                #region Dispose
                p.Dispose();
                br.Dispose();
                #endregion

            });
            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtContent, rtLeft, rtRight, rtText, rtTextP, rtTextN) =>
            {
                if (CollisionTool.Check(rtLeft, e.X, e.Y)) bLeft = true;
                if (CollisionTool.Check(rtRight, e.X, e.Y)) bRight = true;
            });
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtLeft, rtRight, rtText, rtTextP, rtTextN) =>
            {
                #region LeftButton
                if (bLeft)
                {
                    if (CollisionTool.Check(rtLeft, e.X, e.Y))
                    {
                        if (Animation)
                        {
                            ani.Stop();
                            if (!ani.IsPlaying)
                            {
                                if (Items.Count > 0)
                                {
                                    SelectedIndex--;
                                    if (SelectedIndex < 0) SelectedIndex = Items.Count - 1;
                                }
                                else SelectedIndex = -1;
                                ani.Start(250, "Left", () => this.Invoke(new Action(() => Invalidate())));
                            }
                        }
                        else
                        {
                            if (Items.Count > 0)
                            {
                                SelectedIndex--;
                                if (SelectedIndex < 0) SelectedIndex = Items.Count - 1;
                            }
                            else SelectedIndex = -1;
                        }
                    }
                    bLeft = false;
                }
                #endregion
                #region RightButton
                if (bRight)
                {
                    if (CollisionTool.Check(rtRight, e.X, e.Y))
                    {
                        if (Animation)
                        {
                            ani.Stop();
                            if (!ani.IsPlaying)
                            {
                                if (Items.Count > 0)
                                {
                                    SelectedIndex++;
                                    if (SelectedIndex >= Items.Count) SelectedIndex = 0;
                                }
                                else SelectedIndex = -1;
                                ani.Start(250, "Right", () => this.Invoke(new Action(() => Invalidate())));
                            }
                        }
                        else
                        {
                            if (Items.Count > 0)
                            {
                                SelectedIndex--;
                                if (SelectedIndex < 0) SelectedIndex = Items.Count - 1;
                            }
                            else SelectedIndex = -1;
                        }
                    }
                    bRight = false;
                }
                #endregion
            });

            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();

            var bwh = ButtonWidth ?? rtContent.Height;
            var w = Convert.ToInt32(bwh * 1.2F);
            var h = Convert.ToInt32(bwh);

            var rtLeft = Util.MakeRectangleAlign(rtContent, new SizeF(w, h), DvContentAlignment.MiddleLeft);
            var rtRight = Util.MakeRectangleAlign(rtContent, new SizeF(w, h), DvContentAlignment.MiddleRight);
            var rtText = Util.FromRect(rtLeft.Right, rtContent.Top, rtRight.Left - rtLeft.Right, rtContent.Bottom);
            var rtTextP = Util.FromRect(rtText.Left - rtText.Width, rtText.Top, rtText.Width, rtText.Height);
            var rtTextN = Util.FromRect(rtText.Right, rtText.Top, rtText.Width, rtText.Height);

            act(rtContent, rtLeft, rtRight, rtText, rtTextP, rtTextN);
        }
        #endregion
        #endregion
    }
}
