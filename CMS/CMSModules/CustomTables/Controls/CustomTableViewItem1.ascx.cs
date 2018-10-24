using System;
using System.Text;

using CMS.Base;
using CMS.CustomTables;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.MacroEngine;
using CMS.UIControls;
using Bluespire.Emerge.Common;
public partial class CMSModules_CustomTables_Controls_CustomTableViewItem : CMSUserControl
{
    #region "Properties"

    public CustomTableItem CustomTableItem
    {
        get;
        set;
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (CustomTableItem != null)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableItem.CustomTableClassName);
            if (dci != null)
            {
                // Get class form definition
                FormInfo fi = FormHelper.GetFormInfo(dci.ClassName, false);

                string fieldCaption;
                FormFieldInfo ffi;
                IDataContainer item = CustomTableItem;

                StringBuilder sb = new StringBuilder();
                sb.Append("<table class=\"table table-hover\">");
                // Table header
                sb.AppendFormat("<thead><tr class=\"unigrid-head\"><th>{0}</th><th class=\"main-column-100\">{1}</th></tr></thead><tbody>", GetString("customtable.data.nametitle"), GetString("customtable.data.namevalue"));

                // Get macro resolver
                MacroResolver resolver = MacroResolverStorage.GetRegisteredResolver(FormHelper.FORM_PREFIX + dci.ClassName);
                int i = 0;
                // Go through the columns
                foreach (string columnName in item.ColumnNames)
                {
                    // Get field caption
                    ffi = fi.GetFormField(columnName);
                    if (ffi == null)
                    {
                        fieldCaption = columnName;
                    }
                    else
                    {
                        string caption = ffi.GetPropertyValue(FormFieldPropertyEnum.FieldCaption, resolver);
                        if (string.IsNullOrEmpty(caption))
                        {
                            fieldCaption = columnName;
                        }
                        else
                        {
                            fieldCaption = ResHelper.LocalizeString(caption);
                        }
                    }
                    string className = ((i % 2) == 0) ? "EvenRow" : "OddRow";
                    string rowContent = "<tr class=\"" + className + "\"><td style=\"font-weight:bold;white-space: nowrap;\">{0}</td><td width=\"100%\">{1}</td></tr>";
                    if (null != ffi.Settings["controlname"] && ffi.Settings["controlname"].ToString() == Constants.CONTROL_ENCRYPTEDFIELD.ToLower())
                        sb.Append(String.Format(rowContent, fieldCaption, HTMLHelper.HTMLEncode(ValidationHelper.GetString(EmergeEncryptionHelper.DecryptData(item.GetValue(columnName).ToString()).ToString(), ""))));
                    else
                        sb.AppendFormat("<tr><td><strong>{0}</strong></td><td class=\"wrap-normal\">{1}</td></tr>", fieldCaption, HTMLHelper.HTMLEncode(ValidationHelper.GetString(item.GetValue(columnName), String.Empty)));
                    ++i;
                }
                sb.Append("</tbody></table>");

                ltlContent.Text = sb.ToString();
            }
            else
            {
                ltlContent.Text = GetString("editedobject.notexists");
            }
        }
        else
        {
            ltlContent.Text = GetString("editedobject.notexists");
        }
    }
}