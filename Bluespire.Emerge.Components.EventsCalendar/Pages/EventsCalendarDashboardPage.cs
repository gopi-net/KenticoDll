using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsCalendarDashboardPage : EmergeToolsPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.EVENTSCALENDAR_DASHBOARDPAGE;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }
    }
}
