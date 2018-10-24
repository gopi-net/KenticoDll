using System;
using CMS.SiteProvider;
using System.Data;
using Bluespire.Emerge.Components.Career;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Base.Web.UI.ActionsConfig;

public partial class CMSModules_CMS_Career_PagesCareer_Jobs_ApplicationsList : CareerDataListPage
{
    int jobId;
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
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_Career/Pages/Career_Data_View_JobApplication_Item.aspx";

            customTableDataList.OnAfterRetrieveData += customTableDataList_OnAfterRetrieveData;

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
                customTableDataList.GridName = "~/CMSModules/CMS_Career/Pages/JobApplicationsList.xml";
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
        //HeaderActions.AddAction(new HeaderAction
        //{
        //    Text = GetString("Emerge.CreateNewItem"),
        //    RedirectUrl = ResolveUrl(NewItemPage + "?new=1&customtableid=" + CustomTableID + (IsSiteManager ? "&sm=1" : "")),
        //});


        HeaderActions.AddAction(new HeaderAction
        {
            Text = GetString("customtable.data.selectdisplayedfields"),
            OnClientClick = "SelectFields();",
        });
    }
    DataSet customTableDataList_OnAfterRetrieveData(DataSet ds)
    {
        string queryName;
        QueryDataParameters parameters;
        queryName = string.Format(CareerConstants.CAREER_QUERY_GET_PROFILE, EmergeCMSContext.CurrentSiteName);
        parameters = new QueryDataParameters();
        parameters.Add(CareerConstants.CAREER_COLUMN_JOBID, jobId);
        return ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
    }
    private string getWhereCondition()
    {
        string whereCondition = string.Empty;
        jobId = QueryHelper.GetInteger(CareerConstants.CAREER_COLUMN_JOBID, 0);
        if (jobId > 0)
            whereCondition = CareerConstants.CAREER_COLUMN_JOBID + " = " + jobId.ToString();
        return whereCondition;
    }
}