using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    public class EventRegistration
    {
        public int ItemID
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string StreetAddress
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string Zip
        {
            get;
            set;
        }

        public int ScheduleID
        {
            get;
            set;
        }

        public int OccurenceID
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }

        public string SelectedSessions
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public double Amount
        {
            get;
            set;
        }

        public string PaymentType
        {
            get;
            set;
        }

        public DateTime PaymentDate
        {
            get;
            set;
        }

        public string DiscountCode
        {
            get;
            set;
        }

        private EventOccurence _occurence = null;
        public EventOccurence Occurence
        {
            get
            {
                if (_occurence == null)
                {
                    _occurence = EventsCalendarHelper.GetEventOccurenceByID(this.OccurenceID);
                }
                return _occurence;
            }
        }
    }
}
