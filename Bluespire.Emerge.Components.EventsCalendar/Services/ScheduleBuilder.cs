using System;
using System.Collections.Generic;
using System.Linq;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public abstract class ScheduleBuilder
    {
        protected EventSchedule _schedule;
        protected ScheduleBuilder(EventSchedule schedule)
        {
            _schedule = schedule;
        }
       
        public abstract TemporalExpression Create(TemporalExpression excludedDates);

        public IEnumerable<EventOccurence> BuildOccurences(IEnumerable<DateTime> excludedDates)
        {
            var union = new UnionTE();
            if (excludedDates != null && excludedDates.Count() > 0)
            {
                foreach(DateTime date in excludedDates)
                {
                    union.Add(new DateTE(date));
                }
            }
            TemporalExpression = Create(union);
            var dateRange = new DateRange(_schedule.StartDate, _schedule.EndDate);

            List<EventOccurence> occurences = new List<EventOccurence>();

            foreach (DateTime date in this.Occurrences(dateRange))
            {
                EventOccurence existingOccurence = EventsCalendarHelper.GetEventOccurenceByScheduleAndOccurenceDate(date, _schedule.ScheduleID);
                EventOccurence occurence = new EventOccurence
                {
                    ScheduleID = _schedule.ScheduleID,
                    OccurenceDate = date,
                    StartTime = _schedule.StartTime,
                    EndTime = _schedule.EndTime,
                    RegistrationLimit = _schedule.RegistrationLimit,
                    OccurenceID = existingOccurence == null ? 0 : existingOccurence.OccurenceID,
                    Location = _schedule.Location

                };
                occurences.Add(occurence);
            }
            return occurences;
        }

        public TemporalExpression TemporalExpression { get; private set; }

        public IEnumerable<EventOccurence> GetOccurences()
        {
            return null;
        }

        public EventOccurence GetPreviousOccurence(DateTime date)
        {
            return null;
        }

        public EventOccurence GetNextOccurence(DateTime date)
        {
            return null;
        }

        public bool IsOccurring(EventSchedule schedule, DateTime date)
        {
            return false;
        }

        /// <summary>
        /// Return true if the date occurs in the schedule.
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public bool IsOccurring(DateTime aDate)
        {
            return TemporalExpression.Includes(aDate);
        }

        /// <summary>
        /// Return all occurrences within the given date range.
        /// </summary>
        /// <param name="during">DateRange</param>
        /// <returns></returns>
        public IEnumerable<DateTime> Occurrences(DateRange during)
        {
            return EachDay(during.StartDateTime, during.EndDateTime).Where(IsOccurring);
        }

        /// <summary>
        /// Return each calendar day in the date range in ascending order
        /// </summary>
        /// <param name="from"></param>
        /// <param name="through"></param>
        /// <returns></returns>
        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime through)
        {
            for (var day = from.Date; day.Date <= through.Date; day = day.AddDays(1))
                yield return day;
        }
        
    }
}
