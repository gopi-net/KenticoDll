using System;
using Bluespire.Emerge.Web.Pages.Maintenance;

public partial class CMSModules_Maintenance_Tools_Maintenance_Data_EditItem : MaintenanceDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_Maintenance/Tools/Maintenance_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_Maintenance/Tools/Maintenance_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_Maintenance/Tools/Maintenance_Data_EditItem.aspx";
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