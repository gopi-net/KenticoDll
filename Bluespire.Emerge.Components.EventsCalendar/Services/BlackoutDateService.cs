using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using CMS.DataEngine;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.CommonService;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class BlackoutDateService : IBlackoutDateService
    {
        public IEnumerable<BlackOutDate> GetBlackOutDates(BlackOutDateFilter filter)
        {
            string whereCondition = string.Empty;
            switch (filter)
            {
                case BlackOutDateFilter.BookingAllowed:
                    whereCondition = EventsConstants.FIELDS_BLACKOUTDATES_ALLOWBOOKING + "= 1"; 
                    break;
                case BlackOutDateFilter.BookingNotAllowed:
                    whereCondition = EventsConstants.FIELDS_BLACKOUTDATES_ALLOWBOOKING + "= 0";
                    break;
            }
            List<BlackOutDate> list = new List<BlackOutDate>();

            
            DataSet ds = CustomTableItemProvider.GetItems(String.Format(EventsConstants.CUSTOMTABLE_EVENT_BLACKOUTDATES, SiteContext.CurrentSiteName), whereCondition, string.Empty);
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    BlackOutDate date = row.ToBlackOutDate();
                    if (date != null)
                        list.Add(date);
                }
                
            }
            return list;
        }


        public bool HasOccurencesOnDate(DateTime date)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName);
            string whereCondition = EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE + "=" + "CONVERT(DATETIME, '" + date.ToString() + "', 101)";
            
            DataSet itemsDS = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);

            if (!DataHelper.DataSourceIsEmpty(itemsDS))
                return true;
            
            return false;
        }


        public IEnumerable<BlackOutDate> GetBlackOutDatesInOccurences(List<EventOccurence> occurenceList)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (EventOccurence occurence in occurenceList)
                dates.Add(occurence.OccurenceDate);
            List<BlackOutDate> blackOutDates = GetBlackOutDatesInDates(dates).ToList<BlackOutDate>();
            return blackOutDates;
        }


        public IEnumerable<BlackOutDate> GetBlackOutDatesInDates(List<DateTime> occurenceList)
        {
            List<BlackOutDate> blackOutDateList = GetBlackOutDates(BlackOutDateFilter.Nofilter).ToList<BlackOutDate>();
            List<BlackOutDate> dates = new List<BlackOutDate>();
            foreach (BlackOutDate date in blackOutDateList)
            {
                if (occurenceList.Count(x => x.Equals(date.Date)) > 0)
                    dates.Add(date);
            }
            return dates;
        }
    }
}
