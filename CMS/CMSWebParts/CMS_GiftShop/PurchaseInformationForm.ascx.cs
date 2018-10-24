using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using CMS.Base.Web.UI;



public partial class CMSWebParts_CMS_GiftShop_PurchaseInformationForm : PurchaseInformationFormWebPart
{
    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    #region "Webpart Properties"

    public string ProductListingPageUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("ProductListingPageUrl"), string.Empty);
        }
        set
        {
            SetValue("ProductListingPageUrl", value);
        }
    }

    public string CartPageUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("CartPageUrl"), string.Empty);
        }
        set
        {
            SetValue("CartPageUrl", value);
        }
    }

    public string PaymentGatewayUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("PaymentGatewayUrl"), string.Empty);
        }
        set
        {
            SetValue("PaymentGatewayUrl", value);
        }
    }

    public string ThankYouPageUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("ThankYouPageUrl"), string.Empty);
        }
        set
        {
            SetValue("ThankYouPageUrl", value);
        }
    }

    #endregion "Webpart Properties"

    #region "Page Events"
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panelPurchaseInformationForm;
        Environment = Constants.Environments.Desktop;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panelPurchaseInformationForm.Visible = false;
            return;
        }
        if ((EmergeCMSContext.IsLiveMode() || EmergeCMSContext.IsPreviewMode()) && 0 == Cart.GetCartObject().GetProducts().Count)
            EmergeURLHelper.Redirect(ProductListingPageUrl);

        SetupEventHandlers();

        if (!EmergeRequestHelper.IsPostBack())
        {
            LoadListControls(true);

            if (null != EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY))
            {
                SetFormFieldsFromDictionary(((Dictionary<string, object>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY)));
            }
        }
    }
    #endregion "Page Events"

    #region "Private Method"
    private void SetupEventHandlers()
    {
        btnClear.Click += btnClear_Click;
        btnBackToCart.Click += btnBackToCart_Click;
        btnProceedToPayment.Click += btnProceedToPayment_Click;
        //chkBillingAddress.CheckedChanged += chkBillingAddress_CheckedChanged;
    }

    private void chkBillingAddress_CheckedChanged(object sender, EventArgs e)
    {
        //if (((CheckBox)sender).Checked)
        //{
        //    CopyHomeAddressToBillingAddress();
        //    ShowHideBillingAddress(false);
        //}
        //else
        //{
        //    ResetBillingAddress();
        //    ShowHideBillingAddress(true);
        //}



    }

   
    #endregion "Private Method"

    #region "Control Events"
    void btnBackToCart_Click(object sender, EventArgs e)
    {
        CreateAndSaveFormFieldsInSession();
        EmergeURLHelper.Redirect(CartPageUrl);
    }
    
    protected void btnProceedToPayment_Click(object sender, EventArgs e)
    {
        if (chkBillingAddress.Checked)
        {
            CopyHomeAddressToBillingAddress();
            ResetValidators(false);
        }

        if (Page.IsValid)
        {
            
            try
            {
                
                CreateAndSaveFormFieldsInSession();
                Cart.GetCartObject().EnsureCartProductsExists();
                Cart.GetCartObject().VerifyStockAvailabilityForProductsInCart();
                //URLHelper.ResponseRedirect(PaymentGatewayUrl);
                EmergeURLHelper.Redirect(ThankYouPageUrl);
            }
            catch (ProductNotFoundException)
            {
                EmergeURLHelper.Redirect(CartPageUrl);  
            }

            catch (ProductOutOfStockException)
            {
                EmergeURLHelper.Redirect(CartPageUrl);
            }
        }
    }
    #endregion "Control Events"

}