using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Web;
using System.Xml.Linq;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.SiteProvider;
using Bluespire.Emerge.Common.Relations;
using Bluespire.Emerge.CommonService;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.CMSHelper;


namespace Bluespire.Emerge.Components.StaffDirectory.Services
{

    /// <summary>
    /// Staff Directory service
    /// </summary>
    public class StaffDirectoryService : IStaffDirectoryService
    {
        string staffSiteName = EmergeStaticHelper.SetSiteName(StaffDirectoryConstants.CUSTOMTABLE_CODENAME_SD_STAFF);

        #region IStaffDirectoryService Members

        /// <summary>
        ///Get PhysicianByCriteria accepts where condition criteria and return physician result in Dataset
        /// </summary>
        /// <param name="WhereCondition">Where condition on the cheer card items.</param>
        /// <returns>dataset containing cheer card items.</returns>
        public System.Data.DataSet GetPhysicianByCriteria(string whereconditon)
        {
            if (!string.IsNullOrEmpty(whereconditon))
                whereconditon += " AND ";
            if (CustomTableDataHelper.HasStatusField(staffSiteName))
                whereconditon += Constants.CUSTOM_TABLE_STATUS_COLUMNNAME + " = 1 ";
            CustomTableItemProvider customTableRelationItem = new CustomTableItemProvider();
            string siteName = EmergeCMSContext.CurrentSiteName;
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(staffSiteName, whereconditon, string.Empty);
            ds = EmergeRelationHelper.GetRelationShipData(ds, staffSiteName);
            return ds;
        }
        /// <summary>
        /// method to Get staff details by Items ID
        /// </summary>
        /// <param name="ID">staff directory item id.</param>
        /// <returns>dataset containing staff details.</returns>
        public System.Data.DataSet GetPhysicianByItemId(int itemId)
        {
            CustomTableItemProvider customTableRelationItem = new CustomTableItemProvider();
            string WhereCondition = " ItemID = " + itemId;
            return GetPhysicianByCriteria(WhereCondition);
        }



        public DataSet GetStaffMemberByQuery(string query)
        {
            DataSet ds = new DataSet();
            ds = EmergeRelationHelper.GetRelationShipData(EmergeSqlHelperClass.ExecuteQuery(query, null, QueryTypeEnum.SQLQuery), EmergeStaticHelper.SetSiteName(StaffDirectoryConstants.CUSTOMTABLE_CODENAME_SD_STAFF));
            return ds;
        }



        public DataSet GetSeletectedRelationalTables()
        {
            return CustomTableDataHelper.GetCustomTableItemsByCondition(EmergeStaticHelper.SetSiteName(StaffDirectoryConstants.CUSTOMTABLE_CODENAME_SEARCHCONFIG), string.Empty, string.Empty);
        }
        #endregion

    }
}
