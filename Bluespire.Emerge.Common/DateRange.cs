using System;
using Bluespire.Emerge.Common.Exceptions;


namespace Bluespire.Emerge.Common
{
    public class DateRange
    {
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        public DateRange(DateTime startDateTime, DateTime endDateTime)
        {
            if (startDateTime > endDateTime)
                throw new InvalidDateRangeException("The date range is not valid.");
            
            _startDateTime = startDateTime;
            _endDateTime = endDateTime;
        }

        public DateTime StartDateTime { get { return _startDateTime; } }
        public DateTime EndDateTime { get { return _endDateTime; } }
    }
}
