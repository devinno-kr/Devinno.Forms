using Devinno.Forms.Extensions;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Devinno.Forms.Icons
{
    public partial class FormIconFA : Form
    {
        #region Properties
        public IconFA Result { get => IconList[CurrentIndex]; }
        public StyleFA Style { get; set; } = StyleFA.Solid;

        #region ViewCount
        public int ViewCount { get { var rtContent = GetContentBounds(); return (rtContent.Width / ItemSize.Width) * (rtContent.Height / ItemSize.Height); } }
        #endregion
        #region Page
        public int Page { get => GetPage(PageItemIndex); }
        #endregion
        #region MaxPage
        public int MaxPage { get => (int)Math.Ceiling((double)IconList.Count / (double)ViewCount) - 1; }
        #endregion
        #region CurrentIndex
        private int nCurrentIndex = 0;
        public int CurrentIndex
        {
            get => nCurrentIndex;
            set
            {
                var val = (int)MathTool.Constrain(value, 0, IconList.Count - 1);
                if (nCurrentIndex != val)
                {
                    nCurrentIndex = val;
                    Invalidate();
                }
            }
        }
        #endregion
        #region PageItemIndex
        int PageItemIndex { get; set; } = 0;
        #endregion
        #region ItemSize
        public Size ItemSize { get; set; } = new Size(180, 60);
        #endregion
        #endregion
        #region Member Variable
        List<IconFA> IconList = new List<IconFA>();
        #endregion

        public FormIconFA()
        {
            InitializeComponent();
            #region Event
            btnOK.Click += (o, s) => { DialogResult = DialogResult.OK; };
            btnCancel.Click += (o, s) => { DialogResult = DialogResult.Cancel; };

            btnPrev.Click += (o, s) => PrevPage();
            btnNext.Click += (o, s) => NextPage();

            radSolid.CheckedChanged += (o, s) => SetStyleFA();
            #endregion
            #region Set
            radSolid.Checked = true;

            IconList = Enum.GetValues(typeof(IconFA)).Cast<IconFA>().OrderBy(x => x.ToString()).ToList();
            #endregion
        }

        #region Override
        #region OnPaint
        protected override void OnPaint(PaintEventArgs e)
        {
            #region Init
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var br = new SolidBrush(Color.White);
            var p = new Pen(Color.Black);
            #endregion
            #region Draw
            #region Bounds
            var rtContent = GetContentBounds();
            var rtDes = GetDescriptBounds();

            int cntw = rtContent.Width / ItemSize.Width;
            int cnth = rtContent.Height / ItemSize.Height;
            float itemw = (float)rtContent.Width / (float)cntw;
            float itemh = (float)rtContent.Height / (float)cnth;
            #endregion
            #region Items
            br.Color = Color.White; e.Graphics.FillRectangle(br, rtContent);
            p.Color = SystemColors.ControlDark; e.Graphics.DrawRectangle(p, rtContent);

            for (int iy = 0, iv = Page * (cntw * cnth); iy < cnth; iy++)
            {
                for (int ix = 0; ix < cntw; ix++, iv++)
                {
                    if (iv < IconList.Count)
                    {
                        var v = IconList[iv];
                        var rt = new RectangleF(ix * itemw, iy * itemh, itemw, itemh); rt.Offset(rtContent.X, rtContent.Y);
                        var rtIco = new Rectangle((int)rt.X, (int)rt.Y, (int)rt.Width, (int)rt.Height - 18);
                        var rtTxt = new Rectangle((int)rt.X, (int)rt.Y + ((int)rt.Height - 18), (int)rt.Width, 18);

                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        p.Color = SystemColors.ControlDark;
                        if (ix != cntw - 1) e.Graphics.DrawLine(p, (int)rt.Right - 1, (int)rt.Top, (int)rt.Right - 1, (int)rt.Bottom - 1);
                        if (iy != cnth - 1) e.Graphics.DrawLine(p, (int)rt.Left, (int)rt.Bottom - 1, (int)rt.Right - 1, (int)rt.Bottom - 1);

                        e.Graphics.DrawIcon(new DvIcon(v, Style) { IconFASize = 18, Alignment = DvTextIconAlignment.TopBottom }, br, rtIco, DvContentAlignment.MiddleCenter);
                        e.Graphics.DrawText(v.ToString(), Font, br, rtTxt, DvContentAlignment.MiddleCenter);
                    }
                }
            }
            #endregion
            #region Selected
            if (CurrentIndex >= Page * (cntw * cnth) && CurrentIndex < Page * (cntw * cnth) + ViewCount)
            {
                var ci = CurrentIndex - (Page * (cntw * cnth));
                var iy = ci / cntw;
                var ix = ci - (iy * cntw);
                var rt = new RectangleF(ix * itemw, iy * itemh, itemw, itemh);
                rt.Offset(rtContent.X, rtContent.Y);

                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                p.Color = Color.Red;

                e.Graphics.DrawLine(p, (int)rt.Left, (int)rt.Top, (int)rt.Right - 1, (int)rt.Top);
                e.Graphics.DrawLine(p, (int)rt.Left, (int)rt.Bottom - 1, (int)rt.Right - 1, (int)rt.Bottom - 1);
                e.Graphics.DrawLine(p, (int)rt.Left, (int)rt.Top, (int)rt.Left, (int)rt.Bottom - 1);
                e.Graphics.DrawLine(p, (int)rt.Right - 1, (int)rt.Top, (int)rt.Right - 1, (int)rt.Bottom - 1);
            }
            #endregion
            #region Page Text
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            br.Color = Color.White; e.Graphics.FillRectangle(br, rtDes);
            p.Color = SystemColors.ControlDark; e.Graphics.DrawRectangle(p, rtDes);

            e.Graphics.DrawText((Page + 1) + " / " + (MaxPage + 1), Font, br, rtDes, DvContentAlignment.MiddleCenter);
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
            #endregion
            base.OnPaint(e);
        }
        #endregion
        #region MouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            #region Bounds
            var rtContent = GetContentBounds();
            var rtDes = GetDescriptBounds();

            int cntw = rtContent.Width / ItemSize.Width;
            int cnth = rtContent.Height / ItemSize.Height;
            float itemw = (float)rtContent.Width / (float)cntw;
            float itemh = (float)rtContent.Height / (float)cnth;
            #endregion
            for (int iy = 0, iv = Page * (cntw * cnth); iy < cnth; iy++)
            {
                for (int ix = 0; ix < cntw; ix++, iv++)
                {
                    if (iv < IconList.Count)
                    {
                        var v = IconList[iv];
                        var rt = new RectangleF(ix * itemw, iy * itemh, itemw, itemh); rt.Offset(rtContent.X, rtContent.Y);
                        if (CollisionTool.Check(rt, e.Location))
                        {
                            CurrentIndex = iv;
                            lblSelect.Text = v.ToString();
                            Invalidate();
                        }
                    }
                }
            }
            base.OnMouseDown(e);
        }
        #endregion
        #endregion

        #region Method
        #region PageControl
        #region PrevPage / NextPage
        //void NextPage() { PageIndex = (int)MathTool.Constrain(PageIndex + ViewCount, 0, IconList.Count - 1); Invalidate(); }
        //void PrevPage() { PageIndex = (int)MathTool.Constrain(PageIndex - ViewCount, 0, IconList.Count - 1); Invalidate(); }

        void NextPage() { if (Page + 1 <= MaxPage) PageItemIndex = ViewCount * (Page + 1); Invalidate(); }
        void PrevPage() { if (Page - 1 >= 0) PageItemIndex = ViewCount * (Page - 1); Invalidate(); }
        #endregion
        #region GetContentBounds
        Rectangle GetContentBounds()
        {
            var rtContent = new Rectangle(btnPrev.Right + 5, btnPrev.Top, btnNext.Left - btnPrev.Right - 10, btnPrev.Height - 1 - 27); rtContent.Inflate(-1, -1);
            return rtContent;
        }
        #endregion
        #region GetDescriptBounds
        Rectangle GetDescriptBounds()
        {
            var rtContent = GetContentBounds();
            var rtDes = new Rectangle(rtContent.X, rtContent.Bottom, rtContent.Width, 27);
            return rtDes;
        }
        #endregion
        #region GetPage
        int GetPage(int index)
        {
            return (int)MathTool.Constrain(Math.Floor((double)index / (double)ViewCount), 0, MaxPage);
        }
        #endregion
        #region SetPage
        void SetPage(int index)
        {
            PageItemIndex = ViewCount * GetPage(index);
        }
        #endregion
        #endregion
        #region ShowIconFA
        public DialogResult ShowIconFA(IWindowsFormsEditorService wfes, IconFA v)
        {
            Style = StyleFA.Solid;
            radSolid.Checked = true;
            lblSelect.Text = v.ToString();
            var r = IconList.IndexOf(v);
            if (r != -1)
            {
                CurrentIndex = r;
                SetPage(CurrentIndex);
            }
            else
            {
                CurrentIndex = 0;
                SetPage(0);
            }
            return this.ShowDialog();
        }
        #endregion
        #region SetStyleFA
        void SetStyleFA()
        {
            if (radBrands.Checked) Style = StyleFA.Brands;
            else if (radRegular.Checked) Style = StyleFA.Regular;
            else Style = StyleFA.Solid;

            Invalidate();
        }
        #endregion
        #endregion
    }
}
