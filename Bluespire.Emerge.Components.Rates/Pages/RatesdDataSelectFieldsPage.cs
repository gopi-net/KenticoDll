using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Rates
{
    public class RatesDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = RatesConstants.RATES_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
    }
}
