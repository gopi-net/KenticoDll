using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;
using System.Text.RegularExpressions;
using System.Collections;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_EventDetailsView : EventDetailsViewWebpart
{

    EventOccurence occurence;
    const string EVENTSSESSIONS = "EventsSessions";
    Dictionary<string, string> webpartProperties;
    int? currentSessionId = null;
    #region Properties
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    /// <summary>
    /// on click of Back button, control will be redirected to this page
    /// </summary>
    public string RegistrationFormURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("RegistrationFormURL"), string.Empty);
        }
        set
        {
            SetValue("RegistrationFormURL", value);
        }
    }

    /// <summary>
    /// on click of Add To Cart button, control will be redirected to this page
    /// </summary>
    public string CartPageURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("CartPageURL"), string.Empty);
        }
        set
        {
            SetValue("CartPageURL", value);
        }
    }

    /// <summary>
    /// on click of "back To All Events" button, control will be redirected to this page
    /// </summary>
    public string EventsCalendarPage
    {
        get
        {

            return ValidationHelper.GetString(GetValue("EventsCalendarPage"), string.Empty);
        }
        set
        {
            SetValue("EventsCalendarPage", value);
        }
    }

    /// <summary>
    /// Transformation Name for Session List.
    /// </summary>
    public string SessionListTransformationName
    {
        get
        {

            return ValidationHelper.GetString(GetValue("SessionListTransformationName"), SessionListRepeater.TransformationName);
        }
        set
        {
            SetValue("SessionListTransformationName", value);
            SessionListRepeater.TransformationName = value;
        }
    }

    public bool ShowAddToOutlookCalendar
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAddToOutlookCalendar"), true);
        }
        set
        {
            SetValue("ShowAddToOutlookCalendar", value);
        }
    }
    public bool ShowAddToGoogleCalendar
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAddToGoogleCalendar"), true);
        }
        set
        {
            SetValue("ShowAddToGoogleCalendar", value);
        }
    }
    public bool ShowAddToYahooCalendar
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAddToYahooCalendar"), true);
        }
        set
        {
            SetValue("ShowAddToYahooCalendar", value);
        }
    }
    public string DateFormat
    {
        get
        {
            return "yyyyMMddTHHmmssZ"; // 20060215T092000Z		
        }
    }
    public string EventTitleField
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventTitleField"), "EventName");
        }
        set
        {
            SetValue("EventTitleField", value);
        }
    }
    public string EventDescriptionField
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventDescriptionField"), "EventDescription");
        }
        set
        {
            SetValue("EventDescriptionField", value);
        }
    }

    #endregion

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            EventsDetailsPanel.Visible = false;
            return;
        }

        // if (!RequestHelper.IsPostBack())
        this.OccurenceIDField.Value = this.OccurenceID.ToString();
        lnkAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");

        loadEventDetails();
		if (occurence != null)
        {
        RegisterEvents();
        AddEventToCalender();
		}
    }

    private void RegisterEvents()
    {
        RegisterButton.Click += RegisterButton_Click;
        AddToCartButton.Click += AddToCartButton_Click;
        backToAllEvents.Click += backToAllEvents_Click;
        ViewCartButton.Click += ViewCartButton_Click;
        SessionListRepeater.ItemDataBound += SessionListRepeater_ItemDataBound;
    }

    void ViewCartButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(CartPageURL))
            URLHelper.Redirect(CartPageURL);
        ShowError(GetString(EventsConstants.STRINGCODE_CARTPAGEURLMISSINGMESSAGE));
    }

    void backToAllEvents_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(EventsCalendarPage))
            URLHelper.Redirect(EventsCalendarPage);
    }

    void AddToCartButton_Click(object sender, EventArgs e)
    {

        List<EventSession> selectedSessions = new List<EventSession>();

        if (occurence.Schedule.HasSessions)
        {
            selectedSessions = GetSelectedSessions();
            if (!VerifySessions(selectedSessions))
            {
                return;
            }
        }

        CartService.AddItem(occurence.OccurenceID, selectedSessions);

        if (!string.IsNullOrEmpty(CartPageURL))
            URLHelper.Redirect(CartPageURL);
        ShowError(GetString(EventsConstants.STRINGCODE_CARTPAGEURLMISSINGMESSAGE));
    }

    private bool VerifySessions(List<EventSession> selectedSessions)
    {
        if (!IsSessionSelected(selectedSessions))
        {
            ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_NOSESSIONSELECTEDMESSAGE)); return false;
        }

        if (!ValidateSessionClashes(selectedSessions))
        {
            ShowError(ResHelper.GetString("Emerge.EC.SessionsClash")); return false;
        }

        return true;
    }

    private bool IsSessionSelected(List<EventSession> selectedSessions)
    {
        return selectedSessions.Count == 0 ? false : true;
    }

    private bool ValidateSessionClashes(List<EventSession> selectedSessions)
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

    private List<EventSession> GetSelectedSessions()
    {
        //NoItemSelectedException
        List<RepeaterItem> selectedItems = null;
        if (SessionListRepeater.Items.Count > 0)
            selectedItems = SessionListRepeater.Items.OfType<RepeaterItem>().Where(x => ((LocalizedCheckBox)x.Controls[0].FindControl("chkSession")).Checked == true).ToList<RepeaterItem>();
        List<EventSession> sessions = null;

        if (ViewState[EVENTSSESSIONS] != null)
            sessions = (List<EventSession>)ViewState[EVENTSSESSIONS];
        else
            sessions = GetSessions();

        List<EventSession> selectedSessions = new List<EventSession>();
        foreach (RepeaterItem item in selectedItems)
        {
            int sessionID = Convert.ToInt32(((HiddenField)item.Controls[0].FindControl("hdnSessionID")).Value);
            EventSession session = sessions.Find(x => x.SessionID == sessionID);
            if (session != null)
                selectedSessions.Add(session);
        }
        return selectedSessions;

    }

    void RegisterButton_Click(object sender, EventArgs e)
    {
        List<EventSession> selectedSessions = new List<EventSession>();
        if (occurence.Schedule.HasSessions)
        {
            selectedSessions = GetSelectedSessions();
        }
        Session["SelectedSessions"] = selectedSessions;
        if (!String.IsNullOrEmpty(RegistrationFormURL))
            URLHelper.Redirect(RegistrationFormURL + "/" + this.OccurenceIDField.Value);
    }

    private void loadEventDetails()
    {
        OccurenceStatus status = IsValidOccurrence();
        if (status == OccurenceStatus.VALID)
        {
            DataSet eventsDS = getEventDetails();
            if (!DataHelper.DataSourceIsEmpty(eventsDS))
            {
                DataRow eventDataRow = eventsDS.Tables[0].Rows[0];
                foreach (DataColumn column in eventDataRow.Table.Columns)
                {
                    if (fieldExists(column.ColumnName))
                    {
                        setFieldValue(column.ColumnName, Convert.ToString(eventDataRow[column]));
                    }
                }
                enableDisableFields(eventDataRow);
            }
            return;
        }
        ShowErrorMessage(status);
    }

    private void ShowErrorMessage(OccurenceStatus status)
    {
        EventsDetailsPanel.Visible = false;
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
            EventOccurence occurrence = EventsCalendarHelper.GetEventOccurenceByID(this.OccurenceID);
            if (occurrence.Schedule.Event.EventType == EventsConstants.EVENTTYPE_VOLUNTEER)
            {
                VolunteerRegistration.Visible = true;
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

    private void enableDisableFields(DataRow eventDataRow)
    {
        RegisterButton.Visible = false;
        bool isEnabled = EventsCalendarHelper.IsEventCartEnabled();
        eventSessionDiv.Visible = AddToCartButton.Visible = isEnabled;
        ViewCartButton.Visible = isEnabled && CartService.GetItems().Count > 0;
        int occurenceID = ValidationHelper.GetInteger(this.OccurenceIDField.Value, 0);

        if (occurenceID > 0)
        {
            occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
            bool allowRegistrations = Convert.ToBoolean(eventDataRow[EventsConstants.FIELDS_EVENTSCHEDULE_NEEDREGISTRATIONS]);
            bool isPaidEvent = Convert.ToBoolean(eventDataRow[EventsConstants.FIELDS_EVENTSCHEDULE_ISPAIDSCHEDULE]);
            CostDiv.Visible = isPaidEvent;
            if (allowRegistrations)
            {
                if (IsRegistrationAllowed(occurence))
                {
                    RegisterButton.Visible = true;
                    //AddToCartButton.Visible = isPaidEvent;
                    if (occurence.Schedule.HasSessions)
                    {
                        SetSessionList();
                        //eventSessionDiv.Visible = true;
                    }
                    else
                        eventSessionDiv.Visible = false;
                }
                else
                {
                    RegisterButton.Visible = false;
                    plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_REGISTRATIONCLOSEDMESSAGE), string.Empty, string.Empty, true);
                    //RegistrationsClosedMessage.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONCLOSEDMESSAGE);
                    eventSessionDiv.Visible = AddToCartButton.Visible = false;
                }
            }
            else
            {
                plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_DOESNOTNEEDREGSITRATIONS), string.Empty, string.Empty, true);
                AddToCartButton.Visible = false;
                eventSessionDiv.Visible = false;
            }
            //RegistrationsClosedMessage.Text = GetString(EventsConstants.STRINGCODE_DOESNOTNEEDREGSITRATIONS);
        }
    }

    private void SetSessionList()
    {
        SessionListRepeater.TransformationName = SessionListTransformationName;
        SessionListRepeater.DataSource = GetSessions();

        SessionListRepeater.DataBind();
    }

    private List<EventSession> GetSessions()
    {
	
        List<EventSession> sessions = EventsCalendarHelper.GetSessionsByOccurenceID(occurence.OccurenceID);
        if (sessions != null && sessions.Count > 0)
            ViewState[EVENTSSESSIONS] = sessions;
			
        return sessions;
    }

    private static bool IsRegistrationAllowed(EventOccurence occurence)
    {
        return occurence.Registrations.Count < occurence.RegistrationLimit && occurence.OccurenceDate >= DateTime.Today.Date && !occurence.IsRegistrationLimitReached() && occurence.OccurenceDate.AddDays(-occurence.Schedule.RegistrationDeadline) >= DateTime.Now.Date;
    }

    private bool fieldExists(string fieldID)
    {
        if (this.EventsDetailsPanel.FindControl(fieldID) != null)
            return true;
        return false;
    }

    private void setFieldValue(string fieldID, string value)
    {
        Control field = this.EventsDetailsPanel.FindControl(fieldID);
        if (field is Literal)
        {
            Literal literalControl = (Literal)field;
            literalControl.Text = value;
        }
    }
    private string AddEventToCalender()
    {

        Hashtable hashedParameterEvent = new Hashtable();
        hashedParameterEvent.Add("@occurenceID", Convert.ToString(OccurenceID));
        AddEventToCalendarWebpart addEventToCalendar = new AddEventToCalendarWebpart();
        DataSet ds = addEventToCalendar.getAddToCalendarEventDetails(hashedParameterEvent);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string EventName = Convert.ToString(ds.Tables[0].Rows[0][EventTitleField]);
            string EventDescription = Convert.ToString(ds.Tables[0].Rows[0][EventDescriptionField]);
            EventDescription = Regex.Replace(EventDescription, "<.*?>", " ");
            EventDescription = Regex.Replace(EventDescription, "&nbsp;", " ");
            EventDescription = Convert.ToString(EventDescription).Replace("'", "''");
            string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = OccurenceDate.Text + " " + StartTime.Text;
            string strEndTime = OccurenceDate.Text + " " + EndTime.Text;
            if (ShowAddToOutlookCalendar)
            {
                lnkAddToOutlook.Visible = true;
            }
            if (occurence.Schedule.HasSessions)
            {
                ltAddToCalendar.Visible = false;
                lnkAddToOutlook.Visible = false;
                List<EventSession> sessions = null;
                if (ViewState[EVENTSSESSIONS] != null)
                    sessions = (List<EventSession>)ViewState[EVENTSSESSIONS];
                if (currentSessionId != null && sessions.Count > 0)
                {
                    EventSession currentSession = sessions.Find(x => x.SessionID == currentSessionId);
                    strStartTime = OccurenceDate.Text + " " + currentSession.StartTime;
                    strEndTime = OccurenceDate.Text + " " + currentSession.EndTime;
                }
            }
            DateTime dtStartTime = Convert.ToDateTime(strStartTime);
            dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            ltAddToCalendar.Text = string.Empty;
            if (ShowAddToGoogleCalendar)
            {
                ltAddToCalendar.Text += addEventToCalendar.getGoogleCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, ResHelper.GetString("Emerge.EC.AddtoGoogleCalendar"));
            }
            if (ShowAddToYahooCalendar)
            {
                ltAddToCalendar.Text += addEventToCalendar.getYahooCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, ResHelper.GetString("Emerge.EC.AddtoYahooCalendar"));
            }
        }
        return ltAddToCalendar.Text;
    }
    private void AddEventToOutlookCalendar(HttpContext context)
    {
        Hashtable hashedParameters = new Hashtable();
        hashedParameters.Add("@occurenceID", Convert.ToString(OccurenceID));
        AddEventToCalendarWebpart addToOutlookCalendar = new AddEventToCalendarWebpart();
        lnkAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");

        DataSet ds = addToOutlookCalendar.getAddToCalendarEventDetails(hashedParameters);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string EventName = Convert.ToString(ds.Tables[0].Rows[0][EventTitleField]);
            string eventDescription = Convert.ToString(ds.Tables[0].Rows[0][EventDescriptionField]);
            eventDescription = Regex.Replace(eventDescription, "<.*?>", " ");
            eventDescription = Regex.Replace(eventDescription, "&nbsp;", " ");
            eventDescription = Regex.Replace(eventDescription, "&#39;", "'");
            string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = OccurenceDate.Text + " " + StartTime.Text;
            string strEndTime = OccurenceDate.Text + " " + EndTime.Text;
            if (occurence.Schedule.HasSessions)
            {
                lnkAddToOutlook.Visible = false;
                List<EventSession> sessions = null;
                if (ViewState[EVENTSSESSIONS] != null)
                    sessions = (List<EventSession>)ViewState[EVENTSSESSIONS];
                if (currentSessionId != null && sessions.Count > 0)
                {
                    EventSession currentSession = sessions.Find(x => x.SessionID == currentSessionId);
                    strStartTime = OccurenceDate.Text + " " + currentSession.StartTime;
                    strEndTime = OccurenceDate.Text + " " + currentSession.EndTime;
                }
            }
            DateTime dtstartTime = Convert.ToDateTime(strStartTime);
            dtstartTime = TimeZoneInfo.ConvertTimeToUtc(dtstartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            addToOutlookCalendar.getOutlookLink(this.Context, EventName, Convert.ToDateTime(dtstartTime).ToString(DateFormat), Convert.ToDateTime(dtEndTime).ToString(DateFormat), Location, eventDescription);
        }
    }
    protected void lnkAddToOutlook_Click(object sender, EventArgs e)
    {
        AddEventToOutlookCalendar(this.Context);
    }
    protected void SessionListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            List<EventSession> sessions = GetSessions();
            if (sessions != null && sessions.Count > 0 && sessions.Any(x=>x.OccurenceID!=0))
            {
                foreach (EventSession eventSession in sessions)
                {
                    currentSessionId = Convert.ToInt32(((HiddenField)e.Item.Controls[0].FindControl("hdnSessionID")).Value);
                    Literal ltAddtoCalendar = (Literal)e.Item.Controls[0].FindControl("ltAddToCalendar");
                    if (ltAddtoCalendar != null)
                    {
                        ltAddtoCalendar.Text = AddEventToCalender();
                        //ltAddtoCalendar.Text = 		
                    }
                    LinkButton lnkSessionAddToOutlook = (LinkButton)e.Item.Controls[0].FindControl("lnkAddToOutlook");
                    if (lnkAddToOutlook != null && ShowAddToOutlookCalendar)
                    {
                        lnkSessionAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");
                        lnkSessionAddToOutlook.Click += lnkSessionAddToOutlook_Click;
                    }
                }
            }
        }
    }
    void lnkSessionAddToOutlook_Click(object sender, EventArgs e)
    {
        AddEventToOutlookCalendar(this.Context);
    }
}