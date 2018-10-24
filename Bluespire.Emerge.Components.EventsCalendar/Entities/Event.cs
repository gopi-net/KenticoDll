using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    public class Event
    {
        public int ItemID
        {
            get;
            set;
        }

        public string EventType
        {
            get;
            set;
        }

        public string EventName
        {
            get;
            set;
        }

        public string ShortTitle
        {
            get;
            set;
        }

        public string LongTitle
        {
            get;
            set;
        }

        public string TeaserText
        {
            get;
            set;
        }

        public string EventDescription
        {
            get;
            set;
        }

        public string EventLocation
        {
            get;
            set;
        }

        public int Department
        {
            get;
            set;
        }


        public int Category
        {
            get;
            set;
        }


        public int SubCategory
        {
            get;
            set;
        }

        public string AttachmentText
        {
            get;
            set;
        }

        public string WebsiteLink
        {
            get;
            set;
        }

        public string ContactName
        {
            get;
            set;
        }

        public string ContactEmail
        {
            get;
            set;
        }

        public string ContactPhone
        {
            get;
            set;
        }

        public bool IsSeries
        {
            get;
            set;
        }

    }
}
