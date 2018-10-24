using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    /// <summary>
    /// Class that represents the event schedule object.
    /// </summary>
    public class EventSchedule
    {
        /// <summary>
        /// Gets or sets the ID of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_SCHEDULEID)]
        public int ScheduleID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start date of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_STARTDATE)]
        public DateTime StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end date of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_ENDDATE)]
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the frequency type of the event schedule. for e.g. Single, Weekly or Monthly.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_FREQUENCYTYPE)]
        public FrequencyType FrequencyType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start time of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_STARTTIME)]
        public string StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end time of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_ENDTIME)]
        public string EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list of selected dates.
        /// </summary>
        public List<DateTime> SelectedDates
        {
            get;
            set;
        }

        /// <summary>
        /// gets or sets the dates
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_SELECTEDDATES)]
        public string Dates
        {
            get;
            set;
        }

        
        public int MonthlyInterval { get; set; }
        /// <summary>
        /// The monthly interval expressed as enumeration
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_WEEKOFMONTH)]
        public MonthlyInterval MonthlyIntervalOptions
        {
            get
            {
                return (MonthlyInterval)MonthlyInterval;
            }
            set
            {
                MonthlyInterval = (int)value;
            }
        }

        public int DaysOfWeek { get; set; }
        /// <summary>
        /// The days of the week expressed as enumeration.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_DAYOFWEEK)]
        public DaysOfWeek DaysOfWeekOptions
        {
            get
            {
                return (DaysOfWeek)DaysOfWeek;
            }
            set
            {
                DaysOfWeek = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the registration limit for the schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT)]
        public int RegistrationLimit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the event ID of this schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID)]
        public int EventID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the schedule is series schedule.
        /// </summary>
        public bool IsSeries
        {
            get
            {
                return this.Event.IsSeries;
            }
        }

        /// <summary>
        /// Gets the type of the schedule, series or non-series.
        /// </summary>
        public EventScheduleType ScheduleType
        {
            get
            {
                return this.IsSeries ? EventScheduleType.Series : EventScheduleType.NonSeries;
            }
        }

        private List<EventOccurence> _occurences = null;
        /// <summary>
        /// Gets the list of occurence for this schedule.
        /// </summary>
        public List<EventOccurence> Occurences
        {
            get
            {
                if (_occurences == null)
                {
                    _occurences = EventsCalendarHelper.GetOccurencesByScheduleID(this.ScheduleID);
                }
                return _occurences;
            }
        }

        private Event _event = null;
        /// <summary>
        /// Gets the event object for this schedule.
        /// </summary>
        public Event Event
        {
            get
            {
                if (_event == null)
                {
                    _event = EventsCalendarHelper.GetEventByScheduleID(this.ScheduleID);
                }
                return _event;
            }
        }

        /// <summary>
        /// Gets or sets whether the schedule is paid or unpaid.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_ISPAIDSCHEDULE)]
        public bool IsPaidSchedule
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this event schedule has sessions.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_HASSESSIONS)]
        public bool HasSessions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or set the session details.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_SESSIONDETAILS)]
        public string SessionDetails
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this event schedule allows duplicate regsitrations.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_HASDUPLICATEREGISTRATIONS)]
        public bool AllowDuplicationRegistrations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether this event schedule allows group registrations.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_ALLOWGROUPREGISTRATIONS)]
        public bool AllowGroupRegistrations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum limit for the group registrations.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT)]
        public int MinimumLimit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum limit for group registrations.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT)]
        public int MaximumLimit
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cost of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_COSTFORPUBLIC)]
        public double CostForPublic
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the discount for the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_DISCOUNT)]
        public string Discount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status of the event schedule.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_STATUS)]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether the schedule needs registrations
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_NEEDREGISTRATIONS)]
        public bool NeedRegistrations
        {
            get;
            set;
        }

        [CustomTableField(FieldName = EventsConstants.FIELDS_BLACKOUTDATES_ALLOWBOOKING)]
        public int RegistrationDeadline
        {
            get;
            set;
        }

        [CustomTableField(FieldName = EventsConstants.FIELDS_EVENTSCHEDULE_LOCATION)]
        public string Location
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
                    registrations = EventsCalendarHelper.GetAllRegistrationsByScheduleID(this.ScheduleID, EventRegistrationStatus.CONFIRMED);
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
