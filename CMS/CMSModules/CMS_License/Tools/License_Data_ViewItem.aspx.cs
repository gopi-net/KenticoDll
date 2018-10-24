using System;
using Bluespire.Emerge.Web.Pages.License;

public partial class CMSModules_License_Tools_License_Data_ViewItem : LicenseDataViewItemPage
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