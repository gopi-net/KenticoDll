using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using System.Web;
using CMS.SiteProvider;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService
{
    /// <summary>
    /// Service to Handle Exceptions.
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Function to Handle exception.
        /// </summary>
        private static void HandleException(Exception ex, ref string errorMessage, bool IsRedirection)
        {

            string ExceptionType = ex.GetType().ToString();
            string title = string.Empty;

            switch (ExceptionType)
            {

                case "Bluespire.Emerge.Common.Exceptions.ModuleNotPurchasedException":
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.ModuleNotPurchasedException");
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.ModuleNotPurchasedException");
                    break;

                case "Bluespire.Emerge.LicenseManager.Exceptions.MissingLicenseKeyException":
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.MissingLicenseKeyException");
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.MissingLicenseKeyException");
                    break;

                case "Bluespire.Emerge.LicenseManager.Exceptions.ExpiredLicenseKeyException":
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.ExpiredLicenseKeyException");
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.ExpiredLicenseKeyException");
                    break;

                case "Bluespire.Emerge.LicenseManager.Exceptions.InvalidDomainNameException":
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.InvalidDomainNameException");
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.InvalidDomainNameException");
                    break;

                case "Bluespire.Emerge.LicenseManager.Exceptions.LicenseKeyException":
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.LicenseKeyException");
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.LicenseKeyException");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.ActionNotFeasibleException":
                    errorMessage = ex.Message.ToString();
                    break;

                case "Bluespire.Emerge.Common.Exceptions.PermissionsMissingInPermissionConfigException":
                    errorMessage = ex.Message.ToString();
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.PermissionsMissingInPermissionConfigException");
                    break;


                case "Bluespire.Emerge.Common.Exceptions.CheerCardPreviewSaveExternalException":
                    errorMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.CheerCardPreviewSaveExternalException"), ex.Message);
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.CheerCardPreviewSaveExternalException");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.CheerCardEmailToFormFieldMissingException":
                    errorMessage = String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.CheerCardEmailToFormFieldMissingException"));
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.CheerCardEmailToFormFieldMissingException");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.CheerCardConfigurationItemNotFound":
                    errorMessage = String.Format(ResHelper.GetString(  String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.CheerCardConfigurationItemNotFound"),ex.Message)));
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.CheerCardConfigurationItemNotFound");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.CheerCardPreviewHtmlItemNotFound":
                    errorMessage = String.Format(ResHelper.GetString(String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.CheerCardPreviewHtmlItemNotFound"), ex.Message)));
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.CheerCardPreviewHtmlItemNotFound");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.CheerCardPreviewImageConfigItemsNotFound":
                    errorMessage = String.Format(ResHelper.GetString(String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.CheerCardPreviewImageConfigItemsNotFound"), ex.Message)));
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.CheerCardPreviewImageConfigItemsNotFound");
                    break;

                case "Bluespire.Emerge.Common.Exceptions.ModuleLevelPermissionsMissingException":
                    errorMessage = ex.Message.ToString();
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.ModuleLevelPermissionsMissingException");
                    break;
                case "Bluespire.Emerge.Common.Exceptions.SystemInMaintenanceModeException":
                    errorMessage = ex.Message.ToString();
                    title = ResHelper.GetString("Emerge.MTN.SystemInMaintenanceTitle");
                    break;
                case "Bluespire.Emerge.Common.Exceptions.ModuleInMaintenanceModeException":
                    errorMessage = ex.Message.ToString();
                    title = ResHelper.GetString("Emerge.MTN.ModuleInMaintenanceTitle");
                    break;

                default:
                    EmergeLogWriter.WriteError("Unghandled Exception", EventCode.EMERGE_UNHANDLED, ex.ToString());
                    title = ResHelper.GetString("Emerge.Exception.PageTitle.UnhandledException");
                    errorMessage = ResHelper.GetString("Emerge.Exception.ErrorMessage.UnhandledException");
                    break;
            }
            if (IsRedirection)
            {
               // if (CMSContext.ViewMode != CMS.PortalEngine.ViewModeEnum.LiveSite)
                    URLHelper.Redirect(Constants.EXCEPTION_ERROR_PAGE_URL_ADMIN + "?title=" + title + "&text=" + errorMessage);
            }
        }

        /// <summary>
        /// This Method will get the Error message based on type of Exception. (to be Called from Webparts.)
        /// </summary>
        public static void HandleException(Exception ex, ref string errorMessage)
        {
            HandleException(ex, ref errorMessage, false);
        }

        /// <summary>
        /// This Method will get the Error message based on type of Exception, and redirect to the Error page. (to be called from Admin pages.)
        /// </summary>
        /// <param name="exception">Occured Exception.</param>
        public static void HandleException(Exception exception)
        {
            string errorMessage = string.Empty;
            HandleException(exception, ref errorMessage, true);
        }
    }
}
