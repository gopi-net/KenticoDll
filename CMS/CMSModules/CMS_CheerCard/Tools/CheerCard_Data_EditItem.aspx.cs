using System;
using Bluespire.Emerge.Components.CheerCard;

public partial class CMSModules_CheerCard_Tools_CheerCard_Data_EditItem : CheerCardDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = CheerCardConstants.PAGEURL_DATA_LIST; //"~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_List.aspx";
            NewItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM;// "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM; //"~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_EditItem.aspx";
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