using System;

using CMS.SiteProvider;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.CommonService;

public partial class CMSModules_CheerCard_Tools_CheerCard_Data_List : CheerCardDataListPage
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
            NewItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM; //"~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_EditItem.aspx";
            ListPage = CheerCardConstants.PAGEURL_LIST_CHEERCARD;//"~/CMSModules/CMS_CheerCard/Tools/CheerCard_List.aspx";
            SelectFieldsPage = CheerCardConstants.PAGEURL_DATA_SELECTFIELDS;// "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM;// "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = CheerCardConstants.PAGEURL_DATA_VIEWITEM; //"~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_ViewItem.aspx";
           
            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_List.xml";
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";
                customTableDataList.UniGrid.FilterFormData = CustomTableDataHelper.New(DataClassInfo.ClassName); 
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

    

    
}