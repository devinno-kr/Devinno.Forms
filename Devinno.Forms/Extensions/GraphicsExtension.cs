using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Devinno.Forms.Tools;
using Devinno.Forms.Enums;
using System.Globalization;
using Devinno.Forms.Icons;
using System.Linq;

namespace Devinno.Forms.Extensions
{
    public static class GraphicsExtension
    {
        #region Properties
        internal static PrivateFontCollection FontAwesome { get; private set; }
        #endregion

        #region Constructor
        static GraphicsExtension()
        {
            #region FontAwesome
            FontAwesome = new PrivateFontCollection();

            var baBrands = Properties.Resources.fa_5_Brands_Regular_400;
            var baRegular = Properties.Resources.fa_5_Free_Regular_400;
            var baSolid = Properties.Resources.fa_Free_Solid_900;

            IntPtr pBrands = Marshal.AllocHGlobal(baBrands.Length);
            IntPtr pRegular = Marshal.AllocHGlobal(baRegular.Length);
            IntPtr pSolid = Marshal.AllocHGlobal(baSolid.Length);

            Marshal.Copy(baBrands, 0, pBrands, baBrands.Length);
            Marshal.Copy(baRegular, 0, pRegular, baRegular.Length);
            Marshal.Copy(baSolid, 0, pSolid, baSolid.Length);

            FontAwesome.AddMemoryFont(pBrands, baBrands.Length);
            FontAwesome.AddMemoryFont(pRegular, baRegular.Length);
            FontAwesome.AddMemoryFont(pSolid, baSolid.Length);

            Marshal.FreeHGlobal(pBrands);
            Marshal.FreeHGlobal(pRegular);
            Marshal.FreeHGlobal(pSolid);
            #endregion
        }
        #endregion

