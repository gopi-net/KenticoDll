using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.Career;
using CMS.DataEngine;
using CMS.UIControls;
using System;

public partial class CMSModules_Career_Tools_Career_Data_EditItem : CareerDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = "~/CMSModules/CMS_Career/Tools/Career_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_Career/Tools/Career_Data_EditItem.aspx";

            OnPageLoad();
            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }
            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_Career/Tools/Career_Data_EditItem.aspx";
                customTableForm.CustomTableId = CustomTableID;
                customTableForm.ItemId = ItemID;
                customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableForm.OnAfterSave += customTableForm_OnAfterSave;
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    private void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        string queryName = string.Format(CareerConstants.CAREER_QUERY_GET_JOBS, EmergeCMSContext.CurrentSiteName);
        EmergeCacheHelper.TouchKey(queryName);
    }
}