using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.Donation.Pages
{
    public class DonationDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.DONATION_DASHBOARDPAGE;
            Module = Constants.Modules.Donation;
            base.OnInit(e);
        }
    }
}
