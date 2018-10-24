using System;
using System.Collections.Generic;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using CMS.SiteProvider;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.CommonService;
using CMS.CustomTables;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class SessionService : ISessionService
    {
        public IList<EventSession> GetSessionsByOccurence(int occurenceID)
        {
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(occurenceID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName));
            EventOccurence occurence = item.ToEventOccurence();

            string whereCondition = EventsConstants.FIELDS_EVENTSESSIONS_OCCURENCEID + " = " + occurenceID.ToString() + " OR " + EventsConstants.FIELDS_EVENTSESSIONS_SCHEDULEID + " = "  + occurence.ScheduleID.ToString();

            return GetSessionsByCondition(whereCondition);
        }

        public IList<EventSession> GetSessionsBySchedule(int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTSESSIONS_SCHEDULEID + " = " + scheduleID.ToString();

            return GetSessionsByCondition(whereCondition);
        }

        public void DeleteSessionsByOccurenceID(int occurenceID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTSESSIONS_OCCURENCEID + " = " + occurenceID.ToString();
            DeleteSessions(whereCondition);
        }

        public void DeleteSessionsByScheduleID(int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTSESSIONS_SCHEDULEID + " = " + scheduleID.ToString();
            DeleteSessions(whereCondition);
        }

        private void DeleteSessions(string whereCondition)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSESSIONS, SiteContext.CurrentSiteName);
            DataSet items = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);

            foreach (DataRow item in items.Tables[0].Rows)
            {
                int itemID = Convert.ToInt32(item[EventsConstants.FIELDS_EVENTSESSIONS_SESSIONID]);
                CustomTableDataHelper.DeleteCustomTableItem(itemID, className);
            }
        }

        public void DeleteSessionByID(int sessionID)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSESSIONS, SiteContext.CurrentSiteName);
            CustomTableDataHelper.DeleteCustomTableItem(sessionID, className);
        }


        public IList<EventSession> GetSessionsByCondition(string whereCondition)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSESSIONS, SiteContext.CurrentSiteName);
            DataSet sessionsDS = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);
            
            IList<EventSession> list = new List<EventSession>();
            foreach (DataRow row in sessionsDS.Tables[0].Rows)
            {
                EventSession session = row.ToEventSession();
                list.Add(session);
            }

            return list;
        }
    }
}
