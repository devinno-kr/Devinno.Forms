using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
    public class DvToggleIcon : DvControl
    {
        #region Properties
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if (bOnOff != value)
                {
                    bOnOff = value;
                    OnOffChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion

        #region Offset X/Y
        private int ox = 0, oy = 0;
        public int OffsetX { get => ox; set { if (ox != value) { ox = value; Invalidate(); } } }
        public int OffsetY { get => oy; set { if (oy != value) { oy = value; Invalidate(); } } }
        #endregion

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

        #region Color
        public Color OnColor { get; set; } = Color.White;
        public Color OffColor { get; set; } = Color.FromArgb(120, 120, 120);
        #endregion

        #region RadioMode
        public bool RadioMode { get; set; } = false;
        #endregion
        #endregion

        #region Event
        public event EventHandler OnOffChanged;
        public event EventHandler IconClick;
        #endregion

        #region Member Variable
        private bool bDown = false;
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            var c = OnOff ? OnColor : OffColor;
            var rt = GetContentBounds();

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.TranslateTransform(ox, oy);
            Theme.DrawTextIcon(e.Graphics, texticon, Font, c, rt);
            e.Graphics.ResetTransform();

            base.OnThemeDraw(e, Theme);
        }
        #endregion

        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            var rt = GetContentBounds();
            if (CollisionTool.Check(rt, e.X, e.Y)) bDown = true;

            base.OnMouseDown(e);
        }
        #endregion

        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            var rt = GetContentBounds();
            if (bDown)
            {
                bDown = false;
                if (CollisionTool.Check(rt, e.X, e.Y))
                {
                    IconClick?.Invoke(this, null);

                    #region RadioMode
                    if (RadioMode)
                    {
                        foreach (var v in Parent.Controls)
                            if (v is DvToggleIcon && v != this)
                                ((DvToggleIcon)v).OnOff = false;

                        OnOff = !OnOff;
                    }
                    #endregion
                }
            }
            base.OnMouseUp(e);
        }
        #endregion
        #endregion
    }
}
