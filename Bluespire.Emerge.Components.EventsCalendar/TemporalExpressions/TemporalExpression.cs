using System;


namespace Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions
{
    public abstract class TemporalExpression
    {
        public abstract bool Includes(DateTime aDate);
    }
}
