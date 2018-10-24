using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.CommonService.GridActions;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using System.Collections;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_EventsList : EventsDataListPage
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
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;
            customTableDataList.OnBeforeAction += customTableDataList_OnBeforeAction;

            customTableDataList.EditItemPage = NewItemPage;
            customTableDataList.ViewItemPage = ViewItemPage;

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
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Pages/EventsList.xml";
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