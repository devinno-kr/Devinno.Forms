using Devinno.Forms.Controls;
using Devinno.Forms.Extensions;
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
    public partial class DvDateTimePickerBox : DvForm
    {
        #region Properties
        private DateTime SelectedValue
        {
            get
            {
                DateTime ret = new DateTime();

                switch (pickerType)
                {
                    case DateTimePickerType.DateTime:
                        {
                            var dt = calendar.SelectedDays.Count > 0 ? calendar.SelectedDays.First() : DateTime.Now.Date;
                            if (inHour.Error == InputError.None && inMin.Error == InputError.None && inSec.Error == InputError.None)
                                ret = new DateTime(dt.Year, dt.Month, dt.Day, inHour.Value.Value, inMin.Value.Value, inSec.Value.Value);

                        }
                        break;
                    case DateTimePickerType.Date:
                        {
                            var dt = calendar.SelectedDays.Count > 0 ? calendar.SelectedDays.First() : DateTime.Now.Date;
                            ret = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                        }
                        break;
                    case DateTimePickerType.Time:
                        {
                            var dt = DateTime.Now.Date;
                            if (inHour.Error == InputError.None && inMin.Error == InputError.None && inSec.Error == InputError.None)
                                ret = new DateTime(dt.Year, dt.Month, dt.Day, inHour.Value.Value, inMin.Value.Value, inSec.Value.Value);
                        }
                        break;
                }

                return ret;
            }
        }
        #endregion

        #region Member Variable
        DvButton btnOK;
        DvButton btnCancel;
        DvCalendar calendar;
        DvValueInputInt inHour, inMin, inSec;

        DateTimePickerType pickerType = DateTimePickerType.DateTime;
        #endregion

        #region Constructor
        public DvDateTimePickerBox()
        {
            InitializeComponent();

            #region new
            calendar = new DvCalendar { Name = nameof(calendar), Dock = DockStyle.Fill };
            inHour = new DvValueInputInt { Name = nameof(inHour), Unit = "시", UnitWidth = 30, Minimum = 0, Maximum = 23, Dock = DockStyle.Fill };
            inMin = new DvValueInputInt { Name = nameof(inMin), Unit = "분", UnitWidth = 30, Minimum = 0, Maximum = 59, Dock = DockStyle.Fill };
            inSec = new DvValueInputInt { Name = nameof(inSec), Unit = "초", UnitWidth = 30, Minimum = 0, Maximum = 59, Dock = DockStyle.Fill };

            btnOK = new DvButton { Name = nameof(btnOK), Text = "확인", Dock = DockStyle.Fill };
            btnCancel = new DvButton { Name = nameof(btnCancel), Text = "취소", Dock = DockStyle.Fill };

            inHour.OriginalTextBox.MaxLength = 2;
            inMin.OriginalTextBox.MaxLength = 2;
            inSec.OriginalTextBox.MaxLength = 2;
            #endregion

            #region Event
            btnOK.ButtonClick += (o, s) =>
            {
                switch (pickerType)
                {
                    case DateTimePickerType.DateTime:
                        if (inHour.Error == InputError.None && inMin.Error == InputError.None && inSec.Error == InputError.None)
                            DialogResult = DialogResult.OK;
                        break;

                    case DateTimePickerType.Date:
                        DialogResult = DialogResult.OK;
                        break;

                    case DateTimePickerType.Time:
                        if (inHour.Error == InputError.None && inMin.Error == InputError.None && inSec.Error == InputError.None)
                            DialogResult = DialogResult.OK;
                        break;
                }
            };
            btnCancel.ButtonClick += (o, s) => DialogResult = DialogResult.Cancel;
            #endregion

            SetExComposited();
        }
        #endregion

        #region Method
        #region show
        DateTime? show(string Title, Action act1)
        {
            Theme = GetCallerFormTheme();

            this.Width = 10 + (256 + 6) + 10 + 20 + 10 + 100 + 10;
            this.Height = TitleHeight + 10 + (256 + 6) + 10;

            this.Title = this.Text = Title;

            act1();

            return ShowDialog() == DialogResult.OK ? SelectedValue : null;
        }
        #endregion
        #region ShowDateTimePicker 
        public DateTime? ShowDateTimePicker(string Title, DateTime? value)
        {
            pickerType = DateTimePickerType.DateTime;

            return show(Title, () =>
            {
                this.Width = 300;
                this.Height = TitleHeight + 10 + 246 + 4 + 36 + 4 + 36 + 10;
                this.TitleIconString = "fa-calendar-check";

                #region Set
                if (value.HasValue)
                {
                    calendar.SelectedDays.Add(value.Value);
                    inHour.Value = Convert.ToByte(value.Value.Hour);
                    inMin.Value = Convert.ToByte(value.Value.Minute);
                    inSec.Value = Convert.ToByte(value.Value.Second);
                }
                else
                {
                    calendar.SelectedDays.Add(DateTime.Now.Date);
                    inHour.Value = 0;
                    inMin.Value = 0;
                    inSec.Value = 0;
                }
                #endregion
                #region Layout
                tpnl.RowStyles.Clear();
                tpnl.Controls.Clear();

                tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 4));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 4));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

                tpnl.Controls.Add(calendar, 0, 0, 5, 1);

                tpnl.Controls.Add(inHour, 0, 2);
                tpnl.Controls.Add(inMin, 2, 2);
                tpnl.Controls.Add(inSec, 4, 2);

                tpnl.Controls.Add(btnOK, 2, 4);
                tpnl.Controls.Add(btnCancel, 4, 4);
                #endregion

            });
        }
        #endregion
        #region ShowDatePicker
        public DateTime? ShowDatePicker(string Title, DateTime? value)
        {
            pickerType = DateTimePickerType.Date;

            return show(Title, () =>
            {
                this.Width = 300;
                this.Height = TitleHeight + 10 + 246 + 4 + 36 + 10;
                this.TitleIconString = "fa-calendar-check";

                #region Set
                if (value.HasValue)
                {
                    calendar.SelectedDays.Add(value.Value);
                }
                else
                {
                    calendar.SelectedDays.Add(DateTime.Now.Date);
                }
                #endregion
                #region Layout
                tpnl.RowStyles.Clear();
                tpnl.Controls.Clear();

                tpnl.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 4));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

                tpnl.Controls.Add(calendar, 0, 0, 5, 1);
                tpnl.Controls.Add(btnOK, 2, 2);
                tpnl.Controls.Add(btnCancel, 4, 2);
                #endregion

            });
        }
        #endregion
        #region ShowTimePicker
        public DateTime? ShowTimePicker(string Title, DateTime? value)
        {
            pickerType = DateTimePickerType.Time;

            return show(Title, () =>
            {
                this.Width = 300;
                this.Height = TitleHeight + 10 + 36 + 4 + 36 + 10;
                this.TitleIconString = "fa-clock";

                #region Set
                if (value.HasValue)
                {
                    inHour.Value = Convert.ToByte(value.Value.Hour);
                    inMin.Value = Convert.ToByte(value.Value.Minute);
                    inSec.Value = Convert.ToByte(value.Value.Second);
                }
                else
                {
                    inHour.Value = 0;
                    inMin.Value = 0;
                    inSec.Value = 0;
                }
                #endregion
                #region Layout
                tpnl.RowStyles.Clear();
                tpnl.Controls.Clear();

                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 4));
                tpnl.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

                tpnl.Controls.Add(inHour, 0, 0);
                tpnl.Controls.Add(inMin, 2, 0);
                tpnl.Controls.Add(inSec, 4, 0);
                tpnl.Controls.Add(btnOK, 2, 2);
                tpnl.Controls.Add(btnCancel, 4, 2);
                #endregion

            });
        }
        #endregion
        #endregion
    }
}
