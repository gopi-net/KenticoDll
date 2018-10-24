using System;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.Pages;

public partial class CMSModules_GiftShop_Tools_GiftShop_Data_EditItem : GiftShopDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage =  GiftShopConstants.PAGEURL_DATA_LIST ;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_List.aspx";
            NewItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM;// "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM;// "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_EditItem.aspx";
                customTableForm.CustomTableId = CustomTableID;
                customTableForm.ItemId = ItemID;
                customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
            }
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }

    }
}