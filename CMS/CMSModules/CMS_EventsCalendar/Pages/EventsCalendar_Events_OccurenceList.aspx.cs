using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.CommonService.GridActions;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using System.Collections;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.Helpers;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Base;
using CMS.Base.Web.UI.ActionsConfig;
using CMS.Base.Web.UI;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_OccurenceList : EventsOccurenceDataListPage
{
   // private string ViewRegistrationsPage = EventsConstants.PAGEURL_LIST_EVENTSREGISTRATIONS; 
    protected void Page_Init(object sender, EventArgs e)
    {
        SearchControl.UniGrid = customTableDataList.UniGrid;
        RequireSite = false;
        
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;
            customTableDataList.OnBeforeAction += customTableDataList_OnBeforeAction;
            customTableDataList.OnExternalDataBound += customTableDataList_OnExternalDataBound;
            
            customTableDataList.EditItemPage = NewItemPage;
            customTableDataList.ViewItemPage = ViewItemPage;

            // Register Javascripts
            ScriptHelper.RegisterDialogScript(Page);
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ViewRegistrations", ScriptHelper.GetScript(
                "function ViewRegistrations(itemId, scheduleID, status, className) { " +
                "  document.location.replace('" + ResolveUrl(ViewRegistrationsPage) + "?OccurenceID=' + itemId + '&ScheduleID=' + scheduleID + '&RegistrationStatus=' + status + '&CustomTableName=' + className); } " 
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
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Pages/EventsOccurenceList.xml";
                customTableDataList.UniGrid.WhereCondition = getWhereCondition();
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
            OnError(ex, true);
        }
    }

    protected override void setHeaderActions()
    {
        

        HeaderActions.AddAction(new HeaderAction
        {
            Text = GetString("customtable.data.selectdisplayedfields"),
            OnClientClick = "SelectFields();",
        });
    }

    private string getWhereCondition()
    {
        string whereCondition = string.Empty;
        int scheduleID = QueryHelper.GetInteger("ScheduleID", 0);

        if (scheduleID > 0)
        {
            whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID + " = " + scheduleID.ToString();
        }
        return whereCondition;

    }

    object customTableDataList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string source = sourceName.ToLowerCSafe();
        // Get button and grid view row
      //  ImageButton button = sender as ImageButton;
        CMSGridActionButton button = sender as CMSGridActionButton;
        GridViewRow grv = parameter as GridViewRow;

        if (grv != null)
        {
            DataRowView drv = grv.DataItem as DataRowView;
            ObjectTypeInfo ti = CustomTableItemProvider.GetTypeInfo(customTableDataList.CustomTableClassInfo.ClassName);
            // Hide Move Up/Down buttons when there is no Order field
            int occurenceID = Convert.ToInt32(drv[ti.IDColumn]);
            EventOccurence occurence = EventsCalendarHelper.GetEventOccurenceByID(occurenceID);
            switch (source)
            {
                case "edit":
                    if ((button != null) && (drv != null))
                    {
                        if (occurence.IsSeries || occurence.OccurenceDate < DateTime.Now.Date)
                        {
                            button.Enabled = false;
                            button.ToolTip = ResHelper.GetString(EventsConstants.STRINGCODE_OCCURENCEEDITMESSAGE);
                        }
                    }
                    break;
                case "viewregistrations":
                    if ((button != null) && (drv != null))
                    {
                        if (occurence.IsSeries)
                        {
                            button.Enabled = false;
                            button.ToolTip = ResHelper.GetString(EventsConstants.STRINGCODE_VIEWREGISTRATIONMESSAGE);
                        }
                        button.OnClientClick = "ViewRegistrations(" + drv[ti.IDColumn] + ", " + QueryHelper.GetInteger("ScheduleID", 0) +", '" + EventRegistrationStatus.CONFIRMED.ToString() + "', '" + EventsConstants.CUSTOMTABLE_EVENT_EVENTREGISTRATIONS + "'); return false;";
                    }
                    break;

            }
        }

        return parameter;
    }

    private bool customTableDataList_OnBeforeAction(string actionName, object actionArgument, IGridAction actionObject)
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

    private bool customTableDataList_OnAfterAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        actionObject.AfterAction(null);
        return true;
    }
}