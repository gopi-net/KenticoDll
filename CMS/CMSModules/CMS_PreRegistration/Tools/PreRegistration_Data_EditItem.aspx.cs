using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.PreRegistration.Pages;
using Bluespire.Emerge.Components.PreRegistration;
public partial class PreRegistration_Data_EditItem : PreRegistrationDataEditItemPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = PreRegistrationConstants.PAGEURL_DATA_LIST;
            NewItemPage = PreRegistrationConstants.PAGEURL_NEW_ITEM;

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = PreRegistrationConstants.PAGEURL_NEW_ITEM;
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