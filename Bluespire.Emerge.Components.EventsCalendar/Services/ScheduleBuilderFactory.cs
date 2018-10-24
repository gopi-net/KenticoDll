using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class ScheduleBuilderFactory
    {
        public static ScheduleBuilder GetScheduleBuilder(EventSchedule schedule)
        {
            ScheduleBuilder scheduleBuilder = null;
            switch (schedule.FrequencyType)
            {
                case FrequencyType.Single:
                    scheduleBuilder = new SingleScheduleBuilder(schedule);
                    break;
                case FrequencyType.Weekly:
                    scheduleBuilder = new WeeklyScheduleBuilder(schedule);
                    break;
                case FrequencyType.Monthly:
                    scheduleBuilder = new MonthlyScheduleBuilder(schedule);
                    break;
            }
            return scheduleBuilder;
        }
    }
}
