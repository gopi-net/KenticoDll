using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using CMS.Base.Web.UI;
using System;
using System.Data;

public partial class CMSWebParts_CMS_Career_JobThankYou : JobSearchWebpart
{
    DataSet ds = new DataSet();
    string jobId;
    string incompleteDataPageUrl;
    #region Webpart Properties
    public string LogInPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("LogInPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("LogInPageUrl"), string.Empty);
        }
    }
    public string JobSearchPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("JobSearchPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("JobSearchPageUrl"), string.Empty);
        }
    }
    public string JobApplicationsPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("JobApplicationsPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("JobApplicationsPageUrl"), string.Empty);
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
        Submit.Click += Submit_Click;
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string message;
        jobId = Convert.ToString(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOBID));
        if (!EmergeCMSContext.IsAuthenticated())
            Response.Redirect(string.Format("{0}?{1}={2}", LogInPageUrl, CareerConstants.CAREER_QUERYSTRING_URL_REFERRER, EmergeURLHelper.CurrentURL));

        if (IsDuplicateApplication(Convert.ToInt32(jobId)))
        {
            ShowError(string.Format("You have already applied for this job. <a href='{0}'>Click here</a> to view all you applications", JobApplicationsPageUrl));
            return;
        }

        incompleteDataPageUrl = ApplyForJob(Convert.ToInt32(jobId));
        if (!incompleteDataPageUrl.Equals(string.Empty))
        {
            message = string.Format("Incomplete Application Details, Please Complete <a href='{0}?{1}={2}'>this form</a> and apply again.", incompleteDataPageUrl, CareerConstants.CAREER_QUERYSTRING_URL_REFERRER, EmergeURLHelper.CurrentURL.ToString());
            ShowError(message);
            return;
        }
        message = string.Format("Job Application Successful. <a href='{0}'>Click here</a> to view all you applications", JobApplicationsPageUrl);
        ShowMessage(MessageTypeEnum.Confirmation, message, string.Empty, string.Empty, false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlJobDetails.Visible = false;
            return;
        }
        if (!Page.IsPostBack)
        {
            jobId = Convert.ToString(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOBID));
            Submit.Visible = (jobId != string.Empty);
        }
    }
    #endregion
}