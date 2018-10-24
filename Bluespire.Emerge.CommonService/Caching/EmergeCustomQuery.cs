using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using System;
using System.Data;

namespace Bluespire.Emerge.CommonService.Caching
{
    public class EmergeCustomQuery : ICacheable
    {

        public DataSet GetData()
        {
            return EmergeSqlHelperClass.ExecuteQuery(Key, null, null, null);
        }

        public string Key
        {
            get;
            set;
        }
    }
}
