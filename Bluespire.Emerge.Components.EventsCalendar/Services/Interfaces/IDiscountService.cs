using Bluespire.Emerge.Components.EventsCalendar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface IDiscountService
    {

        void DeleteDiscountsByScheduleID(int scheduleID);

        DiscountDetails GetDicountDetailsByCodeAndScheduleID(string discountCode, int scheduleID);

        double GetDiscountedCostbyCodeAndScheduleID(string discountCode, int scheduleID);
    }
}
