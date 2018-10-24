using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using CMS.SiteProvider;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.Base;
using CMS.Core;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Web
{
    /// <summary>
    /// Summary description for EmergeTransformationHelper
    /// </summary>
    public class EmergeTransformationHelper : TransformationHelper
    {
        public EmergeTransformationHelper()
            : base()
        {
            
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Helper object.
        /// </summary> 
        private static EmergeTransformationHelper mEmergeHelperObject = null;
        
        /// <summary>
        /// Helper object.
        /// </summary>
        public static EmergeTransformationHelper EmergeHelperObject
        {
            get
            {
                if (mEmergeHelperObject == null)
                {
                    // Load the transformation helper
                    mEmergeHelperObject = CMSExtensibilitySection.LoadHelper<EmergeTransformationHelper>();
                }

                return mEmergeHelperObject;
            }
            set
            {
                mEmergeHelperObject = value;
            }
        }


        /// <summary>
        /// Returns URL for current search result.
        /// </summary>
        /// <param name="id">ID of the item</param>
        /// <param name="type">Type</param>
        /// <param name="absolute">Indicates whether generated url should be absolute</param>
        public static string EmergeSearchResultUrl(string id, string type, bool absolute)
        {
            string url = string.Empty;

            if (type != Constants.INDEX_TYPE_CUSTOMTABLE)
            {
                url = TransformationHelper.HelperObject.SearchResultUrl(id, type, absolute);
            }
            else
            {
                url = EmergeTransformationHelper.GetSearchUrl(id, type, absolute);
            }

            return url;
        }



        private static string GetSearchUrl(string id, string type, bool absolute)
        {
            string url = string.Empty;

            if (!String.IsNullOrEmpty(id))
            {
                // Get Datarows for current results
//                Hashtable resultRows = RequestStockHelper.GetItem(CMS.Search.SearchContext.CurrentSearchResults) as Hashtable;
                Hashtable resultRows = CMS.Search.SearchContext.CurrentSearchResults;
                // Check whether id and datarow collection exists
                if (resultRows != null)
                {
                    // Get current datarow
                    DataRow dr = resultRows[id] as DataRow;

                    string[] idValue = id.Split(';');
                    string tableName = string.Empty;
                    if (idValue.Length > 1)
                    {
                        tableName = idValue[1].Replace("_cms.customtable", string.Empty);
                        tableName = EmergeStaticHelper.SetSiteName(tableName);
                    }
                    // Check whether datarow exists and contains required column
                    if (dr != null)
                    {

                        //get URL from common custom table "Custom Table search result URL"
                        // which contain all the search result url and there respective parameters

                        if (HttpContext.Current.Application[EmergeStaticHelper.SetSiteName(Constants.SMART_SEARCH_RESULT_URL_TABLE)] == null)
                        {
                            string where = string.Empty;
                            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.SMART_SEARCH_RESULT_URL_TABLE), where, string.Empty);
                            HttpContext.Current.Application[EmergeStaticHelper.SetSiteName(Constants.SMART_SEARCH_RESULT_URL_TABLE)] = ds;

                        }

                        DataSet resultds = (DataSet)HttpContext.Current.Application[EmergeStaticHelper.SetSiteName(Constants.SMART_SEARCH_RESULT_URL_TABLE)];

                        //check url specified in the "Custom Table search result URL"
                        if (resultds != null && !DataHelper.DataSourceIsEmpty(resultds))
                        {
                            // check current table present in the "Custom Table search result URL" table
                            DataTable dt = resultds.Tables[0];
                            //dr.Table.TableName does not contain actual table name
                            var res = from resultTable in dt.AsEnumerable()
                                      where Convert.ToString(resultTable[Constants.SMART_SEARCH_RESULT_URL_CUSTOMTABLE_COLUMN]).ToLower() == tableName.ToLower()
                                      select resultTable;
                            foreach (DataRow item in res.ToList())
                            {
                                string[] parameterColumns = Convert.ToString(item[Constants.SMART_SEARCH_RESULT_URL_COLUMN_PARAMETER_COLUMN]).Split(Constants.MULTI_VALUE_SEPERATOR);

                                //replace url with mention paramenter columnValue
                                string newUrl = Convert.ToString(item[Constants.SMART_SEARCH_RESULT_URL_COLUMN_RESULT_URL]);
                                for (int i = 0; i < parameterColumns.Length; i++)
                                {
                                    if ((dr != null) && (dr.Table.Columns.Contains(parameterColumns[i])))
                                    {
                                        newUrl = newUrl.Replace("{" + i + "}", Convert.ToString(dr[parameterColumns[i]]));
                                    }
                                }
                                url = newUrl;
                            }
                        }
                    }
                }
            }
            return url;
        }
    }
}