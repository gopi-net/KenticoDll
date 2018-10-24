using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Web.Pages.HistoryTracker;
using CMS.CustomTables;

public partial class CMSModules_CMS_HistoryTracker_Tools_HistoryTracker_Data_List : HistoryTrackerDataListPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
        customTableSearch.UniGrid = customTableDataList.UniGrid;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_ViewItem.aspx";
           
            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_List.xml";
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";
                customTableDataList.UniGrid.FilterFormData = CustomTableItem.New(DataClassInfo.ClassName);
                customTableDataList.ShowObjectMenu = false;
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
                
            }
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
    }

    

    
}