using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.HistoryTracker
{
    public class HistoryTrackerListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.HISTORYTRACKER_LISTPAGE;
            Module = Constants.Modules.HistoryTracker;
            base.OnInit(e);
        }
    }
}
