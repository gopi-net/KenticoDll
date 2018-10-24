using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.SiteProvider;
using System;
using System.Collections;
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
    public class GridActivateVolunteerUserAction : IGridActivateVolunteerUserAction
    {
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(EmergeValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);

                if (item != null)
                {
                    if (EmergeRelationHelper.IsActionFeasible(item, Constants.GridActions.Activate))
                    {
                        item.SetValue(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME, "true");
                        item.Update();
                    }
                }
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
                    user.Enabled = true;
                    user.Update();
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("BeforeAction:GridActivateVolunteerUserAction", EventCode.EMERGE_UPDATE, ex.ToString());
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
