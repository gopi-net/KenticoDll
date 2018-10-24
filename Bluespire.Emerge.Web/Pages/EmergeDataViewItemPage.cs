using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Web.Pages.BasePages;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.Membership;
using CMS.CustomTables;

namespace Bluespire.Emerge.Web.Pages
{
    /// <summary>
    /// Base class for data view item page.
    /// </summary>
    public class EmergeDataViewItemPage : EmergeModalPage
    {

        private int? customTableID;
        /// <summary>
        /// Gets the id of the current custom table.
        /// </summary>
        protected int CustomTableID
        {
            get
            {
                if (!customTableID.HasValue)
                    customTableID = QueryHelper.GetInteger("customtableid", 0);
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
        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// OnPageLoad method. This method should be called from page_load event in the page.
        /// </summary>
        protected void OnPageLoad()
        {
            CurrentMaster.Title.TitleText = GetString("customtable.data.viewitemtitle");
            Page.Title = CurrentMaster.Title.TitleText;

            

            // Get custom table item id
            int itemId = QueryHelper.GetInteger("itemid", 0);

            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            // Set edited object
            EditedObject = dci;

            if (dci != null)
            {
                // Check 'Read' permission
                if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                    !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
                {
                    RedirectToAccessDenied("cms.customtables", "Read");
                }
            }
        }

        /// <summary>
        /// Returns the current custom table item.
        /// </summary>
        /// <returns></returns>
        protected CustomTableItem GetCustomTableItem()
        {
            CustomTableItem item = null;
           
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            if (dci != null)
            {
                int itemId = QueryHelper.GetInteger("itemid", 0);
                item = CustomTableItemProvider.GetItem(itemId, dci.ClassName);
            }
            return item;
        }
    }
}
