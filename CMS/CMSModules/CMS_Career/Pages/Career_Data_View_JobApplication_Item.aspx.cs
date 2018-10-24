using System;
using Bluespire.Emerge.Components.Career;
using CMS.SiteProvider;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using System.Collections.Generic;
using System.Data;
using CMS.DataEngine;
using CMS.CustomTables;
using CMS.DocumentEngine.Web.UI;

public partial class CMSModules_Career_Pages_Career_Data_View_JobApplication_Item : CareerDataViewItemPage
{
    int userId, jobId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();
            CustomTableItem item = GetCustomTableItem();
            userId = EmergeValidationHelper.GetInteger(item[CareerConstants.CAREER_COLUMN_USERID], 0);
            jobId = EmergeValidationHelper.GetInteger(item[CareerConstants.CAREER_COLUMN_JOBID], 0);
            BindAllItems();
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }

    }

    private void BindAllItems()
    {
        BindPersonalInfoData();
        BindJobInterest();
        BindEducationTraining();
        BindEmploymentHistory();
        BindReferences();
        BindAffidavit();
    }
    private void BindPersonalInfoData()
    {
        BindData(CareerConstants.CAREER_TABLE_PERSONAL_INFORMATION, personalIformationView);
    }
    private void BindJobInterest()
    {
        BindData(CareerConstants.CAREER_TABLE_JOB_INTEREST, jobInterestView);
    }
    private void BindEducationTraining()
    {
        BindData(CareerConstants.CAREER_TABLE_EDUCATION_TRAINING, educationTrainingView);
        BindData(CareerConstants.CAREER_TABLE_PROFESSIONAL_LICENSURE, professionalLicenseView);
    }
    private void BindEmploymentHistory()
    {
        BindData(CareerConstants.CAREER_TABLE_EMPLOYMENT_HISTORY, employmentHistoryView);
        BindData(CareerConstants.CAREER_TABLE_MILITARY_RECORD, militaryRecordView);
    }
    private void BindReferences()
    {
        BindData(CareerConstants.CAREER_TABLE_REFERENCES, referencesView);
        BindData(CareerConstants.CAREER_TABLE_REFERRALS, referralView);
    }
    private void BindAffidavit()
    {
        BindData(CareerConstants.CAREER_TABLE_JOB_AFFIDAVIT_AUTHORIZATION, affidavitView);
    }

    private void BindData(string className, CMSRepeater itemView)
    {
        string queryName;
        QueryDataParameters parameters;
        className = string.Format(className, EmergeCMSContext.CurrentSiteName);
        queryName = string.Format("{0}{1}", className, ".selectWithJoin");
        parameters = new QueryDataParameters();
        parameters.Add("@" + CareerConstants.CAREER_COLUMN_USERID, userId);
        itemView.DataSource = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
        itemView.TransformationName = string.Format("{0}{1}", className, CareerConstants.CAREER_TRANSFORMATION_SUFFIX_JOB_APPLICATION);
        itemView.DataBind();
    }
}