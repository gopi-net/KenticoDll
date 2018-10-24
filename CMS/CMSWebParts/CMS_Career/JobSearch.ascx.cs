using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using CMS.Base.Web.UI;
using System;
using System.Collections.Generic;
using System.Data;

public partial class CMSWebParts_CMS_Career_JobSearch : JobSearchWebpart
{
    IDictionary<string, object> parameters = new Dictionary<string, object>();
    private const string jobTitle = "JobTitle";
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {


        ControlPanel = pnlJobSearch;
        Submit.Click += Submit_Click;
        Clear.Click += Clear_Click;
        if (EmergeCMSContext.IsLiveMode() && EmergeCMSContext.IsAuthenticated())
        {
            regContent.Visible = false;
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        EmergeSessionHelper.Remove(CareerConstants.CAREER_SESSION_JOB_SEARCH_PARAMETERS);
        Response.Redirect(EmergeURLHelper.CurrentURL);
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        SetParameters();
        SearchJobs();
    }

    private void SearchJobs()
    {
        DataTable dt;
        dt = GetJobs(parameters);
        repJobs.TransformationName = string.Format("customtable.Emerge_{0}_CR_Jobs.Default", EmergeCMSContext.CurrentSiteName);
        repJobs.DataSource = dt;
        repJobs.DataBind();
        
        litNoRecordsFound.Visible = (dt != null && dt.Rows.Count == 0);

        if (lblSearchResult != null)
            lblSearchResult.Visible = true;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlJobSearch.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            LoadListControls(false);
            GetParameters();
            if (hasParameters())
                SearchJobs();
        }
    }

    private bool hasParameters()
    {
        return parameters != null;
    }
    #endregion

    #region Private Methods
    private void SetParameters()
    {
        if (!String.IsNullOrEmpty(JobTitle.Text))
        {
            parameters.Add(jobTitle, JobTitle.Text);
        }
        AddDropDownValueToParameter(Department, "Department");
        AddDropDownValueToParameter(Location, "Location");
        AddDropDownValueToParameter(EmploymentType, "EmploymentType");
        AddDropDownValueToParameter(JobShift, "JobShift");
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_JOB_SEARCH_PARAMETERS, parameters);
    }
    private void GetParameters()
    {
        parameters = (IDictionary<string, object>)EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOB_SEARCH_PARAMETERS);
        SetFormFields();
    }

    private void SetFormFields()
    {
        if (hasParameters())
        {
            JobTitle.Text = parameters.ContainsKey(jobTitle) ? parameters[jobTitle].ToString() : string.Empty;
            SetDropDownValueFromParameter(Department, "Department");
            SetDropDownValueFromParameter(Location, "Location");
            SetDropDownValueFromParameter(EmploymentType, "EmploymentType");
            SetDropDownValueFromParameter(JobShift, "JobShift");
        }
    }

    private void SetDropDownValueFromParameter(LocalizedDropDownList dropDown, string parameterKey)
    {
        if (parameters.ContainsKey(parameterKey))
        {
            dropDown.SelectedValue = parameters[parameterKey].ToString();
        }
    }

    private void AddDropDownValueToParameter(LocalizedDropDownList dropDown, string parameterKey)
    {
        if (dropDown.SelectedValue != "-1")
        {
            parameters.Add(parameterKey, Convert.ToInt32(dropDown.SelectedValue));
        }
    }
    #endregion
}