﻿using Devinno.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
    public class DvLabel : DvControl
    {
        #region Properties
        #region Icon
        private DvIcon ico = new DvIcon();

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [Category("- 아이콘")]
        public Bitmap IconImage
        {
            get => ico.IconImage;
            set { if (ico.IconImage != value) { ico.IconImage = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public string IconString
        {
            get => ico.IconString;
            set { if (ico.IconString != value) { ico.IconString = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public int IconGap
        {
            get => ico.Gap;
            set { if (ico.Gap != value) { ico.Gap = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public DvTextIconAlignment IconAlignment
        {
            get => ico.Alignment;
            set { if (ico.Alignment != value) { ico.Alignment = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public float IconSize
        {
            get => ico.IconSize;
            set { if (ico.IconSize != value) { ico.IconSize = value; Invalidate(); } }
        }
        #endregion
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        [Category("- 모양")]
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
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
        #region LabelColor
        private Color cLabelColor = DvTheme.DefaultTheme.Color2;
        [Category("- 색상")]
        public Color LabelColor
        {
            get => cLabelColor;
            set
            {
                if (cLabelColor != value)
                {
                    cLabelColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Text
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Category("- 모양")]
        public override string Text
        {
            get => base.Text;
            set { if (base.Text != value) { base.Text = value; Invalidate(); } }
        }
        #endregion
        #region TextPadding
        private Padding padText = new Padding(0, 0, 0, 0);
        [Category("- 모양")]
        public Padding TextPadding
        {
            get => padText;
            set
            {
                if (padText != value)
                {
                    padText = value;
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
                if(eStyle != value)
                {
                    eStyle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Unit
        private string strUnit = "";
        public string Unit
        {
            get => strUnit;
            set
            {
                if (strUnit != value)
                {
                    strUnit = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UnitWidth
        private int nUnitWidth = 36;
        public int UnitWidth
        {
            get => nUnitWidth;
            set
            {
                if (nUnitWidth != value)
                {
                    nUnitWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region UseLongClick
        public bool UseLongClick { get => click.UseLongClick; set => click.UseLongClick = value; }
        #endregion 
        #region LongClickTime
        public int LongClickTime { get => click.LongClickTime; set => click.LongClickTime = value; }
        #endregion
        #endregion

        #region Event
        public event EventHandler LongClick;
        #endregion

        #region Member Variable
        private LongClick click = new LongClick();
        #endregion

        #region Constructor
        public DvLabel()
        {
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            Size = new Size(80, 36);

            TabStop = true;

            click.GenLongClick = new Action(() => { this.Invoke(new Action(() => LongClick?.Invoke(this, null))); });
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var szUnitW = 0;
            if (!string.IsNullOrWhiteSpace(Unit)) szUnitW = UnitWidth;

            var rtContent = Areas["rtContent"];
            var rtTextAll = new Rectangle(TextPadding.Left, TextPadding.Top, rtContent.Width - (TextPadding.Left + TextPadding.Right), rtContent.Height - (TextPadding.Top + TextPadding.Bottom));
            var rtUnit = new Rectangle(rtTextAll.Right - szUnitW, rtTextAll.Top, szUnitW, rtTextAll.Height);
            var rtText = new Rectangle(rtTextAll.Left, rtTextAll.Top, rtTextAll.Width - rtUnit.Width, rtTextAll.Height); rtText.Inflate(-1, 0);
            SetArea("rtText", rtText);
            SetArea("rtUnit", rtUnit);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var LabelColor = UseThemeColor ? Theme.Color2 : this.LabelColor;
            var BackColor = this.BackColor;
            if (BackColor == Color.Transparent) BackColor = GetParentBackColor(Parent);
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtText = Areas["rtText"];
            var rtUnit = Areas["rtUnit"];
            #endregion
            #region Init
            var p = new Pen(LabelColor, 1);
            var br = new SolidBrush(LabelColor);
            #endregion
            #region Draw
            if (BackgroundDraw)
            {
                switch (Style)
                {
                    case LabelStyle.FLAT:
                        Theme.DrawBox(e.Graphics, LabelColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_SHADOW);
                        break;
                    case LabelStyle.CONCAVE:
                        Theme.DrawBox(e.Graphics, LabelColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.OUT_BEVEL | BoxDrawOption.IN_SHADOW);
                        break;
                    case LabelStyle.CONVEX:
                        Theme.DrawBox(e.Graphics, LabelColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL_LT | BoxDrawOption.OUT_SHADOW);
                        break;
                }
            }
                        
            Theme.DrawTextShadow(e.Graphics, ico, Text, Font, ForeColor, BackgroundDraw ? LabelColor : BackColor, rtText, ContentAlignment);
            if (UnitWidth > 0 && !string.IsNullOrWhiteSpace(Unit))
            {
                Theme.DrawTextShadow(e.Graphics, null, Unit, Font, ForeColor, BackgroundDraw ? LabelColor : BackColor, rtUnit, ContentAlignment);
            }
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
            click.MouseDown(e);
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            click.MouseUp(e);
            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        
    }

    #region enum : LabelStyle 
    public enum LabelStyle 
    {
        /// <summary>
        /// 평평한
        /// </summary>
        FLAT,
        /// <summary>
        /// 오목한
        /// </summary>
        CONCAVE, 
        /// <summary>
        /// 볼록한
        /// </summary>
        CONVEX
    }
    #endregion
}
