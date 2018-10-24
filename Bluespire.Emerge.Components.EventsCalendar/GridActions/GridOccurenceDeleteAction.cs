using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.GridActions
{
    public class GridOccurenceDeleteAction : IGridOccurenceDeleteAction
    {
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            try
            {
                if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
                {
                    int occurenceID = EmergeValidationHelper.GetInteger(actionArgument, 0);
                    EventsCalendarHelper.DeleteEventOccurenceByID(occurenceID);
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        public object BeforeAction(params object[] parameters)
        {
            try
            {
                int occurenceID = EmergeValidationHelper.GetInteger(parameters[0], 0);
                ArrayList returnList = new ArrayList();
                bool isOccurrenceExpired = EventsCalendarHelper.IsOccurenceExpired(occurenceID);
                if (isOccurrenceExpired)
                {
                    returnList.Add(false);
                    return returnList;
                }
                List<EventRegistration> registrations = EventsCalendarHelper.GetRegistrationsByOccurenceID(occurenceID, Common.EventRegistrationStatus.CONFIRMED);

                if (!registrations.Count.Equals(0))
                {
                    returnList.Add(true);
                    string message = String.Format(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONSEXISTSFOROCCURENCE), registrations.Count.ToString());
                    returnList.Add(message);
                }
                else 
                {
                    EventSchedule schedule = EventsCalendarHelper.GetScheduleByOccurenceID(occurenceID);
                    if (schedule.Occurences.Count.Equals(1) && schedule.Occurences[0].OccurenceID.Equals(occurenceID))
                    {
                        returnList.Add(true);
                        string message = ResHelper.GetString(EventsConstants.STRINGCODE_LASTOCCURENCE);
                        returnList.Add(message);
                    }
                    else
                        returnList.Add(false);
                }
                return returnList;
            }
            catch
            {
                throw;
            }
        }

        public object AfterAction(params object[] parameters)
        {
            EmergeURLHelper.Redirect(EmergeURLHelper.Url.ToString());
            return null;
        }
    }
}
