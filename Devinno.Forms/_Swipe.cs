using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms
{
    public  class Swipe
    {
        #region Const
        public const int GapSize = 10;
        public const int GapTime = 1;
        const double decelerationRate = 0.998;
        const int ThreadInterval = 10;
        #endregion

        #region Properties
        public bool TouchMode { get; set; } = false;

        public bool IsPageChanging => IsTouhcStart;
        public bool IsTouchScrolling => tcDown != null;
        
        public Func<int> GetPageCount { get; set; }
        public Func<int> GetPageWidth { get; set; }
        public Func<double> GetScrollScaleFactor { get; set; }


        public int PageCount { get { return GetPageCount != null ? GetPageCount() : 0; } }
        public int PageWidth { get { return GetPageWidth != null ? GetPageWidth() : 0; } }
        public double ScrollScaleFactor { get { return GetScrollScaleFactor != null ? GetScrollScaleFactor() : 1D; } }
        
        private int _CurrentPage = -1;
        public int CurrentPage
        {
            get => _CurrentPage;
            set
            {
                var v = PageCount == 0 ? -1 : Convert.ToInt32(MathTool.Constrain(value, 0, PageCount - 1));
                if (_CurrentPage != v)
                {
                    _CurrentPage = v;
                }
            }
        }

        public long TouchOffset
        {
            get
            {
                if (TouchMode)
                {
                    return tcDown != null && PageCount > 0 ? Convert.ToInt64((tcDown.MovePoint.X - tcDown.DownPoint.X) * ScrollScaleFactor) : 0;
                }
                else return 0;
            }
        }

        long ScrollOffset;
        long ScrollPosition => (CurrentPage == -1 ? 0 : 0 + (IsTouhcStart ? ScrollOffset : 0));
        long ScrollTotal => PageWidth * PageCount;
        long ScrollView => PageWidth;

        public long ScrollPositionWithOffset => -ScrollPosition + TouchOffset;

        #endregion

        #region Member Variable
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
            if (TouchMode && tcDown != null)
            {
                initPos = ((tcDown.DownPoint.X - e.X) * ScrollScaleFactor);
                initVel = (((tcDown.DownPoint.X - e.X) * ScrollScaleFactor) / ((double)(DateTime.Now - tcDown.DownTime).TotalMilliseconds / 1000.0)) * 2.0;
                destPos = initPos - initVel / dCoeff;
                var destPos2 = tcDown.DownPoint.X < e.X ? -PageWidth : +PageWidth;
                destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;

                if (Math.Abs(initPos - destPos) > GapSize && destTime > GapTime && (initPos > destPos ? (destPos < destPos2) : (destPos > destPos2)))
                {
                    destPos = destPos2;
                    destTime = Math.Log(-dCoeff * threshold / Math.Abs(initVel)) / dCoeff;

                    var th = new Thread(new ThreadStart(() =>
                    {
                        IsTouhcStart = true;
                        ScrollOffset = 0;

                        var stime = DateTime.Now;
                        var time = 0.0;
                        var tot = (ScrollTotal - ScrollView);
                        var target = CurrentPage;

                        if (initPos > destPos) target = CurrentPage - 1;
                        else if (initPos < destPos) target = CurrentPage + 1;

                        if (target < 0) target = PageCount - 1;
                        if (target >= PageCount) target = 0;

                        while (IsTouhcStart && time < destTime * 1000)
                        {
                            time = (DateTime.Now - stime).TotalMilliseconds;
                            var oldV = ScrollOffset;
                            var newV = Convert.ToInt64(initPos + (Math.Pow(decelerationRate, time) - 1) / dCoeff * initVel);
                            if (oldV != newV) { ScrollOffset = newV; try { ScrollChanged?.Invoke(this, null); } catch { } }

                            if (initPos > destPos && ScrollOffset <= destPos) 
                                break;
                            else if (initPos < destPos && ScrollOffset >= destPos) 
                                break;

                            Thread.Sleep(ThreadInterval);
                        }

                        CurrentPage = target;

                        IsTouhcStart = false;
                        try { ScrollChanged?.Invoke(this, null); } catch { }

                    }))
                    { IsBackground = true };
                    th.Start();

                }

                tcDown = null;
            }
        }
        #endregion
    }
}
