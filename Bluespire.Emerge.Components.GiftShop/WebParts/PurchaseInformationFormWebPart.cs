using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Base.Web.UI;

namespace Bluespire.Emerge.Components.GiftShop.WebParts
{
    public class PurchaseInformationFormWebPart : GiftShopWebPart
    {
        const string ID_FOR_PURCHASEINFORMATIONBILLINGADDRESS_CHECKBOX = "chkBillingAddress";
        const string ID_FOR_SENDERHOMEADDRESS_STREET_TEXTBOX = "SenderHomeAddressStreet";
        const string ID_FOR_BILLINGADDRESS_STREET_TEXTBOX = "BillingAddressStreet";
        const string ID_FOR_SENDERHOMEADDRESSCITY_TEXTBOX = "SenderHomeAddressCity";
        const string ID_FOR_BILLINGADDRESSCITY_TEXTBOX = "BillingAddressCity";
        const string ID_FOR_SENDERHOMEADDRESS_STATE_DROPDOWN = "SenderHomeAddressStateID";
        const string ID_FOR_BILLINGADDRESS_STATE_DROPDOWN = "BillingAddressStateID";
        const string ID_FOR_SENDERHOMEADDRESSZIPCODE_TEXTBOX = "SenderHomeAddressZipCode";
        const string ID_FOR_BILLINGADDRESSZIPCODE_TEXTBOX = "BillingAddressZipCode";


        const string ID_FOR_REQUIREDBILLINGADDRESSSTREET = "RequiredBillingAddressStreet";
        const string ID_FOR_REQUIREDBILLINGADDRESSSTREETIE = "RequiredBillingAddressStreetIE";
        const string ID_FOR_REGULARBILLINGADDRESSSTREET = "RegularBillingAddressStreet";
        const string ID_FOR_REQUIREDBILLINGADDRESSCITYIE = "RequiredBillingAddressCityIE";
        const string ID_FOR_REQUIREDBILLINGADDRESSCITY = "RequiredBillingAddressCity";
        const string ID_FOR_REQUIREDBILLINGADDRESSSTATEID = "RequiredBillingAddressStateID";
        const string ID_FOR_REGULARBILLINGADDRESSZIPCODE = "RegularBillingAddressZipCode";
        const string ID_FOR_REQUIREDBILLINGADDRESSZIPCODE = "RequiredBillingAddressZipCode";
        const string ID_FOR_REQUIREDBILLINGADDRESSZIPCODEIE = "RequiredBillingAddressZipCodeIE";

        LocalizedCheckBox chkBillingAddress
        { get { return (LocalizedCheckBox)ControlPanel.FindControl(ID_FOR_PURCHASEINFORMATIONBILLINGADDRESS_CHECKBOX); } }

        protected void ResetValidators(bool status)
        {
            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTREET))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTREET)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTREETIE))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTREETIE)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REGULARBILLINGADDRESSSTREET))
                ((RegularExpressionValidator)ControlPanel.FindControl(ID_FOR_REGULARBILLINGADDRESSSTREET)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSCITYIE))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSCITYIE)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSCITY))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSCITY)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTATEID))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSSTATEID)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REGULARBILLINGADDRESSZIPCODE))
                ((RegularExpressionValidator)ControlPanel.FindControl(ID_FOR_REGULARBILLINGADDRESSZIPCODE)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSZIPCODE))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSZIPCODE)).Enabled = status;

            if (null != ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSZIPCODEIE))
                ((RequiredFieldValidator)ControlPanel.FindControl(ID_FOR_REQUIREDBILLINGADDRESSZIPCODEIE)).Enabled = status;
        }


        protected void CopyHomeAddressToBillingAddress()
        {
            if (null != ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESS_STREET_TEXTBOX) && null != ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STREET_TEXTBOX))
                ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STREET_TEXTBOX)).Text = ((TextBox)ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESS_STREET_TEXTBOX)).Text.Trim();
            if (null != ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESSCITY_TEXTBOX) && null != ControlPanel.FindControl(ID_FOR_BILLINGADDRESSCITY_TEXTBOX))
                ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESSCITY_TEXTBOX)).Text = ((TextBox)ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESSCITY_TEXTBOX)).Text.Trim();
            if (null != ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESS_STATE_DROPDOWN) && null != ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STATE_DROPDOWN))
            {
                ((DropDownList)ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STATE_DROPDOWN)).SelectedIndex = -1;
                if (((DropDownList)ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESS_STATE_DROPDOWN)).SelectedIndex > 0)
                    ((DropDownList)ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STATE_DROPDOWN)).Items.FindByValue(((DropDownList)ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESS_STATE_DROPDOWN)).SelectedValue).Selected = true;
            }

            if (null != ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESSZIPCODE_TEXTBOX) && null != ControlPanel.FindControl(ID_FOR_BILLINGADDRESSZIPCODE_TEXTBOX))
                ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESSZIPCODE_TEXTBOX)).Text = ((TextBox)ControlPanel.FindControl(ID_FOR_SENDERHOMEADDRESSZIPCODE_TEXTBOX)).Text.Trim();
        }

        protected void ResetBillingAddress()
        {
            ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STREET_TEXTBOX)).Text = string.Empty;
            ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESSCITY_TEXTBOX)).Text = string.Empty;
            ((DropDownList)ControlPanel.FindControl(ID_FOR_BILLINGADDRESS_STATE_DROPDOWN)).SelectedIndex = -1;
            ((TextBox)ControlPanel.FindControl(ID_FOR_BILLINGADDRESSZIPCODE_TEXTBOX)).Text = string.Empty;
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            EmergeSessionHelper.Remove(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY);
        }

        protected void CreateAndSaveFormFieldsInSession()
        {
            CreateFormParameters();
            EmergeSessionHelper.SetValue(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY, FormParameters);
        }

    }
}
