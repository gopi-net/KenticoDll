using System;
using System.Linq;

namespace Bluespire.Emerge.Components.EventsCalendar.TemporalExpressions
{
    public class IntersectionTE : CollectionTE
    {
        /// <summary>
        /// Returns true if the date is included in all of the expressions
        /// </summary>
        /// <param name="aDate"></param>
        /// <returns></returns>
        public override bool Includes(DateTime aDate)
        {
            return Expressions.All(e => e.Includes(aDate));
        }
    }
}
