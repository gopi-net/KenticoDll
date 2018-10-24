using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface IEventService
    {
        /// <summary>
        /// Gets the event by event ID.
        /// </summary>
        /// <param name="eventID">item id of the event to be retrieved.</param>
        /// <returns>returns the event object.</returns>
        Event GetEventByEventID(int eventID);

        /// <summary>
        /// Gets the event by occurence ID.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>returns the event object.</returns>
        Event GetEventByOccurenceID(int occurenceID);

        /// <summary>
        /// Gets the event by schedule ID.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>returns the event object.</returns>
        Event GetEventByScheduleID(int scheduleID);

        /// <summary>
        /// Deletes the event by event ID
        /// </summary>
        /// <param name="eventID">ID of the event</param>
        void DeleteEventByEventID(int eventID);

        bool IsCartEnabled();

    }
}
