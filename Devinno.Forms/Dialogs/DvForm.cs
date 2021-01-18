using Devinno.Forms.Icons;
using Devinno.Forms.Menus;
using Devinno.Forms.Themes;
using Devinno.Tools;
using Devinno.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Devinno.Forms.Extensions;

namespace Devinno.Forms.Dialogs
{
    public partial class DvForm : Form
    {
        #region Properties
        #region Theme
        private DvTheme thm = DvTheme.DefaultTheme;
        public DvTheme Theme
        {
            get { return thm; }
            set
            {
                if (thm != value)
                {
                    var old = thm;
                    thm = value;
                    if(old != null & old != DvTheme.DefaultTheme) 
                        DvTheme.SetTheme(this, value);
                }
            }
        }
        #endregion

        #region Title
        private string strTitle = "TITLE";
        public string Title
        {
            get { return strTitle; }
            set
            {
                if (strTitle != value)
                {
                    strTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleIcon
        private DvIcon ico = new DvIcon();

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [Category("- 아이콘")]
        public Bitmap TitleIconImage
        {
            get => ico.IconImage;
            set { if (ico.IconImage != value) { ico.IconImage = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public string TitleIconString
        {
            get => ico.IconString;
            set { if (ico.IconString != value) { ico.IconString = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public int TitleIconGap
        {
            get => ico.Gap;
            set { if (ico.Gap != value) { ico.Gap = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public DvTextIconAlignment TitleIconAlignment
        {
            get => ico.Alignment;
            set { if (ico.Alignment != value) { ico.Alignment = value; Invalidate(); } }
        }
        [Category("- 아이콘")]
        public float TitleIconSize
        {
            get => ico.IconSize;
            set { if (ico.IconSize != value) { ico.IconSize = value; Invalidate(); } }
        }
        #endregion
        #region TitleFont
        private Font ftTitle= new Font("맑은 고딕", 9, FontStyle.Regular);
        public Font TitleFont
        {
            get => ftTitle;
            set
            {
                if (ftTitle != value)
                {
                    ftTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region UseThemeColor
        private bool bUseThemeColor = true;
        [Category("- 색상")]
        public bool UseThemeColor
        {
            get => bUseThemeColor;
            set
            {
                if (bUseThemeColor != value)
                {
                    bUseThemeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleIconColor
        private Color cTitleIconColor = DvTheme.DefaultTheme.ForeColor;
        [Category("- 색상")]
        public Color TitleIconColor
        {
            get => cTitleIconColor;
            set
            {
                if (cTitleIconColor != value)
                {
                    cTitleIconColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region FrameColor
        private Color cFrameColor = DvTheme.DefaultTheme.FrameColor;
        [Category("- 색상")]
        public Color FrameColor
        {
            get => cFrameColor;
            set
            {
                if (cFrameColor != value)
                {
                    cFrameColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region WindowStateButtonColor
        private Color cWindowStateButtonColor = DvTheme.DefaultTheme.ForeColor;
        [Category("- 색상")]
        public Color WindowStateButtonColor
        {
            get => cWindowStateButtonColor;
            set
            {
                if (cWindowStateButtonColor != value)
                {
                    cWindowStateButtonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region ExitBox
        private bool bExitBox = true;
        [Category("- 모양")]
        public bool ExitBox
        {
            get => bExitBox; 
            set
            {
                if (bExitBox != value)
                {
                    bExitBox = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Movable
        private bool bMovable = true;
        [Category("- 기능")]
        public bool Movable
        {
            get => bMovable;
            set => bMovable = value;
        }
        #endregion
        #region Fixed
        private bool bFixed = false;
        [Category("- 기능")]
        public bool Fixed
        {
            get => bFixed;
            set
            {
                if (bFixed != value)
                {
                    bFixed = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region Block
        private bool bBlock = false;
        [Category("- 기능")]
        public bool Block
        {
            get => bBlock;
            set
            {
                if (bBlock != value)
                {
                    bBlock = value; 
                    Invalidate();
                    DvTheme.LoopControl(this, (c) => c.Invalidate());
                }
            }
        }
        #endregion
        #region BlankForm
        private bool bBlankForm = false;
        [Category("- 기능")]
        public bool BlankForm
        {
            get => bBlankForm;
            set
            {
                if (bBlankForm != value)
                {
                    bBlankForm = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Member Variable
        internal bool bdExit = false, bdMax = false, bdMin = false;
        internal bool bExit = false, bMax = false, bMin = false;

        bool useTrayicon = false;
        NotifyIcon notifyIcon;

        WNDMV mvdown = null;
        #endregion

        #region Constructor
        public DvForm()
        {
            InitializeComponent();

            #region Default Property
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            DoubleBuffered = true;
            Padding = new Padding(0, 40, 0, 0);
            BackColor = BlackTheme.StaticBackColor;
            #endregion

            #region Timer
            var th = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
            {
                while (true)
                {
                    if (this.IsHandleCreated && !this.IsDisposed && !DesignMode)
                    {
                        try
                        {
                            this.Invoke(new EventHandler((o, s) =>
                            {
                                #region Rectangle
                                var c = this;
                                var rt = new Rectangle(0, 0, c.Width - 1, c.Height - 1);
                                var rtContent = new Rectangle(c.Padding.Left, c.Padding.Top, rt.Width - (c.Padding.Right + c.Padding.Left) + 1, rt.Height - (c.Padding.Top + c.Padding.Bottom) + 1);
                                var rtTitle = new Rectangle(rt.X, rt.Y, rt.Width, c.Padding.Top);
                                var rts = GetBoxBounds();
                                Rectangle rtExit = rts[2], rtMax = rts[1], rtMin = rts[0];
                                #endregion
                                #region Min/Max/Exit
                                if (!DesignMode)
                                {
                                    var e = this.PointToClient(MousePosition);

                                    if (ExitBox)
                                    {
                                        if (!bExit && CollisionTool.Check(rtExit, e.X, e.Y)) { bExit = true; Invalidate(); }
                                        if (bExit && !CollisionTool.Check(rtExit, e.X, e.Y)) { bExit = false; Invalidate(); }
                                    }
                                    if (MaximizeBox)
                                    {
                                        if (!bMax && CollisionTool.Check(rtMax, e.X, e.Y)) { bMax = true; Invalidate(); }
                                        if (bMax && !CollisionTool.Check(rtMax, e.X, e.Y)) { bMax = false; Invalidate(); }
                                    }
                                    if (MinimizeBox)
                                    {
                                        if (!bMin && CollisionTool.Check(rtMin, e.X, e.Y)) { bMin = true; Invalidate(); }
                                        if (bMin && !CollisionTool.Check(rtMin, e.X, e.Y)) { bMin = false; Invalidate(); }
                                    }
                                }
                                #endregion

                            }));
                        }
                        catch (Exception) { }
                    }
                    System.Threading.Thread.Sleep(100);
                }
            }));
            th.IsBackground = true;
            th.Start();
            #endregion
        }
        #endregion

        #region Override
        #region OnClosing
        protected override void OnClosing(CancelEventArgs e)
        {
            if (useTrayicon)
            {
                this.Visible = false;
                this.ShowIcon = false;
                if (notifyIcon != null) notifyIcon.Visible = true;
                e.Cancel = true;
            }
            base.OnClosing(e);
        }
        #endregion
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            #region Color
            var FrameColor = UseThemeColor ? Theme.FrameColor : this.FrameColor;
            var WindowStateButtonColor = UseThemeColor ? Theme.ForeColor : this.WindowStateButtonColor;
            #endregion
            #region Init
            var p = new Pen(FrameColor, 1);
            var br = new SolidBrush(FrameColor);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            #endregion

            if (!BlankForm)
            {
                #region Draw Background
                var rtContent = new Rectangle(Padding.Left, Padding.Top, Width - Padding.Right - Padding.Left, Height - Padding.Bottom - Padding.Top);
                var rtIcon = new Rectangle(0, 0, Padding.Top, Padding.Top);

                e.Graphics.Clear(FrameColor);
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                br.Color = BackColor; e.Graphics.FillRectangle(br, rtContent);

                br.Color = FrameColor.BrightnessTransmit(-0.2); e.Graphics.FillRectangle(br, rtIcon);

                p.Width = 1;
                p.Color = FrameColor.BrightnessTransmit(-0.1); e.Graphics.DrawLine(p, rtContent.Left, rtContent.Top + 0, rtContent.Right, rtContent.Top + 0);
                p.Color = BackColor.BrightnessTransmit(0.2); e.Graphics.DrawLine(p, rtContent.Left, rtContent.Top + 1, rtContent.Right, rtContent.Top + 1);
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

                #endregion
                #region Draw Exit / Max / Min
                var boxes = GetBoxBounds();
                int cn = 4;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                #region Exit
                if (ExitBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[2], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = bExit ? Color.Red : WindowStateButtonColor;
                    p.Width = 1;

                    e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Bottom);
                    e.Graphics.DrawLine(p, rt.Right, rt.Top, rt.Left, rt.Bottom);
                }
                else if(MinimizeBox || MaximizeBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[2], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                    p.Width = 1;

                    e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Bottom);
                    e.Graphics.DrawLine(p, rt.Right, rt.Top, rt.Left, rt.Bottom);
                }
                #endregion
                #region Max
                if (MaximizeBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[1], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = bMax ? Color.DeepSkyBlue : WindowStateButtonColor;
                    p.Width = 1;

                    if (WindowState == System.Windows.Forms.FormWindowState.Maximized)
                    {
                        int n = 4;
                        e.Graphics.DrawRectangle(p, new Rectangle(rt.X + n, rt.Y, rt.Width - n, rt.Height - n));
                        e.Graphics.FillRectangle(br, new Rectangle(rt.X, rt.Y + n, rt.Width - n, rt.Height - n));
                        e.Graphics.DrawRectangle(p, new Rectangle(rt.X, rt.Y + n, rt.Width - n, rt.Height - n));
                    }
                    else e.Graphics.DrawRectangle(p, rt);
                }
                else if(MinimizeBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[1], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                    p.Width = 1;

                    e.Graphics.DrawRectangle(p, rt);
                }
                #endregion
                #region Min
                if (MinimizeBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[0], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = bMin ? Color.Yellow : WindowStateButtonColor;
                    p.Width = 1;

                    e.Graphics.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                }
                else if(MaximizeBox)
                {
                    var rt = MathTool.MakeRectangle(boxes[0], new Size(Padding.Top / cn, Padding.Top / cn));
                    p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                    p.Width = 1;

                    e.Graphics.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                }
                #endregion
                #endregion
                #region Title
                var rtTitle = new Rectangle(Padding.Top + 15, 0, Width - (Padding.Top * 4) - 15, Padding.Top);

                br.Color = TitleIconColor; 
                e.Graphics.DrawIcon(ico, br, rtIcon, DvContentAlignment.MiddleCenter);
                
                br.Color = ForeColor; 
                e.Graphics.DrawText(Title, TitleFont, br, rtTitle, DvContentAlignment.MiddleLeft);
                #endregion
            }

            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnPaint(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!BlankForm)
            {
                #region Rectangle
                var c = this;
                var rtTitle = new Rectangle(0, 0, this.Width, c.Padding.Top);
                var rts = GetBoxBounds();
                #endregion
                #region Min/Max/Exit
                {
                    if (ExitBox && CollisionTool.Check(rts[2], e.X, e.Y)) { bdExit = true; Invalidate(); }
                    if (MaximizeBox && CollisionTool.Check(rts[1], e.X, e.Y)) { bdMax = true; Invalidate(); }
                    if (MinimizeBox && CollisionTool.Check(rts[0], e.X, e.Y)) { bdMin = true; Invalidate(); }
                }
                #endregion
                #region Form Move
                if (Movable)
                {
                    if (CollisionTool.Check(rtTitle, e.X, e.Y) && (!CollisionTool.Check(rts[0], e.X, e.Y) && !CollisionTool.Check(rts[1], e.X, e.Y) && !CollisionTool.Check(rts[2], e.X, e.Y)))
                        mvdown = new WNDMV() { Point = this.PointToScreen(e.Location), Location = this.Location };
                }
                #endregion
            }
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!BlankForm)
            {
                #region Rectangle
                var c = this;
                var rtTitle = new Rectangle(0, 0, this.Width, c.Padding.Top);
                var rts = GetBoxBounds();
                #endregion
                #region Min/Max/Exit
                {
                    if (ExitBox && bdExit && CollisionTool.Check(rts[2], e.X, e.Y))
                    {
                        this.Close();
                    }
                    if (MaximizeBox && bdMax && CollisionTool.Check(rts[1], e.X, e.Y))
                    {
                        if (WindowState != FormWindowState.Maximized) WindowState = FormWindowState.Maximized;
                        else WindowState = FormWindowState.Normal;
                    }
                    if (MinimizeBox && bdMin && CollisionTool.Check(rts[0], e.X, e.Y))
                    {
                        WindowState = FormWindowState.Minimized;
                    }

                    bdExit = bdMax = bdMin = false;
                    bExit = bMax = bMin = false;
                    Invalidate();
                }
                #endregion
                #region Form Move
                if (mvdown != null)
                {
                    var p = this.PointToScreen(e.Location);
                    var gx = p.X - mvdown.Point.X;
                    var gy = p.Y - mvdown.Point.Y;

                    if (Math.Abs(gx) > 5 || Math.Abs(gy) > 5) this.Location = new Point(mvdown.Location.X + gx, mvdown.Location.Y + gy);

                    mvdown = null;
                }
                #endregion
            }
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            #region Form Move
            if (mvdown != null)
            {
                var p = this.PointToScreen(e.Location);
                var gx = p.X - mvdown.Point.X;
                var gy = p.Y - mvdown.Point.Y;

                if (Math.Abs(gx) > 5 || Math.Abs(gy) > 5) this.Location = new Point(mvdown.Location.X + gx, mvdown.Location.Y + gy);
            }
            #endregion
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseDoubleClick
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (!BlankForm)
            {
                #region Rectangle
                var c = this;
                var rtTitle = new Rectangle(0, 0, this.Width, c.Padding.Top);
                var rts = GetBoxBounds();
                #endregion
                #region DoubleClick Maximize
                if (MaximizeBox)
                {
                    var vchk = CollisionTool.Check(rtTitle, e.X, e.Y) && (!CollisionTool.Check(rts[0], e.X, e.Y) && !CollisionTool.Check(rts[1], e.X, e.Y) && !CollisionTool.Check(rts[2], e.X, e.Y));
                    if (vchk)
                    {
                        if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
                        else if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
                    }
                }
                #endregion
            }
            base.OnMouseDoubleClick(e);
        }
        #endregion
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            const int NC_HITTEST = 0x84;

            #region Resize
            if (!Fixed && WindowState != FormWindowState.Maximized)
            {
                const int htLeft = 10;
                const int htRight = 11;
                const int htTop = 12;
                const int htTopLeft = 13;
                const int htTopRight = 14;
                const int htBottom = 15;
                const int htBottomLeft = 16;
                const int htBottomRight = 17;
                if (m.Msg == NC_HITTEST)
                {
                    var rts = GetBoxBounds();
                    Rectangle rtExit = rts[2], rtMax = rts[1], rtMin = rts[0];

                    int x = (short)(m.LParam.ToInt64() & 0xFFFF);
                    int y = (short)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                    Point pt = PointToClient(new Point(x, y));
                    Size clientSize = ClientSize;

                    if (!CollisionTool.Check(rtExit, pt.X, pt.Y) && !CollisionTool.Check(rtMax, pt.X, pt.Y) && !CollisionTool.Check(rtMin, pt.X, pt.Y))
                    {
                        int GAP = 10;
                        if (pt.X >= clientSize.Width - GAP && pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight); return; }
                        if (pt.X <= GAP && pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft); return; }
                        if (pt.X <= GAP && pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft); return; }
                        if (pt.X >= clientSize.Width - GAP && pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight); return; }
                        if (pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(htTop); return; }
                        if (pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(htBottom); return; }
                        if (pt.X <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(htLeft); return; }
                        if (pt.X >= clientSize.Width - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(htRight); return; }
                    }
                }
            }
            #endregion
            base.WndProc(ref m);
        }
        #endregion
        #endregion

        #region Method
        #region GetNotifyIcon
        public NotifyIcon GetNotifyIcon()
        {
            return notifyIcon;
        }
        #endregion
        #region GetBoxBounds
        internal Rectangle[] GetBoxBounds()
        {
            int szwh = Padding.Top / 4;
            int wh = Padding.Top;
            var rtRight = new Rectangle(this.Width - wh, 0, wh, wh);
            var rtMiddle = new Rectangle(rtRight.X - wh, 0, wh, wh);
            var rtLeft = new Rectangle(rtMiddle.X - wh, 0, wh, wh);
            return new Rectangle[] { rtLeft, rtMiddle, rtRight };
        }
        #endregion
        #region SetTrayicon
        public void SetTrayIcon(ContextMenuStrip cms, Icon ico, string text)
        {
            if (cms is DvContextMenuStrip) { ((DvContextMenuStrip)cms).SetReferenceTheme(this.Theme); }

            useTrayicon = true;
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = ico;

            notifyIcon.ContextMenuStrip = cms;
            notifyIcon.Text = text;
            notifyIcon.Visible = false;

            notifyIcon.MouseDoubleClick += (o, s) => {
                this.Visible = true;
                this.ShowIcon = true;
                notifyIcon.Visible = false;
            };

        }
        #endregion
        #endregion
    }

    #region class : WNDMV
    internal class WNDMV
    {
        internal Point Point { get; set; }
        internal Point Location { get; set; }
    }
    #endregion
}
