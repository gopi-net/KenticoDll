using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_GiftShop_ProductListing : ProductListingWebPart
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


    #region Webpart properties"

    public string CategoryDropDownDefaultText
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("CategoryDropDownDefaultText"), "");
        }
        set
        {
            SetValue("CategoryDropDownDefaultText", value);
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


    public string CheckOutPageUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("CheckOutPageUrl"), string.Empty);
        }
        set
        {
            SetValue("CheckOutPageUrl", value);
        }
    }


   

    #endregion

    #region "Page Events"
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panProductListing;
        Environment = Constants.Environments.Desktop;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panProductListing.Visible = false;
            return;
        }
         
        SetupEventHandlers();

        if (!EmergeRequestHelper.IsPostBack())
        {
            //if (ddlCategories.Items.Count == 0)
            //{
            //    BindCategories();
            //    SetDefaultTextForCategoryDropdown(CategoryDropDownDefaultText);
            //}
            EmergeSessionHelper.Remove(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY);
            LoadListControls(true);
        }
      
        UniPagerGiftShopItems.PageSize =  PageSize;
        UniPagerGiftShopItems.GroupSize = GroupSize;

        SetupProductRepeater();

        if (UniPagerGiftShopItems.DataSourceItemsCount <= PageSize)
            pagerMessage.Visible = false;
        else
            SetMessage(1);

        ManageControlVisibility();
        

    }

    private void ManageControlVisibility()
    {
        if (this.repGiftShopItems.Items.Count > 0 && Cart.GetCartObject().GetProducts().Count > 0)
            btnViewCart.Visible = btnChekOut.Visible = true;
        else
            btnViewCart.Visible = btnChekOut.Visible = false;
    }
    #endregion "Page Events"

    #region "Private Methods"

    private void SetupEventHandlers()
    {
        btnViewCart.Click += btnViewCart_Click;
        btnChekOut.Click += btnChekOut_Click;
        ddlCategories.SelectedIndexChanged += ddlCategories_SelectedIndexChanged;
        UniPagerGiftShopItems.OnPageChanged += UniPagerGiftShopItems_OnPageChanged;
    }
    
    private void SetMessage(int pageNumber)
    {
        int from = ((pageNumber - 1) * UniPagerGiftShopItems.PageSize) + 1;
        int to = (pageNumber * UniPagerGiftShopItems.PageSize > UniPagerGiftShopItems.DataSourceItemsCount) ? UniPagerGiftShopItems.DataSourceItemsCount : pageNumber * UniPagerGiftShopItems.PageSize;
        pagerMessage.Text = EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_PRODUCTLISTINGPAGING_MESSAGE, from.ToString(), to.ToString(), UniPagerGiftShopItems.DataSourceItemsCount.ToString());
    }

    #endregion 

    #region "Control Events"
    void UniPagerGiftShopItems_OnPageChanged(object sender, int pageNumber)
    {
        SetMessage(pageNumber);
    }
  
    void btnChekOut_Click(object sender, EventArgs e)
    {
        if (Cart.GetCartObject().GetProducts().Count == 0)
            ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ZEROPRODUCTSINCART));

        else if (!string.IsNullOrEmpty(CheckOutPageUrl))
        {
            EmergeURLHelper.Redirect(CheckOutPageUrl);
        }
    }

    void btnViewCart_Click(object sender, EventArgs e)
    {
        if (Cart.GetCartObject().GetProducts().Count == 0)
            ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ZEROPRODUCTSINCART));
        else if (!string.IsNullOrEmpty(CartPageUrl))
            EmergeURLHelper.Redirect(CartPageUrl);
    }

    void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetupProductRepeater();
    }
    #endregion "Control Events"


}