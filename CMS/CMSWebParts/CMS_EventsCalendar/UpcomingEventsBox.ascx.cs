using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using System.Data;
using CMS.Helpers;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_EventsCalendar_UpcomingEventsBox : UpcomingEventsBoxWebpart 
{
    #region Properties
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }


    /// <summary>
    /// on click of "View All Events" button, control will be redirected to this page
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

    public int EventsCount
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("EventsCount"), 0);
        }
        set
        {
            SetValue("EventsCount", value);
        }
    }

    /// <summary>
    /// Transformation Name for Session List.
    /// </summary>
    public string EventsRepeaterTransformationName
    {
        get
        {

            return ValidationHelper.GetString(GetValue("EventsRepeaterTransformationName"), EventsRepeater.TransformationName);
        }
        set
        {
            SetValue("EventsRepeaterTransformationName", value);
            EventsRepeater.TransformationName = value;
        }
    }


    #endregion

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            UpcomingEventsPanel.Visible = false;
            return;
        }
        loadEvents();
        ViewAllEventsLink.HRef = this.EventsCalendarPage;
    }

    private void loadEvents()
    {
        DataSet eventsDS = GetUpcomingEvents(this.EventsCount);
        EventsRepeater.TransformationName = EventsRepeaterTransformationName;
        EventsRepeater.DataSource = eventsDS;
        EventsRepeater.DataBind();
    }
}