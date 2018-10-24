using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using CMS.FormEngine;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.Helpers;

namespace Bluespire.Emerge.Web
{
    /// <summary>
    /// Service to handle common CMS functions.
    /// </summary>
    public class EmergeCMSHelper
    {

        /// <summary>
        /// retunrs display Name of the Custom table
        /// </summary>
        /// <param name="CustomtableName">Code Name for Custom Table</param>
        /// <exception cref="InvalidCustomTableNameException"> Thrown if  invalid Custom Table Name.</exception>
        public static DataClassInfo GetDataClassInfo(string CustomtableName)
        {
            DataClassInfo dcp = DataClassInfoProvider.GetDataClassInfo(CustomtableName);
            if (dcp == null) throw new InvalidCustomTableNameException(String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.InvalidCustomTableNameException"), CustomtableName));
            return dcp;
        }

        /// <summary>
        /// Gets existing columns from form info
        /// </summary>
        /// <param name="sort">Indicates if the columns should be sorted</param>
        /// <param name="fi">Indicates the FormInfo</param>
        public static List<string> GetExistingColumns(bool sort, FormInfo fi)
        {
            var existingColumns = fi.GetColumnNames();
            if (sort)
            {
                existingColumns.Sort(StringComparer.InvariantCultureIgnoreCase);
            }

            return existingColumns;
        }

       
    }
}
