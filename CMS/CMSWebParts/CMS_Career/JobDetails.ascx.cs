using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using System;
using System.Data;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_Career_JobDetails : JobSearchWebpart
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
        FillForm.Click += FillForm_Click;
        Back.Click += Back_Click;
    }

    protected void FillForm_Click(object sender, EventArgs e)
    {
        Response.Redirect(NextPageUrl);
    }
    protected void Back_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        string message;
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
        jobId = EmergeQueryHelper.GetString(CareerConstants.CAREER_QUERYSTRING_JOBID, string.Empty);
        if (jobId != string.Empty)
        {
            ds = GetJobDetails(jobId);
            SaveJobDetailsInSession();
            ShowJobDetails();
        }
    }
    #endregion

    #region Private Methods
    private void ShowJobDetails()
    {
        if (!EmergeDataHelper.DataSourceIsEmpty(ds))
        {
            JobDetails.TransformationName = string.Format("customtable.Emerge_{0}_CR_Jobs.Preview", EmergeCMSContext.CurrentSiteName); 
            JobDetails.DataSource = GetJobDetails(jobId);
            JobDetails.DataBind();
            lblNoDataFound.Visible = false;
        }
        else
            lblNoDataFound.Visible = true;
    }
    private void SaveJobDetailsInSession()
    {
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_JOBID, jobId);
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_JOB_TITLE, ds.Tables[0].Rows[0][CareerConstants.CAREER_COLUMN_JOB_TITLE].ToString());
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_JOB_LOCATION, ds.Tables[0].Rows[0][CareerConstants.CAREER_COLUMN_JOB_LOCATION].ToString());
    }
    #endregion
}