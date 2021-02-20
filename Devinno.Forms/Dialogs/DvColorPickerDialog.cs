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
            #region Control Event
            inR.OriginalTextBox.TextChanged += OriginalTextBox_TextChanged;
            inG.OriginalTextBox.TextChanged += OriginalTextBox_TextChanged;
            inB.OriginalTextBox.TextChanged += OriginalTextBox_TextChanged;

            inR.OriginalTextBox.LostFocus += (o, s) => inR.Value = Color.R.ToString();
            inG.OriginalTextBox.LostFocus += (o, s) => inG.Value = Color.G.ToString();
            inB.OriginalTextBox.LostFocus += (o, s) => inB.Value = Color.B.ToString();
            #endregion
            #region Set
            inR.OriginalTextBox.MaxLength = inG.OriginalTextBox.MaxLength = inB.OriginalTextBox.MaxLength = 3;
            #endregion

            Fixed = true;

            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Event Handler
        #region Draw_MouseUp
        private void Draw_MouseUp(object sender, MouseEventArgs e)
        {
            var inv = false;
            #region Bounds
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (bHueDown)
            {
                var nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom)); 
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                var c = vc2.ToRGB();

                this.Color = c;
                this.nHueDownY = nHueDownY;
                this.Hue = this.Color.GetHue();
                //this.nColorX = nColorX;
                //this.nColorY = nColorY;

                bHueDown = false;
                bHueChange = true;
                inv = true;
            }
            #endregion
            #region Color
            if (bColorDown)
            {
                int nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                int nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
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
                //this.nHueDownY = nHueDownY;
                //this.Hue = this.Color.GetHue();
                this.nColorX = nColorX;
                this.nColorY = nColorY;

                bColorDown = false;
                inv |= true;
            }
            #endregion

            if (inv)
            {
                Set();
                draw.Invalidate();
            }
        }
        #endregion
        #region Draw_MouseMove
        private void Draw_MouseMove(object sender, MouseEventArgs e)
        {
            var inv = false;
            #region Bounds
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (bHueDown)
            {
                var nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom));
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                var c = vc2.ToRGB();

                this.Color = c;
                this.nHueDownY = nHueDownY;
                this.Hue = this.Color.GetHue();
                //this.nColorX = nColorX;
                //this.nColorY = nColorY;

                bHueChange = true;
                inv = true;
            }
            #endregion
            #region Color
            if (bColorDown)
            {
                int nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                int nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
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
                //this.nHueDownY = nHueDownY;
                //this.Hue = this.Color.GetHue();
                this.nColorX = nColorX;
                this.nColorY = nColorY;

                inv |= true;
            }
            #endregion

            if (inv)
            {
                Set();
                draw.Invalidate();
            }
        }
        #endregion
        #region Draw_MouseDown
        private void Draw_MouseDown(object sender, MouseEventArgs e)
        {
            var inv = false;
            #region Bounds
            var NW = 30;
            var rtContent = draw.GetContentBounds();
            var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
            #endregion
            #region Hue
            if (CollisionTool.Check(rtHue, e.Location))
            {
                var nHueDownY = Convert.ToInt32(MathTool.Constrain(e.Y, rtHue.Top, rtHue.Bottom));
                var i = Convert.ToInt32(MathTool.Constrain(nHueDownY - rtHue.Y, 0, rtColor.Height));
                var vc = new HsvColor() { A = 1, H = MathTool.Map(i, 0D, rtHue.Height, 0D, 360D), S = 1, V = 1 };
                var vc2 = Color.ToHSV();
                vc2.H = vc.H;
                var c = vc2.ToRGB();

                this.Color = c;
                this.nHueDownY = nHueDownY;
                this.Hue = this.Color.GetHue();
                //this.nColorX = nColorX;
                //this.nColorY = nColorY;

                bHueDown = true;
                bHueChange = true;
                inv |= true;
            }
            #endregion
            #region Color
            if (CollisionTool.Check(rtColor, e.Location))
            {
                int nColorY = Convert.ToInt32(MathTool.Constrain(e.Y, rtColor.Top, rtColor.Bottom));
                int nColorX = Convert.ToInt32(MathTool.Constrain(e.X, rtColor.Left, rtColor.Right));
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
                //this.nHueDownY = nHueDownY;
                //this.Hue = this.Color.GetHue();
                this.nColorX = nColorX;
                this.nColorY = nColorY;

                bColorDown = true;
                inv |= true;
            }
            #endregion

            if (inv)
            {
                Set();
                draw.Invalidate();
            }
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
            var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
            var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
            var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
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
                e.Graphics.SetClip(new Rectangle(rtHue.X - 1, rtHue.Y - 1, rtHue.Width + 2, rtHue.Height + 2));

                p.Width = 1;

                p.Color = Color.FromArgb(120, Color.Black);
                e.Graphics.DrawLine(p, rtHue.Left + 1, nHueDownY + 1, rtHue.Right - 1 + 1, nHueDownY + 1);

                p.Color = Color.White; 
                e.Graphics.DrawLine(p, rtHue.Left, nHueDownY, rtHue.Right - 1, nHueDownY);

                e.Graphics.ResetClip();
            }

            //if (bColorDown)
            {
                e.Graphics.SetClip(new Rectangle(rtColor.X - 1, rtColor.Y - 1, rtColor.Width + 2, rtColor.Height + 2));

                p.Width = 1; 
                int n = Convert.ToInt32(DpiRatio * 5);

                p.Color = Color.FromArgb(120, Color.Black);
                e.Graphics.DrawLine(p, nColorX - n + 1, nColorY + 1, nColorX + n + 1, nColorY + 1);
                e.Graphics.DrawLine(p, nColorX + 1, nColorY - n + 1, nColorX + 1, nColorY + n + 1);

                p.Color = Color.White;
                e.Graphics.DrawLine(p, nColorX - n, nColorY, nColorX + n, nColorY);
                e.Graphics.DrawLine(p, nColorX, nColorY - n, nColorX, nColorY + n);

                e.Graphics.ResetClip();
            }
            #endregion
            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
        }
        #endregion
        #region OriginalTextBox_TextChanged
        private void OriginalTextBox_TextChanged(object sender, EventArgs e)
        {
            byte r, g, b;
            if (!(bColorDown || bHueDown) && byte.TryParse(inR.Value, out r) && byte.TryParse(inG.Value, out g) && byte.TryParse(inB.Value, out b))
            {
                Color = Color.FromArgb(r, g, b);
                #region Set
                {
                    #region Bounds
                    var NW = 30;
                    var rtContent = draw.GetContentBounds();
                    var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
                    var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
                    var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
                    #endregion

                    Hue = Color.GetHue();
                    var vc = Color.ToHSV();

                    var nHueDownY = Convert.ToInt32(MathTool.Map(Hue, 0D, 360D, rtHue.Top, rtHue.Bottom));
                    var nColorX = Convert.ToInt32(MathTool.Map(vc.S, 0D, 1D, rtColor.Left, rtColor.Right));
                    var nColorY = Convert.ToInt32(MathTool.Map(vc.V, 0D, 1D, rtColor.Bottom, rtColor.Top));

                    bool change = false;
                    if (this.nHueDownY != nHueDownY) { this.nHueDownY = nHueDownY; change = true; bHueChange = true; }
                    if (this.nColorX != nColorX) { this.nColorX = nColorX; change = true; }
                    if (this.nColorY != nColorY) { this.nColorY = nColorY; change = true; }

                    if (change) draw.Invalidate();

                    lblColor.LabelColor = Color;
                }
                #endregion
            }
        }
        #endregion
        #endregion

        #region Method
        #region Set
        void Set()
        {
            var c = this.Color;
            lblColor.LabelColor = c;
            inR.Value = c.R.ToString();
            inG.Value = c.G.ToString();
            inB.Value = c.B.ToString();
        }
        #endregion
        #region ShowColorPicker
        public Color? ShowColorPicker(Color? color = null)
        {
            Color? ret = null;
           
            Theme = GetCallerFormTheme() ?? Theme;

            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);
            
            foreach (var c in tbl.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(300 * f), Convert.ToInt32(350 * f));

            lblColor.Width = inR.TitleWidth = inG.TitleWidth = inB.TitleWidth = Convert.ToInt32(30 * f);
            #endregion

            #region Set
            {
                #region Bounds
                var NW = 30;
                var rtContent = draw.GetContentBounds();
                var rt = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width, rtContent.Height);
                var rtHue = new Rectangle(rt.Right - NW, rt.Y, NW, rt.Height);
                var rtColor = new Rectangle(rt.X, rt.Y, rt.Width - 10 - rtHue.Width, rt.Height);
                #endregion

                Color = color.HasValue? color.Value : Color.White;
                Set();
            }
            #endregion
           
            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = this.Color;
            }
            return ret;
        }
        #endregion
        #endregion
    }
}
