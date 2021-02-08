using Devinno.Extensions;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
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
        private Color cBoxColor = DvTheme.DefaultTheme.Color3;
        public Color BoxColor
        {
            get { return cBoxColor; }
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
        #region ItemColor
        private Color cItemColor = DvTheme.DefaultTheme.Color3;
        public Color ItemColor
        {
            get { return cItemColor; }
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
        #region SelectItemColor
        private Color cSelectedItemColor = DvTheme.DefaultTheme.PointColor;
        public Color SelectedItemColor
        {
            get { return cSelectedItemColor; }
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
        #region ButtonWidth
        int nButtonWidth = 60;
        public int ButtonWidth
        {
            get { return nButtonWidth; }
            set { if (nButtonWidth != value) { nButtonWidth = value; Invalidate(); } }
        }
        #endregion

        #region MaximumViewCount
        int nMaximumViewCount = 10;
        public int MaximumViewCount
        {
            get { return nMaximumViewCount; }
            set { nMaximumViewCount = value; }
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
        #region Items
        public List<ComboBoxItem> Items { get; } = new List<ComboBoxItem>();
        #endregion
        #region ItemPadding
        private Padding padItem = new Padding(0, 0, 0, 0);
        public Padding ItemPadding
        {
            get => padItem;
            set
            {
                if (padItem != value)
                {
                    padItem = value;
                    Invalidate();
                }
            }
        }
        #endregion
        #region ContentAlignment
        private DvContentAlignment eContentAlignment = DvContentAlignment.MiddleCenter;
        public DvContentAlignment ContentAlignment
        {
            get { return eContentAlignment; }
            set { if (eContentAlignment != value) { eContentAlignment = value; Invalidate(); } }
        }
        #endregion
        #region SelectedIndex
        private int nSelectIndex = -1;
        public int SelectedIndex
        {
            get { return nSelectIndex; }
            set
            {
                if (nSelectIndex != value)
                {
                    int old = nSelectIndex;

                    nSelectIndex = value;
                    if (Items == null || Items.Count == 0) nSelectIndex = -1;
                    else if (nSelectIndex < 0 && nSelectIndex >= Items.Count) nSelectIndex = -1;

                    if (SelectedIndexChanged != null && old != nSelectIndex)
                    {
                        SelectedIndexChanged.Invoke(this, null);
                    }
                    Invalidate();
                }
            }
        }
        #endregion
        #endregion

        #region Event
        public event EventHandler SelectedIndexChanged;
        #endregion

        #region Member Variable
        private bool bDown = false;
        #endregion

        #region Constructor
        public DvComboBox()
        {
            #region SetStyle : Selectable
            SetStyle(ControlStyles.Selectable, true);
            UpdateStyles();

            TabStop = true;
            #endregion

            Size = new Size(150, 30);
        }
        #endregion

        #region Override
        #region LoadAreas
        protected override void LoadAreas(Graphics g)
        {
            base.LoadAreas(g);

            var rtContent = Areas["rtContent"];
            var rtIco = new Rectangle(rtContent.Right - ButtonWidth, rtContent.Y, ButtonWidth, rtContent.Height);
            var rtBox = new Rectangle(rtContent.X, rtContent.Y, rtContent.Width - rtIco.Width, rtContent.Height);
            var rtText = new Rectangle(rtBox.X + ItemPadding.Left, rtBox.Y + ItemPadding.Top, rtBox.Width - (ItemPadding.Left + ItemPadding.Right), rtBox.Height - (ItemPadding.Top + ItemPadding.Bottom));

            SetArea("rtIco", rtIco);
            SetArea("rtText", rtText);
        }
        #endregion
        #region OnThemeDraw
        protected override void OnThemeDraw(PaintEventArgs e, DvTheme Theme)
        {
            #region Color
            var BoxColor = UseThemeColor ? Theme.Color3 : this.BoxColor;
            var ItemColor = UseThemeColor ? Theme.Color3 : this.ItemColor;
            var SelectedItemColor = UseThemeColor ? Theme.PointColor : this.SelectedItemColor;
            #endregion
            #region Set
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            #endregion
            #region Bounds
            var rtContent = Areas["rtContent"];
            var rtText = Areas["rtText"];
            var rtIco = Areas["rtIco"];
            #endregion
            #region Init
            var p = new Pen(Color.Black, 2);
            var br = new SolidBrush(Color.Black);
            #endregion
            #region Draw
            #region Item
            if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
            {
                var vrt = this.RectangleToScreen(rtContent);
                if (dropContainer != null && dropContainer.Top <= vrt.Top) Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.B, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_BEVEL);
                else Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.T, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_BEVEL);
            }
            else Theme.DrawBox(e.Graphics, BoxColor, BackColor, rtContent, RoundType.ALL, BoxDrawOption.BORDER | BoxDrawOption.IN_BEVEL | BoxDrawOption.OUT_SHADOW);

            if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
            {
                var v = Items[nSelectIndex];
                Theme.DrawTextShadow(e.Graphics,v.Icon, v.Text, Font, ForeColor, BoxColor, rtText, ContentAlignment);
            }
            #endregion
            #region Icon
            var nisz = rtIco.Height / 4;
            if (DropState == DvDropState.Dropped || DropState == DvDropState.Dropping)
            {
                Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-up") { IconSize = nisz, Gap = 0}, "", Font, ForeColor, BoxColor, rtIco);
            }
            else
            {
                Theme.DrawTextShadow(e.Graphics, new DvIcon("fa-chevron-down") { IconSize = nisz, Gap = 0 }, "", Font, ForeColor, BoxColor, rtIco);
            }
            #endregion
            #region Seperate
            var szh = Convert.ToInt32(rtIco.Height / 2);

            p.Width = 1;

            p.Color = BoxColor.BrightnessTransmit(Theme.OutBevelBright);
            e.Graphics.DrawLine(p, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);

            p.Color = BackColor.BrightnessTransmit(Theme.BorderBright);
            e.Graphics.DrawLine(p, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) - (szh / 2) + 1, rtIco.X - 1, (rtContent.Y + (rtContent.Height / 2)) + (szh / 2) + 1);
            #endregion
            #endregion
            #region Dispose
            br.Dispose();
            p.Dispose();
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
            if (bDown)
            {
                bDown = false;
                if (CollisionTool.Check(Areas["rtContent"], e.Location) && Items != null && Items.Count > 0) OpenDropDown();
                Invalidate();
            }
            base.OnMouseUp(e);
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
            Point pt = this.Parent.PointToScreen(new Point(this.Left, this.Bottom - 2));
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
            nSelectIndex = index;
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
            public int VScrollPosition
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
                ListBox.RectMode = true;
                ListBox.Items.AddRange(c.Items);
                ListBox.SelectionMode = ItemSelectionMode.SINGLE;
                //ListBox.Corner = 0;
                ListBox.RowHeight = c.ItemHeight;
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
                var BoxColor = c.UseThemeColor ? Theme.Color2 : c.BoxColor;
                var ItemColor = c.UseThemeColor ? Theme.Color3 : c.ItemColor;
                var SelectedItemColor = c.UseThemeColor ? Theme.PointColor : c.SelectedItemColor;
                #endregion
                this.BackColor = ListBox.BackColor = c.BackColor;
                this.ForeColor = ListBox.ForeColor = c.ForeColor;
                ListBox.UseThemeColor = false;
                ListBox.BoxColor = BoxColor;
                ListBox.ItemColor = ItemColor;
                ListBox.SelectedItemColor = SelectedItemColor;
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

    #region class : ComboBoxItem
    public class ComboBoxItem : ListBoxItem
    {
        public ComboBoxItem(string Text) : base(Text) { }
        public ComboBoxItem(string Text, Bitmap img) : base(Text, img) { }
        public ComboBoxItem(string Text, string iconString, float size) : base(Text, iconString, size) { }
    }
    #endregion
    #region enum : DvDropState
    public enum DvDockSide { Left, Right }
    public enum DvDropState { Closed, Closing, Dropping, Dropped }
    #endregion
  
}
