using System;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Web.Pages.HistoryTracker
{
    public class HistoryTrackerDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.HISTORYTRACKER_DATAEDITITEMPAGE;
            Module = Constants.Modules.HistoryTracker;
            base.OnInit(e);
        }
    }
}
