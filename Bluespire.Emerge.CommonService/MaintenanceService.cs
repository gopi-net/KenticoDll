using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CMS.SiteProvider;
using Bluespire.Emerge.Common;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.CMSHelper;
namespace Bluespire.Emerge.CommonService
{
    /// <summary>
    /// Service to check if any module or the entire system is in Maintenance mode
    /// </summary>
    public class MaintenanceService
    {
        /// <summary>
        /// Checks if the module is in maintenance mode
        /// </summary>
        /// <param name="module">module</param>
        /// <returns>true, if the module is in maintenance mode else, false</returns>
        public static bool IsModuleInMaintenanceMode(Constants.Modules module)
        {
            if (module == Constants.Modules.Maintenance || module == Constants.Modules.License || module == Constants.Modules.HistoryTracker)
                return false;
            return IsInMaintenanceMode(module.ToString());
        }

        /// <summary>
        /// Checks if the system is in maintenance mode
        /// </summary>
        /// <returns>true, if the system is in maintenance mode else, false</returns>
        public static bool IsSystemInMaintenanceMode(Constants.Modules module)
        {
            if (module == Constants.Modules.Maintenance || module == Constants.Modules.License || module == Constants.Modules.HistoryTracker)
                return false;
            return IsInMaintenanceMode("System");
        }

        private static bool IsInMaintenanceMode(string parameter)
        {
            try
            {
                if (parameter == Constants.Modules.Maintenance.ToString())
                    return false;
                bool isMaintenanceMode = false;
                string siteName = EmergeCMSContext.CurrentSiteName;
                DataSet maintenanceDS = CustomTableItemProvider.GetItems(string.Format("customtable.Emerge_{0}_MTN_MaintenanceInfo", siteName), null, null);

                if (null != maintenanceDS && maintenanceDS.Tables[0].Rows.Count > 0)
                    isMaintenanceMode = Convert.ToBoolean(maintenanceDS.Tables[0].Rows[0][parameter]);

                return isMaintenanceMode;
            }
            catch
            {
                return false;
            }
        }
    }
}
