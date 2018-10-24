using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using CMS.Helpers;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class EventCartRegistrationConfirmationWebPart : EventsCalendarWebPart
    {
        protected List<int> RegistrationIDs = new List<int>();

        public SaveRegistrationStatus SaveRegistrationsAndSendEmail()
        {
            List<Dictionary<string, object>> registrations = EventsCalendarHelper.GetRegistrationsFromCart();

            SaveRegistrationStatus status = EventsCalendarHelper.SaveRegistrations(registrations, ref RegistrationIDs);

            if (status == SaveRegistrationStatus.SUCCESS)
            {
                SendEmail();
                //SendEmailToAdmin.
            }
            return status;

        }

        private bool SaveRegistration(Dictionary<string, object> cartItemData)
        {
            int itemID = 0;
            bool isSuccess = CustomTableDataHelper.SaveCustomTableItem(EmergeStaticHelper.SetSiteName(EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS), ref itemID, cartItemData);
            RegistrationIDs.Add(itemID);
            return isSuccess;
        }

        protected void SendEmail()
        {
            try
            {
                List<EventRegistration> registrations = new List<EventRegistration>();

                string strRegistrationIds = string.Empty;


                foreach (int registrationID in RegistrationIDs)
                {
                    EventRegistration registration = EventsCalendarHelper.GetEventRegistrationByID(registrationID);
                    registrations.Add(registration);
                    strRegistrationIds += registrationID.ToString() + Constants.COMMA_SEPERATOR.ToString() ;
                }

                if (strRegistrationIds.EndsWith(Constants.COMMA_SEPERATOR.ToString()))
                    strRegistrationIds = strRegistrationIds.Substring(0, strRegistrationIds.LastIndexOf(Constants.COMMA_SEPERATOR));
                SessionHelper.SetValue(EventsConstants.SESSIONKEY_CARTEVENTREGISTRATIONIDS, strRegistrationIds);


                SendEmailConfirmationForCartRegistrations(registrations,GetEventCartEmailRepeaterHtml());
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("Event Cart Registration Confirmation Webpart:SendEmail", EventCode.EMERGE_EMAIL, ex.ToString());
            }
        }

        private string GetEventCartEmailRepeaterHtml()
        {
            StringBuilder repeaterSB = new StringBuilder();
            if (null != ControlPanel)
            {
                if (null != ControlPanel.FindControl("EventCartEmailRepeater"))
                {
                    ((CMSRepeater)ControlPanel.FindControl("EventCartEmailRepeater")).Visible = true;
                    ((CMSRepeater)ControlPanel.FindControl("EventCartEmailRepeater")).RenderControl(new HtmlTextWriter(new StringWriter(repeaterSB)));
                    ((CMSRepeater)ControlPanel.FindControl("EventCartEmailRepeater")).Visible = false;
                }
            }

            return repeaterSB.ToString();
        }

        private bool SendEmailConfirmationForCartRegistrations(List<EventRegistration> registrations,string emailEventCartHtml)
        {
            return EventsCalendarHelper.SendCartRegistrationEmail(registrations, emailEventCartHtml, RegistrationEmailMode.CARTINSERT);
        }

        protected bool SendEmailConfirmation(EventRegistration registration)
        {
            return EventsCalendarHelper.SendRegistrationEmail(registration, RegistrationEmailMode.GENERAL_INSERT);
        }

        protected void RemoveSessionData()
        {
            SessionHelper.Remove(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART);
        }
    }
}
