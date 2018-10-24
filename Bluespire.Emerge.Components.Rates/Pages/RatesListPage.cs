using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Rates
{
    public class RatesListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = RatesConstants.RATES_LISTPAGE;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
    }
}
