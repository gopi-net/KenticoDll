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
using System.Data;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_EventsCalendar_ChooseTimeSlotView : ChooseTimeSlotViewWebpart
{
    #region Properties
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), SessionsRepeater.TransformationName);
        }
        set
        {
            SetValue("TransformationName", value);
           
        }
    }

    public string ThankYouPageURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("ThankYouPageURL"), string.Empty);
        }
        set
        {
            SetValue("ThankYouPageURL", value);
        }
    }
    #endregion 

    protected override void OnInit(EventArgs e)
    {
        ControlPanel = this.TimeslotPanel;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (StopProcessing)
            {
                TimeslotPanel.Visible = false;
                return;
            }

            if (!RequestHelper.IsPostBack())
            {
                this.OccurenceIDField.Value = this.OccurenceID.ToString();
                if (!EmergeCMSContext.CurrentUser.IsInRole(EventsConstants.ROLE_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteName))
                {
                    plcMess.ShowMessage(MessageTypeEnum.Error, String.Format(GetString(EventsConstants.STRINGCODE_NOTAVOLUNTEERUSER), EmergeCMSContext.CurrentUser.UserName), string.Empty, string.Empty, true);
                    TimeslotPanel.Visible = false;
                }
            }

            loadEventDetails();
            registerEvents();
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    private void registerEvents()
    {
        RegisterButton.Click += RegisterButton_Click;
    }

    void RegisterButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (String.IsNullOrEmpty(SelectedSession.Value.Trim()))
            {
                MessageInfo.Text = GetString(EventsConstants.STRINGCODE_VOLUNTEERSELECTSESSIONMESSAGE);
                return;
            }
            if (ValidateSessions(SelectedSession.Value.Trim()))
            {
                MessageInfo.Text = string.Empty;
                if (RegisterUser())
                {
                    SessionHelper.SetValue(EventsConstants.USERSELECTEDSESSIONS, SelectedSession.Value.Trim());
                    URLHelper.Redirect(this.ThankYouPageURL + "/" + this.OccurenceIDField.Value);
                }
                else
                    MessageInfo.Text = GetString(EventsConstants.STRINGCODE_REGSITRATIONSSAVEEXCEPTIONMESSAGE);
            }
            else
                MessageInfo.Text = GetString(EventsConstants.STRINGCODE_SESSIONCLASH);
        }
        catch (EmailSendException)
        {
            MessageInfo.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONEMAILSENDMESSAGE);
        }
        catch (RegistrationExistException)
        {
            MessageInfo.Text = GetString(EventsConstants.STRINGCODE_VOLUNTEERDUPLICATEREGISTRTAION);
        }
        catch (EventRegistrationLimitReachedException)
        {
            MessageInfo.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONCLOSEDMESSAGE);
        }
        catch (UserNotVolunteerException)
        {
            MessageInfo.Text = String.Format(GetString(EventsConstants.STRINGCODE_NOTAVOLUNTEERUSER), EmergeCMSContext.CurrentUser.UserName);
        }
        catch (Exception ex)
        {
            //OnError(ex);
        }
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
                SessionsRepeater.TransformationName = this.TransformationName;
                SessionsRepeater.DataSource = getSessionDetails();
                SessionsRepeater.DataBind();

                enableDisableFields(eventDataRow);
            }
            return;
        }
        ShowErrorMessage(status);
    }

    private void ShowErrorMessage(OccurenceStatus status)
    {
        TimeslotPanel.Visible = false;
        string message = string.Empty;
        switch (status)
        {
            case OccurenceStatus.INVALIDVOLUNTEEROCCURENCE:
                message = GetString(EventsConstants.STRINGCODE_INVALIDVOLUNTEEROCCURENCE);
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
            if (occurrence.Schedule.Event.EventType != EventsConstants.EVENTTYPE_VOLUNTEER)
            {
                status = OccurenceStatus.INVALIDVOLUNTEEROCCURENCE;
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
            //EventsDetailsPanel.Visible = false;
            //plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_OCCURRENCEDOESNOTEXISTS), string.Empty, string.Empty, true);
            status = OccurenceStatus.DOESNOTEXIST;
        }
        return status;
    }

    private void enableDisableFields(DataRow eventDataRow)
    {
        RegisterButton.Visible = false;
        int occurenceID = ValidationHelper.GetInteger(this.OccurenceIDField.Value, 0);

        if (occurenceID > 0)
        {
            EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
            
            if (occurence.Schedule.NeedRegistrations)
            {
                if (!occurence.IsRegistrationLimitReached())
                    RegisterButton.Visible = true;
                else
                {
                    RegisterButton.Visible = false;
                    MessageInfo.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONCLOSEDMESSAGE);
                }
            }
            else
                MessageInfo.Text = GetString(EventsConstants.STRINGCODE_DOESNOTNEEDREGSITRATIONS);
        }
    }

    private bool fieldExists(string fieldID)
    {
        if (this.TimeslotPanel.FindControl(fieldID) != null)
            return true;
        return false;
    }

    private void setFieldValue(string fieldID, string value)
    {
        Control field = this.TimeslotPanel.FindControl(fieldID);
        if (field is Literal)
        {
            Literal literalControl = (Literal)field;
            literalControl.Text = value;
        }
    }
}