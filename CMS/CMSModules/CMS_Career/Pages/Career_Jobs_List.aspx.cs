using System;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using System.Data;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.DataEngine;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Base;
using CMS.Base.Web.UI;

public partial class CMSModules_CMS_Career_PagesCareer_Jobs_List : CareerDataListPage
{
    string ViewApplicationsPage = "~/CMSModules/CMS_Career/Pages/Career_Jobs_ApplicationsList.aspx";
    protected void Page_Init(object sender, EventArgs e)
    {
        SearchControl.UniGrid = customTableDataList.UniGrid;
        RequireSite = false;
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

            customTableDataList.OnExternalDataBound +=customTableDataList_OnExternalDataBound;

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
                customTableDataList.GridName = "~/CMSModules/CMS_Career/Pages/JobList.xml";
                customTableDataList.UniGrid.WhereCondition = String.Empty;
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
            ScriptHelper.RegisterDialogScript(Page);
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ViewApplications", ScriptHelper.GetScript(
                "function ViewApplications(itemId, status, className) { " +
                "  document.location.replace('" + ResolveUrl(ViewApplicationsPage) + "?JobId=' + itemId + '&RegistrationStatus=' + status + '&CustomTableName=' + className); } "
                ));
            base.OnPageLoad();

        }
        catch (Exception ex)
        {
            OnError(ex, true);
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
            ObjectTypeInfo ti = CustomTableItemProvider.GetTypeInfo(customTableDataList.CustomTableClassInfo.ClassName);
            // Hide Move Up/Down buttons when there is no Order field
            switch (source)
            {
                case "viewapplications":
                    if ((button != null) && (drv != null))
                    {
                        button.OnClientClick = "ViewApplications(" + drv[ti.IDColumn] + ", '1', '" + CareerConstants.CAREER_TABLE_JOB_APPLICATION + "'); return false;";
                    }
                    break;
            }
        }

        return parameter;
    }
}