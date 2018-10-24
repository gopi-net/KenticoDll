using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface IBlackoutDateService
    {
        IEnumerable<BlackOutDate> GetBlackOutDates(BlackOutDateFilter filter);

        bool HasOccurencesOnDate(DateTime date);

        IEnumerable<BlackOutDate> GetBlackOutDatesInOccurences(List<EventOccurence> occurenceList);

        IEnumerable<BlackOutDate> GetBlackOutDatesInDates(List<DateTime> occurenceList);
 
    }
}
