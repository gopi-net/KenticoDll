using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.License;

public partial class CMSModules_License_Tools_License_Data_EditItem : LicenseDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_License/Tools/License_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_License/Tools/License_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_License/Tools/License_Data_EditItem.aspx";
                customTableForm.CustomTableId = CustomTableID;
                customTableForm.ItemId = ItemID;
                customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
            }
            customTableForm.OnAfterSave += customTableForm_OnAfterSave;
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }

    }

    void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        Bluespire.Emerge.CommonService.License.LicenseProvider.ClearCachedLicenseModules();
    }
}