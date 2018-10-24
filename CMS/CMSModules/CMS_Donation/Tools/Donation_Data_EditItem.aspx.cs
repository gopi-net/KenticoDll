using System;
using Bluespire.Emerge.Components.Donation.Pages;
using Bluespire.Emerge.Components.Donation;

public partial class CMSModules_Donation_Tools_Donation_Data_EditItem : DonationDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = DonationConstants.PAGEURL_DATA_LIST;
            NewItemPage = DonationConstants.PAGEURL_NEW_ITEM;

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = DonationConstants.PAGEURL_NEW_ITEM;
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