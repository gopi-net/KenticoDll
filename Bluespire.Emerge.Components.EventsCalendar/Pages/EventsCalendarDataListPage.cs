using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsCalendarDataListPage : EmergeDataListPage
    {
        protected string ViewItemPage { get; set; }

        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.EVENTSCALENDAR_DATALISTPAGE;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            BreadcrumbItem item = new BreadcrumbItem
            {
                Text = GetString(EventsConstants.STRINGCODE_EVENTHOME),
                RedirectUrl = EventsConstants.PAGEURL_EVENTS_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);

            item = new BreadcrumbItem
            {
                Text = GetString(EventsConstants.STRINGCODE_EVENTHOME),
                RedirectUrl = EventsConstants.PAGEURL_EVENTS_DASHBOARD
            };
            PageBreadcrumbs.AddBreadcrumb(item);


            item = new BreadcrumbItem
            {
                Text = DataClassInfo.ClassDisplayName
            };
            PageBreadcrumbs.AddBreadcrumb(item);
        }
    }
}
