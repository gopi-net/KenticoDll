using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using System.Data;
using Bluespire.Emerge.CommonService;
using CMS.MacroEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS;
using Bluespire.Emerge.Components.EventsCalendar.CustomMacros;

// Makes all methods in the 'CustomMacroMethods' container class available for string objects
[assembly: RegisterExtension(typeof(CustomMacroMethods), typeof(string))]

// Registers methods from the 'CustomMacroMethods' container into the "String" macro namespace
[assembly: RegisterExtension(typeof(CustomMacroMethods), typeof(StringNamespace))]
namespace Bluespire.Emerge.Components.EventsCalendar.CustomMacros
{

    public class CustomMacroMethods : MacroMethodContainer
    {
        //public static void RegisterMacroMethods()
        //{
        //    MacroMethod isPaidEventScheduleMethod = new MacroMethod("IsPaidEventSchedule", IsPaidEventSchedule)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_COMMENT_ISPAIDEVENTSCHEDULE),
        //        Type = typeof(bool),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(isPaidEventScheduleMethod);
        //    MacroMethod isSeriesScheduleMethod = new MacroMethod("IsSeriesOccurence", IsSeriesOccurence)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_COMMENT_ISSERIESOCCURENCE),
        //        Type = typeof(bool),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(isSeriesScheduleMethod);
        //    MacroMethod hasSessionsMethod = new MacroMethod("HasSessions", HasSessions)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_COMMENT_ALLOWSESSIONS),
        //        Type = typeof(bool),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(hasSessionsMethod);
        //    MacroMethod allowRegistrationsMethod = new MacroMethod("AllowsRegistrations", AllowsRegistrations)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_COMMENT_ALLOWREGISTRATIONS),
        //        Type = typeof(bool),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(allowRegistrationsMethod);
        //    MacroMethod eventTypeMethod = new MacroMethod("EventType", EventType)
        //    {
        //        Comment = ResHelper.GetString("Returns the event type of the event."),
        //        Type = typeof(string),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(eventTypeMethod);
        //    MacroMethod eventTypeofScheduleMethod = new MacroMethod("EventTypeOfSchedule", EventTypeOfSchedule)
        //    {
        //        Comment = ResHelper.GetString("Returns the event type of the event."),
        //        Type = typeof(string),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 1
        //    };
        //    MacroMethods.RegisterMethod(eventTypeofScheduleMethod);
        //    MacroMethod getEventRegistrations = new MacroMethod("GetCartEventRegistrations", GetCartEventRegistrations)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_COMMENT_GETCARTEVENTREGISTRATIONS),
        //        Type = typeof(DataSet),
        //        AllowedTypes = new List<Type>() { },
        //        MinimumParameters = 0
        //    };
        //    MacroMethods.RegisterMethod(getEventRegistrations);
        //    MacroMethod hasScheduleForEvent = new MacroMethod("HasScheduleForEvent", HasScheduleForEvent)
        //    {
        //        Comment = ResHelper.GetString(EventsConstants.STRINGCODE_HASSCHEDULEFOREVENTCOMMENT),
        //        Type = typeof(bool),
        //        AllowedTypes = new List<Type>() { typeof(int) },
        //        MinimumParameters = 0
        //    };
        //    MacroMethods.RegisterMethod(hasScheduleForEvent);
        //}

