using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.Donation.Pages
{
    public class DonationDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.DONATION_DATALISTPAGE;
            Module = Constants.Modules.Donation;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(DonationConstants.STRINGCODE_DONATIONHOME),
                RedirectUrl = DonationConstants.PAGEURL_DONATION_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(DonationConstants.STRINGCODE_DONATIONHOME),
                RedirectUrl = DonationConstants.PAGEURL_DONATION_DASHBOARD
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
