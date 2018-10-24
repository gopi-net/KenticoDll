using System;
using Bluespire.Emerge.Components.CheerCard;

public partial class CMSModules_CheerCard_Tools_CheerCard_Data_ViewItem : CheerCardDataViewItemPage
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