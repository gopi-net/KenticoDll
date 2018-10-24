using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.PreRegistration;
using Bluespire.Emerge.Components.PreRegistration.WebParts;
using System.Data;
using System.Collections.Generic;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_PreRegistration_PreRegistrationControl : PreRegistrationStep1Webpart
{
    #region Properties
    public string NextPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NextPageURL"), string.Empty);
        }
        set
        {
            SetValue("NextPageURL", value);
        }
    }
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    public DateTime MinDate
    {
        get
        {
            return System.DateTime.Today.AddYears(-125);
        }
    }
    public DateTime MaxDate
    {
        get
        {
            return System.DateTime.Today;
        }
    }
    #endregion


    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
        BindEventMethods();
        SetOptionalValidations();
        ControlPanel = pnlStep1;
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {
            if (StopProcessing)
            {
                pnlStep1.Visible = false;
                return;
            }
            IDictionary itemDictionary;
            if (!IsPostBack)
            {
                LoadListControls(false);
                if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
                {
                    itemDictionary = (IDictionary)EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO);
                    FillPreRegInformation(pnlStep1, (Dictionary<string, object>)itemDictionary);                                        
                }
            }
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFormFields();        
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>CheckSameAsAboveChecked2();</script>");
            if (AreDatesValid())
            {
                BindFormValues();
                URLHelper.Redirect(this.NextPageURL);
            }
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }     
    #endregion
    #region Methods
    private void SetOptionalValidations()
    {
        SetMinMaxValues();
    }
    private void SetMinMaxValues()
    {
        DateOfBirth.MaxDate = MaxDate;
        DateOfBirth.MinDate = MinDate;
        GIDateOfBirth.MaxDate = MaxDate;
        GIDateOfBirth.MinDate = MinDate;
    }   
    private void BindEventMethods()
    {
        btnNext.Click += btnNext_Click;
        btnClear.Click += btnClear_Click;
        if (NextPageURL.Equals(string.Empty))
            btnNext.Visible = false;
    }
    #endregion
}
