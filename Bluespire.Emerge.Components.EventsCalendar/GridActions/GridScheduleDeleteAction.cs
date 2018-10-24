using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using System.Collections;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.GridActions
{
    /// <summary>
    /// Represents the delete action for the events schedule.
    /// </summary>
    public class GridScheduleDeleteAction : IGridScheduleDeleteAction
    {
        const string ITEMID = "ItemID";
        /// <summary>
        /// Processes the delete action on the specified custom table.
        /// </summary>
        /// <param name="actionArgument">argument object to be processed.</param>
        /// <param name="CustomTableClassName">name of the custom table the argument object belongs to.</param>
        /// <param name="AfterActionRedirectToUrl">redirect url when the action completes.</param>
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            try
            {
                if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
                {
                    int scheduleID = EmergeValidationHelper.GetInteger(actionArgument, 0);
                    EventsCalendarHelper.DeleteEventScheduleByID(scheduleID);
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
            int scheduleID = EmergeValidationHelper.GetInteger(parameters[0], 0);
            ArrayList returnList = new ArrayList();
            bool isScheduleExpired = EventsCalendarHelper.IsScheduleExpired(scheduleID);
            if (isScheduleExpired)
            {
                returnList.Add(false);
                return returnList;
            }
            List<EventRegistration> registrations = EventsCalendarHelper.GetAllRegistrationsByScheduleID(scheduleID, EventRegistrationStatus.CONFIRMED);
            if (registrations.Count > 0)
            {
                returnList.Add(true);
                string message = String.Format(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONSEXISTSFORSCHEDULE), registrations.Count.ToString());
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
