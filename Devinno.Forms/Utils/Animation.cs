using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Utils
{
    public class Animation
    {
        #region Properties
        public double TotalMillls { get; private set; }
        public double AclMillls { get; private set; }
        public double DclMillls { get; private set; }
        public double PlayMillis => tmStart.HasValue ? (DateTime.Now - tmStart.Value).TotalMilliseconds : 0;
        public bool IsPlaying => tmStart.HasValue && PlayMillis < TotalMillls;
        public string Variable { get; private set; }
        public int Interval { get; set; } = 5;
        #endregion

        #region Member Variable
        DateTime? tmStart = null;
        bool MO = false;
        #endregion

        #region Method
        #region Start
        public void Start(double totalMillis, string variable = null, Action act = null, Action complete = null)
        {
            this.tmStart = DateTime.Now;
            this.TotalMillls = totalMillis;
            this.AclMillls = totalMillis / 20.0;
            this.DclMillls = totalMillis / 20.0;
            this.Variable = variable;

            if (act != null)
            {
                while (MO) Thread.Sleep(10);

                ThreadPool.QueueUserWorkItem((o) =>
                {
                    MO = true;
                    var tm = DateTime.Now;
                    var ts = totalMillis;
                    while (MO && tmStart.HasValue && (DateTime.Now - tmStart.Value).TotalMilliseconds < ts)
                    {
                        if (act != null) act();
                        Thread.Sleep(Interval);
                    }
                    if (act != null) act();
                    if (complete != null) complete();

                    tmStart = null;
                    MO = false;
                });
            }
        }

        public void Start(double totalMillis, double aclMillis, double dclMillis, string variable = null, Action act = null, Action complete = null)
        {
            this.tmStart = DateTime.Now;
            this.TotalMillls = totalMillis;
            this.AclMillls = aclMillis;
            this.DclMillls = dclMillis;
            this.Variable = variable;

            if (act != null)
            {
                while (MO) Thread.Sleep(10);

                ThreadPool.QueueUserWorkItem((o) =>
                {
                    MO = true;
                    var tm = DateTime.Now;
                    var ts = totalMillis;
                    while (MO && tmStart.HasValue && (DateTime.Now - tmStart.Value).TotalMilliseconds < ts)
                    {
                        if (act != null) act();
                        Thread.Sleep(Interval);
                    }
                    if (act != null) act();
                    if (complete != null) complete();

                    tmStart = null;
                    MO = false;
                });
            }
        }
        #endregion
        #region Stop
        public void Stop()
        {
            this.tmStart = null;
            this.MO = false;
            this.TotalMillls = 0;
        }
        #endregion
        #region Linear / Accel / Decel
        double Linear(double now, double goal) => Linear(PlayMillis, TotalMillls, now, goal);
        double Accel(double now, double goal) => Accel(PlayMillis, TotalMillls, now, goal);
        double Decel(double now, double goal) => Decel(PlayMillis, TotalMillls, now, goal);
        double Profile(double now, double goal) => Profile(PlayMillis, TotalMillls, AclMillls, DclMillls, now, goal);

        public static double Linear(double val, double max, double now, double goal) => now + (goal - now) * MathTool.Constrain(val / max, 0, 1);
        public static double Accel(double val, double max, double now, double goal) => goal - (goal - now) * Math.Sqrt(1.0 - (Math.Pow(MathTool.Constrain(val / max, 0.0, 1.0), 2.0) / Math.Pow(1.0, 2.0)));
        public static double Decel(double val, double max, double now, double goal) => now + (goal - now) * Math.Sqrt(1.0 - (Math.Pow(1.0 - MathTool.Constrain(val / max, 0.0, 1.0), 2.0) / Math.Pow(1.0, 2.0)));
        public static double Profile(double val, double max, double acl, double dcl, double now, double goal)
        {
            if (val < acl)
            {
                var goal2 = acl / max * goal;

                return goal2 - (goal2 - now) * Math.Sqrt(1.0 - (Math.Pow(MathTool.Constrain(MathTool.Map(val, 0, acl, 0, 1), 0.0, 1.0), 2.0) / Math.Pow(1.0, 2.0)));
            }
            else if (val > max - dcl)
            {
                var goal2 = dcl / max * goal;
                var now2 = now - (max - dcl);
                var gap = goal - goal2;

                return gap + now2 + (goal2 - now2) * Math.Sqrt(1.0 - (Math.Pow(1.0 - MathTool.Constrain(MathTool.Map(val, max - dcl, max, 0, 1), 0.0, 1.0), 2.0) / Math.Pow(1.0, 2.0)));
            }
            else
            {
                return now + (goal - now) * MathTool.Constrain(val / max, 0, 1);
            }
        }
        #endregion

        #region Value
        #region Value(int, int)
        public int Value(AnimationAccel velocity, int start, int end)
        {
            int ret = end;
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear: ret = Convert.ToInt32(Linear(start, end)); break;
                    case AnimationAccel.ACL: ret = Convert.ToInt32(Accel(start, end)); break;
                    case AnimationAccel.DCL: ret = Convert.ToInt32(Decel(start, end)); break;
                    case AnimationAccel.Profile: ret = Convert.ToInt32(Profile(start, end)); break;
                }
            }
            return ret;
        }
        #endregion
        #region Value(float, float)
        public float Value(AnimationAccel velocity, float start, float end)
        {
            float ret = end;
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear: ret = Convert.ToSingle(Linear(start, end)); break;
                    case AnimationAccel.ACL: ret = Convert.ToSingle(Accel(start, end)); break;
                    case AnimationAccel.DCL: ret = Convert.ToSingle(Decel(start, end)); break;
                    case AnimationAccel.Profile: ret = Convert.ToSingle(Profile(start, end)); break;
                }
            }
            return ret;
        }
        #endregion
        #region Value(Point, Rect)
        public RectangleF Value(AnimationAccel velocity, PointF start, RectangleF end)
        {
            RectangleF ret = Util.FromRect(end);
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear:
                        ret.X = Convert.ToSingle(Linear(start.X, end.Left));
                        ret.Y = Convert.ToSingle(Linear(start.Y, end.Top));
                        ret.Width = Convert.ToSingle(Linear(start.X, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Linear(start.Y, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.ACL:
                        ret.X = Convert.ToSingle(Accel(start.X, end.Left));
                        ret.Y = Convert.ToSingle(Accel(start.Y, end.Top));
                        ret.Width = Convert.ToSingle(Accel(start.X, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Accel(start.Y, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.DCL:
                        ret.X = Convert.ToSingle(Decel(start.X, end.Left));
                        ret.Y = Convert.ToSingle(Decel(start.Y, end.Top));
                        ret.Width = Convert.ToSingle(Decel(start.X, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Decel(start.Y, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.Profile:
                        ret.X = Convert.ToSingle(Profile(start.X, end.Left));
                        ret.Y = Convert.ToSingle(Profile(start.Y, end.Top));
                        ret.Width = Convert.ToSingle(Profile(start.X, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Profile(start.Y, end.Bottom)) - ret.Y;
                        break;
                }
            }
            return ret;
        }
        #endregion
        #region Value(Rect, Point)
        public RectangleF Value(AnimationAccel velocity, RectangleF start, PointF end)
        {
            RectangleF ret = Util.FromRect(end.X, end.Y, 0, 0);
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear:
                        ret.X = Convert.ToSingle(Linear(start.Left, end.X));
                        ret.Y = Convert.ToSingle(Linear(start.Top, end.Y));
                        ret.Width = Convert.ToSingle(Linear(start.Right, end.X)) - ret.X;
                        ret.Height = Convert.ToSingle(Linear(start.Bottom, end.Y)) - ret.Y;
                        break;
                    case AnimationAccel.ACL:
                        ret.X = Convert.ToSingle(Accel(start.Left, end.X));
                        ret.Y = Convert.ToSingle(Accel(start.Top, end.Y));
                        ret.Width = Convert.ToSingle(Accel(start.Right, end.X)) - ret.X;
                        ret.Height = Convert.ToSingle(Accel(start.Bottom, end.Y)) - ret.Y;
                        break;
                    case AnimationAccel.DCL:
                        ret.X = Convert.ToSingle(Decel(start.Left, end.X));
                        ret.Y = Convert.ToSingle(Decel(start.Top, end.Y));
                        ret.Width = Convert.ToSingle(Decel(start.Right, end.X)) - ret.X;
                        ret.Height = Convert.ToSingle(Decel(start.Bottom, end.Y)) - ret.Y;
                        break;
                    case AnimationAccel.Profile:
                        ret.X = Convert.ToSingle(Profile(start.Left, end.X));
                        ret.Y = Convert.ToSingle(Profile(start.Top, end.Y));
                        ret.Width = Convert.ToSingle(Profile(start.Right, end.X)) - ret.X;
                        ret.Height = Convert.ToSingle(Profile(start.Bottom, end.Y)) - ret.Y;
                        break;
                }
            }
            return ret;
        }
        #endregion
        #region Value(Rect, Rect)
        public RectangleF Value(AnimationAccel velocity, RectangleF start, RectangleF end)
        {
            RectangleF ret = Util.FromRect(end);
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear:
                        ret.X = Convert.ToSingle(Linear(start.Left, end.Left));
                        ret.Y = Convert.ToSingle(Linear(start.Top, end.Top));
                        ret.Width = Convert.ToSingle(Linear(start.Right, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Linear(start.Bottom, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.ACL:
                        ret.X = Convert.ToSingle(Accel(start.Left, end.Left));
                        ret.Y = Convert.ToSingle(Accel(start.Top, end.Top));
                        ret.Width = Convert.ToSingle(Accel(start.Right, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Accel(start.Bottom, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.DCL:
                        ret.X = Convert.ToSingle(Decel(start.Left, end.Left));
                        ret.Y = Convert.ToSingle(Decel(start.Top, end.Top));
                        ret.Width = Convert.ToSingle(Decel(start.Right, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Decel(start.Bottom, end.Bottom)) - ret.Y;
                        break;
                    case AnimationAccel.Profile:
                        ret.X = Convert.ToSingle(Profile(start.Left, end.Left));
                        ret.Y = Convert.ToSingle(Profile(start.Top, end.Top));
                        ret.Width = Convert.ToSingle(Profile(start.Right, end.Right)) - ret.X;
                        ret.Height = Convert.ToSingle(Profile(start.Bottom, end.Bottom)) - ret.Y;
                        break;
                }
            }
            return ret;
        }
        #endregion
        #region Value(Color, Color)
        public Color Value(AnimationAccel velocity, Color start, Color end)
        {
            byte a = end.A, r = end.R, g = end.G, b = end.B;
            if (IsPlaying)
            {
                switch (velocity)
                {
                    case AnimationAccel.Linear:
                        a = Convert.ToByte(Linear(start.A, end.A));
                        r = Convert.ToByte(Linear(start.R, end.R));
                        g = Convert.ToByte(Linear(start.G, end.G));
                        b = Convert.ToByte(Linear(start.B, end.B));
                        break;
                    case AnimationAccel.ACL:
                        a = Convert.ToByte(Accel(start.A, end.A));
                        r = Convert.ToByte(Accel(start.R, end.R));
                        g = Convert.ToByte(Accel(start.G, end.G));
                        b = Convert.ToByte(Accel(start.B, end.B));
                        break;
                    case AnimationAccel.DCL:
                        a = Convert.ToByte(Decel(start.A, end.A));
                        r = Convert.ToByte(Decel(start.R, end.R));
                        g = Convert.ToByte(Decel(start.G, end.G));
                        b = Convert.ToByte(Decel(start.B, end.B));
                        break;
                    case AnimationAccel.Profile:
                        a = Convert.ToByte(Profile(start.A, end.A));
                        r = Convert.ToByte(Profile(start.R, end.R));
                        g = Convert.ToByte(Profile(start.G, end.G));
                        b = Convert.ToByte(Profile(start.B, end.B));
                        break;
                }
            }
            return Util.FromArgb(a, r, g, b);
        }
        #endregion
        #endregion
        #endregion
    }

    #region enum : AnimationAccel 
    public enum AnimationAccel { Linear, ACL, DCL, Profile }
    #endregion
    #region enum : AnimationType 
    public enum AnimationType { SlideV, SlideH, Drill, Fade, None }
    #endregion
}
