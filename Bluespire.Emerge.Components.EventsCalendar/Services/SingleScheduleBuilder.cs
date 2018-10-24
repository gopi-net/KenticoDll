using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class SingleScheduleBuilder : ScheduleBuilder
    {
        public SingleScheduleBuilder(EventSchedule schedule): base(schedule)
        {

        }

        public override TemporalExpression Create(TemporalExpression excludedDates)
        {
            var union = new UnionTE();

            foreach (DateTime date in _schedule.SelectedDates)
            {
                var selectedDate = new DateTE(date);
                union.Add(selectedDate);
            }
            return new DifferenceTE(union, excludedDates);
        }
    }
}
