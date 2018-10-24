using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Bluespire.Emerge.Web.Controls;
using CMS.Helpers;
using CMS.Base;

public partial class CMSFormControls_EmergeFormControls_USphone : EmergeBaseFormEngineUserControl
{
    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;

            txt1st.Enabled = value;
            txt2nd.Enabled = value;
            txt3rd.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (IsEmpty())
            {
                return "";
            }

            if (!string.IsNullOrEmpty(txtExt.Text) && AllowExtension)
            {
                return "(" + txt1st.Text + ") " + txt2nd.Text + "-" + txt3rd.Text + ResHelper.GetString("Emerge.USPhone.ExtensionNo") + txtExt.Text;
            }
            else
            {
                return "(" + txt1st.Text + ") " + txt2nd.Text + "-" + txt3rd.Text;
            }
        }
        set
        {
            string number = (string)value;
            Clear();

            // Parse numbers from incoming string.
            if ((number != null) && (number != ""))
            {
                try
                {
                    if (number.Contains(ResHelper.GetString("Emerge.USPhone.ExtensionNo")) && AllowExtension)
                    {
                        txt1st.Text = number.Substring(1,number.IndexOfCSafe(")") - 1);
                        txt2nd.Text = number.Substring(number.IndexOfCSafe(")") + 2, number.IndexOfCSafe("-") - number.IndexOfCSafe(")") - 2);
                        txt3rd.Text = number.Substring(number.IndexOfCSafe("-") + 1, number.IndexOfCSafe(ResHelper.GetString("Emerge.USPhone.ExtensionNo")) - number.IndexOfCSafe("-") - 1);
                        txtExt.Text = number.Substring(number.IndexOfCSafe(ResHelper.GetString("Emerge.USPhone.ExtensionNo")) + ResHelper.GetString("Emerge.USPhone.ExtensionNo").Length, number.Length - (number.IndexOfCSafe(ResHelper.GetString("Emerge.USPhone.ExtensionNo")) + ResHelper.GetString("Emerge.USPhone.ExtensionNo").Length));
                    }
                    else
                    {
                        txt1st.Text = number.Substring(1, number.IndexOfCSafe(")") - 1);
                        txt2nd.Text = number.Substring(number.IndexOfCSafe(")") + 2, number.IndexOfCSafe("-") - number.IndexOfCSafe(")") - 2);
                        txt3rd.Text = number.Substring(number.IndexOfCSafe("-") + 1, number.Length - number.IndexOfCSafe("-") - 1);
                    }

                }
                catch
                {
                }
            }
        }
    }


    public bool AllowExtension
    {
        get
        {
           return ValidationHelper.GetBoolean(GetValue("AllowExtension"),false);
        }
        set
        {
            SetValue("AllowExtension", value);
            
        }
    }

    /// <summary>
    /// Clears current value.
    /// </summary>
    public void Clear()
    {
        txt1st.Text = "";
        txt2nd.Text = "";
        txt3rd.Text = "";

        //Extension no
        txtExt.Text = string.Empty;
    }


    /// <summary>
    /// Returns true if the number is empty.
    /// </summary>
    public bool IsEmpty()
    {
        return (DataHelper.IsEmpty(txt1st.Text) && DataHelper.IsEmpty(txt2nd.Text) && DataHelper.IsEmpty(txt3rd.Text));
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        if (!this.Visible)
            return true;
        if (IsEmpty())
        {
            return true;
        }

        // US phone number must be in form: (ddd) ddd-dddd, where 'd' is digit
        Validator val = new Validator();
        string result = val.IsRegularExp(txt1st.Text, @"\d{3}", "error").IsRegularExp(txt2nd.Text, @"\d{3}", "error").IsRegularExp(txt3rd.Text, @"\d{4}", "error").Result;
        if (AllowExtension)
        {
            if (txtExt.Text != string.Empty)
            {
                result = val.IsRegularExp(txt1st.Text, @"\d{3}", "error").IsRegularExp(txt2nd.Text, @"\d{3}", "error").IsRegularExp(txt3rd.Text, @"\d{4}", "error").IsRegularExp(txtExt.Text, @"^[0-9]{3,4}$", "error").Result;
            }
        }
        
        if (result != "")
        {
            ValidationError = GetString("USPhone.ValidationError");
            return false;
        }
        return true;
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (AllowExtension)
        {
            txtExt.Visible = true;
            localizedExtensionLabel.Visible = true;
        }
        // WAI validation
        lbl2nd.Attributes.Add("style", "display:none;");
        lbl3rd.Attributes.Add("style", "display:none;");
    }
}