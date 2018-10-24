using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;
using CMS.Base.Web.UI.ActionsConfig;

namespace Bluespire.Emerge.Components.CheerCard.Pages
{
    public class CheerCardRequestsDataPage : CheerCardDataListPage
    {
          
        protected bool MarkDeliveryStatusPending(string className, string itemIdstoMarkPending)
        {
            return ChangeDeliveryStatus(CheerCardConstants.DeliveryStatus.Pending.ToString(), className, itemIdstoMarkPending);
        }

        private static bool ChangeDeliveryStatus(string statusUpdateTo, string className, string itemIds)
        {
            if (string.IsNullOrEmpty(itemIds)) throw new NoItemSelectedException();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_DELIVERYSTATUS, statusUpdateTo);
            string WhereCondition = Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME + " in ( " + itemIds + " )";
            return (CustomTableDataHelper.UpdateCustomTableItems(CustomTableDataHelper.GetCustomTableItemsByCriteria(className, WhereCondition), data));
        }

        protected bool MarkDeliveryStatusDelivered(string className, string itemIdstoMarkCompleted)
        {
            return ChangeDeliveryStatus(CheerCardConstants.DeliveryStatus.Delivered.ToString(), className, itemIdstoMarkCompleted);
        }
        protected override void setHeaderActions()
        {
            HeaderActions.AddAction(new HeaderAction
            {
                Text = GetString("customtable.data.selectdisplayedfields"),
                OnClientClick = "SelectFields();",
            });
        }
    }
}
