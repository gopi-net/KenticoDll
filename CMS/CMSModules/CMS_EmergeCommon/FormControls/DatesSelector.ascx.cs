using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using OboutInc.Calendar2;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Common;
using CMS.Helpers;

public partial class CMSModules_CMS_EmergeCommon_FormControls_DatesSelector : EmergeBaseFormEngineUserControl
{

    #region Properties

    /// <summary>
    /// Gets or sets the value of the control.
    /// </summary>
    public override object Value
    {
        get
        {
            string returnValue = string.Empty;
            foreach (DateTime d in DatesSelector.SelectedDates)
            {
                returnValue += d.ToString(Constants.EMERGE_DATEFORMAT) + " | ";
            }

            if (returnValue.Length > 0)
                return returnValue.Remove(returnValue.LastIndexOf(" | "));
            else
                return returnValue;
        }
        set
        {
            string selectedDates = Convert.ToString(value);
            if(!String.IsNullOrEmpty(selectedDates))
            {
                string[] values = selectedDates.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                OboutInc.Calendar2.Calendar.SelectedDatesList dates = new OboutInc.Calendar2.Calendar.SelectedDatesList();
                foreach (string date in values)
                {
                    DateTime dateValue = Convert.ToDateTime(date.Trim());
                    dates.Add(dateValue);
                }
                DatesSelector.SelectedDates = dates;
            }
        }
    }

    /// <summary>
    /// Gets or sets whether this control should return empty values
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

    #endregion

    #region Methods and Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Determines whether the control is valid
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        if (!this.Visible)
            return true;

        if (!this.AcceptEmptyValue)
        {
            if (this.DatesSelector.SelectedDates.Count == 0)
            {
                ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
                return false;
            }
            foreach (DateTime date in this.DatesSelector.SelectedDates)
            {
                if (date.Date < DateTime.Now.Date)
                {
                    ValidationError = GetString(Constants.STRINGCODE_DATESSELECTORMESSAGE);
                    return false;
                }
            }
        }
        return true;
    }

    #endregion
}