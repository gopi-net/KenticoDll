using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.DataEngine;
using CMS.CustomTables;

public partial class CMSModules_EventsCalendar_Tools_EventsCalendar_Data_List : EventsCalendarDataListPage
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

            NewItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_SelectFields.aspx";

            
            customTableDataList.EditItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_ViewItem.aspx";

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
                customTableDataList.GridName = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_List.xml";
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
}