using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Components.GiftShop.Helpers;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_GiftShop_ProductDetails : ProductDetailsWebPart
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

    # region "Webpart Properties"
    public string CartPageURL
    {
        get
        {

            return EmergeValidationHelper.GetString(GetValue("CartPageURL"), string.Empty);
        }
        set
        {
            SetValue("CartPageURL", value);

        }
    }
    public string BackToProductResultPage
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("BackToProductResultPage"), string.Empty);
        }
        set
        {
            SetValue("BackToProductResultPage", value);
        }
    }
    #endregion

    #region "page events"
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panelProductDetails;
        Environment = Constants.Environments.Desktop;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panelProductDetails.Visible = false;
            return;
        }
            
        SetupEventHandlers();
        
        if (0 == ProductID || null == EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY))
            EmergeURLHelper.Redirect(BackToProductResultPage);

        if (!EmergeRequestHelper.IsPostBack())
        {

            try
            {
                SetupProductDetail();
            }
            catch (SessionDataMissingException)
            {
                EmergeURLHelper.Redirect(BackToProductResultPage);
            }
            catch (ProductNotFoundException)
            {
                EmergeURLHelper.Redirect(BackToProductResultPage);
            }
        }
        EnableDisabledFields();
    }

    protected void SetupEventHandlers()
    {
        btnAddToCart.Click += btnAddToCart_Click;
        btnContinueShopping.Click += btnContinueShopping_Click;
        btnProductInfo.Click += btnProductInfo_Click;
    }

    
    #endregion 

    #region "Control Events"
    void btnProductInfo_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ControlPanel = panSendProductInformation;
            CreateFormParameters();
            ProductManager productManager = new ProductManager();
            FormParameters = productManager.GetCombinedPropertiesForProductAndSender(SelectedProduct, FormParameters);
            productManager.SaveProductInformationRequest(FormParameters);

            ShowConfirmation(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_PRODUCTINFORMATIONREQUEST_MESSAGE));
            SendNotificationEmail();

            ClearFormFields();
        }

    }

    void btnAddToCart_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (SelectedProduct.IsQuantityInStock(Convert.ToInt32(PurchasedQty.Text)))
                {
                    AddProductToCart();
                    EmergeURLHelper.Redirect(CartPageURL);
                }
                else
                {
                    if (SelectedProduct.GetStock()<=0)
                        ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_MESSAGEFOR_ZERO_OR_NEGATIVESTOCKPRODUCT));
                    else
                        ShowError(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_OUTOFSTOCK_MESSAGE, PurchasedQty.Text.Trim(), SelectedProduct.GetStock()));
                }
            }
            catch (ZeroPurchasedQuantityException)
            {
                ShowError(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_ZEROPURCHASEDQUANTITY_EXCEPTION, PurchasedQty.Text.Trim(), SelectedProduct.GetStock()));
            }
        }
    }

    void btnContinueShopping_Click(object sender, EventArgs e)
    {
        EmergeURLHelper.Redirect(BackToProductResultPage);
    }
    #endregion 
}