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
        const int GapSize = 10;
        const int GapTime = 1;
        const double decelerationRate = 0.997;
        #endregion

        #region Properties
        public bool IsScrolling => scDown != null;
        public bool IsTouchScrolling => tcDown != null;
        public bool TouchMode { get; set; } = false;

        public ScrollDirection Direction { get; set; } = ScrollDirection.Vertical;

        public Func<int> GetScrollTotal { get; set; }
        public Func<int> GetScrollView { get; set; }
        public Func<int> GetScrollTick { get; set; }

        public int ScrollTotal { get { return GetScrollTotal != null ? GetScrollTotal() : 0; } }
        public int ScrollView { get { return GetScrollView != null ? GetScrollView() : 0; } }
        public int ScrollTick { get { return GetScrollTick != null ? GetScrollTick() : 0; } }

        private int _ScrollPosition = 0;
        public int ScrollPosition
        {
            get
            {
                if (ScrollView < ScrollTotal) _ScrollPosition = Convert.ToInt32(MathTool.Constrain(_ScrollPosition, 0, ScrollTotal - ScrollView));
                else _ScrollPosition = 0;
                return _ScrollPosition;
            }
            set
            {
                if (ScrollView < ScrollTotal) _ScrollPosition = Convert.ToInt32(MathTool.Constrain(value, 0, ScrollTotal - ScrollView));
                else _ScrollPosition = 0;
            }
        }

        public int TouchOffset 
        {
            get
            {
                if (TouchMode)
                {
                    if (Direction == ScrollDirection.Vertical) return tcDown != null && ScrollView < ScrollTotal ? tcDown.MovePoint.Y - tcDown.DownPoint.Y : 0;
                    else return tcDown != null && ScrollView < ScrollTotal ? tcDown.MovePoint.X - tcDown.DownPoint.X : 0;
                }
                else return 0;
            }
        }

        public int ScrollPositionWithOffset => -ScrollPosition + TouchOffset;
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
                    int y = Convert.ToInt32(MathTool.Map(ScrollPosition - TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Top, rtScroll.Bottom - h));

                    return new Rectangle(rtScroll.X, y, rtScroll.Width, h);
                }
                else
                {
                    int w = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    int x = Convert.ToInt32(MathTool.Map(ScrollPosition - TouchOffset, 0, ScrollTotal - ScrollView, rtScroll.Left, rtScroll.Right - w));

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
            ScrollPosition += ((e.Delta / -120) * ScrollTick);
            var rtcur = GetScrollCursorRect(rtScroll);
            if (rtcur.HasValue && CollisionTool.Check(rtcur.Value, e.Location)) scDown = new SCDI() { DownPoint = e.Location, CursorBounds = rtcur.Value };
        }
        #endregion
        #region MouseUP
        public void MouseUP(MouseEventArgs e)
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
                    int h = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Height));
                    ScrollPosition = Convert.ToInt32(MathTool.Map(e.Y - (scDown.DownPoint.Y - scDown.CursorBounds.Y), rtScroll.Top, rtScroll.Bottom - h, 0, ScrollTotal - ScrollView));
                }
                else if(Direction == ScrollDirection.Horizon)
                {
                    int w = Convert.ToInt32(MathTool.Map(ScrollView, 0, ScrollTotal, 0, rtScroll.Width));
                    ScrollPosition = Convert.ToInt32(MathTool.Map(e.X - (scDown.DownPoint.X - scDown.CursorBounds.X), rtScroll.Left, rtScroll.Right - w, 0, ScrollTotal - ScrollView));
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
                        ScrollPosition = (int)MathTool.Constrain(ScrollPosition + (tcDown.DownPoint.Y - e.Y), 0, (ScrollTotal - ScrollView));
                        initPos = ScrollPosition;
                        initVel = (tcDown.DownPoint.Y - e.Y) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = (int)MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    else
                    {
                        ScrollPosition = (int)MathTool.Constrain(ScrollPosition + (tcDown.DownPoint.X - e.X), 0, (ScrollTotal - ScrollView));
                        initPos = ScrollPosition;
                        initVel = (tcDown.DownPoint.X - e.X) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0);
                        destPos = (int)MathTool.Constrain(initPos - initVel / dCoeff, 0, (ScrollTotal - ScrollView));
                        destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;
                    }
                    if (Math.Abs(initPos - destPos) > GapSize && destTime > GapTime)
                    {
                        var th = new Thread(new ThreadStart(() =>
                        {
                            IsTouhcStart = true;

                            var stime = DateTime.Now;
                            var time = 0.0;
                            while (IsTouhcStart && time < destTime * 1000 && ScrollPosition != (int)destPos)
                            {
                                time = (DateTime.Now - stime).TotalMilliseconds;
                                var oldV = ScrollPosition;
                                var newV = (int)MathTool.Constrain(Convert.ToInt32(initPos + (Math.Pow(decelerationRate, time) - 1) / dCoeff * initVel), 0, (ScrollTotal - ScrollView));
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
