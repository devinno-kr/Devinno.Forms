using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
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
    public class DvBorderPanel : DvContainer
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
        #region BorderWidth
        private int nBorderWidth = 5;
        public int BorderWidth
        {
            get => nBorderWidth;
            set
            {
                if (nBorderWidth != value)
                {
                    nBorderWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region BorderColor
        private Color? cBorderColor = null;
        public Color? BorderColor
        {
            get => cBorderColor;
            set
            {
                if (cBorderColor != value)
                {
                    cBorderColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Constructor
        public DvBorderPanel()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, false);
            UpdateStyles();

            TabStop = false;
            #endregion

            Padding = new Padding(0, nTitleHeight, 0, 0);
            Size = new Size(150, 100);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BorderColor = this.BorderColor ?? Theme.BorderPanelColor;
            var BorderDarkColor = Theme.GetBorderColor(BorderColor, BackColor); 
            var Corner = Theme.Corner;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            Pen p = new Pen(Color.Black);
            SolidBrush br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtPanel, rtTitle, rtText) =>
            {
                e.Graphics.Clear(Parent.BackColor);

                var rti = rtContent; rti.Inflate(-BorderWidth, -BorderWidth);
                using (var path = DrawingTool.GetRoundRectPath(rtContent, Corner))
                {
                    #region Path
                    using (var path2 = new GraphicsPath())
                    {
                        if (DrawTitle)
                        {
                            var TitleWidth = rtTitle.Width;

                            var rt = Util.FromRect(rti.Left, TitleHeight, Corner * 2, Corner * 2);
                            path2.AddArc(rt, 180, 90);
                            path2.AddBeziers(new PointF[]{ new PointF(TitleWidth + 0, TitleHeight),
                                                        new PointF(TitleWidth + 10, TitleHeight),
                                                        new PointF(TitleWidth + 15, TitleHeight/2F),
                                                        new PointF(TitleWidth + 30, rti.Top),
                            });
                            path2.AddArc(Util.FromRect(rti.Right - Corner * 2, rti.Top, Corner * 2, Corner * 2), -90, 90);
                            path2.AddArc(Util.FromRect(rti.Right - Corner * 2, rti.Bottom - Corner * 2, Corner * 2, Corner * 2), 0, 90);
                            path2.AddArc(Util.FromRect(rti.Left, rti.Bottom - Corner * 2, Corner * 2, Corner * 2), 90, 90);
                            path2.CloseAllFigures();
                        }
                        else
                        {
                            using (var pth = DrawingTool.GetRoundRectPath(rti, Corner)) path2.AddPath(pth, false);
                        }
                        path.AddPath(path2, false);
                    }
                    #endregion

                    #region Back
                    Theme.DrawBox(e.Graphics, rtContent, BackColor, BackColor, RoundType.All, Box.FlatBox());
                    #endregion
                    #region Shadow
                    e.Graphics.TranslateTransform(1, 1);
                    br.Color = Color.FromArgb(Theme.OutShadowAlpha, Color.Black);
                    e.Graphics.FillPath(br, path);
                    e.Graphics.ResetTransform();
                    #endregion
                    #region Fill
                    br.Color = BorderColor;
                    e.Graphics.FillPath(br, path);
                    #endregion
                    #region Gradient
                    var rtT = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width, TitleHeight);
                    e.Graphics.SetClip(rtT);
                    using (var lgbr = new LinearGradientBrush(rtT, Color.FromArgb(Theme.GradientLightAlpha, Color.White), Color.FromArgb(0, Color.White), 90))
                    {
                        e.Graphics.FillPath(lgbr, path);
                    }
                    e.Graphics.ResetClip();
                    #endregion
                    #region Bevel
                    e.Graphics.SetClip(path);
                    e.Graphics.TranslateTransform(1, 1);
                    using (var lgbr = new LinearGradientBrush(rtContent, Color.FromArgb(60, Color.White), Color.FromArgb(0, Color.White), 60))
                    {
                        using (var p2 = new Pen(lgbr, 1))
                        {
                            e.Graphics.DrawPath(p2, path);
                        }
                    }
                    e.Graphics.ResetTransform();
                    e.Graphics.ResetClip();
                    #endregion
                    #region Border
                    p.Width = 1;
                    p.Color = BorderDarkColor;
                    e.Graphics.DrawPath(p, path);
                    #endregion
                    #region Text
                    Theme.DrawTextIcon(e.Graphics, texticon, Font, ForeColor, rtText);
                    #endregion
                }

            });

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtTitle = Util.FromRect(rtContent.Left + 1, rtContent.Top, rtContent.Width - 2, TitleHeight);
            var rtPanel = rtContent;
            var rtText = Util.FromRect(rtTitle.Left + 5, rtTitle.Top, rtTitle.Width - 5, rtTitle.Height);

            if (DrawTitle)
            {
                using (var g = CreateGraphics())
                {
                    var sz = g.MeasureTextIcon(texticon, Font);
                    var TitleWidth = sz.Width + 20;

                    rtTitle = Util.FromRect(rtContent.Left + 1, rtContent.Top, TitleWidth, TitleHeight);
                    rtPanel = Util.FromRect(rtContent.Left, rtTitle.Bottom, rtContent.Width, rtContent.Height - rtTitle.Bottom);
                    rtText = Util.FromRect(rtTitle.Left + TextPadding.Left, rtTitle.Top + TextPadding.Top, rtTitle.Width - (TextPadding.Left + TextPadding.Right), rtTitle.Height - (TextPadding.Top + TextPadding.Bottom));
                }
            }

            act(rtContent, rtPanel, rtTitle, rtText);
        }
        #endregion
        #endregion
    }
}
