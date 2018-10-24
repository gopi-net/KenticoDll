using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.FormEngine;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using System.Threading;
using Bluespire.Emerge.Components.EventsCalendar.CustomMacros;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.Membership;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_Registrations : EventsRegistrationDataEditItemPage
{
    private string editItemPage = EventsConstants.PAGEURL_NEW_EVENTSREGISTRATIONS; //"~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Registrations.aspx";
    private DataClassInfo dci = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = EventsConstants.PAGEURL_DATA_LIST; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_List.aspx";
            NewItemPage = EventsConstants.PAGEURL_NEW_EVENTSREGISTRATIONS; //"~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Registrations.aspx";

            RegisterEvents();

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                EventFormControl.CustomTableId = CustomTableID;
                EventFormControl.ItemId = ItemID;
            }
            if (CustomTableID > 0)
            {
                dci = CustomTableDataHelper.GetCustomTableClassInfo(CustomTableID);
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    private void RegisterEvents()
    {
        EventFormControl.OnAfterSave += EventFormControl_OnAfterSave;
        SaveButton.Click += SaveButton_Click;
        EmergePopup.OnOKButtonClick += EmergePopup_OnOKButtonClick;
        EventFormControl.OnBeforeSave += EventFormControl_OnBeforeSave;
    }

    void EventFormControl_OnBeforeSave(object sender, EventArgs e)
    {
        SessionHelper.Remove(EventsConstants.NEWREGISTRATIONS);
        int occurenceID = Convert.ToInt32(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID));
        EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);

        if (isDuplicateRegistration(occurence))
            return;

        if (showRegistrationLimitReached(occurence))
            return;

        setRegistrationSessionValue();
    }

    private void setRegistrationSessionValue()
    {
        if (EventFormControl.CustomTableForm.ItemID == 0)
            SessionHelper.SetValue(EventsConstants.NEWREGISTRATIONS, true);
        else
            SessionHelper.SetValue(EventsConstants.NEWREGISTRATIONS, false);
    }

    private bool isDuplicateRegistration(EventOccurence occurence)
    {
        string email = Convert.ToString(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL));
        string eventType = CustomMacroMethods.EventTypeOfSchedule(occurence.ScheduleID);
        if (eventType == EventsConstants.EVENTTYPE_VOLUNTEER)
        {
            int userID = ValidationHelper.GetInteger(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_VOLUNTEERUSER), 0);
            UserInfo user = UserInfoProvider.GetUserInfo(userID);
            email = Convert.ToString(user.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_EMAIL));
        }
        if (occurence.IsRegistrationExist(email, EventFormControl.CustomTableForm.ItemID))
        {
            EmergePopup.Body.Controls.Add(getPopupControlBody("This occurrence does not allow duplicate registrations."));
            EmergePopup.Show();
            EventFormControl.CustomTableForm.StopProcessing = true;
            return true;
        }
        return false;
    }

    private bool showRegistrationLimitReached(EventOccurence occurence)
    {
        if (occurence.IsRegistrationLimitReached() && null == occurence.Registrations.Find(a => a.ItemID == EventFormControl.CustomTableForm.ItemID))
        {
            EmergePopup.Body.Controls.Add(getPopupControlBody(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONLIMITREACHED)));
            EmergePopup.Show();
            EventFormControl.CustomTableForm.StopProcessing = true;
            return true;
        }
        return false;
    }

    void EmergePopup_OnOKButtonClick(object sender, EventArgs e)
    {

    }


    void EventFormControl_OnAfterSave(object sender, EventArgs e)
    {
        try
        {
            EventsCalendarHelper.UpdateStartTimeForRegistration(EventFormControl.CustomTableForm.ItemID, CustomTableID);

            int scheduleID = ValidationHelper.GetInteger(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID), 0);
            string eventType = EventsConstants.EVENTTYPE_GENERAL;
            if (scheduleID > 0)
            {
                eventType = CustomMacroMethods.EventTypeOfSchedule(scheduleID);
                if (eventType == EventsConstants.EVENTTYPE_VOLUNTEER)
                {
                    int userID = ValidationHelper.GetInteger(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTREGISTRATIONS_VOLUNTEERUSER), 0);
                    EventsCalendarHelper.UpdateRegistrationForVolunteerUser(EventFormControl.CustomTableForm.ItemID, userID);
                }
            }

            sendEmail(eventType);
        }
        catch (ThreadAbortException ex)
        {
            OnError(ex, false);
        }
        catch (CustomTableItemNotFoundException ex)
        {
            OnError(ex);
        }
        catch (CustomTableNotExistsException ex)
        {
            OnError(ex);
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        if (EventFormControl.CustomTableForm.ValidateData())
        {
            saveData();
        }
    }

    private void sendEmail(string eventType)
    {
        EventRegistration registration = EventsCalendarHelper.GetEventRegistrationByID(EventFormControl.CustomTableForm.ItemID);
        bool isNew = Convert.ToBoolean(SessionHelper.GetValue(EventsConstants.NEWREGISTRATIONS));
        if (isNew)
        {
            RegistrationEmailMode mode = (eventType == EventsConstants.EVENTTYPE_VOLUNTEER) ? RegistrationEmailMode.VOLUNTEER_INSERT : RegistrationEmailMode.GENERAL_INSERT;
            EventsCalendarHelper.SendRegistrationEmail(registration, mode);
        }
        else
        {
            RegistrationEmailMode mode = (eventType == EventsConstants.EVENTTYPE_VOLUNTEER) ? RegistrationEmailMode.VOLUNTEER_UPDATE : RegistrationEmailMode.GENERAL_UPDATE;
            EventsCalendarHelper.SendRegistrationEmail(registration, mode);
        }

        SessionHelper.Remove(EventsConstants.NEWREGISTRATIONS);
    }

    private Control getPopupControlBody(string message)
    {
        Panel parent = new Panel();
        
        LiteralControl literal = new LiteralControl();
        literal.Text = message;
        parent.Controls.Add(literal);
        
        return parent;
    }

    private List<DateTime> getExcludedDates()
    {
        List<DateTime> excludedDates = new List<DateTime>();
        
        return excludedDates;
    }

    private void saveData()
    {
        EventFormControl.CustomTableForm.SaveData(URLHelper.GetAbsoluteUrl(EventFormControl.EditItemPage) + "?customtableid=" + EventFormControl.CustomTableId + "&itemid=" + EventFormControl.CustomTableForm.ItemID);
    }
}