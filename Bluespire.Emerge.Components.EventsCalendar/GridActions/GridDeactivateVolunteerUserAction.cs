using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Logging;
using CMS.CustomTables;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;

namespace Bluespire.Emerge.Components.EventsCalendar.GridActions
{
    public class GridDeactivateVolunteerUserAction : IGridDeactivateVolunteerUserAction 
    {
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(EmergeValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);


                if (item != null)
                {
                    if (EmergeRelationHelper.IsActionFeasible(item, Constants.GridActions.Deactivate))
                    {
                        item.SetValue(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME, "false");
                        item.Update();

                        EmergeURLHelper.Redirect(EmergeURLHelper.Url.ToString());
                    }
                }

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
                    user.Enabled = false;
                    user.Update();
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("BeforeAction:GridDeactivateVolunteerUserAction", EventCode.EMERGE_UPDATE, ex.ToString());
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
