using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using System.Data;
//using CMS.DataEngine;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.GiftShop.BL;
namespace Bluespire.Emerge.Components.GiftShop.DL
{
    public class ProductDAL
    {
        string productTableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.PRODUCTTABLE_CODENAME);
        
        public DataSet GetAllProducts(string OrderBy)
        {
            string WhereCondition = GetWhereConditionForActiveProducts();
            
            return CustomTableDataHelper.GetCustomTableItemsByCondition(productTableCodeName, WhereCondition, OrderBy);

        }

        private string GetWhereConditionForActiveProducts()
        {
            string WhereCondition = string.Empty;
            if (CustomTableDataHelper.HasStatusField(productTableCodeName))
            {
                WhereCondition = Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
            }

          
            return WhereCondition;
        }

        public DataSet GetProductsByCategoryID(int CategoryID)
        {
            string WhereCondition = GetWhereConditionForActiveProducts();
        
            
            if (!String.IsNullOrEmpty(WhereCondition))
                WhereCondition += Constants.WHERE_CONDITION_OPERATOR_AND + GiftShopConstants.PRODUCT_CATEGORY_COLUMNNAME + Constants.WHERE_CONDITION_OPERATOR_EQUAL + CategoryID;
            else
                WhereCondition += GiftShopConstants.PRODUCT_CATEGORY_COLUMNNAME + Constants.WHERE_CONDITION_OPERATOR_EQUAL + CategoryID;

            return CustomTableDataHelper.GetCustomTableItemsByCondition(productTableCodeName, WhereCondition, string.Empty);
        }

        public DataSet GetProductByProductID(int ProductID)
        {
            string WhereCondition = GetWhereConditionForActiveProducts();
            WhereCondition += Constants.WHERE_CONDITION_OPERATOR_AND + " " + Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME + " " + Constants.WHERE_CONDITION_OPERATOR_EQUAL + ProductID;

            return CustomTableDataHelper.GetCustomTableItemsByCondition(productTableCodeName, WhereCondition, string.Empty);
        }

        public void UpdateStock(Product product )
        {
            Dictionary<string, object> stockData = new Dictionary<string, object>();
            stockData.Add(GiftShopConstants.PRODUCT_STOCK_COLUMNNAME, Convert.ToInt32(CustomTableDataHelper.GetFieldValue(product.ProductID, productTableCodeName, GiftShopConstants.PRODUCT_STOCK_COLUMNNAME)) - product.GetProductPurchasedQuantity());
            int productID = product.ProductID;
   
            CustomTableDataHelper.SaveCustomTableItem(productTableCodeName, ref productID, stockData);
        }

        public void SaveProductInformationRequest( Dictionary<string, object> SenderInformation)
        {
            string productInformationRequestTableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.PRODUCTINFORMATIONREQUESTSTABLE_CODENAME);
            int itemID = 0;
            CustomTableDataHelper.SaveCustomTableItem(productInformationRequestTableCodeName, ref itemID, SenderInformation);

        }
    }
}
