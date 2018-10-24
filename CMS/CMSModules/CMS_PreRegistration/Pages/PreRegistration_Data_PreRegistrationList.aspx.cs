using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.PreRegistration.Pages;
using Bluespire.Emerge.Components.PreRegistration;
using CMS.CustomTables;

public partial class PreRegistration_Data_PreRegistrationList : PreRegistrationDataListPage
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
            NewItemPage = PreRegistrationConstants.PAGEURL_NEW_ITEM;
            ListPage = PreRegistrationConstants.PAGEURL_LIST;
            SelectFieldsPage = PreRegistrationConstants.PAGEURL_DATA_SELECTFIELDS;

            customTableDataList.EditItemPage = PreRegistrationConstants.PAGEURL_NEW_ITEM;
            customTableDataList.ViewItemPage = PreRegistrationConstants.PAGEURL_DATA_VIEWITEM;

            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = PreRegistrationConstants.PATH_PREREGDATALIST_XML;
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
            OnError(ex, true);
        }
    }   
}