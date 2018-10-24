using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.StaffDirectory;
using Bluespire.Emerge.Components.StaffDirectory.Pages;
using System;

public partial class CMSModules_StaffDirectory_Tools_StaffDirectory_Data_EditItem : StaffDirectoryDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_StaffDirectory/Tools/StaffDirectory_Data_EditItem.aspx";
                customTableForm.CustomTableId = CustomTableID;
                customTableForm.ItemId = ItemID;
                customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableForm.OnAfterSave += customTableForm_OnAfterSave;
            }
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }

    }

   protected void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        string queryName = string.Format(StaffDirectoryConstants.CUSTOMTABLE_QUERY_GET_STAFF, EmergeCMSContext.CurrentSiteName);
        EmergeCacheHelper.TouchKey(queryName);
    }
}