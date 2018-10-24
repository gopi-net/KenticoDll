using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.GiftShop.Pages
{
    public class GiftShopOrderDataListPage : GiftShopDataListPage
    {

        protected bool MarkDeliveryStatusPending(string className, string itemIdstoMarkPending)
        {
            return ChangeDeliveryStatus(GiftShopConstants.DeliveryStatus.Pending.ToString(), className, itemIdstoMarkPending);
        }

        private static bool ChangeDeliveryStatus(string statusUpdateTo, string className, string itemIds)
        {
            if (string.IsNullOrEmpty(itemIds)) throw new NoItemSelectedException();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add(GiftShopConstants.ORDER_DELIVERYSTATUS_COLUMNNAME, statusUpdateTo);
            string WhereCondition = Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME + " in ( " + itemIds + " )";
            return (CustomTableDataHelper.UpdateCustomTableItems(CustomTableDataHelper.GetCustomTableItemsByCriteria(className, WhereCondition), data));
        }

        protected bool MarkDeliveryStatusCompleted(string className, string itemIdstoMarkCompleted)
        {
            return ChangeDeliveryStatus(GiftShopConstants.DeliveryStatus.Completed.ToString(), className, itemIdstoMarkCompleted);
        }

    }
}
