using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Career.WebParts;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using System.Data;
using System.Collections;
namespace Bluespire.Emerge.Components.Careers.WebParts
{
    public class JobEmploymentHistoryWebpart : CareerWebPart
    {
        protected bool SaveJobEmploymentHistory(int itemId)
        {
            if (AreDatesValid())
            {
                CreateFormParameters();
                string customTableName = string.Format(CareerConstants.CAREER_TABLE_EMPLOYMENT_HISTORY, EmergeCMSContext.CurrentSiteName);
                FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
                return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
                
            }
            return false;
        }
        protected bool SaveMilitaryRecord()
        {
            if (AreDatesValid())
            {
                CreateFormParameters();
                string customTableName = string.Format(CareerConstants.CAREER_TABLE_MILITARY_RECORD, EmergeCMSContext.CurrentSiteName);
                int itemId = GetItemIdForUser(customTableName);
                FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
                return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
            }
            return false;
        }
        protected DataSet GetUserEmploymentHistoryInformation()
        {
            return GetUserDataSet(CareerConstants.CAREER_TABLE_EMPLOYMENT_HISTORY);
        }
        protected bool DeleteEmploymentHistoryItem(int itemId)
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_EMPLOYMENT_HISTORY, EmergeCMSContext.CurrentSiteName);
            return CustomTableDataHelper.DeleteCustomTableItem(itemId, customTableName);
        }
        protected IDictionary GetEmploymentHistoryItem(int itemId)
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_EMPLOYMENT_HISTORY, EmergeCMSContext.CurrentSiteName);
            return GetUserData(customTableName, itemId);
        }
        protected IDictionary GetUserMilitaryRecord()
        {
            string customTableName = string.Format(CareerConstants.CAREER_TABLE_MILITARY_RECORD, EmergeCMSContext.CurrentSiteName);
            return GetUserData(customTableName);
        }
    }
}
