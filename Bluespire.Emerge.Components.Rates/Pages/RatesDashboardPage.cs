using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.Rates.Pages
{
    public class RatesDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = RatesConstants.RATES_DASHBOARDPAGE;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
    }
}
