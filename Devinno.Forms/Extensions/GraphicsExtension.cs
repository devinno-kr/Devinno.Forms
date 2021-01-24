using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Devinno.Forms.Tools;
using System.Globalization;
using Devinno.Forms.Icons;
using System.Linq;
using Devinno.Tools;

namespace Devinno.Forms.Extensions
{
    public static class GraphicsExtension
    {
        #region Properties
        #endregion

        #region Constructor
        static GraphicsExtension()
        {
            
        }
        #endregion

        #region Method
        #region Tool
        private static int INTR(float v) => Convert.ToInt32(Math.Round(v));
        private static int INTC(float v) => Convert.ToInt32(Math.Ceiling(v));
        #endregion

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
        public static void DrawText(this Graphics g, string Text, Font Font, Brush br, Rectangle Bounds, DvContentAlignment Align)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var strfrm = new StringFormat() { LineAlignment = StringAlignment.Center };
                var sz = g.MeasureString(Text, Font, Bounds.Size);
                int x = Bounds.X, y = Bounds.Y, w = INTC(sz.Width), h = INTC(sz.Height);
                switch (Align)
                {
                    case DvContentAlignment.TopLeft:        /**/    x = Bounds.X;                                                   /**/    y = Bounds.Y;                                        /**/  strfrm.Alignment = StringAlignment.Near;  break;
                    case DvContentAlignment.TopCenter:      /**/    x = Bounds.X + INTR(Bounds.Width / 2F - w / 2F);                /**/    y = Bounds.Y;                                        /**/  strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.TopRight:       /**/    x = Bounds.Right - w;                                           /**/    y = Bounds.Y;                                        /**/  strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.MiddleLeft:     /**/    x = Bounds.X;                                                   /**/    y = Bounds.Y + INTR(Bounds.Height / 2F - h / 2F);    /**/  strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.MiddleCenter:   /**/    x = Bounds.X + INTR(Bounds.Width / 2F - w / 2F);                /**/    y = Bounds.Y + INTR(Bounds.Height / 2F - h / 2F);    /**/  strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.MiddleRight:    /**/    x = Bounds.Right - w;                                           /**/    y = Bounds.Y + INTR(Bounds.Height / 2F - h / 2F);    /**/  strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.BottomLeft:     /**/    x = Bounds.X;                                                   /**/    y = Bounds.Bottom - h;                               /**/  strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.BottomCenter:   /**/    x = Bounds.X + INTR(Bounds.Width / 2F - w / 2F);                /**/    y = Bounds.Bottom - h;                               /**/  strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.BottomRight:    /**/    x = Bounds.Right - w;                                           /**/    y = Bounds.Bottom - h;                               /**/  strfrm.Alignment = StringAlignment.Far; break;
                }

