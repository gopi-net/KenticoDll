using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class EventDetailsViewWebpart : EventsCalendarWebPart
    {
        public virtual int OccurenceID
        {
            get
            {
                return QueryHelper.GetInteger("OccurenceID", 0);
            }
        }

        public virtual DataSet getEventDetails()
        {
            string queryName = string.Format(EventsConstants.QUERY_GETEVENTDETAILS, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@OccurenceID", this.OccurenceID);
            DataSet eventsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);

            return eventsDS;
        }
    }
}
