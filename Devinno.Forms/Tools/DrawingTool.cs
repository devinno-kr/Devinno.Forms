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

        #region Blur
        #region class : GaussianBlur
        private class GaussianBlur
        {
            private readonly int[] _alpha;
            private readonly int[] _red;
            private readonly int[] _green;
            private readonly int[] _blue;

            private readonly int _width;
            private readonly int _height;

            private readonly ParallelOptions _pOptions = new ParallelOptions { MaxDegreeOfParallelism = 16 };

            public GaussianBlur(Bitmap image)
            {
                var rct = new Rectangle(0, 0, image.Width, image.Height);
                var source = new int[rct.Width * rct.Height];
                var bits = image.LockBits(rct, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Marshal.Copy(bits.Scan0, source, 0, source.Length);
                image.UnlockBits(bits);

                _width = image.Width;
                _height = image.Height;

                _alpha = new int[_width * _height];
                _red = new int[_width * _height];
                _green = new int[_width * _height];
                _blue = new int[_width * _height];

                Parallel.For(0, source.Length, _pOptions, i =>
                {
                    _alpha[i] = (int)((source[i] & 0xff000000) >> 24);
                    _red[i] = (source[i] & 0xff0000) >> 16;
                    _green[i] = (source[i] & 0x00ff00) >> 8;
                    _blue[i] = (source[i] & 0x0000ff);
                });
            }

            public Bitmap Process(int radial)
            {
                var newAlpha = new int[_width * _height];
                var newRed = new int[_width * _height];
                var newGreen = new int[_width * _height];
                var newBlue = new int[_width * _height];
                var dest = new int[_width * _height];

                Parallel.Invoke(
                    () => gaussBlur_4(_alpha, newAlpha, radial),
                    () => gaussBlur_4(_red, newRed, radial),
                    () => gaussBlur_4(_green, newGreen, radial),
                    () => gaussBlur_4(_blue, newBlue, radial));

                Parallel.For(0, dest.Length, _pOptions, i =>
                {
                    if (newAlpha[i] > 255) newAlpha[i] = 255;
                    if (newRed[i] > 255) newRed[i] = 255;
                    if (newGreen[i] > 255) newGreen[i] = 255;
                    if (newBlue[i] > 255) newBlue[i] = 255;

                    if (newAlpha[i] < 0) newAlpha[i] = 0;
                    if (newRed[i] < 0) newRed[i] = 0;
                    if (newGreen[i] < 0) newGreen[i] = 0;
                    if (newBlue[i] < 0) newBlue[i] = 0;

                    dest[i] = (int)((uint)(newAlpha[i] << 24) | (uint)(newRed[i] << 16) | (uint)(newGreen[i] << 8) | (uint)newBlue[i]);
                });

                var image = new Bitmap(_width, _height);
                var rct = new Rectangle(0, 0, image.Width, image.Height);
                var bits2 = image.LockBits(rct, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                Marshal.Copy(dest, 0, bits2.Scan0, dest.Length);
                image.UnlockBits(bits2);
                return image;
            }

            private void gaussBlur_4(int[] source, int[] dest, int r)
            {
                var bxs = boxesForGauss(r, 3);
                boxBlur_4(source, dest, _width, _height, (bxs[0] - 1) / 2);
                boxBlur_4(dest, source, _width, _height, (bxs[1] - 1) / 2);
                boxBlur_4(source, dest, _width, _height, (bxs[2] - 1) / 2);
            }

            private int[] boxesForGauss(int sigma, int n)
            {
                var wIdeal = Math.Sqrt((12 * sigma * sigma / n) + 1);
                var wl = (int)Math.Floor(wIdeal);
                if (wl % 2 == 0) wl--;
                var wu = wl + 2;

                var mIdeal = (double)(12 * sigma * sigma - n * wl * wl - 4 * n * wl - 3 * n) / (-4 * wl - 4);
                var m = Math.Round(mIdeal);

                var sizes = new List<int>();
                for (var i = 0; i < n; i++) sizes.Add(i < m ? wl : wu);
                return sizes.ToArray();
            }

            private void boxBlur_4(int[] source, int[] dest, int w, int h, int r)
            {
                for (var i = 0; i < source.Length; i++) dest[i] = source[i];
                boxBlurH_4(dest, source, w, h, r);
                boxBlurT_4(source, dest, w, h, r);
            }

            private void boxBlurH_4(int[] source, int[] dest, int w, int h, int r)
            {
                var iar = (double)1 / (r + r + 1);
                Parallel.For(0, h, _pOptions, i =>
                {
                    var ti = i * w;
                    var li = ti;
                    var ri = ti + r;
                    var fv = source[ti];
                    var lv = source[ti + w - 1];
                    var val = (r + 1) * fv;
                    for (var j = 0; j < r; j++) val += source[ti + j];
                    for (var j = 0; j <= r; j++)
                    {
                        val += source[ri++] - fv;
                        dest[ti++] = (int)Math.Round(val * iar);
                    }
                    for (var j = r + 1; j < w - r; j++)
                    {
                        val += source[ri++] - dest[li++];
                        dest[ti++] = (int)Math.Round(val * iar);
                    }
                    for (var j = w - r; j < w; j++)
                    {
                        val += lv - source[li++];
                        dest[ti++] = (int)Math.Round(val * iar);
                    }
                });
            }

            private void boxBlurT_4(int[] source, int[] dest, int w, int h, int r)
            {
                var iar = (double)1 / (r + r + 1);
                Parallel.For(0, w, _pOptions, i =>
                {
                    var ti = i;
                    var li = ti;
                    var ri = ti + r * w;
                    var fv = source[ti];
                    var lv = source[ti + w * (h - 1)];
                    var val = (r + 1) * fv;
                    for (var j = 0; j < r; j++) val += source[ti + j * w];
                    for (var j = 0; j <= r; j++)
                    {
                        val += source[ri] - fv;
                        dest[ti] = (int)Math.Round(val * iar);
                        ri += w;
                        ti += w;
                    }
                    for (var j = r + 1; j < h - r; j++)
                    {
                        val += source[ri] - source[li];
                        dest[ti] = (int)Math.Round(val * iar);
                        li += w;
                        ri += w;
                        ti += w;
                    }
                    for (var j = h - r; j < h; j++)
                    {
                        val += lv - source[li];
                        dest[ti] = (int)Math.Round(val * iar);
                        li += w;
                        ti += w;
                    }
                });
            }
        }
        #endregion

        public static Bitmap Blur(Bitmap bm, int radial)
        {
            return new GaussianBlur(bm).Process(radial);
        }
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
    }
}
