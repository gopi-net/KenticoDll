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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using System.ComponentModel;
using CMS.Base.Web.UI;


public partial class CMSWebParts_CMS_PreRegistration_PreRegistrationStep4 : PreRegistrationStep4Webpart
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
    public string CancelPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CancelPageURL"), string.Empty);
        }
        set
        {
            SetValue("CancelPageURL", value);
        }
    }
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
        BindEventMethods();
        ControlPanel = pnlStep4;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (StopProcessing)
            {
                pnlStep4.Visible = false;
                return;
            }
            IDictionary itemDictionary;
            if (!RequestHelper.IsPostBack())
            {
                LoadListControls(false);
                if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
                {
                    itemDictionary = (IDictionary)EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO);
                    ReviewPreRegistrationInfo(itemDictionary);

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            IDictionary itemDictionary;
            if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
            {
                itemDictionary = (IDictionary)EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO);

                if (SavePreRegistrationInfo(itemDictionary))
                {
                    SendAutoresponderEmail(itemDictionary[PreRegistrationConstants.FIELD_PREREGINFO_EMAIL].ToString());
                    SendAdminEmailNotification();
                    EmergeSessionHelper.SetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO, null);
                    URLHelper.Redirect(this.NextPageURL);
                }
                else
                {
                    lblErrorMsg.Text = PreRegistrationConstants.PREREGINFO_MESSAGE_FAILURE;
                    lblErrorMsg.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>if (document.getElementById('" + lblErrorMsg.ClientID + "') != null){ document.getElementById('" + lblErrorMsg.ClientID + "').scrollIntoView(true);}</script>");
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EmergeSessionHelper.SetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO, null);
        URLHelper.Redirect(this.CancelPageURL);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(this.PrevPageURL);
    }
    #endregion
    #region Methods
    private void BindEventMethods()
    {
        btnSubmit.Click += btnSubmit_Click;
        btnCancel.Click += btnCancel_Click;
        btnBack.Click += btnBack_Click;
        if (PrevPageURL.Equals(string.Empty))
            btnBack.Visible = false;
        if (NextPageURL.Equals(string.Empty))
            btnSubmit.Visible = false;
        if (CancelPageURL.Equals(string.Empty))
            btnCancel.Visible = false;
    }
    private bool SavePreRegistrationInfo(IDictionary itemDictionary)
    {
        IDictionary<string, object> FormParameterData = FormatPreRegistrationData((IDictionary<string, object>)itemDictionary);
        IDictionary<string, object> tableData = new Dictionary<string, object>();
        foreach (KeyValuePair<string, object> dict in FormParameterData)
        {
            if (dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_SSN || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_GISSN || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_PIPOLICYNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_PIGROUPNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_PISOCIALSECURITYNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_SIPOLICYNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_SIGROUPNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_SISOCIALSECURITYNUMBER || dict.Key == PreRegistrationConstants.FIELD_PREREGINFO_BPISSN)
            {
                if (FormParameterData[dict.Key].ToString() != string.Empty)            
                    tableData[dict.Key] = EmergeEncryptionHelper.EncryptData(FormParameterData[dict.Key].ToString());
                else
                    tableData[dict.Key] = FormParameterData[dict.Key];
            }
            else
                tableData[dict.Key] = FormParameterData[dict.Key];
        }
        return SaveInformation(tableData);
    }
    private void ReviewPreRegistrationInfo(IDictionary itemDictionary)
    {
        PreRegistrationInfo.TransformationName = string.Format(PreRegistrationConstants.TRANSFORMATION_PREVIEW, EmergeCMSContext.CurrentSiteName);
        PreRegistrationInfo.DataSource = CopyToDataTable(itemDictionary);
        PreRegistrationInfo.DataBind();
    }

    #endregion
}