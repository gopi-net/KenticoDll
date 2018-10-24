using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.GiftShop.Pages;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.CommonService;

public partial class CMSModules_GiftShop_Tools_GiftShop_Data_List : GiftShopDataListPage
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
            NewItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM;
            ListPage = GiftShopConstants.PAGEURL_LIST_GIFTSHOP;
            SelectFieldsPage = GiftShopConstants.PAGEURL_DATA_SELECTFIELDS;
            customTableDataList.EditItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM;
            customTableDataList.ViewItemPage = GiftShopConstants.PAGEURL_DATA_VIEWITEM;

            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_List.xml";
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
            OnError(ex, true);
        }
    }
}