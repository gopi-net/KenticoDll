using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Web.Pages.HistoryTracker;

public partial class CMSModules_CMS_HistoryTracker_Tools_HistoryTracker_Data_EditItem : HistoryTrackerDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_List.aspx";//"~/CMSModules/CMS_Location/Tools/Location_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = NewItemPage;
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