using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Web.Pages;
using CMS.DataEngine;
using CMS.UIControls;

namespace Bluespire.Emerge.Components.EventsCalendar.Pages
{
    public class EventsCalendarDataEditItemPage : EmergeDataEditItemPage
    {
        protected override void OnInit(EventArgs e)
        {
            PageName = Constants.EVENTSCALENDAR_DATAEDITITEMPAGE;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }
        protected override void setBreadCrumb()
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            string currentItem = string.Empty;
            if (ItemID > 0)
                currentItem = GetString("customtable.data.Edititem");
            else
                currentItem = GetString("customtable.data.NewItem");

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

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = dci.ClassDisplayName + " List",
                RedirectUrl = ListPage + "?customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : String.Empty)
            });

            PageBreadcrumbs.AddBreadcrumb(new BreadcrumbItem
            {
                Text = currentItem
            });

        }
    }
}
