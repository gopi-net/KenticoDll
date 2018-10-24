using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.Rates
{
    public class RatesDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = RatesConstants.RATES_DATAVIEWITEMPAGE;
            Module = Constants.Modules.Rates;
            base.OnInit(e);
        }
    }
}
