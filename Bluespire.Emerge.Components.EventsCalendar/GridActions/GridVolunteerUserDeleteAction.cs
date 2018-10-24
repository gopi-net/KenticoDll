using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.CustomTables;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;

namespace Bluespire.Emerge.Components.EventsCalendar.GridActions
{
    public class GridVolunteerUserDeleteAction : IGridVolunteerUserDeleteAction
    {

        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                int itemID = EmergeValidationHelper.GetInteger(actionArgument, 0);
                CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(itemID, CustomTableClassName);
                
                item.Delete();

                if (AfterActionRedirectToUrl == null || AfterActionRedirectToUrl.Equals(string.Empty))
                    EmergeURLHelper.Redirect(EmergeURLHelper.Url.ToString());
                else
                    EmergeURLHelper.Redirect(AfterActionRedirectToUrl);
            }
            return true;
        }

        public object BeforeAction(params object[] parameters)
        {
            try
            {
                int itemID = EmergeValidationHelper.GetInteger(parameters[0], 0);

                string customTableName = String.Format(EventsConstants.CUSTOMTABLE_EVENT_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteName);
                CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(itemID, customTableName);
                int userID = EmergeValidationHelper.GetInteger(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERID), 0);
                if (userID > 0)
                {
                    UserInfo user = UserInfoProvider.GetUserInfo(userID);
                    user.Delete();
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("BeforeAction:GridVolunteerUserDeleteAction", EventCode.EMERGE_DELETE, ex.ToString());
                return false;
            }
            return true;
        }

        public object AfterAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
