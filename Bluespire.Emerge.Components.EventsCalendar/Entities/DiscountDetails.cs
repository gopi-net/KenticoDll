using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.EventsCalendar.Entities
{
    public class DiscountDetails
    {
        public int DiscountID
        {
            get;
            set;
        }

        public int ScheduleID
        {
            get;
            set;
        }

        public string DiscountType
        {
            get;
            set;
        }

        public string DiscountCode
        {
            get;
            set;
        }

        public double DiscountFactor
        {
            get;
            set;
        }
    }
}
