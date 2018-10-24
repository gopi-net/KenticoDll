using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.Rates
{
    public class RatesDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = RatesConstants.RATES_DATALISTPAGE;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(RatesConstants.STRINGCODE_RATESHOME),
                RedirectUrl = RatesConstants.PAGEURL_RATES_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(RatesConstants.STRINGCODE_RATESHOME),
                RedirectUrl = RatesConstants.PAGEURL_RATES_DASHBOARD
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
