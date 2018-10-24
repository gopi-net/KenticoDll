using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Career.WebParts;
using System.Collections.Generic;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using System.Data;
using System.Collections;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
namespace Bluespire.Emerge.Components.Careers.WebParts
{
    public class JobReferencesAndReferralsWebpart : CareerWebPart
    {
        protected bool SaveReferences(int itemId)
        {
            if (AreDatesValid())
            {
                CreateFormParameters();
                string customTableName = string.Format(CareerConstants.CAREER_TABLE_REFERENCES, EmergeCMSContext.CurrentSiteName);
                FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
                return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
            }
            return false;
        }
        protected bool SaveReferrals()
        {
            if (AreDatesValid())
            {
                CreateFormParameters();
                string customTableName = string.Format(CareerConstants.CAREER_TABLE_REFERRALS, EmergeCMSContext.CurrentSiteName);
                int itemId = GetItemIdForUser(customTableName);
                FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
                return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
            }
            return false;
        }

        protected DataSet GetUserReferences()
        {
            string queryName;
            Hashtable parameters;
            queryName = string.Format(CareerConstants.CAREER_QUERY_GET_REFERENCES, EmergeCMSContext.CurrentSiteName);
            parameters = new Hashtable();
            parameters.Add("@UserId", EmergeCurrentUser.UserID);
            return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
        }
        protected bool DeleteReferenceItem(int itemId)
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_REFERENCES, EmergeCMSContext.CurrentSiteName);
            return CustomTableDataHelper.DeleteCustomTableItem(itemId, customTableName);
        }
        protected IDictionary GetReferencesItem(int itemId)
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_REFERENCES, EmergeCMSContext.CurrentSiteName);
            return GetUserData(customTableName, itemId);
        }

        protected Dictionary<string, object> GetUserReferrals()
        {
            return (Dictionary<string, object>)GetUserData(CareerConstants.CAREER_TABLE_REFERRALS);
        }
    }

}
