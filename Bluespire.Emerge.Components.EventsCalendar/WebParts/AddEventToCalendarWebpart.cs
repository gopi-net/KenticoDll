using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class AddEventToCalendarWebpart : EventsCalendarWebPart
    {
        public DataSet getAddToCalendarEventDetails(Hashtable hashedParameters)
        {
            string queryName = string.Format(EventsConstants.QUERY_GETADDTOCALENDAREVENTDATA, SiteContext.CurrentSiteName);
            DataSet ds = new DataSet();
            ds = EmergeSqlHelperClass.ExecuteQuery(queryName, hashedParameters, null, null);
            return ds;
            //QueryDataParameters parameters = new QueryDataParameters();
            //parameters.Add("@OccurenceID", this.OccurenceID);
            //DataSet eventsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
            //return eventsDS;
        }

        public virtual DataSet getEventLocationDetails(Hashtable hashedParameters)
        {
            string queryName = string.Format(EventsConstants.QUERY_GETEVENTLOCATIONDETAILS, SiteContext.CurrentSiteName);
            DataSet ds = new DataSet();
            ds = EmergeSqlHelperClass.ExecuteQuery(EventsConstants.QUERY_GETEVENTLOCATIONDETAILS, hashedParameters, null, null);
            return ds;
        }

        public string getGoogleCalendarLink(string eventName, string dtstartTime, string dtEndTime, string location, string eventDescription, string displayName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='https://www.google.com/calendar/render?action=TEMPLATE&text=");
            sb.Append(eventName);
            sb.Append("&dates=");
            sb.Append(dtstartTime);
            sb.Append("/");
            sb.Append(dtEndTime);
            sb.Append("&details=");
            sb.Append(eventDescription);
            sb.Append("&location=");
            sb.Append(location);
            sb.Append("&sf=true&output=xml' target='_blank'>" + displayName + "</a>&nbsp;");
            return sb.ToString();
        }
        public string getYahooCalendarLink(string eventName, string dtstartTime, string dtEndTime, string location, string eventDescription, string displayName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<a href='http://calendar.yahoo.com/?v=60&view=d&type=10&title=");
            sb.Append(eventName);
            sb.Append("&st=");
            sb.Append(dtstartTime);
            sb.Append("/");
            sb.Append(dtEndTime);
            sb.Append("&desc=");
            sb.Append(eventDescription);
            sb.Append("&in_loc=");
            sb.Append(location);
            sb.Append("&sf=true&output=xml' target='_blank'>" + displayName + "</a>&nbsp;");
            return sb.ToString();
        }

        public void getOutlookLink(HttpContext context, string eventName, string dtstartTime, string dtEndTime, string location, string EventDescription)
        {
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-disposition", "attachment; filename=\\Calender.ics");
            context.Response.Write("BEGIN:VCALENDAR");
            context.Response.Write("\nVERSION:2.0");
            context.Response.Write("\nMETHOD:PUBLISH");
            context.Response.Write("\nBEGIN:VEVENT");


            context.Response.Write("\nDTSTART:" + dtstartTime);
            context.Response.Write("\nDTEND:" + dtEndTime);


            context.Response.Write("\nLOCATION:" + location);

            context.Response.Write("\nUID:" + DateTime.Now.ToUniversalTime().ToString());
            context.Response.Write("\nDTSTAMP:" + DateTime.Now.ToUniversalTime().ToString());
            context.Response.Write("\nSUMMARY:" + eventName);

            
            context.Response.Write("\nDESCRIPTION:" + EventDescription);

            context.Response.Write("\nPRIORITY:5");
            context.Response.Write("\nCLASS:PUBLIC");
            context.Response.Write("\nEND:VEVENT");
            context.Response.Write("\nEND:VCALENDAR");
            context.Response.End();
        }



    }
}
