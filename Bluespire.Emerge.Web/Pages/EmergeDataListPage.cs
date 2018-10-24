using System;
using CMS.SiteProvider;
using CMS.UIControls;
using Bluespire.Emerge.Web.Pages.BasePages;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;
using CMS.Base.Web.UI.ActionsConfig;

namespace Bluespire.Emerge.Web.Pages
{
    /// <summary>
    /// Base class for the data list pages. This class must be inherited by all the pages those shows the list of data of custom tables.
    /// </summary>
    public class EmergeDataListPage : EmergeToolsPage
    {

        /// <summary>
        /// Gets or sets the Title Text
        /// </summary>

        private string _TitleText = string.Empty;
        public string TitleText {
            get {
                if (string.IsNullOrEmpty(_TitleText))
                {
                    return GetString("customtable.edit.header");
                }
                else
                {
                    return _TitleText;
                }
            }
            set {
                _TitleText = value;
            }
        }


        /// <summary>
        /// Gets or sets the new Item page url.
        /// </summary>
        protected string NewItemPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the select fields page url.
        /// </summary>
        protected string SelectFieldsPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or set the list page url.
        /// </summary>
        protected string ListPage
        {
            get;
            set;
        }

        private bool? isSiteManager;
        /// <summary>
        /// Gets whether this page is being accessed from site manager.
        /// </summary>
        protected bool IsSiteManager
        {
            get
            {
                if (!isSiteManager.HasValue)
                    isSiteManager = (QueryHelper.GetInteger("sm", 0) == 1);
                return isSiteManager.Value;
            }
        }

        private int? customTableID;
        /// <summary>
        /// Gets the id of the selected custom table.
        /// </summary>
        protected int CustomTableID
        {
            get
            {
                if (!customTableID.HasValue)
                    customTableID = QueryHelper.GetInteger("customtableid", 0);
                if (customTableID == 0)
                    customTableID = QueryHelper.GetInteger("objectid", 0);
                if (customTableID == 0)
                {
                    string customTableName = QueryHelper.GetString("customtablename", string.Empty);
                    if (!String.IsNullOrEmpty(customTableName))
                    {
                        string className = string.Format(customTableName, SiteContext.CurrentSiteName);
                        DataClassInfo classInfo = CustomTableDataHelper.GetCustomTableClassInfo(className);
                        customTableID = classInfo.ClassID;
                    }
                }
                return customTableID.Value;
            }
        }

        private DataClassInfo dci = null;
        /// <summary>
        /// Gets or sets the Data class info object.
        /// </summary>
        protected DataClassInfo DataClassInfo
        {
            get
            {
                return dci;
            }
            set
            {
                dci = value;
            }
        }

        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RequireSite = false;
        }

        /// <summary>
        /// On page load method. This method should be called from page_load event in the derived class.
        /// </summary>
        protected void OnPageLoad()
        {

            // Read data only if user is site manager global admin or table is bound to current site
            if (CurrentUser.IsGlobalAdministrator || (ClassSiteInfoProvider.GetClassSiteInfo(CustomTableID, EmergeCMSContext.CurrentSiteID) != null))
            {
                // Get CustomTable class
                DataClassInfo = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            }

            // Set edited object
            EditedObject = DataClassInfo;

            if (DataClassInfo != null)
            {
                ScriptHelper.RegisterDialogScript(this);
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectFields", ScriptHelper.GetScript("function SelectFields() { modalDialog('" +
                                                                                                                    ResolveUrl(SelectFieldsPage) + "?customtableid=" + CustomTableID + "'  ,'CustomTableFields', 500, 500); }"));
                if (!IsSiteManager)
                {
                    CurrentMaster.Title.TitleText = TitleText;
                   
                    CurrentMaster.Title.HelpTopicName = "custom_tables_data";
                    CurrentMaster.Title.HelpName = "helpTopic";
                }

                // Check 'Read' permission
                if (CheckForPermissions())
                {
                    ShowError(String.Format(GetString("customtable.permissiondenied.read"), DataClassInfo.ClassName));
                    return;
                }
                setBreadCrumb();
                setHeaderActions();
            }
        }
        protected virtual void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString("customtable.list.title"),
                RedirectUrl = ListPage 
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = DataClassInfo.ClassDisplayName
            };
            PageBreadcrumbs.AddBreadcrumb(item);
        }

        protected virtual void setHeaderActions()
        {
            HeaderActions.AddAction(new HeaderAction
            {
                Text = GetString("Emerge.CreateNewItem"),
                RedirectUrl = ResolveUrl(NewItemPage + "?new=1&customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : "")),
            });


            HeaderActions.AddAction(new HeaderAction
            {
                Text = GetString("customtable.data.selectdisplayedfields"),
                OnClientClick = "SelectFields();",
            });
        }

        /// <summary>
        /// Checks if the current user has the required permissions to access the resources on this page.
        /// </summary>
        /// <returns></returns>
        protected bool CheckForPermissions()
        {
            if (DataClassInfo != null)
            {
                if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(DataClassInfo.ClassName, "Read"))
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
