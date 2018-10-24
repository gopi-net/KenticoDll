using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.CheerCard
{
    public class CheerCardDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = CheerCardConstants.CHEERCARD_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.CheerCard;
            base.OnInit(e);
        }
    }
}
