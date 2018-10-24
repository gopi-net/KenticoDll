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
using System.Web.UI.HtmlControls;
using Bluespire.Emerge.Components.EventsCalendar;
using System.Threading;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.FormEngine.Web.UI;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_EventSchedule : EventsCalendarDataEditItemPage
{
    private string editItemPage = EventsConstants.PAGEURL_NEW_SCHEDULE;
    string eventId;
    private DataClassInfo dci = null;
    private const string ITEMID = "ItemID";
    private const string ID_CHKPANEL = "chkPanel";
    private const string ID_PARENT = "parent";
    private const string ID_TXTPANEL = "textPanel";
    private const string CLASHEDDATES = "ClashedDates";    
    
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = EventsConstants.PAGEURL_DATA_LIST;
            NewItemPage = EventsConstants.PAGEURL_NEW_SCHEDULE;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          
            if (ViewState[CLASHEDDATES] != null)
            {
                GetDynamicControlsForClashDates();
//                ViewState[CLASHEDDATES] = null;
            }
            if (ViewState[EventsConstants.REGISTRATIONSPOPUP] != null && (bool)ViewState[EventsConstants.REGISTRATIONSPOPUP] == true)
            {
                Control popup = (Control)SessionHelper.GetValue(EventsConstants.POPUPCONTROL);
                RegistrationsPopup.Body.Controls.Clear();
                RegistrationsPopup.Body.Controls.Add(popup);
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }
    private void selectDefaultEvent()
    {
        if (!IsWizard())
            return;
        EditingFormControl eventIdControl = EventFormControl.CustomTableForm.FieldEditingControls["EventID"] as EditingFormControl;
        if (eventIdControl == null)
            return;
        eventId = Request.QueryString["EventID"].ToString();
        eventIdControl.Value = eventId;
    }

    private bool IsWizard()
    {
        return Request.QueryString["EventID"] != null;
    }
    private void RegisterEvents()
    {
        EventFormControl.OnAfterSave += EventFormControl_OnAfterSave;
        EventFormControl.OnBeforeSave += EventFormControl_OnBeforeSave;
        EventFormControl.PreRender += EventFormControl_PreRender;
        SaveButton.Click += SaveButton_Click;
        BackButton.Visible = IsWizard();
        BackButton.Click += BackButton_Click;
        EmergePopup.OnOKButtonClick += EmergePopup_OnOKButtonClick;
        InformationPopup.OnOKButtonClick += InformationPopup_OnOKButtonClick;
        RegistrationsPopup.OnOKButtonClick += RegistrationsPopup_OnOKButtonClick;
        RegistrationsPopup.OnCancelButtonClick += RegistrationsPopup_OnCancelButtonClick;
        RegistrationConflictPopup.OnOKButtonClick += RegistrationConflictPopup_OnOKButtonClick;
        EmergePopup.OnCancelButtonClick += EmergePopup_OnCancelButtonClick;
        DeadlinePopup.OnOKButtonClick += DeadlinePopup_OnOKButtonClick;
        DeadlinePopup.OnCancelButtonClick += DeadlinePopup_OnCancelButtonClick;
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Form.aspx?customtablename=customtable.Emerge_{0}_EC_Events&itemId=" + Request.QueryString["EventID"].ToString());
    }

    void EventFormControl_PreRender(object sender, EventArgs e)
    {
        selectDefaultEvent();
    }

    void DeadlinePopup_OnCancelButtonClick(object sender, EventArgs e)
    {
        try
        {
            CustomTableItem item = (CustomTableItem)SessionHelper.GetValue(EventsConstants.CUSTOMTABLESCHEDULEITEM);
            if (item != null)
            {
                item.Update();
                ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULENOTSAVED));
                ClearSession();
            }
            else
            {
                int id = Convert.ToInt32(ViewState[ITEMID]);
                CustomTableDataHelper.DeleteCustomTableItem(id, CustomTableID);
                EventFormControl.CustomTableForm.ItemID = 0;
                ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULENOTSAVED));
                ClearSession();
            }
        }
        catch (CustomTableItemNotFoundException ex)
        {
            OnError(ex);
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    void DeadlinePopup_OnOKButtonClick(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(ViewState[ITEMID]);
        List<EventOccurence> occurences = (List<EventOccurence>)SessionHelper.GetValue("CurrentOccurences");
        if (occurences.Count > 0)
        {
            CheckForScheduleRegistrations(id, occurences);
        }
    }

    void EmergePopup_OnCancelButtonClick(object sender, EventArgs e)
    {
        CancelSave();
    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        if (EventFormControl.CustomTableForm.ValidateData())
        {
            saveData();
        }
    }

    void EventFormControl_OnBeforeSave(object sender, EventArgs e)
    {
        EventSchedule schedule = new EventSchedule();
        if (ItemID > 0)
        {
            schedule = EventsCalendarHelper.GetScheduleByScheduleID(ItemID);
            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(ItemID, string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, EmergeCMSContext.CurrentSiteName));
            SessionHelper.SetValue(EventsConstants.CUSTOMTABLESCHEDULEITEM, item);                        
            SessionHelper.SetValue(EventsConstants.SCHEDULE, schedule);
        }
    }

    void EventFormControl_OnAfterSave(object sender, EventArgs e)
    {
        try
        {
            int itemID = EventFormControl.CustomTableForm.ItemID;
            ViewState[ITEMID] = itemID;
            List<EventOccurence> occurences = EventsCalendarHelper.BuildOccurences(itemID, null);
            if (occurences.Count > 0)
                processOccurences(itemID, occurences);
            else
                ShowInformationPopup(ResHelper.GetString(EventsConstants.STRINGCODE_ZEROOCCURENCES));
        }
        catch (ThreadAbortException ex)
        {
            OnError(ex, false);
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    void EmergePopup_OnOKButtonClick(object sender, EventArgs e)
    {
        if (ViewState[CLASHEDDATES] != null)
        {
         //   GetDynamicControlsForClashDates();
        }
        List<DateTime> excludedDates = getExcludedDates();
        int id = Convert.ToInt32(ViewState[ITEMID]);
        List<EventOccurence> occurences = EventsCalendarHelper.BuildOccurences(id, excludedDates);

        if (occurences.Count > 0)
        {
            int registrationDeadline = ValidationHelper.GetInteger(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE), 0);
            if (occurences.OrderBy(a => a.OccurenceDate).First().OccurenceDate.AddDays(-registrationDeadline) < DateTime.Now.Date)
                ShowDeadlinePopup(occurences);
            else
                CheckForScheduleRegistrations(id, occurences);
        }
        else
            ShowInformationPopup(ResHelper.GetString(EventsConstants.STRINGCODE_ZEROOCCURENCES));
    }

    void RegistrationConflictPopup_OnOKButtonClick(object sender, EventArgs e)
    {
        Control popup = (Control)SessionHelper.GetValue(EventsConstants.POPUPCONTROL);
        RegistrationsPopup.Body.Controls.Clear();
        RegistrationsPopup.Body.Controls.Add(popup);
        RegistrationsPopup.Show();
    }

    void RegistrationsPopup_OnCancelButtonClick(object sender, EventArgs e)
    {
        CancelSave();
    }

    private void CancelSave()
    {
        try
        {
            CustomTableItem item = (CustomTableItem)SessionHelper.GetValue(EventsConstants.CUSTOMTABLESCHEDULEITEM);            
            if (item != null)
            {
                item.Update();
                ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULENOTSAVED));
                //EventFormControl.ShowChangesSaved();
                ClearSession();
            }
            else
            {
                int id = Convert.ToInt32(ViewState[ITEMID]);
                CustomTableDataHelper.DeleteCustomTableItem(id, CustomTableID);
                EventFormControl.CustomTableForm.ItemID = 0;
                ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULENOTSAVED));
                ClearSession();
            }
        }
        catch (CustomTableItemNotFoundException ex)
        {
            OnError(ex);
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    void RegistrationsPopup_OnOKButtonClick(object sender, EventArgs e)
    {
        string message = string.Empty;
        EventSchedule schedule = (EventSchedule)SessionHelper.GetValue(EventsConstants.SCHEDULE);
        List<EventOccurence> oldOccurences = (List<EventOccurence>)SessionHelper.GetValue(EventsConstants.OLDOCCURENCES);
        List<EventOccurence> newOccurences = (List<EventOccurence>)SessionHelper.GetValue(EventsConstants.NEWOCCURENCES);
        bool allowMove = checkForAllowMoveRegistrations(oldOccurences, newOccurences, ref message);
        if (!allowMove)
            ShowRegistrationConflictPopup(message);
        else
        {
            FinalizeSchedule(schedule.ScheduleID, oldOccurences, newOccurences);
            EventsCalendarHelper.MoveRegistrationsForSchedule(schedule.ScheduleID, getRegistrationsToMove());
            ClearSession();
            EventFormControl.processAfterSave();
        }
    }

    void InformationPopup_OnOKButtonClick(object sender, EventArgs e)
    {
        CancelSave();
		 URLHelper.Redirect("~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_ScheduleList.aspx?customtablename=customtable.Emerge_{0}_EC_EventsSchedule");
    }

    private void ClearSession()
    {
        SessionHelper.Remove(EventsConstants.CUSTOMTABLESCHEDULEITEM);
        SessionHelper.Remove(EventsConstants.SCHEDULE);
        SessionHelper.Remove(EventsConstants.POPUPCONTROL);
        SessionHelper.Remove(EventsConstants.OLDOCCURENCES);
        SessionHelper.Remove(EventsConstants.NEWOCCURENCES);
        SessionHelper.Remove(EventsConstants.DROPDOWNLISTITEMS);
        ViewState[EventsConstants.REGISTRATIONSPOPUP] = null;
    }

    private IDictionary<string, string> getRegistrationsToMove()
    {
        IDictionary<string, string> registrations = new Dictionary<string, string>();
        List<ListItem> list = (List<ListItem>)SessionHelper.GetValue(EventsConstants.DROPDOWNLISTITEMS);
        Control parent = RegistrationsPopup.Body.FindControl(ID_PARENT);
        Control registrationsPanel = parent.FindControl("RegistrationsPanel");
        foreach (ListItem item in list)
        {
            string value = item.Value;
            List<DropDownList> occurenceDropdownList = new List<DropDownList>();
            getDropdownListbyValue(registrationsPanel, ref occurenceDropdownList, value);
            foreach (DropDownList ddl in occurenceDropdownList)
            {
                if (!registrations.ContainsKey(ddl.ID))
                    registrations.Add(new KeyValuePair<string, string>(ddl.ID, value));
            }
        }
        return registrations;
    }

    private bool checkForAllowMoveRegistrations(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences, ref string returnMessage)
    {
        List<ListItem> list = (List<ListItem>)SessionHelper.GetValue(EventsConstants.DROPDOWNLISTITEMS);
        Control parent = RegistrationsPopup.Body.FindControl(ID_PARENT);
        Control registrationsPanel = parent.FindControl("RegistrationsPanel");
        int registrationLimit = newOccurences[0].RegistrationLimit;
        bool allowMoveRegistrations = true;
        string message = String.Format(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONMESSAGE), registrationLimit.ToString());
        foreach (ListItem item in list)
        {
            List<DropDownList> occurenceDropdownList = new List<DropDownList>();
            string value = item.Value;
            //occurenceDropdownList = registrationsPanel.Controls.OfType<DropDownList>().Where(a => a.SelectedValue == value).ToList<DropDownList>();
            getDropdownListbyValue(registrationsPanel, ref occurenceDropdownList, value);
            EventOccurence occurence = getOccurenceByDropdownValue(value, oldOccurences, newOccurences);
            int totalRegistrationCount = 0;
            if (occurence.Registrations.Count > registrationLimit)
                totalRegistrationCount = occurenceDropdownList.Count;
            else
                totalRegistrationCount = occurenceDropdownList.Count + occurence.Registrations.Count;
            if (totalRegistrationCount > registrationLimit)
            {
                allowMoveRegistrations = false;
                message += occurence.OccurenceDate.ToString() + " - " + totalRegistrationCount.ToString() + "<br/>";
            }
        }
        returnMessage = message;
        return allowMoveRegistrations;
    }

    private void getDropdownListbyValue(Control parentControl, ref List<DropDownList> occurenceDropdownList, string value)
    {
        foreach (Control control in parentControl.Controls)
        {
            if (control.GetType() == typeof(DropDownList) && ((DropDownList)control).SelectedValue == value)
                occurenceDropdownList.Add(((DropDownList)control));
            getDropdownListbyValue(control, ref occurenceDropdownList, value);
        }
    }

    private EventOccurence getOccurenceByDropdownValue(string value, List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        EventOccurence occurence = oldOccurences.Find(a => a.OccurenceDate.ToString() == value);
        if (occurence == null)
            occurence = newOccurences.Find(a => a.OccurenceDate.ToString() == value);
        return occurence;
    }

    private IDictionary<string, object> getTableData(List<string> eventOccurenceFields, EventSchedule schedule)
    {
        IDictionary<string, object> tableData = new Dictionary<string, object>();
        foreach (string field in eventOccurenceFields)
        {
            string propertyName = EmergeStaticHelper.GetPropertyNameByCustomAttribute<EventSchedule, CustomTableFieldAttribute>(x => x.FieldName == field)[0];
            string value = Convert.ToString(typeof(EventSchedule).GetProperty(propertyName).GetValue(schedule, null));
            KeyValuePair<string, object> data;

            if (!String.IsNullOrEmpty(value))
                data = new KeyValuePair<string, object>(field, value);
            else
                data = new KeyValuePair<string, object>(field, string.Empty);

            tableData.Add(data);
        }
        return tableData;
    }

    private void FinalizeSchedule(int id, List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        List<EventOccurence> toDeleteOccurenceList = getOrphanOccurences(oldOccurences, newOccurences);
        foreach (EventOccurence occ in toDeleteOccurenceList)
            if (occ.OccurenceID > 0)
                EventsCalendarHelper.DeleteEventOccurenceByIDForFinalizeSchedule(occ.OccurenceID);
        EventsCalendarHelper.SaveEventScheduleOccurences(newOccurences);
        EventsCalendarHelper.UpdateSelectedDatesForSchedule(id);
        if (IsCustomTableSelectorControlAvailable())
        {
            GetAndBulkUpdateCustomTableItems(id);
        }
    }

    private void processOccurences(int scheduleID, List<EventOccurence> occurences)
    {
        List<BlackOutDate> clashedDates = EventsCalendarHelper.GetClashedDatesForOccurences(occurences);
        if (clashedDates.Count > 0)
            ShowEmergePopup(clashedDates);
        else
        {
            int registrationDeadline = ValidationHelper.GetInteger(EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE), 0);
            if (occurences.OrderBy(a => a.OccurenceDate).First().OccurenceDate.AddDays(-registrationDeadline) < DateTime.Now.Date)
                ShowDeadlinePopup(occurences);
            else
                CheckForScheduleRegistrations(scheduleID, occurences);
        }
    }

    private void CheckForScheduleRegistrations(int scheduleID, List<EventOccurence> occurences)
    {
        List<EventOccurence> oldOccurences = EventsCalendarHelper.GetOccurencesByScheduleID(scheduleID);
        if (oldOccurences.Count > 0 && HasScheduleChanged(oldOccurences, occurences))
        {
            ShowRegistrationPopup(oldOccurences, occurences);
        }
        else
        {
            FinalizeSchedule(scheduleID, oldOccurences, occurences);
            EventFormControl.CustomTableForm.ItemID = scheduleID;
            ClearSession();
            EventFormControl.processAfterSave();
        }
    }

    private void ShowEmergePopup(List<BlackOutDate> clashedDates)
    {
        Control panel = getPopupControlBody(clashedDates);
        ViewState[CLASHEDDATES] = clashedDates;
        EmergePopup.Body.Controls.Clear();
        EmergePopup.Body.Controls.Add(panel);
        EmergePopup.Show();
        EventFormControl.CustomTableForm.StopProcessing = true;
    }

    private void ShowRegistrationPopup(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        List<EventOccurence> occurences = getConflictedOccurences(oldOccurences, newOccurences);
        List<EventOccurence> occurencesForPopup = new List<EventOccurence>();
        occurencesForPopup.AddRange(newOccurences);
        if (occurencesForPopup[0].IsSeries && occurencesForPopup.Count > 1)
        {
            occurencesForPopup.RemoveRange(1, occurencesForPopup.Count - 1);
        }
        Control popup = GetPopupBodyForRegistrations(occurences, occurencesForPopup);
        RegistrationsPopup.Body.Controls.Clear();
        RegistrationsPopup.Body.Controls.Add(popup);
        RegistrationsPopup.Show();
        EventFormControl.CustomTableForm.StopProcessing = true;
        SessionHelper.SetValue(EventsConstants.POPUPCONTROL, popup);
        ViewState[EventsConstants.REGISTRATIONSPOPUP] = true;
        SessionHelper.SetValue(EventsConstants.OLDOCCURENCES, oldOccurences);
        SessionHelper.SetValue(EventsConstants.NEWOCCURENCES, newOccurences);
    }

    private List<EventOccurence> getConflictedOccurences(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        List<EventOccurence> orphanedOccurences = getOrphanOccurences(oldOccurences, newOccurences);
        List<EventOccurence> commonOccurences = getCommonOccurences(oldOccurences, newOccurences);
        List<EventOccurence> registrationsFullOccurences = commonOccurences.Where(a => a.Registrations.Count > newOccurences[0].RegistrationLimit).ToList<EventOccurence>();
        if (commonOccurences.Count > 0)
        {
            commonOccurences = commonOccurences.OrderBy(a => a.OccurenceDate).ToList<EventOccurence>();
            if (commonOccurences[0].IsSeries && commonOccurences[0].Registrations.Count > 0 && commonOccurences[0].OccurenceDate > newOccurences[0].OccurenceDate)
            {
                registrationsFullOccurences.Add(commonOccurences[0]);
            }
        }
        List<EventOccurence> conflictedOccurences = registrationsFullOccurences.Concat(orphanedOccurences).ToList<EventOccurence>();
        return conflictedOccurences;
    }

    private void ShowInformationPopup(string message)
    {
        Panel panel = new Panel();
        LiteralControl literal = new LiteralControl();
        literal.Text = message;
        panel.Controls.Add(literal);
        InformationPopup.Body.Controls.Add(panel);
        InformationPopup.Show();
    }

    private void ShowDeadlinePopup(List<EventOccurence> occurrences)
    {
        Panel panel = new Panel();
        LiteralControl literal = new LiteralControl();
        literal.Text = GetString(EventsConstants.STRINGCODE_REGISTRATIONDEADLINECLASH);
        panel.Controls.Add(literal);
        DeadlinePopup.Body.Controls.Add(panel);
        DeadlinePopup.Show();
        SessionHelper.SetValue("CurrentOccurences", occurrences);
        EventFormControl.CustomTableForm.StopProcessing = true;
    }

    private void ShowRegistrationConflictPopup(string message)
    {
        LiteralControl control = new LiteralControl();
        control.Text = message;
        RegistrationConflictPopup.Body.Controls.Add(control);
        RegistrationConflictPopup.Show();
    }

    private List<EventOccurence> getOrphanOccurences(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        List<EventOccurence> orphanedOccurences = oldOccurences.Except<EventOccurence>(newOccurences, new EventOccurenceComparer()).ToList<EventOccurence>();
        return orphanedOccurences;
    }

    private List<EventOccurence> getCommonOccurences(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        List<EventOccurence> commonOccurences = oldOccurences.Intersect<EventOccurence>(newOccurences, new EventOccurenceComparer()).ToList<EventOccurence>();
        return commonOccurences;
    }

    private bool HasScheduleChanged(List<EventOccurence> oldOccurences, List<EventOccurence> newOccurences)
    {
        bool hasChanged = false;
        List<EventOccurence> orphanedOccurences = getOrphanOccurences(oldOccurences, newOccurences);
        if (orphanedOccurences.Count > 0 && orphanedOccurences.Any<EventOccurence>(x => x.Registrations.Count > 0))
            hasChanged = true;
        if (!hasChanged)
        {
            List<EventOccurence> commonOccurences = getCommonOccurences(oldOccurences, newOccurences);
            commonOccurences = commonOccurences.OrderBy(a => a.OccurenceDate).ToList<EventOccurence>();
            if (commonOccurences.Count > 0)
            {
                if (commonOccurences[0].IsSeries && commonOccurences[0].Registrations.Count > 0 && commonOccurences[0].OccurenceDate > newOccurences[0].OccurenceDate)
                {
                    return true;
                }
            }
            hasChanged = commonOccurences.Any(a => a.Registrations.Count > newOccurences[0].RegistrationLimit);
        }
        return hasChanged;
    }

    private List<DateTime> getExcludedDates()
    {
        List<DateTime> excludedDates = new List<DateTime>();
        Control parent = EmergePopup.Body.FindControl(ID_PARENT);
        Control chkPanel = parent.FindControl(ID_CHKPANEL);
        foreach (CheckBox chk in chkPanel.Controls.OfType<CheckBox>())
        {
            if (!chk.Checked)
            {
                DateTime date = Convert.ToDateTime(chk.Text);
                excludedDates.Add(date);
            }
        }
        return excludedDates;
    }

    private Control GetPopupBodyForRegistrations(List<EventOccurence> occurences, List<EventOccurence> newOccurences)
    {
        Panel parent = new Panel();
        parent.ID = ID_PARENT;
        Panel registrationPanel = new Panel();
        registrationPanel.ID = "RegistrationsPanel";
        AddDropdownListItemsToSession(newOccurences);
        HtmlTable table = new HtmlTable();
        table.Width = "100%";
        foreach (EventOccurence occurence in occurences.Where(z => z.Registrations.Count > 0))
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            LiteralControl literal = new LiteralControl();
            literal.Text = "<b>" + occurence.OccurenceDate.ToString() + "</b>";
            cell.Controls.Add(literal);
            row.Cells.Add(cell);
            row.Cells.Add(new HtmlTableCell());
            table.Rows.Add(row);
            foreach (EventRegistration registration in occurence.Registrations)
            {
                row = new HtmlTableRow();
                cell = new HtmlTableCell();
                literal = new LiteralControl();
                literal.Text = registration.FirstName + " " + registration.LastName;
                cell.Controls.Add(literal);
                row.Cells.Add(cell);
                cell = new HtmlTableCell();
                DropDownList dropdown = getNewOccurenceDropdownList(registration.ItemID.ToString(), newOccurences);
                cell.Controls.Add(dropdown);
                row.Cells.Add(cell);
                table.Rows.Add(row);
            }
        }

        bool isSeries = newOccurences[0].IsSeries;
        if (isSeries)
        {
            string message = ResHelper.GetString(EventsConstants.STRINGCODE_OCCURENCEDROPDOWNMESSAGE);
            LiteralControl control = new LiteralControl();
            control.Text = message;
            registrationPanel.Controls.Add(control);
        }
        registrationPanel.Controls.Add(table);
        parent.Controls.Add(registrationPanel);
        return parent;
    }

    private void AddDropdownListItemsToSession(List<EventOccurence> newOccurences)
    {
        List<ListItem> dropdownItems = new List<ListItem>();
        foreach (EventOccurence occu in newOccurences)
            dropdownItems.Add(new ListItem(occu.OccurenceDate.ToString(), occu.OccurenceDate.ToString()));
        SessionHelper.SetValue(EventsConstants.DROPDOWNLISTITEMS, dropdownItems);
    }

    private DropDownList getNewOccurenceDropdownList(string id, List<EventOccurence> newOccurences)
    {
        DropDownList dropDown = new DropDownList();
        dropDown.ID = id;
        foreach (EventOccurence occu in newOccurences)
            dropDown.Items.Add(new ListItem(occu.OccurenceDate.ToString(), occu.OccurenceDate.ToString()));
        return dropDown;
    }

    private Control getPopupControlBody(List<BlackOutDate> clashedDates)
    {
        Panel parent = new Panel();
        parent.ID = ID_PARENT;
        Panel chkPanel = new Panel();
        chkPanel.ID = ID_CHKPANEL;
        Panel textPanel = new Panel();
        textPanel.ID = ID_TXTPANEL;
        string bookingNotAllowedDate = string.Empty;
        if (clashedDates.Any(a => a.AllowBooking))
        {
            LiteralControl literal = new LiteralControl();
            literal.Text = string.Format(ResHelper.GetString(EventsConstants.STRINGCODE_BLACKOUTDATESALLOWBOOKING), bookingNotAllowedDate);
            chkPanel.Controls.Add(literal);
            chkPanel.Controls.Add(new LiteralControl("<br/><br/>"));
        }
        foreach (BlackOutDate date in clashedDates)
        {
            CheckBox chk = new CheckBox();
            chk.ID = date.BlackOutDateID.ToString();
            chk.Text = date.Date.ToString();
            if (!date.AllowBooking)
            {
                bookingNotAllowedDate += date.Date.ToString(Constants.EMERGE_DATEFORMAT) + ", ";
                chk.Visible = false;
            }
            chkPanel.Controls.Add(chk);
            chkPanel.Controls.Add(new LiteralControl("<br/>"));
        }
        chkPanel.Controls.Add(new LiteralControl("<br/>"));
        bookingNotAllowedDate.TrimEnd(',', ' ');
        if (!string.IsNullOrEmpty(bookingNotAllowedDate))
        {
            LiteralControl literal = new LiteralControl();
            literal.Text = string.Format(ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULECLASH), bookingNotAllowedDate);
            textPanel.Controls.Add(literal);
        }
        parent.Controls.Add(chkPanel);
        parent.Controls.Add(textPanel);

        return parent;
    }

    private bool IsCustomTableSelectorControlAvailable()
    {
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider();
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableID);

        bool isCustomTableAvailable = false;
        if (customTable != null)
        {
            foreach (string columnName in EventFormControl.CustomTableForm.Fields)
            {
                FormFieldInfo ffi = EventFormControl.CustomTableForm.FieldControls[columnName].FieldInfo;
                HiddenField hdnItemIDsForUpdate = ((HiddenField)(EventFormControl.CustomTableForm.FieldControls[columnName].FindControl("hdnItemIdsForUpdate")));

                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower()) && hdnItemIDsForUpdate != null)
                {
                    isCustomTableAvailable = true;
                    break;
                }
            }
        }
        return isCustomTableAvailable;
    }

    private void GetAndBulkUpdateCustomTableItems(int scheduleID)
    {
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
        if (customTable != null)
        {
            foreach (string columnName in EventFormControl.CustomTableForm.Fields)
            {
                FormFieldInfo ffi = EventFormControl.CustomTableForm.FieldControls[columnName].FieldInfo;
                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower()))
                {
                    string where = string.Empty;

                    HiddenField hdnItemIDsForUpdate = ((HiddenField)(EventFormControl.CustomTableForm.FieldControls[columnName].FindControl("hdnItemIdsForUpdate")));
                    string itemIdsForUpdate = string.Empty;

                    if (hdnItemIDsForUpdate != null)
                    {
                        itemIdsForUpdate = hdnItemIDsForUpdate.Value;
                        if (!itemIdsForUpdate.Equals(string.Empty))
                        {
                            if (itemIdsForUpdate.Trim().EndsWith(","))
                            {
                                itemIdsForUpdate = itemIdsForUpdate.Substring(0, itemIdsForUpdate.LastIndexOf(","));
                            }
                            where = " ItemID in ( " + itemIdsForUpdate.Replace('|', ',') + " )";
                            DataSet customTableItems = CustomTableItemProvider.GetItems(ffi.Settings["SelectorCustomTableName"].ToString(), where, null);
                            if (!DataHelper.DataSourceIsEmpty(customTableItems))
                            {
                                foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
                                {
                                    CustomTableItem modifyCustomTableItem = CustomTableItem.New(ffi.Settings["SelectorCustomTableName"].ToString(), customTableItemDr);
                                    modifyCustomTableItem.SetValue(ffi.Settings["RelationColumnName"].ToString(), scheduleID.ToString());
                                    modifyCustomTableItem.Update();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void saveData()
    {
        EventFormControl.CustomTableForm.SaveData(URLHelper.GetAbsoluteUrl(EventFormControl.EditItemPage) + "?customtableid=" + EventFormControl.CustomTableId + "&itemid=" + EventFormControl.CustomTableForm.ItemID);
    }

    private void ResetHiddenFields()
    {
        foreach (string fieldName in EventFormControl.CustomTableForm.Fields)
        {
            if (!EventFormControl.CustomTableForm.IsFieldVisible(fieldName))
            {
                EventFormControl.CustomTableForm.Data.SetValue(fieldName, string.Empty);
            }
        }
    }

    private void GetDynamicControlsForClashDates()
    {

        List<BlackOutDate> list = (List<BlackOutDate>)ViewState[CLASHEDDATES];
        Control Panel = getPopupControlBody(list);
        //EmergePopup.Body.Controls.Clear();
        EmergePopup.Body.Controls.Add(Panel);

    }

}