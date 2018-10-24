using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.DocumentEngine.Web.UI;
using CMS.Base.Web.UI;

namespace Bluespire.Emerge.Components.GiftShop.WebParts
{
    public class CartWebPart : GiftShopWebPart
    {
        MessagesPlaceHolder plcHolder = new MessagesPlaceHolder();
        const string ID_FOR_CART_REPEATER = "cartRepeater";
        const string ID_FOR_NUMBER_OF_ITEMS_IN_CART_LABEL = "lblNumberOfItems";
        const string ID_FOR_CARTTOTALAMOUNT_LITERAL = "Total";
        const string ID_FOR_CARTTOTALTAXAMOUNT_LITERAL = "TotalTaxAmount";
        const string ID_FOR_CARTGRANDTOTALAMOUNT_LITERAL = "GrandTotal";
        const string ID_FOR_CARTTAXPERCENTAGE_LITERAL = "litTaxPercentage";
        const string ID_FOR_CARTPURCHASEDQTY_TEXTBOX = "PurchasedQty";


        CMSRepeater CartRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(ID_FOR_CART_REPEATER); } }

        Label lblNumberOfItems
        { get { return (Label)ControlPanel.FindControl(ID_FOR_NUMBER_OF_ITEMS_IN_CART_LABEL); } }

        Literal Total
        { get { return (Literal)ControlPanel.FindControl(ID_FOR_CARTTOTALAMOUNT_LITERAL); } }

        Literal TotalTaxAmount
        { get { return (Literal)ControlPanel.FindControl(ID_FOR_CARTTOTALTAXAMOUNT_LITERAL); } }


        Literal GrandTotal
        { get { return (Literal)ControlPanel.FindControl(ID_FOR_CARTGRANDTOTALAMOUNT_LITERAL); } }

        Literal litTaxPercentage
        { get { return (Literal)ControlPanel.FindControl(ID_FOR_CARTTAXPERCENTAGE_LITERAL); } }


        #region "Webpart Properties"

        public string PurchaseInformationFormUrl
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("PurchaseInformationFormUrl"), string.Empty);
            }
            set
            {
                SetValue("PurchaseInformationFormUrl", value);
            }
        }

        public string TransformationName
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("TransformationName"), CartRepeater.TransformationName);
            }
            set
            {
                SetValue("TransformationName", value);
                CartRepeater.TransformationName = value;
            }
        }

      
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

        public string CartHeaderTemplateText
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("CartHeaderTemplateText"), string.Empty);
            }
            set
            {
                SetValue("CartHeaderTemplateText", value);
            }
        }

        #endregion

        protected Cart CurrentGiftShopCart { get { return Cart.GetCartObject(); } }

        


        protected void SetupCartRepeater()
        {
            CartRepeater.TransformationName = TransformationName;
            CartRepeater.ItemCommand += cartRepeater_ItemCommand;
        }

        private void SetNumberOfProductsInTheCart()
        {
            lblNumberOfItems.Text = EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_NUMBEROFITEMSINCART, CurrentGiftShopCart.GetProducts().Count);
        }

        protected void ReviseTotalAmounts()
        {
            Total.Text = CurrentGiftShopCart.GetTotalAmountForProductsInCart().ToString(GiftShopConstants.FORMAT_TODISPLAYCOSTS);
            TotalTaxAmount.Text = CurrentGiftShopCart.GetTotalTaxAmountForProductsInCart().ToString(GiftShopConstants.FORMAT_TODISPLAYCOSTS);
            GrandTotal.Text = (CurrentGiftShopCart.GetTotalAmountForProductsInCart() + CurrentGiftShopCart.GetTotalTaxAmountForProductsInCart()).ToString(GiftShopConstants.FORMAT_TODISPLAYCOSTS);

            litTaxPercentage.Text = EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_TAXPERCENTAGE, TaxCalculator.GetTaxPercentage().ToString());
        }

        protected void BindCartProducts()
        {

            CartRepeater.DataSource = CurrentGiftShopCart.GetProducts();
            CartRepeater.DataBind();

            SetCartRepeaterHeader();

            SetNumberOfProductsInTheCart();
            ReviseTotalAmounts();
        }

        private void SetCartRepeaterHeader()
        {
            ((LocalizedLiteral)CartRepeater.Controls[0].FindControl("CartHeaderLiteral")).Text = CartHeaderTemplateText;
        }


        protected void cartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                plcHolder.ClearLabels();
                if (e.CommandName == "RemoveProduct")
                {

                    CurrentGiftShopCart.RemoveProductFromCart(CurrentGiftShopCart.GetProducts().Find(x => (x.ProductID == Convert.ToInt32(e.CommandArgument))));
                    if (CurrentGiftShopCart.GetProducts().Count == 0)
                        EmergeURLHelper.Redirect(ProductListingPageUrl);
                    BindCartProducts();

                }
                else if (e.CommandName == "PurchasedQuantityChanged")
                {


                    TextBox purchasedQty = (TextBox)e.Item.Controls[0].FindControl(ID_FOR_CARTPURCHASEDQTY_TEXTBOX);// textbox


                    try
                    {
                        CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).SyncStock(GetFreshProduct(Convert.ToInt32(e.CommandArgument)));
                    }
                    catch (ProductNotFoundException)
                    {
                        purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                        ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE)); return;
                    }


                    if (CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).GetStock() == 0)
                    {
                        purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                        ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE)); return;
                    }

                    if (CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).IsQuantityInStock(Convert.ToInt32(purchasedQty.Text.Trim())))
                    {
                        try
                        {
                            CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).SetPurchasedQuantity(Convert.ToInt32(purchasedQty.Text.Trim()));
                            CurrentGiftShopCart.AddProductToCart(CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)));
                            BindCartProducts();
                            //ClearRepeaterValidationMessages();
                            purchasedQty.CssClass = "";
                            ShowConfirmation(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_CHANGED_MESSAGE, purchasedQty.Text.Trim()));
                        }
                        catch (ZeroPurchasedQuantityException)
                        {
                            purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                            ShowError(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_ZEROPURCHASEDQUANTITY_EXCEPTION)); return;
                        }

                    }
                    else
                    {
                        purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                        ShowError(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_OUTOFSTOCK_CARTMESSAGE, purchasedQty.Text.Trim(), CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).GetStock()));
                        purchasedQty.Text = CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(e.CommandArgument)).GetProductPurchasedQuantity().ToString();
                    }
                }


            }
        }

        private void ClearRepeaterValidationMessages()
        {
            foreach (RepeaterItem ri in CartRepeater.Items)
            {

                TextBox purchasedQty = (TextBox)ri.Controls[0].FindControl(ID_FOR_CARTPURCHASEDQTY_TEXTBOX);
                if (null != purchasedQty)
                {
                    purchasedQty.CssClass = string.Empty;
                }
            }
           
            plcHolder.ClearLabels();
        }


        protected bool ValidateProductsAndSetMessage()
        {
            bool isValid = true;
            foreach (RepeaterItem ri in CartRepeater.Items)
            {
                HiddenField ProductID = (HiddenField)ri.Controls[0].FindControl("hdnProductID");
                TextBox purchasedQty = (TextBox)ri.Controls[0].FindControl(ID_FOR_CARTPURCHASEDQTY_TEXTBOX);

                if (null != ProductID && null != purchasedQty)
                {
                    try
                    {

                        CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(ProductID.Value)).SyncStock(GetFreshProduct(Convert.ToInt32(ProductID.Value)));


                        if (false == CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(ProductID.Value)).IsPurchasedQuantityAvailableInStock())
                        {
                            purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                            isValid = false;
                          //  ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE));
                        }
                        else
                        {
                            purchasedQty.CssClass = string.Empty;
                        }

                    }
                    catch (ProductNotFoundException)
                    {
                        purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
                        isValid = false;
                    }
                }

            }

            if (!isValid)
                ShowError(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE));

            return isValid;

        }

        //protected void cartRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        TextBox purchasedQty = (TextBox)e.Item.Controls[0].FindControl(ID_FOR_CARTPURCHASEDQTY_TEXTBOX);

        //        if (null != purchasedQty)
        //        {
        //            try
        //            {

        //                CurrentGiftShopCart.GetProducts().Find(x => x.ProductID == Convert.ToInt32(((Product)e.Item.DataItem).ProductID)).SyncStock(GetFreshProduct(Convert.ToInt32(((Product)e.Item.DataItem).ProductID)));



        //                if (false == ((Product)e.Item.DataItem).IsPurchasedQuantityAvailableInStock())
        //                {
        //                    purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
        //                    ShowError(ResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE));
        //                }
        //                else
        //                {
        //                    purchasedQty.CssClass = "";
        //                }

        //            }
        //            catch (ProductNotFoundException)
        //            {
        //                purchasedQty.CssClass = GiftShopConstants.CSSCLASS_FOR_TEXTBOX_WITH_RED_BORDER;
        //                ShowError(ResHelper.GetString(GiftShopConstants.STRINGCODE_PURCHASED_QUANTITY_PRODUCTNOTFOUNDOROUTOFSTOCK_GENERICMESSAGE));
        //            }
        //        }

        //    }

        //    if (e.Item.ItemType == ListItemType.Footer)
        //    {
        //        Repeater cartRepeater = sender as Repeater;

        //        if (CurrentGiftShopCart.GetProducts().Count == 0)
        //        {
        //            cartRepeater.Visible = false;
        //        }
        //    }

        //}

        private Product GetFreshProduct(int ProductID)
        {
            ProductManager productManager = new ProductManager();
            return productManager.GetProductByProductID(ProductID);
        }

    }
}
