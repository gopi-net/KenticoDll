using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.GiftShop.Pages
{
    public class GiftShopDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = GiftShopConstants.GIFTSHOP_DATAVIEWITEMPAGE;
            Module = Constants.Modules.GiftShop;
            base.OnInit(e);
        }
    }
}
