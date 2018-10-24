using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Web.Controls;
using System.Data;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.CustomTables;

public partial class CMSFormControls_EmergeFormControls_UniqueTextField : EmergeBaseFormEngineUserControl
{

    /// <summary>
    /// Maximum text length
    /// </summary>
    public int MaxLength
    {
        get
        {
            return EmergeValidationHelper.GetInteger(GetValue("size"), 0);
        }
        set
        {
            SetValue("size", value);
            FieldText.MaxLength = value;
        }
    }

    /// <summary>
    /// get or set default text for drop down(first selected value)
    /// </summary>
    public string GroupedUniqueColumns
    {

        get
        {
            return EmergeValidationHelper.GetString(GetValue("GroupedUniqueColumns"), string.Empty);
        }
        set
        {
            SetValue("GroupedUniqueColumns", value);
        }
    }

    public override string ErrorMessage
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the value for the entire control.
    /// </summary>
    public override object Value
    {
        get
        {
            return FieldText.Text.Trim();
        }
        set
        {
            FieldText.Text = EmergeValidationHelper.GetString(value, string.Empty);
        }
    }


    public override bool IsValid()
    {
        FormFieldInfo formFieldInfo = new FormFieldInfo();
        if (!FieldInfo.AllowEmpty && String.IsNullOrEmpty(FieldText.Text))
        {
            ValidationError = EmergeResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }

        if (!String.IsNullOrWhiteSpace(FieldInfo.RegularExpression))
        {
            System.Text.RegularExpressions.Regex reg = EmergeRegexHelper.GetRegex(FieldInfo.RegularExpression);
            if (!reg.IsMatch(FieldText.Text))
            {
                ValidationError = formFieldInfo.Properties["validationerrormessage"].ToString();
                return false;
            }
        }

        if (FieldInfo.MaxStringLength > -1 && FieldInfo.MaxStringLength < FieldText.Text.Length)
        {
            ValidationError = string.Format(EmergeResHelper.GetString("BasicForm.TooLong"), FieldInfo.MaxStringLength, FieldText.Text.Length);
            return false;
        }

        if (FieldInfo.MinStringLength > -1 && FieldInfo.MinStringLength > FieldText.Text.Length)
        {
            ValidationError = string.Format(EmergeResHelper.GetString("BasicForm.TooShort"), FieldInfo.MaxStringLength, FieldText.Text.Length);
            return false;
        }
        
        if (!IsUnique())
        {
            ErrorMessage = EmergeResHelper.GetString("Emerge.UniqueFormControl.Duplicate");
            formFieldInfo.Properties["validationerrormessage"] = EmergeResHelper.GetString("Emerge.UniqueFormControl.Duplicate");
            ValidationError = EmergeResHelper.GetString("Emerge.UniqueFormControl.Duplicate");
            return false;
        }

        return true;
    }

    private bool IsUnique()
    {
        CustomTableItem item = (CustomTableItem)Form.Data;
        if (!string.IsNullOrEmpty(item.ClassName))
        {
            if (CustomTableDataHelper.GetCustomTableClassInfo(item.ClassName).ClassIsCustomTable)
            {
                DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(item.ClassName, GetWhereCondition(), string.Empty);
                if (!EmergeDataHelper.DataSourceIsEmpty(ds))
                    return false;
            }
        }
        return true;
    }

    private string GetWhereCondition()
    {
        string where = string.Empty;
        const string CLOUMN_NAME_ITEM_ID = "ItemId";
        CustomTableItem item = (CustomTableItem)Form.Data;
        if (!string.IsNullOrEmpty(GroupedUniqueColumns.Trim()))
        {
            where = GroupedUniqueColumns.Replace(Constants.DELIMITER_IN_LIST_VALUES, "]+[");
            where = "[" + where + "]+";
        }
        where = where + "[" + this.FieldInfo.Name + "]";
        where += " = '" + GetValueToSearch(where.Split('+').ToList().Where(x => !string.IsNullOrEmpty(x)).ToList()) + "'";

        if (Convert.ToInt32(Form.GetFieldValue(CLOUMN_NAME_ITEM_ID)) > 0)
            where += " AND " + CLOUMN_NAME_ITEM_ID + " <> " + Form.GetFieldValue(CLOUMN_NAME_ITEM_ID).ToString();

        return where;
    }


    private string GetValueToSearch(List<string> columnsLst)
    {
        string value = string.Empty;
        foreach (string column in columnsLst)
        {
            if (Form.Data.ContainsColumn(column.Replace("[", "").Replace("]", "")))
            {
                if (Form.GetFieldValue(column.Replace("[", "").Replace("]", "")) != null)
                    value += Form.GetFieldValue(column.Replace("[", "").Replace("]", "")).ToString().Trim();
            }
        }
        return value.Replace("'", "''");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetClientSideMaxLength();
    }

    private void SetClientSideMaxLength()
    {

        if (FieldInfo != null)
        {
            switch (FieldInfo.DataType)
            {
                case (FieldDataType.Text):
                    FieldText.MaxLength = FieldInfo.Size;
                    break;

                case (FieldDataType.Integer):
                case (FieldDataType.LongInteger):
                    if (string.IsNullOrEmpty(FieldInfo.MaxValue) || string.IsNullOrEmpty(FieldInfo.MinValue))
                    {
                        // One of the limit value is not set => set maxint/maxlong length
                        FieldText.MaxLength = (FieldInfo.DataType == FieldDataType.Integer) ? EmergeValidationHelper.GetMaxIntLength() : EmergeValidationHelper.GetMaxLongIntLength();
                    }
                    else
                    {
                        // Set maxlength to the bigger one
                        FieldText.MaxLength = Math.Max(FieldInfo.MaxValue.Length, FieldInfo.MinValue.Length);
                    }
                    break;
            }
        }

        if (FieldInfo.Size != 0)
        {
            FieldText.MaxLength = FieldInfo.Size;
        }
    }
}