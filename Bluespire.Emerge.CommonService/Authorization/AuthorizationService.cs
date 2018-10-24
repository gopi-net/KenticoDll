using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using CMS.DocumentEngine;
using System.Web;
using System.IO;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;
using CMS.Membership;

namespace Bluespire.Emerge.CommonService
{
    /// <summary>
    /// Service to check the permissions based on Page Name.
    /// </summary>
    public class AuthorizationService
    {

        /// <summary>
        /// Returns true if the current user is authorized to access the given Page with required permission.
        /// </summary>
        /// <param name="module">module name enum</param>
        /// <param name="PageName">name of the page for which persmissions to be check</param>
        /// <exception cref="PermissionsConfigFileNotFoundException"> Thrown if  Permissions Config FileNotFound Exception</exception>
        /// <exception cref="PermissionsMissingInPermissionConfigException"> Thrown if  Permissions not found for a given page, in PermissionConfig file</exception>
        public static bool CheckPermission(Constants.Modules module, string PageName)
        {
            string moduleCodeName = string.Empty;
            string permissionName = string.Empty;
            bool IsAuthorised = false;



            moduleCodeName = GetCodeNameByModuleEnum(module);
            XDocument xdoc;

            xdoc = LoadXMLDocument(Constants.PERMISSIONS_CONFIG_DOCUMENT_CACHE_KEY_NAME, Constants.PERMISSIONS_CONFIG_FILE_PATH, "Emerge.Exception.ErrorMessage.PermissionsConfigFileNotFoundException");

            var permissions = xdoc.Descendants(Constants.PERMISSIONS_CONFIG_ROOT_NODE_NAME).Elements(Constants.PERMISSIONS_CONFIG_PAGE_PERMISSION_NODE_NAME);



            string permissionInConfig = string.Empty;
            try
            {
                foreach (var permission in permissions)
                {
                    if (permission.Elements(Constants.PERMISSIONS_PAGENAME).First().Value.ToLower().Equals(PageName.ToLower()))
                    {
                        permissionInConfig = permission.Elements(Constants.PERMISSIONS_PAGEPERMISSIONS).First().Value;
                        break;
                    }
                }

            }
            catch (InvalidOperationException)
            {
                string logMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorLog.PermissionsMissingInPermissionConfig"), PageName, Constants.PERMISSIONS_CONFIG_FILE_PATH);
                string EventMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.PermissionsMissingInPermissionConfig"), PageName);

                EmergeLogWriter.WriteError("Method Name: AuthorizationService.CheckPermission", EventCode.EMERGE_PERMISSIONMISSING, logMessage);
                throw new PermissionsMissingInPermissionConfigException(EventMessage);
            }


            if (permissionInConfig.Equals(string.Empty))
            {
                string logMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorLog.PermissionsMissingInPermissionConfig"), PageName, Constants.PERMISSIONS_CONFIG_FILE_PATH);
                string EventMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.PermissionsMissingInPermissionConfig"), PageName);

                EmergeLogWriter.WriteError("Method Name: AuthorizationService.CheckPermission", EventCode.EMERGE_PERMISSIONMISSING, logMessage);
                throw new PermissionsMissingInPermissionConfigException(EventMessage);
            }


            string[] permissionsInConfig;


            permissionsInConfig = permissionInConfig.Split(new string[] { Constants.PERMISSIONS_SEPARATOR }, StringSplitOptions.None);

            foreach (string permission in permissionsInConfig)
            {
                IsAuthorised = MembershipContext.AuthenticatedUser.IsAuthorizedPerResource(moduleCodeName, permission);

                if (IsAuthorised) break;
            }

            return IsAuthorised;

        }


        /// <summary>
        /// Returns conde name based on module enum.
        /// </summary>
        /// <param name="module">module name enum</param>
        /// <exception cref="ModuleCodeNameNotFoundException"> Thrown if Code name of a module not found in Constants.</exception>
        private static string GetCodeNameByModuleEnum(Constants.Modules module)
        {
            string codeName = string.Empty;

            switch (module)
            {
                case Constants.Modules.StaffDirectory:
                    codeName = Constants.STAFF_DIRECTORY_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.EventsCalendar:
                    codeName = Constants.EVENTS_CALENDAR_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.CheerCard:
                    codeName = Constants.CHEER_CARD_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.Maintenance:
                    codeName = Constants.MAINTENANCE_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.License:
                    codeName = Constants.LICENSE_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.GiftShop:
                    codeName = Constants.GIFTSHOP_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.Career:
                    codeName = Constants.CAREER_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.Rates:
                    codeName = Constants.RATES_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.Donation:
                    codeName = Constants.DONATION_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.Location:
                    codeName = Constants.LOCATION_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.PreRegistration:
                    codeName = Constants.PREREGISTRATION_MODULE_CODE_NAME;
                    break;

                case Constants.Modules.HistoryTracker:
                    codeName = Constants.HISTORYTRACKER_MODULE_CODE_NAME;
                    break;                    
                    
                default:
                    string EventMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.ModuleCodeNameNotFoundException"), module.ToString());

                    EmergeLogWriter.WriteError("Method Name: AuthorizationService.GetCodeNameByModuleEnum", EventCode.EMERGE_MODULECODENAME_MISSING, EventMessage);
                    throw new ModuleCodeNameNotFoundException(EventMessage);

            }

            return codeName;
        }

