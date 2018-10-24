using System;
using System.Collections.Generic;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.SiteProvider;
using Bluespire.Emerge.Common;
using System.Linq;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.DataEngine;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    /// <summary>
    /// Service class for processing event registrations.
    /// </summary>
    public class EventRegistrationService : IEventRegistrationService
    {
        #region IEventRegistrationService members
        
        public IEnumerable<EventRegistration> GetAllRegistrationsByScheduleID(int scheduleID, EventRegistrationStatus status)
        {
            try
            {
                string whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID + " = " + scheduleID.ToString();
                if(status != EventRegistrationStatus.NONE)
                    whereCondition += " AND " + EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS + " = '" + status.ToString() + "'";
                DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), whereCondition, string.Empty);
                List<EventRegistration> registrations = new List<EventRegistration>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    EventRegistration registration = row.ToEventRegistration();
                    registrations.Add(registration);
                }
                return registrations;
            }
            catch (CustomTableNotExistsException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<EventRegistration> GetRegistrationsByOccurenceID(int occurenceID, EventRegistrationStatus status)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID + " = " + occurenceID.ToString();
            if (status != EventRegistrationStatus.NONE)
                whereCondition += " AND " + EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS + " = '" + status.ToString() + "'";
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), whereCondition, string.Empty);
            List<EventRegistration> registrations = new List<EventRegistration>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                EventRegistration registration = row.ToEventRegistration();
                registrations.Add(registration);
            }
            return registrations;
        }

        public EventRegistration GetEventRegistrationByID(int registrationID)
        {
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(registrationID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName));
            EventRegistration registration = item.ToEventRegistration();
            return registration;
        }

        public void UpdateStartTimeForRegistration(int registrationID)
        {
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName);
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(registrationID, className);
            int occurenceID = Convert.ToInt32(item.GetValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID));
            

            string occurenceClassName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES, SiteContext.CurrentSiteName);
            string startTime = Convert.ToString(CustomTableDataHelper.GetFieldValue(occurenceID, occurenceClassName, EventsConstants.FIELDS_EVENTOCCURENCES_STARTTIME));

            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add(new KeyValuePair<string, object>(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME, startTime));
            
            int scheduleID = Convert.ToInt32(item.GetValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID));
            EventSchedule schedule = EventsCalendarHelper.GetScheduleByScheduleID(scheduleID);
            if (schedule.IsPaidSchedule)
            {
                string scheduleClassName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, SiteContext.CurrentSiteName);
                double costforpublic = ValidationHelper.GetDouble(CustomTableDataHelper.GetFieldValue(scheduleID, scheduleClassName, EventsConstants.FIELDS_EVENTSCHEDULE_COSTFORPUBLIC), 0);
                data.Add(new KeyValuePair<string, object>(EventsConstants.FIELDS_EVENTREGISTRATIONS_COSTFORPUBLIC, costforpublic));
            }
            CustomTableDataHelper.SaveCustomTableItem(className, ref registrationID, data);
        }

        public void UpdateOccurenceForRegistration(int registrationID, int occurenceID)
        {
            IDictionary<string, object> data = new Dictionary<string, object>();
            data.Add(new KeyValuePair<string, object>(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID, occurenceID));
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName);
            CustomTableDataHelper.SaveCustomTableItem(className, ref registrationID, data);
        }

        public void MoveRegistrationsForSchedule(int scheduleID, IDictionary<string, string> registrations)
        {
            List<EventOccurence> savedOccurences = EventsCalendarHelper.GetOccurencesByScheduleID(scheduleID);

            foreach (KeyValuePair<string, string> registrationEntry in registrations)
            {
                int registrationID = Convert.ToInt32(registrationEntry.Key);
                EventOccurence occ = savedOccurences.Find(a => a.OccurenceDate.ToString() == registrationEntry.Value.ToString());

                IDictionary<string, object> tabledata = new Dictionary<string, object>();
                tabledata.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID, occ.OccurenceID.ToString());

                int id = registrationID;
                CustomTableDataHelper.SaveCustomTableItem(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), ref id, tabledata);
            }
        }

        public void AddEventRegistration(EventRegistration registration)
        {

        }

        public SaveRegistrationStatus SaveEventRegistrations(List<Dictionary<string, object>> registrations, ref List<int> savedItemIDs)
        {
            SaveRegistrationStatus status = ValidateRegistrations(registrations);
            if (status == SaveRegistrationStatus.VALID)
            {
                foreach (Dictionary<string, object> registration in registrations)
                {
                    int id = 0;
                    bool isSuccess = CustomTableDataHelper.SaveCustomTableItem(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), ref id, registration);

                    if (isSuccess)
                        savedItemIDs.Add(id);
                    else
                    {
                        DeleteSavedRegistrations(savedItemIDs);
                        return SaveRegistrationStatus.FAILED;
                    }
                }
                status = SaveRegistrationStatus.SUCCESS;
            }
            return status;
        }

        private void DeleteSavedRegistrations(List<int> ids)
        {
            foreach (int id in ids)
            {
                CustomTableDataHelper.DeleteCustomTableItem(id, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName));
            }
        }

        public SaveRegistrationStatus ValidateRegistrations(List<Dictionary<string, object>> registrations)
        {
            SaveRegistrationStatus status = SaveRegistrationStatus.VALID;
            foreach (Dictionary<string, object> registration in registrations)
            {
                int occurenceID = ValidationHelper.GetInteger(registration[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0);
                if (occurenceID > 0)
                {
                    EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);

                    if (!occurence.Schedule.AllowDuplicationRegistrations)
                    {
                        if (registrations.Any(a => ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID], 0) == occurence.ScheduleID && ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0) != occurence.OccurenceID))
                        {
                            status = SaveRegistrationStatus.DUPLICATEREGISTRATIONSINCART;
                            return status;
                        }
                    }
                    string discountCode = string.Empty;
                    
                    if (occurence.Schedule.IsPaidSchedule)
                    {
                        discountCode = ValidationHelper.GetString(registration[EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE], string.Empty);

                        if (!String.IsNullOrEmpty(discountCode))
                        {
                            if (!EventsCalendarHelper.IsDiscountCodeValid(discountCode, occurence.ScheduleID))
                            {
                                status = SaveRegistrationStatus.INVALIDDISCOUNTCODE;
                                return status;
                            }
                            if (registrations.Any(a =>
                                (
                                ValidationHelper.GetString(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE], string.Empty) == discountCode
                                && ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0) != occurence.OccurenceID
                                && ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID], 0) == occurence.ScheduleID
                                )
                                ||
                                (
                                !String.IsNullOrEmpty(ValidationHelper.GetString(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE], string.Empty))
                                && ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0) != occurence.OccurenceID
                                && ValidationHelper.GetInteger(a[EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID], 0) == occurence.ScheduleID
                                )
                                ))
                            {
                                status = SaveRegistrationStatus.DISCOUNTCODEUSEDINCART;
                                return status;
                            }
                        }
                        if (EventsCalendarHelper.IsDiscountCodeUsed(Convert.ToString(registration[EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL]), occurence, discountCode))
                        {
                            status = SaveRegistrationStatus.DISCOUNTCODEUSED;
                            return status;
                        }
                    }

                    if (occurence.IsRegistrationLimitReached())
                    {
                        status = SaveRegistrationStatus.REGISTRATIONLIMITREACHED;
                        return status;
                    }

                    if (occurence.IsRegistrationExist(Convert.ToString(registration[EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL]), 0))
                    {
                        status = SaveRegistrationStatus.DUPLICATEREGISTRATIONS;
                        return status;
                    }

                    
                }
                else
                    return SaveRegistrationStatus.INVALID;
            }
            return SaveRegistrationStatus.VALID;
        }

        public IEnumerable<EventRegistration> GetRegistrationsBySessionID(int sessionID, EventRegistrationStatus status)
        {
            try
            {
                string queryName = string.Format(EventsConstants.QUERY_GETREGISTRATIONSBYSESSION, SiteContext.CurrentSiteName);
                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@SessionID", sessionID);
                parameters.Add("@Status", status.ToString());
                DataSet registrationDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
                List<EventRegistration> registrations = new List<EventRegistration>();
                foreach (DataRow row in registrationDS.Tables[0].Rows)
                {
                    EventRegistration registration = row.ToEventRegistration();
                    registrations.Add(registration);
                }
                return registrations;
            }
            catch (CustomTableNotExistsException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        private IDictionary<string, object> getTableData(List<string> eventOccurenceFields, EventOccurence occurence)
        {
            IDictionary<string, object> tableData = new Dictionary<string, object>();
            foreach (string field in eventOccurenceFields)
            {
                string propertyName = EmergeStaticHelper.GetPropertyNameByCustomAttribute<EventOccurence, CustomTableFieldAttribute>(x => x.FieldName == field)[0];
                string value = Convert.ToString(typeof(EventOccurence).GetProperty(propertyName).GetValue(occurence, null));
                KeyValuePair<string, object> data;

                if (!String.IsNullOrEmpty(value))
                    data = new KeyValuePair<string, object>(field, value);
                else
                    data = new KeyValuePair<string, object>(field, string.Empty);

                tableData.Add(data);
            }
            return tableData;
        }


        public IEnumerable<EventRegistration> GetRegistrationsByEventID(int eventID, EventRegistrationStatus status)
        {
            List<EventSchedule> scheduleList = EventsCalendarHelper._factory.GetTypeInstance<IEventScheduleService>().GetEventSchedulesByEventID(eventID).ToList<EventSchedule>();
            List<EventRegistration> registrations = new List<EventRegistration>();
            foreach (EventSchedule schedule in scheduleList)
            {
                registrations.AddRange(GetAllRegistrationsByScheduleID(schedule.ScheduleID, status));
            }
            return registrations;
        }


        public void UpdateRegistrationForVolunteerUser(int registrationID, int userID)
        {
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
                int itemID = registrationID;

                CustomTableDataHelper.SaveCustomTableItem(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), ref itemID, tabledata);
            }
        }


        public bool AllowEmailSend(RegistrationEmailMode mode)
        {
            string customTableName = string.Format(EventsConstants.CUSTOMTABLE_EVENT_REGISTRATIONEMAILCONFIG, SiteContext.CurrentSiteName);
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, string.Empty, "ItemModifiedBy DESC");
            bool result = true;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                result = ValidationHelper.GetBoolean(ds.Tables[0].Rows[0][mode.ToString()], true);
            }
            return result;
        }


        public void DeleteRegistrationsByScheduleID(int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID + " = " + scheduleID.ToString();
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), whereCondition, string.Empty);
            List<int> itemIDs = new List<int>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int itemID = Convert.ToInt32(dr[EventsConstants.FIELDS_EVENTREGISTRATIONS_ITEMID]);
                itemIDs.Add(itemID);
            }
            if (itemIDs.Count > 0)
                DeleteSavedRegistrations(itemIDs);
        }

        public void DeleteRegistrationsByOccurrenceID(int occurrenceID)
        {
            string whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID + " = " + occurrenceID.ToString();
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS, SiteContext.CurrentSiteName), whereCondition, string.Empty);
            List<int> itemIDs = new List<int>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int itemID = Convert.ToInt32(dr[EventsConstants.FIELDS_EVENTREGISTRATIONS_ITEMID]);
                itemIDs.Add(itemID);
            }
            if (itemIDs.Count > 0)
                DeleteSavedRegistrations(itemIDs);
        }
    }
}
