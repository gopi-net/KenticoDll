using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions;
using Bluespire.Emerge.Components.EventsCalendar.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class MonthlyScheduleBuilder : ScheduleBuilder
    {
        public MonthlyScheduleBuilder(EventSchedule schedule)
            : base(schedule)
        {
        }

        public override TemporalExpression Create(TemporalExpression excludedDates)
        {
            var union = new UnionTE();

            var monthlyIntervals = EventsCalendarHelper.GetFlags(_schedule.MonthlyIntervalOptions);
            var daysOfWeek = EventsCalendarHelper.GetFlags(_schedule.DaysOfWeekOptions);
            foreach (MonthlyInterval monthlyInterval in monthlyIntervals)
            {
                foreach (DaysOfWeek dayOfWeek in daysOfWeek)
                {
                    var dayInMonth = new MonthTE(1, _schedule.StartDate, dayOfWeek, monthlyInterval);
                    union.Add(dayInMonth);
                }
            }


            return new DifferenceTE(union, excludedDates); 
        }
    }
}
