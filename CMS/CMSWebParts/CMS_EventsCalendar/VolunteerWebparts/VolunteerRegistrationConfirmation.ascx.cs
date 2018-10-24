using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using CMS.Base.Web.UI;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CMSWebParts_CMS_EventsCalendar_VolunteerRegistrationConfirmation : VolunteerEventRegistrationConfirmationWebpart
{
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

    protected override void OnInit(EventArgs e)
    {
        ControlPanel = this.ConfirmationPanel;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ConfirmationPanel.Visible = false;
            return;
        }

        if (!EmergeCMSContext.CurrentUser.IsInRole(EventsConstants.ROLE_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteName))
        {
            plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_NOTAVOLUNTEERUSER), string.Empty, string.Empty, true);
            ConfirmationPanel.Visible = false;
        }
        BackButton.Click += BackButton_Click;
        loadEventDetails();
        ShowConfirmationMessage();
    }

    private void loadEventDetails()
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

            DataSet sessionDS = getSelectedSessions();
            SessionsRepeater.TransformationName = this.SessionViewTransformationName;
            SessionsRepeater.DataSource = sessionDS;
            SessionsRepeater.DataBind();
        }
        else
        {
        }
    }

    void BackButton_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(CalendarHomePageURL);
    }

    private void ShowConfirmationMessage()
    {
        ThankYouMessage.Text = GetString(EventsConstants.STRINGCODE_VOLUNTEERREGISTRATIONCONFIRMATIONMESSAGE);
    }
}