using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using CMS.Helpers;
using CMS.FormEngine;

public partial class CMSFormControls_EmergeFormControls_EncryptedLabelField : EmergeBaseFormEngineUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FieldText.Visible = !(FieldValue.Visible = (FieldViewMode == ViewMode.View));

        //if (FieldInfo.MaxStringLength > -1)
        //    FieldText.MaxLength = FieldInfo.MaxStringLength;
    }

    /// <summary>
    /// View mode of the control
    /// </summary>
    public enum ViewMode
    {
        AddEdit,
        View
    }

    /// <summary>
    /// Gets or sets the viewmode of the control.
    /// </summary>
    public ViewMode FieldViewMode
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
            if (string.IsNullOrEmpty(FieldText.Text))
                return string.Empty;
            
            return EmergeEncryptionHelper.EncryptData(FieldText.Text);
        }
        set
        {
            value = ValidationHelper.GetString(value, string.Empty);
            string decryptedValue = string.Empty;
            if (value != string.Empty)
                decryptedValue = EmergeEncryptionHelper.DecryptData(value.ToString());

            FieldText.Text = decryptedValue;
            FieldValue.Text = decryptedValue;
        }
    }

    public override bool IsValid()
    {
        FormFieldInfo formFieldInfo = new FormFieldInfo();
        if (!FieldInfo.AllowEmpty && String.IsNullOrEmpty(FieldText.Text))
        {
            ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }

        if (!String.IsNullOrWhiteSpace(FieldInfo.RegularExpression))
        {
            System.Text.RegularExpressions.Regex reg = RegexHelper.GetRegex(FieldInfo.RegularExpression);

            if (!reg.IsMatch(FieldText.Text))
            {
                ValidationError = formFieldInfo.Properties["validationerrormessage"].ToString();
                return false;
            }
            else
            {
               // FieldInfo.RegularExpression = string.Empty;
            }
            if (FieldInfo.MaxStringLength > -1 && FieldInfo.MaxStringLength < FieldText.Text.Length)
            {
                ValidationError = string.Format(ResHelper.GetString("BasicForm.TooLong"), FieldInfo.MaxStringLength, FieldText.Text.Length);
                return false;
            }
            else
            {
               // FieldInfo.MaxStringLength = -1;
            }

            if (FieldInfo.MinStringLength > -1 && FieldInfo.MinStringLength > FieldText.Text.Length)
            {
                ValidationError = string.Format(ResHelper.GetString("BasicForm.TooShort"), FieldInfo.MaxStringLength, FieldText.Text.Length);
                return false;
            }
            else
            {
                //FieldInfo.MinStringLength = -1;
            }
            
        }
        return true;
    }
}