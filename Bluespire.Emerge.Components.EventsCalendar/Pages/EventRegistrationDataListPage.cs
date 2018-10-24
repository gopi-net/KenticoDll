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
    public class EventRegistrationDataListPage : EventsCalendarDataListPage
    {
        protected override void OnInit(EventArgs e)
        {
            if (!EmergeStaticHelper.GridActions.ContainsKey("deleteregistrations"))
                EmergeStaticHelper.GridActions.Add("deleteregistrations", () => new GridRegistrationDeleteAction());
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
