using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using System.Data;
using CMS.Helpers;
using CMS.CustomTables;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class EventService : IEventService
    {

        public Event GetEventByEventID(int eventID)
        {
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(eventID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTS, SiteContext.CurrentSiteName));
            Event eventItem = item.ToEvent();
            return eventItem;
        }

        public Event GetEventByOccurenceID(int occurenceID)
        {
            int scheduleID = Convert.ToInt32(CustomTableDataHelper.GetFieldValue(occurenceID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID));
            int eventID = Convert.ToInt32(CustomTableDataHelper.GetFieldValue(scheduleID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName), EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID));
            Event eventItem = GetEventByEventID(eventID);
            return eventItem;
        }

        public Event GetEventByScheduleID(int scheduleID)
        {
            int eventID = Convert.ToInt32(CustomTableDataHelper.GetFieldValue(scheduleID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName), EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID));
            Event eventItem = GetEventByEventID(eventID);
            return eventItem;
        }

        public void DeleteEventByEventID(int eventID)
        {
            try
            {
                EventsCalendarHelper._factory.GetTypeInstance<IEventScheduleService>().DeleteEventSchedulesByEventID(eventID);
                CustomTableDataHelper.DeleteCustomTableItem(eventID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTS, SiteContext.CurrentSiteName));
            }
            catch
            {
                throw;
            }
        }

        public bool IsCartEnabled()
        {
            string customTableName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTCARTCONFIGURATION, SiteContext.CurrentSiteName);
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, string.Empty, "ItemModifiedBy DESC");
            bool result = true;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                result = ValidationHelper.GetBoolean(ds.Tables[0].Rows[0][EventsConstants.FIELDS_EVENTCARTCONFIGURTAION_ENABLECART], true);
            }
            return result;
        }
    }
}
