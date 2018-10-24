using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.GiftShop
{
    public static class GiftShopConstants
    {
        #region "Pages"
        public const string GIFTSHOP_DASHBOARDPAGE = "GiftShopDashboardPage";
        public const string GIFTSHOP_LISTPAGE = "GiftShopListPage";
        public const string GIFTSHOP_DATAVIEWITEMPAGE = "GiftShopDataViewItemPage";
        public const string GIFTSHOP_DATASELECTFIELDSPAGE = "GiftShopDataSelectFieldsPage";
        public const string GIFTSHOP_DATALISTPAGE = "GiftShopDataListPage";
        public const string GIFTSHOP_DATAEDITITEMPAGE = "GiftShopDataEditItemPage";
        #endregion "Pages"


        #region "Field Names"
        
        public const string PRODUCT_ADDTOCART_OR_SHOWINFO_COLUMNNAME = "AddCartOrInformation";
        public const string PRODUCT_STOCK_COLUMNNAME = "Stock";
        public const string PRODUCT_PRODUCTNAME_COLUMNNAME = "ProductName";
        public const string PRODUCT_PRODUCTIMAGE_COLUMNNAME = "ProductImage";
        public const string PRODUCT_CATEGORY_COLUMNNAME = "CategoryID";
        public const string ORDER_DELIVERYSTATUS_COLUMNNAME = "DeliveryStatus";
        public const string ORDER_SENDEREMAILADDRESS_COLUMNNAME = "SenderEmailAddress";
        public const string ORDERDETAIL_PURCHASEDQTY_COLUMNNAME = "PurchasedQty";
        public const string ORDERDETAIL_UNITPRICE_COLUMNNAME = "UnitPrice";
        public const string ORDERDETAIL_TAXAMOUNT_COLUMNNAME = "TaxAmount";
        public const string CATEGORY_CATEGORYNAME_COLUMNNAME = "CategoryName";
        public const string CONFIGURATION_VALUE_COLUMNNAME = "Value";
        public const string CONFIGURATION_KEY_COLUMNNAME = "Key";
        public const string PRODUCTINFORMATIONREQUESTSTABLE_PRODUCTID_COLUMNNAME = "ProductID";
        #endregion


        #region "Table Names"
        public const string PRODUCTTABLE_CODENAME = "customtable.Emerge_{0}_GS_Products";
        public const string CONFIGURATIONTABLE_CODENAME = "customtable.Emerge_{0}_GS_Configurations_INTERN";
        public const string CATEGORYTABLE_CODENAME = "customtable.Emerge_{0}_GS_Categories";
        public const string ORDERTABLE_CODENAME = "customtable.Emerge_{0}_GS_Orders";
        public const string ORDERDETAILTABLE_CODENAME = "customtable.Emerge_{0}_GS_OrderDetails_INTERN";
        public const string PRODUCTINFORMATIONREQUESTSTABLE_CODENAME = "customtable.Emerge_{0}_GS_ProductInformationRequests";

        #endregion "Table Names"


        #region "Session Keys"
        public const string GIFT_SHOP_PRODUCTS_SESSIONKEY = "GIFT_SHOP_PRODUCTS";
        public const string GIFT_SHOP_CART_SESSIONKEY = "GIFT_SHOP_CART";
        public const string GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY = "GIFT_SHOP_PURCHASE_INFORMATION_DETAILS";
        public const string RECENTLY_ADDED_PRODUCT_SESSIONKEY = "RECENTLY_ADDED_PRODUCT_ID";
        
        #endregion


        #region "Configuration Keys"
        public const string GIFT_SHOP_ADMIN_EMAIL_ADDRESS_KEY = "AdminNotificationToEmailAddress";
        #endregion


        #region "Email Template code names"
        public const string ADMIN_NOTIFICATION_EMAILTEMPLATE = "Emerge_{0}_GS_NotificationEmailAdmin";
        public const string USER_NOTIFICATION_EMAILTEMPLATE = "Emerge_{0}_GS_NotificationEmailUser";
        public const string ADMIN_NOTIFICATION_NEGATIVESTOCKREACHED_ALERTEMAILTEMPLATE = "Emerge_{0}_GS_NotificationNegativeStockReachedEmailAdmin";
        public const string ADMIN_NOTIFICATION_STOCKNOTREDUCED_ALERTEMAILTEMPLATE = "Emerge_{0}_GS_NotificationProductStockNotReducedEmailAdmin";
        public const string ADMIN_NOTIFICATION_ORDERNOTSAVED_ALERTEMAILTEMPLATE = "Emerge_{0}_GS_NotificationOrderNotSavedEmailAdmin";
        public const string ADMIN_NOTIFICATION_PRODUCTINFORMATIONREQUEST_EMAILTEMPLATE = "Emerge_{0}_GS_NotificationProductInformationRequestEmailAdmin";
        #endregion


        #region "Email Template place holders"
        public const string GIFT_SHOP_CARTDETAILS_EMAILTEMPLATEPLACEHOLDER = "CART_DETAILS";
        public const string GIFT_SHOP_NEGATIVEPRODUCTSDETAILS_EMAILTEMPLATEPLACEHOLDER = "NEGATIVE_PRODUCT_DETAILS";
        public const string GIFT_SHOP_EMAILTEMPLATEPLACEHOLDER = "OrderID";
        public const string GIFT_SHOP_ORDERNOTSAVED_EMAILTEMPLATEPLACEHOLDER = "ORDER_HTML";
        public const string GIFT_SHOP_STOCKNOTREDUCED_PRODUCTIDS_EMAILTEMPLATEPLACEHOLDER = "STOCK_NOT_REDUCED_FOR_PRODUCT_IDS";

        #endregion


        # region "String Codes"
        public const string STRINGCODE_CONFIGURATIONITEMMISSINGEXCEPTION_MESSAGE = "Emerge.GS.Message.ConfigurationItemMissingException";
        public const string STRINGCODE_EXPECTEDCOLUMNNOTFOUNDEXCEPTION_MESSAGE = "Emerge.Exception.ErrorMessage.ExpectedColumnNotFoundException";
        public const string STRINGCODE_ZEROPURCHASEDQUANTITYEXCEPTION_MESSAGE = "Emerge.Exception.ErrorMessage.ZeroPurchasedQuantityException";
        public const string STRINGCODE_PROPERTYDOESNOTEXISTSEXCEPTION_MESSAGE = "Emerge.Exception.Message.PropertyDoesNotExistsException";
        public const string STRINGCODE_ZERO_OR_NEGATIVESTOCK = "Emerge.GS.ProductDetails.Label.NegativeStock";
        public const string STRINGCODE_MESSAGEFOR_ZERO_OR_NEGATIVESTOCKPRODUCT = "Emerge.GS.ProductDetails.Message.NegativeStock";
        public const string STRINGCODE_POSITIVESTOCK = "Emerge.GS.ProductDetails.Label.PositiveStock";
        public const string STRINGCODE_ZEROPRODUCTSINCART = "Emerge.GS.ProductListing.ShowMessage.ZeroProducts";
        public const string STRINGCODE_CONFIRMATIONMESSAGE_SUCCESS = "Emerge.GS.GiftShopCart.ConfirmationMessage.Success";
        public const string STRINGCODE_CONFIRMATIONMESSAGE_FAIL = "Emerge.GS.GiftShopCart.ConfirmationMessage.Fail";
        public const string STRINGCODE_TAXPERCENTAGE = "Emerge.GS.GiftShopCart.Label.TaxPercentage";
        public const string STRINGCODE_NUMBEROFITEMSINCART = "Emerge.GS.GiftShopCart.Label.NumberOfItems";
        public const string STRINGCODE_PURCHASED_QUANTITY_CHANGED_MESSAGE = "Emerge.GS.Cart.ShowMessage.PurchasedQuantityChanged";
        public const string STRINGCODE_ZEROPURCHASEDQUANTITY_EXCEPTION = "Emerge.GS.Cart.ShowMessage.ZeroPurchasedQuantityException";
        public const string STRINGCODE_PURCHASED_QUANTITY_OUTOFSTOCK_MESSAGE = "Emerge.GS.Cart.ShowMessage.PurchasedQuantityOutOfStock";
        public const string STRINGCODE_PURCHASED_QUANTITY_OUTOFSTOCK_CARTMESSAGE = "Emerge.GS.Cart.ShowMessage.CARTPurchasedQuantityOutOfStock";
        public const string STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE = "Emerge.GS.Cart.ShowGenericMessage.PurchasedQuantityOutOfStockOrProductNotFound";
        public const string STRINGCODE_PRODUCTINFORMATIONREQUEST_MESSAGE = "Emerge.GS.Cart.ShowMessage.ProductInformationRequestReceived";
        public const string STRINGCODE_CARTPRODUCTSNOTEXISTSINDATABASE_MESSAGE = "Emerge.GS.Cart.ShowMessage.CartProductNotFoundInDatabase";
        public const string STRINGCODE_NOPRODUCTFOUNDTEXT_MESSAGE = "Emerge.GS.ProductListing.Message.NoProductFoundText";
        public const string STRINGCODE_PRODUCTLISTINGPAGING_MESSAGE = "Emerge.GS.ProductListing.Message.PagingMessage";
        public const string STRINGCODE_CART_PRODUCTNOTFOUND_ORREACHZEROSTOCK_MESSAGE = "Emerge.GS.GiftShopCart.Message.ProductNotFoundOrReachedZeroStockMessage";

        public const string STRINGCODE_GIFTSHOPHOME = "Emerge.GS.Dashboard";
        public const string STRINGCODE_ORDERDATALIST_STATUSCHANGEMESSAGE = "Emerge.GC.OrderDataList.StatusChangeMessage";
        public const string STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE = "Emerge.Exception.ErrorMessage.NoItemSelectedException";

        #endregion

        # region "Control Names"
        
        #endregion

        #region "Others"
        public const string ADDTOCART_VALUE = "Add to Cart";
        public const string BLANK_SPACE = " ";
        public const string CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER = "redcolorborder";
        public const string FORMAT_TODISPLAYCOSTS = "0.00";
        #endregion

        # region enum
        public enum DeliveryStatus
        {
            Pending,
            Completed
        }
        public enum NotificationEmail
        {
            NegativeStockReachedDueToConcurrentTransactions,
            OrderNotSavedinDatabase,
            StockNotReduced,
            ConfirmationEmailToAdmin,
            ConfirmationEmailToUser,
            ProductInformationRequestEmail
        }

        public enum TransactionStatus
        {
            Success,
            Fail
        }
        #endregion


        #region "Page urls"
        #region PageURLs

        public const string PAGEURL_LIST_GIFTSHOP = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_List.aspx";
        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_ViewItem.aspx";
        public const string PAGEURL_DATA_EDITITEM = "~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_EditItem.aspx";
        public const string PAGEURL_GIFTSHOP_DASHBOARD = "~/CMSModules/CMS_GiftShop/Dashboard/Dashboard.aspx";


        #endregion
        #endregion
    }
}
