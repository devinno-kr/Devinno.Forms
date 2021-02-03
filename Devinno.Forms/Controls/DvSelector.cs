using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.Forms.Controls
{
    public class DvSelector : DvControl
    {
        #region Properties
        #region BackgroundDraw
        private bool bBackgroundDraw = true;
        [Category("- 모양")]
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
        private Color cSelectorColor = DvTheme.DefaultTheme.Color3;
        [Category("- 색상")]
        public Color SelectorColor
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
        #region Style
        private LabelStyle eStyle = LabelStyle.FLAT;
        public LabelStyle Style
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
        #region Items
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<ITextIconItem> Items { get; } = new List<ITextIconItem>();
        #endregion
        #region SelectedIndex
        private int nSelectedIndex = -1;
        public int SelectedIndex
        {
            get=> nSelectedIndex;
            set
            {
                if(nSelectedIndex != value)
                {
                    nSelectedIndex = value;
                    SelectedIndexChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #region UseAnimation
        public bool UseAnimation { get; set; }
        #endregion
        #endregion

        #region Member Variable
        private bool bLeft = false, bRight = false;
        private bool bAni = false;
        private int ox;
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

            Size = new Size(80, 36);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var sz = g.MeasureIcon(new DvIcon("fa-chevron-right") { IconSize = Convert.ToInt32(Font.Size * 1.33) });

            int w = Convert.ToInt32(sz.Width * 2);
            var rtContent = Areas["rtContent"];
            var rtTextAll = new Rectangle(rtContent.Left, rtContent.Top, rtContent.Width, rtContent.Height);
            var rtLeft = new Rectangle(rtTextAll.Left, rtTextAll.Top, w, rtTextAll.Height);
            var rtRight = new Rectangle(rtTextAll.Right - w, rtTextAll.Top, w, rtTextAll.Height);
            var rtText = new Rectangle(rtLeft.Right, rtLeft.Top, rtTextAll.Width - rtLeft.Width - rtRight.Width, rtLeft.Height);

            SetArea("rtLeft", rtLeft);
            SetArea("rtRight", rtRight);
            SetArea("rtText", rtText);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var SelectorColor = UseThemeColor ? Theme.Color3 : this.SelectorColor;
            var BackColor = this.BackColor;

            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtLeft = Areas["rtLeft"];
            var rtRight = Areas["rtRight"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(SelectorColor, 1);
            var br = new SolidBrush(SelectorColor);
            #endregion
            #region Draw
            #region Background
            if (BackgroundDraw)
            {
                switch (Style)
                {
                    case LabelStyle.FLAT:
                        Theme.DrawBox(e.Graphics, SelectorColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        break;
                    case LabelStyle.CONCAVE:
                        Theme.DrawBox(e.Graphics, SelectorColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                        break;
                    case LabelStyle.CONVEX:
                        Theme.DrawBox(e.Graphics, SelectorColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT | BoxDrawOption.OUT_SHADOW);
                        break;
                }
            }
            #endregion
            #region Button
            if (bLeft) rtLeft.Offset(0, 1);
            if (bRight) rtRight.Offset(0, 1);

            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-left") { IconSize = Convert.ToInt32(Font.Size * 1.33) }, null, Font, ForeColor, BackgroundDraw ? SelectorColor : BackColor, rtLeft, DvContentAlignment.MiddleCenter);
            Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-right") { IconSize = Convert.ToInt32(Font.Size * 1.33) }, null, Font, ForeColor, BackgroundDraw ? SelectorColor : BackColor, rtRight, DvContentAlignment.MiddleCenter);
            #endregion
            #region Text
            e.Graphics.SetClip(rtText);
            if (Items.Count > 0)
            {
                if (UseAnimation)
                {
                    if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                    {
                        if (bAni)
                        {
                            for (int i = 0; i < Items.Count; i++)
                            {
                                var v = Items[i];
                                int x = rtText.X + ((i - SelectedIndex) * rtText.Width) + ox;
                                if (SelectedIndex == 0 && x > rtText.X + ((Items.Count - 1 - SelectedIndex) * rtText.Width)) x = rtText.X + ((-1 - SelectedIndex) * rtText.Width) + ox;
                                if (SelectedIndex == Items.Count - 1 && x < rtText.X + ((0 - SelectedIndex) * rtText.Width)) x = rtText.X + ((Items.Count - SelectedIndex) * rtText.Width) + ox;
                                var rt = new Rectangle(x, rtText.Y, rtText.Width, rtText.Height);
                                if (CollisionTool.Check(rt, rtText))
                                {
                                    var cp = MathTool.CenterPoint(rtText);
                                    var alpha = Convert.ToByte(MathTool.Constrain(MathTool.Map(Math.Abs((rt.X + (rt.Width / 2.0)) - cp.X), 0.0, (rtText.Width / 3), 255.0, 0), 0.0, 255.0));
                                    Theme.DrawTextShadow(e.Graphics, v.Icon, v.Text, Font, Color.FromArgb(alpha, ForeColor), BackgroundDraw ? SelectorColor : BackColor, rt, DvContentAlignment.MiddleCenter);
                                }
                            }
                        }
                        else
                        {
                            var v = Items[SelectedIndex];
                            Theme.DrawTextShadow(e.Graphics, v.Icon, v.Text, Font, ForeColor, BackgroundDraw ? SelectorColor : BackColor, rtText, DvContentAlignment.MiddleCenter);
                        }
                    }
                }
                else
                {
                    if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                    {
                        var v = Items[SelectedIndex];
                        Theme.DrawTextShadow(e.Graphics, v.Icon, v.Text, Font, ForeColor, BackgroundDraw ? SelectorColor : BackColor, rtText, DvContentAlignment.MiddleCenter);
                    }
                }
            }
            e.Graphics.ResetClip();
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
            if(Areas.Count>1)
            {
                if (CollisionTool.Check(Areas["rtLeft"], e.Location)) { bLeft = true; }
                if (CollisionTool.Check(Areas["rtRight"], e.Location)) { bRight = true; }
            }
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (UseAnimation)
            {
                #region Left / Right
                if (bLeft && !bAni)
                {
                    #region Left Animation
                    var th = new Thread(new ThreadStart(() =>
                    {
                        double MX = Areas["rtText"].Width;

                        var decelerationRate = 0.998;
                        var dCoeff = 1000D * Math.Log(decelerationRate);
                        var threshold = 0.1;

                        var initPos = 0;
                        var initVel = (MX / 0.15);
                        var destPos = MX;
                        var destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;

                        bAni = true;

                        var stime = DateTime.Now;
                        var time = 0D;
                        var nowpos = 0D;
                        while (time < destTime * 1000 && nowpos != destPos)
                        {
                            time = (DateTime.Now - stime).TotalMilliseconds;
                            nowpos = MathTool.Constrain(initPos + (Math.Pow(decelerationRate, time) - 1D) / dCoeff * initVel, 0D, MX);
                            ox = Convert.ToInt32(nowpos);
                            this.Invoke(new Action(() => Invalidate()));
                            Thread.Sleep(10);
                        }

                        ox = 0;
                        this.Invoke(new Action(() =>
                        {
                            var ns = SelectedIndex - 1;
                            if (ns < 0) ns = Items.Count - 1;
                            SelectedIndex = ns;
                            Invalidate();
                        }));

                        bAni = false;

                    }))
                    { IsBackground = true };
                    th.Start();
                    #endregion
                }
                if (bRight && !bAni)
                {
                    #region Right Animation
                    var th = new Thread(new ThreadStart(() =>
                    {
                        double MX = Areas["rtText"].Width;

                        var decelerationRate = 0.998;
                        var dCoeff = 1000D * Math.Log(decelerationRate);
                        var threshold = 0.1;

                        var initPos = 0;
                        var initVel = (MX / 0.15);
                        var destPos = MX;
                        var destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;

                        bAni = true;

                        var stime = DateTime.Now;
                        var time = 0D;
                        var nowpos = 0D;
                        while (time < destTime * 1000 && nowpos != destPos)
                        {
                            time = (DateTime.Now - stime).TotalMilliseconds;
                            nowpos = MathTool.Constrain(initPos + (Math.Pow(decelerationRate, time) - 1D) / dCoeff * initVel, 0D, MX);
                            ox = -Convert.ToInt32(nowpos);
                            this.Invoke(new Action(() => Invalidate()));
                            Thread.Sleep(10);
                        }

                        ox = 0;
                        this.Invoke(new Action(() =>
                        {
                            var ns = SelectedIndex+1;
                            if (ns >= Items.Count) ns = 0;
                            SelectedIndex = ns;
                            Invalidate();
                        }));

                        bAni = false;
                    }))
                    { IsBackground = true };
                    th.Start();
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Left / Right
                if (bLeft)
                {
                    if (Items.Count > 0)
                    {
                        SelectedIndex--;
                        if (SelectedIndex < 0) SelectedIndex = Items.Count - 1;
                    }
                    else SelectedIndex = -1;
                }
                if (bRight)
                {
                    if (Items.Count > 0)
                    {
                        SelectedIndex++;
                        if (SelectedIndex >= Items.Count) SelectedIndex = 0;
                    }
                    else SelectedIndex = -1;
                }
                #endregion
            }
            bLeft = bRight = false;
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        #endregion
        #endregion
    }

    #region interface : ITextIconItem
    public interface ITextIconItem
    {
        DvIcon Icon { get; }
        string Text { get; }
        object Tag { get; set; }
    }
    #endregion
    #region class : TextIconItem
    public class TextIconItem : ITextIconItem
    {
        #region Icon
        public DvIcon Icon { get; } = new DvIcon();
        public Bitmap IconImage { get => Icon.IconImage; set => Icon.IconImage = value; }
        public string IconString { get => Icon.IconString; set => Icon.IconString = value; }
        public int IconGap { get => Icon.Gap; set => Icon.Gap = value; }
        public DvTextIconAlignment IconAlignment { get => Icon.Alignment; set => Icon.Alignment = value; }
        public float IconSize { get => Icon.IconSize; set => Icon.IconSize = value; }
        #endregion
        #region Text
        public string Text { get; }
        #endregion
        #region Tag
        public object Tag { get; set; }
        #endregion
    }
    #endregion
}
