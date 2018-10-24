using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface IEventScheduleService
    {
        EventSchedule GetEventScheduleByID(int scheduleID);

        EventSchedule GetEventScheduleByOccurenceID(int occurenceID);

        void UpdateScheduleDateFields(int scheduleID);

        void DeleteEventScheduleByID(int scheduleID);

        void DeleteEventSchedulesByEventID(int eventID);

        IEnumerable<EventSchedule> GetEventSchedulesByEventID(int eventID);
    }
}
