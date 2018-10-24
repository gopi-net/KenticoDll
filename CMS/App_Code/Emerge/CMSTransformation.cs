using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Bluespire.Emerge.Web;
using System.Data;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using System.Collections;
using Bluespire.Emerge.Components.PreRegistration;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using CMS.PortalEngine.Web.UI;
using CMS.DocumentEngine.Web.UI;
/// <summary>
/// Summary description for CMSTransformation
/// </summary>
/// 
namespace CMS.DocumentEngine.Web.UI
{
    public partial class CMSTransformation 
    {
        public CMSTransformation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public CMSAbstractWebPart FindWebpart(Control o)
        {
            if (o is CMSAbstractWebPart)
            {
                return o as CMSAbstractWebPart;
            }
            else
            {
                if (o.Parent != null)
                {
                    return FindWebpart(o.Parent);
                }
                else
                {
                    return null;
                }
            }
        }
        public string GetWebPartproperty(Control control, string propertyName)
        {
            CMSAbstractWebPart currentWebpart = FindWebpart(control);

            if (currentWebpart != null)
            {
                return EmergeValidationHelper.GetString(currentWebpart.GetValue(propertyName), "");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns URL for current search result.
        /// </summary>
        /// <param name="absolute">Indicates whether generated url should be absolute</param>
        public string EmergeSearchResultUrl(bool absolute)
        {
            CMSAbstractTransformation objCMSAbstractTransformation = new CMSAbstractTransformation();
            return EmergeTransformationHelper.EmergeSearchResultUrl(EmergeValidationHelper.GetString(objCMSAbstractTransformation.Eval("id"), String.Empty), EmergeValidationHelper.GetString(objCMSAbstractTransformation.Eval("type"), String.Empty), absolute);
        }

        public string GetValueByPropertyKeyForProducts(string key, int ProductID, int CategoryID)
        {
            if (null == EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY)) return string.Empty;
            return ((List<Product>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY)).Find(x => x.ProductID == ProductID).GetPropertyValueByName(key);
        }
        public string GetValueByPropertyKeyForCartProducts(string key, int ProductID, int CategoryID)
        {
            Cart currentGiftShopCart = Cart.GetCartObject();
            return currentGiftShopCart.GetProducts().Find(x => x.ProductID == ProductID).GetPropertyValueByName(key);
        }
        public static string GetFieldValuesById(int Id, string queryname)
        {
            string value = string.Empty;
            DataSet ds = new DataSet();
            ds = GetFieldValueByFieldId(Id, queryname);
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    value = Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            return value;
        }
        public static DataSet GetFieldValueByFieldId(int Id, string query)
        {
            string value = string.Empty;
            Hashtable parameters = new Hashtable();
            string queryName = string.Format(query, EmergeCMSContext.CurrentSiteName);
            parameters.Add("@" + PreRegistrationConstants.QUERY_FIELD_PARAMETER, Id);
            return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
        }
        public static string GetCommaSeperatedValues(string itemValue, string query)
        {
            string commaSeperatedValue = string.Empty;
            string[] values;
            values = itemValue.Split('|');
            string queryName = string.Format(query, EmergeCMSContext.CurrentSiteName);
            foreach (string val in values)
            {
                if (val != string.Empty)
                    commaSeperatedValue = commaSeperatedValue + GetFieldValuesById(Convert.ToInt16(val), queryName) + ", ";
            }
            return commaSeperatedValue.Trim().Trim(',');
        }
    }
}