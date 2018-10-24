using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using System.Collections;
using System.Data;
using Bluespire.Emerge.Web.Controls;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_FormControls_MandatoryTextBox : EmergeBaseFormEngineUserControl
{
    /// <summary>
    /// Gets or sets the value entered into the field, a hexadecimal color code in this case.
    /// </summary>
    public override object Value
    {
        get
        {
            if (!this.Visible)
                return string.Empty;
            return textbox.Text;
        }
        set {
            if (!this.Visible)
                textbox.Text = string.Empty;
            else
                textbox.Text = ValidationHelper.GetString(value, null);
        }
    }

    public override bool IsValid()
    {
        if (!this.Visible)
            return true;

        if (String.IsNullOrEmpty(this.textbox.Text.Trim()))
        {
            ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }
        return true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            textbox.Text = string.Empty;
            //this.Form.Data.SetValue(this.FieldInfo.Name, string.Empty);
            //this.FieldInfo.SetValue(this.FieldInfo.Name, string.Empty);
        }
    }
}