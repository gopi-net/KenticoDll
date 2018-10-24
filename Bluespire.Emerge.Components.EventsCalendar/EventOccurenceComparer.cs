using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar
{
    public class EventOccurenceComparer : IEqualityComparer<EventOccurence>
    {
        public bool Equals(EventOccurence x, EventOccurence y)
        {
            return x.OccurenceDate == y.OccurenceDate;
        }

        public int GetHashCode(EventOccurence obj)
        {
            return obj.OccurenceDate.GetHashCode();
        }
    }
}
