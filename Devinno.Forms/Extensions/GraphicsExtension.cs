using Devinno.Forms.Icons;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Extensions
{
    public static class GraphicsExtension
    {
        #region Constructor
        static GraphicsExtension()
        {

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
        public static void DrawText(this Graphics g, string Text, Font Font, Brush br, RectangleF Bounds, DvContentAlignment Align = DvContentAlignment.MiddleCenter, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                #region Text
                var strfrm = new StringFormat() { LineAlignment = StringAlignment.Center };
                var sz = g.MeasureString(Text, Font, Bounds.Size);
                var rt = Util.MakeRectangleAlign(Bounds, sz, Align);

                switch (Align)
                {
                    case DvContentAlignment.TopLeft: strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.TopCenter: strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.TopRight: strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.MiddleLeft: strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.MiddleCenter: strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.MiddleRight: strfrm.Alignment = StringAlignment.Far; break;
                    case DvContentAlignment.BottomLeft: strfrm.Alignment = StringAlignment.Near; break;
                    case DvContentAlignment.BottomCenter: strfrm.Alignment = StringAlignment.Center; break;
                    case DvContentAlignment.BottomRight: strfrm.Alignment = StringAlignment.Far; break;
                }

                rt.Offset(TextOffsetX, TextOffsetY);
                g.DrawString(Text, Font, br, rt, strfrm);
                 
                strfrm.Dispose();
                #endregion
            }
        }
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
                else if (FA.Contains(icon.IconString) && icon.IconSize > 0)
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
        public static void DrawIcon(this Graphics g, DvIcon icon, Brush br, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter)
        {
            if (icon != null)
            {
                if (icon.IconImage != null)
                {
                    if (!icon.Shadow)
                    {
                        var sz = g.MeasureIcon(icon);
                        var rt = Util.MakeRectangleAlign(bounds, sz, align);
                        g.DrawImage(icon.IconImage, rt);
                    }
                }
                else if (FA.Contains(icon.IconString) && icon.IconSize > 0)
                {
                    var r = FA.GetFAI(icon.IconString);

                    var old = g.TextRenderingHint;
                    g.TextRenderingHint = icon.RenderingHint.HasValue ? icon.RenderingHint.Value : TextRenderingHint.AntiAlias;
                    using (var ft = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                    {
                        var text = r.IconText;
                        var sz = g.MeasureIcon(icon);
                                          
                        var rt = Util.MakeRectangleAlign(bounds, sz, align);
                        rt.Offset(0, 1);
                        g.DrawString(text, ft, br, rt);
                    }
                    g.TextRenderingHint = old;
                }
            }
        }
        #endregion

        #region MeasureTextIcon
        public static SizeF MeasureTextIcon(this Graphics g, TextIcon texticon, Font font)
        {
            SizeF ret = new SizeF(0F, 0F);
            if (texticon != null) ret = MeasureTextIcon(g, texticon.Icon, texticon.Text, font);
            return ret;
        }
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
        public static SizeF MeasureTextIcon(this Graphics g, DvTextIconAlignment icoAlign, SizeF icoSize, float icoGap, string text, Font font)
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
        public static void DrawTextIcon(this Graphics g, TextIcon texticon, Font font, Brush br, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (texticon != null)
                DrawTextIcon(g, texticon.Icon, texticon.Text, font, br, Util.FromRect(bounds, texticon.TextPadding), align, TextOffsetX, TextOffsetY);
        }
        public static void DrawTextIcon(this Graphics g, DvIcon icon, string text, Font font, Brush br, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (icon == null || (icon != null && icon.IconImage == null && !FA.Contains(icon.IconString)))
            {
                #region Text
                var rt = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                rt.Offset(TextOffsetX, TextOffsetY);
                DrawText(g, text, font, br, rt, align);
                #endregion
            }
            else
            {
                if (icon.IconImage != null)
                {
                    #region Image/Text
                    var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                    var szTX = g.MeasureString(text, font);
                    var szFA = g.MeasureIcon(icon);
                    var szv = g.MeasureTextIcon(icon, text, font);
                    var rt = Util.MakeRectangleAlign(bounds, szv, align);

                    if (icon.Alignment == DvTextIconAlignment.LeftRight)
                    {
                        var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, ALIGN(align));
                        g.DrawText(text, font, br, rtTX, ALIGN(align));
                    }
                    else
                    {
                        var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, br, rtFA, ALIGN(align));
                        g.DrawText(text, font, br, rtTX, ALIGN(align));
                    }
                    #endregion
                }
                else
                {
                    #region FA/Text
                    {
                        var r = FA.GetFAI(icon.IconString);

                        var textFA = r.IconText;
                        var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                        var szTX = g.MeasureString(text, font);
                        var szFA = g.MeasureIcon(icon);
                        var szv = g.MeasureTextIcon(icon, text, font);
                        var rt = Util.MakeRectangleAlign(bounds, szv, align);

                        if (icon.IconSize > 0)
                        {
                            using (var fontFA = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                            {
                                if (icon.Alignment == DvTextIconAlignment.LeftRight)
                                {
                                    var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                                    g.DrawIcon(icon, br, rtFA, ALIGN(align));
                                }
                                else
                                {
                                    var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                                    g.DrawIcon(icon, br, rtFA, ALIGN(align));
                                }
                            }
                        }

                        if (icon.Alignment == DvTextIconAlignment.LeftRight)
                        {
                            var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);
                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);
                            g.DrawText(text, font, br, rtTX, ALIGN(align));
                        }
                        else
                        {
                            var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);
                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);
                            g.DrawText(text, font, br, rtTX, ALIGN(align));
                        }
                    }
                    #endregion
                }
            }
        }

        public static void DrawTextIcon(this Graphics g, TextIcon texticon, Brush brico, Brush brText, Font font, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (texticon != null)
                DrawTextIcon(g, texticon.Icon, brico, texticon.Text, font, brText, Util.FromRect(bounds, texticon.TextPadding), align, TextOffsetX, TextOffsetY);
        }
        public static void DrawTextIcon(this Graphics g, DvIcon icon, Brush brico, string text, Font font, Brush brText, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, int TextOffsetX = 0, int TextOffsetY = 0)
        {
            if (icon == null || (icon != null && icon.IconImage == null && !FA.Contains(icon.IconString)))
            {
                #region Text
                var rt = new RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                rt.Offset(TextOffsetX, TextOffsetY);
                DrawText(g, text, font, brText, rt, align);
                #endregion
            }
            else
            {
                if (icon.IconImage != null)
                {
                    #region Image/Text
                    var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                    var szTX = g.MeasureString(text, font);
                    var szFA = g.MeasureIcon(icon);
                    var szv = g.MeasureTextIcon(icon, text, font);
                    var rt = Util.MakeRectangleAlign(bounds, szv, align);

                    if (icon.Alignment == DvTextIconAlignment.LeftRight)
                    {
                        var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, brico, rtFA, ALIGN(align));
                        g.DrawText(text, font, brText, rtTX, ALIGN(align));
                    }
                    else
                    {
                        var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                        var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                        if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);

                        g.DrawIcon(icon, brico, rtFA, ALIGN(align));
                        g.DrawText(text, font, brText, rtTX, ALIGN(align));
                    }
                    #endregion
                }
                else
                {
                    #region FA/Text
                    {
                        var r = FA.GetFAI(icon.IconString);

                        var textFA = r.IconText;
                        var gap = string.IsNullOrWhiteSpace(text) ? 0 : icon.Gap;
                        var szTX = g.MeasureString(text, font);
                        var szFA = g.MeasureIcon(icon);
                        var szv = g.MeasureTextIcon(icon, text, font);
                        var rt = Util.MakeRectangleAlign(bounds, szv, align);

                        if (icon.IconSize > 0)
                        {
                            using (var fontFA = new Font(r.FontFamily, icon.IconSize, FontStyle.Regular))
                            {
                                if (icon.Alignment == DvTextIconAlignment.LeftRight)
                                {
                                    var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                                    g.DrawIcon(icon, brico, rtFA, ALIGN(align));
                                }
                                else
                                {
                                    var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                                    g.DrawIcon(icon, brico, rtFA, ALIGN(align));
                                }
                            }
                        }

                        if (icon.Alignment == DvTextIconAlignment.LeftRight)
                        {
                            var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);
                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);
                            g.DrawText(text, font, brText, rtTX, ALIGN(align));
                        }
                        else
                        {
                            var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);
                            if (TextOffsetX != 0 || TextOffsetY != 0) rtTX.Offset(TextOffsetX, TextOffsetY);
                            g.DrawText(text, font, brText, rtTX, ALIGN(align));
                        }
                    }
                    #endregion
                }
            }
        }


        #endregion

        #region ALIGN
        static DvContentAlignment ALIGN(DvContentAlignment align)
        {
            var ret = DvContentAlignment.MiddleCenter;
            switch (align)
            {
                case DvContentAlignment.TopLeft:
                case DvContentAlignment.MiddleLeft:
                case DvContentAlignment.BottomLeft:
                    ret = DvContentAlignment.MiddleLeft;
                    break;

                case DvContentAlignment.TopCenter:
                case DvContentAlignment.MiddleCenter:
                case DvContentAlignment.BottomCenter:
                    ret = DvContentAlignment.MiddleCenter;
                    break;

                case DvContentAlignment.TopRight:
                case DvContentAlignment.MiddleRight:
                case DvContentAlignment.BottomRight:
                    ret = DvContentAlignment.MiddleRight;
                    break;
            }
            return ret;
        }
        #endregion
        #region INT
        private static Rectangle INT(RectangleF rt) => Util.INT(rt);
        #endregion
        #endregion
    }
}
