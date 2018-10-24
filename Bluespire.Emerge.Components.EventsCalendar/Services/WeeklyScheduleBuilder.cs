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
    public class WeeklyScheduleBuilder : ScheduleBuilder
    {
        public WeeklyScheduleBuilder(EventSchedule schedule)
            : base(schedule)
        {
        }

        public override TemporalExpression Create(TemporalExpression excludedDates)
        {
            var union = new UnionTE();
            var daysOfWeek = EventsCalendarHelper.GetFlags(_schedule.DaysOfWeekOptions);

            foreach (DayOfWeek day in daysOfWeek)
            {
                var dayOfWeek = new DayOfWeekTE(day);
                union.Add(dayOfWeek);
            }

            return new DifferenceTE(union, excludedDates);
        }

        
    }
}
