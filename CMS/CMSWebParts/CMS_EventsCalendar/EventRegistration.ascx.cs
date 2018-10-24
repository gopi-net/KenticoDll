using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;
using System.Collections;
using System.Data;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_EventRegistration : EventRegistrationWebPart
{
    const string EVENTSSESSIONS = "EventsSessions";
    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    /// <summary>
    /// on click of Register button, control will be redirected to this page
    /// </summary>
    public string ConfirmationPageURL
    {
        get
        {

            return EmergeValidationHelper.GetString(GetValue("ConfirmationPageURL"), string.Empty);
        }
        set
        {
            SetValue("ConfirmationPageURL", value);
        }
    }

    /// <summary>
    /// on click of Register button, control will be redirected to this page
    /// </summary>
    public string CalendarHomePageURL
    {
        get
        {

            return EmergeValidationHelper.GetString(GetValue("CalendarHomePageURL"), string.Empty);
        }
        set
        {
            SetValue("CalendarHomePageURL", value);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ControlPanel = this.RegistrationPanel;
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            RegistrationPanel.Visible = false;
            return;
        }
        LoadListControls(false);
        if(!EmergeRequestHelper.IsPostBack())
            OccurenceID.Value = EmergeValidationHelper.GetInteger(this.OccurenceIDValue, 0).ToString();
        RegisterEvents();
        setupFields();
    }

    private void setupFields()
    {
        OccurenceStatus status = IsValidOccurrence();
        if (status == OccurenceStatus.VALID)
        {
            int occurenceID = EmergeValidationHelper.GetInteger(OccurenceID.Value, 0);

            if (occurenceID > 0)
            {
                EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
                enableDisableFields(occurence);
                DisplaySelectedFields(occurenceID);
                setFieldValues(occurence);
            }
            return;
        }
        ShowErrorMessage(status);
    }

    private void ShowErrorMessage(OccurenceStatus status)
    {
        RegistrationPanel.Visible = false;
        string message = string.Empty;
        switch (status)
        {
            case OccurenceStatus.INVALIDGENERALOCCURENCE:
                message = GetString(EventsConstants.STRINGCODE_INVALIDGENERALOBSERVATIONOCCURENCE);
                break;
            case OccurenceStatus.DOESNOTEXIST:
                message = GetString(EventsConstants.STRINGCODE_OCCURRENCEDOESNOTEXISTS);
                break;
            case OccurenceStatus.INVALIDSERIESOCCURENCE:
                message = GetString(EventsConstants.STRINGCODE_INVAIDSERIESOCCURENCE);
                break;
        }
        plcMess.ShowMessage(MessageTypeEnum.Error, message, string.Empty, string.Empty, true);
    }

    private OccurenceStatus IsValidOccurrence()
    {
        OccurenceStatus status = OccurenceStatus.VALID;
        try
        {
            EventOccurence occurrence = EventsCalendarHelper.GetEventOccurenceByID(EmergeValidationHelper.GetInteger(OccurenceID.Value, 0));
            if (occurrence.Schedule.Event.EventType == EventsConstants.EVENTTYPE_VOLUNTEER)
            {
                status = OccurenceStatus.INVALIDGENERALOCCURENCE;
                return status;
            }
            if (occurrence.IsSeries)
            {
                if (occurrence.Schedule.Occurences.OrderBy(a => a.OccurenceDate).First().OccurenceID != occurrence.OccurenceID)
                {
                    status = OccurenceStatus.INVALIDSERIESOCCURENCE;
                    return status;
                }
            }
        }
        catch (CustomTableItemNotFoundException)
        {
            status = OccurenceStatus.DOESNOTEXIST;
        }
        return status;
    }

    private void enableDisableFields(EventOccurence occurence)
    {
        AmountTR.Visible = DiscountCodeTR.Visible = DiscountedCostTR.Visible = occurence.Schedule.IsPaidSchedule;
        SessionTR.Visible = occurence.Schedule.HasSessions;
        if (!IsRegistrationAllowed(occurence))
        {
            RegistrationPanel.Visible = false;
            plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_REGISTRATIONCLOSEDMESSAGE), string.Empty, string.Empty, true);
        }
        
    }

    private static bool IsRegistrationAllowed(EventOccurence occurence)
    {
        return occurence.OccurenceDate >= DateTime.Today.Date && !occurence.IsRegistrationLimitReached() && occurence.OccurenceDate.AddDays(-occurence.Schedule.RegistrationDeadline) >= DateTime.Now.Date;
    }

    private void setFieldValues(EventOccurence occurence)
    {
        ScheduleID.Value = EmergeValidationHelper.GetInteger(occurence.ScheduleID, 0).ToString();
        Amount.Text = occurence.Schedule.CostForPublic.ToString();
        DiscountedCost.Text = Amount.Text;
        if (!EmergeRequestHelper.IsPostBack())
        {
            List<EventSession> sessions = EventsCalendarHelper.GetSessionsByOccurenceID(occurence.OccurenceID);
            SelectedSessions.Items.Clear();
            if (sessions.Count > 0)
                ViewState[EVENTSSESSIONS] = sessions;
            foreach (EventSession session in sessions)
            {
                SelectedSessions.Items.Add(new ListItem(session.Title + " - " + session.StartTime + " - " + session.EndTime, session.SessionID.ToString()));
            }
        }
        if (Convert.ToString(Session["SelectedSessions"]) != "" && Convert.ToString(Session["SelectedSessions"]) != null)
        {
            List<EventSession> selSessions = (List<EventSession>)Session["SelectedSessions"];
            if (SelectedSessions.Items.Count > 0 && selSessions != null)
            {
                for (int i = 0; i < SelectedSessions.Items.Count; i++)
                {
                    foreach (var sessionvalue in selSessions)
                    {
                        if (SelectedSessions.Items[i].Value == Convert.ToString(sessionvalue.SessionID))
                        {
                            SelectedSessions.Items[i].Selected = true;
                        }
                    }
                }
            }
            Session.Remove("SelectedSessions");
        }
    }

    private void RegisterEvents()
    {
        RegisterButton.Click += RegisterButton_Click;
        DiscountCode.TextChanged += DiscountCode_TextChanged;
        BackButton.Click += BackButton_Click;
    }

    void BackButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(CalendarHomePageURL))
            URLHelper.Redirect(CalendarHomePageURL);
    }

    void DiscountCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DiscountCodeMessage.Text = string.Empty;
            string discountCode = DiscountCode.Text.Trim();
            if (!String.IsNullOrEmpty(discountCode))
            {
                double discountedCost = GetDiscountedCostbyCodeAndScheduleID(discountCode, Convert.ToInt32(ScheduleID.Value));
                DiscountedCost.Text = discountedCost.ToString(); 
            }
        }
        catch (InvalidDiscountCodeException)
        {
            DiscountCodeMessage.Text = GetString(EventsConstants.STRINGCODE_INVALIDDISCOUNTCODEMESSAGE);
            DiscountedCost.Text = Amount.Text;
            DiscountCode.Text = string.Empty;
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    void RegisterButton_Click(object sender, EventArgs e)
    {
        try
        {
            List<EventSession> sessions = getSelectedSessions();
            if (ValidateSessions(sessions))
            {
                object[] parameters = new object[5];

                parameters[0] = Convert.ToInt32(OccurenceID.Value);
                parameters[1] = Convert.ToInt32(ScheduleID.Value);
                parameters[2] = Convert.ToDouble(DiscountedCost.Text);
                parameters[3] = sessions;
                parameters[4] = DiscountCode.Text;

                SaveRegistrationStatus status = SetRegistrationParameters(parameters);
                if (status == SaveRegistrationStatus.VALID)
                    URLHelper.Redirect(ConfirmationPageURL + "/" + this.OccurenceIDValue);
                else
                    ShowMessage(status);
            }
            else
                SessionsMessage.Text = GetString(EventsConstants.STRINGCODE_SESSIONCLASH);
        }
        catch (RegistrationExistException)
        {
            plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_DUPLICATEREGISTRATIONMESSAGE), string.Empty, string.Empty, true);
            //InformationMessage.Text = GetString(EventsConstants.STRINGCODE_DUPLICATEREGISTRATIONMESSAGE);
        }
        catch(Exception ex)
        {
            OnError(ex);
        }
    }

    private void ShowMessage(SaveRegistrationStatus status)
    {
        string message = string.Empty;
        switch (status)
        {
            case SaveRegistrationStatus.DISCOUNTCODEUSED:
                {
                    message = GetString(EventsConstants.STRINGCODE_DISCOUNTCODEUSED);
                    DiscountCode.Text = string.Empty;
                }
                break;
            case SaveRegistrationStatus.DUPLICATEREGISTRATIONS:
                message = GetString(EventsConstants.STRINGCODE_DUPLICATEREGISTRATIONMESSAGE);
                break;
            case SaveRegistrationStatus.REGISTRATIONLIMITREACHED:
                message = GetString(EventsConstants.STRINGCODE_REGISTRATIONLIMITREACHEDUI);
                break;
            case SaveRegistrationStatus.INVALIDDISCOUNTCODE:
                {
                    message = GetString(EventsConstants.STRINGCODE_INVALIDDISCOUNTCODEMESSAGE);
                    DiscountCode.Text = string.Empty;
                }
                break;
        }
        plcMess.ShowMessage(MessageTypeEnum.Error, message, string.Empty, string.Empty, true);
    }
    private void DisplaySelectedFields(int occurenceID)
    {
        DataSet ds = getEventRegistrationField();
        if (DataHelper.DataSourceIsEmpty(ds))
            return;
        RegistrationFormFields.Value = Convert.ToString(ds.Tables[0].Rows[0]["RegistrationFields"]).Trim();
        bindChangeEvent.Text = "<script> showHideFields();</script>";
    }
}