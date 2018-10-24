using System;
using CMS.UIControls;
using Bluespire.Emerge.Web.Pages.BasePages;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Core;
using CMS.Base.Web.UI.ActionsConfig;
using CMS.Base.Web.UI;

namespace Bluespire.Emerge.Web.Pages
{
    /// <summary>
    /// Base class for Edit item or new item page. All the pages where data is either edited or added should inherit from this class.
    /// </summary>
    [Help("custom_tables_edit_item", "helpTopic")]
    [UIElement(ModuleName.CUSTOMTABLES, "CustomTables")]
    public class EmergeDataEditItemPage : EmergeToolsPage
    {
        private bool? isSiteManager;
        /// <summary>
        /// Gets whether this page is being accessed from site manager.
        /// </summary>
        protected bool IsSiteManager
        {
            get
            {
                if (!isSiteManager.HasValue)
                    isSiteManager = (EmergeQueryHelper.GetInteger("sm", 0) == 1);
                return isSiteManager.Value;
            }
        }

        private int? itemID;
        /// <summary>
        /// Gets the id of the current selected item.
        /// </summary>
        protected int ItemID
        {
            get
            {
                if (!itemID.HasValue)
                    itemID = EmergeQueryHelper.GetInteger("itemid", 0);
                return itemID.Value;
            }
        }

        private int? customTableID;
        /// <summary>
        /// Gets the id of the current custom table.
        /// </summary>
        protected int CustomTableID
        {
            get
            {
                if (!customTableID.HasValue)
                    customTableID = EmergeQueryHelper.GetInteger("customtableid", 0);
                if (customTableID == 0)
                {
                    string customTableName = EmergeQueryHelper.GetString("customtablename", string.Empty);
                    if (!String.IsNullOrEmpty(customTableName))
                    {
                        string className = string.Format(customTableName, EmergeCMSContext.CurrentSiteName);
                        DataClassInfo classInfo = CustomTableDataHelper.GetCustomTableClassInfo(className);
                        customTableID = classInfo.ClassID;
                    }
                }
                return customTableID.Value;
            }
        }

        /// <summary>
        /// Gets or sets whether access is granted to this page.
        /// </summary>
        protected bool AccessGranted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list page url.
        /// </summary>
        protected string ListPage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the new item page url.
        /// </summary>
        protected string NewItemPage
        {
            get;
            set;
        }

        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            RequireSite = false;
            base.OnInit(e);
        }

        /// <summary>
        /// On page load method. This method should be called from page_load event in the derived class.
        /// </summary>
        protected void OnPageLoad()
        {
            AccessGranted = true;
            CurrentMaster.Title.HelpTopicName = "custom_tables_edit_item";
            CurrentMaster.Title.HelpName = "helpTopic";


            if (CustomTableID > 0)
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
                // Set edited object
                EditedObject = dci;

                // If class exists
                if (dci != null)
                {
                    // Edit item
                    if (ItemID > 0)
                    {
                        // Check 'Read' permission
                        if (CheckForPermissions())
                        {
                            ShowError(String.Format(GetString("customtable.permissiondenied.read"), dci.ClassName));
                            AccessGranted = false;
                        }
                        
                        if (!IsSiteManager)
                        {
                            CurrentMaster.Title.TitleText = GetString("customtable.data.edititemtitle");
                            //CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/edititem.png");
                        }
                    }
                    // New item
                    else
                    {
                        if (!IsSiteManager)
                        {
                            CurrentMaster.Title.TitleText = GetString("customtable.data.newitemtitle");
                           // CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/newitem24.png");
                        }
                    }

                    // Set custom pages
                    if (dci.ClassListPageURL != String.Empty)
                    {
                        ListPage = dci.ClassListPageURL;
                    }
                    else if (dci.ClassNewPageURL != String.Empty)
                    {
                        NewItemPage = dci.ClassNewPageURL;
                    }

                    if (QueryHelper.GetString("saved", String.Empty) != String.Empty)
                    {
                        // If this was creating of new item show the link again
                        if ((QueryHelper.GetString("new", String.Empty) != String.Empty))
                        {
                            CurrentMaster.HeaderActions.AddAction(new HeaderAction
                            {
                                Text = GetString("customtable.data.createanother"),
                                RedirectUrl = ResolveUrl(NewItemPage + "?new=1&customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : String.Empty))
                            });
                        }
                    }
                    setBreadCrumb();
                }
            }
        }




        protected virtual void setBreadCrumb()
        {
            DataClassInfo classInfo = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = classInfo.ClassDisplayName,
                RedirectUrl = ListPage + "?customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : String.Empty)
            });
            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = ItemID > 0 ? GetString("customtable.data.Edititem") : GetString("customtable.data.NewItem")
            });

            // Do not include type as breadcrumbs suffix
            UIHelper.SetBreadcrumbsSuffix("");
        }
        /// <summary>
        /// Checks if the current user has the required permissions to access the resources on this page.
        /// </summary>
        /// <returns></returns>
        protected bool CheckForPermissions()
        {
            if (CustomTableID > 0)
            {
                if (ItemID > 0)
                {
                    DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
                    if (!EmergeCMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                                   !EmergeCMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
