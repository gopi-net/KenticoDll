using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.GiftShop.BL;

using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService.Email;
using System.IO;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.GiftShop.Helpers;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.GiftShop;
using CMS.Helpers;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;
using System.Web.UI;

namespace Bluespire.Emerge.Components.GiftShop.WebParts
{
    public class ConfirmationMessageWebPart : GiftShopWebPart
    {

        public const string ID_FOR_TOTALAMOUNT_LITERAL = "Total";
        public const string ID_FOR_TOTALTAXAMOUNT_LITERAL = "TotalTaxAmount";
        public const string ID_FOR_GRANDTOTALAMOUNT_LITERAL = "GrandTotal";
        public const string ID_FOR_TAXPERCENTAGE_LITERAL = "litTaxPercentage";
        public const string ID_FOR_CART_USED_IN_EMAIL_TEMPLATE_REPEATER = "emailCartRepeater";
        public const string ID_FOR_CONFIRMATION_CART_REPEATER = "cartRepeater";
        public const string ID_FOR_NEGATIVE_PRODUCT_LIST_USED_IN_EMAIL_TEMPLATE_REPEATER = "productsWithNegativeStockRepeater";

        private List<Product> productsWithNegativeStock = new List<Product>();
        Order ord = new Order(Cart.GetCartObject());

