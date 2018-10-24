using System;
using Bluespire.Emerge.Components.Career;

public partial class CMSModules_Career_Tools_Career_Data_ViewItem : CareerDataViewItemPage
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