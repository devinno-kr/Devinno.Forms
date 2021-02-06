using Devinno.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
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
    public class DvOnOff : DvControl
    {
        #region Properties
        #region CursorColor
        private Color cCursorColor = DvTheme.DefaultTheme.Color3;
        public Color CursorColor
        {
            get => cCursorColor;
            set
            {
                if(cCursorColor != value)
                {
                    cCursorColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffBoxColor
        private Color cOffBoxColor = DvTheme.DefaultTheme.Color1;
        public Color OffBoxColor
        {
            get => cOffBoxColor;
            set
            {
                if (cOffBoxColor != value)
                {
                    cOffBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnBoxColor
        private Color cOnBoxColor = DvTheme.DefaultTheme.PointColor;
        public Color OnBoxColor
        {
            get => cOnBoxColor;
            set
            {
                if (cOnBoxColor != value)
                {
                    cOnBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OffTextColor
        private Color cOffTextColor = DvTheme.DefaultTheme.Color3;
        public Color OffTextColor
        {
            get => cOffTextColor;
            set
            {
                if (cOffTextColor != value)
                {
                    cOffTextColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnTextColor
        private Color cOnTextColor = Color.White;
        public Color OnTextColor
        {
            get => cOnTextColor;
            set
            {
                if (cOnTextColor != value)
                {
                    cOnTextColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region OnOff
        private bool bOnOff = false;
        public bool OnOff
        {
            get => bOnOff;
            set
            {
                if(bOnOff != value)
                {
                    bOnOff = value;
                    OnOffChanged?.Invoke(this, null);
                    Invalidate();
                }
            }
        }
        #endregion
        #region DrawText
        private bool bDrawText = true;
        public bool DrawText
        {
            get => bDrawText;
            set
            {
                if(bDrawText != value)
                {
                    bDrawText = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Event 
        public event EventHandler OnOffChanged;
        #endregion

        #region Mebmer Variable
        bool bDown = false;
        #endregion

        #region Constructor
        public DvOnOff()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 60);

        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var w = Convert.ToInt32(rtContent.Width * 0.6);
            var ng = rtContent.Height / 10;
            
            var rtCursor = new Rectangle(rtContent.X, rtContent.Y, w, rtContent.Height); rtCursor.Inflate(-ng, -ng);
            rtCursor.X = OnOff ? (rtContent.Right - rtCursor.Width - ng) : (rtContent.Left + ng);
            SetArea("rtCursor", rtCursor);

            if (OnOff)
            {
                var rtText = new Rectangle(rtContent.Left + (ng / 2), rtCursor.Y, rtCursor.Left - rtContent.Left, rtCursor.Height);
                SetArea("rtText", rtText);
            }
            else
            {
                var rtText = new Rectangle(rtCursor.Right - (ng / 2), rtCursor.Y, rtContent.Right - rtCursor.Right, rtCursor.Height);
                SetArea("rtText", rtText);
            }

            var rg = rtCursor.Height / 3 / 3;
            var rtCursorAch = DrawingTool.MakeRectangleAlign(rtCursor, new Size(rg * 3 + 4, rg * 3 + 4), DvContentAlignment.MiddleCenter);
            var rtCursorAch1 = new Rectangle(rtCursorAch.X, rtCursorAch.Y, rg, rtCursorAch.Height);
            var rtCursorAch2 = new Rectangle(rtCursorAch.X + rg + 2, rtCursorAch.Y, rg, rtCursorAch.Height);
            var rtCursorAch3 = new Rectangle(rtCursorAch.X + rg + 2 + rg + 2, rtCursorAch.Y, rg, rtCursorAch.Height);
            SetArea("rtCursorA1", rtCursorAch1);
            SetArea("rtCursorA2", rtCursorAch2);
            SetArea("rtCursorA3", rtCursorAch3);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var OnBoxColor = UseThemeColor ? Theme.PointColor : this.OnBoxColor;
            var OffBoxColor = UseThemeColor ? Theme.Color1 : this.OffBoxColor;
            var OnTextColor = UseThemeColor ? Color.White : this.OnTextColor;
            var OffTextColor = UseThemeColor ? Theme.Color3 : this.OffTextColor;
            var CursorColor = UseThemeColor ? Theme.Color3 : this.CursorColor;
            var BoxColor = OnOff ? OnBoxColor : OffBoxColor;
            var TextColor = OnOff ? OnTextColor : OffTextColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtCursor = Areas["rtCursor"];
            var rtA1 = Areas["rtCursorA1"];
            var rtA2 = Areas["rtCursorA2"];
            var rtA3 = Areas["rtCursorA3"];
            var rtText = Areas["rtText"];
            #endregion
            #region Init
            var p = new Pen(BoxColor, 1);
            var br = new SolidBrush(BoxColor);
            #endregion
            #region Draw
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var cc = bDown ? CursorColor.BrightnessTransmit(Theme.DownBright * -1) : CursorColor;
            Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.FULL_HORIZON, BoxDrawOption.OUT_BEVEL | BoxDrawOption.BORDER | BoxDrawOption.IN_SHADOW);
            Theme.DrawBox(e.Graphics, cc, BoxColor, rtCursor, RoundType.FULL_HORIZON, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL2 | BoxDrawOption.OUT_SHADOW | BoxDrawOption.GRADIENT_V);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            
            var c = cc.BrightnessTransmit(-0.3);
            var cD = c.BrightnessTransmit(Theme.InShadowBright);
            var cL = cc.BrightnessTransmit(Theme.OutBevelBright);

            br.Color = c; 
            e.Graphics.FillRectangle(br, rtA1); 
            e.Graphics.FillRectangle(br, rtA2);
            e.Graphics.FillRectangle(br, rtA3);

            p.Width = 1;
            
            p.Color = cD;
            e.Graphics.DrawLine(p, rtA1.Left, rtA1.Top, rtA1.Left, rtA1.Bottom); e.Graphics.DrawLine(p, rtA1.Left, rtA1.Top, rtA1.Right, rtA1.Top);
            e.Graphics.DrawLine(p, rtA2.Left, rtA2.Top, rtA2.Left, rtA2.Bottom); e.Graphics.DrawLine(p, rtA2.Left, rtA2.Top, rtA2.Right, rtA2.Top);
            e.Graphics.DrawLine(p, rtA3.Left, rtA3.Top, rtA3.Left, rtA3.Bottom); e.Graphics.DrawLine(p, rtA3.Left, rtA3.Top, rtA3.Right, rtA3.Top);

            p.Color = cL; 
            e.Graphics.DrawLine(p, rtA1.Right, rtA1.Top, rtA1.Right, rtA1.Bottom); e.Graphics.DrawLine(p, rtA1.Left, rtA1.Bottom, rtA1.Right, rtA1.Bottom);
            e.Graphics.DrawLine(p, rtA2.Right, rtA2.Top, rtA2.Right, rtA2.Bottom); e.Graphics.DrawLine(p, rtA2.Left, rtA2.Bottom, rtA2.Right, rtA2.Bottom);
            e.Graphics.DrawLine(p, rtA3.Right, rtA3.Top, rtA3.Right, rtA3.Bottom); e.Graphics.DrawLine(p, rtA3.Left, rtA3.Bottom, rtA3.Right, rtA3.Bottom);

            if (DrawText) Theme.DrawTextShadow(e.Graphics, null, OnOff ? "ON" : "OFF", Font, TextColor, BoxColor, rtText, DvContentAlignment.MiddleCenter);
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion

            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            bDown = false;
            Focus();
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (Areas.ContainsKey("rtText") && CollisionTool.Check(Areas["rtText"], e.Location))
            {
                OnOff = !OnOff;
                Invalidate();
            }

            if (Areas.ContainsKey("rtCursor") && CollisionTool.Check(Areas["rtCursor"], e.Location))
            {
                bDown = true;
                Invalidate();
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if(bDown)
            {
                var hx = this.Width / 2;
                OnOff = e.X > hx;
            }
            base.OnMouseMove(e);
        }
        #endregion
        #endregion
    }
}
