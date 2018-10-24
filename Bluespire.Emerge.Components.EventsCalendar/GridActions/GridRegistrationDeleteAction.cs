using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.GlobalHelper;

namespace Bluespire.Emerge.Components.EventsCalendar.GridActions
{
    public class GridRegistrationDeleteAction : IGridRegistrationDeleteAction
    {
        protected EventRegistration Registration
        {
            get;
            set;
        }

        protected string[,] RegistrationMacros
        {
            get;
            set;
        }

        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                int itemID = EmergeValidationHelper.GetInteger(actionArgument, 0);
                CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(itemID, CustomTableClassName);

                item.Delete();
               
            }
            return true;
        }

        public object BeforeAction(params object[] parameters)
        {
            int registrationID = EmergeValidationHelper.GetInteger(parameters[0], 0);
            this.Registration = EventsCalendarHelper.GetEventRegistrationByID(registrationID);
            this.RegistrationMacros = EventsCalendarHelper.GetRegistrationMacros(this.Registration);
            return true;
        }

        public object AfterAction(params object[] parameters)
        {
            if (this.Registration != null)
            {
                
                RegistrationEmailMode mode = (this.Registration.Occurence.Schedule.Event.EventType == EventsConstants.EVENTTYPE_GENERAL) ? RegistrationEmailMode.GENERAL_DELETE : RegistrationEmailMode.VOLUNTEER_DELETE;
                EventsCalendarHelper.SendRegistrationEmail(this.Registration, mode, this.RegistrationMacros);
            }
            EmergeURLHelper.Redirect(EmergeURLHelper.Url.ToString());
            return true;
        }
    }
}
