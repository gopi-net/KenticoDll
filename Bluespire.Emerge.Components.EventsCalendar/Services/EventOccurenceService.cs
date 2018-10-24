using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Reflection;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    /// <summary>
    /// Service class for processing event occurences.
    /// </summary>
    public class EventOccurenceService : IEventOccurenceService
    {
        /// <summary>
        /// Saves the occurences for the event schedule.
        /// </summary>
        /// <param name="eventOccurences">list of occurences to be saved.</param>
        /// <param name="mode">mode, whether insert or update</param>
        /// <returns>true, if saved successfully, else false.</returns>
        public bool SaveEventOccurences(IList<EventOccurence> eventOccurences)
        {
            try
            {
                if (eventOccurences != null && eventOccurences.Count == 0)
                    return true;

                List<string> eventOccurenceFields = EmergeStaticHelper.GetFieldsOf<EventOccurence>();
                
                foreach (EventOccurence occurence in eventOccurences)
                {
                    IDictionary<string, object> tableData = getTableData(eventOccurenceFields, occurence);

                    int id = occurence.OccurenceID;
                    CustomTableDataHelper.SaveCustomTableItem(String.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), ref id, tableData);
                }
                return true;
            }
            catch(Exception ex)
            {
                EmergeLogWriter.WriteError("EventSchedule", EventCode.EMERGE_EVENTSOCCURENCES_SAVE, ex.ToString());
                throw;
            }
        }


        /// <summary>
        /// Gets the list of event occurences by scheduleID.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>returns the list of event occurences.</returns>
        public IEnumerable<EventOccurence> GetOccurencesByScheduleID(int scheduleID)
        {
            List<EventOccurence> list = new List<EventOccurence>();
            DataSet ds = CustomTableItemProvider.GetItems(string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID + " = " + scheduleID.ToString(), string.Empty);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                EventOccurence occurence = dr.ToEventOccurence();
                list.Add(occurence);
            }
            
            return list;

        }

        /// <summary>
        /// Gets the event occurence by id.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>returns the event occurence object.</returns>
        public EventOccurence GetEventOccurenceByID(int occurenceID)
        {
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(occurenceID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName));
            EventOccurence occurence = item.ToEventOccurence();
            return occurence;
        }

        /// <summary>
        /// Deletes the event occurence by ID.
        /// </summary>
        /// <param name="occurenceID"></param>
        /// <returns></returns>
        public bool DeleteEventOccurenceByID(int occurenceID)
        {
            EventsCalendarHelper._factory.GetTypeInstance<ISessionService>().DeleteSessionsByOccurenceID(occurenceID);
            EventsCalendarHelper.DeleteRegistrationsByOccurrenceID(occurenceID);
            return CustomTableDataHelper.DeleteCustomTableItem(occurenceID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName));
        }
        public bool DeleteEventOccurenceByIDForFinalizeSchedule(int occurenceID)
        {
            EventsCalendarHelper._factory.GetTypeInstance<ISessionService>().DeleteSessionsByOccurenceID(occurenceID);            
            return CustomTableDataHelper.DeleteCustomTableItem(occurenceID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName));
        }
        /// <summary>
        /// Gets the event occurence object by schedule id and occurence date.
        /// </summary>
        /// <param name="occurenceDate">date of the event occurence.</param>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>Event occurence object.</returns>
        public EventOccurence GetEventOccurenceByScheduleAndOccurenceID(DateTime occurenceDate, int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE + "=" + "CONVERT(DATETIME, '" + occurenceDate.ToString() + "', 101) AND " + EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID + " = " + scheduleID;
            DataSet occurenceDS = CustomTableDataHelper.GetCustomTableItemsByCondition(String.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), whereCondition, string.Empty);
            if (!DataHelper.DataSourceIsEmpty(occurenceDS))
            {
                DataRow row = occurenceDS.Tables[0].Rows[0];
                EventOccurence occurence = row.ToEventOccurence();
                return occurence;
            }
            return null;
        }

        public void DeleteEventScheduleOccurences(int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID + " = " + scheduleID.ToString();
            DataSet itemDS = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), whereCondition, string.Empty);

            foreach (DataRow dr in itemDS.Tables[0].Rows)
            {
                int itemID = Convert.ToInt32(dr[EventsConstants.FIELDS_EVENTOCCURENCES_ITEMID]);
                DeleteEventOccurenceByID(itemID);
            }
        }

        public IEnumerable<EventOccurence> BuildOccurencesForSchedule(int scheduleID, List<DateTime> excludedDates)
        {
            IEventScheduleService scheduleService = EventsCalendarHelper._factory.GetTypeInstance<IEventScheduleService>();
            EventSchedule schedule = scheduleService.GetEventScheduleByID(scheduleID);

            ScheduleBuilder scheduleBuilder = ScheduleBuilderFactory.GetScheduleBuilder(schedule);
            IEnumerable<EventOccurence> eventOccurrences = scheduleBuilder.BuildOccurences(excludedDates);

            return eventOccurrences.OrderBy(z => z.OccurenceDate);
        }

        public IEnumerable<EventOccurence> GetOccurencesForRange(DateRange range)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName);
            string whereCondition = string.Format("{0} BETWEEN {1} AND {2}", EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE, range.StartDateTime.ToString("d"), range.EndDateTime.ToString("d"));
            DataSet itemDS = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);
            List<EventOccurence> occurences = new List<EventOccurence>();
            if (!DataHelper.DataSourceIsEmpty(itemDS))
            {
                foreach (DataRow row in itemDS.Tables[0].Rows)
                {
                    occurences.Add(row.ToEventOccurence());
                }
            }
            return occurences;
        }

        public IEnumerable<EventOccurence> GetOccurencesForMonth(int month, int year)
        {
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            
            DateRange range = new DateRange(startDate, endDate);
            return GetOccurencesForRange(range);
        }

        public IEnumerable<EventOccurence> GetOccurencesForCurrentWeek()
        {
            DateTime startDate = DateTime.Now.AddDays(-((int)DateTime.Today.DayOfWeek));
            DateTime endDate = startDate.AddDays(7).AddSeconds(-1);

            DateRange range = new DateRange(startDate, endDate);
            return GetOccurencesForRange(range);
        }

        public IEnumerable<EventOccurence> GetOccurencesForToday()
        {
            DateRange range = new DateRange(DateTime.Now, DateTime.Now);
            return GetOccurencesForRange(range);
        }
    

        #region Private Methods

        private IDictionary<string, object> getTableData(List<string> eventOccurenceFields, EventOccurence occurence)
        {
            IDictionary<string, object> tableData = new Dictionary<string, object>();
            foreach (string field in eventOccurenceFields)
            {
                string propertyName = EmergeStaticHelper.GetPropertyNameByCustomAttribute<EventOccurence, CustomTableFieldAttribute>(x => x.FieldName == field)[0];
                string value = Convert.ToString(typeof(EventOccurence).GetProperty(propertyName).GetValue(occurence, null));
                KeyValuePair<string, object> data;

                if (!String.IsNullOrEmpty(value))
                    data = new KeyValuePair<string, object>(field, value);
                else
                    data = new KeyValuePair<string, object>(field, string.Empty);

                tableData.Add(data);
            }
            return tableData;
        }

        #endregion
    }
}
