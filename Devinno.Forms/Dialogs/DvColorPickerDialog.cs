using Devinno.Extensions;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvColorPickerDialog : DvForm
    {
        #region Member Variable
        double Hue;
        bool bHueDown = false;
        int nHueDownY = 0;

        Color Color = Color.Black;
        bool bColorDown = false;
        int nColorX = 0, nColorY = 0;

        Timer tmr = new Timer() { Interval = 10 };

        Size sz;
        Bitmap bmHue, bmColor;
        bool bHueChange;

        DvColorBox ColorBox = new DvColorBox();
        DvMessageBox msg = new DvMessageBox();
        #endregion

        #region Constructor
        public DvColorPickerDialog()
        {
            InitializeComponent();

            #region Draw Event
            draw.ThemeDraw += Draw_ThemeDraw;
            draw.MouseDown += Draw_MouseDown;
            draw.MouseMove += Draw_MouseMove;
            draw.MouseUp += Draw_MouseUp;
            #endregion
            #region Timer Event
            tmr.Tick += (o, s) =>
            {
                var c = this.Color;
                lblColor.LabelColor = c;
                lblR.Value = c.R.ToString();
                lblG.Value = c.G.ToString();
                lblB.Value = c.B.ToString();
            };
            tmr.Enabled = true;
            #endregion
            #region Control Event
            lblR.MouseClick += Input;
            lblG.MouseClick += Input;
            lblB.MouseClick += Input;
            #endregion

            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Event Handler
        #region Draw_MouseUp
        private void Draw_MouseUp(object sender, MouseEventArgs e)
        {
            #region Bounds
            var inv = false;
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (bHueDown)
            {
                nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom)); 
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                Hue = vc.ToRGB().GetHue();

                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                Color = vc2.ToRGB();

                bHueDown = false;
                bHueChange = true;
                inv = true;
            }
            #endregion
            #region Color
            if (bColorDown)
            {
                nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
                int nw = rtColor.Width;
                int nh = rtColor.Height;
                int y = nColorY - rtColor.Y;
                int x = nColorX - rtColor.X;
                int numBytes = (y * (nw * 4)) + (x * 4);

                double s = MathTool.Map(x, 0.0, nw, 0.0, 1.0);
                double v = MathTool.Map(y, 0.0, nh, 1.0, 0.0);
                var vc = new HsvColor() { H = Hue, S = s, V = v, A = 1 };
                var c = vc.ToRGB();

                this.Color = c;

                bColorDown = false;
                inv |= true;
            }
            #endregion

            if (inv) draw.Invalidate();
        }
        #endregion
        #region Draw_MouseMove
        private void Draw_MouseMove(object sender, MouseEventArgs e)
        {
            #region Bounds
            var inv = false;
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (bHueDown)
            {
                nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom));
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                Hue = vc.ToRGB().GetHue();

                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                Color = vc2.ToRGB();

                bHueChange = true;
                inv = true;
            }
            #endregion
            #region Color
            if (bColorDown)
            {
                nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
                int nw = rtColor.Width;
                int nh = rtColor.Height;
                int y = nColorY - rtColor.Y;
                int x = nColorX - rtColor.X;
                int numBytes = (y * (nw * 4)) + (x * 4);

                double s = MathTool.Map(x, 0.0, nw, 0.0, 1.0);
                double v = MathTool.Map(y, 0.0, nh, 1.0, 0.0);
                var vc = new HsvColor() { H = Hue, S = s, V = v, A = 1 };
                var c = vc.ToRGB();

                this.Color = c;

                inv |= true;
            }
            #endregion

            if (inv) draw.Invalidate();
        }
        #endregion
        #region Draw_MouseDown
        private void Draw_MouseDown(object sender, MouseEventArgs e)
        {
            #region Bounds
            var inv = false;
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (CollisionTool.Check(rtHue, e.Location))
            {
                nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom));
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                Hue = vc.ToRGB().GetHue();

                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                Color = vc2.ToRGB();

                bHueDown = true;
                bHueChange = true;
                inv |= true;
            }
            #endregion
            #region Color
            if (CollisionTool.Check(rtColor, e.Location))
            {
                nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
                int nw = rtColor.Width;
                int nh = rtColor.Height;
                int y = nColorY - rtColor.Y;
                int x = nColorX - rtColor.X;
                int numBytes = (y * (nw * 4)) + (x * 4);

                double s = MathTool.Map(x, 0.0, nw, 0.0, 1.0);
                double v = MathTool.Map(y, 0.0, nh, 1.0, 0.0);
                var vc = new HsvColor() { H = Hue, S = s, V = v, A = 1 };
                var c = vc.ToRGB();

                this.Color = c;

                bColorDown = true;
                inv |= true;
            }
            #endregion

            if (inv) draw.Invalidate();
        }
        #endregion
        #region Draw_ThemeDraw
        private void Draw_ThemeDraw(object sender, Controls.ThemeDrawEventArgs de)
        {
            #region Args
            var e = de.PaintArgs;
            var Theme = de.Theme;
            #endregion
            #region Init
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Bounds
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
            #endregion
            #region Make
            if (sz != this.Size || bmColor == null || bmHue == null || bHueChange)
            {
                #region Hue 
                if(!bHueChange)
                {
                    if (bmHue != null) bmHue.Dispose();
                    bmHue = new Bitmap(rtHue.Width, rtHue.Height);

                    var bmp = bmHue;
                    var bmpData = bmp.LockBits(new Rectangle(0, 0, rtHue.Width, rtHue.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    var ptr = bmpData.Scan0;
                    var bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                    var rgbValues = new byte[bytes];
                    int nw = bmp.Width;
                    int nh = bmp.Height;

                    var r = Parallel.For(0, nh * nw, (iv) =>
                    {
                        int y = iv / nw;
                        int x = iv - (y * nw);
                        int numBytes = (y * (nw * 4)) + (x * 4);

                        var vc = new HsvColor() { A = 1, H = MathTool.Map(y, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                        var c = vc.ToRGB();
                        rgbValues[numBytes] = c.B;
                        rgbValues[numBytes + 1] = c.G;
                        rgbValues[numBytes + 2] = c.R;
                        rgbValues[numBytes + 3] = c.A;
                    });

                    while (!r.IsCompleted) System.Threading.Thread.Sleep(1);

                    Marshal.Copy(rgbValues, 0, ptr, bytes);
                    bmp.UnlockBits(bmpData);
                }
                #endregion
                #region Color 
                {
                    if (bmColor != null) bmColor.Dispose();
                    bmColor = new Bitmap(rtColor.Width, rtColor.Height);

                    var bmp = bmColor;
                    var bmpData = bmp.LockBits(new Rectangle(0, 0, rtColor.Width, rtColor.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                    var ptr = bmpData.Scan0;
                    var bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                    var rgbValues = new byte[bytes];
                    int nw = bmp.Width;
                    int nh = bmp.Height;

                    var r = Parallel.For(0, nh * nw, (iv) =>
                    {
                        int y = iv / nw;
                        int x = iv - (y * nw);
                        int numBytes = (y * (nw * 4)) + (x * 4);

                        double s = MathTool.Map(x, 0.0, nw, 0.0, 1.0);
                        double v = MathTool.Map(y, 0.0, nh, 1.0, 0.0);
                        var vc = new HsvColor() { H = Hue, S = s, V = v, A = 1 };
                        var c = vc.ToRGB();
                        rgbValues[numBytes] = c.B;
                        rgbValues[numBytes + 1] = c.G;
                        rgbValues[numBytes + 2] = c.R;
                        rgbValues[numBytes + 3] = c.A;
                    });

                    while (!r.IsCompleted) System.Threading.Thread.Sleep(1);

                    Marshal.Copy(rgbValues, 0, ptr, bytes);
                    bmp.UnlockBits(bmpData);

                    bHueChange = false;
                }
                #endregion
                #region Set
                sz = this.Size;
                #endregion
            }
            #endregion
            #region Draw

            e.Graphics.DrawImage(bmHue, rtHue);
            e.Graphics.DrawImage(bmColor, rtColor);

            //if (bHueDown)
            {
                p.Width = 1;

                p.Color = Color.FromArgb(120, Color.Black);
                e.Graphics.DrawLine(p, rtHue.Left + 1, nHueDownY + 1, rtHue.Right - 1 + 1, nHueDownY + 1);

                p.Color = Color.White; 
                e.Graphics.DrawLine(p, rtHue.Left, nHueDownY, rtHue.Right - 1, nHueDownY);
            }

            //if (bColorDown)
            {
                p.Width = 1; 
                int n = Convert.ToInt32(DpiRatio * 5);

                p.Color = Color.FromArgb(120, Color.Black);
                e.Graphics.DrawLine(p, nColorX - n + 1, nColorY + 1, nColorX + n + 1, nColorY + 1);
                e.Graphics.DrawLine(p, nColorX + 1, nColorY - n + 1, nColorX + 1, nColorY + n + 1);

                p.Color = Color.White;
                e.Graphics.DrawLine(p, nColorX - n, nColorY, nColorX + n, nColorY);
                e.Graphics.DrawLine(p, nColorX, nColorY - n, nColorX, nColorY + n);
            }
            #endregion
            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
        }
        #endregion
        #endregion

        #region Input
        void Input(object o, MouseEventArgs s)
        {
            if (!ColorBox.Visible)
            {
                var ret = ColorBox.ShowColorBox(Color);
                if (ret.HasValue)
                {
                    Color = ret.Value;

                    #region Set
                    {
                        #region Bounds
                        var NW = 30;
                        var rtContent = draw.GetContentBounds();
                        var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
                        var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
                        var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
                        #endregion

                        Hue = Color.GetHue();
                        var vc = Color.ToHSV();

                        nHueDownY = Convert.ToInt32(MathTool.Map(Hue, 0D, 360D, rtHue.Top, rtHue.Bottom));

                        nColorX = Convert.ToInt32(MathTool.Map(vc.S, 0D, 1D, rtColor.Left, rtColor.Right));
                        nColorY = Convert.ToInt32(MathTool.Map(vc.V, 0D, 1D, rtColor.Bottom, rtColor.Top));
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region ShowColorPicker
        public Color? ShowColorPicker(Color? color = null)
        {
            Color? ret = null;
           
            Theme = GetCallerFormTheme() ?? Theme;

            #region Set
            {
                #region Bounds
                var NW = 30;
                var rtContent = draw.GetContentBounds();
                var rt = new Rectangle(rtContent.X, rtContent.Y + 4, rtContent.Width, rtContent.Height - 4); rt.Inflate(-3, -3);
                var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
                var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 8 - rtHue.Width, rt.Height);
                #endregion

                Color = color.HasValue? color.Value : Color.White;
                Hue = Color.GetHue();
                var vc = Color.ToHSV();

                nHueDownY = Convert.ToInt32(MathTool.Map(Hue, 0D, 360D, rtHue.Top, rtHue.Bottom));

                nColorX = Convert.ToInt32(MathTool.Map(vc.S, 0D, 1D, rtColor.Left, rtColor.Right));
                nColorY = Convert.ToInt32(MathTool.Map(vc.V, 0D, 1D, rtColor.Bottom, rtColor.Top));
            }
            #endregion
           
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = this.Color;
            }
            return ret;
        }
        #endregion
    }
}
