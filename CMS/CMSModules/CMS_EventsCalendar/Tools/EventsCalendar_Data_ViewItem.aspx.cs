using System;
using Bluespire.Emerge.Components.EventsCalendar.Pages;

public partial class CMSModules_EventsCalendar_Tools_EventsCalendar_Data_ViewItem : EventsCalendarDataViewItemPage
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