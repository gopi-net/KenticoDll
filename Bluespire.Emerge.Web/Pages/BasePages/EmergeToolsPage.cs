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
using System.Data;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using System.Web.UI.WebControls;
using CMS.Helpers;

namespace Bluespire.Emerge.Web.Pages.BasePages
{
    /// <summary>
    /// Base class for the modules tools pages.
    /// </summary>
    public class EmergeToolsPage : CMSCustomTablesToolsPage
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
        /// Gets or sets the name of the module.
        /// </summary>
        protected  Constants.Modules Module
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
            Page.ErrorPage = Constants.EXCEPTION_ERROR_PAGE_URL_ADMIN;
            Page.Error += Page_Error;

            base.OnInit(e);
            try
            {
                ConfigurationHelper.CheckWebsiteConfiguration(Module, PageName);
            }
            catch (SystemInMaintenanceModeException systemInMaintenance)
            {
                OnError(systemInMaintenance, true);
            }
            catch (ModuleInMaintenanceModeException moduleInMaintenance)
            {
                OnError(moduleInMaintenance, true);
            }
            catch (ModuleNotPurchasedException moduleNotPurchased)
            {
                OnError(moduleNotPurchased, true);
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
                OnError(LicenseKeyException, true);
            }
            catch (ModuleLevelPermissionsMissingException permissionMisingException)
            {
                OnError(permissionMisingException, true);
            }
            catch (Exception ex)
            {
                OnError(ex, true);
            }
        }

        void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            EmergeLogWriter.WriteError(Module.ToString(), Common.Logging.EventCode.EMERGE_ADD, ex.ToString());
            EmergeURLHelper.Redirect(Page.ErrorPage);
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
                string errorMessage =  string.Empty;
                ExceptionHandler.HandleException(ex, ref errorMessage);
                ShowError(errorMessage);
            }
           
        }

        /// <summary>
        /// OnError method in case of Redirection parameter is not available. This method will treated IsRedirection as true.
        /// </summary>
        /// <param name="ex"></param>
        protected void OnError(Exception ex)
        {
            OnError(ex, true);
        }


        protected DataSet GetApplications(string url)
        {
            string queryName = string.Format("customtable.Emerge_{0}_Common_Dashboard.GetDashboardItems", EmergeCMSContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@CurrentUrl",url );

            return ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
        }
        protected DataSet GetApplicationItems(int appId)
        {
            string queryName = string.Format("customtable.Emerge_{0}_Common_Dashboard.GetDashboardApplicationItems", EmergeCMSContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@AppId", appId);

            return ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
        }
        protected void repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton btn = (LinkButton)e.Item.FindControl("lnkItem") as LinkButton;
            if (e.CommandName == "Open")
                EmergeURLHelper.Redirect(btn.CommandArgument);
        }
        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataRowView row = (DataRowView)e.Item.DataItem;
            if (row != null)
            {
                LinkButton btn = (LinkButton)e.Item.FindControl("lnkItem") as LinkButton;
                btn.CommandArgument = row["ElementTargetURL"].ToString();
            }
        }
        protected void SetupInnerRepeater(Repeater repInner, int itemId)
        {
            var applicationItems = GetApplicationItems(itemId);
            if (!((applicationItems == null) || DataHelper.DataSourceIsEmpty(applicationItems)))
            {
                repInner.DataSource = applicationItems.Tables[0].DefaultView;
                repInner.DataBind();
            }
        }
    }
}
