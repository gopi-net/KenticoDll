using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.Career
{
    public static class CareerConstants
    {
        #region "Career"
        public const string CAREER_DASHBOARDPAGE = "CareerDashboardPage";
        public const string CAREER_DATAEDITITEMPAGE = "CareerDataEditItemPage";
        public const string CAREER_DATASELECTFIELDSPAGE = "CareerDataSelectFieldsPage";
        public const string CAREER_DATALISTPAGE = "CareerDataListPage";
        public const string CAREER_DATAVIEWITEMPAGE = "CareerDataViewItemPage";
        public const string CAREER_LISTPAGE = "CareerListPage";

        public const string CAREER_TABLE_JOBS = "customtable.Emerge_{0}_CR_Jobs";
        public const string CAREER_TABLE_PERSONAL_INFORMATION = "customtable.Emerge_{0}_CR_JobApplicantPersonalInfo";
        public const string CAREER_TABLE_JOB_INTEREST = "customtable.Emerge_{0}_CR_JobApplicantInterest";
        public const string CAREER_TABLE_EDUCATION_TRAINING = "customtable.Emerge_{0}_CR_JobApplicantEducation";
        public const string CAREER_TABLE_PROFESSIONAL_LICENSURE = "customtable.Emerge_{0}_CR_ApplicantProfessionalInfo";
        public const string CAREER_TABLE_EMPLOYMENT_HISTORY = "customtable.Emerge_{0}_CR_ApplicantEmploymentHistory";
        public const string CAREER_TABLE_MILITARY_RECORD = "customtable.Emerge_{0}_CR_MilitaryRecord";
        public const string CAREER_TABLE_REFERRALS = "customtable.Emerge_{0}_CR_Referrals";
        public const string CAREER_TABLE_REFERENCES = "customtable.Emerge_{0}_CR_References";
        public const string CAREER_TABLE_JOB_AFFIDAVIT_AUTHORIZATION = "customtable.Emerge_{0}_CR_AffidavitAndAuthorization";
        public const string CAREER_TABLE_AFFIRMATIVE_ACTION = "customtable.Emerge_{0}_CR_AffirmativeActionRecord";
        public const string CAREER_TABLE_COMPULSORY_DATA = "customtable.Emerge_{0}_CR_Workflow";
        public const string CAREER_TABLE_JOB_APPLICATION = "customtable.Emerge_{0}_CR_JobApplications";
        public const string CAREER_TRANSFORMATION_SUFFIX_JOB_APPLICATION = ".ApplicationPreview";

        public const string CAREER_ACTIVE_DATA_CONDITION = "IsActive = 1";
        public const string CAREER_COLUMN_ITEMID = "ItemID";
        public const string CAREER_COLUMN_USERID = "UserId";
        public const string CAREER_COLUMN_JOBID = "JobId";
        public const string CAREER_COLUMN_JOB_TITLE = "JobTitle";
        public const string CAREER_COLUMN_JOB_LOCATION = "LocationName";
        public const string CAREER_COLUMN_COMPULSORY_DATA_TABLE_NAME = "TableName";
        public const string CAREER_COLUMN_COMPULSORY_DATA_PAGE_URL = "PageUrl";
        public const string CAREER_COLUMN_APPLICATION_DATE = "ApplicationDate";

        public const string CAREER_SESSION_JOBID = "CAREER_SESSION_JOBID";
        public const string CAREER_SESSION_JOB_TITLE = "CAREER_SESSION_JOB_TITLE";
        public const string CAREER_SESSION_JOB_LOCATION = "CAREER_SESSION_JOB_LOCATION";
        public const string CAREER_SESSION_EMPLOYMENT_HISTORY_ID = "CAREER_SESSION_EMPLOYMENT_HISTORY_ID";
        public const string CAREER_SESSION_JOB_REFERENCE_ID = "CAREER_SESSION_JOB_REFERENCE_ID";
        public const string CAREER_SESSION_JOB_SEARCH_PARAMETERS = "CAREER_SESSION_JOB_SEARCH_PARAMETERS";

        public const string CAREER_QUERYSTRING_URL_REFERRER = "ReturnUrl";
        public const string CAREER_QUERYSTRING_JOBID = "JobId";

        public const string CAREER_QUERY_GET_PROFILE = "customtable.Emerge_{0}_CR_JobApplications.GetProfile";
        public const string CAREER_QUERY_GET_REFERENCES = "customtable.Emerge_{0}_CR_References.GetReferences";
        public const string CAREER_QUERY_GET_JOBS = "customtable.Emerge_{0}_CR_Jobs.GetJobs";
        public const string CAREER_QUERY_GET_USER_JOB_APPLICATIONS = "customtable.Emerge_{0}_CR_JobApplications.GetUserApplications";
        
        public const string CAREER_ITEM_COMMAND_EDIT = "edit";
        public const string CAREER_ITEM_COMMAND_DELETE = "delete";

        public const string STRINGCODE_CAREERHOME = "Emerge.CR.Dashboard";
        public const string PAGEURL_CAREER_DASHBOARD = "~/CMSModules/CMS_Career/Dashboard/Dashboard.aspx";


        public const string CAREER_MESSAGE_SUCCESS = "Data Saved Successfully";
        public const string CAREER_MESSAGE_MINIMUM_EMPLOYMENT_HISTORY_FAILED = "Minimum {0} previous employment detail(s) required";
        public const string CAREER_MESSAGE_MINIMUM_REFERENCES_FAILED = "Minimum {0} reference(s) required";
        public const string CAREER_MESSAGE_FAILURE = "Action Failed";
        public const string CAREER_MESSAGE_INVALID_INPUT = "Invalid Input";

        public const string CAREER_CONCATENATE_PHONE_EXTENSION = "{0}  Ext: {1}";
        public const string CAREER_SPLITTER_PHONE_EXTENSION = "Ext:";
        public const string CAREER_DATETIME_FORMAT = "MMM-dd-yyyy";
        public const string CAREER_BUTTON_TEXT_ADD = "Add";
        public const string CAREER_BUTTON_TEXT_UPDATE = "Update";
        #endregion
    }
}
