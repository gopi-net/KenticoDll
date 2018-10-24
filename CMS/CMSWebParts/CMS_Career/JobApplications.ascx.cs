using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Careers.WebParts;
using System;
using System.Data;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_Career_JobApplications : JobSearchWebpart
{
    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    #region Webpart Properties
    public string ApplicationPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("ApplicationPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("ApplicationPageUrl"), string.Empty);
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

    #endregion
    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
       
        Search.Click += Search_Click;
        ApplicationForm.Click += ApplicationForm_Click;
        Search.Visible = !JobSearchPageUrl.Equals(string.Empty);
        ApplicationForm.Visible = !ApplicationPageUrl.Equals(string.Empty);
        ControlPanel = pnlJobSearch;
    }

    protected void ApplicationForm_Click(object sender, EventArgs e)
    {
        Response.Redirect(ApplicationPageUrl);
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        Response.Redirect(JobSearchPageUrl);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlJobSearch.Visible = false;
            return;
        }

        DataSet ds;

        ds = GetUserJobAppications();
        repJobs.ZeroRowsText = EmergeResHelper.GetString("Emerge.CR.NoApplicationsFound");
        repJobs.TransformationName = string.Format("customtable.Emerge_{0}_CR_JobApplications.Default", EmergeCMSContext.CurrentSiteName);
        repJobs.DataSource = ds;
        repJobs.DataBind();
    }
    #endregion


}