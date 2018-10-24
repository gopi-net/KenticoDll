using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Common;
using CMS.Helpers;
using CMS.FormEngine.Web.UI;

public partial class CMSModules_CMS_EmergeCommon_FormControls_TimeSelector : EmergeBaseFormEngineUserControl
{
    #region Constants
    private const string TwelveHour = "12Hour";
    private const string TwentyFourHour = "24Hour";
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the value of the control.
    /// </summary>
    public override object Value
    {
        get
        {
            string returnValue = string.Empty;
            
            if (HourFormat == TwelveHour)
                returnValue = HoursDropdown.SelectedValue + ":" + MinutesDropdown.SelectedValue + " " + PeriodDropdown.SelectedValue;
            else if (HourFormat == TwentyFourHour)
                returnValue = HoursDropdown.SelectedValue + ":" + MinutesDropdown.SelectedValue;

            return returnValue;
        }
        set
        {
            // These methods are called because Page load method is called after this set has been executed.
            FillMinutesDropdown();
            FillHoursDropdown();
            FillPeriodDropdown();

            string controlValue = Convert.ToString(value);
            if (!String.IsNullOrEmpty(controlValue))
            {
                HoursDropdown.SelectedValue = controlValue.Trim().Substring(0, 2);
                MinutesDropdown.SelectedValue = controlValue.Trim().Substring(3, 2);
                if (HourFormat == TwelveHour)
                    PeriodDropdown.SelectedValue = controlValue.Trim().Substring(6, 2);
            }
        }
    }

    /// <summary>
    /// Gets or sets the Hour Format for the control.
    /// </summary>
    public string HourFormat
    {
        get
        {
            return ValidationHelper.GetString(GetValue("HourFormat"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("HourFormat"), string.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the Dependent field name
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
    /// Gets or sets the Parent Dependent field name
    /// </summary>
    public string ParentDependentFieldName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ParentDependentFieldName"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("ParentDependentFieldName"), string.Empty);
        }
    }

    /// <summary>
    /// Gets or sets the parent dependent field comaparison operator
    /// </summary>
    public string ParentDependentFieldOperator
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ParentDependentFieldOperator"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("ParentDependentFieldOperator"), string.Empty);
        }
    }

    #endregion

    #region Methods and Events

    protected void Page_Load(object sender, EventArgs e)
    {
        FillHoursDropdown();
        FillMinutesDropdown();
        FillPeriodDropdown();
    }

    private void FillHoursDropdown()
    {
        if (HoursDropdown.Items.Count == 0)
        {
            int startCounter = 1;
            int maxCounter = 12;
            int conditionCounter = 10;

            if (HourFormat == TwentyFourHour)
            {
                startCounter = 0;
                maxCounter = 23;
                conditionCounter = 9;
            }

            for (int i = startCounter; i <= maxCounter; i++)
            {
                if (i < conditionCounter)
                    HoursDropdown.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                else
                    HoursDropdown.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
    }

    private void FillMinutesDropdown()
    {
        if (MinutesDropdown.Items.Count == 0)
        {
            MinutesDropdown.Items.Add(new ListItem("00", "00"));
            MinutesDropdown.Items.Add(new ListItem("15", "15"));
            MinutesDropdown.Items.Add(new ListItem("30", "30"));
            MinutesDropdown.Items.Add(new ListItem("45", "45"));
        }
    }

    private void FillPeriodDropdown()
    {
        if (PeriodDropdown.Items.Count == 0)
        {
            PeriodDropdown.Items.Add(new ListItem("AM", "AM"));
            PeriodDropdown.Items.Add(new ListItem("PM", "PM"));
        }
    }

    /// <summary>
    /// Determines whether the value of the control is valid and passes validation.
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        if (!this.Visible) 
            return true;
        
        if (this.Value == null)
        {
            ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }
        
        DateTime currentValue = Convert.ToDateTime(this.Value);

        bool value = true;
        if (!String.IsNullOrEmpty(DependentFieldName))
        {
            value = ValidateDependentFields(currentValue, DependentFieldName, DependentFieldOperator, this.Form);
        }

        if (value && !String.IsNullOrEmpty(ParentDependentFieldName))
        {
            BasicForm form = getParentFormControl(this.Form);
            if (form != null)
            {
                return ValidateDependentFields(currentValue, ParentDependentFieldName, ParentDependentFieldOperator, form);
            }
        }

        return value;
    }

    private BasicForm getParentFormControl(Control control)
    {
        if (control.Parent is BasicForm)
            return ((BasicForm)(control.Parent));
        return getParentFormControl(control.Parent);
    }

    private bool ValidateDependentFields(DateTime currentValue, string dependentFieldName, string comparisonOperator, BasicForm form)
    {
        object value = form.GetFieldValue(dependentFieldName);
        DateTime dependentValue = DateTime.MinValue;

        if (null != value)
            dependentValue = Convert.ToDateTime(value);

        if (!String.IsNullOrEmpty(comparisonOperator))
        {
            string fieldCaption = this.FieldInfo.Caption;
            if(this.FieldInfo.Caption.IndexOf("<") > 0)
                fieldCaption = this.FieldInfo.Caption.Remove(this.FieldInfo.Caption.IndexOf("<"));

            string dependentFieldCaption = form.FieldLabels[dependentFieldName].Text;
            if (form.FieldLabels[dependentFieldName].Text.IndexOf("<") > 0)
                dependentFieldCaption = form.FieldLabels[dependentFieldName].Text.Remove(form.FieldLabels[dependentFieldName].Text.IndexOf("<"));

            switch (comparisonOperator.ToLower())
            {
                case "lessthan":
                    if (currentValue > dependentValue)
                    {
                        ValidationError += String.Format(GetString(Constants.STRINGCODE_DATELESSTHANVALIDATIONMESSAGE), fieldCaption, dependentFieldCaption);
                        return false;
                    }
                    break;
                case "greaterthan":
                    if (currentValue < dependentValue)
                    {
                        ValidationError += String.Format(GetString(Constants.STRINGCODE_DATEGREATERTHANVALIDATIONMESSAGE), fieldCaption, dependentFieldCaption);
                        return false;
                    }
                    break;
                case "equal":
                    if (dependentValue != currentValue)
                    {
                        ValidationError += String.Format(GetString(Constants.STRINGCODE_DATEEQUALVALIDATIONMESSAGE), fieldCaption, dependentFieldCaption);
                        return false;
                    }
                    break;
            }
        }
        return true;
    }

    #endregion
}