using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.Career;
using CMS.CustomTables;
using CMS.SiteProvider;
using System;
public partial class CMSModules_Career_Tools_Career_Data_List : CareerDataListPage
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
            NewItemPage = "~/CMSModules/CMS_Career/Tools/Career_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_Career/Tools/Career_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_Career/Tools/Career_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_Career/Tools/Career_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_Career/Tools/Career_Data_ViewItem.aspx";
            customTableDataList.OnAfterAction += customTableDataList_OnAfterAction;
            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_Career/Tools/Career_Data_List.xml";
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
                customTableSearch.CustomTableClassInfo = DataClassInfo;
            }
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
    }

    private bool customTableDataList_OnAfterAction(string actionName, object actionArgument, Bluespire.Emerge.CommonService.GridActions.IGridAction actionObject)
    {
        if (actionName.Equals("delete") || actionName.Equals("deactivate"))
        {
            string queryName = string.Format(CareerConstants.CAREER_QUERY_GET_JOBS, EmergeCMSContext.CurrentSiteName);
            EmergeCacheHelper.TouchKey(queryName);
            actionObject.AfterAction();
        }
        return true;
    }   
}