using System;
using Bluespire.Emerge.Components.EventsCalendar.Common;

namespace Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions
{
    internal static class TEHelpers
    {
        internal static int GetWeekInMonth(int dayNumber)
        {
            var value = ((dayNumber - 1) / 7) + 1;
            return value;
        }

        internal static int GetDaysLeftInMonth(DateTime aDate)
        {
            var actualMaximum = DateTime.DaysInMonth(aDate.Year, aDate.Month);
            return actualMaximum - aDate.Day;
        }

        internal static int GetDayOfWeek(DateTime aDate)
        {
            return (int)aDate.DayOfWeek;
        }

        internal static int GetDayOfWeekValue(DaysOfWeek dayOfWeekOption)
        {
            switch (dayOfWeekOption)
            {
                case DaysOfWeek.Sunday:
                    return 0;

                case DaysOfWeek.Monday:
                    return 1;

                case DaysOfWeek.Tuesday:
                    return 2;

                case DaysOfWeek.Wednesday:
                    return 3;

                case DaysOfWeek.Thursday:
                    return 4;

                case DaysOfWeek.Friday:
                    return 5;

                case DaysOfWeek.Saturday:
                    return 6;

                default:
                    return 0;
            }
        }

        internal static int GetMonthlyIntervalValue(MonthlyInterval monthlyIntervalOption)
        {
            switch (monthlyIntervalOption)
            {
                case MonthlyInterval.First:
                    return 1;

                case MonthlyInterval.Second:
                    return 2;

                case MonthlyInterval.Third:
                    return 3;

                case MonthlyInterval.Fourth:
                    return 4;

                case MonthlyInterval.Fifth:
                    return 5;

                default:
                    return 0;
            }
        }

        internal static bool DayMatches(DateTime aDate, int day)
        {
            var matchFound = (int)aDate.Day == day;
            if (!matchFound)
            {
                var totalDaysInMonth = DateTime.DaysInMonth(aDate.Year, aDate.Month);
                if (day > totalDaysInMonth && aDate.Day == totalDaysInMonth)
                    matchFound = true;
            }

            return matchFound;
        }

        internal static bool DayOfWeekMatches(DateTime aDate, int dayOfWeek)
        {
            return GetDayOfWeek(aDate) == dayOfWeek;
        }

        internal static bool WeekMatches(DateTime aDate, int monthlyInterval)
        {
            if (monthlyInterval > 0)
            {
                return WeekFromStartMatches(aDate, monthlyInterval);
            }

            return WeekFromEndMatches(aDate, monthlyInterval);
        }

        private static bool WeekFromStartMatches(DateTime aDate, int monthlyInterval)
        {
            var week = GetWeekInMonth(aDate.Day);
            return (week == monthlyInterval);
        }

        private static bool WeekFromEndMatches(DateTime aDate, int monthlyInterval)
        {
            var daysFromMonthEnd = GetDaysLeftInMonth(aDate) + 1;
            return GetWeekInMonth(daysFromMonthEnd) == Math.Abs(monthlyInterval);
        }

    }
}
