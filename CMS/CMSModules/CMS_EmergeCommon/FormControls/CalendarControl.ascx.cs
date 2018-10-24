using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using System.Data;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;
using CMS.Globalization;
using CMS.Base;
using CMS.MacroEngine;

public partial class CMSFormControls_Basic_CalendarControl : EmergeBaseFormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return timePicker.Enabled;
        }
        set
        {
            timePicker.Enabled = value;
        }
    }


    /// <summary>
    /// If true, macros are allowed 
    /// </summary>
    public bool AllowMacros
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowMacros"), false);
        }
        set
        {
            SetValue("AllowMacros", value);
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            String value = timePicker.DateTimeTextBox.Text;
            if (AllowMacros && MacroProcessor.ContainsMacro(value))
            {
                return value;
            }

            if (timePicker.SelectedDateTime == DateTimeHelper.ZERO_TIME)
            {
                return null;
            }
            else
            {
                return timePicker.SelectedDateTime;
            }
        }
        set
        {
            if (GetValue("timezonetype") != null)
            {
                //timePicker.TimeZone = TimeZoneInfoProvider.GetTimeZoneTypeEnum(ValidationHelper.GetString(GetValue("timezonetype"), ""));

                //timePicker.TimeZone = TimeZoneInfoProvider.EnumStringRepresentationExtensions(ValidationHelper.GetString(GetValue("timezonetype"), ""));
                timePicker.TimeZone = EnumStringRepresentationExtensions.ToEnum<CMS.Globalization.TimeZoneTypeEnum>(ValidationHelper.GetString(GetValue("timezonetype"), ""));
            }
            if (GetValue("timezone") != null)
            {
                timePicker.CustomTimeZone = TimeZoneInfoProvider.GetTimeZoneInfo(ValidationHelper.GetString(GetValue("timezone"), ""));
            }

            string strValue = ValidationHelper.GetString(value, "");
            string strValueLowered = strValue.ToLowerCSafe();

            if (AllowMacros && MacroProcessor.ContainsMacro(strValue))
            {
                timePicker.DateTimeTextBox.Text = strValue;
                return;
            }

            if ((strValueLowered == DateTimeHelper.MACRO_DATE_TODAY.ToLowerCSafe()) || (strValueLowered == DateTimeHelper.MACRO_TIME_NOW.ToLowerCSafe()))
            {
                timePicker.SelectedDateTime = DateTime.Now;
            }
            else
            {
                timePicker.SelectedDateTime = ValidationHelper.GetDateTimeSystem(value, DateTimeHelper.ZERO_TIME);
            }
        }
    }


    /// <summary>
    /// Gets or sets if calendar control enables to edit time.
    /// </summary>
    public bool EditTime
    {
        get
        {
            return timePicker.EditTime;
        }
        set
        {
            timePicker.EditTime = value;
        }
    }

    /// <summary>
    /// Gets or sets the dependent field on which this control depends for validation.
    /// </summary>
    public string DependentFieldName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentFieldName"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("DependentFieldName"), string.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the validation operator to compare value with the dependent field.
    /// </summary>
    public string DependentFieldOperator
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentFieldOperator"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("DependentFieldOperator"), string.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the validation operator to compare value with the dependent field.
    /// </summary>
    public bool AcceptEmptyValue
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AcceptEmptyValue"), false);
        }
        set
        {
            ValidationHelper.GetBoolean(GetValue("AcceptEmptyValue"), false);
        }
    }

    /// <summary>
    /// Gets or sets whether to check validation for the todays date
    /// </summary>
    public bool ValidateForTodaysDate
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ValidateForTodaysDate"), false);
        }
        set
        {
            ValidationHelper.GetBoolean(GetValue("ValidateForTodaysDate"), false);
        }
    }

    /// <summary>
    /// Gets or sets the validation operator to compare value with the todays date.
    /// </summary>
    public string TodaysDateOperator
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TodaysDateOperator"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("TodaysDateOperator"), string.Empty);
        }
    }

    
    public bool CheckForUnique
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CheckForUnique"), false);
        }
        set
        {
            ValidationHelper.GetBoolean(GetValue("CheckForUnique"), false);
        }
    }

    

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup control
        if (FieldInfo != null)
        {
            timePicker.AllowEmptyValue = FieldInfo.AllowEmpty;
        }
        timePicker.DisplayNow = false;//ValidationHelper.GetBoolean(GetValue("displaynow"), true);
        timePicker.EditTime = false; //ValidationHelper.GetBoolean(GetValue("edittime"), EditTime);
        timePicker.SupportFolder = "~/CMSAdminControls/Calendar";
        timePicker.DateTimeTextBox.CssClass = "EditingFormCalendarTextBox";
        timePicker.IsLiveSite = IsLiveSite;

        if (!String.IsNullOrEmpty(CssClass))
        {
            timePicker.CssClass = CssClass;
            CssClass = null;
        }
        if (!String.IsNullOrEmpty(ControlStyle))
        {
            timePicker.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckFieldEmptiness = false;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // Check value
        string strValue = timePicker.DateTimeTextBox.Text.Trim();
        
        if ((FieldInfo != null) && !FieldInfo.AllowEmpty && String.IsNullOrEmpty(strValue))
        {
            // Empty error
            if (ErrorMessage != null)
            {
                if (ErrorMessage != ResHelper.GetString("BasicForm.InvalidInput"))
                {
                    ValidationError = ErrorMessage;
                }
                else
                {
                    ValidationError += ResHelper.GetString("BasicForm.ErrorEmptyValue");
                }
            }
            return false;
        }

        if (AllowMacros && MacroProcessor.ContainsMacro(strValue))
        {
            return true;
        }

        if ((((FieldInfo != null) && !FieldInfo.AllowEmpty) || !String.IsNullOrEmpty(strValue)) && (ValidationHelper.GetDateTime(strValue, DateTimeHelper.ZERO_TIME) == DateTimeHelper.ZERO_TIME))
        {
            DateTime showDate = new DateTime(2005, 1, 31);
            DateTime showDateTime = new DateTime(2005, 1, 31, 11, 59, 59);

            if (timePicker.EditTime)
            {
                // Error invalid DateTime
                ValidationError += ResHelper.GetString("BasicForm.ErrorInvalidDateTime") + " " + showDateTime + ".";
            }
            else
            {
                // Error invalid date
                ValidationError += ResHelper.GetString("BasicForm.ErrorInvalidDate") + " " + showDate.ToString("d") + ".";
            }

            return false;
        }

        if (!timePicker.IsValidRange())
        {
            ValidationError += GetString("general.errorinvaliddatetimerange");
            return false;
        }

        return ValidateDependency();

        //return true;
    }

    private bool ValidateDependency()
    {
        if (!this.Visible)
            return true;

        if (this.AcceptEmptyValue)
        {
            if (this.Value == null)
                return true;
        }
        else
        {
            if (this.Value == null)
            {
                ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
                return false;
            }
        }
        if (!IsValidForTodaysDate())
            return false;

        if (CheckForUnique)
        {
            if (!IsUnique())
            {
                ValidationError = ResHelper.GetString(Constants.STRINGCODE_DATEUNIQUEVALIDATION);
                return false;
            }
        }

        if (!String.IsNullOrEmpty(DependentFieldName))
        {
            object value = this.Form.GetFieldValue(DependentFieldName);
            DateTime dependentDate = DateTime.MinValue;
            if (null != value)
            {
                dependentDate = Convert.ToDateTime(value);
            }
            if (!String.IsNullOrEmpty(DependentFieldOperator))
            {
                switch (DependentFieldOperator.ToLower())
                {
                    case "lessthan":
                        if (Convert.ToDateTime(this.Value).Date > dependentDate.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_DATELESSTHANVALIDATIONMESSAGE), this.FieldInfo.Caption, DependentFieldName);
                            return false;
                        }
                        break;
                    case "greaterthan":
                        if (Convert.ToDateTime(this.Value).Date < dependentDate.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_DATEGREATERTHANVALIDATIONMESSAGE), this.FieldInfo.Caption, DependentFieldName);
                            return false;
                        }
                        break;
                    case "equal":
                        if (Convert.ToDateTime(this.Value).Date != dependentDate.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_DATEEQUALVALIDATIONMESSAGE), this.FieldInfo.Caption, DependentFieldName);
                            return false;
                        }
                        break;
                }
            }
        }
        
        return true;
    }

    private bool IsValidForTodaysDate()
    {
        if (this.ValidateForTodaysDate)
        {
            if (!String.IsNullOrEmpty(TodaysDateOperator))
            {
                switch (TodaysDateOperator.ToLower())
                {
                    case "lessthan":
                        if (Convert.ToDateTime(this.Value).Date > DateTime.Now.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_LESSTHANVALIDATIONMESSAGE), this.FieldInfo.Caption);
                            return false;
                        }
                        break;
                    case "greaterthan":
                        if (Convert.ToDateTime(this.Value).Date < DateTime.Now.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_GREATERTHANVALIDATIONMESSAGE), this.FieldInfo.Caption);
                            return false;
                        }
                        break;
                    case "equal":
                        if (Convert.ToDateTime(this.Value).Date != DateTime.Now.Date)
                        {
                            ValidationError += String.Format(GetString(Constants.STRINGCODE_EQUALVALIDATIONMESSAGE), this.FieldInfo.Caption);
                            return false;
                        }
                        break;
                }
            }
        }
        return true;
    }


    private bool IsUnique()
    {

        if (Form.Data.ToString().ToLower().Contains("cms.customtables.customtableitem"))
        {
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(((CMS.CustomTables.CustomTableItem)Form.Data).ClassName, GetWhereCondition(), string.Empty);
            if (!EmergeDataHelper.DataSourceIsEmpty(ds))
            {
                return false;
            }
        }

        return true;
    }

    private string GetWhereCondition()
    {
        string where = this.FieldInfo.Name;

        where += " = '" + Form.GetFieldValue(this.FieldInfo.Name).ToString().Trim() + "'";
        if (Form.Data.ContainsColumn("ItemID"))
        {
            if (((CMS.CustomTables.CustomTableItem)(Form.Data)).ItemID > 0)
            {
                where += " AND ItemID <> " + ((CMS.CustomTables.CustomTableItem)(Form.Data)).ItemID.ToString();
            }
        }

        return where;
    }

    #endregion
}