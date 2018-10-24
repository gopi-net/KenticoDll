using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory;
using Bluespire.Emerge.Components.StaffDirectory.Pages;
using System;


public partial class CMSModules_StaffDirectory_Tools_StaffDirectory_Data_List : StaffDirectoryDataListPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
        searchCustomTable.UniGrid = customTableDataList.UniGrid;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_ViewItem.aspx";
            customTableDataList.GridName = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_List.xml";
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;
            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";
                customTableDataList.UniGrid.FilterFormData = CustomTableDataHelper.New(DataClassInfo.ClassName);

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
                searchCustomTable.CustomTableClassInfo = DataClassInfo;
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    bool customTableDataList_OnAfterAction(string actionName, object actionArgument, Bluespire.Emerge.CommonService.GridActions.IGridAction actionObject)
    {
        if (actionName.Equals("delete") || actionName.Equals("deactivate"))
        {
            string queryName = string.Format(StaffDirectoryConstants.CUSTOMTABLE_QUERY_GET_STAFF, EmergeCMSContext.CurrentSiteName);
            EmergeCacheHelper.TouchKey(queryName);
            actionObject.AfterAction();
        }
        return true;
    }

    
}