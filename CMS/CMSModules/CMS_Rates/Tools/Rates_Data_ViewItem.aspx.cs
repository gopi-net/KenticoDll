using System;
using Bluespire.Emerge.Components.Rates;

public partial class CMSModules_Rates_Tools_Rates_Data_ViewItem : RatesDataViewItemPage
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