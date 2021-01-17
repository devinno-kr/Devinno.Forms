using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace Devinno.Forms.Icons
{
    public class IconFAConverter : TypeConverter//ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value != null && value is IconFA)
            {
                var v = (IconFA)value;
                return v.ToString();
            }
            return "(없음)";
        }
    }

    public class IconFAEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var v = (IconFA)value;

            var wfes = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            if (wfes != null)
            {
                using (var frm = new FormIconFA())
                {
                    var ret = frm.ShowIconFA(wfes, v);
                    if (ret == System.Windows.Forms.DialogResult.OK) v = frm.Result;
                }
            }
            return v;
        }
    }




}









