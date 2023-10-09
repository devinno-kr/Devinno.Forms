using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Utils
{
    public class Util
    {
        #region SetDoubleBuffered
        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;
            System.Reflection.PropertyInfo aProp = typeof(System.Windows.Forms.Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp.SetValue(c, true, null);
        }
        #endregion

        #region FromArgb
        public static Color FromArgb(Color c) => Color.FromArgb(c.A, c.R, c.G, c.B);
        public static Color FromArgb(int c) => Color.FromArgb(c);
        public static Color FromArgb(byte a, byte r, byte g, byte b) => Color.FromArgb(a, r, g, b);
        public static Color FromArgb(byte a, Color c) => Color.FromArgb(a, c);
        public static Color FromArgb(byte r, byte g, byte b) => Color.FromArgb(r, g, b);
        #endregion
        #region FromRect
        public static RectangleF FromRect(DvControl v) => FromRect(v.Left, v.Top, v.Width, v.Height);
        public static RectangleF FromRect(RectangleF rt) => new RectangleF(rt.Left, rt.Top, rt.Width, rt.Height);
        public static RectangleF FromRect(Rectangle rt) => new RectangleF(rt.Left, rt.Top, rt.Width, rt.Height);
        public static RectangleF FromRect(int x, int y, int width, int height) => new Rectangle(x, y, width, height);
        public static RectangleF FromRect(float x, float y, float width, float height) => new RectangleF(x, y, width, height);
        public static RectangleF FromRect(RectangleF rt, Padding pad) => new RectangleF(rt.Left + pad.Left, rt.Top + pad.Top, rt.Width - (pad.Left + pad.Right), rt.Height - (pad.Top + pad.Bottom));
        #endregion

        #region MakeRectangleAlign
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

        //public static int CenterX(Rectangle rt, Size sz) => rt.X + (rt.Width / 2 - sz.Width / 2);
        //public static int CenterY(Rectangle rt, Size sz) => rt.Y + (rt.Height / 2 - sz.Height / 2);
        public static float CenterX(RectangleF rt, SizeF sz) => rt.X + (rt.Width / 2F - sz.Width / 2F);
        public static float CenterY(RectangleF rt, SizeF sz) => rt.Y + (rt.Height / 2F - sz.Height / 2F);
        #endregion
        #region MakeRectangle
        public static RectangleF MakeRectangle(RectangleF rect, SizeF size) => FromRect(rect.Left + (rect.Width / 2) - (size.Width / 2), rect.Top + (rect.Height / 2) - (size.Height / 2), size.Width, size.Height);
        public static RectangleF MakeRectangle(PointF cp, float width, float height) => FromRect(cp.X - (width / 2F), cp.Y - (height / 2), width, height);
        #endregion

        #region DevideSize
        public static RectangleF[] DevideSizeH(RectangleF bounds, List<SizeInfo> cols)
        {
            var ret = new RectangleF[cols.Count];
            var tw = bounds.Width;
            var cw = tw - cols.Where(x => x.Mode == DvSizeMode.Pixel).Sum(x => x.Size);

            float x = bounds.Left;
            for (int i = 0; i < cols.Count; i++)
            {
                var v = cols[i];
                var w = v.Mode == DvSizeMode.Pixel ? (v.Size) : (cw * (v.Size / 100F));

                ret[i] = new RectangleF(x, bounds.Y, w, bounds.Height);
                x += w;
            }
            return ret;
        }

        public static RectangleF[] DevideSizeV(RectangleF bounds, List<SizeInfo> rows)
        {
            var ret = new RectangleF[rows.Count];
            var th = bounds.Height;
            var ch = th - rows.Where(x => x.Mode == DvSizeMode.Pixel).Sum(x => x.Size);

            float y = bounds.Top;
            for (int i = 0; i < rows.Count; i++)
            {
                var v = rows[i];
                var h = v.Mode == DvSizeMode.Pixel ? (v.Size) : (ch * (v.Size / 100F));

                ret[i] = new RectangleF(bounds.Left, y, bounds.Width, h);
                y += h;
            }
            return ret;
        }

        public static RectangleF[,] DevideSizeVH(RectangleF bounds, List<SizeInfo> rows, List<SizeInfo> cols)
        {
            var ret = new RectangleF[rows.Count, cols.Count];
            var th = bounds.Height;
            var tw = bounds.Width;
            var cw = tw - cols.Where(x => x.Mode == DvSizeMode.Pixel).Sum(x => x.Size);
            var ch = th - rows.Where(x => x.Mode == DvSizeMode.Pixel).Sum(x => x.Size);

            float y = bounds.Top;
            for (int ir = 0; ir < rows.Count; ir++)
            {
                var vr = rows[ir];
                var h = vr.Mode == DvSizeMode.Pixel ? (vr.Size) : (ch * (vr.Size / 100F));

                float x = bounds.Left;
                for (int ic = 0; ic < cols.Count; ic++)
                {
                    var vc = cols[ic];
                    var w = vc.Mode == DvSizeMode.Pixel ? (vc.Size) : (cw * (vc.Size / 100F));

                    ret[ir, ic] = new RectangleF(x, y, w, h);
                    x += w;
                }

                y += h;
            }
            return ret;
        }
        #endregion
        #region MergeBounds
        public static RectangleF MergeBounds(RectangleF[,] rts, int col, int row, int colspan, int rowspan)
        {
            var rtLT = Util.FromRect(rts[row, col]);
            var rtRB = rts[row + rowspan - 1, col + colspan - 1];
            var rtLT_Right = rtRB.Right;
            var rtLT_Bottom = rtRB.Bottom;

            return new RectangleF(rtLT.X, rtLT.Y, rtLT_Right - rtLT.Left, rtLT_Bottom - rtLT.Top);
        }

        public static RectangleF MergeBoundsH(RectangleF[] rts, int col, int colspan)
        {
            var rtL = Util.FromRect(rts[col]);
            var rtR = rts[col + colspan - 1];
            var rtL_Right = rtR.Right;

            return new RectangleF(rtL.X, rtL.Y, rtL_Right - rtL.Left, rtL.Top);
        }

        public static RectangleF MergeBoundsV(RectangleF[] rts, int row, int rowspan)
        {
            var rtT = Util.FromRect(rts[row]);
            var rtB = rts[row + rowspan - 1];
            var rtT_Bottom = rtB.Bottom;

            return new RectangleF(rtT.X, rtT.Y, rtT.Width, rtT_Bottom - rtT.Top);
        }

        public static RectangleF MergeBounds(RectangleF rt1, RectangleF rt2)
        {
            var L = Math.Min(rt1.Left, rt2.Left);
            var R = Math.Max(rt1.Right, rt2.Right);
            var T = Math.Min(rt1.Top, rt2.Top);
            var B = Math.Max(rt1.Bottom, rt2.Bottom);

            return new RectangleF(L, T, R, B);
        }
        #endregion

        #region CenterPoint
        public static PointF CenterPoint(List<PointF> vertices)
        {
            PointF centroid = new PointF() { X = 0.0F, Y = 0.0F };
            float signedArea = 0.0F;
            float x0 = 0.0F; // Current vertex X
            float y0 = 0.0F; // Current vertex Y
            float x1 = 0.0F; // Next vertex X
            float y1 = 0.0F; // Next vertex Y
            float a = 0.0F;  // Partial signed area

            // For all vertices except last
            int i = 0;
            for (i = 0; i < vertices.Count - 1; ++i)
            {
                x0 = vertices[i].X;
                y0 = vertices[i].Y;
                x1 = vertices[i + 1].X;
                y1 = vertices[i + 1].Y;
                a = x0 * y1 - x1 * y0;
                signedArea += a;
                centroid.X += (x0 + x1) * a;
                centroid.Y += (y0 + y1) * a;
            }

            // Do last vertex
            x0 = vertices[i].X;
            y0 = vertices[i].Y;
            x1 = vertices[0].X;
            y1 = vertices[0].Y;
            a = x0 * y1 - x1 * y0;
            signedArea += a;
            centroid.X += (x0 + x1) * a;
            centroid.Y += (y0 + y1) * a;

            signedArea *= 0.5F;
            centroid.X /= (6 * signedArea);
            centroid.Y /= (6 * signedArea);

            return centroid;
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

        #region TextIconBounds
        public static void TextIconBounds(Graphics g, RectangleF bounds, DvContentAlignment align, string text, Font font, float iconGap, SizeF iconSize,  DvTextIconAlignment iconAlign, Action<RectangleF, RectangleF> act)
        {
            var gap = string.IsNullOrWhiteSpace(text) ? 0F : iconGap;
            var szTX = g.MeasureString(text, font);
            var szFA = iconSize;
            var szv = g.MeasureTextIcon(iconAlign, szFA, gap, text, font);
            var rt = Util.MakeRectangleAlign(bounds, szv, align);

            if (iconAlign == DvTextIconAlignment.LeftRight)
            {
                var rtFA = new RectangleF(rt.X, Util.CenterY(rt, szFA), szFA.Width, szFA.Height);
                var rtTX = new RectangleF(rt.Right - szTX.Width, Util.CenterY(rt, szTX), szTX.Width, szTX.Height);

                act(rtFA, rtTX);
            }
            else
            {
                var rtFA = new RectangleF(Util.CenterX(rt, szFA), rt.Y, szFA.Width, szFA.Height);
                var rtTX = new RectangleF(Util.CenterX(rt, szTX), rt.Bottom - szTX.Height, szTX.Width, szTX.Height);

                act(rtFA, rtTX);
            }
        }
        #endregion

        #region GetBoxPath
        public static GraphicsPath GetBoxPath(RectangleF rt, RoundType round, int Corner)
        {
            GraphicsPath path = null;
            switch (round)
            {
               
                case RoundType.All: path = DrawingTool.GetRoundRectPath(rt, Corner); break;
                case RoundType.L: path = DrawingTool.GetRoundRectPathL(rt, Corner); break;
                case RoundType.R: path = DrawingTool.GetRoundRectPathR(rt, Corner); break;
                case RoundType.T: path = DrawingTool.GetRoundRectPathT(rt, Corner); break;
                case RoundType.B: path = DrawingTool.GetRoundRectPathB(rt, Corner); break;
                case RoundType.LT: path = DrawingTool.GetRoundRectPathLT(rt, Corner); break;
                case RoundType.RT: path = DrawingTool.GetRoundRectPathRT(rt, Corner); break;
                case RoundType.LB: path = DrawingTool.GetRoundRectPathLB(rt, Corner); break;
                case RoundType.RB: path = DrawingTool.GetRoundRectPathRB(rt, Corner); break;
                case RoundType.Rect:
                    path = new GraphicsPath();
                    path.AddRectangle(rt);
                    break;
                case RoundType.Ellipse: 
                    path = new GraphicsPath();
                    path.AddEllipse(rt);
                    break;
            }
            return path;
        }
        #endregion

        #region INT
        public static Rectangle INT(RectangleF rt) => new Rectangle(Convert.ToInt32(rt.X), Convert.ToInt32(rt.Y), Convert.ToInt32(rt.Width), Convert.ToInt32(rt.Height));
        #endregion

        #region Rounds
        public static RoundType[] Rounds(DvDirectionHV dir, RoundType round, int count)
        {
            var ret = new RoundType[count];

            for (int i = 0; i < count; i++) ret[i] = RoundType.Rect;

            if (ret.Length == 1)
            {
                ret[0] = round;
            }
            else if (ret.Length > 1)
            {
                var si = 0;
                var ei = count - 1;

                if (dir == DvDirectionHV.Horizon)
                {
                    switch (round)
                    {
                        #region L / T / R / B
                        case RoundType.L: ret[si] = RoundType.L; ret[ei] = RoundType.Rect; break;
                        case RoundType.R: ret[si] = RoundType.Rect; ret[ei] = RoundType.R; break;
                        case RoundType.T: ret[si] = RoundType.LT; ret[ei] = RoundType.RT; break;
                        case RoundType.B: ret[si] = RoundType.LB; ret[ei] = RoundType.RB; break;
                        #endregion

                        #region LT / RT / LB / RB
                        case RoundType.LT: ret[si] = RoundType.LT; ret[ei] = RoundType.Rect; break;
                        case RoundType.RT: ret[si] = RoundType.Rect; ret[ei] = RoundType.RT; break;
                        case RoundType.LB: ret[si] = RoundType.LB; ret[ei] = RoundType.Rect; break;
                        case RoundType.RB: ret[si] = RoundType.Rect; ret[ei] = RoundType.RB; break;
                        #endregion

                        #region All
                        case RoundType.All: ret[si] = RoundType.L; ret[ei] = RoundType.R; break;
                            #endregion
                    }
                }
                else if (dir == DvDirectionHV.Vertical)
                {
                    switch (round)
                    {
                        #region L / T / R / B
                        case RoundType.L: ret[si] = RoundType.LT; ret[ei] = RoundType.LB; break;
                        case RoundType.R: ret[si] = RoundType.RT; ret[ei] = RoundType.RB; break;
                        case RoundType.T: ret[si] = RoundType.T; ret[ei] = RoundType.Rect; break;
                        case RoundType.B: ret[si] = RoundType.Rect; ret[ei] = RoundType.B; break;
                        #endregion

                        #region LT / RT / LB / RB
                        case RoundType.LT: ret[si] = RoundType.LT; ret[ei] = RoundType.Rect; break;
                        case RoundType.RT: ret[si] = RoundType.RT; ret[ei] = RoundType.Rect; break;
                        case RoundType.LB: ret[si] = RoundType.Rect; ret[ei] = RoundType.LB; break;
                        case RoundType.RB: ret[si] = RoundType.Rect; ret[ei] = RoundType.RB; break;
                        #endregion

                        #region All
                        case RoundType.All: ret[si] = RoundType.T; ret[ei] = RoundType.B; break;
                            #endregion
                    }
                }
            }
            return ret;
        }
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
