using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.Membership;
using CMS.CustomTables;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class ChooseTimeSlotViewWebpart : EventsCalendarWebPart
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

        public virtual DataSet getSessionDetails()
        {
            string queryName = string.Format(EventsConstants.QUERY_GETSESSIONSFOROCCURENCE, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@OccurenceID", this.OccurenceID);
            DataSet sessionsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);

            return sessionsDS;

        }

        protected virtual bool ValidateSessions(string selectedSessions)
        {
            string[] sessionIDs = selectedSessions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            string commaSeperatedSessionIDs = String.Join(",", sessionIDs);
            string whereCondition = EventsConstants.FIELDS_EVENTSESSIONS_SESSIONID + " IN (" + commaSeperatedSessionIDs + ")";
            List<EventSession> sessions = EventsCalendarHelper.GetSessionsByCondition(whereCondition);

            bool result = EventsCalendarHelper.ValidateSessions(sessions);
            return result;
        }

        private bool SaveRegistration(EventOccurence occurence, string selectedSessions, ref int itemID)
        {
            int userID = MembershipContext.AuthenticatedUser.UserID;

            List<CustomTableItem> items = CustomTableDataHelper.GetCustomTableItemsByCriteria(String.Format(EventsConstants.CUSTOMTABLE_EVENT_VOLUNTEERUSERS, SiteContext.CurrentSiteName), "UserID = " + userID.ToString());
            if (items.Count > 0)
            {
                CustomTableItem item = items[0];
                string firstName = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_FIRSTNAME), string.Empty);
                string lastName = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_LASTNAME), string.Empty);
                string phone = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PHONE), string.Empty);
                string city = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_CITY), string.Empty);
                string state = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_STATE), string.Empty);
                string streetAddress = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_STREETADDRESS), string.Empty);
                string zip = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_ZIP), string.Empty);
                string email = ValidationHelper.GetString(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_EMAIL), string.Empty);

                IDictionary<string, object> tabledata = new Dictionary<string, object>();

                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL, email);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_FIRSTNAME, firstName);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_LASTNAME, lastName);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PHONE, phone);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATE, state);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STREETADDRESS, streetAddress);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_ZIP, zip);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_CITY, city);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID, occurence.OccurenceID);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID, occurence.ScheduleID);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS, selectedSessions);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS, EventRegistrationStatus.CONFIRMED.ToStringRepresentation());
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_VOLUNTEERUSER, userID);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS, string.Empty);
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME, occurence.StartTime);

                return CustomTableDataHelper.SaveCustomTableItem(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), ref itemID, tabledata);
            }
            return false;
        }

        protected virtual bool RegisterUser()
        {
            if (!MembershipContext.AuthenticatedUser.IsInRole(EventsConstants.ROLE_VOLUNTEERUSERS, SiteContext.CurrentSiteName))
                throw new UserNotVolunteerException();

            HiddenField field = (HiddenField)ControlPanel.FindControl("OccurenceIDField");
            int occurenceID = ValidationHelper.GetInteger(field.Value, 0);

            EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
            if (occurence.IsRegistrationLimitReached())
                throw new EventRegistrationLimitReachedException();

            if (occurence.IsRegistrationExist(MembershipContext.AuthenticatedUser.Email, 0))
                throw new RegistrationExistException();

            HiddenField selectedSession = (HiddenField)ControlPanel.FindControl("SelectedSession");
            string selectedSessions = selectedSession.Value.Trim();

            int itemID = 0;
            if (SaveRegistration(occurence, selectedSessions, ref itemID))
            {
                EventRegistration registration = EventsCalendarHelper.GetEventRegistrationByID(itemID);
                EventsCalendarHelper.SendRegistrationEmail(registration, RegistrationEmailMode.VOLUNTEER_INSERT_UI);
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
