using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsCalendarListPage : EmergeListPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.EVENTSCALENDAR_LISTPAGE;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }
    }
}
