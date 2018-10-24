using System;
using Bluespire.Emerge.Components.StaffDirectory.Pages;


public partial class CMSModules_StaffDirectory_Tools_StaffDirectory_Data_ViewItem : StaffDirectoryDataViewItemPage
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
            OnError(ex,false);
        }
    }
}