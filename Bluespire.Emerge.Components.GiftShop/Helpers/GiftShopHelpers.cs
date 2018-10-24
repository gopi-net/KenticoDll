using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;


namespace Bluespire.Emerge.Components.GiftShop.Helpers
{
    public static class GiftShopHelpers
    {

        /// <summary>
        /// method to Get GiftShop configuration settings by key.
        /// </summary>
        /// <param name="Key">Key.</param>
        /// <returns>Value related to Passed key.</returns>
        /// <exception cref="ConfigurationItemMissingException"> Thrown in case of key not found in GIft Shop Configuration Custom Table.</exception>
        /// <exception cref="ExpectedColumnNotFoundException"> Thrown in case of expected column not found in Gift Shop Configuration Custom Table.</exception>
        public static string GetConfigurationValueByKey(string Key)
        {
            string WhereCondition = "[" + GiftShopConstants.CONFIGURATION_KEY_COLUMNNAME + "] = '" + Key + "'";

            string TableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME);

            string StatusWhereCondition = GetWhereConditionIfTableHasStatusField(TableCodeName);

            if (!string.IsNullOrEmpty(StatusWhereCondition))
                WhereCondition += Constants.WHERE_CONDITION_OPERATOR_AND + " " + StatusWhereCondition;

            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME), WhereCondition, string.Empty);


            if (ds.Tables[0].Rows.Count == 0)
                throw new ConfigurationItemMissingException(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_CONFIGURATIONITEMMISSINGEXCEPTION_MESSAGE, EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME), Key));

            if (!ds.Tables[0].Columns.Contains(GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME))
                throw new ExpectedColumnNotFoundException(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_EXPECTEDCOLUMNNOTFOUNDEXCEPTION_MESSAGE, GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME, EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME)));

            return ds.Tables[0].Rows[0][GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME].ToString();

        }

        public static string GetWhereConditionIfTableHasStatusField(string TableCodeName)
        {
            string WhereCondition = string.Empty;
            if (CustomTableDataHelper.HasStatusField(TableCodeName))
            {
                WhereCondition += Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
            }

            return WhereCondition;
        }

        public static string GetEmailTemplateCodeName(GiftShopConstants.NotificationEmail notificationType)
        {
            switch (notificationType)
            {
                case GiftShopConstants.NotificationEmail.ConfirmationEmailToAdmin :
                    return String.Format(GiftShopConstants.ADMIN_NOTIFICATION_EMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                case GiftShopConstants.NotificationEmail.ConfirmationEmailToUser :
                    return String.Format(GiftShopConstants.USER_NOTIFICATION_EMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                case GiftShopConstants.NotificationEmail.NegativeStockReachedDueToConcurrentTransactions:
                    return String.Format(GiftShopConstants.ADMIN_NOTIFICATION_NEGATIVESTOCKREACHED_ALERTEMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                case GiftShopConstants.NotificationEmail.OrderNotSavedinDatabase:
                    return String.Format(GiftShopConstants.ADMIN_NOTIFICATION_ORDERNOTSAVED_ALERTEMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                case GiftShopConstants.NotificationEmail.ProductInformationRequestEmail:
                    return String.Format(GiftShopConstants.ADMIN_NOTIFICATION_PRODUCTINFORMATIONREQUEST_EMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                case GiftShopConstants.NotificationEmail.StockNotReduced:
                    return String.Format(GiftShopConstants.ADMIN_NOTIFICATION_STOCKNOTREDUCED_ALERTEMAILTEMPLATE, EmergeSiteInfoProvider.CurrentSiteName);
                default:
                    throw new NotImplementedException();
            }
            
        }

        public static string GetAdminEmailRecipient()
        {
            return GiftShopHelpers.GetConfigurationValueByKey(GiftShopConstants.GIFT_SHOP_ADMIN_EMAIL_ADDRESS_KEY);
        }
    }
}
