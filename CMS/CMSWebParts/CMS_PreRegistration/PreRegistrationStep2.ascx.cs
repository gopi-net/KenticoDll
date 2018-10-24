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
using Bluespire.Emerge.Components.PreRegistration.WebParts;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.PreRegistration;
using System.Collections.Generic;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_PreRegistration_PreRegistrationStep2 : PreRegistrationStep2Webpart
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
    public string PrevPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PrevPageURL"), string.Empty);
        }
        set
        {
            SetValue("PrevPageURL", value);
        }
    }
    public string SessionTimeOutPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SessionTimeOutPageURL"), string.Empty);
        }
        set
        {
            SetValue("SessionTimeOutPageURL", value);
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
        ControlPanel = pnlStep2;
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {
            if (StopProcessing)
            {
                pnlStep2.Visible = false;
                return;
            }
            IDictionary itemDictionary;
            if (!RequestHelper.IsPostBack())
            {
                LoadListControls(false);                
                if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
                {
                    itemDictionary = (IDictionary)EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO);
                    FillPreRegInformation(pnlStep2, (Dictionary<string, object>)itemDictionary);                                       
                }
                else
                    URLHelper.Redirect(this.SessionTimeOutPageURL);
            }
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
            {
                if (CheckDatesValid())
                {
                    BindFormValues();
                    URLHelper.Redirect(this.NextPageURL);
                }
            }
            else
                URLHelper.Redirect(this.SessionTimeOutPageURL);
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        BindFormValues();
        URLHelper.Redirect(this.PrevPageURL);
    }     
    #endregion

    #region Methods
    private void SetOptionalValidations()
    {
        SetMinMaxValues();
    }
    private void SetMinMaxValues()
    {
        PIDateOfBirth.MaxDate = MaxDate;
        PIDateOfBirth.MinDate = MinDate;
        SIDateOfBirth.MaxDate = MaxDate;
        SIDateOfBirth.MinDate = MinDate;
    }    
    private bool CheckDatesValid()
    {
        bool isValid = true;
        if (DoYouHaveSecInsPolicy.SelectedValue == "Yes")
            isValid = AreDatesValid();
        return isValid;
    }    
    private void BindEventMethods()
    {
        btnNext.Click += btnNext_Click;
        btnClear.Click += btnClear_Click;
        btnBack.Click += btnBack_Click;
        if (NextPageURL.Equals(string.Empty))
            btnNext.Visible = false;
        if (PrevPageURL.Equals(string.Empty))
            btnBack.Visible = false;
    }
    #endregion
}

