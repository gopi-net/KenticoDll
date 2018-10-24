using System;
using Bluespire.Emerge.Components.GiftShop.Pages;
      
public partial class CMSModules_GiftShop_Tools_GiftShop_Data_ViewItem : GiftShopDataViewItemPage
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