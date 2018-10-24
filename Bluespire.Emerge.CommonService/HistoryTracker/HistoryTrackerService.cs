using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.FormEngine;
using CMS.SiteProvider;

namespace Bluespire.Emerge.CommonService.HistoryTracker
{
    public class HistoryTrackerService
    {



        public static void TrackHistory(List<HistoryTrackerInfo> records)
        {
            try
            {
                foreach (HistoryTrackerInfo record in records)
                {
                    Dictionary<string, object> recordValues = new Dictionary<string, object>();
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_CUSTOMTABLEBEINGCHANGED, record.CustomTableClassName);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_DISPLAYNAMEOFCUSTOMTABLEBEINGCHANGED, record.CustomTableDisplayName);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_COLUMNNAMEOFTABLEBEINGCHANGED, record.ColumnName);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_OLDVALUE, record.OldValue);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_NEWVALUE, record.NewValue);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_DESCRIPTION, record.Description);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_GROUPGUID, record.GroupGuid);
                    recordValues.Add(Constants.FIELDS_HISTORYTRACKER_ITEMIDOFITEMBEINGCHANGED, record.ItemID);
                    int historyItemID = 0;


                    CustomTableDataHelper.SaveCustomTableItem(EmergeStaticHelper.SetSiteName(Constants.CUSTOMTABLE_CODENAME_FOR_HISTORYTRACKER), ref historyItemID, recordValues);

                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("History Tracker", EventCode.EMERGE_ADD, ex.ToString());
            }

        }

        /// <summary>
        /// method to check if Custom table being changed is allowed to track history.
        /// </summary>
        /// <param name="ClassNameOfTableBeingChanged"></param>
        /// <returns></returns>
        public static bool IsTableAllowedForHistoryTracking(string ClassNameOfTableBeingChanged)
        {

            return (CustomTableDataHelper.GetCustomTableItemsByCriteria(EmergeStaticHelper.SetSiteName(Constants.CUSTOMTABLE_CODENAME_CONTAINING_TABLESEXCLUDED_FROM_HISTORYTRACKING), " TableName = '" + ClassNameOfTableBeingChanged + "'").Count == 0 ? true : false);
        }

        public static void DeleteHistoryDetails(string fromDateTime, string toDateTime)
        {
            string queryName;


            queryName = string.Format(Constants.QUERY_DELETELOGGEDHISTORYDETAILS, EmergeSiteInfoProvider.CurrentSiteName);


            Hashtable hashedQueryParameters = new Hashtable();
            hashedQueryParameters.Add("@from", fromDateTime);
            hashedQueryParameters.Add("@to", toDateTime);
            

            EmergeSqlHelperClass.ExecuteQuery(queryName, hashedQueryParameters, null, null);
        }

    }
}
