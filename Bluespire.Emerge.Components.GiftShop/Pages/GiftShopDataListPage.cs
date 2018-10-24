using System;
using System.Collections.Generic;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Web.Pages;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.GiftShop.Pages
{
    public class GiftShopDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = GiftShopConstants.GIFTSHOP_DATALISTPAGE;
            Module = Constants.Modules.GiftShop;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(GiftShopConstants.STRINGCODE_GIFTSHOPHOME),
                RedirectUrl = GiftShopConstants.PAGEURL_GIFTSHOP_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(GiftShopConstants.STRINGCODE_GIFTSHOPHOME),
                RedirectUrl = GiftShopConstants.PAGEURL_GIFTSHOP_DASHBOARD
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
