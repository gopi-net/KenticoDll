using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.Career
{
    public class CareerDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CareerConstants.CAREER_DATALISTPAGE;
            Module = Constants.Modules.Career;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(CareerConstants.STRINGCODE_CAREERHOME),
                RedirectUrl = CareerConstants.PAGEURL_CAREER_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(CareerConstants.STRINGCODE_CAREERHOME),
                RedirectUrl = CareerConstants.PAGEURL_CAREER_DASHBOARD
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