        /// <summary>
        /// Checks the specified Action permission.
        /// </summary>
        public static bool CheckActionPermission(string ActionName, string CustomTableClassName)
        {
            // Check 'Modify' permission
            bool IsAuthorised = true;

            XDocument xdoc;

            xdoc = LoadXMLDocument(Constants.ACTION_PERMISSIONS_CONFIG_DOCUMENT_CACHE_KEY_NAME, Constants.ACTION_PERMISSIONS_CONFIG_FILE_PATH, "Emerge.Exception.ErrorMessage.ActionPermissionsConfigFileNotFoundException");

            var permissions = xdoc.Descendants(Constants.ACTION_PERMISSIONS_CONFIG_ROOT_NODE_NAME).Elements(Constants.ACTION_PERMISSIONS_CONFIG_PAGE_PERMISSION_NODE_NAME);



            string permissionInConfig = string.Empty;

            try
            {
                foreach (var permission in permissions)
                {
                    if (permission.Elements(Constants.ACTION_PERMISSIONS_ACTIONNAME).First().Value.ToLower().Equals(ActionName.ToLower()))
                    {
                        permissionInConfig = permission.Elements(Constants.ACTION_PERMISSIONS_PERMISSIONNAME).First().Value;
                        break;
                    }
                }

            }
            catch (InvalidOperationException)
            {
                string EventMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.ActionPermissionsMissingInPermissionConfig"), ActionName, Constants.ACTION_PERMISSIONS_CONFIG_FILE_PATH);
                EmergeLogWriter.WriteError("Method Name: AuthorizationService.CheckActionPermission", EventCode.EMERGE_ACTION_PERMISSIONMISSING, EventMessage);
                throw new PermissionsMissingInPermissionConfigException(EventMessage);
            }

            if (permissionInConfig.Equals(string.Empty))
            {
                string EventMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.ActionPermissionsMissingInPermissionConfig"), ActionName, Constants.ACTION_PERMISSIONS_CONFIG_FILE_PATH);
                EmergeLogWriter.WriteError("Method Name: AuthorizationService.CheckActionPermission", EventCode.EMERGE_ACTION_PERMISSIONMISSING, EventMessage);
                throw new PermissionsMissingInPermissionConfigException(EventMessage);
            }


            string[] permissionsInConfig;


            permissionsInConfig = permissionInConfig.Split(new string[] { Constants.ACTION_PERMISSIONS_SEPARATOR }, StringSplitOptions.None);

            foreach (string PermissionName in permissionsInConfig)
            {

                if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", PermissionName) &&
               !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(CustomTableClassName, PermissionName))
                {
                    IsAuthorised = false;
                }

                if (!IsAuthorised) break;
            }


            return IsAuthorised;


        }


        /// <summary>
        /// Method loads document from either from cache or from file system and returns back to caller.
        /// </summary>
        /// <param name="CacheKeyName">Cache key in case document need to be cached.</param>
        /// <param name="DocumentPath">path of the document to be load</param>
        /// <param name="ResourceKeyForFileNotFoundException">Resource key for File not found exception.</param>
        /// <exception cref="PermissionsConfigFileNotFoundException"> Thrown if  Permissions Config FileNotFound Exception</exception>
        private static XDocument LoadXMLDocument(string CacheKeyName, string DocumentPath, string ResourceKeyForFileNotFoundException)
        {
            XDocument xdoc;

            if (null == CacheHelper.GetItem(CacheKeyName, false))
            {
                try
                {
                    xdoc = XDocument.Load(HttpContext.Current.Server.MapPath(DocumentPath));
                }
                catch (FileNotFoundException)
                {
                    string EventMessage = String.Format(ResHelper.GetString(ResourceKeyForFileNotFoundException), DocumentPath);
                    EmergeLogWriter.WriteError("Method Name: AuthorizationService.LoadActionPermissionConfigDocument", EventCode.EMERGE_PERMISSIONCONFIGFILEMISSING, EventMessage);
                    throw new PermissionsConfigFileNotFoundException(EventMessage);
                }
            }
            else
            {
                xdoc = (XDocument)CacheHelper.GetItem(CacheKeyName, false);
            }

            return xdoc;
        }



    }
}
