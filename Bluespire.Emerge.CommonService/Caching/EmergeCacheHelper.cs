
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.CommonService.Caching;
using CMS.SiteProvider;
using System.Data;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.Membership;
namespace Bluespire.Emerge.Common
{

    public static class EmergeCacheHelper
    {
        static int cacheMin = CacheHelper.CacheMinutes(EmergeCMSContext.CurrentSiteName);
        public static void TouchKey(string key)
        {
            CacheHelper.TouchKey(key + Constants.EMERGE_CACHE_CLEAR_KEY_SUFFIX);
        }
        private static bool CachingEnabled(string className)
        {
            //  CustomTableItemProvider ctProvider = new CustomTableItemProvider(MembershipContext.AuthenticatedUser);
            string cacheTablesTable = string.Format(Constants.EMERGE_TABLE_CACHED_TABLES, EmergeCMSContext.CurrentSiteName);
            string filterCondition = string.Format("{0}='{1}'", Constants.EMERGE_COLUMN_CACHED_TABLENAME, className);
            DataSet cacheTables = CustomTableItemProvider.GetItems(cacheTablesTable, filterCondition, string.Empty);
            if (EmergeDataHelper.DataSourceIsEmpty(cacheTables))
                return false;
            return true;
        }

        public static DataSet GetData(ICacheable objCaching)
        {
            DataSet items = new DataSet();
            if (CachingEnabled(objCaching.Key))
                items = GetFromCache(objCaching);
            else
                items = objCaching.GetData();
            return items;
        }

        private static DataSet GetFromCache(ICacheable objCaching)
        {
            DataSet items = new DataSet();
            CachedSection<DataSet> cs = new CachedSection<DataSet>(ref items, cacheMin, true, objCaching.Key, null);
            if (cs.LoadData)
                items = SetCache(objCaching, cs);
            return items;
        }
        private static DataSet SetCache(ICacheable objCaching, CachedSection<DataSet> cs)
        {
            DataSet items = objCaching.GetData();
            cs.Data = items;
            cs.CacheDependency = CacheHelper.GetCacheDependency(objCaching.Key + Constants.EMERGE_CACHE_CLEAR_KEY_SUFFIX);
            return items;
        }

    }
}
