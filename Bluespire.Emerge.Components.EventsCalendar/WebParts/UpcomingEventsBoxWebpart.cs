using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;
using CMS.DataEngine;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class UpcomingEventsBoxWebpart : EventsCalendarWebPart
    {

        public virtual DataSet GetUpcomingEvents(int count)
        {
            string queryName = string.Format(EventsConstants.QUERY_GETUPCOMINGEVENTS, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@EventsCount", count);
            DataSet eventsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
            return eventsDS;
        }

    }
}
