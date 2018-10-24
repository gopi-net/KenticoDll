using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Career.WebParts;
using System.Collections.Generic;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
namespace Bluespire.Emerge.Components.Careers.WebParts
{
    public class PersonalInformationWebpart : CareerWebPart
    {
        protected bool SavePresonalInformation()
        {
            if (AreDatesValid())
            {
                CreateFormParameters();
                string customTableName = string.Format(CareerConstants.CAREER_TABLE_PERSONAL_INFORMATION, EmergeCMSContext.CurrentSiteName);
                int itemId = GetItemIdForUser(customTableName);
                FormParameters.Add(CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
                return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemId, FormParameters);
            }
            return false;
        }
        protected Dictionary<string, object> GetUserPresonalInformation()
        {
            return (Dictionary<string, object>)GetUserData(CareerConstants.CAREER_TABLE_PERSONAL_INFORMATION);
        }
    }
}
