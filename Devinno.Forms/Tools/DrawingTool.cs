using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        #region RoundCorners
        public static GraphicsPath RoundCorners(Rectangle r, float radius) { return RoundCorners(new PointF[] { new PointF(r.Left, r.Top), new PointF(r.Right, r.Top), new PointF(r.Right, r.Bottom), new PointF(r.Left, r.Bottom) }, radius); }
        public static GraphicsPath RoundCorners(PointF[] points, float radius)
        {
            GraphicsPath retval = new GraphicsPath();
            if (points.Length < 3)
            {
                throw new ArgumentException();
            }
            var rects = new RectangleF[points.Length];
            PointF pt1, pt2;
            //Vectors for polygon sides and normal vectors
            Vector v1, v2, n1 = new Vector(), n2 = new Vector();
            //Rectangle that bounds arc
            SizeF size = new SizeF(2 * radius, 2 * radius);
            //Arc center
            PointF center = new PointF();

            for (int i = 0; i < points.Length; i++)
            {
                pt1 = points[i];//First vertex
                pt2 = points[i == points.Length - 1 ? 0 : i + 1];//Second vertex
                v1 = new Vector(pt2.X, pt2.Y) - new Vector(pt1.X, pt1.Y);//One vector
                pt2 = points[i == 0 ? points.Length - 1 : i - 1];//Third vertex
                v2 = new Vector(pt2.X, pt2.Y) - new Vector(pt1.X, pt1.Y);//Second vector
                //Angle between vectors
                float sweepangle = (float)Vector.AngleBetween(v1, v2);
                //Direction for normal vectors
                if (sweepangle < 0)
                {
                    n1 = new Vector(v1.Y, -v1.X);
                    n2 = new Vector(-v2.Y, v2.X);
                }
                else
                {
                    n1 = new Vector(-v1.Y, v1.X);
                    n2 = new Vector(v2.Y, -v2.X);
                }

                n1.Normalize(); n2.Normalize();
                n1 *= radius; n2 *= radius;
                /// Points for lines which intersect in the arc center
                PointF pt = points[i];
                pt1 = new PointF((float)(pt.X + n1.X), (float)(pt.Y + n1.Y));
                pt2 = new PointF((float)(pt.X + n2.X), (float)(pt.Y + n2.Y));
                double m1 = v1.Y / v1.X, m2 = v2.Y / v2.X;
                //Arc center
                if (v1.X == 0)
                {// first line is parallel OY
                    center.X = pt1.X;
                    center.Y = (float)(m2 * (pt1.X - pt2.X) + pt2.Y);
                }
                else if (v1.Y == 0)
                {// first line is parallel OX
                    center.X = (float)((pt1.Y - pt2.Y) / m2 + pt2.X);
                    center.Y = pt1.Y;
                }
                else if (v2.X == 0)
                {// second line is parallel OY
                    center.X = pt2.X;
                    center.Y = (float)(m1 * (pt2.X - pt1.X) + pt1.Y);
                }
                else if (v2.Y == 0)
                {//second line is parallel OX
                    center.X = (float)((pt2.Y - pt1.Y) / m1 + pt1.X);
                    center.Y = pt2.Y;
                }
                else
                {
                    center.X = (float)((pt2.Y - pt1.Y + m1 * pt1.X - m2 * pt2.X) / (m1 - m2));
                    center.Y = (float)(pt1.Y + m1 * (center.X - pt1.X));
                }
                rects[i] = new RectangleF(center.X - 2, center.Y - 2, 4, 4);
                //Tangent points on polygon sides
                n1.Negate(); n2.Negate();
                pt1 = new PointF((float)(center.X + n1.X), (float)(center.Y + n1.Y));
                pt2 = new PointF((float)(center.X + n2.X), (float)(center.Y + n2.Y));
                //Rectangle that bounds tangent arc
                RectangleF rect = new RectangleF(new PointF(center.X - radius, center.Y - radius), size);
                sweepangle = (float)Vector.AngleBetween(n2, n1);
                retval.AddArc(rect, (float)Vector.AngleBetween(new Vector(1, 0), n2), sweepangle);
            }
            retval.CloseAllFigures();
            return retval;
        }

        public GraphicsPath RoundedCorner(PointF angularPoint, PointF p1, PointF p2, float radius)
        {
            //Vector 1
            double dx1 = angularPoint.X - p1.X;
            double dy1 = angularPoint.Y - p1.Y;

            //Vector 2
            double dx2 = angularPoint.X - p2.X;
            double dy2 = angularPoint.Y - p2.Y;

            //Angle between vector 1 and vector 2 divided by 2
            double angle = (Math.Atan2(dy1, dx1) - Math.Atan2(dy2, dx2)) / 2;

            // The length of segment between angular point and the
            // points of intersection with the circle of a given radius
            double tan = Math.Abs(Math.Tan(angle));
            double segment = radius / tan;

            //Check the segment
            double length1 = GetLength(dx1, dy1);
            double length2 = GetLength(dx2, dy2);

            double length = Math.Min(length1, length2);

            if (segment > length)
            {
                segment = length;
                radius = (float)(length * tan);
            }

            // Points of intersection are calculated by the proportion between 
            // the coordinates of the vector, length of vector and the length of the segment.
            var p1Cross = GetProportionPoint(angularPoint, segment, length1, dx1, dy1);
            var p2Cross = GetProportionPoint(angularPoint, segment, length2, dx2, dy2);

            // Calculation of the coordinates of the circle 
            // center by the addition of angular vectors.
            double dx = angularPoint.X * 2 - p1Cross.X - p2Cross.X;
            double dy = angularPoint.Y * 2 - p1Cross.Y - p2Cross.Y;

            double L = GetLength(dx, dy);
            double d = GetLength(segment, radius);

            var circlePoint = GetProportionPoint(angularPoint, d, L, dx, dy);

            //StartAngle and EndAngle of arc
            var startAngle = Math.Atan2(p1Cross.Y - circlePoint.Y, p1Cross.X - circlePoint.X);
            var endAngle = Math.Atan2(p2Cross.Y - circlePoint.Y, p2Cross.X - circlePoint.X);

            //Sweep angle
            var sweepAngle = endAngle - startAngle;

            //Some additional checks
            if (sweepAngle < 0)
            {
                startAngle = endAngle;
                sweepAngle = -sweepAngle;
            }

            if (sweepAngle > Math.PI)
                sweepAngle = Math.PI - sweepAngle;

            GraphicsPath pth = new GraphicsPath();
            pth.AddLine(p1, p1Cross);
            pth.AddLine(p2, p2Cross);

            var left = circlePoint.X - radius;
            var top = circlePoint.Y - radius;
            var diameter = 2 * radius;
            var degreeFactor = 180 / Math.PI;
            pth.AddArc(left, top, diameter, diameter, (float)(startAngle * degreeFactor), (float)(sweepAngle * degreeFactor));

            return pth;
        }

        private double GetLength(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private PointF GetProportionPoint(PointF point, double segment,
                                          double length, double dx, double dy)
        {
            double factor = segment / length;

            return new PointF((float)(point.X - dx * factor),
                              (float)(point.Y - dy * factor));
        }
        #endregion

        #region PtToPixel / PixelToPt
        public static float PtToPixel(int pt) => (float)pt * 1.3281472327365F;
        public static float PtToPixel(float pt) => pt * 1.3281472327365F;
        public static float PixelToPt(float pixel) => pixel / 1.3281472327365F;
        #endregion
    }

    internal struct Vector
    {
        #region Properties
        #region X
        internal double _x;
        public double X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }

        }
        #endregion
        #region Y
        internal double _y;
        public double Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
            }

        }
        #endregion
        #region Length
        public double Length
        {
            get
            {
                return Math.Sqrt(_x * _x + _y * _y);
            }
        }
        #endregion
        #region LengthSquared
        public double LengthSquared
        {
            get
            {
                return _x * _x + _y * _y;
            }
        }
        #endregion
        #endregion

        #region Constructor
        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }
        #endregion

        #region Method
        #region Equals
        public override bool Equals(object o)
        {
            if ((null == o) || !(o is Vector))
            {
                return false;
            }

            Vector value = (Vector)o;
            return Vector.Equals(this, value);
        }

        public bool Equals(Vector value)
        {
            return Vector.Equals(this, value);
        }
        #endregion
        #region GetHashCode
        public override int GetHashCode()
        {
            // Perform field-by-field XOR of HashCodes
            return X.GetHashCode() ^
                   Y.GetHashCode();
        }
        #endregion
        #region Normalize
        public void Normalize()
        {
            this /= Math.Max(Math.Abs(_x), Math.Abs(_y));
            this /= Length;
        }
        #endregion
        #region Negate
        public void Negate()
        {
            _x = -_x;
            _y = -_y;
        }
        #endregion
        #endregion

        #region Static Method
        #region Equals
        public static bool Equals(Vector vector1, Vector vector2)
        {
            return vector1.X.Equals(vector2.X) &&
                   vector1.Y.Equals(vector2.Y);
        }
        #endregion
        #region CrossProduct
        public static double CrossProduct(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._y - vector1._y * vector2._x;
        }
        #endregion
        #region AngleBetween
        public static double AngleBetween(Vector vector1, Vector vector2)
        {
            double sin = vector1._x * vector2._y - vector2._x * vector1._y;
            double cos = vector1._x * vector2._x + vector1._y * vector2._y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }
        #endregion
        #region Operator
        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X &&
                   vector1.Y == vector2.Y;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
        }

        public static Vector operator -(Vector vector)
        {
            return new Vector(-vector._x, -vector._y);
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x + vector2._x,
                              vector1._y + vector2._y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x + vector2._x,
                              vector1._y + vector2._y);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x - vector2._x,
                              vector1._y - vector2._y);
        }

        public static Vector Subtract(Vector vector1, Vector vector2)
        {
            return new Vector(vector1._x - vector2._x,
                              vector1._y - vector2._y);
        }

        public static PointF operator +(Vector vector, Point point)
        {
            return new PointF(Convert.ToSingle(point.X + vector.X), Convert.ToSingle(point.Y + vector.Y));
        }

        public static PointF Add(Vector vector, Point point)
        {
            return new PointF(Convert.ToSingle(point.X + vector.X), Convert.ToSingle(point.Y + vector.Y));
        }

        public static Vector operator *(Vector vector, double scalar)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        public static Vector Multiply(Vector vector, double scalar)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        public static Vector operator *(double scalar, Vector vector)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        public static Vector Multiply(double scalar, Vector vector)
        {
            return new Vector(vector._x * scalar,
                              vector._y * scalar);
        }

        public static Vector operator /(Vector vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        public static Vector Divide(Vector vector, double scalar)
        {
            return vector * (1.0 / scalar);
        }

        /*
        public static Vector operator *(Vector vector, Matrix matrix)
        {
            return matrix.Transform(vector);
        }

        public static Vector Multiply(Vector vector, Matrix matrix)
        {
            return matrix.Transform(vector);
        }
        */
        public static double operator *(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._x + vector1._y * vector2._y;
        }

        public static double Multiply(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._x + vector1._y * vector2._y;
        }

        public static double Determinant(Vector vector1, Vector vector2)
        {
            return vector1._x * vector2._y - vector1._y * vector2._x;
        }
        /*
        public static explicit operator Size(Vector vector)
        {
            return new Size(Math.Abs(vector._x), Math.Abs(vector._y));
        }

        public static explicit operator Point(Vector vector)
        {
            return new Point(vector._x, vector._y);
        }
        */
        #endregion
        #endregion
    }
}
