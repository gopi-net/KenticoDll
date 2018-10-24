using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.FormEngine;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Components.Career;

public partial class CMSFormControls_EmergeFormControls_EmergeDateTimeControl : EmergeDateTimePickerUserControl
{
    bool isRequired = false;
    bool needsValidation = true;

    #region Public Properties
    public override DateTime SelectedDateTime
    {
        get
        {
            return dtPicker.SelectedDateTime == System.DateTime.MinValue ? dtPicker.MinDate : dtPicker.SelectedDateTime;
        }
        set
        {
            if (value == System.DateTime.Today.AddYears(-100))
                dtPicker.SelectedDateTime = new DateTime();
            else
                dtPicker.SelectedDateTime = value;
        }
    }
    public DateTime MinDate
    {
        get
        {
            return dtPicker.MinDate;

        }
        set
        {
            dtPicker.MinDate = value;
        }
    }
    public DateTime MaxDate
    {
        get
        {
            return dtPicker.MaxDate;

        }
        set
        {
            dtPicker.MaxDate = value;
        }
    }
    public bool EditTime
    {
        set
        {
            dtPicker.EditTime = value;
        }
    }
    public bool DisplayNow
    {
        set
        {
            dtPicker.DisplayNow = value;
        }
    }
    public bool IsRequired
    {
        set
        {
            isRequired = value;
        }
        get
        {
            return isRequired;
        }
    }
    public override bool NeedsValidation
    {
        set
        {
            needsValidation = value;
        }
        get
        {
            return needsValidation;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        dtPicker.MinDate = System.DateTime.Today.AddYears(-100);
    }
    #endregion

    #region Methods
    public override bool IsValid()
    {
        bool isValid = true;
        if (needsValidation)
        {
            isValid = IsDate();
            isValid = isValid && IsRequiredDateValid();
            isValid = isValid && IsMinDateValid();
            isValid = isValid && IsMaxDateValid();
        }
        if (isValid)
        {
            ShowValidationMessage(string.Empty);
        }

        return isValid;
    }
    private bool IsDate()
    {
        if (dtPicker.DateTimeTextBox.Text.Trim() != string.Empty)
        {
            string selectedDate = dtPicker.DateTimeTextBox.Text;
            DateTime date;
            if (DateTime.TryParse(selectedDate, out date))
                return true;
            else
            {
                ShowValidationMessage("Invalid Date");
                return false;
            }
        }
        return true;
    }
    private bool IsRequiredDateValid()
    {
        if (dtPicker.SelectedDateTime.ToUniversalTime() == System.DateTime.MinValue.ToUniversalTime() && isRequired)
        {
            ShowValidationMessage("Required");
            return false;
        }
        return true;
    }
    private bool IsMinDateValid()
    {
        if (dtPicker.SelectedDateTime.ToUniversalTime() < dtPicker.MinDate.ToUniversalTime() && dtPicker.SelectedDateTime.ToUniversalTime() != System.DateTime.MinValue.ToUniversalTime())
        {
            ShowValidationMessage("Selected Date should be greater than " + dtPicker.MinDate.ToString(CareerConstants.CAREER_DATETIME_FORMAT));
            return false;
        }
        return true;
    }
    private bool IsMaxDateValid()
    {
        if (dtPicker.SelectedDateTime.ToUniversalTime() > dtPicker.MaxDate.ToUniversalTime())
        {
            ShowValidationMessage("Selected Date should be less than " + dtPicker.MaxDate.ToString(CareerConstants.CAREER_DATETIME_FORMAT));
            return false;
        }
        return true;
    }
    private void ShowValidationMessage(string message)
    {
        ltStatus.Text = message;
    }
    #endregion
}