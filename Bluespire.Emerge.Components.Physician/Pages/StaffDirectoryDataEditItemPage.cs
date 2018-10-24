using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Web.Pages;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.StaffDirectory.Pages
{
    public class StaffDirectoryDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.STAFF_DIRECTORY_DATAEDITITEMPAGE;
            Module = Constants.Modules.StaffDirectory;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            string currentItem = string.Empty;
            if (ItemID > 0)
                currentItem = GetString("customtable.data.Edititem");
            else
                currentItem = GetString("customtable.data.NewItem");

            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(StaffDirectoryConstants.STRINGCODE_STAFFDIRECTORYHOME),
                RedirectUrl = StaffDirectoryConstants.PAGEURL_STAFFDIRECTORY_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);
            item = new BreadcrumbItem
            {
                Text = GetString(StaffDirectoryConstants.STRINGCODE_STAFFDIRECTORYHOME),
                RedirectUrl = StaffDirectoryConstants.PAGEURL_STAFFDIRECTORY_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = dci.ClassDisplayName + " List",
                RedirectUrl = ListPage + "?customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : String.Empty)
            });

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = currentItem
            });

        }
    }
}
