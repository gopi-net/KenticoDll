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

public partial class CMSWebParts_CMS_EventsCalendar_VolunteerEventDetailsView : VolunteerEventDetailsViewWebpart
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
    /// on click of Back button, control will be redirected to this page
    /// </summary>
    public string SelectTimeSlotsURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("SelectTimeSlotsURL"), string.Empty);
        }
        set
        {
            SetValue("SelectTimeSlotsURL", value);
        }
    }
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            EventsDetailsPanel.Visible = false;
            return;
        }

        if (!EmergeCMSContext.CurrentUser.IsInRole(EventsConstants.ROLE_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteName))
        {
            plcMess.ShowMessage(MessageTypeEnum.Error, GetString(EventsConstants.STRINGCODE_NOTAVOLUNTEERUSER), string.Empty, string.Empty, true);
            EventsDetailsPanel.Visible = false;
            return;
        }

    //    if (!RequestHelper.IsPostBack())
            this.OccurenceIDField.Value = this.OccurenceID.ToString();
            BindInsuranceDataToRepeater();
        loadEventDetails();
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        SelectTimeslotsButton.Click += SelectTimeslotsButton_Click;
         //ViewVolunteerRegistration.Click += ViewVolunteerRegistration_Click;		
    }		
    void ViewVolunteerRegistration_Click(object sender, EventArgs e)		
    {		
    }
    private void BindInsuranceDataToRepeater()
    {
        UserRegistrationRepeater.ZeroRowsText = "No Data Found";
        UserRegistrationRepeater.TransformationName = "customtable.Emerge_EmergeWebsite_EC_Events.RegisteredUserVolunteer";//string.Format(PreRegistrationConstants.PR_INSURANCE_INFORMATION_DEFAULT, EmergeCMSContext.CurrentSiteName);		
        UserRegistrationRepeater.DataSource = getVolunteerRegisteredForEvent();
        UserRegistrationRepeater.DataBind();
    }

    void SelectTimeslotsButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(SelectTimeSlotsURL))
            URLHelper.Redirect(SelectTimeSlotsURL + "/" + this.OccurenceIDField.Value);
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
}