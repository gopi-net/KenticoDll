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
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.DataEngine;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Base;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_VolunteerList : EventsCalendarDataListPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        SearchControl.UniGrid = customTableDataList.UniGrid;
        RequireSite = false;
        if (!EmergeStaticHelper.GridActions.ContainsKey("deactivateVolunteerUser"))
            EmergeStaticHelper.GridActions.Add("deactivateVolunteerUser", () => new GridDeactivateVolunteerUserAction());
        if (!EmergeStaticHelper.GridActions.ContainsKey("activateVolunteerUser"))
            EmergeStaticHelper.GridActions.Add("activateVolunteerUser", () => new GridActivateVolunteerUserAction());
        if (!EmergeStaticHelper.GridActions.ContainsKey("deletevolunteeruser"))
            EmergeStaticHelper.GridActions.Add("deletevolunteeruser", () => new GridVolunteerUserDeleteAction());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = EventsConstants.PAGEURL_NEW_VOLUNTEERUSERS;
            ListPage = EventsConstants.PAGEURL_LIST_EVENTSCALENDAR; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_List.aspx";
            SelectFieldsPage = EventsConstants.PAGEURL_DATA_SELECTFIELDS; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_SelectFields.aspx";

            customTableDataList.OnBeforeAction += customTableDataList_OnBeforeAction;
            customTableDataList.OnExternalDataBound += customTableDataList_OnExternalDataBound;
            customTableDataList.EditItemPage = EventsConstants.PAGEURL_NEW_VOLUNTEERUSERS; 
            customTableDataList.ViewItemPage = EventsConstants.PAGEURL_DATA_VIEWITEM; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_ViewItem.aspx";

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
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Pages/VolunteerUsersList.xml";
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
        ImageButton button = sender as ImageButton;
        GridViewRow grv = parameter as GridViewRow;

        if (grv != null)
        {
            DataRowView drv = grv.DataItem as DataRowView;

            // Hide Move Up/Down buttons when there is no Order field
            switch (source)
            {

                case "deactivatevolunteeruser":
                    if ((button != null) && (drv != null))
                    {
                        if (drv.Row.Table.Columns.Contains(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME))
                            button.Visible = ValidationHelper.GetBoolean(drv.Row[Constants.CUSTOM_TABLE_STATUS_COLUMNNAME], true);
                        else
                            button.Visible = false;

                    }
                    break;
                case "activatevolunteeruser":
                    if ((button != null) && (drv != null))
                    {
                        if (drv.Row.Table.Columns.Contains(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME))
                        {
                            bool isActiveRow = ValidationHelper.GetBoolean(drv.Row[Constants.CUSTOM_TABLE_STATUS_COLUMNNAME], true);
                            button.Visible = !isActiveRow;
                        }
                        else
                            button.Visible = false;
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
        
        return ValidationHelper.GetBoolean(returnObject, false);
    }
}