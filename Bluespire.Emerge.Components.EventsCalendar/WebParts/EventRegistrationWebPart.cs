using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;
using System.Data;
namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{

    public class EventRegistrationWebPart : EventsCalendarWebPart
    {
        const string EVENTSSESSIONS = "EventsSessions";
        private int? customTableID;
        /// <summary>
        /// Gets the id of the selected custom table.
        /// </summary>
        protected virtual int CustomTableID
        {
            get
            {
                if (!customTableID.HasValue)
                    customTableID = QueryHelper.GetInteger("customtableid", 0);
                if (customTableID == 0)
                {
                    string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName);
                    DataClassInfo classInfo = CustomTableDataHelper.GetCustomTableClassInfo(className);
                    customTableID = classInfo.ClassID;
                }
                return customTableID.Value;
            }
        }

        public virtual int OccurenceIDValue
        {
            get
            {
                return QueryHelper.GetInteger("OccurenceID", 0);
            }
        }

        protected double GetDiscountedCostbyCodeAndScheduleID(string discountCode, int scheduleID)
        {
            return EventsCalendarHelper.GetDiscountedCostbyCodeAndScheduleID(discountCode, scheduleID);
        }


        protected bool ValidateSessions(List<EventSession> selectedSessions)
        {
            return EventsCalendarHelper.ValidateSessions(selectedSessions);
        }

        protected SaveRegistrationStatus SetRegistrationParameters(params object[] data)
        {
            SaveRegistrationStatus status = SaveRegistrationStatus.VALID;
            CreateFormParameters();

            int occurenceID = ValidationHelper.GetInteger(data[0], 0);
            int scheduleID = ValidationHelper.GetInteger(data[1], 0);
            double discountCost = ValidationHelper.GetDouble(data[2], 0);
            List<EventSession> sessions = (List<EventSession>)data[3];
            string discountcode = ValidationHelper.GetString(data[4], string.Empty);

            setCalculatedFields(occurenceID, scheduleID, discountCost, discountcode);
            string customTableName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName);

            EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);

            if (occurence.IsRegistrationExist(Convert.ToString(FormParameters[EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL]), 0))
                throw new RegistrationExistException();

            List<Dictionary<string, object>> registrations = new List<Dictionary<string, object>>();
            registrations.Add(FormParameters);
            status = EventsCalendarHelper.ValidateRegistrations(registrations);
            if (status == SaveRegistrationStatus.VALID)
            {
                setSelectedSessions(sessions);
                SessionHelper.SetValue(EventsConstants.REGISTRATIONINFO, FormParameters);

                if (occurence.Schedule.IsPaidSchedule)
                {
                    //ToDo : Send payment request to payment gateway.
                }
            }

            return status;
        }

        private void setCalculatedFields(int occurenceID, int scheduleID, double discountCost, string discountCode)
        {
            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID))
                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID, occurenceID);

            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID))
                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID, scheduleID);

            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS))

                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS, EventRegistrationStatus.CONFIRMED.ToStringRepresentation());

            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS))
                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS, string.Empty);

            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS))
                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS, getSelectedSessionValues());

            setPaymentFields(occurenceID, discountCost, discountCode);
        }

        private void setPaymentFields(int occurenceID, double discountCost, string discountCode)
        {
            EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
            if (occurence.Schedule.IsPaidSchedule)
            {
                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE, DateTime.Now);

                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT, discountCost);

                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE, "ONLINE");

                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_COSTFORPUBLIC))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_COSTFORPUBLIC, occurence.Schedule.CostForPublic);

                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE, discountCode);
            }
            else
            {
                if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE))
                    FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE, string.Empty);
            }

            if (!FormParameters.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME))
                FormParameters.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME, occurence.StartTime);
        }

        protected void setSelectedSessions(List<EventSession> sessions)
        {
            string selectedSessions = string.Empty;
            foreach (EventSession session in sessions)
                selectedSessions += session.SessionID + "|";

            if (!string.IsNullOrEmpty(selectedSessions))
                SessionHelper.SetValue(EventsConstants.GENERALSELECTEDSESSIONS, selectedSessions);
        }

        protected List<EventSession> getSelectedSessions()
        {
            List<ListItem> list = ((ListBox)ControlPanel.FindControl("SelectedSessions")).Items.OfType<ListItem>().Where(x => x.Selected == true).ToList<ListItem>();
            List<EventSession> sessions = null;
            if (ViewState[EVENTSSESSIONS] != null)
                sessions = (List<EventSession>)ViewState[EVENTSSESSIONS];
            else
                sessions = EventsCalendarHelper.GetSessionsByOccurenceID(Convert.ToInt32(((HiddenField)ControlPanel.FindControl("OccurenceID")).Value));
            List<EventSession> selectedSessions = new List<EventSession>();
            foreach (ListItem item in list)
            {
                int sessionID = Convert.ToInt32(item.Value);
                EventSession session = sessions.Find(x => x.SessionID == sessionID);
                if (session != null)
                    selectedSessions.Add(session);
            }
            return selectedSessions;
        }

        protected string getSelectedSessionValues()
        {
            string selectedSessions = string.Empty;
            List<EventSession> sessions = getSelectedSessions();
            foreach (EventSession session in sessions)
                selectedSessions += session.SessionID + "|";

            return selectedSessions;
        }
        protected DataSet getEventRegistrationField()
        {
            string queryName = string.Format(EventsConstants.QUERY_GETREGISTRATIONFIELD, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@OccurenceID", OccurenceIDValue);
            DataSet eventsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
            return eventsDS;
        }
    }
}
