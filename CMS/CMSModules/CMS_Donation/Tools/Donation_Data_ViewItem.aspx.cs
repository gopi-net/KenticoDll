using System;
using Bluespire.Emerge.Components.Donation.Pages;

public partial class CMSModules_Donation_Tools_Donation_Data_ViewItem : DonationDataViewItemPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();
            customTableViewItem.CustomTableItem = GetCustomTableItem();
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
    }
}