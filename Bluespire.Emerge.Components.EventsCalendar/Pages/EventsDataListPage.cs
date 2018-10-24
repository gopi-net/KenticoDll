using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.GridActions;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsDataListPage : EventsCalendarDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            if (!EmergeStaticHelper.GridActions.ContainsKey("deleteevents"))
                EmergeStaticHelper.GridActions.Add("deleteevents", () => new GridEventDeleteAction());

            NewItemPage = EventsConstants.PAGEURL_DATA_EDITITEM;
            ListPage = EventsConstants.PAGEURL_LIST_EVENTSCALENDAR;
            SelectFieldsPage = EventsConstants.PAGEURL_DATA_SELECTFIELDS; 
            ViewItemPage = EventsConstants.PAGEURL_DATA_VIEWITEM;
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
