using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Web.Pages.License;
using CMS.CustomTables;

public partial class CMSModules_License_Tools_License_Data_List : LicenseDataListPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = "~/CMSModules/CMS_License/Tools/License_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_License/Tools/License_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_License/Tools/License_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_License/Tools/License_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_License/Tools/License_Data_ViewItem.aspx";
           
            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_License/Tools/License_Data_List.xml";
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