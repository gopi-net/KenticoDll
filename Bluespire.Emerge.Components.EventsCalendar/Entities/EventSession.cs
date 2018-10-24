using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    /// <summary>
    /// Class that represents the session of the event occurence.
    /// </summary>
    [Serializable]
    public class EventSession
    {
        /// <summary>
        /// Gets or sets the ID of the event session.
        /// </summary>
        public int SessionID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Title of the session.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start time of the session.
        /// </summary>
        public string StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end time of the session.
        /// </summary>
        public string EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scheduleID of the event schedule of which this session is a part of.
        /// </summary>
        public int? ScheduleID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the occurence ID of the event occurence of which this session is a part of.
        /// </summary>
        public int? OccurenceID
        {
            get;
            set;
        }

        private List<EventRegistration> registrations;

        /// <summary>
        /// Gets or set the registrations for the event schedule.
        /// </summary>
        public List<EventRegistration> Registrations
        {
            get
            {
                if (registrations == null)
                {
                    registrations = EventsCalendarHelper.GetRegistrationsBySession(this.SessionID, EventRegistrationStatus.CONFIRMED);
                }
                return registrations;
            }
            set
            {
                registrations = value;
            }
        }
    }
}
