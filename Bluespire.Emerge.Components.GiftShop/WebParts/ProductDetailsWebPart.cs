using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.MediaLibrary;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Components.GiftShop.Helpers;
using Bluespire.Emerge.Components.GiftShop.WebParts;
using CMS.Base.Web.UI;
namespace Bluespire.Emerge.Components.GiftShop.WebParts
{
    public class ProductDetailsWebPart : GiftShopWebPart
    {
         const string ID_FOR_PRODUCTIMAGE = "ProductImage";
         const string ID_FOR_ADDTOCART_BUTTON = "btnAddToCart";
         const string ID_FOR_SENDINFORMATION_BUTTON = "btnSendInformation";
         const string ID_FOR_PURCHASEDQTY_TEXTBOX = "PurchasedQty";
         const string ID_FOR_STOCK_LABEL = "Stock";
         const string ID_FOR_PURCHASEDQUANTITY_LABEL = "lblPurchasedQuantity";
         const string ID_FOR_AVAILABILITY_LABEL = "lblAvailability";

        protected ProductManager productManager = new ProductManager();


        private string SelectedProductImageGUID
        {
            get
            {

                if (SelectedProduct.GetProperties().ContainsKey(GiftShopConstants.PRODUCT_PRODUCTIMAGE_COLUMNNAME))
                {
                    return SelectedProduct.GetProperties()[GiftShopConstants.PRODUCT_PRODUCTIMAGE_COLUMNNAME].ToString();
                }
                return string.Empty;

            }
        }

        public int ProductID
        {
            get
            {
                int _ProductID = 0;
                if (Convert.ToInt32(EmergeQueryHelper.GetString("ProductID", "0")) == 0)
                {
                    if (null != EmergeSessionHelper.GetValue(GiftShopConstants.RECENTLY_ADDED_PRODUCT_SESSIONKEY))
                        _ProductID = Convert.ToInt32(EmergeSessionHelper.GetValue(GiftShopConstants.RECENTLY_ADDED_PRODUCT_SESSIONKEY));
                }
                else
                    _ProductID = Convert.ToInt32(EmergeQueryHelper.GetString("ProductID", "0"));
                return _ProductID;
            }
        }

        public Product SelectedProduct
        {
            get
            {

                return ((List<Product>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY)).Find(x => x.ProductID == ProductID);
            }
        }

        Image ProductImage
        { get { return (Image)ControlPanel.FindControl(ID_FOR_PRODUCTIMAGE); } }

        Button btnAddToCart
        { get { return (Button)ControlPanel.FindControl(ID_FOR_ADDTOCART_BUTTON); } }

        Button btnSendInformation
        { get { return (Button)ControlPanel.FindControl(ID_FOR_SENDINFORMATION_BUTTON); } }

        TextBox PurchasedQty
        { get { return (TextBox)ControlPanel.FindControl(ID_FOR_PURCHASEDQTY_TEXTBOX); } }

        LocalizedLabel Stock
        { get { return (LocalizedLabel)ControlPanel.FindControl(ID_FOR_STOCK_LABEL); } }

        LocalizedLabel lblPurchasedQuantity
        { get { return (LocalizedLabel)ControlPanel.FindControl(ID_FOR_PURCHASEDQUANTITY_LABEL); } }

        LocalizedLabel lblAvailability
        { get { return (LocalizedLabel)ControlPanel.FindControl(ID_FOR_AVAILABILITY_LABEL); } }
        
        /// <summary>
        /// Method to display selected product detail.
        /// </summary>
        /// <exception cref="SessionDataMissingException">Throws SessionDataMissingException if Selectedproduct is null.</exception>
        protected void SetupProductDetail()
        {
            if (null == SelectedProduct) throw new SessionDataMissingException();
            SelectedProduct.SyncStock(GetFreshProduct(ProductID));

            SetFormFieldsFromDictionary(GetFormFieldsForDisplay(EmergeStaticHelper.SetSiteName(GiftShopConstants.PRODUCTTABLE_CODENAME), SelectedProduct.GetProperties()));

            SetProductImage();
            SetStockLabel();
            
           
        }

        private void SetProductImage()
        {
            if (!string.IsNullOrEmpty(SelectedProductImageGUID) && ProductImage != null)
                ProductImage.ImageUrl = EmergeMediaLibraryHelper.GetMediaFileUrl(SelectedProductImageGUID, EmergeSiteInfoProvider.CurrentSiteName);
        }

        public void EnableDisabledFields()
        {
            if (SelectedProduct.IsShowAddToCartOption())
            {
                btnAddToCart.Visible = true;
                btnSendInformation.Visible = false;
                lblPurchasedQuantity.Visible = true;
                PurchasedQty.Visible = true;
                lblAvailability.Visible = true;
                Stock.Visible = true;
            }
            else
            {
                btnAddToCart.Visible = false;
                btnSendInformation.Visible = true;
                lblPurchasedQuantity.Visible = false;
                PurchasedQty.Visible = false;
                Stock.Visible = false;
                lblAvailability.Visible = false;
            }
        }

        private void SetStockLabel()
        {
            if (Stock != null)
            {
                int stock = SelectedProduct.GetStock();
                if (stock > 0)
                    Stock.Text = EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_POSITIVESTOCK, stock.ToString());
                else
                    Stock.Text = EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ZERO_OR_NEGATIVESTOCK);
            }
        }

        private Product GetFreshProduct(int ProductID)
        {
            return productManager.GetProductByProductID(ProductID);
        }

        protected void SendNotificationEmail()
        {
            EmailMessageInfo emailMessageInfo = new EmailMessageInfo();
            emailMessageInfo.Recipients = GiftShopHelpers.GetAdminEmailRecipient();
            emailMessageInfo.IsBodyHtml = true;

            EmailService.SendEmailUsingTemplate(emailMessageInfo, GiftShopHelpers.GetEmailTemplateCodeName(GiftShopConstants.NotificationEmail.ProductInformationRequestEmail), EmergeStaticHelper.GetMacrosForEmailTemplate(GetFormFieldsForDisplay(EmergeStaticHelper.SetSiteName(GiftShopConstants.PRODUCTINFORMATIONREQUESTSTABLE_CODENAME), FormParameters)), false);
        }

        protected void AddProductToCart()
        {
            SelectedProduct.SetPurchasedQuantity(Convert.ToInt32(PurchasedQty.Text));

            Cart giftShopCart = Cart.GetCartObject();
            giftShopCart.AddProductToCart(SelectedProduct);

            EmergeSessionHelper.SetValue(GiftShopConstants.RECENTLY_ADDED_PRODUCT_SESSIONKEY, SelectedProduct.ProductID);
        }

    }
}
