using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms
{
    public class LongClick
    {
        public bool UseLongClick { get; set; } = false;
        public int LongClickTime { get; set; } = 0;

        private bool bDownLongClick = false;
        private DateTime dtDown;

        public Action GenLongClick;
        public Action Reset;

        public void MouseDown(MouseEventArgs e)
        {
            dtDown = DateTime.Now;
            if (UseLongClick && LongClickTime > 0)
            {
                if (!bDownLongClick)
                {
                    bDownLongClick = true;

                    var th = new Thread(new ThreadStart(() =>
                    {
                        while (bDownLongClick && (DateTime.Now - dtDown).TotalMilliseconds < LongClickTime) Thread.Sleep(100);

                        if (bDownLongClick && (DateTime.Now - dtDown).TotalMilliseconds >= LongClickTime)
                        {
                            bDownLongClick = false;

                            if (Reset != null) Reset();
                            if (GenLongClick != null) GenLongClick();
                        }
                        bDownLongClick = false;
                    }))
                    { IsBackground = true };
                    th.Start();
                }
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            bDownLongClick = false;
        }
    }

    public class ClickExEventArgs
    {
        public TimeSpan Time;
        public ClickExEventArgs(TimeSpan Time) => this.Time = Time;
    }
}
