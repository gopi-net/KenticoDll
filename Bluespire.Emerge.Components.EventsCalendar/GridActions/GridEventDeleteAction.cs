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
    public class GridEventDeleteAction : IGridEventDeleteAction
    {
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            try
            {
                int eventID = EmergeValidationHelper.GetInteger(actionArgument, 0);
                EventsCalendarHelper.DeleteEventByEventID(eventID);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public object BeforeAction(params object[] parameters)
        {
            int eventID = EmergeValidationHelper.GetInteger(parameters[0], 0);
            ArrayList returnList = new ArrayList();
            bool isEventExpired = EventsCalendarHelper.IsEventExpired(eventID);
            if (isEventExpired)
            {
                returnList.Add(false);
                return returnList;
            }
            List<EventRegistration> registrations = EventsCalendarHelper.GetRegistrationsByEventID(eventID, Common.EventRegistrationStatus.CONFIRMED);
            if (registrations.Count > 0)
            {
                returnList.Add(true);
                string message = String.Format(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONSEXISTSFOREVENT), registrations.Count.ToString());
                returnList.Add(message);
            }
            else
                returnList.Add(false);
            return returnList;
        }

        public object AfterAction(params object[] parameters)
        {
            EmergeURLHelper.Redirect(EmergeURLHelper.Url.ToString());
            return null;
        }
    }
}
