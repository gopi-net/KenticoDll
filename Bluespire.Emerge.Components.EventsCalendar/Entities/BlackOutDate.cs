using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    /// <summary>
    /// Class that represents the blackout date.
    /// </summary>
    [Serializable]
    public class BlackOutDate
    {
        /// <summary>
        /// Gets or sets the item id of the blackout date.
        /// </summary>
        [CustomTableField(FieldName= EventsConstants.FIELDS_BLACKOUTDATES_ITEMID)]
        public int BlackOutDateID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the title of the blackout date.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_BLACKOUTDATES_TITLE)]
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Blackout date.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_BLACKOUTDATES_BLACKOUTDATE)]
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether booking is allowed for this blackout date.
        /// </summary>
        [CustomTableField(FieldName = EventsConstants.FIELDS_BLACKOUTDATES_ALLOWBOOKING)]
        public bool AllowBooking
        {
            get;
            set;
        }
    }
}
