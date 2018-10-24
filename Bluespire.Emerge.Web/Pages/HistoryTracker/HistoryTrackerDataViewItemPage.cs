using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.HistoryTracker
{
    public class HistoryTrackerDataViewItemPage : EmergeDataViewItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.HISTORYTRACKER_DATAVIEWITEMPAGE;
            Module = Constants.Modules.HistoryTracker;
            base.OnInit(e);
        }
    }
}
