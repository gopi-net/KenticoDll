using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.Location;
using Bluespire.Emerge.Components.Location.Pages;

public partial class CMSModules_CMS_Location_Tools_Location_Data_EditItem : LocationDataEditItemPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = LocationConstants.PAGEURL_DATA_LIST;
            NewItemPage = LocationConstants.PAGEURL_DATA_EDITITEM;

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