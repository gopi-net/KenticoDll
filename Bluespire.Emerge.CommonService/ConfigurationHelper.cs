using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService.License;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService
{
    public class ConfigurationHelper
    {
        
        public static void CheckWebsiteConfiguration(Constants.Modules module, string pageName)
        {
            //TODO: Check for License
            //TODO: Check for Permissions
            
            if (!LicenseProvider.ValidateLicense(module.ToString()))
            {
                throw new ModuleNotPurchasedException(ResHelper.GetString("Emerge.LM.ModuleNotPurchasedException"));
            }

            if (MaintenanceService.IsSystemInMaintenanceMode(module))
            {
                throw new SystemInMaintenanceModeException(ResHelper.GetString("Emerge.MTN.SystemInMaintenanceModeException"));
            }

            if (MaintenanceService.IsModuleInMaintenanceMode(module))
            {
                throw new ModuleInMaintenanceModeException( ResHelper.GetStringFormat("Emerge.MTN.ModuleInMaintenanceModeException", module.ToString()));
            }

            if ( !string.IsNullOrEmpty(pageName) &&  !AuthorizationService.CheckPermission(module, pageName))
            {
                EmergeLogWriter.WriteInformation("Method Name: AuthorizationService.CheckPermission", EventCode.EMERGE_PERMISSIONMISSING, ResHelper.GetStringFormat("Emerge.Permission.ModuleLevelPermissionsMissingException", pageName, module.ToString()));
                throw new ModuleLevelPermissionsMissingException(ResHelper.GetStringFormat("Emerge.Permission.ModuleLevelPermissionsMissingException", pageName,  module.ToString()));
            }
        }
    }
}
