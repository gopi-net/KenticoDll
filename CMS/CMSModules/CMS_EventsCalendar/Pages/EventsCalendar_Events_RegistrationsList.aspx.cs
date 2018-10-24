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
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_RegistrationsList : EventRegistrationDataListPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        SearchControl.UniGrid = customTableDataList.UniGrid;
        RequireSite = false;


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = EventsConstants.PAGEURL_NEW_EVENTSREGISTRATIONS; //"~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Registrations.aspx";
            ListPage = EventsConstants.PAGEURL_LIST_EVENTSCALENDAR; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_List.aspx";
            SelectFieldsPage = EventsConstants.PAGEURL_DATA_SELECTFIELDS; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_SelectFields.aspx";


            customTableDataList.EditItemPage = EventsConstants.PAGEURL_NEW_EVENTSREGISTRATIONS; //"~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Registrations.aspx";
            customTableDataList.ViewItemPage = EventsConstants.PAGEURL_DATA_VIEWITEM; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_ViewItem.aspx";

            customTableDataList.OnBeforeAction += customTableDataList_OnBeforeAction;
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;

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
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Pages/EventsRegistrationsList.xml";
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

    bool customTableDataList_OnAfterAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        actionObject.AfterAction(null);
        return true;
    }

    bool customTableDataList_OnBeforeAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        object[] parameters = new object[1];
        parameters[0] = Convert.ToInt32(actionArgument);

        object returnObject = actionObject.BeforeAction(parameters);

        bool returnValue = (bool)returnObject;
        return returnValue;
    }

    private string getWhereCondition()
    {
        string whereCondition = string.Empty;
        int scheduleID = QueryHelper.GetInteger("ScheduleID", 0);
        int occurenceID = QueryHelper.GetInteger("OccurenceID", 0);

        string statusCondition = getStatusConditon();

        
        if (occurenceID > 0)
        {
            whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID + " = " + occurenceID.ToString() + statusCondition;
        }
        else if (scheduleID > 0)
        {
            whereCondition = EventsConstants.FIELDS_EVENTREGISTRATIONS_SCHEDULEID + " = " + scheduleID.ToString() + statusCondition;
        }
        if (String.IsNullOrEmpty(whereCondition))
            whereCondition = statusCondition.Replace(" AND ", "");
        return whereCondition;

    }

    private string getStatusConditon()
    {
        string statusCondition = string.Empty;
        string status = QueryHelper.GetString("RegistrationStatus", string.Empty);
        EventRegistrationStatus registrationStatus = EventRegistrationStatus.NONE;
        if (status == string.Empty)
            registrationStatus = EventRegistrationStatus.NONE;
        else
            registrationStatus = (EventRegistrationStatus)Enum.Parse(typeof(EventRegistrationStatus), status);
        switch (registrationStatus)
        {
            case EventRegistrationStatus.CONFIRMED:
                statusCondition = " AND " + EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS + " = '" + EventRegistrationStatus.CONFIRMED.ToString() + "'"; 
                break;
            case EventRegistrationStatus.CANCELLED:
                statusCondition = " AND " + EventsConstants.FIELDS_EVENTREGISTRATIONS_STATUS + " = '" + EventRegistrationStatus.CANCELLED.ToString() + "'";
                break;
        }
        return statusCondition;
    }
}