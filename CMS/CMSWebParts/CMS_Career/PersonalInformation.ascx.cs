using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using CMS.Base.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


public partial class CMSWebParts_CMS_Career_PersonalInformation : PersonalInformationWebpart
{
    #region Webpart Properties
    public string NextPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty);
        }
    }
    public string PrevPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty);
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

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        IDictionary itemDictionary;
        ControlPanel = pnlPersonalInformation;
        BindEventMethods();
        if (!IsPostBack)
        {
            LoadListControls(false);
            itemDictionary = GetUserPresonalInformation();
            PopulateUserInformation();
            FillUserInformation(pnlPersonalInformation, (Dictionary<string, object>)itemDictionary);
            SetWorkPhoneValue();
            GetApplicationDetails();
        }
    }

    private void PopulateUserInformation()
    {
        ApplicantFirstName.Text = CurrentUser.FirstName;
        ApplicantLastName.Text = CurrentUser.LastName;
        ApplicantEmail.Text = CurrentUser.Email;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlPersonalInformation.Visible = false;
            return;
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        if (IsSaved())
        {
            if (Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER] != null)
                Response.Redirect(Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER].ToString());
            Response.Redirect(NextPageUrl);
        }
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (IsSaved())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    #endregion

    #region Private Methods
    private bool IsSaved()
    {
        ApplicantEmergengyContactPhoneWork.Text = string.Format(CareerConstants.CAREER_CONCATENATE_PHONE_EXTENSION, ApplicantEmergengyContactPhoneWorkNum.Text, ApplicantEmergengyContactPhoneWorkExt.Text);
        return SavePresonalInformation();
    }
    private void GetApplicationDetails()
    {
        string jobId = Convert.ToString(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOBID));
        if (!jobId.Equals(string.Empty))
        {
            Position.Text = Convert.ToString(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOB_TITLE));
            ApplicationDate.Text = System.DateTime.Today.ToString(CareerConstants.CAREER_DATETIME_FORMAT);
            Location.Text = Convert.ToString(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOB_LOCATION));
            pnlPostionDetails.Visible = true;
        }
    }
    private void SetWorkPhoneValue()
    {
        ApplicantEmergengyContactPhoneWorkNum.Text = GetPhoneNumber(ApplicantEmergengyContactPhoneWork.Text);
        ApplicantEmergengyContactPhoneWorkExt.Text = GetExtension(ApplicantEmergengyContactPhoneWork.Text);
    }
    private void BindEventMethods()
    {
        Submit.Click += Submit_Click;
        Next.Click += Next_Click;
        Clear.Click += Clear_Click;
        Previous.Click += Previous_Click;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
    }

   
    #endregion
}