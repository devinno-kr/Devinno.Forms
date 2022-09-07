using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using Devinno.Forms.Tools;
using Devinno.Forms.Utils;
using Devinno.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Controls
{
    public class DvComboBox : DvControl
    {
        #region Properties
        #region BoxColor
        private Color? cBoxColor = null;
        public Color? BoxColor
        {
            get => cBoxColor;
            set
            {
                if (cBoxColor != value)
                {
                    cBoxColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedItemColor
        private Color? cSelectedItemColor = null;
        public Color? SelectedItemColor
        {
            get => cSelectedItemColor;
            set
            {
                if (cSelectedItemColor != value)
                {
                    cSelectedItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ItemColor
        private Color? cItemColor = null;
        public Color? ItemColor
        {
            get => cItemColor;
            set
            {
                if (cItemColor != value)
                {
                    cItemColor = value;
                    Invalidate();
                }
            }
        }
        #endregion
        
        #region ButtonWidth
        private int nButtonWidth = 40;
        public int ButtonWidth
        {
            get => nButtonWidth; 
            set
            {
                if (nButtonWidth != value)
                {
                    nButtonWidth = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region MaximumViewCount
        private int nMaximumViewCount = 8;
        public int MaximumViewCount
        {
            get => nMaximumViewCount;
            set
            {
                if (nMaximumViewCount != value)
                {
                    nMaximumViewCount = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ItemHeight
        private int nItemHeight = 30;
        public int ItemHeight
        {
            get => nItemHeight;
            set
            {
                if (nItemHeight != value)
                {
                    nItemHeight = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region SelectedIndex
        private int nSelectedIndex = -1;
        public int SelectedIndex
        {
            get => nSelectedIndex;
            set
            {
                if (nSelectedIndex != value)
                {
                    nSelectedIndex = value;
                    SelectedIndexChanged?.Invoke(this, new EventArgs());
                    Invalidate();
                }
            }
        }
        #endregion
        #region TouchMode
        private bool bTouchMode = false;
        public bool TouchMode
        {
            get => bTouchMode;
            set
            {
                if (bTouchMode != value)
                {
                    bTouchMode = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Round
        private RoundType? round = null;
        public RoundType? Round
        {
            get => round;
            set
            {
                if (round != value)
                {
                    round = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region Items
        public List<TextIcon> Items { get; } = new List<TextIcon>();
        #endregion
        #endregion

        #region Member Variable
        bool bDown = false;
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Constructor
        public DvComboBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 100);
        }
        #endregion

        #region Override
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Var
            var BoxColor = this.BoxColor ?? Theme.ButtonColor;
            var ItemColor = this.ItemColor ?? Theme.ButtonColor;
            var SelectedItemColor = this.SelectedItemColor ?? Theme.PointColor;
            var BorderColor = Theme.GetBorderColor(BoxColor,  BackColor);

            var Round = this.Round ?? RoundType.All;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Init
            var p = new Pen(Color.Black);
            var br = new SolidBrush(Color.Black);
            #endregion

            Areas((rtContent, rtBox, rtIco, rtText) =>
            {
                #region Box
                if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
                {
                    var rndU = RoundType.Rect;
                    var rndD = RoundType.Rect;

                    switch (Round)
                    {
                        case RoundType.Ellipse: rndU = RoundType.Rect; rndD = RoundType.Rect; break;
                        case RoundType.Rect: rndU = RoundType.Rect; rndD = RoundType.Rect; break;

                        case RoundType.L: rndU = RoundType.LB; rndD = RoundType.LT; break;
                        case RoundType.R: rndU = RoundType.RB; rndD = RoundType.RT; break;
                        case RoundType.T: rndU = RoundType.Rect; rndD = RoundType.T; break;
                        case RoundType.B: rndU = RoundType.B; rndD = RoundType.Rect; break;

                        case RoundType.LT: rndU = RoundType.Rect; rndD = RoundType.LT; break;
                        case RoundType.RT: rndU = RoundType.Rect; rndD = RoundType.RT; break;
                        case RoundType.LB: rndU = RoundType.LB; rndD = RoundType.Rect; break;
                        case RoundType.RB: rndU = RoundType.RB; rndD = RoundType.Rect; break;

                        case RoundType.All: rndU = RoundType.B; rndD = RoundType.T; break;
                    }

                    var vrt = this.RectangleToScreen(Util.INT(rtContent));
                    if (dropContainer != null && dropContainer.Top <= vrt.Top) 
                        Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, rndU, Box.ButtonUp_Flat(ShadowGap));
                    else 
                        Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, rndD, Box.ButtonUp_Flat(ShadowGap));
                }
                else Theme.DrawBox(e.Graphics, rtContent, BoxColor, BorderColor, Round, Box.ButtonUp_Flat(ShadowGap));



                #endregion
                #region Item
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                    Theme.DrawTextIcon(e.Graphics, Items[SelectedIndex], Font, ForeColor, rtText);
                #endregion
                #region Icon
                var nisz = Convert.ToInt32(DrawingTool.PixelToPt(rtIco.Height / 2));
                Theme.DrawIcon(e.Graphics, new DvIcon("fa-chevron-down", nisz), ForeColor, rtIco);
                #endregion
                #region Unit Sep
                {
                    var szh = Convert.ToInt32(rtIco.Height / 2);

                    p.Width = 1;
                    p.Color = BoxColor.BrightnessTransmit(Theme.BorderBrightness);
                    e.Graphics.DrawLine(p, rtIco.Left + 0F, (rtContent.Top + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.Left + 0F, (rtContent.Top + (rtContent.Height / 2)) + (szh / 2) + 1);

                    p.Color = Theme.GetInBevelColor(BoxColor);
                    e.Graphics.DrawLine(p, rtIco.Left + 1F, (rtContent.Top + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.Left + 1F, (rtContent.Top + (rtContent.Height / 2)) + (szh / 2) + 1);
                }
                #endregion

            });

            #region Dispose
            p.Dispose();
            br.Dispose();
            #endregion
            base.OnThemeDraw(e, Theme);
        }
        #endregion
        #region OnMouseDown
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();
            if (!bDown)
            {
                bDown = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }
        #endregion
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            Areas((rtContent, rtBox, rtIco, rtText) =>
            {
                if (bDown)
                {
                    bDown = false;
                    if (CollisionTool.Check(rtContent, e.Location) && Items != null && Items.Count > 0) OpenDropDown();
                    Invalidate();
                }
            });

            base.OnMouseUp(e);
        }
        #endregion
        #endregion

        #region Method
        #region Areas
        void Areas(Action<RectangleF, RectangleF, RectangleF, RectangleF> act)
        {
            var rtContent = GetContentBounds();
            var rtIco = Util.FromRect(rtContent.Right - ButtonWidth, rtContent.Top, ButtonWidth, rtContent.Height);
            var rtBox = Util.FromRect(rtContent.Left, rtContent.Top, rtContent.Width - rtIco.Width, rtContent.Height);
            var rtText = Util.FromRect(rtBox.Left, rtBox.Top, rtBox.Width, rtBox.Height);

            act(rtContent, rtBox, rtIco, rtText);
        }
        #endregion
        #endregion

        #region DropDown
        #region Member Variable
        private bool closedWhileInControl;
        private DropDownContainer dropContainer;
        #endregion

        #region Properties
        #region CanDrop
        protected virtual bool CanDrop
        {
            get
            {
                if (dropContainer != null)
                    return false;

                if (dropContainer == null && closedWhileInControl)
                {
                    closedWhileInControl = false;
                    return false;
                }

                return !closedWhileInControl;
            }
        }
        #endregion
        #region DropState
        public DvDropState DropState { get; private set; }
        #endregion
        #endregion

        #region Method
        #region FreezeDropDown
        internal void FreezeDropDown(bool remainVisible)
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = true;
                if (!remainVisible)
                    dropContainer.Visible = false;
            }
        }
        #endregion
        #region UnFreezeDropDown
        internal void UnFreezeDropDown()
        {
            if (dropContainer != null)
            {
                dropContainer.Freeze = false;
                if (!dropContainer.Visible)
                    dropContainer.Visible = true;
            }
        }
        #endregion
        #region OpenDropDown
        private void OpenDropDown()
        {
            this.Move += (o, s) => { if (dropContainer != null) dropContainer.Bounds = GetDropDownBounds(); };

            var vpos = SelectedIndex == -1 ? 0 : SelectedIndex * ItemHeight;
            vpos = (int)MathTool.Constrain(vpos - (ItemHeight * 2), 0, (Items.Count * ItemHeight));

            dropContainer = new DropDownContainer(this);
            dropContainer.Bounds = GetDropDownBounds();
            dropContainer.DropStateChanged += (o, s) => { DropState = s.DropState; };
            dropContainer.FormClosed += (o, s) =>
            {
                if (!dropContainer.IsDisposed) dropContainer.Dispose();
                dropContainer = null;
                closedWhileInControl = (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position));
                DropState = DvDropState.Closed;
                this.Invalidate();
            };
            DropState = DvDropState.Dropping;
            dropContainer.VScrollPosition = vpos;
            dropContainer.Show(this);
            DropState = DvDropState.Dropped;
            this.Invalidate();
        }
        #endregion
        #region GetDropDownBounds
        private Rectangle GetDropDownBounds()
        {
            int n = Items.Count;
            Point pt = this.Parent.PointToScreen(new Point(this.Left, this.Bottom - 1));
            if (MaximumViewCount != -1) n = Items.Count > MaximumViewCount ? MaximumViewCount : Items.Count;
            Size inflatedDropSize = new Size(this.Width, n * ItemHeight + 2);
            Rectangle screenBounds = new Rectangle(pt, inflatedDropSize);
            Rectangle workingArea = Screen.GetWorkingArea(screenBounds);

            if (screenBounds.X < workingArea.X) screenBounds.X = workingArea.X;
            if (screenBounds.Y < workingArea.Y) screenBounds.Y = workingArea.Y;

            if (screenBounds.Right > workingArea.Right && workingArea.Width > screenBounds.Width) screenBounds.X = workingArea.Right - screenBounds.Width;
            if (screenBounds.Bottom > workingArea.Bottom && workingArea.Height > screenBounds.Height) screenBounds.Y = pt.Y - this.Height - screenBounds.Height + 3;
            return screenBounds;
        }
        #endregion
        #region CloseDropDown
        public void CloseDropDown()
        {
            if (dropContainer != null)
            {
                DropState = DvDropState.Closing;
                dropContainer.Freeze = false;
                dropContainer.Close();
            }
        }
        #endregion
        #region GetDropDownContainerDir
        internal int GetDropDownContainerDir()
        {
            int ret = -1;
            if (DropState == DvDropState.Dropping || DropState == DvDropState.Dropped)
            {
                var p1 = this.PointToScreen(new Point(0, 0));
                var p2 = dropContainer.Location;

                ret = p1.Y < p2.Y ? 1 : 2;
            }
            return ret;
        }
        #endregion
        #region SetSelectIndexForNotRaiseEvent
        public void SetSelectIndexForNotRaiseEvent(int index)
        {
            nSelectedIndex = index;
            Invalidate();
        }
        #endregion
        #endregion

        #region Class
        #region DropWindowEventArgs
        internal class DropWindowEventArgs : EventArgs
        {
            internal DvDropState DropState { get; private set; }
            public DropWindowEventArgs(DvDropState DropState)
            {
                this.DropState = DropState;
            }
        }
        #endregion
        #region DropDownContainer
        public class DropDownContainer : DvForm, IMessageFilter
        {
            #region Properties
            internal bool Freeze { get; set; }
            public DvComboBox ComboBox { get; private set; }
            public double VScrollPosition
            {
                get => ListBox.ScrollPosition;
                set
                {
                    if (ListBox.ScrollPosition != value)
                    {
                        ListBox.ScrollPosition = value;
                        ListBox.Invalidate();
                    }
                }
            }
            #endregion

            #region Member Variable
            private DvListBox ListBox = new DvListBox();
            #endregion

            #region Event
            internal event EventHandler<DropWindowEventArgs> DropStateChanged;
            #endregion

            public DropDownContainer(DvComboBox c)
            {
                #region Init
                this.BlankForm = true;
                this.DoubleBuffered = true;
                this.StartPosition = FormStartPosition.Manual;
                this.ShowInTaskbar = false;
                this.ControlBox = false;
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.AutoSize = false;
                this.AutoScroll = false;
                this.MinimumSize = new Size(10, 10);
                this.Padding = new Padding(0, 0, 0, 0);

                this.Theme = c.GetTheme();
                #endregion
                #region Set
                this.ComboBox = c;
                this.Font = c.Font;
                this.BackColor = c.BackColor;
                Application.AddMessageFilter(this);
                #endregion
                #region ListBox
                ListBox.Dock = DockStyle.Fill;
                ListBox.ForeColor = c.ForeColor;
                ListBox.BackColor = c.BackColor;
                ListBox.BoxColor = c.BoxColor;
                ListBox.Round = RoundType.Rect;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.Single;
                //ListBox.Corner = 0;
                ListBox.ItemHeight = c.ItemHeight;
                ListBox.TouchMode = c.TouchMode;
                ListBox.ItemClicked += (o, s) =>
                {
                    if (s.Item != null)
                    {
                        if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                        c.SelectedIndex = ListBox.Items.IndexOf(s.Item);
                        this.Close();
                    }
                };

                if (c.SelectedIndex != -1) ListBox.SelectedItems.Add(c.Items[c.SelectedIndex]);

                this.Controls.Add(ListBox);
                #endregion

                #region Color
                var Theme = c.GetTheme();
                var BoxColor = Theme.ListBackColor;
                var ItemColor = c.ItemColor ?? Theme.ButtonColor;
                var SelectedItemColor = c.SelectedItemColor ?? Theme.PointColor;
                #endregion
                this.BackColor = ListBox.BackColor = c.BackColor;
                this.ForeColor = ListBox.ForeColor = c.ForeColor;
                ListBox.BoxColor = BoxColor;
                ListBox.RowColor = ItemColor;
                ListBox.SelectedColor = SelectedItemColor;
            }

            #region Implements
            #region PreFilterMessage
            public bool PreFilterMessage(ref Message m)
            {
                if (!Freeze && this.Visible && (Form.ActiveForm == null || !Form.ActiveForm.Equals(this)))
                {
                    if (DropStateChanged != null) DropStateChanged.Invoke(this, new DropWindowEventArgs(DvDropState.Closing));
                    this.Close();
                }
                return false;
            }
            #endregion
            #endregion
        }
        #endregion
        #endregion
        #endregion
    }
}
