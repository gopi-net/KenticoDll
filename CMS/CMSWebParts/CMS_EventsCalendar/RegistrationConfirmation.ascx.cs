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
using Bluespire.Emerge.Common.Logging;
using System.Data;
using CMS.Helpers;
using System.Collections;
using System.Text.RegularExpressions;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_RegistrationConfirmation : RegistrationConfirmationWebPart
{
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    #region "Webpart Properties"
    /// <summary>
    /// on click of Register button, control will be redirected to this page
    /// </summary>
    public string CalendarHomePageURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("CalendarHomePageURL"), string.Empty);
        }
        set
        {
            SetValue("CalendarHomePageURL", value);
        }
    }

    public string SessionViewTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SessionViewTransformationName"), SessionsRepeater.TransformationName);
        }
        set
        {
            SetValue("SessionViewTransformationName", value);
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
            return ValidationHelper.GetString(GetValue("EventTitleField"), string.Empty);
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
            return ValidationHelper.GetString(GetValue("EventDescriptionField"), string.Empty);
        }
        set
        {
            SetValue("EventDescriptionField", value);
        }
    }
    #endregion
    int currentSessionId = 0;
    protected override void OnInit(EventArgs e)
    {
        ControlPanel = ConfirmationPanel;
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ConfirmationPanel.Visible = false;
            return;
        }
        if (!RequestHelper.IsPostBack())
            SaveRegistration();
        loadEventDetails();
        BackButton.Click += BackButton_Click;
        lnkAddToOutlook.Click += lnkAddToOutlook_Click;
        lnkAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");
        SessionsRepeater.ItemDataBound += SessionsRepeater_ItemDataBound;
        AddEventToCalender();
    }

    void BackButton_Click(object sender, EventArgs e)
    {
        SessionHelper.Remove(EventsConstants.GENERALSELECTEDSESSIONS);
        URLHelper.Redirect(CalendarHomePageURL);
    }

    private void SaveRegistration()
    {

        if (SessionHelper.GetValue(EventsConstants.REGISTRATIONINFO) != null)
        {
            bool isSuccess = base.SaveRegistrations();
            try
            {
                if (isSuccess)
                    SendEmailUsingSelectedTemplate();
                else
                    ConfirmationMessage.Text = GetString(EventsConstants.STRINGCODE_REGSITRATIONSSAVEEXCEPTIONMESSAGE);
            }
            catch
            {
                ConfirmationMessage.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONEMAILSENDMESSAGE);
                return;
            }
        }
        try
        {

            try
            {
            }
            catch
            {
                ConfirmationMessage.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONEMAILSENDMESSAGE);
                return;
            }

        }
        catch
        {
            ConfirmationMessage.Text = GetString(EventsConstants.STRINGCODE_REGSITRATIONSSAVEEXCEPTIONMESSAGE);
            return;
        }
        ConfirmationMessage.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONSUCCESSMESSAGE);
    }

    private void loadEventDetails()
    {
        Dictionary<string, string> te = new Dictionary<string, string>();
        DataSet eventsDS = getEventDetails();
        if (!DataHelper.DataSourceIsEmpty(eventsDS))
        {
            DataRow eventDataRow = eventsDS.Tables[0].Rows[0];
            foreach (DataColumn column in eventDataRow.Table.Columns)
            {
                te.Add(column.ColumnName, Convert.ToString(eventDataRow[column]));
                if (fieldExists(column.ColumnName))
                {
                    setFieldValue(column.ColumnName, Convert.ToString(eventDataRow[column]));
                }
            }

            DataSet sessionDS = getSelectedSessions();
            SessionsRepeater.TransformationName = this.SessionViewTransformationName;
            SessionsRepeater.DataSource = sessionDS;
            Session["SelectedSession"] = sessionDS;
            SessionsRepeater.DataBind();
            SessionsCount.Value = DataHelper.DataSourceIsEmpty(sessionDS) ? "0" : sessionDS.Tables[0].Rows.Count.ToString();
            if (!RequestHelper.IsPostBack() && sessionDS == null)
                SessionHelper.Remove(EventsConstants.GENERALSELECTEDSESSIONS);
        }
    }
    void SessionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataSet sessionDS = null;
            if (Session["SelectedSession"] != null)
                sessionDS = (DataSet)Session["SelectedSession"];
            if (sessionDS != null)
            {
                Panel plcLnkOutlook = (Panel)e.Item.Controls[0].FindControl("pnlAddToCalendar");
                if (plcLnkOutlook != null)
                {
                    int itemSessionID = 0;
                    LocalizedHidden hdnOccuranceID = (LocalizedHidden)e.Item.Controls[0].FindControl("hdnSessionID");
                    if (int.TryParse(hdnOccuranceID.Value, out itemSessionID))
                    {
                        foreach (DataRow item in sessionDS.Tables[0].Rows)
                        {
                            int sessionId = ValidationHelper.GetInteger(item["ItemID"], 0);
                            currentSessionId = sessionId;
                            if (sessionId != itemSessionID)
                                continue;
                            Literal ltAddToCalendar = new Literal();
                            ltAddToCalendar.Text += AddEventToCalender();
                            plcLnkOutlook.Controls.Add(ltAddToCalendar);
                            if (ShowAddToOutlookCalendar)
                            {
                                LinkButton lnkAddToOutlook = new LinkButton();
                                lnkAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");
                                lnkAddToOutlook.Click += lnkAddToOutlook_Click;
                                plcLnkOutlook.Controls.Add(lnkAddToOutlook);
                            }
                        }
                    }
                }
            }
        }
    }
    void lnkAddToOutlook_Click(object sender, EventArgs e)
    {
        AddEventToOutlookCalendar(this.Context);
    }
    private string AddEventToCalender()
    {
        Hashtable hashedParameterEvent = new Hashtable();
        hashedParameterEvent.Add("@occurenceID", Convert.ToString(OccurenceID));
        AddEventToCalendarWebpart addEventToCalendar = new AddEventToCalendarWebpart();
        DataSet ds = addEventToCalendar.getAddToCalendarEventDetails(hashedParameterEvent);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string EventName = Convert.ToString(ds.Tables[0].Rows[0]["EventName"]);
            string EventDescription = Convert.ToString(ds.Tables[0].Rows[0]["EventDescription"]);
            EventDescription = Regex.Replace(EventDescription, "<.*?>", " ");
            EventDescription = Regex.Replace(EventDescription, "&nbsp;", " ");
            EventDescription = Convert.ToString(EventDescription).Replace("'", "''");
            string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["StartTime"]);//OccurenceDate.Text + " " + StartTime.Text;
            string strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["EndTime"]);
            if (ShowAddToOutlookCalendar)
            {
                lnkAddToOutlook.Visible = true;
            }
            if (currentSessionId != 0 && Session["SelectedSession"] != null)
            {
                ltAddToCalendar.Visible = false;
                lnkAddToOutlook.Visible = false;
                DataSet sessionDS = (DataSet)Session["SelectedSession"];
                DataRow sessionDr = sessionDS.Tables[0].AsEnumerable().Where(x => x.Field<int>("ItemID") == currentSessionId).Single();
                strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDr["StartTime"].ToString();
                strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDr["EndTime"].ToString();
                ltAddToCalendar.Visible = false;
                lnkAddToOutlook.Visible = false;
            }
            DateTime dtStartTime = Convert.ToDateTime(strStartTime);
            dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            ltAddToCalendar.Text = string.Empty;
            if (ShowAddToGoogleCalendar)
            {
                ltAddToCalendar.Text += addEventToCalendar.getGoogleCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, "Google Calendar");
            }
            if (ShowAddToYahooCalendar)
            {
                ltAddToCalendar.Text += addEventToCalendar.getYahooCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, "Yahoo Calendar");
            }
        }
        return ltAddToCalendar.Text;
    }
    private void AddEventToOutlookCalendar(HttpContext context)
    {
        Hashtable hashedParameters = new Hashtable();
        hashedParameters.Add("@occurenceID", Convert.ToString(OccurenceID));
        AddEventToCalendarWebpart addToOutlookCalendar = new AddEventToCalendarWebpart();
        DataSet ds = addToOutlookCalendar.getAddToCalendarEventDetails(hashedParameters);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string EventName = Convert.ToString(ds.Tables[0].Rows[0]["EventName"]);
            string EventDescription = Convert.ToString(ds.Tables[0].Rows[0]["EventDescription"]);
            EventDescription = Regex.Replace(EventDescription, "<.*?>", " ");
            EventDescription = Regex.Replace(EventDescription, "&nbsp;", " ");
            EventDescription = Convert.ToString(EventDescription).Replace("'", "''");
            string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["StartTime"]);//OccurenceDate.Text + " " + StartTime.Text;
            string strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["EndTime"]);
            if (true)
            {
                lnkAddToOutlook.Visible = true;
            }
            if (currentSessionId != 0 && Session["SelectedSession"] != null)
            {
                DataSet sessionDS = (DataSet)Session["SelectedSession"];
                DataRow sessionDr = sessionDS.Tables[0].AsEnumerable().Where(x => x.Field<int>("ItemID") == currentSessionId).Single();
                strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDr["StartTime"].ToString();
                strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDr["EndTime"].ToString();
                ltAddToCalendar.Visible = false;
                lnkAddToOutlook.Visible = false;
            }
            DateTime dtStartTime = Convert.ToDateTime(strStartTime);
            dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            addToOutlookCalendar.getOutlookLink(this.Context, EventName, Convert.ToDateTime(dtStartTime).ToString(DateFormat), Convert.ToDateTime(dtEndTime).ToString(DateFormat), Location, EventDescription);
        }
    }
}