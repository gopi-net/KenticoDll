using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Components.GiftShop;

using Bluespire.Emerge.Components.GiftShop.Helpers;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using Bluespire.Emerge.CommonService;
using System.Text;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.GiftShop.BL;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_GiftShop_ConfirmationMessage : ConfirmationMessageWebPart
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


    #region "Webpart Propeties"
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

    public string TransformationNameForCartRepeater
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("TransformationNameForCartRepeater"), cartRepeater.TransformationName);
        }
        set
        {
            SetValue("TransformationNameForCartRepeater", value);
            cartRepeater.TransformationName = value;
        }
    }

    public string TransformationNameUsedForNegativeProductListUsedInEmail
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("TransformationNameUsedForNegativeProductListUsedInEmail"), productsWithNegativeStockRepeater.TransformationName);
        }
        set
        {
            SetValue("TransformationNameUsedForNegativeProductListUsedInEmail", value);
            productsWithNegativeStockRepeater.TransformationName = value;
        }
    }
    
    public string TransformationNameForCartRepeaterUsedInEmail
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("TransformationNameForCartRepeaterUsedInEmail"), emailCartRepeater.TransformationName);
        }
        set
        {
            SetValue("TransformationNameForCartRepeaterUsedInEmail", value);
            emailCartRepeater.TransformationName = value;
        }
    }

    public string CartHeaderTemplateText
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("CartHeaderTemplateText"), emailCartRepeater.TransformationName);
        }
        set
        {
            SetValue("CartHeaderTemplateText", value);
           
        }
    }

    #endregion "Webpart Propeties"

    #region "Page Events"
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panelConfirmationMessage;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panelConfirmationMessage.Visible = false;
            return;
        }
        if (EmergeCMSContext.IsLiveMode() || EmergeCMSContext.IsPreviewMode())
        {
            if (EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY) == null)
                EmergeURLHelper.Redirect(ProductListingPageUrl);


            bool isTransactionSucceed = true;
            if (isTransactionSucceed)
            {
                SetCartRepeaters();
                SetNegativeStockRepeater();
                ProcessOrder();
            }
            else
            {
                DisplayConfirmationMessage(GiftShopConstants.TransactionStatus.Fail);
            }
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Cart.GetCartObject().Destroy();
    }
    #endregion "Page Events"

    #region "Private Methods"
    private void SetNegativeStockRepeater()
    {
        productsWithNegativeStockRepeater.TransformationName = TransformationNameUsedForNegativeProductListUsedInEmail;
    }

    private void SetCartRepeaters()
    {
        cartRepeater.TransformationName = TransformationNameForCartRepeater;
        emailCartRepeater.TransformationName = TransformationNameForCartRepeaterUsedInEmail;

        BindCartProducts();

        ((LocalizedLiteral)cartRepeater.Controls[0].FindControl("CartHeaderLiteral")).Text = CartHeaderTemplateText;

    }
    #endregion "Private Methods"
}