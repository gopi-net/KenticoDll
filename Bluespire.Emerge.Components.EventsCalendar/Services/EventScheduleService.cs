using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using System.Data;
using Bluespire.Emerge.Common;
using CMS.CustomTables;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class EventScheduleService : IEventScheduleService
    {
        public EventSchedule GetEventScheduleByID(int scheduleID)
        {
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(scheduleID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName));
            EventSchedule schedule = item.ToEventSchedule();
            return schedule;
        }

        public EventSchedule GetEventScheduleByOccurenceID(int occurenceID)
        {
            int scheduleID = Convert.ToInt32(CustomTableDataHelper.GetFieldValue(occurenceID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName), EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID));
            EventSchedule schedule = GetEventScheduleByID(scheduleID);
            return schedule;
        }


        public void UpdateScheduleDateFields(int scheduleID)
        {
            EventSchedule schedule = GetEventScheduleByID(scheduleID);
            IDictionary<string, object> tabledata = new Dictionary<string, object>();
            if (schedule.FrequencyType == FrequencyType.Single)
            {
                List<EventOccurence> occurences = schedule.Occurences;
                occurences = occurences.OrderBy(x => x.OccurenceDate).ToList<EventOccurence>();
                setStartEndDateFromOccurences(occurences, ref tabledata);
                setSelectedDates(occurences, ref tabledata);
            }
            else
                setSelectedDates(null, ref tabledata);
            resetNonRequiredFields(schedule, ref tabledata);
            string scheduleTable = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName);
            CustomTableDataHelper.SaveCustomTableItem(scheduleTable, ref scheduleID, tabledata);
            deleteRelationData(schedule);
            
        }

        private void deleteRelationData(EventSchedule schedule)
        {
            if (!schedule.HasSessions)
            {
                EventsCalendarHelper._factory.GetTypeInstance<ISessionService>().DeleteSessionsByScheduleID(schedule.ScheduleID);
            }
            if (!schedule.IsPaidSchedule)
            {
                EventsCalendarHelper._factory.GetTypeInstance<IDiscountService>().DeleteDiscountsByScheduleID(schedule.ScheduleID);
            }
        }

        private void resetNonRequiredFields(EventSchedule schedule, ref IDictionary<string, object> tableData)
        {
            switch(schedule.FrequencyType)
            {
                case FrequencyType.Single:
                    tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_DAYOFWEEK, string.Empty);
                    tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_WEEKOFMONTH, string.Empty);
                    break;
                case FrequencyType.Weekly:
                    tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_WEEKOFMONTH, string.Empty);
                    break;
            }
            if (!schedule.HasSessions)
            {
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_SESSIONDETAILS, string.Empty);
            }
            if (!schedule.IsPaidSchedule)
            {
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_COSTFORPUBLIC, DBNull.Value);
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_DISCOUNT, string.Empty);
            }
            if (!schedule.NeedRegistrations)
            {
                setRegistrationFieldValues(schedule, ref tableData);  
            }
            else if (!schedule.AllowGroupRegistrations)
            {
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT, DBNull.Value);
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT, DBNull.Value);
            }
            if (schedule.Event.EventType == "OBSERVATION")
            {
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_NEEDREGISTRATIONS, false);
                setRegistrationFieldValues(schedule, ref tableData);
            }

            
        }


        private void setRegistrationFieldValues(EventSchedule schedule, ref IDictionary<string, object> tableData)
        {
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT, DBNull.Value);
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE, DBNull.Value);
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_HASDUPLICATEREGISTRATIONS))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_HASDUPLICATEREGISTRATIONS, false);
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_ALLOWGROUPREGISTRATIONS))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_ALLOWGROUPREGISTRATIONS, false);
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT, DBNull.Value);
            if (!tableData.ContainsKey(EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT))
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT, DBNull.Value);
        }

        private void setStartEndDateFromOccurences(List<EventOccurence> occurences, ref IDictionary<string, object> tableData)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            if (occurences.Count > 0)
            {
                startDate = occurences[0].OccurenceDate;
                endDate = occurences[occurences.Count - 1].OccurenceDate;
            }

            tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_STARTDATE, startDate.ToString());
            tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_ENDDATE, endDate.ToString());
        }

        private void setSelectedDates(List<EventOccurence> occurences, ref IDictionary<string, object> tableData)
        {
            string selectedDates = string.Empty;
            if (occurences != null)
            {
                foreach (EventOccurence occurence in occurences)
                {
                    selectedDates += occurence.OccurenceDate.ToString(Constants.EMERGE_DATEFORMAT) + " | ";
                }
            }
            if (selectedDates.LastIndexOf(" | ") >= 0)
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_SELECTEDDATES, selectedDates.Remove(selectedDates.LastIndexOf(" | ")));
            else
                tableData.Add(EventsConstants.FIELDS_EVENTSCHEDULE_SELECTEDDATES, selectedDates);
        }


        public void DeleteEventScheduleByID(int scheduleID)
        {
            try
            {
                EventsCalendarHelper.DeleteEventScheduleOccurences(scheduleID);
                EventsCalendarHelper.DeleteEventSessionsByScheduleID(scheduleID);
                EventsCalendarHelper.DeleteDiscountDetailsByScheduleID(scheduleID);
                EventsCalendarHelper.DeleteRegistrationsByScheduleID(scheduleID);
                CustomTableDataHelper.DeleteCustomTableItem(scheduleID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName));
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventSchedulesByEventID(int eventID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID + " = " + eventID.ToString();
            DataSet itemDS = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName), whereCondition, string.Empty);

            foreach (DataRow dr in itemDS.Tables[0].Rows)
            {
                int itemID = Convert.ToInt32(dr[EventsConstants.FIELDS_EVENTSCHEDULE_SCHEDULEID]);
                DeleteEventScheduleByID(itemID);
            }
        }

        public IEnumerable<EventSchedule> GetEventSchedulesByEventID(int eventID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID + " = " + eventID.ToString();
            DataSet itemDS = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName), whereCondition, string.Empty);

            List<EventSchedule> scheduleList = new List<EventSchedule>();
            foreach (DataRow dr in itemDS.Tables[0].Rows)
            {
                scheduleList.Add(dr.ToEventSchedule());
            }
            return scheduleList;
        }

    }
}
