using Bluespire.Emerge.Components.EventsCalendar.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class VolunteerEventRegistrationConfirmationWebpart : EventsCalendarWebPart
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

        public virtual DataSet getSelectedSessions()
        {
            string selectedSessions = ValidationHelper.GetString(SessionHelper.GetValue(EventsConstants.USERSELECTEDSESSIONS), string.Empty);
            if (!String.IsNullOrEmpty(selectedSessions))
            {
                string[] sessionIDs = selectedSessions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                string commaSeperatedSessionIDs = String.Join(",", sessionIDs);
                
                string queryName = string.Format(EventsConstants.QUERY_GETSELECTEDSESSIONS, SiteContext.CurrentSiteName);
                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@SelectedSessions", commaSeperatedSessionIDs);
                
                DataSet sessionDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
                return sessionDS;
            }
            return null;
        }

        public bool fieldExists(string fieldID)
        {
            if (ControlPanel.FindControl(fieldID) != null)
                return true;
            return false;
        }

        public void setFieldValue(string fieldID, string value)
        {
            Control field = ControlPanel.FindControl(fieldID);
            if (field is Literal)
            {
                Literal literalControl = (Literal)field;
                literalControl.Text = value;
            }
        }
    }
}
