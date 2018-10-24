using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.UIControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.LicenseManager.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.CMS.GlobalHelper;


namespace Bluespire.Emerge.Web.Pages.BasePages
{
    /// <summary>
    /// Base class for the modal pages.
    /// </summary>
    public class EmergeModalPage :CMSModalPage
    {
        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        protected string PageName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the Module
        /// </summary>
        protected Constants.Modules Module
        {
            get;
            set;
        }

        /// <summary>
        /// OnInit method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            
            Page.ErrorPage = Constants.UNHNADLED_EXCEPTION_ERROR_PAGE_ADMIN;
            Page.Error += Page_Error;

            base.OnInit(e);
            try
            {
                ConfigurationHelper.CheckWebsiteConfiguration(Module, PageName);
            }
            catch (ModuleNotPurchasedException moduleNotPurchased)
            {
                OnError(moduleNotPurchased, true);
            }
            catch (SystemInMaintenanceModeException systemInMaintenance)
            {
                OnError(systemInMaintenance, true);
            }
            catch (ModuleInMaintenanceModeException moduleInMaintenance)
            {
                OnError(moduleInMaintenance, true);
            }
            catch (MissingLicenseKeyException missingLicenseKey)
            {
                OnError(missingLicenseKey, true);
            }
            catch (ExpiredLicenseKeyException expiredLicenseKey)
            {
                OnError(expiredLicenseKey, true);
            }
            catch (InvalidDomainNameException invalidDomainName)
            {
                OnError(invalidDomainName, true);
            }
            catch (LicenseKeyException LicenseKeyException)
            {
                OnError(LicenseKeyException,true);
            }
            catch (ModuleLevelPermissionsMissingException permissionMisingException)
            {
                OnError(permissionMisingException, true);
            }
        }

        /// <summary>
        /// OnError method in case of Redirection to error page
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="IsRedirection">true if redirection required</param>
        protected void OnError(Exception ex, bool IsRedirection)
        {
            if (IsRedirection)
                ExceptionHandler.HandleException(ex);
            else
            {
                string errorMessage = string.Empty;
                ExceptionHandler.HandleException(ex, ref errorMessage);
                ShowError(errorMessage);
            }

        }

        void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            EmergeLogWriter.WriteError("CheerCard", Common.Logging.EventCode.EMERGE_ADD, ex.ToString());
            EmergeURLHelper.Redirect(Page.ErrorPage);
        }

        /// <summary>
        /// OnError method in case of Redirection parameter is not available. This method will treated IsRedirection as true.
        /// </summary>
        /// <param name="ex"></param>
        protected void OnError(Exception ex)
        {
            OnError(ex, true);
        }
    }
}
