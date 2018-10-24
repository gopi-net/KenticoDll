using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.Donation.Pages
{
    public class DonationDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.DONATION_DATAVIEWITEMPAGE;
            Module = Constants.Modules.Donation;
            base.OnInit(e);
        }
    }
}
