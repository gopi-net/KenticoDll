﻿using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.CheerCard
{
    public class CheerCardDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CheerCardConstants.CHEERCARD_DATAVIEWITEMPAGE;
            Module = Constants.Modules.CheerCard;
            base.OnInit(e);
        }
    }
}
