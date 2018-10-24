using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;

namespace Bluespire.Emerge.Components.GiftShop.DL
{
    public static class TaxCalculatorDAL
    {
        const string keyName = "TaxPercentage";
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Tax Percentage.</returns>
        /// <exception cref="ConfigurationItemMissingException">thrown if Record not found in Configuration Table.</exception>
        /// <exception cref="ExpectedColumnNotFoundException">thrown if Value column not found in Configuration Table.</exception>
        public static double GetTaxPercentage()
        {
            string WhereCondition = GetWhereCondition();

            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME), WhereCondition, string.Empty);
            
            if (ds.Tables[0].Rows.Count == 0)
                throw new ConfigurationItemMissingException(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_CONFIGURATIONITEMMISSINGEXCEPTION_MESSAGE, EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME), keyName));

            if (!ds.Tables[0].Columns.Contains(GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME))
                throw new ExpectedColumnNotFoundException(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_EXPECTEDCOLUMNNOTFOUNDEXCEPTION_MESSAGE, GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME, EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME)));

            return Convert.ToDouble(ds.Tables[0].Rows[0][GiftShopConstants.CONFIGURATION_VALUE_COLUMNNAME].ToString());
        }

        private static string GetWhereCondition()
        {
            string WhereCondition = "[" + GiftShopConstants.CONFIGURATION_KEY_COLUMNNAME + "] = '" + keyName + "'";

            if (CustomTableDataHelper.HasStatusField(EmergeStaticHelper.SetSiteName(GiftShopConstants.CONFIGURATIONTABLE_CODENAME)))
            {
                WhereCondition += Constants.WHERE_CONDITION_OPERATOR_AND + Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
            }
            return WhereCondition;
        }

    }
}
