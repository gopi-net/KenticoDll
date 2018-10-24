using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;
using System.Collections;
using Bluespire.Emerge.Common.CMS.SettingsProvider;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class RegistrationConfirmationWebPart : EventsCalendarWebPart
    {

        string adminEmailTemplateName = string.Empty;
        string userEmailTemplateName = string.Empty;
        protected int ItemID
        {
            get;
            set;
        }

        public virtual int OccurenceID
        {
            get
            {
                return QueryHelper.GetInteger("OccurenceID", 0);
            }
        }

        protected virtual DataSet getEventDetails()
        {
            string queryName = string.Format(EventsConstants.QUERY_GETEVENTDETAILS, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@OccurenceID", this.OccurenceID);
            DataSet eventsDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);

            return eventsDS;
        }

        public virtual DataSet getSelectedSessions()
        {
            string selectedSessions = ValidationHelper.GetString(SessionHelper.GetValue(EventsConstants.GENERALSELECTEDSESSIONS), string.Empty);
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

        protected bool SaveRegistrations()
        {
            bool isSuccess = false;
            try
            {
                string customTableName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName);
                Dictionary<string, object> tableData = (Dictionary<string, object>)SessionHelper.GetValue(EventsConstants.REGISTRATIONINFO);
                SessionHelper.Remove(EventsConstants.REGISTRATIONINFO);
                int itemID = 0;
                isSuccess = CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemID, tableData);
                ItemID = itemID;
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Confirmation Webpart:SaveRegistration", EventCode.EMERGE_EMAIL, ex.ToString());
                throw;
            }
            return isSuccess;
        }

        protected void SendEmail()
        {
            try
            {
                EventRegistration registration = EventsCalendarHelper.GetEventRegistrationByID(ItemID);
                SendEmailConfirmation(registration);
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Confirmation Webpart:SendEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw;
            }
        }

        protected bool SendEmailConfirmation(EventRegistration registration)
        {
            return EventsCalendarHelper.SendRegistrationEmail(registration, RegistrationEmailMode.GENERAL_INSERT_UI);
        }


        protected void SendEmailUsingSelectedTemplate()
        {
            try
            {
                EventRegistration registration = EventsCalendarHelper.GetEventRegistrationByID(ItemID);
                GetEmailTemplateNames();
                SendEmailConfirmationUsingSelectedemplate(registration);
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Confirmation Webpart:SendEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw;
            }
        }
        protected bool SendEmailConfirmationUsingSelectedemplate(EventRegistration registration)
        {
            return EventsCalendarHelper.SendRegistrationEmailUsingSelectedTemplate(registration, RegistrationEmailMode.GENERAL_INSERT_UI, adminEmailTemplateName, userEmailTemplateName);
        }
        private void GetEmailTemplateNames()
        {
            Hashtable hashedParameters = new Hashtable();
            hashedParameters.Add("@OccurenceID", OccurenceID);
            string queryName = string.Format(EventsConstants.QUERY_GETEVENTEMAILTEMPLATE, SiteContext.CurrentSiteName);
            DataSet dsEmailTemplate = new DataSet();
            dsEmailTemplate = EmergeSqlHelperClass.ExecuteQuery(queryName, hashedParameters, null, null);
            if (DataHelper.DataSourceIsEmpty(dsEmailTemplate))
                return;
            adminEmailTemplateName = ValidationHelper.GetString(dsEmailTemplate.Tables[0].Rows[0]["AdminEmailTemplate"], string.Empty);
            userEmailTemplateName = ValidationHelper.GetString(dsEmailTemplate.Tables[0].Rows[0]["UserEmailTemplate"], string.Empty);
        }


        protected bool fieldExists(string fieldID)
        {
            if (ControlPanel.FindControl(fieldID) != null)
                return true;
            return false;
        }

        protected void setFieldValue(string fieldID, string value)
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
