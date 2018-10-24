using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    /// <summary>
    /// Class that represent one occurence of the event.
    /// </summary>
    public class EventOccurence
    {
        /// <summary>
        /// Gets or sets the ID of the event occurence.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_ITEMID)]
        public int OccurenceID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date of the event occurence.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE)]
        public DateTime OccurenceDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start time of the event occurence.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_STARTTIME)]
        public string StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end time of the event occurence.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_ENDTIME)]
        public string EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scheduleID of the event schedule of which this occurence is a part of.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID)]
        public int ScheduleID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the registration limit for the occurence.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTOCCURENCES_REGISTRTAIONLIMIT)]
        public int RegistrationLimit
        {
            get;
            set;
        }

        [CustomTableField(FieldName= EventsConstants.FIELDS_EVENTOCCURENCES_LOCATION)]
        public string Location
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether the occurence is a part of series event.
        /// </summary>
        public bool IsSeries
        {
            get
            {
                if (this.Schedule == null)
                    return false;
                return this.Schedule.IsSeries;

            }
        }

        private EventSchedule _schedule;
        /// <summary>
        /// Gets the event schedule object for this occurence.
        /// </summary>
        public EventSchedule Schedule
        {
            get
            {
                if (_schedule == null)
                {
                    EventSchedule schedule = null;
                    if (this.OccurenceID > 0)
                        schedule = EventsCalendarHelper.GetScheduleByOccurenceID(this.OccurenceID);
                    else if (this.ScheduleID > 0)
                        schedule = EventsCalendarHelper.GetScheduleByScheduleID(this.ScheduleID);
                    _schedule = schedule;
                }
                return _schedule;
            }
        }

        private List<EventRegistration> registrations;

        /// <summary>
        /// Gets or set the registrations for the event occurence.
        /// </summary>
        public List<EventRegistration> Registrations
        {
            get
            {
                if (registrations == null)
                {
                    registrations = EventsCalendarHelper.GetRegistrationsByOccurenceID(this.OccurenceID, EventRegistrationStatus.CONFIRMED);
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