                var rt = new Rectangle(x, y, w, h);
                g.DrawString(Text, Font, br, rt, strfrm);
                strfrm.Dispose();
            }
        }

        public static void DrawText(this Graphics g, string Text, Font Font, Brush br, RectangleF Bounds, DvContentAlignment Align)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var strfrm = new StringFormat() { LineAlignment = StringAlignment.Center };
                var sz = g.MeasureString(Text, Font, Bounds.Size);
                float x = Bounds.X, y = Bounds.Y, w = sz.Width, h = sz.Height;
                switch (Align)
                {
                    case DvContentAlignment.TopLeft:        /**/    x = Bounds.X;                                   /**/    y = Bounds.Y;                                   /**/    strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.TopCenter:      /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Y;                                   /**/    strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.TopRight:       /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Y;                                   /**/    strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.MiddleLeft:     /**/    x = Bounds.X;                                   /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.MiddleCenter:   /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.MiddleRight:    /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Y + (Bounds.Height / 2F - h / 2F);   /**/    strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.BottomLeft:     /**/    x = Bounds.X;                                   /**/    y = Bounds.Bottom - h;                          /**/    strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.BottomCenter:   /**/    x = Bounds.X + (Bounds.Width / 2F - w / 2F);    /**/    y = Bounds.Bottom - h;                          /**/    strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.BottomRight:    /**/    x = Bounds.Right - w;                           /**/    y = Bounds.Bottom - h;                          /**/    strfrm.Alignment = StringAlignment.Far; break;
                }

                var rt = new RectangleF(x, y, w, h);
                g.DrawString(Text, Font, br, rt, strfrm);
                strfrm.Dispose();
            }
        }
        #endregion

        #region ToChar
        //internal static char ToChar<TEnum>(this TEnum icon, IFormatProvider formatProvider = null) where TEnum : struct, IConvertible, IComparable, IFormattable
        //{
        //    return char.ConvertFromUtf32(icon.ToInt32(formatProvider ?? CultureInfo.InvariantCulture)).Single();
        //}
        #endregion

        #region MeasureIcon
        public static SizeF MeasureIcon(this Graphics g, DvIcon icon)
        {
            SizeF ret = new SizeF(0F, 0F);
            if (icon != null)
            {
                if (icon.IconImage != null)
                {
                    ret = new SizeF(Convert.ToSingle(icon.IconImage.Width), Convert.ToSingle(icon.IconImage.Height));
                }
                else if(FA.Valid(icon.IconString))
                {
                    var r = FA.GetFAI(icon.IconString);
                    using (var FontFA = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        ret = g.MeasureString(r.IconText, FontFA);
                    }
                }
            }
            return ret;
        }
        #endregion
        #region DrawIcon
        public static void DrawIcon(this Graphics g, DvIcon icon, Brush br, Rectangle bounds, DvContentAlignment align)
        {
            if (icon != null)
            {
                if (icon.IconImage != null)
                {
                    var sz = g.MeasureIcon(icon);
                    var rt = DrawingTool.MakeRectangleAlign(bounds, sz, align);
                    g.DrawImage(icon.IconImage, rt);
                }
                else if (FA.Valid(icon.IconString))
                {
                    var r = FA.GetFAI(icon.IconString);

                    var old = g.TextRenderingHint;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    using (var ft = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        var text = r.IconText;
                        var sz = g.MeasureIcon(icon);
                        var rt = DrawingTool.MakeRectangleAlign(bounds, sz, align);
                        rt.Offset(INTR(icon.IconSize / 30f), INTR(icon.IconSize / 4.8f));
                        g.DrawString(text, ft, br, rt);
                    }
                    g.TextRenderingHint = old;
                }
            }
        }
        public static void DrawIcon(this Graphics g, DvIcon icon, Brush br, RectangleF bounds, DvContentAlignment align)
        {
            if (icon != null)
            {
                if (icon.IconImage != null)
                {
                    var sz = g.MeasureIcon(icon);
                    var rt = DrawingTool.MakeRectangleAlign(bounds, sz, align);
                    g.DrawImage(icon.IconImage, rt);
                }
                else if (FA.Valid(icon.IconString))
                {
                    var r = FA.GetFAI(icon.IconString);

                    var old = g.TextRenderingHint;
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    using (var ft = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        var text = r.IconText;
                        var sz = g.MeasureIcon(icon);
                        var rt = DrawingTool.MakeRectangleAlign(bounds, sz, align);
                        rt.Offset(icon.IconSize / 30f, icon.IconSize / 4.8f);
                        g.DrawString(text, ft, br, rt);
                    }
                    g.TextRenderingHint = old;
                }
            }
        }
        #endregion

        #region MeasureTextIcon
        public static SizeF MeasureTextIcon(this Graphics g, DvIcon icon, string text, Font font)
        {
            SizeF ret = new SizeF(0F, 0F);

            if (icon != null)
            {
                var sz = g.MeasureString(text, font);
                var szFA = g.MeasureIcon(icon);

                if (icon.Alignment == DvTextIconAlignment.LeftRight)
                {
                    ret = new SizeF(szFA.Width + icon.Gap + sz.Width, Math.Max(sz.Height, szFA.Height));
                }
                else
                {
                    ret = new SizeF(Math.Max(sz.Width, szFA.Width), szFA.Height + icon.Gap + sz.Height);
                }
            }
            return ret;
        }
        public static SizeF MeasureTextIcon(this Graphics g, DvTextIconAlignment icoAlign, Size icoSize, int icoGap, string text, Font font)
        {
            SizeF ret = new SizeF(0F, 0F);

            var sz = g.MeasureString(text, font);
            var szFA = icoSize;

            if (icoAlign == DvTextIconAlignment.LeftRight)
            {
                ret = new SizeF(szFA.Width + icoGap + sz.Width, Math.Max(sz.Height, szFA.Height));
            }
            else
            {
                ret = new SizeF(Math.Max(sz.Width, szFA.Width), szFA.Height + icoGap + sz.Height);
            }
            return ret;
        }
        #endregion
        #region DrawTextIcon
        public static void DrawTextIcon(this Graphics g, DvIcon icon, string text, Font font, Brush br, Rectangle bounds, DvContentAlignment align, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (icon == null || (icon != null && icon.IconImage == null && !FA.Valid(icon.IconString)))
            {
                var rt = new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                rt.Offset(TextOffsetX, TextOffsetY);
                DrawText(g, text, font, br, rt, align);
            }
            else
            {
                if (icon.IconImage != null)
                {
                    var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                    var szTX = g.MeasureString(text, font);
                    var szFA = g.MeasureIcon(icon);
                    var szv = g.MeasureTextIcon(icon, text, font);
                    var rt = DrawingTool.MakeRectangleAlign(bounds, szv, align);

                    if (icon.Alignment == DvTextIconAlignment.LeftRight)
                    {
                        var rtFA = new Rectangle(rt.X, INTC(DrawingTool.CenterY(rt, szFA)), INTC(szFA.Width), INTC(szFA.Height));
                        var rtTX = new Rectangle(rt.Right - INTC(szTX.Width), INTC(DrawingTool.CenterY(rt, szTX)), INTC(szTX.Width), INTC(szTX.Height));

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleCenter);
                        g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleCenter);
                    }
                    else
                    {
                        var rtFA = new Rectangle(INTC(DrawingTool.CenterX(rt, szFA)), rt.Y, INTC(szFA.Width), INTC(szFA.Height));
                        var rtTX = new Rectangle(INTC(DrawingTool.CenterX(rt, szTX)), rt.Bottom - INTC(szTX.Height), INTC(szTX.Width), INTC(szTX.Height));
                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleCenter);
                        g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleCenter);
                    }
                }
                else
                {
                    var r = FA.GetFAI(icon.IconString);

                    using (var fontFA = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        var textFA = r.IconText;
                        var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                        var szTX = g.MeasureString(text, font);
                        var szFA = g.MeasureIcon(icon);
                        var szv = g.MeasureTextIcon(icon, text, font);
                        var rt = DrawingTool.MakeRectangleAlign(bounds, szv, align);

                        if (icon.Alignment == DvTextIconAlignment.LeftRight)
                        {
                            var rtFA = new Rectangle(rt.X, INTC(DrawingTool.CenterY(rt, szFA)), INTC(szFA.Width), INTC(szFA.Height));
                            var rtTX = new Rectangle(rt.Right - INTC(szTX.Width), INTC(DrawingTool.CenterY(rt, szTX)), INTC(szTX.Width), INTC(szTX.Height));

                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                            g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleCenter);
                            g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleCenter);
                        }
                        else
                        {
                            var rtFA = new Rectangle(INTC(DrawingTool.CenterX(rt, szFA)), rt.Y, INTC(szFA.Width), INTC(szFA.Height));
                            var rtTX = new Rectangle(INTC(DrawingTool.CenterX(rt, szTX)), rt.Bottom - INTC(szTX.Height), INTC(szTX.Width), INTC(szTX.Height));
                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                            g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleCenter);
                            g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleCenter);
                        }
                    }
                }
            }
        }

        public static void DrawTextIcon(this Graphics g, DvIcon icon, string text, Font font, Brush br, RectangleF bounds, DvContentAlignment align, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (icon == null || (icon != null && icon.IconImage == null && !FA.Valid(icon.IconString)))
            {
                var rt = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                rt.Offset(TextOffsetX, TextOffsetY);
                DrawText(g, text, font, br, bounds, align);
            }
            else
            {
                if (icon.IconImage != null)
                {
                    var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                    var szTX = g.MeasureString(text, font);
                    var szFA = g.MeasureIcon(icon);
                    var szv = g.MeasureTextIcon(icon, text, font);
                    var rt = DrawingTool.MakeRectangleAlign(bounds, szv, align);

                    if (icon.Alignment == DvTextIconAlignment.LeftRight)
                    {
                        var rtFA = new RectangleF(rt.X, DrawingTool.CenterY(rt, szFA), szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(rt.Right - szTX.Width, DrawingTool.CenterY(rt, szTX), szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleLeft);
                        g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleLeft);
                    }
                    else
                    {
                        var rtFA = new RectangleF(DrawingTool.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(DrawingTool.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleLeft);
                        g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleLeft);
                    }
                }
                else
                {
                    var r = FA.GetFAI(icon.IconString);

                    using (var fontFA = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        var textFA = r.IconText;
                        var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                        var szTX = g.MeasureString(text, font);
                        var szFA = g.MeasureIcon(icon);
                        var szv = g.MeasureTextIcon(icon, text, font);
                        var rt = DrawingTool.MakeRectangleAlign(bounds, szv, align);

                        if (icon.Alignment == DvTextIconAlignment.LeftRight)
                        {
                            var rtFA = new RectangleF(rt.X, DrawingTool.CenterY(rt, szFA), szFA.Width, szFA.Height);
                            var rtTX = new RectangleF(rt.Right - szTX.Width, DrawingTool.CenterY(rt, szTX), szTX.Width, szTX.Height);

                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                            g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleLeft);
                            g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleLeft);
                        }
                        else
                        {
                            var rtFA = new RectangleF(DrawingTool.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                            var rtTX = new RectangleF(DrawingTool.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                            g.DrawIcon(icon, br, rtFA, DvContentAlignment.MiddleLeft);
                            g.DrawText(text, font, br, rtTX, DvContentAlignment.MiddleLeft);
                        }
                    }
                }
            }
        }
        #endregion
        #endregion
    }
}
