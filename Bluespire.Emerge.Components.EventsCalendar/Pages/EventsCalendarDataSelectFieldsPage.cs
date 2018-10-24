using System;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsCalendarDataSelectFieldsPage : EmergeDataSelectFieldsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.EVENTSCALENDAR_DATASELECTFIELDSPAGE;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }
    }
}