        #region Method
        #region DrawRectangle
        public static void DrawRectangle(this Graphics g, Pen p, RectangleF rt) => g.DrawRectangle(p, rt.X, rt.Y, rt.Width, rt.Height);
        #endregion
        #region FillRoundRectangle
        public static void FillRoundRectangleLT(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleLT(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleRT(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleRT(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleLB(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleLB(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleRB(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleRB(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleT(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleT(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleB(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleB(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleL(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleL(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangleR(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangleR(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangle(this Graphics g, System.Drawing.Brush brush, Rectangle rect, int radius) => g.FillRoundRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void FillRoundRectangle(this Graphics g, System.Drawing.Brush brush, RectangleF rect, int radius) => g.FillRoundRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, radius);

        public static void FillRoundRectangle(this Graphics g, System.Drawing.Brush brush, int x, int y, int width, int height, int radius) => g.FillRoundRectangle(brush, (float)x, (float)y, (float)width, (float)height, (float)radius);

        public static void FillRoundRectangle(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPath(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleT(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathT(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleB(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathB(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleL(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathL(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleR(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathR(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleLT(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathLT(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleRT(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathRT(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleLB(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathLB(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        public static void FillRoundRectangleRB(this Graphics g, System.Drawing.Brush brush, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathRB(new RectangleF(x, y, width, height), radius)) { g.FillPath(brush, path); } }
        #endregion
        #region DrawRoundRectangle
        public static void DrawRoundRectangleLT(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleLT(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleRT(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleRT(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleLB(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleLB(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleRB(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleRB(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleL(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleL(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleR(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleR(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleT(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleT(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangleB(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangleB(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangle(this Graphics g, System.Drawing.Pen pen, RectangleF rect, int radius) => g.DrawRoundRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);
        public static void DrawRoundRectangle(this Graphics g, System.Drawing.Pen pen, Rectangle rect, int radius) => g.DrawRoundRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, radius);

        public static void DrawRoundRectangle(this Graphics g, System.Drawing.Pen pen, int x, int y, int width, int height, int radius) => g.DrawRoundRectangle(pen, (float)x, (float)y, (float)width, (float)height, (float)radius);

        public static void DrawRoundRectangle(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPath(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleT(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathT(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleB(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathB(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleL(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathL(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleR(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathR(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleLT(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathLT(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleRT(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathRT(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleLB(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathLB(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        public static void DrawRoundRectangleRB(this Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height, float radius) { using (var path = DrawingTool.GetRoundRectPathRB(new RectangleF(x, y, width, height), radius)) { g.DrawPath(pen, path); } }
        #endregion

        #region DrawText
        public static void DrawText(this Graphics g, string Text, Font Font, Brush br, Rectangle Bounds, DvContentAlignment Align, StringFormat strfrm = null) => DrawText(g, Text, Font, br, new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height), Align, strfrm);
        public static void DrawText(this Graphics g, string Text, Font Font, Brush br, RectangleF Bounds, DvContentAlignment Align, StringFormat strfrm = null)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var sz = g.MeasureString(Text, Font);
                float x = Bounds.X, y = Bounds.Y, w = sz.Width, h = sz.Height;
                switch (Align)
                {
                    case DvContentAlignment.TopLeft:        /**/    x = Bounds.X;                                   /**/    y = Bounds.Y;                                   /**/    break;
                    case DvContentAlignment.TopCenter:      /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Y;                                   /**/    break;
                    case DvContentAlignment.TopRight:       /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Y;                                   /**/    break;
                    case DvContentAlignment.MiddleLeft:     /**/    x = Bounds.X;                                   /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    break;
                    case DvContentAlignment.MiddleCenter:   /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    break;
                    case DvContentAlignment.MiddleRight:    /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    break;
                    case DvContentAlignment.BottomLeft:     /**/    x = Bounds.X;                                   /**/    y = Bounds.Bottom - h;                          /**/    break;
                    case DvContentAlignment.BottomCenter:   /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Bottom - h;                          /**/    break;
                    case DvContentAlignment.BottomRight:    /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Bottom - h;                          /**/    break;
                }

                if (strfrm != null) g.DrawString(Text, Font, br, new RectangleF(x, y, w, h));
                else g.DrawString(Text, Font, br, new RectangleF(x, y, w, h), strfrm);
            }
        }
        #endregion

        #region ToChar
        internal static char ToChar<TEnum>(this TEnum icon, IFormatProvider formatProvider = null) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            return char.ConvertFromUtf32(icon.ToInt32(formatProvider ?? CultureInfo.InvariantCulture)).Single();
        }
        #endregion
        #region DrawIconFA
        public static void DrawIconFA(this Graphics g, DvIcon icon, Brush br, Rectangle bounds, StringFormat strfrm = null)
        {
            using (var ft = new Font(FontAwesome.Families[(int)icon.StyleFA], icon.IconSize, FontStyle.Regular))
            {
                var text = icon.IconFA.ToChar(CultureInfo.InvariantCulture).ToString();

                if (strfrm == null) g.DrawString(text, ft, br, bounds);
                else g.DrawString(text, ft, br, bounds, strfrm);
            }
        }
        #endregion
        #region DrawTextIconFA
        public static void DrawTextIconFA(this Graphics g, DvIcon Icon, string Text, Font Font, Brush br, Rectangle Bounds, DvContentAlignment Align, StringFormat strfrm = null)
        {
            using (var FontFA = new Font(FontAwesome.Families[(int)Icon.StyleFA], Icon.IconSize, FontStyle.Regular))
            {
                var TextFA = Icon.IconFA.ToChar(CultureInfo.InvariantCulture).ToString();

                if (Icon.Alignment == DvTextIconAlignment.LeftRight)
                {
                    float gap = string.IsNullOrWhiteSpace(Text) ? 0 : Icon.Gap;
                    SizeF sz = g.MeasureString(Text, Font);
                    SizeF szFA = g.MeasureString(TextFA, FontFA);
                    float tx = Bounds.X, ty = Bounds.Y, tw = sz.Width, th = sz.Height;
                    float ix = Bounds.X, iy = Bounds.Y, iw = szFA.Width, ih = szFA.Height;

                    switch (Align)
                    {
                        case DvContentAlignment.TopLeft:        /**/    ix = Bounds.X;                                                      /**/    iy = Bounds.Y;                                  /**/    break;
                        case DvContentAlignment.TopCenter:      /**/    ix = Bounds.X + ((Bounds.Width / 2F) - ((iw + gap + tw) / 2F));     /**/    iy = Bounds.Y;                                  /**/    break;
                        case DvContentAlignment.TopRight:       /**/    ix = Bounds.Right - (iw + gap + tw);                                /**/    iy = Bounds.Y;                                  /**/    break;
                        case DvContentAlignment.MiddleLeft:     /**/    ix = Bounds.X;                                                      /**/    iy = Bounds.Y + (Bounds.Height / 2F - ih / 2F); /**/    break;
                        case DvContentAlignment.MiddleCenter:   /**/    ix = Bounds.X + ((Bounds.Width / 2F) - ((iw + gap + tw) / 2F));     /**/    iy = Bounds.Y + (Bounds.Height / 2F - ih / 2F); /**/    break;
                        case DvContentAlignment.MiddleRight:    /**/    ix = Bounds.Right - (iw + gap + tw);                                /**/    iy = Bounds.Y + (Bounds.Height / 2F - ih / 2F); /**/    break;
                        case DvContentAlignment.BottomLeft:     /**/    ix = Bounds.X;                                                      /**/    iy = Bounds.Bottom - ih;                        /**/    break;
                        case DvContentAlignment.BottomCenter:   /**/    ix = Bounds.X + ((Bounds.Width / 2F) - ((iw + gap + tw) / 2F));     /**/    iy = Bounds.Bottom - ih;                        /**/    break;
                        case DvContentAlignment.BottomRight:    /**/    ix = Bounds.Right - (iw + gap + tw);                                /**/    iy = Bounds.Bottom - ih;                        /**/    break;
                    }

                    tx = (ix + iw) + gap;
                    ty = iy + ((ih / 2) - (th / 2F));

                    if (strfrm == null)
                    {
                        g.DrawString(Text, Font, br, new RectangleF(tx, ty, tw, th));
                        g.DrawString(TextFA, FontFA, br, new RectangleF(ix + Icon.Gap, iy + Icon.Gap, iw, ih));
                    }
                    else
                    {
                        g.DrawString(Text, Font, br, new RectangleF(tx, ty, tw, th), strfrm);
                        g.DrawString(TextFA, FontFA, br, new RectangleF(ix + Icon.Gap, iy + Icon.Gap, iw, ih), strfrm);
                    }
                }
                else if (Icon.Alignment == DvTextIconAlignment.TopBottom)
                {
                    int gap = string.IsNullOrWhiteSpace(Text) ? 0 : Icon.Gap;
                    SizeF sz = g.MeasureString(Text, Font);
                    SizeF szFA = g.MeasureString(TextFA, FontFA);
                    float tx = Bounds.X, ty = Bounds.Y, tw = sz.Width, th = sz.Height;
                    float ix = Bounds.X, iy = Bounds.Y, iw = szFA.Width, ih = szFA.Height;

                    float mw = Math.Max(tw, iw);
                    switch (Align)
                    {
                        case DvContentAlignment.TopLeft:        /**/    ix = Bounds.X + ((mw / 2F) - (iw / 2F));                            /**/    iy = Bounds.Y;                                                  /**/    break;
                        case DvContentAlignment.TopCenter:      /**/    ix = Bounds.X + ((Bounds.Width / 2F) - (iw / 2F));                  /**/    iy = Bounds.Y;                                                  /**/    break;
                        case DvContentAlignment.TopRight:       /**/    ix = Bounds.Right - mw + ((mw / 2F) - (iw / 2F));                   /**/    iy = Bounds.Y;                                                  /**/    break;
                        case DvContentAlignment.MiddleLeft:     /**/    ix = Bounds.X + ((mw / 2F) - (iw / 2F));                            /**/    iy = Bounds.Y + (Bounds.Height / 2F - (ih + gap + th) / 2F);    /**/    break;
                        case DvContentAlignment.MiddleCenter:   /**/    ix = Bounds.X + ((Bounds.Width / 2F) - (iw / 2F));                  /**/    iy = Bounds.Y + (Bounds.Height / 2F - (ih + gap + th) / 2F);    /**/    break;
                        case DvContentAlignment.MiddleRight:    /**/    ix = Bounds.Right - mw + ((mw / 2F) - (iw / 2F));                   /**/    iy = Bounds.Y + (Bounds.Height / 2F - (ih + gap + th) / 2F);    /**/    break;
                        case DvContentAlignment.BottomLeft:     /**/    ix = Bounds.X + ((mw / 2F) - (iw / 2F));                            /**/    iy = Bounds.Bottom - (th + ih + gap);                           /**/    break;
                        case DvContentAlignment.BottomCenter:   /**/    ix = Bounds.X + ((Bounds.Width / 2F) - (iw / 2F));                  /**/    iy = Bounds.Bottom - (th + ih + gap);                           /**/    break;
                        case DvContentAlignment.BottomRight:    /**/    ix = Bounds.Right - mw + ((mw / 2F) - (iw / 2F));                   /**/    iy = Bounds.Bottom - (th + ih + gap);                           /**/    break;
                    }

                    tx = ix + ((iw / 2F) - (tw / 2F));
                    ty = (iy + ih) + gap;
                    
                    if (strfrm == null)
                    {
                        g.DrawString(Text, Font, br, new RectangleF(tx, ty, tw, th));
                        g.DrawString(TextFA, FontFA, br, new RectangleF(ix + Icon.IconOffsetX, iy + Icon.IconOffsetY, iw, ih));
                    }
                    else
                    {
                        g.DrawString(Text, Font, br, new RectangleF(tx, ty, tw, th), strfrm);
                        g.DrawString(TextFA, FontFA, br, new RectangleF(ix + Icon.IconOffsetX, iy + Icon.IconOffsetY, iw, ih), strfrm);
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}
