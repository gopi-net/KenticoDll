﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.CheerCard.Pages
{
    public class CheerCardDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CheerCardConstants.CHEERCARD_DASHBOARDPAGE;
            Module = Constants.Modules.CheerCard;
            base.OnInit(e);
        }
    }
}
