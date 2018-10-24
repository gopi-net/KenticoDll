using System;

namespace Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions
{
    /// <summary>
    /// Compares two specific days of week exactly
    /// </summary>
    public class DayOfWeekTE : TemporalExpression
    {
        private readonly DayOfWeek _dayOfWeek;

        /// <summary>
        /// The day of week value
        /// </summary>
        /// <param name="aDayOfWeek"></param>
        public DayOfWeekTE(DayOfWeek aDayOfWeek)
        {
            _dayOfWeek = aDayOfWeek;
        }

        /// <summary>
        /// Returns true if the date day of week matches the flag
        /// attribute value:
        /// 
        ///     Sun = 1,
        ///     Mon = 2,
        ///     Tue = 4,
        ///     Wed = 8,
        ///     Thu = 16,
        ///     Fri = 32,
        ///     Sat = 64
        /// 
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            switch(aDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return ((int) _dayOfWeek == 1);

                case DayOfWeek.Monday:
                    return ((int)_dayOfWeek == 2);

                case DayOfWeek.Tuesday:
                    return ((int)_dayOfWeek == 4);

                case DayOfWeek.Wednesday:
                    return ((int)_dayOfWeek == 8);

                case DayOfWeek.Thursday:
                    return ((int)_dayOfWeek == 16);

                case DayOfWeek.Friday:
                    return ((int)_dayOfWeek == 32);

                case DayOfWeek.Saturday:
                    return ((int)_dayOfWeek == 64);

                default:
                    return false;
            }
        }
    }
}
