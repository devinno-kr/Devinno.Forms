using Devinno.Collections;
using Devinno.Extensions;
using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
using Devinno.Forms.Icons;
using Devinno.Forms.Menus;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Thread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Devinno.Forms.Dialogs
{

    public partial class DvForm : Form
    {
        #region Const
        public const int BlockAlpha = 100;

        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;

        private const int WM_ACTIVATE = 0x0006;
        private const int WM_PAINT = 0x000f;
        private const int WM_CREATE = 0x0001;
        private const int WM_NCHITTEST = 0x84;
        private const int WM_SIZING = 0x214;
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_ENTERSIZEMOVE = 0x0231;
        private const int WM_EXITSIZEMOVE = 0x0232;
        private const int WM_NCCALCSIZE = 0x0083;
        private const int WMSZ_LEFT = 1;
        private const int WMSZ_RIGHT = 2;
        private const int WMSZ_TOP = 3;
        private const int WMSZ_TOPLEFT = 4;
        private const int WMSZ_TOPRIGHT = 5;
        private const int WMSZ_BOTTOM = 6;
        private const int WMSZ_BOTTOMLEFT = 7;
        private const int WMSZ_BOTTOMRIGHT = 8;

        private const uint WTNCA_NODRAWCAPTION = 0x00000001;
        private const uint WTNCA_NODRAWICON = 0x00000002;
        #endregion

        #region Event
        public event EventHandler<ThemeDrawEventArgs> ThemeDraw;
        public event EventHandler<MenuSelectedEventArgs> MenuSelected;
        public event EventHandler EnterSizeMove;
        public event EventHandler ExitSizeMove;
        #endregion

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
                    if (old != null & old != DvTheme.DefaultTheme)
                        DvTheme.SetTheme(this, value);
                }
            }
        }
        #endregion
        #region ExitBox
        private bool bExitBox = true;
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
        public bool Movable
        {
            get => bMovable;
            set => bMovable = value;
        }
        #endregion
        #region Fixed
        private bool bFixed = false;
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

        #region Title
        public string Title
        {
            get => base.Text;
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    Invalidate();
                }
            }
        }

        public override string Text
        {
            get => base.Text;
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleIcon
        private DvIcon ico = new DvIcon();

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        public Bitmap TitleIconImage
        {
            get => ico.IconImage;
            set { if (ico.IconImage != value) { ico.IconImage = value; Invalidate(); } }
        }
        public string TitleIconString
        {
            get => ico.IconString;
            set { if (ico.IconString != value) { ico.IconString = value; Invalidate(); } }
        }
        public int TitleIconGap
        {
            get => ico.Gap;
            set { if (ico.Gap != value) { ico.Gap = value; Invalidate(); } }
        }
        public DvTextIconAlignment TitleIconAlignment
        {
            get => ico.Alignment;
            set { if (ico.Alignment != value) { ico.Alignment = value; Invalidate(); } }
        }
        public float TitleIconSize
        {
            get => ico.IconSize;
            set { if (ico.IconSize != value) { ico.IconSize = value; Invalidate(); } }
        }
        #endregion
        #region TitleIconColor
        private Color? cTitleIconColor = null;
        public Color? TitleIconColor
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
        #region TitleIconBoxColor
        private Color? cIconBoxColor = null;
        public Color? TitleIconBoxColor
        {
            get => cIconBoxColor;
            set
            {
                if (cIconBoxColor != value)
                {
                    cIconBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleFont
        private Font ftTitle = new Font("나눔고딕", 9, FontStyle.Regular);
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
        #region TitlePadding
        private Padding padTitle = new Padding(0, 0, 0, 0);
        [Category("- 모양")]
        public Padding TitlePadding
        {
            get => padTitle;
            set
            {
                if (padTitle != value)
                {
                    padTitle = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleBarColor
        private Color? cTitleBarColor = null;
        public Color? TitleBarColor
        {
            get => cTitleBarColor;
            set
            {
                if (cTitleBarColor != value)
                {
                    cTitleBarColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region TitleHeight
        public int TitleHeight
        {
            get => Padding.Top;
            set
            {
                if (Padding.Top != value)
                {
                    Padding = new Padding(Padding.Left, value, Padding.Right, Padding.Bottom);
                    Invalidate();
                }
            }
        }
        #endregion
        #region WindowStateButtonColor
        private Color? cWindowStateButtonColor = null;
        public Color? WindowStateButtonColor
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
        #region Menus
        public EventList<DvFormMenuItem> Menus { get; } = new EventList<DvFormMenuItem>();
        #endregion
        #region MenuGap
        private int nMenuGap = 0;
        public int MenuGap
        {
            get => nMenuGap;
            set
            {
                if (nMenuGap != value)
                {
                    nMenuGap = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Loaded
        public bool Loaded { get; private set; }
        #endregion
        #endregion

        #region Member Variable
        private bool bdExit = false, bdMax = false, bdMin = false;

        private Thread th;

        private bool useTrayicon = false;
        private NotifyIcon notifyIcon;

        private Size o_sz;
        private FormWindowState o_st;
        private Point o_oe;

        private Point? pdown = null;
        private Rectangle? pbounds = null;
        private Rectangle? plast = null;
        private DwmTool.RECT prc_m = new DwmTool.RECT() { Left = 500, Top = 500, Right = 500, Bottom = 500 };
        private DwmTool.RECT prc_s = new DwmTool.RECT() { Left = 500, Top = 500, Right = 500, Bottom = 500 };
        private int offsetT = 0, offsetB = 0;
        #endregion

        #region Interop
        private static readonly int GWL_EXSTYLE = -20;
        private static readonly int WS_EX_COMPOSITED = 0x02000000;

        [DllImport("user32")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion

        #region Constructor
        public DvForm()
        {
            InitializeComponent();

            #region Styles
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            UpdateStyles();
            #endregion
            #region Properties
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            Padding = new Padding(0, 40, 0, 0);
            BackColor = DvTheme.DefaultTheme.BackColor;
            Font = new Font("나눔고딕", 9);
            ResizeRedraw = true;
            #endregion
            #region Thread
            th = new Thread(() =>
            {
                if (!DesignMode)
                {
                    while (true)
                    {
                        if (this.Created && this.Loaded && !this.IsDisposed)
                        {
                            #region Invalidate
                            try
                            {
                                bool bInv = false;

                                #region Size / State / Move
                                if (o_sz != this.Size) { o_sz = this.Size; bInv |= true; }
                                if (o_st != this.WindowState) { o_st = this.WindowState; bInv |= true; }
                                #endregion
                                #region WindowButton
                                var e = this.PointToClient(MousePosition);
                                if (o_oe.X != e.X || o_oe.Y != e.Y)
                                {
                                    o_oe = e;

                                    Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
                                    {
                                        if (ExitBox)
                                        {
                                            var b = CollisionTool.Check(rtExit, e.X, e.Y);
                                            bInv |= bdExit != b;
                                            bdExit = b;
                                        }
                                        if (MaximizeBox)
                                        {
                                            var b = CollisionTool.Check(rtMax, e.X, e.Y);
                                            bInv |= bdMax != b;
                                            bdMax = b;
                                        }
                                        if (MinimizeBox)
                                        {
                                            var b = CollisionTool.Check(rtMin, e.X, e.Y);
                                            bInv |= bdMin != b;
                                            bdMin = b;
                                        }

                                    });
                                }
                                #endregion

                                if (bInv) this.Invoke(new Action(() => this.Invalidate()));
                            }
                            catch { }
                            #endregion
                        }

                        Thread.Sleep(50);
                    }
                }

            })
            { IsBackground = true };
            //th.Start();
            #endregion
            
            Menus.Changed += (o, s) => { foreach (var v in Menus) v.form = this; };

            ResourceTool.Init();
        }
        #endregion

        #region Override
        #region OnHandleCreated
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }
        #endregion
        #region OnShown
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
        #endregion
        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            Loaded = true;
            base.OnLoad(e);
        }
        #endregion
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

        #region OnClientSizeChanged
        protected override void OnClientSizeChanged(EventArgs e)
        {
            Invalidate();
            base.OnClientSizeChanged(e);
        }
        #endregion

        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Theme != null)
            {
                #region Color
                var WindowStateButtonColor = this.WindowStateButtonColor ?? Theme.ForeColor;
                var TitleBarColor = this.TitleBarColor ?? Theme.WindowTitleColor;
                var TitleIconBoxColor = this.TitleIconBoxColor ?? Theme.PointColor;
                var TitleIconColor = this.TitleIconColor ?? Theme.ForeColor;
                #endregion
                #region Init
                var p = new Pen(TitleBarColor, 1);
                var br = new SolidBrush(TitleBarColor);

                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                #endregion
                #region Draw
                Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
                {
                    if (!BlankForm)
                    {
                        #region Draw Background
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                        br.Color = TitleBarColor; e.Graphics.FillRectangle(br, rtTitleBar);
                        br.Color = TitleIconBoxColor; e.Graphics.FillRectangle(br, rtTitleIconBox);

                        p.Width = 1;
                        p.Color = TitleBarColor.BrightnessTransmit(-0.1); e.Graphics.DrawLine(p, rtContent.Left, rtContent.Top - 1, rtContent.Right, rtContent.Top - 1);
                        p.Color = BackColor.BrightnessTransmit(0.2); e.Graphics.DrawLine(p, rtContent.Left, rtContent.Top, rtContent.Right, rtContent.Top);
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        #endregion
                        #region Draw Exit / Max / Min
                        int cn = 4;
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                        #region Hover Check
                        var mp = this.PointToClient(MousePosition);

                        bool bExit = false, bMax = false, bMin = false;

                        if (ExitBox) bExit = CollisionTool.Check(rtExit, mp.X, mp.Y);
                        if (MaximizeBox) bMax = CollisionTool.Check(rtMax, mp.X, mp.Y);
                        if (MinimizeBox) bMin = CollisionTool.Check(rtMin, mp.X, mp.Y);
                        #endregion
                        #region Exit
                        if (ExitBox)
                        {
                            var rt = MathTool.MakeRectangle(rtExit, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = bExit ? Color.Red : WindowStateButtonColor;
                            p.Width = 1;

                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Bottom);
                            e.Graphics.DrawLine(p, rt.Right, rt.Top, rt.Left, rt.Bottom);
                        }
                        else if (MinimizeBox || MaximizeBox)
                        {
                            var rt = MathTool.MakeRectangle(rtExit, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                            p.Width = 1;

                            e.Graphics.DrawLine(p, rt.Left, rt.Top, rt.Right, rt.Bottom);
                            e.Graphics.DrawLine(p, rt.Right, rt.Top, rt.Left, rt.Bottom);
                        }
                        #endregion
                        #region Max
                        if (MaximizeBox)
                        {
                            var rt = MathTool.MakeRectangle(rtMax, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = bMax ? Color.DeepSkyBlue : WindowStateButtonColor;
                            p.Width = 1;
                            br.Color = TitleBarColor;
                            if (WindowState == System.Windows.Forms.FormWindowState.Maximized)
                            {
                                int n = 4;
                                e.Graphics.DrawRectangle(p, new RectangleF(rt.X + n, rt.Y, rt.Width - n, rt.Height - n));
                                e.Graphics.FillRectangle(br, new RectangleF(rt.X, rt.Y + n, rt.Width - n, rt.Height - n));
                                e.Graphics.DrawRectangle(p, new RectangleF(rt.X, rt.Y + n, rt.Width - n, rt.Height - n));
                            }
                            else e.Graphics.DrawRectangle(p, rt);
                        }
                        else if (MinimizeBox)
                        {
                            var rt = MathTool.MakeRectangle(rtMax, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                            p.Width = 1;

                            e.Graphics.DrawRectangle(p, rt);
                        }
                        #endregion
                        #region Min
                        if (MinimizeBox)
                        {
                            var rt = MathTool.MakeRectangle(rtMin, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = bMin ? Color.Yellow : WindowStateButtonColor;
                            p.Width = 1;

                            e.Graphics.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                        }
                        else if (MaximizeBox)
                        {
                            var rt = MathTool.MakeRectangle(rtMin, new Size(Padding.Top / cn, Padding.Top / cn));
                            p.Color = WindowStateButtonColor.BrightnessTransmit(-0.7);
                            p.Width = 1;

                            e.Graphics.DrawLine(p, rt.Left, rt.Bottom, rt.Right, rt.Bottom);
                        }
                        #endregion

                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        #endregion
                        #region Title
                        br.Color = TitleIconColor;
                        e.Graphics.DrawIcon(ico, br, rtTitleIconBox, DvContentAlignment.MiddleCenter);

                        br.Color = ForeColor;
                        e.Graphics.DrawText(Title, TitleFont, br, rtTitleText, DvContentAlignment.MiddleLeft);
                        #endregion
                        #region Menus
                        Areas2(rtMin, rtMax, rtExit, (menus) =>
                        {
                            foreach (var v in menus)
                            {
                                if (v.Button != null) v.Button.ThemeDraw(e, this, Theme, v.Bounds);
                            }
                        });
                        #endregion
                    }

                    #region ThemeDraw
                    if (Theme != null) OnThemeDraw(e, Theme);
                    #endregion
                    #region Block
                    if (Block)
                    {
                        br.Color = Color.FromArgb(BlockAlpha, Color.Black);
                        e.Graphics.FillRectangle(br, new Rectangle(0, 0, Width, Height));
                    }
                    #endregion
                });
                #endregion
                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            base.OnPaint(e);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
            {
                if (!BlankForm)
                {
                    #region Min/Max/Exit
                    {
                        if (ExitBox && CollisionTool.Check(rtExit, e.X, e.Y)) { bdExit = true; Invalidate(); }
                        if (MaximizeBox && CollisionTool.Check(rtMax, e.X, e.Y)) { bdMax = true; Invalidate(); }
                        if (MinimizeBox && CollisionTool.Check(rtMin, e.X, e.Y)) { bdMin = true; Invalidate(); }
                    }
                    #endregion
                    #region Menus
                    Areas2(rtMin, rtMax, rtExit, (menus) =>
                    {
                        foreach (var v in menus)
                        {
                            if (v.Button != null)
                                if (CollisionTool.Check(v.Bounds, e.Location))
                                    v.Button.MouseDown(e, v.Bounds);
                        }
                    });
                    #endregion
                }
            });
            Invalidate();
            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
            {
                if (!BlankForm)
                {
                    #region Min/Max/Exit
                    {
                        if (ExitBox && bdExit && CollisionTool.Check(rtExit, e.X, e.Y))
                        {
                            this.Close();
                        }
                        if (MaximizeBox && bdMax && CollisionTool.Check(rtMax, e.X, e.Y))
                        {
                            if (WindowState != FormWindowState.Maximized) WindowState = FormWindowState.Maximized;
                            else WindowState = FormWindowState.Normal;
                        }
                        if (MinimizeBox && bdMin && CollisionTool.Check(rtMin, e.X, e.Y))
                        {
                            WindowState = FormWindowState.Minimized;
                        }

                        bdExit = bdMax = bdMin = false;
                        Invalidate();
                    }
                    #endregion
                    #region Menus
                    Areas2(rtMin, rtMax, rtExit, (menus) =>
                    {
                        foreach (var v in menus)
                        {
                            if (v.Button != null)
                                if (CollisionTool.Check(v.Bounds, e.Location))
                                    v.Button.MouseUp(e, v.Bounds);
                        }
                    });
                    #endregion
                }
            });
            Invalidate();
            base.OnMouseUp(e);
        }
        #endregion
        #region OnMouseMove
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Invalidate();
            base.OnMouseMove(e);
        }
        #endregion
        #region OnMouseLeave
        protected override void OnMouseLeave(EventArgs e)
        {
            Invalidate();
            base.OnMouseLeave(e);
        }
        #endregion

        #region OnThemeDraw
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            ThemeDraw?.Invoke(this, new ThemeDrawEventArgs(e.Graphics, e.ClipRectangle, Theme));
        }
        #endregion

        #region WndProc
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (!BlankForm && !DesignMode && Loaded)
            {
                #region WM_LBUTTONDBLCLK
                if (m.Msg == WM_LBUTTONDBLCLK)
                {
                    var x = (short)(m.LParam.ToInt64() & 0xFFFF);
                    var y = (short)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);

                    Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
                    {
                        if (CollisionTool.Check(rtTitleArea, x, y) && MaximizeBox)
                        {
                            if (WindowState == FormWindowState.Normal) WindowState = FormWindowState.Maximized;
                            else if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
                        }
                    });
                }
                #endregion
                #region WM_LBUTTONDOWN
                if (m.Msg == WM_LBUTTONDOWN)
                {
                    var x = (short)(m.LParam.ToInt64() & 0xFFFF);
                    var y = (short)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);

                    Areas((rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent) =>
                    {
                        if (Movable && CollisionTool.Check(rtTitleArea, x, y))
                        {
                            Win32Tool.ReleaseCapture();
                            Win32Tool.SendMessage(Handle, Win32Tool.WM_NCLBUTTONDOWN, Win32Tool.HT_CAPTION, 0);
                        }
                    });
                }
                #endregion

                #region WM_NCHITTEST
                if (m.Msg == WM_NCHITTEST && WindowState != FormWindowState.Maximized && !Fixed)
                {
                    var x = (short)(m.LParam.ToInt64() & 0xFFFF);
                    var y = (short)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                    var pt = PointToClient(new Point(x, y));
                    var clientSize = ClientSize;

                    int GAP = 7;
                    if (pt.X >= clientSize.Width - GAP && pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? HTBOTTOMLEFT : HTBOTTOMRIGHT); return; }
                    if (pt.X <= GAP && pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? HTBOTTOMRIGHT : HTBOTTOMLEFT); return; }
                    if (pt.X <= GAP && pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? HTTOPRIGHT : HTTOPLEFT); return; }
                    if (pt.X >= clientSize.Width - GAP && pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(IsMirrored ? HTTOPLEFT : HTTOPRIGHT); return; }
                    if (pt.Y <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(HTTOP); return; }
                    if (pt.Y >= clientSize.Height - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(HTBOTTOM); return; }
                    if (pt.X <= GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(HTLEFT); return; }
                    if (pt.X >= clientSize.Width - GAP && clientSize.Height >= GAP) { m.Result = (IntPtr)(HTRIGHT); return; }
                }
                #endregion
                #region WM_SIZING
                if (m.Msg == WM_SIZING && !Fixed)
                {
                    if (pbounds.HasValue)
                    {
                        var rc = (DwmTool.RECT)Marshal.PtrToStructure(m.LParam, typeof(DwmTool.RECT));

                        int w = rc.Right - rc.Left;
                        int h = rc.Bottom - rc.Top;
                        var scr = Screen.FromControl(this);
                        var type = m.WParam.ToInt32();

                        switch (type)
                        {
                            case WMSZ_TOP:
                            case WMSZ_TOPLEFT:
                            case WMSZ_TOPRIGHT:
                                {
                                    if (prc_s.Top > scr.WorkingArea.Top && rc.Top <= scr.WorkingArea.Top)
                                        rc.Bottom = scr.WorkingArea.Bottom;
                                    else if (prc_s.Top <= scr.WorkingArea.Top && rc.Top > scr.WorkingArea.Top)
                                    {
                                        if (plast.HasValue)
                                        {
                                            pbounds = plast;
                                            plast = null;
                                        }

                                        rc.Bottom = pbounds.Value.Bottom;
                                    }
                                }
                                break;

                            case WMSZ_BOTTOM:
                            case WMSZ_BOTTOMLEFT:
                            case WMSZ_BOTTOMRIGHT:
                                {
                                    if (prc_s.Bottom < scr.WorkingArea.Bottom && rc.Bottom >= scr.WorkingArea.Bottom)
                                        rc.Top = scr.WorkingArea.Top;
                                    else if (prc_s.Bottom >= scr.WorkingArea.Bottom && rc.Bottom < scr.WorkingArea.Bottom)
                                    {
                                        if (plast.HasValue)
                                        {
                                            pbounds = plast;
                                            plast = null;
                                        }

                                        rc.Top = pbounds.Value.Top;
                                    }
                                }
                                break;
                        }

                        prc_s = rc;
                        Marshal.StructureToPtr(rc, m.LParam, true);
                    }
                }
                #endregion
                #region WM_MOVING
                if (m.Msg == 0x0216 && !Fixed)
                {
                    var ox = 0;
                    var oy = 0;

                    if(pdown.HasValue)
                    {
                        ox = MousePosition.X - pdown.Value.X;
                        oy = MousePosition.X - pdown.Value.Y;
                    }

                    var rc = (DwmTool.RECT)Marshal.PtrToStructure(m.LParam, typeof(DwmTool.RECT));
                    
                    int w = rc.Right - rc.Left;
                    int h = rc.Bottom - rc.Top;
                    var scr = Screen.FromControl(this);
                    var type = m.WParam.ToInt32();

                    if (MousePosition.Y + offsetT <= scr.WorkingArea.Top)
                    {
                        rc.Left = scr.WorkingArea.Left;
                        rc.Right = scr.WorkingArea.Right;
                        rc.Top = scr.WorkingArea.Top;
                        rc.Bottom = scr.WorkingArea.Bottom;
                    }
                    else if (prc_m.Top <= scr.WorkingArea.Top && rc.Top > scr.WorkingArea.Top)
                    {

                        if (plast.HasValue)
                        {
                            pbounds = plast;
                            plast = null;
                        }

                        //rc.Left = MousePosition.X - (pbounds.Value.Width / 2);
                        rc.Left = pbounds.Value.Left + ox;
                        rc.Top = Math.Max(scr.WorkingArea.Top, MousePosition.Y + offsetT);
                        rc.Right = rc.Left + pbounds.Value.Width;
                        rc.Bottom = rc.Top + pbounds.Value.Height;

                    }

                    prc_m = rc;
                    Marshal.StructureToPtr(rc, m.LParam, true);
                }
                #endregion

                #region WM_ENTERSIZEMOVE
                if (m.Msg == WM_ENTERSIZEMOVE)
                {
                    pbounds = Bounds;
                    pdown = MousePosition;

                    var p = MousePosition;
                    offsetT = Bounds.Top - p.Y;
                    offsetB = Bounds.Bottom - p.Y;
                    prc_m = new DwmTool.RECT() { Left = Left, Right = Right, Top = Top, Bottom = Bottom };
                    prc_s = new DwmTool.RECT() { Left = Left, Right = Right, Top = Top, Bottom = Bottom };
                }
                #endregion
                #region WM_EXITSIZEMOVE
                if (m.Msg == WM_EXITSIZEMOVE)
                {
                    plast = pbounds;
                    pbounds = null;
                    pdown = null;
                    offsetT = offsetB = 0;
                }
                #endregion
            }
        }
        #endregion
        #region CreateParams
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    //cp.ExStyle |= 0x02000000;
                }
                return cp;
            }
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        /// <summary>
        /// ( rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent )
        /// </summary>
        /// <param name="act"></param>
        public void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            int wh = Padding.Top;
            int szwh = Padding.Top / 4;
            var rtContent = new RectangleF(Padding.Left, Padding.Top, Width - Padding.Right - Padding.Left, Height - Padding.Bottom - Padding.Top);
            var rtExit = new RectangleF(this.Width - wh, 0, wh, wh);
            var rtMax = new RectangleF(rtExit.X - wh, 0, wh, wh);
            var rtMin = new RectangleF(rtMax.X - wh, 0, wh, wh);
            var rtTitleBar = new RectangleF(0, 0, this.Width, wh);
            var rtTitleIconBox = new RectangleF(0, 0, wh, wh);
            var rtTitleArea = new RectangleF(wh + 15, 0, Width - (wh * (MaximizeBox || MinimizeBox ? 4 : 2)) - MenuGap - (Menus.Count > 0 ? Menus.Sum(x => x.Width) : 0) - 15, Padding.Top);
            var rtTitleText = new RectangleF(rtTitleArea.Left + TitlePadding.Left, rtTitleArea.Top + TitlePadding.Top, rtTitleArea.Width - (TitlePadding.Left + TitlePadding.Right), rtTitleArea.Height - (TitlePadding.Top + TitlePadding.Bottom));

            act(rtTitleBar, rtTitleIconBox, rtTitleArea, rtTitleText, rtMin, rtMax, rtExit, rtContent);
        }

        internal void Areas2(RectangleF rtMin, RectangleF rtMax, RectangleF rtExit, Action<ItemFMI[]> act)
        {
            int wh = Padding.Top;
            var menus = Menus.Select(x => new ItemFMI { Button = x }).ToArray();

            var x = (MaximizeBox || MinimizeBox ? rtMin.Left : rtExit.Left) - MenuGap;

            for (int i = menus.Length - 1; i >= 0; i--)
            {
                x -= menus[i].Button.Width;
                menus[i].Bounds = new RectangleF(x, 0, menus[i].Button.Width, wh);
            }

            act(menus);
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

            notifyIcon.MouseDoubleClick += (o, s) =>
            {
                this.Visible = true;
                this.ShowIcon = true;
                notifyIcon.Visible = false;
            };

        }
        #endregion
        #region GetCallerFormTheme
        public DvTheme GetCallerFormTheme()
        {
            DvTheme ret = null;
            if (Form.ActiveForm is DvForm) ret = ((DvForm)Form.ActiveForm).Theme;
            return ret;
        }
        public Form GetCallerForm()
        {
            Form ret = null;
            if (Form.ActiveForm is DvForm) ret = Form.ActiveForm;
            return ret;
        }
        #endregion
        #region GetNotifyIcon
        public NotifyIcon GetNotifyIcon()
        {
            return notifyIcon;
        }
        #endregion
        #region InvokeMenuSelected
        internal void InvokeMenuSelected(MenuSelectedEventArgs e) => MenuSelected?.Invoke(this, e);
        #endregion
        #region SetExComposited
        public void SetExComposited()
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            style |= WS_EX_COMPOSITED;
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }
        #endregion
        #region ResetExComposited
        public bool ResetExComposited()
        {
            bool ret = false;
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            if ((style & WS_EX_COMPOSITED) == WS_EX_COMPOSITED)
            {
                style ^= WS_EX_COMPOSITED;
                SetWindowLong(this.Handle, GWL_EXSTYLE, style);
                ret = true;
            }
            return ret;
        }
        #endregion
        #endregion
    }


    #region class : DvFormMenuItem
    public abstract class DvFormMenuItem : TextIcon
    {
        #region Properties
        public string Name { get; private set; }
        public int Width { get; set; } = 70;
        public bool PrevSep { get; set; } = false;
        public bool NextSep { get; set; } = false;
        protected bool DownState => bDown;
        #endregion

        #region MenuClicked
        public event MouseEventHandler MenuClicked;
        #endregion

        #region Member Variable
        private bool bDown;
        internal DvForm form;
        #endregion

        #region Constructor
        public DvFormMenuItem(string Name) => this.Name = Name;
        #endregion

        #region ThemeDraw / MouseDown / MouseUp
        internal void ThemeDraw(PaintEventArgs e, DvForm form, DvTheme Theme, RectangleF bounds)
        {
            if (form != null)
            {
                using (var p = new Pen(form.Theme.MenuNormalColor))
                {
                    var cp = MathTool.CenterPoint(bounds);
                    if (PrevSep) e.Graphics.DrawLine(p, bounds.Left, cp.Y - 5, bounds.Left, cp.Y + 5);
                    if (NextSep) e.Graphics.DrawLine(p, bounds.Right, cp.Y - 5, bounds.Right, cp.Y + 5);
                }
            }
            e.Graphics.SetClip(bounds);
            e.Graphics.TranslateTransform(bounds.Left, bounds.Top);
            OnThemeDraw(e, Theme);
            e.Graphics.ResetTransform();
            e.Graphics.ResetClip();
        }

        internal void MouseDown(MouseEventArgs e, RectangleF bounds)
        {
            if (CollisionTool.Check(bounds, e.Location))
            {
                var pt = e.Location;
                OnMouseDown(new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X - bounds.Left), Convert.ToInt32(e.Y - bounds.Top), e.Delta));
                bDown = true;
            }
        }

        internal void MouseUp(MouseEventArgs e, RectangleF bounds)
        {
            if (bDown)
            {
                bDown = false;
                if (CollisionTool.Check(bounds, e.Location)) MenuClicked?.Invoke(this, e);
            }
            OnMouseUp(new MouseEventArgs(e.Button, e.Clicks, Convert.ToInt32(e.X - bounds.Left), Convert.ToInt32(e.Y - bounds.Top), e.Delta));
        }
        #endregion

        #region OnThemeDraw / OnMouseDown / OnMouseUp
        protected virtual void OnThemeDraw(PaintEventArgs e, DvTheme Theme) { }
        protected virtual void OnMouseDown(MouseEventArgs e) { }
        protected virtual void OnMouseUp(MouseEventArgs e) { }
        #endregion
    }
    #endregion
    #region class : DvFormMenuButton
    public class DvFormMenuButton : DvFormMenuItem
    {
        #region Properties
        public Color? ForeColor { get; set; } = null;
        #endregion

        #region Member Variable
        #endregion

        #region Constructor
        public DvFormMenuButton(string name) : base(name) { }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            if (form != null)
            {
                #region Init
                var p = new Pen(Color.Black);
                var br = new SolidBrush(Color.Black);
                #endregion
                #region Var
                var i = form.Menus.IndexOf(this);
                var ForeColor = this.ForeColor ?? form.ForeColor;
                var bounds = new RectangleF(0, 0, Width, form.Padding.Top);
                var cp = MathTool.CenterPoint(bounds);
                #endregion

                #region Draw
                if (DownState) bounds.Offset(0, 1);
                br.Color = ForeColor;
                e.Graphics.DrawTextIcon(this, form.Font, br, bounds);
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : DvFormMenuSelector
    public class DvFormMenuSelector : DvFormMenuItem
    {
        #region Properties
        public Color? NormalColor { get; set; } = null;
        public Color? SelectedColor { get; set; } = null;

        private bool bSelected = false;
        public bool Selected
        {
            get => bSelected;
            set
            {
                if (bSelected != value)
                {
                    bSelected = value;
                    if (bSelected)
                    {
                        if (form != null)
                        {
                            foreach (var m in form.Menus.Where(x => x != this && x is DvFormMenuSelector).Select(x => x as DvFormMenuSelector)) m.Selected = false;
                            form.InvokeMenuSelected(new MenuSelectedEventArgs(this));
                        }
                    }
                    form?.Invalidate();
                }
            }
        }
        #endregion

        #region Member Variable
        #endregion

        #region Constructor
        public DvFormMenuSelector(string name) : base(name) { }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            if (form != null)
            {
                #region Init
                var p = new Pen(Color.Black);
                var br = new SolidBrush(Color.Black);
                #endregion
                #region Var
                var i = form.Menus.IndexOf(this);
                var NormalColor = this.NormalColor ?? form.Theme.MenuNormalColor;
                var SelectedColor = this.SelectedColor ?? form.Theme.MenuSelectedColor;
                var bounds = new RectangleF(0, 0, Width, form.Padding.Top);
                var cp = MathTool.CenterPoint(bounds);
                var c = (Selected ? SelectedColor : NormalColor);
                #endregion

                #region Draw
                if (DownState) bounds.Offset(0, 1);
                br.Color = c;
                e.Graphics.DrawTextIcon(this, form.Font, br, bounds);
                #endregion

                #region Dispose
                br.Dispose();
                p.Dispose();
                #endregion
            }
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (form != null)
            {
                var bounds = new RectangleF(0, 0, Width, form.Padding.Top);

                if (CollisionTool.Check(bounds, e.Location))
                {
                    this.Selected = true;
                }
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : ItemFMI
    internal class ItemFMI
    {
        public RectangleF Bounds { get; set; }
        public DvFormMenuItem Button { get; set; }
    }
    #endregion



}