        [MacroMethod(typeof(string), "Returns the event type of the event.", 1)]
        [MacroMethodParam(0, "EventID", typeof(int), "The event id of the event")]
        public static object EventType(params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return EventType(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string EventType(int eventID)
        {
            try
            {
                Event events = EventsCalendarHelper.GetEventByEventID(eventID);
                return events.EventType;
            }
            catch (CustomTableItemNotFoundException)
            {
                return string.Empty;
            }
            catch (CustomTableIdNotFoundException)
            {
                return string.Empty;
            }
            catch (CustomTableNotExistsException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        [MacroMethod(typeof(string), "HasScheduleForEvent", 1)]
        [MacroMethodParam(0, "HasScheduleForEvent", typeof(int), "hasScheduleForEvent")]
        public static object HasScheduleForEvent(EvaluationContext context,params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return HasScheduleForEvent(EmergeValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string HasScheduleForEvent(int eventID)
        {
            try
            {
                string className = String.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, EmergeCMSContext.CurrentSiteName);
                string whereCondition = EventsConstants.FIELDS_EVENTSCHEDULE_EVENTID + " = " + eventID.ToString();
                DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);
                if (!EmergeDataHelper.DataSourceIsEmpty(ds))
                    if (ds.Tables[0].Rows.Count > 0)
                        return "true";
                return "false";
            }
            catch (CustomTableItemNotFoundException)
            {
                return "false";
            }
            catch (CustomTableIdNotFoundException)
            {
                return "false";
            }
            catch (CustomTableNotExistsException)
            {
                return "false";
            }
            catch (Exception)
            {
                return "false";
            }
        }


        [MacroMethod(typeof(string), "isPaidEventSchedule", 1)]
        [MacroMethodParam(0, "isPaidEventSchedule", typeof(int), "isPaidEventSchedule")]
         public static object IsPaidEventSchedule(EvaluationContext context,params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return IsPaidEventSchedule(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string IsPaidEventSchedule(int scheduleID)
        {
            try
            {
                EventSchedule schedule = EventsCalendarHelper.GetScheduleByScheduleID(scheduleID);
                return schedule.IsPaidSchedule ? "true" : "false";
            }
            catch (CustomTableItemNotFoundException)
            {
                return "false";
            }
            catch (CustomTableIdNotFoundException)
            {
                return "false";
            }
            catch (CustomTableNotExistsException)
            {
                return "false";
            }
            catch (Exception)
            {
                return "false";
            }
        }



        [MacroMethod(typeof(string), "IsSeriesOccurence", 1)]
        [MacroMethodParam(0, "IsSeriesOccurence", typeof(int), "isSeriesOccurence")]
        public static object IsSeriesOccurence(EvaluationContext context, params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return IsSeriesOccurence(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string IsSeriesOccurence(int occurenceID)
        {
            try
            {
                EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
                return occurence.IsSeries ? "false" : "true";
            }
            catch (CustomTableItemNotFoundException)
            {
                return "false";
            }
            catch (CustomTableIdNotFoundException)
            {
                return "false";
            }
            catch (CustomTableNotExistsException)
            {
                return "false";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        [MacroMethod(typeof(string), "HasSessions", 1)]
        [MacroMethodParam(0, "HasSessions", typeof(int), "hasSessions")]
        public static object HasSessions(EvaluationContext context, params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return HasSessions(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string HasSessions(int occurenceID)
        {
            try
            {
                EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
                return occurence.Schedule.HasSessions? "true" : "false";
            }
            catch (CustomTableItemNotFoundException)
            {
                return "false";
            }
            catch (CustomTableIdNotFoundException)
            {
                return "false";
            }
            catch (CustomTableNotExistsException)
            {
                return "false";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        [MacroMethod(typeof(string), "AllowsRegistrations", 1)]
        [MacroMethodParam(0, "AllowsRegistrations", typeof(int), "AllowsRegistrations")]
        public static object AllowsRegistrations(EvaluationContext context, params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return AllowsRegistrations(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string AllowsRegistrations(int occurenceID)
        {
            try
            {
                EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
                return occurence.Schedule.NeedRegistrations?"true":"false";
            }
            catch (CustomTableItemNotFoundException)
            {
                return "false";
            }
            catch (CustomTableIdNotFoundException)
            {
                return "false";
            }
            catch (CustomTableNotExistsException)
            {
                return "false";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        public static DataSet GetCartEventRegistrations(params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 0:
                    return GetRegistrationsForIds(SessionHelper.GetValue(EventsConstants.SESSIONKEY_CARTEVENTREGISTRATIONIDS).ToString());
                default:
                    throw new NotSupportedException();
            }
        }

        private static DataSet GetRegistrationsForIds(string registrationIDs)
        {

            string queryName = string.Format(EventsConstants.QUERY_GETCARTEVENTREGISTRATIONS, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@SelectedRegistrations", registrationIDs);
            return ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);

        }



        [MacroMethod(typeof(string), "Returns the event type of the event.", 1)]
        [MacroMethodParam(0, "ScheduleID", typeof(int), "The scheduleID of the event")]
        public static object EventTypeOfSchedule(EvaluationContext context, params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return EventTypeOfSchedule(ValidationHelper.GetInteger(parameters[0], 0));
                default:
                    throw new NotSupportedException();
            }
        }

        public static string EventTypeOfSchedule(int scheduleID)
        {
            try
            {
                Event events = EventsCalendarHelper.GetEventByScheduleID(scheduleID);
                return events.EventType;
            }
            catch (CustomTableItemNotFoundException)
            {
                return string.Empty;
            }
            catch (CustomTableIdNotFoundException)
            {
                return string.Empty;
            }
            catch (CustomTableNotExistsException)
            {
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
