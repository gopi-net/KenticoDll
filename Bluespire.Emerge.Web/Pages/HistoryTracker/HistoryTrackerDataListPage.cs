using System;

using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.HistoryTracker
{
    public class HistoryTrackerDataListPage : EmergeDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.HISTORYTRACKER_DATALISTPAGE;
            Module = Constants.Modules.HistoryTracker;
            base.OnInit(e);
        }
    }
}
