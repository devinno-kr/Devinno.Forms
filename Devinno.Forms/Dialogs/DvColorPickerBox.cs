using Devinno.Extensions;
using Devinno.Forms.Controls;
using Devinno.Forms.Tools;
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
    public partial class DvColorPickerBox : DvForm
    {
        #region Properties
        public Color SelectedColor
        {
            get
            {
                var c = new HsvColor() { A = 1, H = nH, S = nS, V = nV };
                return c.ToRGB();
            }
        }

        public DvButton ButtonOK => btnOK;
        public DvButton ButtonCancel => btnCancel;
        #endregion

        #region Member Variable
        double nH, nS, nV;
        bool bDownHue, bDownColor;
        bool ignore = false;
        byte[] ba = new byte[256 * 256 * 4];
        #endregion

        #region Constructor
        public DvColorPickerBox()
        {
            InitializeComponent();

            #region hue.ThemeDraw
            hue.ThemeDraw += (o, s) => 
            {
                var bm = ResourceTool.saturation;
                s.Graphics.DrawImage(bm, new Rectangle(0, 0, bm.Width, bm.Height));

                var hy = Convert.ToInt32(MathTool.Map(nH, 360.0, 0.0, 0.0, 255.0));
                using (var p = new Pen(Color.Black, 1))
                {
                    p.Color = Color.White;
                    s.Graphics.DrawLine(p, 0, hy, 18, hy);
                    
                    p.Color = Color.Black; 
                    s.Graphics.DrawLine(p, 0, hy - 1, 18, hy - 1);
                    s.Graphics.DrawLine(p, 0, hy + 1, 18, hy + 1);
                }
            };
            #endregion
            #region hue.MouseDown
            hue.MouseDown += (o, s) =>
            {
                bDownHue = true;

                nH = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 360.0, 0.0));
                HueSet();
            };
            #endregion
            #region hue.MouseMove
            hue.MouseMove += (o, s) =>
            {
                if (bDownHue)
                {
                    nH = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 360.0, 0.0));
                    HueSet();
                }
            };
            #endregion
            #region hue.MouseUp
            hue.MouseUp += (o, s) =>
            {
                if (bDownHue)
                {
                    nH = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 360.0, 0.0));
                    HueSet();

                    bDownHue = false;
                }
            };
            #endregion

            #region color.ThemeDraw
            color.ThemeDraw += (o, s) =>
            {
                using (var bm = new Bitmap(256, 256))
                {
                    var bmpData = bm.LockBits(new Rectangle(0, 0, 256, 256), ImageLockMode.ReadWrite, bm.PixelFormat);
                    var ptr = bmpData.Scan0;
                    var bytes = Math.Abs(bmpData.Stride) * bm.Height;

                    Marshal.Copy(ba, 0, ptr, bytes);
                    bm.UnlockBits(bmpData);

                    s.Graphics.DrawImage(bm, new Rectangle(0, 0, 256, 256));
                }

                using (var p = new Pen(Color.Black, 1))
                {
                    var x = Convert.ToInt32(MathTool.Map(nS, 0, 1.0, 0, 255));
                    var y = Convert.ToInt32(MathTool.Map(nV, 0, 1.0, 255, 0));

                    p.Width = 1;

                    p.Color = Color.Black;
                    s.Graphics.DrawLine(p, x - 4 + 1, y + 1, x + 4 + 1, y + 1);
                    s.Graphics.DrawLine(p, x + 1, y - 4 + 1, x + 1, y + 4 + 1);

                    p.Color = Color.White;
                    s.Graphics.DrawLine(p, x - 4, y, x + 4, y);
                    s.Graphics.DrawLine(p, x, y - 4, x, y + 4);

                }
            };
            #endregion
            #region color.MouseDown 
            color.MouseDown += (o, s) =>
            {
                bDownColor = true;

                nS = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.X, 0, 255), 0.0, 255.0, 0.0, 1.0));
                nV = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 1.0, 0.0));

                Set(true);
            };
            #endregion
            #region color.MouseUp
            color.MouseUp += (o, s) =>
            {
                if (bDownColor)
                {
                    nS = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.X, 0, 255), 0.0, 255.0, 0.0, 1.0));
                    nV = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 1.0, 0.0));

                    Set(true);

                    bDownColor = false;
                }
            };
            #endregion
            #region color.MouseMove
            color.MouseMove += (o, s) =>
            {
                if (bDownColor)
                {
                    nS = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.X, 0, 255), 0.0, 255.0, 0.0, 1.0));
                    nV = Convert.ToSingle(MathTool.Map(MathTool.Constrain(s.Y, 0, 255), 0.0, 255.0, 1.0, 0.0));

                    Set(true);
                }
            };
            #endregion

            btnOK.ButtonClick += (o, s) => DialogResult = DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;

            SetExComposited();
        }
        #endregion

        #region Method
        #region Set
        void Set(bool set = false)
        {
            lbl.LabelColor = SelectedColor;
            if (set) ignore = true;
            inR.Value = SelectedColor.R;
            inG.Value = SelectedColor.G;
            inB.Value = SelectedColor.B;

            hue.Invalidate();
            color.Invalidate();

            ignore = false;
        }
        #endregion
        #region HueSet
        void HueSet()
        {
            var H = 256;
            var W = 256;
            var r = Parallel.For(0, H * W, (iv) =>
            {
                int y = iv / W;
                int x = iv - (y * W);
                int numBytes = (y * (W * 4)) + (x * 4);

                var s = Convert.ToSingle(MathTool.Map(x, 0.0, 255, 0.0, 1.0));
                var v = Convert.ToSingle(MathTool.Map(y, 0.0, 255, 1.0, 0.0));
                var hsv = new HsvColor() { A = 1, H = nH, S = s, V = v };
                var c = hsv.ToRGB();

                ba[numBytes] = c.B;
                ba[numBytes + 1] = c.G;
                ba[numBytes + 2] = c.R;
                ba[numBytes + 3] = c.A;
            });
            while (!r.IsCompleted) System.Threading.Thread.Sleep(1);

            Set(true);
        }
        #endregion

        #region ShowColorPicker
        public Color? ShowColorPicker(string Title, Color? value = null)
        {
            Theme = GetCallerFormTheme();

            Color? ret = null;

            var vc = value ?? Color.White;
            var hsv = vc.ToHSV();
            nH = hsv.H;
            nS = hsv.S;
            nV = hsv.V;

            this.Title = this.Text = Title;
            HueSet();
            
            if(this.ShowDialog() == DialogResult.OK)
            {
                ret = SelectedColor;
            }

            return ret;
        }
        #endregion
        #endregion
    }
}
