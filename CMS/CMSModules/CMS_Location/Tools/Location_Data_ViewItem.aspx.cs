using System;
using Bluespire.Emerge.Components.Location.Pages;

public partial class CMSModules_Location_Tools_Location_Data_ViewItem : LocationDataViewItemPage
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