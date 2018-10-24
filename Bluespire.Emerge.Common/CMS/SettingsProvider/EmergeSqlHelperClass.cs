using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DataEngine;

namespace Bluespire.Emerge.Common.CMS.SettingsProvider
{
    public static class EmergeSqlHelperClass
    {
        public static DataSet ExecuteQuery(string queryName, Hashtable  hashedParameters, string where, string orderBy)
        {
            return ConnectionHelper.ExecuteQuery(queryName, GetQueryDataParameters(hashedParameters), where, orderBy);
        }

        public static DataSet ExecuteQuery(string queryText,Hashtable  hashedParameters, QueryTypeEnum queryType )
        {
            return ConnectionHelper.ExecuteQuery(queryText, GetQueryDataParameters(hashedParameters), queryType);
        }

        public static DataSet ExecuteQuery(string queryName, Hashtable hashedParameters, string columns, string where, string orderBy, int topN)
        {

            return ConnectionHelper.ExecuteQuery(queryName, GetQueryDataParameters(hashedParameters), where, orderBy, topN, columns);
        }

        public static DataSet ExecuteQuery1(string queryName, EmergeQueryDataParameters parameters, string where, string orderBy)
        {
            return ConnectionHelper.ExecuteQuery(queryName, parameters.Parameters, where, orderBy);
        }

        private static QueryDataParameters GetQueryDataParameters(Hashtable hashedParameters)
        {
            QueryDataParameters parameters = new QueryDataParameters();
            if (null != hashedParameters)
            {
                foreach (DictionaryEntry parameter in hashedParameters)
                {
                    parameters.Add(parameter.Key.ToString(), parameter.Value);
                }
            }
            return parameters;
        }
    }
}
