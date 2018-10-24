using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common.Exceptions;
using CMS.PortalEngine;
using CMS.Helpers;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_EventCartRegistrationConfirmation : EventCartRegistrationConfirmationWebPart
{

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

    const string CARTSERVICEITEMS = "CartServiceItem";
    int? occurrenceID = null;
    string commaSeperatedSessionIDs = string.Empty;

    #region "Webpart Properties"
    /// <summary>
    /// This Property will be used to set Header Template Text for Event Cart Repeater.
    /// </summary>
    public string EventCartHeaderTemplateText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartHeaderTemplateText"), string.Empty);
        }
        set
        {
            SetValue("EventCartHeaderTemplateText", value);
        }
    }

    /// <summary>
    /// This Property will be used to set Header Template Text for Excluded Event Repeater.
    /// </summary>
    public string ExcludedEventsHeaderTemplateText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ExcludedEventsHeaderTemplateText"), string.Empty);
        }
        set
        {
            SetValue("ExcludedEventsHeaderTemplateText", value);
        }
    }

    public string ExcludedEventsTransformation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ExcludedEventsTransformation"), EventCartRepeater.TransformationName);
        }
        set
        {
            SetValue("ExcludedEventsTransformation", value);

        }
    }

    /// <summary>
    /// if Events Cart is Empty then it will redirect to Events Calendar Page.
    /// </summary>
    public string EventsCalendarPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventsCalendarPageURL"), string.Empty);
        }
        set
        {
            SetValue("EventsCalendarPageURL", value);
        }
    }


    /// <summary>
    /// Transformation Name set to Events Cart.
    /// </summary>
    public string EventsCartTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventsCartTransformationName"), EventCartRepeater.TransformationName);
        }
        set
        {
            SetValue("EventsCartTransformationName", value);

        }
    }

    public string EventCartPageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartPageUrl"), string.Empty);
        }
        set
        {
            SetValue("EventCartPageUrl", value);
        }
    }

    /// <summary>
    /// Transformation Name set to Events Cart.
    /// </summary>
    public string EventsCartTransformationNameUsedInEmail
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventsCartTransformationNameUsedInEmail"), EventCartRepeater.TransformationName);
        }
        set
        {
            SetValue("EventsCartTransformationNameUsedInEmail", value);

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

    protected override void OnInit(EventArgs e)
    {
        ControlPanel = panelConfirmationMessage;

    }

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            panelConfirmationMessage.Visible = false;
            return;
        }
        EventCartRepeater.ItemDataBound += EventCartRepeater_ItemDataBound;
        BackButton.Click += BackButton_Click;

        if (PortalContext.ViewMode == CMS.PortalEngine.ViewModeEnum.LiveSite || PortalContext.ViewMode == CMS.PortalEngine.ViewModeEnum.Preview)
        {
            if (SessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART) == null)
                URLHelper.Redirect(EventsCalendarPageURL);

            if (CartService.GetItems().Count == 0)
            {
                URLHelper.Redirect(EventsCalendarPageURL);
            }

            if (!RequestHelper.IsPostBack() && SessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART) == null)
            {
                base.RemoveSessionData();
                CartService.Destroy();
                URLHelper.Redirect(EventsCalendarPageURL);
            }


            bool successfullPayment = true;

            if (successfullPayment)
            {
                //List<DataRowView> list = CheckForDuplicateRegistrations();
                //LoadExcludedEvents(list);
                LoadRepeaters();
                SaveRegistrationsAndSendEmail();
                if (CartService.GetTotalCost() > 0.00d)
                    ShowConfirmation(ResHelper.GetStringFormat(EventsConstants.STRINGCODE_SUCCESSFULCARTREGISTRATIONCONFIRMATIONMESSAGE, CartService.GetTotalCost().ToString("0.00")));
                else
                    ShowConfirmation(ResHelper.GetStringFormat(EventsConstants.STRINGCODE_SUCCESSFULCARTREGISTRATIONCONFIRMATIONMESSAGEFREE));
            }
            else
            {
                ShowError(GetString(EventsConstants.STRINGCODE_FAILEDCARTREGISTRATIONCONFIRMATIONMESSAGE));
            }


        }

    }

    //private List<DataRowView> CheckForDuplicateRegistrations()
    //{
    //    Dictionary<string, object> registrationInfo = (Dictionary<string, object>)SessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART);
    //    string email = ValidationHelper.GetString(registrationInfo[EventsConstants.FIELDS_EVENTREGISTRATIONS_EMAIL], string.Empty);
    //    List<DataRowView> cartItems = new List<DataRowView>();
    //    cartItems.AddRange(CartService.GetItems());
    //    List<DataRowView> removedItems = new List<DataRowView>();
    //    if(!String.IsNullOrEmpty(email))
    //    {
    //        foreach (DataRowView cartItem in cartItems)
    //        {
    //            int occurrenceID = ValidationHelper.GetInteger(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0);
    //            if (occurrenceID > 0)
    //            {
    //                EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurrenceID);
    //                if (occurence.IsRegistrationExist(email, 0))
    //                {
    //                    CartService.RemoveItem(occurrenceID);
    //                    removedItems.Add(cartItem);
    //                }
    //            }
    //        }
    //    }

    //    return removedItems;

    //}

    private void SaveRegistrationsAndSendEmail()
    {
        SaveRegistrationStatus status = base.SaveRegistrationsAndSendEmail();
        if (status != SaveRegistrationStatus.SUCCESS)
        {
            SessionHelper.SetValue(EventsConstants.SESSIONKEY_SAVEREGISTRATIONSTATUS, status);
            RedirectToCartPage();
        }
    }

    private void RedirectToCartPage()
    {
        if (!string.IsNullOrEmpty(EventCartPageUrl))
            URLHelper.Redirect(EventCartPageUrl);
    }

    private void LoadExcludedEvents(List<DataRowView> excludedEvents)
    {
        if (excludedEvents.Count > 0)
        {
            ExcludedEventsRepeater.TransformationName = ExcludedEventsTransformation;
            ExcludedEventsRepeater.DataSource = excludedEvents;
            ExcludedEventsRepeater.DataBind();
            ((LocalizedLiteral)ExcludedEventsRepeater.Controls[0].FindControl("EventCartHeaderLiteral")).Text = ExcludedEventsHeaderTemplateText;
        }
        else
            ExcludedEventsRepeater.Visible = false;

    }

    private void LoadRepeaters()
    {
        List<DataRowView> cartItems = CartService.GetItems();
        if (cartItems.Count > 0)
            Session[CARTSERVICEITEMS] = cartItems;
        EventCartRepeater.TransformationName = EventsCartTransformationName;

        EventCartRepeater.DataSource = CartService.GetItems();
        EventCartRepeater.DataBind();
        TotalLiteral.Text = "$" + CartService.GetTotalCost().ToString("0.00");

        EventCartEmailRepeater.TransformationName = EventsCartTransformationNameUsedInEmail;
        EventCartEmailRepeater.DataSource = CartService.GetItems();
        EventCartEmailRepeater.DataBind();
        //((LocalizedLiteral)EventCartEmailRepeater.Controls[0].FindControl("EventCartEmailHeaderLiteral")).Text = EventCartHeaderTemplateText;        
    }
    protected void EventCartRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            LocalizedLiteral ctrlEventCartHeaderLiteral = (LocalizedLiteral)e.Item.FindControl("EventCartHeaderLiteral");
            if (ctrlEventCartHeaderLiteral != null)
                ctrlEventCartHeaderLiteral.Text = EventCartHeaderTemplateText;
        }
        else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            List<DataRowView> cartItems = null;
            if (Session[CARTSERVICEITEMS] != null)
                cartItems = (List<DataRowView>)Session[CARTSERVICEITEMS];
            Panel pnlAddToCalendar = (Panel)e.Item.Controls[0].FindControl("pnlAddToCalendar");
            if (cartItems != null && pnlAddToCalendar != null)
            {
                foreach (DataRowView cartItem in cartItems)
                {
                    int itemOccurrenceID = 0;
                    LocalizedHidden hdnOccuranceID = (LocalizedHidden)e.Item.Controls[0].FindControl("hdnOccurrenceID");
                    if (!int.TryParse(hdnOccuranceID.Value, out itemOccurrenceID))
                        break;
                    occurrenceID = ValidationHelper.GetInteger(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0);
                    string selectedSession = ValidationHelper.GetString(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS], string.Empty);
                    string[] sessionIDs = selectedSession.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (sessionIDs.Length > 0)
                    {
                        foreach (string item in sessionIDs)
                        {
                            if (occurrenceID != itemOccurrenceID)
                                break;
                            commaSeperatedSessionIDs = item;
                            Literal ltAddToCalendar = new Literal();
                            ltAddToCalendar.Text += AddEventToCalender();
                            pnlAddToCalendar.Controls.Add(ltAddToCalendar);
                            if (ShowAddToOutlookCalendar)
                            {
                                LinkButton lnkAddToOutlook = new LinkButton();
                                lnkAddToOutlook.Text = ResHelper.GetString("Emerge.EC.AddtoOutlookCalendar");
                                lnkAddToOutlook.Click += lnkAddToOutlook_Click;
                                pnlAddToCalendar.Controls.Add(lnkAddToOutlook);
                            }
                        }
                    }
                    else
                    {
                        Literal ltAddToCalendar = new Literal();
                        ltAddToCalendar.Text += AddEventToCalender();
                        pnlAddToCalendar.Controls.Add(ltAddToCalendar);
                        if (ShowAddToOutlookCalendar)
                        {
                            LinkButton lnkAddToOutlook = new LinkButton();
                            lnkAddToOutlook.Text = "Add To Outlook";
                            lnkAddToOutlook.Click += lnkAddToOutlook_Click;
                            pnlAddToCalendar.Controls.Add(lnkAddToOutlook);
                        }
                    }
                }
            }
        }
    }

    void BackButton_Click(object sender, EventArgs e)
    {
        base.RemoveSessionData();
        CartService.Destroy();
        URLHelper.Redirect(EventsCalendarPageURL);
    }
    protected void lnkAddToOutlook_Click(object sender, EventArgs e)
    {
        StringBuilder calendarLink = new StringBuilder();
        Hashtable hashedParameterEvent = new Hashtable();
        hashedParameterEvent.Add("@occurenceID", Convert.ToString(occurrenceID));
        AddEventToCalendarWebpart addToOutlookCalendar = new AddEventToCalendarWebpart();
        DataSet ds = addToOutlookCalendar.getAddToCalendarEventDetails(hashedParameterEvent);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string EventName = Convert.ToString(ds.Tables[0].Rows[0]["EventName"]);
            string eventDescription = Convert.ToString(ds.Tables[0].Rows[0]["EventDescription"]);
            //EventDescription = Regex.Replace(EventDescription, "<.*?>", " ");		
            //EventDescription = Regex.Replace(EventDescription, "&nbsp;", " ");		
            //EventDescription = Convert.ToString(EventDescription).Replace("'", "''");		
            string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
            //Code added for location		
            Hashtable hashedParameterLocation = new Hashtable();
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["StartTime"]);
            string strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["EndTime"]);
            if (!String.IsNullOrEmpty(commaSeperatedSessionIDs))
            {
                string queryName = string.Format(EventsConstants.QUERY_GETSELECTEDSESSIONS, SiteContext.CurrentSiteName);
                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@SelectedSessions", commaSeperatedSessionIDs);
                DataSet sessionDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
                strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDS.Tables[0].Rows[0]["StartTime"].ToString();
                strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDS.Tables[0].Rows[0]["EndTime"].ToString();
            }
            DateTime dtStartTime = Convert.ToDateTime(strStartTime);
            dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            addToOutlookCalendar.getOutlookLink(this.Context, EventName, Convert.ToDateTime(dtStartTime).ToString(DateFormat), Convert.ToDateTime(dtEndTime).ToString(DateFormat), Location, eventDescription);
        }
    }
    private string AddEventToCalender()
    {
        StringBuilder calendarLink = new StringBuilder();
        Hashtable hashedParameterEvent = new Hashtable();
        hashedParameterEvent.Add("@occurenceID", Convert.ToString(occurrenceID));
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
            //Code added for location		
            Hashtable hashedParameterLocation = new Hashtable();
            TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            string strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["StartTime"]);
            string strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["EndTime"]);
            if (!String.IsNullOrEmpty(commaSeperatedSessionIDs))
            {
                string queryName = string.Format(EventsConstants.QUERY_GETSELECTEDSESSIONS, SiteContext.CurrentSiteName);
                QueryDataParameters parameters = new QueryDataParameters();
                parameters.Add("@SelectedSessions", commaSeperatedSessionIDs);
                DataSet sessionDS = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
                strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDS.Tables[0].Rows[0]["StartTime"].ToString();
                strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + sessionDS.Tables[0].Rows[0]["EndTime"].ToString();
            }
            DateTime dtStartTime = Convert.ToDateTime(strStartTime);
            dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
            DateTime dtEndTime = Convert.ToDateTime(strEndTime);
            dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
            if (ShowAddToGoogleCalendar)
            {
                calendarLink.Append(addEventToCalendar.getGoogleCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, "Google Calendar"));
            }
            if (ShowAddToYahooCalendar)
            {
                calendarLink.Append(addEventToCalendar.getYahooCalendarLink(EventName, dtStartTime.ToString(DateFormat), dtEndTime.ToString(DateFormat), Location, EventDescription, "Yahoo Calendar"));
            }
        }
        return calendarLink.ToString();
    }
    private void AddEventToOutlookCalendar(HttpContext context)
    {
        List<DataRowView> cartItems = new List<DataRowView>();
        cartItems = CartService.GetItems();
        foreach (DataRowView cartItem in cartItems)
        {
            int occurrenceID = ValidationHelper.GetInteger(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID], 0);
            Hashtable hashedParameterEvent = new Hashtable();
            hashedParameterEvent.Add("@occurenceID", Convert.ToString(occurrenceID));
            AddEventToCalendarWebpart addToOutlookCalendar = new AddEventToCalendarWebpart();
            DataSet ds = addToOutlookCalendar.getAddToCalendarEventDetails(hashedParameterEvent);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string EventName = Convert.ToString(ds.Tables[0].Rows[0]["EventName"]);
                string EventDescription = Convert.ToString(ds.Tables[0].Rows[0]["EventDescription"]);
                EventDescription = Regex.Replace(EventDescription, "<.*?>", " ");
                EventDescription = Regex.Replace(EventDescription, "&nbsp;", " ");
                EventDescription = Convert.ToString(EventDescription).Replace("'", "''");
                string Location = Convert.ToString(ds.Tables[0].Rows[0]["EventLocation"]);
                //Code added for location		
                Hashtable hashedParameterLocation = new Hashtable();
                TimeZoneInfo estTimezone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                string strStartTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + ValidationHelper.GetString(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME], string.Empty);//OccurenceDate.Text + " " + StartTime.Text;		
                DateTime dtStartTime = Convert.ToDateTime(strStartTime);
                dtStartTime = TimeZoneInfo.ConvertTimeToUtc(dtStartTime, estTimezone);
                string strEndTime = Convert.ToString(ds.Tables[0].Rows[0]["OccurenceDate"]) + " " + Convert.ToString(ds.Tables[0].Rows[0]["EndTime"]);
                DateTime dtEndTime = Convert.ToDateTime(strEndTime);
                dtEndTime = TimeZoneInfo.ConvertTimeToUtc(dtEndTime, estTimezone);
                addToOutlookCalendar.getOutlookLink(this.Context, EventName, Convert.ToDateTime(DateFormat).ToString(DateFormat), Convert.ToDateTime(dtEndTime).ToString(DateFormat), Location, EventDescription);
            }
        }
    }
}