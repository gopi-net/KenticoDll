using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.StaffDirectory.Pages
{
    public class StaffDirectoryDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.STAFF_DIRECTORY_DATALISTPAGE;
            Module = Constants.Modules.StaffDirectory;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
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


            item = new BreadcrumbItem
            {
                Text = DataClassInfo.ClassDisplayName
            };
            PageBreadcrumbs.AddBreadcrumb(item);
        }
    }
}
