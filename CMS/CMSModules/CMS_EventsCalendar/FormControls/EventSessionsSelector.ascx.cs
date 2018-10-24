using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_FormControls_EventSessionsSelector : EmergeBaseFormEngineUserControl
{
    #region Properties
    const string EVENTSSESSIONS = "EventsSessions";
    const string OCCURENCEID = "OccurenceID";
    private string selectedSessions = string.Empty;
    /// <summary>
    /// Gets or sets the value of the control.
    /// </summary>
    public override object Value
    {
        get
        {
            string returnValue = string.Empty;
            List<ListItem> list = SessionListBox.Items.OfType<ListItem>().Where(x => x.Selected == true).ToList<ListItem>();
            foreach (ListItem item in list)
            {
                returnValue += item.Value + "|"; 
            }
            return returnValue;
        }
        set
        {
            string val = Convert.ToString(value);
            

            selectedSessions = val;
        }
    }

    /// <summary>
    /// Checks whether the control is valid.
    /// </summary>
    /// <returns></returns>
    public override bool IsValid()
    {
        if (!SessionListBox.Visible)
            return true;
        bool result = true;
        List<EventSession> selectedSessions = getSelectedSessions();

        if (selectedSessions != null && selectedSessions.Count == 0)
        {
            ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }
        result = EventsCalendarHelper.ValidateSessions(selectedSessions);

        if (!result)
            ValidationError = ResHelper.GetString("Emerge.EC.SessionsClash");
        return result;
        
    }

    private List<EventSession> getSelectedSessions()
    {
        List<ListItem> list = SessionListBox.Items.OfType<ListItem>().Where(x => x.Selected == true).ToList<ListItem>();
        List<EventSession> sessions = null;
        if (ViewState[EVENTSSESSIONS] != null)
            sessions = (List<EventSession>)ViewState[EVENTSSESSIONS];
        else
            sessions = getSessions();
        List<EventSession> selectedSessions = new List<EventSession>();
        foreach (ListItem item in list)
        {
            int sessionID = Convert.ToInt32(item.Value);
            EventSession session = sessions.Find(x => x.SessionID == sessionID);
            if (session != null)
                selectedSessions.Add(session);
        }
        return selectedSessions;
    }

    public string OccurenceFieldName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OccurenceFieldName"), string.Empty);
        }
        set
        {
            SetValue("OccurenceFieldName", value);
        }
    }

    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionListBox.Visible = true;
        NoSessionsMessage.Visible = false;
        List<EventSession> sessions = getSessions();
        if (ViewState[OCCURENCEID] == null || (int)ViewState[OCCURENCEID] != Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(Form.GetFieldValue(OccurenceFieldName))) ? 0 : Form.GetFieldValue(OccurenceFieldName)))
        {
            ViewState[OCCURENCEID] = Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(Form.GetFieldValue(OccurenceFieldName))) ? 0 : Form.GetFieldValue(OccurenceFieldName));

            
            SessionListBox.Items.Clear();
            if (sessions.Count > 0)
                ViewState[EVENTSSESSIONS] = sessions;
            else
            {
                SessionListBox.Visible = false;
                NoSessionsMessage.Visible = true;
                NoSessionsMessage.Text = ResHelper.GetString(EventsConstants.STRINGCODE_NOSESSIONSMESSAGE);
                return;
            }
            foreach (EventSession session in sessions)
            {
                SessionListBox.Items.Add(new ListItem(session.Title + " - " + session.StartTime + " - " + session.EndTime, session.SessionID.ToString()));
            }
        }
        if(sessions.Count == 0)
        {
            SessionListBox.Visible = false;
            NoSessionsMessage.Visible = true;
            NoSessionsMessage.Text = ResHelper.GetString(EventsConstants.STRINGCODE_NOSESSIONSMESSAGE);
            return;
        }
        setSelectedSessions();
    }

    private void setSelectedSessions()
    {
        if (!String.IsNullOrEmpty(selectedSessions) && !RequestHelper.IsPostBack())
        {
            string[] sessionIDs = selectedSessions.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (ListItem item in SessionListBox.Items)
            {
                if (sessionIDs.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
        }
    }

    private List<EventSession> getSessions()
    {
        int occurenceID = Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(Form.GetFieldValue(OccurenceFieldName))) ? 0 : Form.GetFieldValue(OccurenceFieldName));
        if (occurenceID <= 0)
            return new List<EventSession>();
        List<EventSession> sessions = EventsCalendarHelper.GetSessionsByOccurenceID(occurenceID);
        return sessions;
    }
}