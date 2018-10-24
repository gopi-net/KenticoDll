using System;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public class EmergeQueryHelper
    {
        public static string GetString(String name, string defaultValue)
        {
            return QueryHelper.GetString(name, defaultValue);
        }

        public static string BuildQuery(params string[] items)
        {
            return QueryHelper.BuildQuery(items);
        }

        public static int GetInteger(string name, int defaultValue)
        {
            return QueryHelper.GetInteger(name, defaultValue);
        }

        public static string EncodedQueryString { get { return QueryHelper.EncodedQueryString; } }

      
    }
}