        CMSRepeater EmailCartRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(ID_FOR_CART_USED_IN_EMAIL_TEMPLATE_REPEATER); } }

        CMSRepeater CartRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(ID_FOR_CONFIRMATION_CART_REPEATER); } }

        CMSRepeater NegativeProductListRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(ID_FOR_NEGATIVE_PRODUCT_LIST_USED_IN_EMAIL_TEMPLATE_REPEATER); } }


        protected void ProcessOrder()
        {
            SaveOrder();
            DisplayConfirmationMessage(GiftShopConstants.TransactionStatus.Success );
            ord.AddProperty(GiftShopConstants.GIFT_SHOP_CARTDETAILS_EMAILTEMPLATEPLACEHOLDER, GetCartHtmlForEmail());

            SendNotificationEmail(GiftShopConstants.NotificationEmail.ConfirmationEmailToAdmin);
            SendNotificationEmail(GiftShopConstants.NotificationEmail.ConfirmationEmailToUser);
            SetFormFieldsFromDictionary(GetFormFieldsForDisplay(EmergeStaticHelper.SetSiteName(GiftShopConstants.ORDERTABLE_CODENAME), ord.GetProperties()));
            ClearSessionVariables();
            
        }

        private void ClearSessionVariables()
        {
            EmergeSessionHelper.Remove(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY);
            EmergeSessionHelper.Remove(GiftShopConstants.RECENTLY_ADDED_PRODUCT_SESSIONKEY);
            EmergeSessionHelper.Remove(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY);
        }

        protected void SaveOrder()
        {
            foreach (KeyValuePair<string, object> property in ((Dictionary<string, object>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PURCHASEINFORMATIONDETAILS_SESSIONKEY)))
            {
                ord.AddProperty(property.Key, property.Value);
            }
            ord.EnsureDefaultProperties();

            try
            {
                OrderManager orderManager = new OrderManager();

                orderManager.Save(ord);

                ord.AddProperty(GiftShopConstants.GIFT_SHOP_EMAILTEMPLATEPLACEHOLDER, ord.OrderID.ToString());

                productsWithNegativeStock = ord.Cart.GetCartProductsWithNegativeStock();
                if (productsWithNegativeStock.Count > 0)
                {
                    BindNegativeProducts(productsWithNegativeStock);
                    ord.AddProperty(GiftShopConstants.GIFT_SHOP_NEGATIVEPRODUCTSDETAILS_EMAILTEMPLATEPLACEHOLDER, GetNegativeProductsHtmlForEmail());
                    SendNotificationEmail(GiftShopConstants.NotificationEmail.NegativeStockReachedDueToConcurrentTransactions);
                }

            }
            catch (GiftShopOrderNotSavedException orderNotSavedEx)
            {
                string ordHtml = ord.ToHtml();
                EmergeLogWriter.WriteError("SaveOrder - ConfirmationMessage  - GiftShopOrderNotSavedException", EventCode.EMERGE_ADD, orderNotSavedEx.ToString());
                EmergeLogWriter.WriteInformation("SaveOrder - ConfirmationMessage  - GiftShopOrderNotSavedException", EventCode.EMERGE_ADD, ordHtml);
                ord.AddProperty(GiftShopConstants.GIFT_SHOP_ORDERNOTSAVED_EMAILTEMPLATEPLACEHOLDER, ordHtml);

                SendNotificationEmail(GiftShopConstants.NotificationEmail.OrderNotSavedinDatabase);
            }
            catch (GiftShopStockNotReducedException ex)
            {
                ord.AddProperty(GiftShopConstants.GIFT_SHOP_STOCKNOTREDUCED_PRODUCTIDS_EMAILTEMPLATEPLACEHOLDER, ex.Message);

                EmergeLogWriter.WriteError("SaveOrder - ConfirmationMessage  - GiftShopStockNotReducedException", EventCode.EMERGE_ADD, ord.OrderID.ToString());
                SendNotificationEmail(GiftShopConstants.NotificationEmail.StockNotReduced);
            }
        }

        private void BindNegativeProducts(List<Product> productsWithNegativeStock)
        {
            if (null != NegativeProductListRepeater)
            {
                NegativeProductListRepeater.DataSource = productsWithNegativeStock;
                NegativeProductListRepeater.DataBind();
            }
        }

        private object GetNegativeProductsHtmlForEmail()
        {
            StringBuilder repeaterSB = new StringBuilder();
            if (null != NegativeProductListRepeater)
            {
                NegativeProductListRepeater.Visible = true;
                NegativeProductListRepeater.RenderControl(new HtmlTextWriter(new StringWriter(repeaterSB)));
                NegativeProductListRepeater.Visible = false;
            }

            return repeaterSB.ToString();
        }

        protected void BindCartProducts()
        {
            if (null != CartRepeater)
            {
                CartRepeater.DataSource = Cart.GetCartObject().GetProducts();
                CartRepeater.DataBind();
            }
            if (null != EmailCartRepeater)
            {
                EmailCartRepeater.DataSource = Cart.GetCartObject().GetProducts();
                EmailCartRepeater.DataBind();
            }

            DisplayTotalAmounts();
        }

        protected void DisplayConfirmationMessage(GiftShopConstants.TransactionStatus transactionStatus)
        {
            if (transactionStatus == GiftShopConstants.TransactionStatus.Success)
                ShowConfirmation(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_CONFIRMATIONMESSAGE_SUCCESS, GetGrandTotal()));
            else
                ShowError(ResHelper.GetString(GiftShopConstants.STRINGCODE_CONFIRMATIONMESSAGE_FAIL));

        }

        protected void SendNotificationEmail(GiftShopConstants.NotificationEmail notificationType)
        {
            EmailMessageInfo emailMessageInfo = new EmailMessageInfo();
            emailMessageInfo.Recipients = GetRecipients(notificationType);
            emailMessageInfo.IsBodyHtml = true;

            EmailService.SendEmailUsingTemplate(emailMessageInfo, GiftShopHelpers.GetEmailTemplateCodeName(notificationType), EmergeStaticHelper.GetMacrosForEmailTemplate(GetFormFieldsForDisplay(EmergeStaticHelper.SetSiteName(GiftShopConstants.ORDERTABLE_CODENAME), ord.GetProperties())), false);
        }


        private string GetCartHtmlForEmail()
        {
            StringBuilder repeaterSB = new StringBuilder();
            if (null != EmailCartRepeater)
            {
                EmailCartRepeater.Visible = true;
                EmailCartRepeater.RenderControl(new HtmlTextWriter(new StringWriter(repeaterSB)));
                EmailCartRepeater.Visible = false;
            }

            return repeaterSB.ToString();
        }

        private string GetRecipients(GiftShopConstants.NotificationEmail notificationEmail)
        {
            string recipient = string.Empty;

            if (notificationEmail == GiftShopConstants.NotificationEmail.ConfirmationEmailToUser)
            {
                return ord.GetProperties().ContainsKey(GiftShopConstants.ORDER_SENDEREMAILADDRESS_COLUMNNAME) && ord.GetProperties()[GiftShopConstants.ORDER_SENDEREMAILADDRESS_COLUMNNAME] != null ? ord.GetProperties()[GiftShopConstants.ORDER_SENDEREMAILADDRESS_COLUMNNAME].ToString() : string.Empty;
            }
            return GiftShopHelpers.GetAdminEmailRecipient();
        }

        private void DisplayTotalAmounts()
        {
            if (null != ControlPanel.FindControl(ID_FOR_TOTALAMOUNT_LITERAL))
                ((LocalizedLiteral)ControlPanel.FindControl(ID_FOR_TOTALAMOUNT_LITERAL)).Text = Cart.GetCartObject().GetTotalAmountForProductsInCart().ToString();
            if (null != ControlPanel.FindControl(ID_FOR_TOTALTAXAMOUNT_LITERAL))
                ((LocalizedLiteral)ControlPanel.FindControl(ID_FOR_TOTALTAXAMOUNT_LITERAL)).Text = Cart.GetCartObject().GetTotalTaxAmountForProductsInCart().ToString();
            if (null != ControlPanel.FindControl(ID_FOR_GRANDTOTALAMOUNT_LITERAL))
                ((LocalizedLiteral)ControlPanel.FindControl(ID_FOR_GRANDTOTALAMOUNT_LITERAL)).Text = GetGrandTotal();
            if (null != ControlPanel.FindControl(ID_FOR_TAXPERCENTAGE_LITERAL))
                ((LocalizedLiteral)ControlPanel.FindControl(ID_FOR_TAXPERCENTAGE_LITERAL)).Text = EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_TAXPERCENTAGE, TaxCalculator.GetTaxPercentage().ToString());

        }

        private static string GetGrandTotal()
        {
            return (Cart.GetCartObject().GetTotalAmountForProductsInCart() + Cart.GetCartObject().GetTotalTaxAmountForProductsInCart()).ToString(GiftShopConstants.FORMAT_TODISPLAYCOSTS);
        }
    }
}

