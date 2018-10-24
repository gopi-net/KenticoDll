using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.CheerCard
{
    public class CheerCardDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CheerCardConstants.CHEERCARD_DATALISTPAGE;
            Module = Constants.Modules.CheerCard;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(CheerCardConstants.STRINGCODE_CHEERCARDHOME),
                RedirectUrl = CheerCardConstants.PAGEURL_CHEERCARD_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(CheerCardConstants.STRINGCODE_CHEERCARDHOME),
                RedirectUrl = CheerCardConstants.PAGEURL_CHEERCARD_DASHBOARD
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
