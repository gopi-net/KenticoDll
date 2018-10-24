using System;
using Bluespire.Emerge.Components.Rates;
using System.Web.UI;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;

public partial class CMSModules_Rates_Tools_Rates_Data_EditItem : RatesDataEditItemPage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            customTableForm.OnBeforeSave += customTableForm_OnBeforeSave;
            EmergePopup.OnCancelButtonClick += EmergePopup_OnCancelButtonClick;
            EmergePopup.OnOKButtonClick += EmergePopup_OnOKButtonClick;
            ListPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_List.aspx";
            NewItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                customTableForm.EditItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";
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
    private void customTableForm_OnBeforeSave(object sender, EventArgs e)
    {
        bool verified = Convert.ToBoolean(EmergeSessionHelper.GetValue("verified"));
        if (!verified)
        {
            LiteralControl control = new LiteralControl();
            control.Text = "<br /><br />Do you wish to save the record?";
            EmergePopup.Body.Controls.Add(control);
            EmergePopup.Show();
            customTableForm.CustomTableForm.StopProcessing = true;
        }
        EmergeSessionHelper.Remove("verified");

    }

    void EmergePopup_OnOKButtonClick(object sender, EventArgs e)
    {
        EmergeSessionHelper.SetValue("verified", true);
        customTableForm.CustomTableForm.SaveData(URLHelper.GetAbsoluteUrl(customTableForm.EditItemPage) + "?customtableid=" + customTableForm.CustomTableId + "&itemid=" + customTableForm.CustomTableForm.ItemID);
    }
    void EmergePopup_OnCancelButtonClick(object sender, EventArgs e)
    {
        customTableForm.CustomTableForm.StopProcessing = true;
    }

}