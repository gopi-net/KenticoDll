using System;
using System.Collections.Generic;
using System.Linq;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.CommonService;
using System.Data;
using Bluespire.Emerge.CommonService.Unity;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using System.Collections;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Common.Logging;
using System.Text;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.DataEngine;

namespace Bluespire.Emerge.Components.EventsCalendar.Helpers
{
    /// <summary>
    /// Helper class for events calendar. This class should contain the utility methods for events calendar.
    /// </summary>
    public static class EventsCalendarHelper
    {
        public static EmergeUnityFactory _factory = new EmergeUnityFactory(EventsConstants.UNITY_CONFIGFILENAME, EventsConstants.UNITY_EVENTSCONTAINER);
        const string ITEMID = "ItemID";

        #region Public Methods

        /// <summary>
        /// Returns the schedule object from the custom table item.
        /// </summary>
        /// <param name="item">Custom table item</param>
        /// <returns>schedule object.</returns>
        public static EventSchedule ToEventSchedule(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            EventSchedule schedule = row.ToEventSchedule();
            return schedule;
        }

        /// <summary>
        /// Converts a datarow to Event schedule object.
        /// </summary>
        /// <param name="row">Datarow to convert to event schedule object.</param>
        /// <returns>Event schedule object representing the datarow.</returns>
        public static EventSchedule ToEventSchedule(this DataRow row)
        {
            try
            {
                EventSchedule schedule = new EventSchedule();
                schedule.ScheduleID = Convert.ToInt32(row[ITEMID]);
                schedule.FrequencyType = (FrequencyType)Enum.Parse(typeof(FrequencyType), Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_FREQUENCYTYPE]));
                schedule.StartDate = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_STARTDATE]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_STARTDATE]);
                schedule.EndDate = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_ENDDATE]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_ENDDATE]);

                schedule.StartTime = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_STARTTIME]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_STARTTIME]).ToString("hh:mm tt");
                schedule.EndTime = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_ENDTIME]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_ENDTIME]).ToString("hh:mm tt");

                string daysOfWeek = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_DAYOFWEEK]);
                if (!String.IsNullOrEmpty(daysOfWeek))
                    schedule.DaysOfWeekOptions = getDaysOfWeekOptionsForSchedule(daysOfWeek);

                string weekofMonth = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_WEEKOFMONTH]);
                if (!String.IsNullOrEmpty(weekofMonth))
                    schedule.MonthlyIntervalOptions = getMonthlyIntervalOptionsForSchedule(weekofMonth);

                string selectedDates = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_SELECTEDDATES]);
                if (!String.IsNullOrEmpty(selectedDates))
                {
                    schedule.Dates = selectedDates;
                    schedule.SelectedDates = getSelectedDates(selectedDates);
                }

                if (schedule.FrequencyType == FrequencyType.Single && schedule.SelectedDates.Count > 0)
                {
                    schedule.StartDate = schedule.SelectedDates[0];
                    schedule.EndDate = schedule.SelectedDates[schedule.SelectedDates.Count - 1];
                }
                schedule.RegistrationLimit = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT]);
                schedule.IsPaidSchedule = Convert.ToBoolean(row[EventsConstants.FIELDS_EVENTSCHEDULE_ISPAIDSCHEDULE]);
                schedule.AllowDuplicationRegistrations = Convert.ToBoolean(row[EventsConstants.FIELDS_EVENTSCHEDULE_HASDUPLICATEREGISTRATIONS]);
                schedule.AllowGroupRegistrations = Convert.ToBoolean(row[EventsConstants.FIELDS_EVENTSCHEDULE_ALLOWGROUPREGISTRATIONS]);
                schedule.CostForPublic = Convert.ToDouble(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_COSTFORPUBLIC]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_COSTFORPUBLIC]);
                schedule.Discount = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_DISCOUNT]);
                schedule.HasSessions = Convert.ToBoolean(row[EventsConstants.FIELDS_EVENTSCHEDULE_HASSESSIONS]);
                schedule.MaximumLimit = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT]);
                schedule.MinimumLimit = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_MINIMUMLIMIT]);
                schedule.SessionDetails = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_SESSIONDETAILS]);
                schedule.Status = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_STATUS]);
                schedule.NeedRegistrations = Convert.ToBoolean(row[EventsConstants.FIELDS_EVENTSCHEDULE_NEEDREGISTRATIONS]);
                schedule.RegistrationDeadline = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE]) ? null : row[EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE]);
                schedule.Location = Convert.ToString(row[EventsConstants.FIELDS_EVENTSCHEDULE_LOCATION]);

                return schedule;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts a custom table item to blackout date.
        /// </summary>
        /// <param name="item">Custom table item to convert to blackout date.</param>
        /// <returns>Blackout date object representing the custom table item.</returns>
        public static BlackOutDate ToBlackOutDate(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            BlackOutDate date = row.ToBlackOutDate();
            return date;
        }

        /// <summary>
        /// Converts a datarow to Blackout date object.
        /// </summary>
        /// <param name="row">Datarow to convert to Blackout date object.</param>
        /// <returns>Blackout date object representing the datarow.</returns>
        public static BlackOutDate ToBlackOutDate(this DataRow row)
        {
            try
            {
                BlackOutDate date = new BlackOutDate();

                date.BlackOutDateID = Convert.ToInt32(row[EventsConstants.FIELDS_BLACKOUTDATES_ITEMID]);
                date.Title = Convert.ToString(row[EventsConstants.FIELDS_BLACKOUTDATES_TITLE]);
                date.Date = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_BLACKOUTDATES_BLACKOUTDATE]) ? null : row[EventsConstants.FIELDS_BLACKOUTDATES_BLACKOUTDATE]);
                date.AllowBooking = Convert.ToBoolean(row[EventsConstants.FIELDS_BLACKOUTDATES_ALLOWBOOKING]);

                return date;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts a custom table item to event occurence.
        /// </summary>
        /// <param name="item">Custom table item to convert to event occurence.</param>
        /// <returns>Event occurence object representing the custom table item.</returns>
        public static EventOccurence ToEventOccurence(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            EventOccurence date = row.ToEventOccurence();
            return date;
        }

        /// <summary>
        /// Converts a datarow to event occurence object.
        /// </summary>
        /// <param name="row">Datarow to convert to event occurence object.</param>
        /// <returns>event occurence object representing the datarow.</returns>
        public static EventOccurence ToEventOccurence(this DataRow row)
        {
            try
            {
                EventOccurence occurence = new EventOccurence();
                occurence.OccurenceID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTOCCURENCES_ITEMID]);
                occurence.OccurenceDate = Convert.ToDateTime(row[EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE]);
                occurence.ScheduleID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTOCCURENCES_SCHEDULEID]);
                occurence.StartTime = Convert.ToString(row[EventsConstants.FIELDS_EVENTOCCURENCES_STARTTIME]);
                occurence.EndTime = Convert.ToString(row[EventsConstants.FIELDS_EVENTOCCURENCES_ENDTIME]);
                occurence.RegistrationLimit = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTOCCURENCES_REGISTRTAIONLIMIT]) ? 0 : row[EventsConstants.FIELDS_EVENTOCCURENCES_REGISTRTAIONLIMIT]);
                occurence.Location = Convert.ToString(row[EventsConstants.FIELDS_EVENTOCCURENCES_LOCATION]);
                return occurence;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts a custom table item to event session.
        /// </summary>
        /// <param name="item">Custom table item to convert to event session.</param>
        /// <returns>Event session object representing the custom table item.</returns>
        public static EventSession ToEventSession(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            EventSession date = row.ToEventSession();
            return date;
        }

        /// <summary>
        /// Converts a datarow to event session object.
        /// </summary>
        /// <param name="row">Datarow to convert to event session object.</param>
        /// <returns>event session object representing the datarow.</returns>
        public static EventSession ToEventSession(this DataRow row)
        {
            try
            {
                EventSession session = new EventSession();
                session.SessionID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTSESSIONS_SESSIONID]);
                session.Title = Convert.ToString(row[EventsConstants.FIELDS_EVENTSESSIONS_TITLE]);
                session.StartTime = Convert.ToString(row[EventsConstants.FIELDS_EVENTSESSIONS_STARTTIME]);
                session.EndTime = Convert.ToString(row[EventsConstants.FIELDS_EVENTSESSIONS_ENDTIME]);
                session.OccurenceID = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSESSIONS_OCCURENCEID]) ? null : row[EventsConstants.FIELDS_EVENTSESSIONS_OCCURENCEID]);
                session.ScheduleID = Convert.ToInt32(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTSESSIONS_SCHEDULEID]) ? null : row[EventsConstants.FIELDS_EVENTSESSIONS_SCHEDULEID]);
                return session;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts the custom table item to event object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Event ToEvent(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            Event events = row.ToEvent();
            return events;
        }

        /// <summary>
        /// Converts the data row to event object.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Event ToEvent(this DataRow row)
        {
            try
            {
                Event events = new Event();

                events.ItemID = EmergeValidationHelper.GetInteger(row[EventsConstants.FIELDS_EVENTS_ITEMID], 0);
                events.AttachmentText = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_ATTACHMENTTEXT], string.Empty);
                events.Category = EmergeValidationHelper.GetInteger(row[EventsConstants.FIELDS_EVENTS_CATEGORY], 0);
                events.ContactEmail = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_CONTACTEMAIL], string.Empty);
                events.ContactName = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_CONTACTNAME], string.Empty);
                events.Department = EmergeValidationHelper.GetInteger(row[EventsConstants.FIELDS_EVENTS_DEPARTMENT], 0);
                events.EventDescription = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_EVENTDESCRIPTION], string.Empty);
                events.EventName = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_EVENTNAME], string.Empty);
                events.IsSeries = EmergeValidationHelper.GetBoolean(row[EventsConstants.FIELDS_EVENTS_ISSERIESEVENT], false);
                events.LongTitle = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_LONGTITLE], string.Empty);
                events.ShortTitle = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_SHORTTITLE], string.Empty);
                events.SubCategory = EmergeValidationHelper.GetInteger(row[EventsConstants.FIELDS_EVENTS_SUBCATEGORY], 0);
                events.TeaserText = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_TEASERTEXT], string.Empty);
                events.WebsiteLink = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_WEBSITELINK], string.Empty);
                events.EventType = EmergeValidationHelper.GetString(row[EventsConstants.FIELDS_EVENTS_EVENTTYPE], string.Empty);

                return events;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts the custom table item to 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static EventRegistration ToEventRegistration(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            return row.ToEventRegistration();
        }

        /// <summary>
        /// Converts a data row object to event registration object.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static EventRegistration ToEventRegistration(this DataRow row)
        {
            EventRegistration registration = new EventRegistration();

            registration.Amount = Convert.ToDouble(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT]) ? null : row[EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT]);
            registration.City = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_CITY]);
            registration.Comments = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS]);
            registration.Email = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL]);
            registration.StartTime = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME]);
            registration.FirstName = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_FIRSTNAME]);
            registration.ItemID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_ITEMID]);
            registration.LastName = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_LASTNAME]);
            registration.OccurenceID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID]);
            registration.PaymentDate = Convert.ToDateTime(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE]) ? null : row[EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE]);
            registration.PaymentType = Convert.ToString(Convert.IsDBNull(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE]) ? null : row[EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE]);
            registration.Phone = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_PHONE]);
            registration.ScheduleID = Convert.ToInt32(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID]);
            registration.SelectedSessions = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS]);
            registration.State = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_STATE]);
            registration.Status = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS]);
            registration.StreetAddress = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_STREETADDRESS]);
            registration.Zip = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_ZIP]);
            registration.DiscountCode = Convert.ToString(row[EventsConstants.FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE]);

            return registration;
        }

        /// <summary>
        /// Converts the custom table item to Discount Details
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static DiscountDetails ToDiscountDetails(this CustomTableItem item)
        {
            DataRow row = item.ToDataRow();
            return row.ToDiscountDetails();
        }

        /// <summary>
        /// Converts a data row object to Discount Details object.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static DiscountDetails ToDiscountDetails(this DataRow row)
        {
            DiscountDetails discountDetail = new DiscountDetails();

            discountDetail.DiscountID = Convert.ToInt32(row[EventsConstants.FIELDS_DISCOUNTS_DISCOUNTID]);
            discountDetail.DiscountCode = Convert.ToString(row[EventsConstants.FIELDS_DISCOUNTS_DISCOUNTCODE]);
            discountDetail.DiscountFactor = Convert.ToDouble(row[EventsConstants.FIELDS_DISCOUNTS_DISCOUNTFACTOR]);
            discountDetail.DiscountType = Convert.ToString(row[EventsConstants.FIELDS_DISCOUNTS_DISCOUNTTYPE]);

            return discountDetail;
        }

        /// <summary>
        /// Gets the collection of enum values.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        /// <summary>
        /// Returns whether the blackout date clashes with any of the event occurences in the system.
        /// </summary>
        /// <param name="blackOutDate">blackout date to be checked.</param>
        /// <returns>true, if the blackout date clashes, else false.</returns>
        public static bool IsBlackOutDateClashed(DateTime blackOutDate)
        {
            IBlackoutDateService blackOutDateService = _factory.GetTypeInstance<IBlackoutDateService>();
            return blackOutDateService.HasOccurencesOnDate(blackOutDate);
        }

        /// <summary>
        /// Returns the list of blackout dates those clash with the occurences in the occurencesList.
        /// </summary>
        /// <param name="occurenceList">List of occurences to check for clashes</param>
        /// <returns>list of blackout dates.</returns>
        public static List<BlackOutDate> GetClashedDatesForOccurences(List<EventOccurence> occurenceList)
        {
            try
            {
                IBlackoutDateService blackOutDateService = _factory.GetTypeInstance<IBlackoutDateService>();
                return blackOutDateService.GetBlackOutDatesInOccurences(occurenceList).ToList<BlackOutDate>();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Saves the schedule occurences.
        /// </summary>
        /// <param name="eventOccurrences">occurences to be saved.</param>
        public static void SaveEventScheduleOccurences(List<EventOccurence> eventOccurrences)
        {
            try
            {
                IEventOccurenceService _occurenceService = _factory.GetTypeInstance<IEventOccurenceService>();
                _occurenceService.SaveEventOccurences(eventOccurrences);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes all the event occurences of a event schedule.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule for which occurences are to be deleted.</param>
        public static void DeleteEventScheduleOccurences(int scheduleID)
        {
            try
            {
                IEventOccurenceService occurenceService = _factory.GetTypeInstance<IEventOccurenceService>();
                occurenceService.DeleteEventScheduleOccurences(scheduleID);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Deletes the event sessions of the event schedule.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        public static void DeleteEventSessionsByScheduleID(int scheduleID)
        {
            ISessionService sessionService = _factory.GetTypeInstance<ISessionService>();
            sessionService.DeleteSessionsByScheduleID(scheduleID);
        }

        /// <summary>
        /// Deletes the event sessions of the event occurene.
        /// </summary>
        /// <param name="occurenceID">id of the occurence.</param>
        public static void DeleteEventSessionsByOccurenceID(int occurenceID)
        {
            ISessionService sessionService = _factory.GetTypeInstance<ISessionService>();
            sessionService.DeleteSessionsByOccurenceID(occurenceID);
        }

        public static void DeleteSessionByID(int sessionID)
        {
            ISessionService sessionService = _factory.GetTypeInstance<ISessionService>();
            sessionService.DeleteSessionByID(sessionID);
        }

        /// <summary>
        /// Deletes the event and its related data.
        /// </summary>
        /// <param name="eventID">id of the event.</param>
        public static void DeleteEventByEventID(int eventID)
        {
            IEventService eventService = _factory.GetTypeInstance<IEventService>();
            eventService.DeleteEventByEventID(eventID);
        }

        public static void DeleteEventScheduleByID(int scheduleID)
        {
            IEventScheduleService scheduleService = _factory.GetTypeInstance<IEventScheduleService>();
            scheduleService.DeleteEventScheduleByID(scheduleID);
        }

        /// <summary>
        /// Deletes the discount details of the event schedule.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        public static void DeleteDiscountDetailsByScheduleID(int scheduleID)
        {
            IDiscountService discountService = _factory.GetTypeInstance<IDiscountService>();
            discountService.DeleteDiscountsByScheduleID(scheduleID);
        }

        /// <summary>
        /// Builds the list of occurences for the event schedule.
        /// </summary>
        /// <param name="id">id of the event schedule.</param>
        /// <param name="excludedDates">list of dates to be excluded from occurences.</param>
        /// <returns>list of occurences.</returns>
        public static List<EventOccurence> BuildOccurences(int id, List<DateTime> excludedDates)
        {
            try
            {
                IEventOccurenceService occurenceService = _factory.GetTypeInstance<IEventOccurenceService>();
                return occurenceService.BuildOccurencesForSchedule(id, excludedDates).ToList<EventOccurence>();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the list of sessions for event occurence.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>list of event sessions.</returns>
        public static List<EventSession> GetSessionsByOccurenceID(int occurenceID)
        {
            ISessionService service = _factory.GetTypeInstance<ISessionService>();
            List<EventSession> sessions = service.GetSessionsByOccurence(occurenceID).ToList<EventSession>();
            return sessions;
        }

        /// <summary>
        /// Updates the event schedule. 
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <param name="occurences"></param>
        public static void UpdateSelectedDatesForSchedule(int scheduleID)
        {
            try
            {
                IEventScheduleService scheduleService = _factory.GetTypeInstance<IEventScheduleService>();
                scheduleService.UpdateScheduleDateFields(scheduleID);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks whether the two date ranges overlaps with each other.
        /// </summary>
        /// <param name="source">source date range.</param>
        /// <param name="target">target date range.</param>
        /// <param name="includeEdge">determines whether the check should also be made for the edges of the source and target date ranges.</param>
        /// <returns>true, if the date ranges overlaps with each other.</returns>
        public static bool OverlapsWith(this DateRange source, DateRange target, bool includeEdge)
        {
            bool result = false;
            if (source.StartDateTime == target.StartDateTime || source.EndDateTime == target.EndDateTime)
                result = true;
            if (includeEdge)
            {
                if (source.StartDateTime < target.StartDateTime && source.EndDateTime >= target.StartDateTime)
                    result = true;
                if (source.StartDateTime > target.StartDateTime && source.StartDateTime < target.EndDateTime)
                    result = true;
                if (source.StartDateTime >= target.EndDateTime)
                    result = true;
            }
            else
            {
                if (source.StartDateTime < target.StartDateTime && source.EndDateTime > target.StartDateTime)
                    result = true;
                if (source.StartDateTime > target.StartDateTime && source.StartDateTime < target.EndDateTime)
                    result = true;
            }

            return result;
        }

        /// <summary>
        /// Updates the start time for the registration.
        /// </summary>
        /// <param name="itemID">item if of the event registration.</param>
        /// <param name="customtableID">Custom table id of the event registration table</param>
        public static void UpdateStartTimeForRegistration(int itemID, int customtableID)
        {
            try
            {
                IEventRegistrationService registrationService = _factory.GetTypeInstance<IEventRegistrationService>();
                registrationService.UpdateStartTimeForRegistration(itemID);
            }
            catch (CustomTableItemNotFoundException ex)
            {
                throw ex;
            }
            catch (CustomTableNotExistsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateRegistrationForVolunteerUser(int registrationID, int userID)
        {
            IEventRegistrationService registrationService = _factory.GetTypeInstance<IEventRegistrationService>();
            registrationService.UpdateRegistrationForVolunteerUser(registrationID, userID);
        }

        /// <summary>
        /// Updates the occurenceID for the event registration.
        /// </summary>
        /// <param name="itemID">id of the event registration.</param>
        /// <param name="occurenceID">id of the event occurence.</param>
        public static void UpdateOccurenceForRegistration(int itemID, int occurenceID)
        {
            try
            {
                IEventRegistrationService registrationService = _factory.GetTypeInstance<IEventRegistrationService>();
                registrationService.UpdateOccurenceForRegistration(itemID, occurenceID);
            }
            catch (CustomTableItemNotFoundException ex)
            {
                throw ex;
            }
            catch (CustomTableNotExistsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the start time for the registration.
        /// </summary>
        /// <param name="itemID">item if of the event registration.</param>
        /// <param name="customtableID">Custom table id of the event registration table</param>
        public static List<EventOccurence> GetOccurencesByScheduleID(int scheduleID)
        {
            try
            {
                List<EventOccurence> occurences = new List<EventOccurence>();
                IEventOccurenceService _service = _factory.GetTypeInstance<IEventOccurenceService>();
                occurences = _service.GetOccurencesByScheduleID(scheduleID).ToList<EventOccurence>();
                return occurences.OrderBy(s => s.OccurenceDate).ToList<EventOccurence>();
            }
            catch (CustomTableItemNotFoundException ex)
            {
                throw ex;
            }
            catch (CustomTableNotExistsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the event occurence by id.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>returns the event occurence object.</returns>
        public static EventOccurence GetEventOccurenceByID(int occurenceID)
        {
            IEventOccurenceService _occurenceService = _factory.GetTypeInstance<IEventOccurenceService>();
            EventOccurence occurence = _occurenceService.GetEventOccurenceByID(occurenceID);
            return occurence;
        }

        /// <summary>
        /// Checks whether the registration limit is reached for this occurence.
        /// </summary>
        /// <param name="occurence"></param>
        /// <returns></returns>
        public static bool IsRegistrationLimitReached(this EventOccurence occurence)
        {
            if (occurence.IsSeries)
                return occurence.Schedule.RegistrationLimit <= occurence.Schedule.Registrations.Count;
            else
                return occurence.RegistrationLimit <= occurence.Registrations.Count;
        }

        /// <summary>
        /// Gets the schedule object for the occurence.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence</param>
        /// <returns></returns>
        public static EventSchedule GetScheduleByOccurenceID(int occurenceID)
        {
            IEventScheduleService scheduleService = _factory.GetTypeInstance<IEventScheduleService>();
            EventSchedule schedule = scheduleService.GetEventScheduleByOccurenceID(occurenceID);
            return schedule;
        }

        /// <summary>
        /// Gets the events object for the schedule
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public static Event GetEventByScheduleID(int scheduleID)
        {
            IEventService eventService = _factory.GetTypeInstance<IEventService>();
            Event eventItem = eventService.GetEventByScheduleID(scheduleID);
            return eventItem;
        }

        /// <summary>
        /// Gets the event schedule object for the schedule.
        /// </summary>
        /// <param name="scheduleID">id of the schedule.</param>
        /// <returns></returns>
        public static EventSchedule GetScheduleByScheduleID(int scheduleID)
        {
            IEventScheduleService scheduleService = _factory.GetTypeInstance<IEventScheduleService>();
            EventSchedule schedule = scheduleService.GetEventScheduleByID(scheduleID);
            return schedule;
        }

        /// <summary>
        /// Gets all the registrations for the event schedule.
        /// </summary>
        /// <param name="scheduleID">id of the event schedule.</param>
        /// <returns>List of event registrations.</returns>
        public static List<EventRegistration> GetAllRegistrationsByScheduleID(int scheduleID, EventRegistrationStatus status)
        {
            IEnumerable<EventRegistration> registrations = _factory.GetTypeInstance<IEventRegistrationService>().GetAllRegistrationsByScheduleID(scheduleID, status);
            return registrations.ToList<EventRegistration>();
        }



        public static List<EventRegistration> GetRegistrationsByEventID(int eventID, EventRegistrationStatus status)
        {
            IEnumerable<EventRegistration> registrations = _factory.GetTypeInstance<IEventRegistrationService>().GetRegistrationsByEventID(eventID, status);
            return registrations.ToList<EventRegistration>();
        }



        /// <summary>
        /// Gets the registrations for the occurence.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>List of event registrations.</returns>
        public static List<EventRegistration> GetRegistrationsByOccurenceID(int occurenceID, EventRegistrationStatus status)
        {
            IEnumerable<EventRegistration> registrations = _factory.GetTypeInstance<IEventRegistrationService>().GetRegistrationsByOccurenceID(occurenceID, status);
            return registrations.ToList<EventRegistration>();
        }

        /// <summary>
        /// Gets the event registration object by id.
        /// </summary>
        /// <param name="registrationID">id of the object</param>
        /// <returns>Event registration object.</returns>
        public static EventRegistration GetEventRegistrationByID(int registrationID)
        {
            return _factory.GetTypeInstance<IEventRegistrationService>().GetEventRegistrationByID(registrationID);
        }

        /// <summary>
        /// Deletes the event occurence by occurence ID.
        /// </summary>
        /// <param name="occurenceID">id of the event occurence.</param>
        /// <returns>event occurence object.</returns>
        public static bool DeleteEventOccurenceByID(int occurenceID)
        {
            return _factory.GetTypeInstance<IEventOccurenceService>().DeleteEventOccurenceByID(occurenceID);
        }
        public static bool DeleteEventOccurenceByIDForFinalizeSchedule(int occurenceID)
        {
            return _factory.GetTypeInstance<IEventOccurenceService>().DeleteEventOccurenceByIDForFinalizeSchedule(occurenceID);
        }
        public static EventOccurence GetEventOccurenceByScheduleAndOccurenceDate(DateTime occurenceDate, int scheduleID)
        {
            return _factory.GetTypeInstance<IEventOccurenceService>().GetEventOccurenceByScheduleAndOccurenceID(occurenceDate, scheduleID);
        }

        public static void MoveRegistrationsForSchedule(int scheduleID, IDictionary<string, string> registrations)
        {
            IEventRegistrationService registrationService = _factory.GetTypeInstance<IEventRegistrationService>();
            registrationService.MoveRegistrationsForSchedule(scheduleID, registrations);
        }

        public static double GetDiscountedCostbyCodeAndScheduleID(string discountCode, int scheduleID)
        {
            IDiscountService discountService = _factory.GetTypeInstance<IDiscountService>();
            return discountService.GetDiscountedCostbyCodeAndScheduleID(discountCode, scheduleID);
        }

        public static bool SendRegistrationEmail(EventRegistration registration, RegistrationEmailMode mode, string[,] macros = null)
        {
            try
            {
                if (registration.Status == EventRegistrationStatus.CANCELLED.ToString())
                    mode = (registration.Occurence.Schedule.Event.EventType == EventsConstants.EVENTTYPE_GENERAL) ? RegistrationEmailMode.GENERAL_CANCELLED : RegistrationEmailMode.VOLUNTEER_CANCELLED;

                bool allowSend = allowSendEmail(mode);
                if (!allowSend)
                    return true;

                EmailMessageInfo messageInfo = new EmailMessageInfo();
                messageInfo.Recipients = registration.Email;
                if (macros == null)
                    macros = GetRegistrationMacros(registration);

                string templateName = getTemplateName(mode);
                if (macros.Length > 0)
                    EmailService.SendEmailUsingTemplate(messageInfo, templateName, macros, true);
                else
                {
                    EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, "The macros for email is empty");
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException();
            }
            return true;
        }
        public static bool SendRegistrationEmailUsingSelectedTemplate(EventRegistration registration, RegistrationEmailMode mode, string adminEmailTemplateName, string userEmailTemplateName, string[,] macros = null)
        {
            try
            {
                if (registration.Status == EventRegistrationStatus.CANCELLED.ToString())
                    mode = (registration.Occurence.Schedule.Event.EventType == EventsConstants.EVENTTYPE_GENERAL) ? RegistrationEmailMode.GENERAL_CANCELLED : RegistrationEmailMode.VOLUNTEER_CANCELLED;
                bool allowSend = allowSendEmail(mode);
                if (!allowSend)
                    return true;

                SendAdminEmail(registration, adminEmailTemplateName, macros);
                SendUserEmail(registration, userEmailTemplateName, macros);
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException();
            }
            return true;
        }
        public static void SendAdminEmail(EventRegistration registration, string adminEmailTemplateName, string[,] macros = null)
        {
            try
            {
                EmailMessageInfo messageInfo = new EmailMessageInfo();
                messageInfo.Recipients = EmergeResHelper.GetString("EventsCalendarAdminEmail");
                if (macros == null)
                    macros = GetRegistrationMacros(registration);
                if (macros.Length > 0)
                    EmailService.SendEmailUsingTemplate(messageInfo, adminEmailTemplateName, macros, true);
                else
                {
                    EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, "The macros for email is empty");
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException();
            }
        }
        public static bool SendUserEmail(EventRegistration registration, string userEmailTemplateName, string[,] macros = null)
        {
            try
            {
                EmailMessageInfo messageInfo = new EmailMessageInfo();
                messageInfo.Recipients = registration.Email;
                if (macros == null)
                    macros = GetRegistrationMacros(registration);
                if (macros.Length > 0)
                    EmailService.SendEmailUsingTemplate(messageInfo, userEmailTemplateName, macros, true);
                else
                {
                    EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, "The macros for email is empty");
                }
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("EventsCalendarHelper:SendRegistrationEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                throw new EmailSendException();
            }
            return true;
        }
        public static Event GetEventByEventID(int eventID)
        {
            IEventService eventservice = _factory.GetTypeInstance<IEventService>();
            return eventservice.GetEventByEventID(eventID);
        }

        public static string GetEventTypeofEvent(int eventID)
        {
            Event events = GetEventByEventID(eventID);
            return events.EventType;
        }

        public static bool ValidateSessions(List<EventSession> selectedSessions)
        {
            bool result = true;
            foreach (EventSession source in selectedSessions)
            {
                if (result == true)
                {
                    DateRange sourceRange = new DateRange(Convert.ToDateTime(source.StartTime), Convert.ToDateTime(source.EndTime));
                    foreach (EventSession target in selectedSessions)
                    {
                        if (target.SessionID == source.SessionID)
                            continue;
                        DateRange targetRange = new DateRange(Convert.ToDateTime(target.StartTime), Convert.ToDateTime(target.EndTime));
                        if (sourceRange.OverlapsWith(targetRange, false))
                        {
                            result = false;
                            break;
                        }
                    }
                }
                else
                    break;
            }

            return result;

        }

        public static bool SendCartRegistrationEmail(List<EventRegistration> registrations, string emailEventCartHtml, RegistrationEmailMode mode)
        {
            EmailMessageInfo messageInfo = new EmailMessageInfo();
            messageInfo.Recipients = registrations[0].Email;
            messageInfo.IsBodyHtml = true;
            string[,] macros = GetRegistrationMacros(registrations[0]);
            string[,] macroswithCartHtml = new string[(macros.GetLength(0)) + 1, 2];

            for (int counter = 0; counter < macros.GetLength(0); counter++)
            {
                macroswithCartHtml[counter, 0] = macros[counter, 0];
                macroswithCartHtml[counter, 1] = macros[counter, 1];
            }

            macroswithCartHtml[macros.GetLength(0) - 1, 0] = "CartHtml";
            macroswithCartHtml[macros.GetLength(0) - 1, 1] = emailEventCartHtml;

            if (registrations[0].Status == EventRegistrationStatus.CANCELLED.ToString())
                mode = RegistrationEmailMode.GENERAL_CANCELLED;
            string templateName = getTemplateName(mode);

            EmailService.SendEmailUsingTemplate(messageInfo, templateName, macroswithCartHtml, false);
            return true;
        }




        public static string ToHtml(this Dictionary<string, object> Data)
        {
            StringBuilder html = new StringBuilder();

            foreach (KeyValuePair<string, object> item in Data)
            {
                html.Append("<b>" + item.Key.ToString() + ": </b>" + item.Value.ToString() + "<br/>");
            }

            return html.ToString();
        }

        public static List<EventSession> GetSessionsByCondition(string whereCondition)
        {
            ISessionService sessionService = _factory.GetTypeInstance<ISessionService>();
            return sessionService.GetSessionsByCondition(whereCondition).ToList<EventSession>();
        }

        public static bool IsDiscountCodeUsed(string email, EventOccurence occurence, string discountCode)
        {
            if (!string.IsNullOrEmpty(discountCode))
                return occurence.Schedule.Registrations.Any(a => a.Email == email && !String.IsNullOrEmpty(a.DiscountCode));

            return false;
        }

        public static bool IsRegistrationExist(this EventOccurence occurence, string email, int registrationID)
        {
            if (occurence.Schedule.AllowDuplicationRegistrations)
                return false;
            //return (occurence.Registrations.Find(a => a.Email == email && a.ItemID != registrationID) != null);
            if (occurence.Schedule.Occurences.Any(a => a.Registrations.Find(x => x.Email == email && x.ItemID != registrationID) != null))
                return true;

            return false;
        }

        public static bool IsDiscountCodeValid(string discountCode, int scheduleID)
        {
            try
            {
                IDiscountService service = _factory.GetTypeInstance<IDiscountService>();
                DiscountDetails details = service.GetDicountDetailsByCodeAndScheduleID(discountCode, scheduleID);
                if (details != null)
                    return true;
            }
            catch (InvalidDiscountCodeException)
            {
                return false;
            }
            return false;
        }

        public static bool IsEventCartEnabled()
        {
            IEventService service = _factory.GetTypeInstance<IEventService>();
            return service.IsCartEnabled();
        }

        public static SaveRegistrationStatus SaveRegistrations(List<Dictionary<string, object>> registrations, ref List<int> savedItemIds)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            return service.SaveEventRegistrations(registrations, ref savedItemIds);
        }

        public static SaveRegistrationStatus ValidateRegistrations(List<Dictionary<string, object>> registrations)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            return service.ValidateRegistrations(registrations);
        }

        public static List<Dictionary<string, object>> GetRegistrationsFromCart()
        {
            List<Dictionary<string, object>> registrations = new List<Dictionary<string, object>>();
            foreach (DataRowView cartItem in CartService.GetItems())
            {
                Dictionary<string, object> cartItemData = new Dictionary<string, object>();

                foreach (DataColumn cartItemColumn in cartItem.Row.Table.Columns)
                {
                    cartItemData.Add(cartItemColumn.ColumnName, cartItem.Row[cartItemColumn]);
                }

                foreach (KeyValuePair<string, object> registrationInfoItem in (Dictionary<string, object>)EmergeSessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART))
                {
                    cartItemData.Add(registrationInfoItem.Key, registrationInfoItem.Value);
                }

                EnsureDefaultFields(cartItemData);


                registrations.Add(cartItemData);

            }
            return registrations;
        }

        private static void EnsureDefaultFields(Dictionary<string, object> registrationData)
        {
            if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS))
            // registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS, EventRegistrationStatus.CONFIRMED.ToString());
            {
                registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS, EventRegistrationStatus.CONFIRMED.ToString());
            }


            int occurrenceID = EmergeValidationHelper.GetInteger(registrationData[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0);
            EventOccurence occurrence = EventsCalendarHelper.GetEventOccurenceByID(occurrenceID);

            if (occurrence.Schedule.IsPaidSchedule)
            {
                if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE))
                    registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE, "ONLINE");
                if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE))
                    registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTDATE, DateTime.Now);
            }
            else
            {
                if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE))
                    registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE, string.Empty);
            }

            if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS))
                registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_COMMENTS, string.Empty);
            if (!registrationData.ContainsKey(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME))
                registrationData.Add(EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME, occurrence.StartTime);
        }

        private static string getTemplateName(RegistrationEmailMode mode)
        {
            string templateName = string.Empty;
            switch (mode)
            {
                case RegistrationEmailMode.VOLUNTEER_INSERT:
                case RegistrationEmailMode.VOLUNTEER_INSERT_UI:
                case RegistrationEmailMode.GENERAL_INSERT:
                case RegistrationEmailMode.GENERAL_INSERT_UI:
                    templateName = String.Format(EventsConstants.EMAILTEMPLATE_EVENTREGISTRATION_CONFIRMATION, EmergeCMSContext.CurrentSiteName);
                    break;
                case RegistrationEmailMode.GENERAL_UPDATE:
                case RegistrationEmailMode.VOLUNTEER_UPDATE:
                    templateName = String.Format(EventsConstants.EMAILTEMPLATE_EVENTREGISTRATION_UPDATE, EmergeCMSContext.CurrentSiteName);
                    break;
                case RegistrationEmailMode.VOLUNTEER_CANCELLED:
                case RegistrationEmailMode.GENERAL_CANCELLED:
                    templateName = String.Format(EventsConstants.EMAILTEMPLATE_EVENTREGISTRATION_CANCELLED, EmergeCMSContext.CurrentSiteName);
                    break;
                case RegistrationEmailMode.GENERAL_DELETE:
                case RegistrationEmailMode.VOLUNTEER_DELETE:
                    templateName = String.Format(EventsConstants.EMAILTEMPLATE_EVENTREGISTRATION_DELETED, EmergeCMSContext.CurrentSiteName);
                    break;
                case RegistrationEmailMode.CARTINSERT:
                    templateName = String.Format(EventsConstants.EMAILTEMPLATE_EVENTREGISTRATION_CARTCONFIRMATION, EmergeCMSContext.CurrentSiteName);
                    break;
            }
            return templateName;
        }

        public static List<EventRegistration> GetRegistrationsBySession(int sessionID, EventRegistrationStatus status)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            return service.GetRegistrationsBySessionID(sessionID, status).ToList<EventRegistration>();
        }

        public static string[,] GetRegistrationMacros(EventRegistration registration)
        {
            string[,] macros = new string[0, 2];
            string queryName = string.Format(EventsConstants.QUERY_GETREGISTRATIONDETAILSFOREMAIL, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@RegistrationID", registration.ItemID);
            DataSet registrationDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);

            if (!EmergeDataHelper.DataSourceIsEmpty(registrationDS))
            {
                DataRow row = registrationDS.Tables[0].Rows[0];
                macros = new string[row.Table.Columns.Count, 2];
                DataColumnCollection columns = row.Table.Columns;
                for (int i = 0; i < columns.Count; i++)
                {
                    macros[i, 0] = columns[i].ColumnName;
                    macros[i, 1] = EmergeValidationHelper.GetString(row[columns[i].ColumnName], string.Empty);
                }
            }


            //macros[0, 0] = EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE;
            //macros[0, 1] = registration.Occurence.OccurenceDate.ToString(Constants.EMERGE_DATEFORMAT);
            //macros[1, 0] = EventsConstants.FIELDS_EVENTS_LONGTITLE;
            //macros[1, 1] = registration.Occurence.Schedule.Event.LongTitle;
            //macros[2, 0] = EventsConstants.FIELDS_EVENTOCCURENCES_STARTTIME;
            //macros[2, 1] = registration.Occurence.StartTime;
            //macros[3, 0] = EventsConstants.FIELDS_EVENTREGISTRATIONS_FIRSTNAME;
            //macros[3, 1] = registration.FirstName;
            //macros[4, 0] = EventsConstants.FIELDS_EVENTREGISTRATIONS_LASTNAME;
            //macros[4, 1] = registration.LastName;
            //macros[5, 0] = EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS;
            //macros[5, 1] = registration.Status;

            return macros;
        }

        public static bool IsScheduleExpired(int scheduleID)
        {
            EventSchedule schedule = GetScheduleByScheduleID(scheduleID);
            if (schedule.Occurences.Count == 0 || schedule.Occurences.Last().OccurenceDate < DateTime.Now.Date)
                return true;
            return false;
        }

        public static bool IsOccurenceExpired(int occurenceID)
        {
            EventOccurence occurrence = GetEventOccurenceByID(occurenceID);
            if (occurrence.OccurenceDate < DateTime.Now.Date)
                return true;
            return false;
        }

        public static List<EventSchedule> GetEventSchedulesByEventID(int eventID)
        {
            IEventScheduleService service = _factory.GetTypeInstance<IEventScheduleService>();
            return service.GetEventSchedulesByEventID(eventID).ToList<EventSchedule>();
        }

        public static bool IsEventExpired(int eventID)
        {
            List<EventSchedule> schedules = GetEventSchedulesByEventID(eventID);
            foreach (EventSchedule schedule in schedules)
            {
                if (schedule.Occurences.Count > 0 && schedule.Occurences.Last().OccurenceDate >= DateTime.Now.Date)
                    return false;
            }
            return true;
        }

        public static void DeleteRegistrationsByScheduleID(int scheduleID)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            service.DeleteRegistrationsByScheduleID(scheduleID);
        }

        public static void DeleteRegistrationsByOccurrenceID(int occurrenceID)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            service.DeleteRegistrationsByOccurrenceID(occurrenceID);
        }

        private static bool allowSendEmail(RegistrationEmailMode mode)
        {
            IEventRegistrationService service = _factory.GetTypeInstance<IEventRegistrationService>();
            return service.AllowEmailSend(mode);
        }

        #endregion

        #region Private methods

        private static DaysOfWeek getDaysOfWeekOptionsForSchedule(string value)
        {
            int daysOfWeek = 0;
            string[] values = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string option in values)
            {
                daysOfWeek |= (int)(DaysOfWeek)Enum.Parse(typeof(DaysOfWeek), option);
            }
            return (DaysOfWeek)daysOfWeek;
        }

        private static MonthlyInterval getMonthlyIntervalOptionsForSchedule(string value)
        {
            int weekOfMonth = 0;
            string[] values = value.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string option in values)
            {
                weekOfMonth |= (int)(MonthlyInterval)Enum.Parse(typeof(MonthlyInterval), option);
            }

            return (MonthlyInterval)weekOfMonth;

        }

        private static List<DateTime> getSelectedDates(string selectedDates)
        {
            string[] values = selectedDates.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

            List<DateTime> list = new List<DateTime>();
            foreach (string value in values)
            {
                DateTime date = Convert.ToDateTime(value.Trim());
                list.Add(date);
            }
            list.Sort();
            return list;
        }

        #endregion


    }

}
