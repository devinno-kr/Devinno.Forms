using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;

namespace Devinno.Forms.Tools
{
    public class DrawingTool
    {
        #region Path
        #region GetRoundRectPath
        public static GraphicsPath GetRoundRectPath(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0) return GetCapsule(baseRect);

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(arc, 180, 90);

            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathT
        public static GraphicsPath GetRoundRectPathT(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            RectangleF rt = baseRect;
            path.AddLine(rt.Left, rt.Bottom, rt.Left, rt.Top + diameter);
            path.AddArc(arc, 180, 90);
            path.AddLine(rt.Left + diameter, rt.Top, rt.Right - diameter, rt.Top);
            arc.X = rt.Right - diameter;
            path.AddArc(arc, 270, 90);
            path.AddLine(rt.Right, rt.Top + diameter, rt.Right, rt.Bottom);
            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathB
        public static GraphicsPath GetRoundRectPathB(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(baseRect.Left, baseRect.Top, baseRect.Right, baseRect.Top);
            arc.X = baseRect.Right - diameter;
            arc.Y = baseRect.Bottom - diameter;
            path.AddLine(baseRect.Right, baseRect.Y, baseRect.Right, arc.Y);
            path.AddArc(arc, 0, 90);
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathL
        public static GraphicsPath GetRoundRectPathL(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(arc, 180, 90);
            arc.X = baseRect.Right - diameter;
            path.AddLine(baseRect.Right, baseRect.Top, baseRect.Right, baseRect.Bottom);
            arc.Y = baseRect.Bottom - diameter;
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathR
        public static GraphicsPath GetRoundRectPathR(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(baseRect.Left, baseRect.Top, baseRect.Right - diameter, baseRect.Top);
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = baseRect.Bottom - diameter;
            path.AddLine(baseRect.Right, baseRect.Top + diameter, baseRect.Right, arc.Y);
            path.AddArc(arc, 0, 90);
            arc.X = baseRect.Left;
            path.AddLine(baseRect.Right - diameter, baseRect.Bottom, baseRect.Left, baseRect.Bottom);
            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathLT
        public static GraphicsPath GetRoundRectPathLT(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }


            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(arc, 180, 90);

            path.AddLine(baseRect.Right, baseRect.Top, baseRect.Right, baseRect.Bottom);
            path.AddLine(baseRect.Right, baseRect.Bottom, baseRect.Left, baseRect.Bottom);

            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathRT
        public static GraphicsPath GetRoundRectPathRT(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }


            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            path.AddLine(baseRect.Right, baseRect.Bottom, baseRect.Left, baseRect.Bottom);
            path.AddLine(baseRect.Left, baseRect.Bottom, baseRect.Left, baseRect.Top);


            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathLB
        public static GraphicsPath GetRoundRectPathLB(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            arc.Y = baseRect.Bottom - diameter;
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.AddLine(baseRect.Left, baseRect.Top, baseRect.Right, baseRect.Top);
            path.AddLine(baseRect.Right, baseRect.Top, baseRect.Right, baseRect.Bottom);


            path.CloseFigure();
            return path;
        }
        #endregion
        #region GetRoundRectPathRB
        public static GraphicsPath GetRoundRectPathRB(RectangleF baseRect, float radius)
        {
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            arc.X = baseRect.Right - diameter;
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            path.AddLine(baseRect.Left, baseRect.Bottom, baseRect.Left, baseRect.Top);
            path.AddLine(baseRect.Left, baseRect.Top, baseRect.Right, baseRect.Top);

            path.CloseFigure();
            return path;
        }
        #endregion

        #region GetCapsule
        private static GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleT
        private static GraphicsPath GetCapsuleT(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleB
        private static GraphicsPath GetCapsuleB(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleL
        private static GraphicsPath GetCapsuleL(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleR
        private static GraphicsPath GetCapsuleR(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleLT
        private static GraphicsPath GetCapsuleLT(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleRT
        private static GraphicsPath GetCapsuleRT(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleLB
        private static GraphicsPath GetCapsuleLB(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #region GetCapsuleRB
        private static GraphicsPath GetCapsuleRB(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception ex)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion
        #endregion

        #region MakeRectangleAlign
        public static Rectangle MakeRectangleAlign(Rectangle Bounds, SizeF Size, DvContentAlignment align)
        {
            var rt = MakeRectangleAlign(new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height), 
                                        new SizeF(Convert.ToSingle(Math.Ceiling(Size.Width)), Convert.ToSingle(Math.Ceiling(Size.Height))), align);
            return new Rectangle(Convert.ToInt32(Math.Ceiling(rt.X)), Convert.ToInt32(Math.Ceiling(rt.Y)), Convert.ToInt32(Math.Ceiling(rt.Width)), Convert.ToInt32(Math.Ceiling(rt.Height)));
        }

        public static RectangleF MakeRectangleAlign(RectangleF Bounds, SizeF Size, DvContentAlignment dv)
        {
            var rt = Bounds;
            var sz = Size;
            var ret = new RectangleF(Bounds.X, Bounds.Y, Size.Width, Size.Height);
            switch (dv)
            {
                case DvContentAlignment.TopLeft: ret.X = Bounds.X; ret.Y = Bounds.Y; break;
                case DvContentAlignment.TopCenter: ret.X = CenterX(rt, sz); ret.Y = Bounds.Y; break;
                case DvContentAlignment.TopRight: ret.X = Bounds.Right - sz.Width; ret.Y = Bounds.Y; break;

                case DvContentAlignment.MiddleLeft: ret.X = Bounds.X; ret.Y = CenterY(rt, sz); ; break;
                case DvContentAlignment.MiddleCenter: ret.X = CenterX(rt, sz); ret.Y = CenterY(rt, sz); break;
                case DvContentAlignment.MiddleRight: ret.X = Bounds.Right - sz.Width; ret.Y = CenterY(rt, sz); break;

                case DvContentAlignment.BottomLeft: ret.X = Bounds.X; ret.Y = Bounds.Bottom - sz.Height; break;
                case DvContentAlignment.BottomCenter: ret.X = CenterX(rt, sz); ret.Y = Bounds.Bottom - sz.Height; break;
                case DvContentAlignment.BottomRight: ret.X = Bounds.Right - sz.Width; ret.Y = Bounds.Bottom - sz.Height; break;
            }
            return ret;
        }

        public static int CenterX(Rectangle rt, Size sz) => rt.X + (rt.Width / 2 - sz.Width / 2);
        public static int CenterY(Rectangle rt, Size sz) => rt.Y + (rt.Height / 2 - sz.Height / 2);
        public static float CenterX(RectangleF rt, SizeF sz) => rt.X + (rt.Width / 2F - sz.Width / 2F);
        public static float CenterY(RectangleF rt, SizeF sz) => rt.Y + (rt.Height / 2F - sz.Height / 2F);
        #endregion

        #region WebImage
        public static Bitmap CreateWebImage(string URL)
        {
            try
            {
                WebClient Downloader = new WebClient();
                Stream ImageStream = Downloader.OpenRead(URL);
                Bitmap DownloadImage = Bitmap.FromStream(ImageStream) as Bitmap;
                return DownloadImage;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        public static float PtToPixel(int pt) => (float)pt * 1.3281472327365F;
    }
}
