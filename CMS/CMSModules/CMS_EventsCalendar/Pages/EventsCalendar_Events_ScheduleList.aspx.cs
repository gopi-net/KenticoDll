using System;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.GridActions;
using Bluespire.Emerge.CommonService.GridActions;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.Helpers;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Base;
using CMS.Base.Web.UI;
public partial class CMSModules_EventsCalendar_Pages_EventsCalendar_Events_EventsScheduleList : EventsCalendarDataListPage
{
    private string ViewRegistrationsPage = EventsConstants.PAGEURL_LIST_EVENTSREGISTRATIONS; 
    private string ViewOccurencesPage = EventsConstants.PAGEURL_LIST_EVENTSOCCURENCE; 
    protected void Page_Init(object sender, EventArgs e)
    {
        SearchControl.UniGrid = customTableDataList.UniGrid;
        RequireSite = false;
        if (!EmergeStaticHelper.GridActions.ContainsKey("deleteschedule"))
            EmergeStaticHelper.GridActions.Add("deleteschedule", () => new GridScheduleDeleteAction());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;
            customTableDataList.OnBeforeAction += customTableDataList_OnBeforeAction;
            customTableDataList.OnExternalDataBound += customTableDataList_OnExternalDataBound;
            NewItemPage = EventsConstants.PAGEURL_NEW_SCHEDULE; 
            ListPage = EventsConstants.PAGEURL_LIST_EVENTSCALENDAR;
            SelectFieldsPage = EventsConstants.PAGEURL_DATA_SELECTFIELDS; 


            customTableDataList.EditItemPage = EventsConstants.PAGEURL_NEW_SCHEDULE; 
            customTableDataList.ViewItemPage = EventsConstants.PAGEURL_DATA_VIEWITEM;

            // Register Javascripts
            ScriptHelper.RegisterDialogScript(Page);
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ViewRegistrations", ScriptHelper.GetScript(
                "function ViewRegistrations(itemId, status, className) { " +
                "  document.location.replace('" + ResolveUrl(ViewRegistrationsPage) + "?ScheduleID=' + itemId + '&RegistrationStatus=' + status + '&CustomTableName=' + className); } " +
                "function ViewOccurences(itemId, className) { " +
                "  document.location.replace('" + ResolveUrl(ViewOccurencesPage) + "?ScheduleID=' + itemId + '&CustomTableName=' + className); } "
                ));


            // Read data only if user is site manager global admin or table is bound to current site
            if (CurrentUser.IsGlobalAdministrator || (ClassSiteInfoProvider.GetClassSiteInfo(CustomTableID, EmergeCMSContext.CurrentSiteID) != null))
            {
                // Get CustomTable class
                DataClassInfo = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            }

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";
                customTableDataList.UniGrid.FilterFormData = CustomTableItem.New(DataClassInfo.ClassName);
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Pages/EventsScheduleList.xml";
                // Set custom pages
                if (DataClassInfo.ClassEditingPageURL != String.Empty)
                {
                    customTableDataList.EditItemPage = DataClassInfo.ClassEditingPageURL;
                }
                if (DataClassInfo.ClassNewPageURL != String.Empty)
                {
                    NewItemPage = DataClassInfo.ClassNewPageURL;
                }
                if (DataClassInfo.ClassViewPageUrl != String.Empty)
                {
                    customTableDataList.ViewItemPage = DataClassInfo.ClassViewPageUrl;
                }
                if (CheckForPermissions())
                {
                    plcContent.Visible = false;
                }
                SearchControl.CustomTableClassInfo = DataClassInfo;
            }

            base.OnPageLoad();
        }
        catch (Exception ex)
        {
            OnError(ex, false);
        }
    }

    object customTableDataList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string source = sourceName.ToLowerCSafe();
        // Get button and grid view row
        CMSGridActionButton button = sender as CMSGridActionButton;
        GridViewRow grv = parameter as GridViewRow;

        if (grv != null)
        {
            DataRowView drv = grv.DataItem as DataRowView;
            ObjectTypeInfo ti = CustomTableItemProvider.GetTypeInfo(customTableDataList.CustomTableClassInfo.ClassName);
            // Hide Move Up/Down buttons when there is no Order field
           
            switch (source)
            {
                case "edit":
                    if ((button != null) && (drv != null))
                    {
                        int scheduleID = Convert.ToInt32(drv[ti.IDColumn]);
                        bool isExpired = EventsCalendarHelper.IsScheduleExpired(scheduleID);
                        if (isExpired)
                        {
                            button.Enabled = false;
                            button.ToolTip = ResHelper.GetString(EventsConstants.STRINGCODE_SCHEDULEEDITMESSAGE);
                        }
                    }
                    break;
                case "viewregistrations":
                    if ((button != null) && (drv != null))
                    {
                        button.OnClientClick = "ViewRegistrations(" + drv[ti.IDColumn] + ", '" + EventRegistrationStatus.CONFIRMED.ToString() + "', '" + EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS + "'); return false;";
                    }
                    break;
                case "viewoccurences":
                    if ((button != null) && (drv != null))
                    {
                        button.OnClientClick = "ViewOccurences(" + drv[ti.IDColumn] + ", '" + EventsConstants.CUSTOMTABLE_EVENT_OCCURENCES + "'); return false;";
                    }
                    break;

            }
        }

        return parameter;
    }

    bool customTableDataList_OnBeforeAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        object[] parameters = new object[1];
        parameters[0] = Convert.ToInt32(actionArgument);
        
        object returnObject = actionObject.BeforeAction(parameters);
        
        ArrayList returnList = (ArrayList)returnObject;
        bool showPopup = (bool)returnList[0];
        if (showPopup)
        {
            LiteralControl control = new LiteralControl();
            string message = (string)returnList[1];
            control.Text = message;
            MessagePopup.Body.Controls.Add(control);
            MessagePopup.Show();
            return false;
        }
        return true;
    }

    bool customTableDataList_OnAfterAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        actionObject.AfterAction(null);
        return true;
    }
}