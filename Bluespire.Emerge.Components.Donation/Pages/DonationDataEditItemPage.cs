using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.Donation.Pages
{
    public class DonationDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.DONATION_DATAEDITITEMPAGE;
            Module = Constants.Modules.Donation;
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
