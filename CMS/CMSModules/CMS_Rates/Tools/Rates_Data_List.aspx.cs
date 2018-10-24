using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.Rates;
using Bluespire.Emerge.Components.GenericParsing;
using System.Data;
using System.IO;
using Bluespire.Emerge.CommonService;
using System.Collections.Generic;
using System.Linq;
using CMS.CustomTables;
public partial class CMSModules_Rates_Tools_Rates_Data_List : RatesDataListPage
{
    
    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_Rates/Tools/Rates_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_SelectFields.aspx";
            customTableSearch.UniGrid = customTableDataList.UniGrid;
            customTableDataList.EditItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_ViewItem.aspx";

            base.OnPageLoad();


            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_Rates/Tools/Rates_Data_List.xml";
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