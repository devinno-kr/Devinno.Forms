using Devinno.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Dialogs
{
    public partial class DvDateTimePickerDialog : DvForm
    {
        #region Member Variable
        int Year, Month, Day, Hour, Min, Sec;
        #endregion

        public DvDateTimePickerDialog()
        {
            InitializeComponent();
            #region Control Event
            btnOK.ButtonClick += (o, s) => DialogResult= DialogResult.OK;
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;

            calendar.SelectedDaysChanged += (o, s) => { if (calendar.SelectedDays.Count > 0) { var v = calendar.SelectedDays.First(); Year = v.Year; Month = v.Month; Day = v.Day; Set(); } };
            inHour.SelectedIndexChanged += (o, s) => { if (inHour.SelectedIndex >= 0) Hour = inHour.SelectedIndex; };
            inMin.SelectedIndexChanged += (o, s) => { if (inMin.SelectedIndex >= 0) Min = inMin.SelectedIndex; };
            inSec.SelectedIndexChanged += (o, s) => { if (inSec.SelectedIndex >= 0) Sec = inSec.SelectedIndex; };
            #endregion
            #region Input
            inHour.ItemHeight = inMin.ItemHeight = inSec.ItemHeight = 45;
            inHour.MaximumViewCount = inMin.MaximumViewCount = inSec.MaximumViewCount = 7;

            for (int i = 0; i < 24; i++) inHour.Items.Add(new ComboBoxItem(i.ToString()));
            for (int i = 0; i < 60; i++)
            {
                inMin.Items.Add(new ComboBoxItem(i.ToString()));
                inSec.Items.Add(new ComboBoxItem(i.ToString()));
            }
            #endregion

            Fixed = true;

        }

        #region Method
        #region ShowDateTimePicker
        public DateTime? ShowDateTimePicker(string Title, DateTime? datetime = null)
        {
            DateTime? ret = null;
            this.Title = this.Text = Title;
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            foreach (var c in tbl.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(370 * f), Convert.ToInt32(420 * f));

            lblYear.TitleWidth = lblMonth.TitleWidth = lblDay.TitleWidth = inHour.TitleWidth = inMin.TitleWidth = inSec.TitleWidth = Convert.ToInt32(30 * f);
            inHour.ButtonWidth = inMin.ButtonWidth = inSec.ButtonWidth = Convert.ToInt32(f * 30);
            inHour.ItemHeight = inMin.ItemHeight = inSec.ItemHeight = inHour.Height;
            #endregion

            #region UI
            tbl.Controls.Clear();
            tbl.ColumnStyles.Clear();
            tbl.RowStyles.Clear();
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            tbl.Controls.Add(this.lblDay, 2, 1);
            tbl.Controls.Add(this.calendar, 0, 0);
            tbl.Controls.Add(this.btnCancel, 2, 3);
            tbl.Controls.Add(this.btnOK, 1, 3);
            tbl.Controls.Add(this.lblMonth, 1, 1);
            tbl.Controls.Add(this.lblYear, 0, 1);
            tbl.Controls.Add(this.inSec, 2, 2);
            tbl.Controls.Add(this.inHour, 0, 2);
            tbl.Controls.Add(this.inMin, 1, 2);
            #endregion
            #region Set
            var v = datetime.HasValue ? datetime.Value : DateTime.Now;
            Year = v.Year;
            Month = v.Month;
            Day = v.Day;
            Hour = v.Hour;
            Min = v.Minute;
            Sec = v.Second;
            Set();
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = new DateTime(Year, Month, Day, Hour, Min, Sec);
            }

            return ret;
        }
        #endregion
        #region ShowDatePicker
        public DateTime? ShowDatePicker(string Title, DateTime? datetime = null)
        {
            DateTime? ret = null;
            this.Title = this.Text = Title;
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            foreach (var c in tbl.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(370 * f), Convert.ToInt32(360 * f));

            lblYear.TitleWidth = lblMonth.TitleWidth = lblDay.TitleWidth = inHour.TitleWidth = inMin.TitleWidth = inSec.TitleWidth = Convert.ToInt32(30 * f);
            inHour.ButtonWidth = inMin.ButtonWidth = inSec.ButtonWidth = Convert.ToInt32(f * 30);
            inHour.ItemHeight = inMin.ItemHeight = inSec.ItemHeight = inHour.Height;
            #endregion

            #region UI
            tbl.Controls.Clear();
            tbl.ColumnStyles.Clear();
            tbl.RowStyles.Clear();
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            tbl.Controls.Add(this.calendar, 0, 0);

            tbl.Controls.Add(this.lblDay, 2, 1);
            tbl.Controls.Add(this.lblMonth, 1, 1);
            tbl.Controls.Add(this.lblYear, 0, 1);
            
            tbl.Controls.Add(this.btnCancel, 2, 2);
            tbl.Controls.Add(this.btnOK, 1, 2);
            #endregion
            #region Set
            var v = datetime.HasValue ? datetime.Value : DateTime.Now;
            Year = v.Year;
            Month = v.Month;
            Day = v.Day;
            Hour = 0;
            Min = 0;
            Sec = 0;
            Set();
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                ret = new DateTime(Year, Month, Day, 0, 0, 0);
            }

            return ret;
        }
        #endregion
        #region ShowTimePicker
        public DateTime? ShowTimePicker(string Title, DateTime? datetime = null)
        {
            DateTime? ret = null;
            this.Title = this.Text = Title;
            #region DPI Size 
            var f = DpiRatio;
            var m3 = Convert.ToInt32(3 * f);
            var m7 = Convert.ToInt32(7 * f);
            var m10 = Convert.ToInt32(10 * f);

            foreach (var c in tbl.Controls.Cast<Control>()) c.Margin = new Padding(m3);
            pnl.Padding = new Padding(m3, m10, m3, m3);
            this.Padding = new Padding(m7, Convert.ToInt32(f * 40), m7, m7);
            this.Size = new Size(Convert.ToInt32(370 * f), Convert.ToInt32(132 * f));
          
            lblYear.TitleWidth = lblMonth.TitleWidth = lblDay.TitleWidth = inHour.TitleWidth = inMin.TitleWidth = inSec.TitleWidth = Convert.ToInt32(33.333333 * f);
            inHour.ButtonWidth = inMin.ButtonWidth = inSec.ButtonWidth = Convert.ToInt32(f * 30);
            inHour.ItemHeight = inMin.ItemHeight = inSec.ItemHeight = inHour.Height;
            #endregion

            #region UI
            tbl.Controls.Clear();
            tbl.ColumnStyles.Clear();
            tbl.RowStyles.Clear();
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tbl.Controls.Add(this.inSec, 2, 0);
            tbl.Controls.Add(this.inMin, 1, 0);
            tbl.Controls.Add(this.inHour, 0, 0);

            tbl.Controls.Add(this.btnCancel, 2, 1);
            tbl.Controls.Add(this.btnOK, 1, 1);
            #endregion
            #region Set
            var v = datetime.HasValue ? datetime.Value : DateTime.Now;
            Year = 0;
            Month = 0;
            Day = 0;
            Hour = v.Hour;
            Min = v.Minute;
            Sec = v.Second;
            Set();
            #endregion

            if (this.ShowDialog() == DialogResult.OK)
            {
                var vv = DateTime.Now;
                ret = new DateTime(vv.Year, vv.Month, vv.Day, Hour, Min, Sec);
            }

            return ret;
        }
        #endregion

        #region Set
        void Set()
        {
            if (Year > 0 && Month > 0 && Day > 0)
            {
                calendar.SetCurrentDate(Year, Month);
                calendar.SelectedDays.Clear();
                calendar.SelectedDays.Add(new DateTime(Year, Month, Day));
                calendar.Invalidate();
            }

            lblYear.Value = Year.ToString();
            lblMonth.Value = Month.ToString();
            lblDay.Value = Day.ToString();

            inHour.SelectedIndex = Hour;
            inMin.SelectedIndex = Min;
            inSec.SelectedIndex = Sec;
        }
        #endregion
        #endregion
    }
}
