using System;
using Bluespire.Emerge.Web.Pages.Maintenance;

public partial class CMSModules_Maintenance_Tools_Maintenance_Data_ViewItem : MaintenanceDataViewItemPage
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
            OnError(ex, true);
        }
    }
}