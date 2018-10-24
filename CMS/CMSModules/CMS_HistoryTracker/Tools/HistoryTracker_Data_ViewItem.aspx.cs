using System;
using Bluespire.Emerge.Web.Pages.HistoryTracker;

public partial class CMSModules_CMS_HistoryTracker_Tools_HistoryTracker_Data_ViewItem : HistoryTrackerDataViewItemPage
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