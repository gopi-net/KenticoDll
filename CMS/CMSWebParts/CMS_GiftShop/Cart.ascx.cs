using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Base.Web.UI;


public partial class CMSWebParts_CMS_GiftShop_Cart : CartWebPart
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

  

    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            gsCartPanel.Visible = false;
            return;
        }
        if (CurrentGiftShopCart.GetProducts().Count == 0 && !string.IsNullOrEmpty(ProductListingPageUrl))
            EmergeURLHelper.Redirect(ProductListingPageUrl);
        SetupEventHandlers();
        MessagesPlaceHolder.ClearLabels();



        BindCartProducts();
        if (!EmergeRequestHelper.IsPostBack())
        {
            ValidateProductsAndSetMessage();
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = gsCartPanel;
        Environment = Constants.Environments.Desktop;
    }

    #endregion "Page Events"

    #region "Private Methods"
    private void SetupEventHandlers()
    {
        SetupCartRepeater();
        cmdChekOut.Click += cmdChekOut_Click;
        cmdContinue.Click += cmdContinue_Click;
    }
    #endregion "Private Methods"

    #region "Control Events"
    protected void cmdChekOut_Click(object sender, EventArgs e)
    {
        
        
        if (CurrentGiftShopCart.GetProducts().Count == 0)
            EmergeURLHelper.Redirect(ProductListingPageUrl);

        if (ValidateProductsAndSetMessage())
            EmergeURLHelper.Redirect(PurchaseInformationFormUrl);

    }

    protected void cmdContinue_Click(object sender, EventArgs e)
    {
        EmergeURLHelper.Redirect(ProductListingPageUrl);
    }
    #endregion "Control Events"

}