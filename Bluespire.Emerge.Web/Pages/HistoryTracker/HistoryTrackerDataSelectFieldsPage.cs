using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.HistoryTracker
{
    public class HistoryTrackerDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.HISTORYTRACKER_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.HistoryTracker;
            base.OnInit(e);
        }
    }
}
