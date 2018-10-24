using System;
using Bluespire.Emerge.Components.EventsCalendar.Pages;

public partial class CMSModules_EventsCalendar_Tools_EventsCalendar_Data_EditItem : EventsCalendarDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";
                customTableForm.CustomTableId = CustomTableID;
                customTableForm.ItemId = ItemID;
                customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }

    }
}