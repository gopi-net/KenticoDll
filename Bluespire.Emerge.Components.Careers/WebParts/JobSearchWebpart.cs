using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Career.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using Bluespire.Emerge.CommonService;
using System.Data;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using System.Collections;
using Bluespire.Emerge.CommonService.Caching;
using Bluespire.Emerge.Common;
namespace Bluespire.Emerge.Components.Careers.WebParts
{
    public class JobSearchWebpart : CareerWebPart
    {
        public string incompleteDataPageURL = string.Empty;
        private IEnumerable<DataRow> GetAllJobs()
        {
            IEnumerable<DataRow> jobs;
            DataSet ds;
            string queryName = string.Format(CareerConstants.CAREER_QUERY_GET_JOBS, EmergeCMSContext.CurrentSiteName);
            ICacheable objCaching = new EmergeCustomQuery();
            objCaching.Key = queryName;
            ds = EmergeCacheHelper.GetData(objCaching);
            if (EmergeDataHelper.DataSourceIsEmpty(ds))
                return null;
            jobs = ds.Tables[0].AsEnumerable();
            return jobs;
        }
        protected DataTable GetJobs(IDictionary<string, object> parameters)
        {
            IEnumerable<DataRow> jobs = GetAllJobs();
            int value;
            if (jobs == null || jobs.Count() == 0)
                return new DataTable();
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                if (Int32.TryParse(parameter.Value.ToString(), out value))
                    jobs = jobs.Where(x => (IsFound(x[parameter.Key].ToString(), parameter.Value.ToString())));
                else
                    jobs = jobs.Where(x => (x[parameter.Key].ToString().ToLower().Contains(parameter.Value.ToString().ToLower())));
            }
            if (jobs == null || jobs.Count() == 0)
                return new DataTable();
            return jobs.CopyToDataTable();
        }

        private static bool IsFound(string dbValue, string selectedValue)
        {
            if (dbValue.Contains('|'))
            {
                dbValue = "|" + dbValue + "|";
                return dbValue.ToLower().Contains("|" + selectedValue.ToLower() + "|");
            }
            else
            {
                return dbValue == selectedValue;
            }

        }
        protected DataSet GetJobDetails(string JobId)
        {
            DataSet ds = new DataSet();
            IEnumerable<DataRow> jobs = GetAllJobs();
            jobs = jobs.Where(x => (x[CareerConstants.CAREER_COLUMN_ITEMID].ToString() == JobId));
            if (jobs.Count() == 0)
            {
                return new DataSet();
            }
            ds.Tables.Add(jobs.CopyToDataTable());
            return ds;
        }
        public bool IsDataAvailableInCompulsoryTables()
        {
            bool isDataAvailable = true;
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_COMPULSORY_DATA, EmergeCMSContext.CurrentSiteName);
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, CareerConstants.CAREER_ACTIVE_DATA_CONDITION, CareerConstants.CAREER_COLUMN_ITEMID);
            if (!EmergeDataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    isDataAvailable = isDataAvailable && IsUserDataAvailableInTable(Convert.ToString(dr[CareerConstants.CAREER_COLUMN_COMPULSORY_DATA_TABLE_NAME]));
                    if (!isDataAvailable)
                    {
                        incompleteDataPageURL = Convert.ToString(dr[CareerConstants.CAREER_COLUMN_COMPULSORY_DATA_PAGE_URL]);
                        break;
                    }
                }
            }
            return isDataAvailable;
        }
        private bool IsUserDataAvailableInTable(string customTableName)
        {
            return !EmergeDataHelper.DataSourceIsEmpty(GetUserDataFromTable(customTableName));
        }

        private DataSet GetUserDataFromTable(string customTableName)
        {
            customTableName = string.Format(customTableName, EmergeCMSContext.CurrentSiteName);
            string wherecondition = string.Format("{0} = {1}", CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            return CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, wherecondition, CareerConstants.CAREER_COLUMN_ITEMID);
        }
        private bool AddJobApplication(int jobId)
        {
            int itemId = 0;
            FormParameters = new Dictionary<string, object>();
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_JOB_APPLICATION, EmergeCMSContext.CurrentSiteName);
            FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            FormParameters.Add(CareerConstants.CAREER_COLUMN_JOBID, jobId);
            FormParameters.Add(CareerConstants.CAREER_COLUMN_APPLICATION_DATE, DateTime.Now);
            return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
        }
        public string ApplyForJob(int jobId)
        {
            if (IsDataAvailableInCompulsoryTables() && AddJobApplication(jobId))
                return incompleteDataPageURL;
            return incompleteDataPageURL;
        }

        public bool IsDuplicateApplication(int jobId)
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_JOB_APPLICATION, EmergeCMSContext.CurrentSiteName);
            string wherecondition = string.Format("{0} = {1} and {2}", CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID, CareerConstants.CAREER_ACTIVE_DATA_CONDITION);
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, wherecondition, CareerConstants.CAREER_COLUMN_ITEMID);
            if (!EmergeDataHelper.DataSourceIsEmpty(ds))
            {
                IEnumerable<DataRow> userJobApplications = ds.Tables[0].AsEnumerable();
                userJobApplications = userJobApplications.Where(x => (x[CareerConstants.CAREER_COLUMN_JOBID].ToString() == jobId.ToString()));
                return userJobApplications.Count() > 0;
            }
            return false;
        }
        protected DataSet GetUserJobAppications()
        {
            Hashtable parameters = new Hashtable();
            string queryName = string.Format(CareerConstants.CAREER_QUERY_GET_USER_JOB_APPLICATIONS, EmergeCMSContext.CurrentSiteName);
            parameters.Add("@" + CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
        }

    }
}
