using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms
{
    public class Scroll
    {
        #region Const
        public const int SC_WH = 16;
        public const int GapSize = 10;
        public const int GapTime = 1;
        const double decelerationRate = 0.997;
        #endregion

        #region Properties
        public bool IsScrolling => scDown != null;
        public bool IsTouchScrolling => tcDown != null;
        public bool IsTouchMoving => IsTouhcStart;
        public bool TouchMode { get; set; } = false;

        public ScrollDirection Direction { get; set; } = ScrollDirection.Vertical;

        public Func<long> GetScrollTotal { get; set; }
        public Func<long> GetScrollView { get; set; }
        public Func<long> GetScrollTick { get; set; }
        public Func<double> GetScrollScaleFactor { get; set; }

        public long ScrollTotal { get { return GetScrollTotal != null ? GetScrollTotal() : 0; } }
        public long ScrollView { get { return GetScrollView != null ? GetScrollView() : 0; } }
        public long ScrollTick { get { return GetScrollTick != null ? GetScrollTick() : 0; } }
        public double ScrollScaleFactor { get { return GetScrollScaleFactor != null ? GetScrollScaleFactor() : 1D; } }
        private long _ScrollPosition = 0;
        public long ScrollPosition
        {
            get
            {
                if (ScrollView < ScrollTotal) _ScrollPosition = Convert.ToInt64(MathTool.Constrain(_ScrollPosition, 0, ScrollTotal - ScrollView));
                else _ScrollPosition = 0;
                return _ScrollPosition;
            }
            set
            {
                if (ScrollView < ScrollTotal) _ScrollPosition = Convert.ToInt64(MathTool.Constrain(value, 0, ScrollTotal - ScrollView));
                else _ScrollPosition = 0;
            }
        }

        public long TouchOffset
        {
            get
            {
                if (TouchMode)
                {
                    var ret = 0L;
                    if (Direction == ScrollDirection.Vertical) ret = tcDown != null && ScrollView < ScrollTotal ? Convert.ToInt64((tcDown.MovePoint.Y - tcDown.DownPoint.Y) * ScrollScaleFactor) : 0;
                    else ret = tcDown != null && ScrollView < ScrollTotal ? Convert.ToInt64((tcDown.MovePoint.X - tcDown.DownPoint.X) * ScrollScaleFactor) : 0;
                    return ret;
                }
                else return 0;
            }
        }

        public long ScrollPositionWithOffset
        {
            get
            {
                return -ScrollPosition + TouchOffset;
            }
        }

        public long ScrollPositionWithOffsetR
        {
            get
            {
                return -(-ScrollPosition - TouchOffset);
            }
        }
        #endregion

        #region Member Variable
        SCDI scDown = null;
        TCDI tcDown = null;

        double initPos;
        double initVel;
        double destPos;
        double destTime;
        double dCoeff = 1000 * Math.Log(decelerationRate);
        double threshold = 0.1;

        bool IsTouhcStart;
        #endregion

        #region Event
        public event EventHandler ScrollChanged;
        #endregion

        #region Constructor
        public Scroll()
        {

        }
        #endregion

        #region Method
        #region GetScrollCursorRect(rtScroll)
        public Rectangle? GetScrollCursorRect(Rectangle rtScroll)
        {
            rtScroll.Inflate(-6, -6);
            if (ScrollView < ScrollTotal)
            {
                if (Direction == ScrollDirection.Vertical)
                {
                    int h = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Height));
                    h = Math.Max(h, 30);

                    int y = 0;
                    y = Convert.ToInt32(MathTool.Map(ScrollPosition - TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Top, rtScroll.Bottom - h));
                    return new Rectangle(rtScroll.X, y, rtScroll.Width, h);
                }
                else
                {
                    int w = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    w = Math.Max(w, 30);

                    int x = 0;
                    x = Convert.ToInt32(MathTool.Map(ScrollPosition - TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Left, rtScroll.Right - w));
                    return new Rectangle(x, rtScroll.Y, w, rtScroll.Height);
                }
            }

            else return null;
        }
        #endregion

        #region MouseWheel
        public void MouseWheel(MouseEventArgs e)
        {
            ScrollPosition += ((e.Delta / -120) * ScrollTick);
        }
        #endregion
        #region MouseDown
        public void MouseDown(MouseEventArgs e, Rectangle rtScroll)
        {
            var rtcur = GetScrollCursorRect(rtScroll);
            if (!IsTouhcStart && rtcur.HasValue && CollisionTool.Check(rtcur.Value, e.Location)) scDown = new SCDI() { DownPoint = e.Location, CursorBounds = rtcur.Value };
        }
        #endregion
        #region MouseUp
        public void MouseUp(MouseEventArgs e)
        {
            if (scDown != null) scDown = null;
        }
        #endregion
        #region MouseMove
        public void MouseMove(MouseEventArgs e, Rectangle rtScroll)
        {
            if (scDown != null)
            {
                if (Direction == ScrollDirection.Vertical)
                {
                    var h = Convert.ToInt64(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Height));
                    h = Math.Max(h, 30);
                    ScrollPosition = Convert.ToInt64(MathTool.Map(e.Y - (scDown.DownPoint.Y - scDown.CursorBounds.Y), rtScroll.Top, rtScroll.Bottom - h, 0, ScrollTotal - ScrollView));
                }
                else if (Direction == ScrollDirection.Horizon)
                {
                    var w = Convert.ToInt64(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    w = Math.Max(w, 30);
                    ScrollPosition = Convert.ToInt64(MathTool.Map(e.X - (scDown.DownPoint.X - scDown.CursorBounds.X), rtScroll.Left, rtScroll.Right - w, 0, ScrollTotal - ScrollView));
                }
            }
        }
        #endregion

        #region TouchDown
        public void TouchDown(MouseEventArgs e)
        {
            if (TouchMode)
            {
                tcDown = new TCDI() { DownPoint = e.Location, MovePoint = e.Location, DownTime = DateTime.Now };
                IsTouhcStart = false;
            }
        }
        #endregion
        #region TouchMove
        public void TouchMove(MouseEventArgs e)
        {
            if (TouchMode)
            {
                if (tcDown != null)
                    tcDown.MovePoint = e.Location;
            }
        }
        #endregion
        #region TouchUp
        public void TouchUp(MouseEventArgs e)
        {
            if(TouchMode && tcDown != null)
            {
                if (ScrollView < ScrollTotal)
                {
                    if (Direction == ScrollDirection.Vertical)
                    {
                        ScrollPosition = Convert.ToInt64(MathTool.Constrain(ScrollPosition + ((tcDown.DownPoint.X - e.X) * ScrollScaleFactor), 0, (ScrollTotal - ScrollView)));
                        initPos = ScrollPosition;
                        initVel = ((tcDown.DownPoint.Y - e.Y) * ScrollScaleFactor) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    else
                    {
                        ScrollPosition = Convert.ToInt64(MathTool.Constrain(ScrollPosition + ((tcDown.DownPoint.X - e.X) * ScrollScaleFactor), 0, (ScrollTotal - ScrollView)));
                        initPos = ScrollPosition;
                        initVel = ((tcDown.DownPoint.X - e.X) * ScrollScaleFactor) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    if (Math.Abs(initPos - destPos) > GapSize && destTime > GapTime)
                    {
                        var th = new Thread(new ThreadStart(() =>
                        {
                            IsTouhcStart = true;

                            var stime = DateTime.Now;
                            var time = 0.0;
                            var tot = (ScrollTotal - ScrollView);
                            while (IsTouhcStart && time < destTime * 1000 && Convert.ToInt64(ScrollPosition / ScrollScaleFactor) != Convert.ToInt64(destPos / ScrollScaleFactor))
                            {
                                time = (DateTime.Now - stime).TotalMilliseconds;
                                var oldV = ScrollPosition;
                                var newV = MathTool.Constrain(Convert.ToInt64(initPos + (Math.Pow(decelerationRate, time) - 1) / dCoeff * initVel), 0, tot);
                                if (oldV != newV) { ScrollPosition = newV; try { ScrollChanged?.Invoke(this, null); } catch { } }

                                Thread.Sleep(10);
                            }

                            IsTouhcStart = false;
                            try { ScrollChanged?.Invoke(this, null); } catch { }

                        }))
                        { IsBackground = true };
                        th.Start();

                    }
                }
                tcDown = null;
            }
        }
        #endregion

        #region GetScrollCursorRectR(rtScroll)
        public Rectangle? GetScrollCursorRectR(Rectangle rtScroll)
        {
            rtScroll.Inflate(-6, -6);
            if (ScrollView < ScrollTotal)
            {
                if (Direction == ScrollDirection.Vertical)
                {
                    int h = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Height));
                    h = Math.Max(h, 30);

                    int y = 0;
                    y = Convert.ToInt32(MathTool.Map(ScrollPosition + TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Bottom - h, rtScroll.Top));
                    return new Rectangle(rtScroll.X, y, rtScroll.Width, h);
                }
                else
                {
                    int w = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    w = Math.Max(w, 30);

                    int x = 0;
                    x = Convert.ToInt32(MathTool.Map(ScrollPosition + TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Right - w, rtScroll.Left));
                    return new Rectangle(x, rtScroll.Y, w, rtScroll.Height);
                }
            }

            else return null;
        }
        #endregion

        #region MouseDownR
        public void MouseDownR(MouseEventArgs e, Rectangle rtScroll)
        {
            var rtcur = GetScrollCursorRectR(rtScroll);
            if (!IsTouhcStart && rtcur.HasValue && CollisionTool.Check(rtcur.Value, e.Location)) scDown = new SCDI() { DownPoint = e.Location, CursorBounds = rtcur.Value };
        }
        #endregion
        #region MouseUpR
        public void MouseUpR(MouseEventArgs e)
        {
            if (scDown != null) scDown = null;
        }
        #endregion
        #region MouseMoveR
        public void MouseMoveR(MouseEventArgs e, Rectangle rtScroll)
        {
            if (scDown != null)
            {
                if (Direction == ScrollDirection.Vertical)
                {
                    var h = Convert.ToInt64(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Height));
                    h = Math.Max(h, 30);
                    ScrollPosition = Convert.ToInt64(MathTool.Map(e.Y - (scDown.DownPoint.Y - scDown.CursorBounds.Y), rtScroll.Bottom - h, rtScroll.Top,  0, ScrollTotal - ScrollView));
                }
                else if (Direction == ScrollDirection.Horizon)
                {
                    var w = Convert.ToInt64(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    w = Math.Max(w, 30);
                    ScrollPosition = Convert.ToInt64(MathTool.Map(e.X - (scDown.DownPoint.X - scDown.CursorBounds.X), rtScroll.Right - w, rtScroll.Left,  0, ScrollTotal - ScrollView));
                }
            }
        }
        #endregion

        #region TouchDownR
        public void TouchDownR(MouseEventArgs e)
        {
            if (TouchMode)
            {
                tcDown = new TCDI() { DownPoint = e.Location, MovePoint = e.Location, DownTime = DateTime.Now };
                IsTouhcStart = false;
            }
        }
        #endregion
        #region TouchMoveR
        public void TouchMoveR(MouseEventArgs e)
        {
            if (TouchMode)
            {
                if (tcDown != null)
                    tcDown.MovePoint = e.Location;
            }
        }
        #endregion
        #region TouchUpR
        public void TouchUpR(MouseEventArgs e)
        {
            if (TouchMode && tcDown != null)
            {
                if (ScrollView < ScrollTotal)
                {
                    if (Direction == ScrollDirection.Vertical)
                    {
                        ScrollPosition = Convert.ToInt64(MathTool.Constrain(ScrollPosition + ((e.X - tcDown.DownPoint.X) * ScrollScaleFactor), 0, (ScrollTotal - ScrollView)));
                        initPos = ScrollPosition;
                        initVel = ((e.Y - tcDown.DownPoint.Y) * ScrollScaleFactor) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    else
                    {
                        ScrollPosition = Convert.ToInt64(MathTool.Constrain(ScrollPosition + ((e.X - tcDown.DownPoint.X) * ScrollScaleFactor), 0, (ScrollTotal - ScrollView)));
                        initPos = ScrollPosition;
                        initVel = ((e.X - tcDown.DownPoint.X) * ScrollScaleFactor) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    if (Math.Abs(initPos - destPos) > GapSize && destTime > GapTime)
                    {
                        var th = new Thread(new ThreadStart(() =>
                        {
                            IsTouhcStart = true;

                            var stime = DateTime.Now;
                            var time = 0.0;
                            var tot = (ScrollTotal - ScrollView);
                            while (IsTouhcStart && time < destTime * 1000 && Convert.ToInt64(ScrollPosition / ScrollScaleFactor) != Convert.ToInt64(destPos / ScrollScaleFactor))
                            {
                                time = (DateTime.Now - stime).TotalMilliseconds;
                                var oldV = ScrollPosition;
                                var newV = MathTool.Constrain(Convert.ToInt64(initPos + (Math.Pow(decelerationRate, time) - 1) / dCoeff * initVel), 0, tot);
                                if (oldV != newV) { ScrollPosition = newV; try { ScrollChanged?.Invoke(this, null); } catch { } }

                                Thread.Sleep(10);
                            }

                            IsTouhcStart = false;
                            try { ScrollChanged?.Invoke(this, null); } catch { }

                        }))
                        { IsBackground = true };
                        th.Start();

                    }
                }
                tcDown = null;
            }
        }
        #endregion
        #endregion

    }

    public enum ScrollDirection { Horizon, Vertical }

    internal class SCDI
    {
        internal Point DownPoint { get; set; }
        internal Rectangle CursorBounds { get; set; }
    }

    internal class TCDI
    {
        internal Point DownPoint { get; set; }
        internal Point MovePoint { get; set; }
        internal DateTime DownTime { get; set; }
    }
}
