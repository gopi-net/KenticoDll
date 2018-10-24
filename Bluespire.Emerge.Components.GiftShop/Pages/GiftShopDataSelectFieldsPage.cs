﻿using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.GiftShop.Pages
{
    public class GiftShopDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = GiftShopConstants.GIFTSHOP_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.GiftShop;
            base.OnInit(e);
        }
    }
}