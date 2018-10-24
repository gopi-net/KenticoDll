using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface IEventOccurenceService
    {
        /// <summary>
        /// Saves the occurences for the event schedule.
        /// </summary>
        /// <param name="eventOccurences">list of occurences to be saved.</param>
        /// <param name="mode"></param>
        /// <returns></returns>
        bool SaveEventOccurences(IList<EventOccurence> eventOccurences);

        /// <summary>
        /// Gets the list of event occurences by scheduleID.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>returns the list of event occurences.</returns>
        IEnumerable<EventOccurence> GetOccurencesByScheduleID(int scheduleID);

        /// <summary>
        /// Gets the event occurence by id.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>returns the event occurence object.</returns>
        EventOccurence GetEventOccurenceByID(int occurenceID);

        /// <summary>
        /// Deletes the event occurence.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns></returns>
        bool DeleteEventOccurenceByID(int occurenceID);
        bool DeleteEventOccurenceByIDForFinalizeSchedule(int occurenceID);

        /// <summary>
        /// Gets the event occurence object by schedule id and occurence date.
        /// </summary>
        /// <param name="occurenceDate">date of the event occurence.</param>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>Event occurence object.</returns>
        EventOccurence GetEventOccurenceByScheduleAndOccurenceID(DateTime occurenceDate, int scheduleID);

        /// <summary>
        /// Deletes all the event occurences of the schedule.
        /// </summary>
        /// <param name="scheduleID">ID of the schedule.</param>
        void DeleteEventScheduleOccurences(int scheduleID);

        /// <summary>
        /// Builds the event occurences for the schedule.
        /// </summary>
        /// <param name="scheduleID">ID of the schedule</param>
        /// <param name="excludedDates">Dates to be excluded from the list of event occurences</param>
        /// <returns>List of event occurences</returns>
        IEnumerable<EventOccurence> BuildOccurencesForSchedule(int scheduleID, List<DateTime> excludedDates);

        /// <summary>
        /// Gets the list of event occurences for the date range
        /// </summary>
        /// <param name="range">date range.</param>
        /// <returns>list of event occurences.</returns>
        IEnumerable<EventOccurence> GetOccurencesForRange(DateRange range);

        IEnumerable<EventOccurence> GetOccurencesForMonth(int month, int year);

        IEnumerable<EventOccurence> GetOccurencesForCurrentWeek();

        IEnumerable<EventOccurence> GetOccurencesForToday();
    }
}
