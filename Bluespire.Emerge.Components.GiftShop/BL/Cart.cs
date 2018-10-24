using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;

namespace Bluespire.Emerge.Components.GiftShop.BL
{

    public class Cart
    {
        #region Variables
        List<Product> products = new List<Product>();
        ProductManager productManager;
        #endregion Variables

        #region Constructors
        private Cart()
        {
            productManager = new ProductManager();
        }
        #endregion Constructors

        #region "public methods"

        /// <summary>
        /// Method will return the total cost for all the products in Cart.
        /// </summary>
        /// <returns>Returns Total Cost (ronded to two decimal places) for all the products  in the cart.</returns>
        public double GetTotalAmountForProductsInCart()
        {
            double subTotal = 0.0;
            foreach (Product product in products)
            {
                subTotal += Convert.ToDouble(product.GetProductPurchasedQuantity() * product.GetProductUnitPrice());
            }

            return (Math.Round(subTotal, 2));
        }

        /// <summary>
        /// Method will return the total tax amount for all the products in Cart.
        /// </summary>
        /// <returns>Returns Total tax amount (ronded to two decimal places) for all the products  in the cart.</returns>
        public double GetTotalTaxAmountForProductsInCart()
        {
            return Math.Round(TaxCalculator.GetTaxAmount(this.GetTotalAmountForProductsInCart()), 2);
        }

        /// <summary>
        /// Method to return all products available in the cart
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            return products;
        }

        /// <summary>
        /// Method to add product in the cart
        /// </summary>
        /// <param name="product"></param>
        public void AddProductToCart(Product product)
        {
            if (product.GetProductPurchasedQuantity() == 0)
                throw new ZeroPurchasedQuantityException(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ZEROPURCHASEDQUANTITYEXCEPTION_MESSAGE));

            CalculateTaxAmountForProduct(product);

            if (IsProductAvailableInCart(product))
            {
                int index = RemoveProductFromCart(product);
                products.Insert(index, product);
            }
            else
            {
                products.Add(product);
            }

        }


        /// <summary>
        /// Method to remove product from the cart
        /// </summary>
        /// <param name="product"></param>
        public int RemoveProductFromCart(Product product)
        {
            int index = 0;
            if (IsProductAvailableInCart(product))
            {
                index = products.FindIndex(x => x.ProductID == product.ProductID);
                products.Remove(products.Find(x => x.ProductID == product.ProductID));
            }
            return index;
        }

        /// <summary>
        /// Method to get Current Cart Object
        /// </summary>
        /// <returns>Cart.</returns>
        public static Cart GetCartObject()
        {
            Cart giftShopCart;

            if (null == EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_CART_SESSIONKEY))
            {
                giftShopCart = new Cart();
                EmergeSessionHelper.SetValue(GiftShopConstants.GIFT_SHOP_CART_SESSIONKEY, giftShopCart);
            }
            else
            {
                giftShopCart = (Cart)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_CART_SESSIONKEY);
            }

            return giftShopCart;
        }

        /// <summary>
        /// Removes Cart From the Session.
        /// </summary>
        public void Destroy()
        {
            EmergeSessionHelper.Remove(GiftShopConstants.GIFT_SHOP_CART_SESSIONKEY);
        }

        /// <summary>
        /// Method to get fresh product from the database and compare the actual stock (available in db) with the purchase quantity of the product in the cart. 
        /// This method will verify stock availibility for each product in the cart.
        /// </summary>
        /// <exception cref="ProductOutOfStockException">throws if a product in the cart goes ou of stock.</exception>
        public void VerifyStockAvailabilityForProductsInCart()
        {
            //ProductManager productManager = new ProductManager();
            foreach (Product product in this.GetProducts())
            {
                Product freshProduct = productManager.GetProductByProductID(product.ProductID);
                if (!product.SyncStockAndCheckIfPurchasedQtyAvailableInStock(freshProduct)) throw new ProductOutOfStockException();
            }

        }

        /// <summary>
        /// checks products added in the cart should be available in database. In case Product is no longer available in the database, it throws ProductNotFoundException exception. 
        /// </summary>
        /// <exception cref="ProductNotFoundException">Throws exception with message containing comma seperated list of Product names which are no longer available in database.</exception>
        public void EnsureCartProductsExists()
        {
            string cartProductsNotFoundInDatabase = string.Empty;

            List<Product> cartProducts = this.GetProducts().ToList();
            foreach (Product product in cartProducts)
            {
                try
                {
                    Product freshProduct = productManager.GetProductByProductID(product.ProductID);
                }
                catch (ProductNotFoundException)
                {
                    cartProductsNotFoundInDatabase += product.GetProductName() + Constants.COMMA_SEPERATOR.ToString();
                    //this.RemoveProductFromCart(product);
                }

            }
            if (!string.IsNullOrEmpty(cartProductsNotFoundInDatabase))
            {
                if (cartProductsNotFoundInDatabase.EndsWith(Constants.COMMA_SEPERATOR.ToString()))
                    cartProductsNotFoundInDatabase = cartProductsNotFoundInDatabase.Substring(0, cartProductsNotFoundInDatabase.LastIndexOf(Constants.COMMA_SEPERATOR));
                throw new ProductNotFoundException(cartProductsNotFoundInDatabase);
            }

        }

        /// <summary>
        /// To get list of products in the cart with the negative stock. Cart  products may goes in negative stocking while payment processing. 
        /// </summary>
        /// <returns>List of products with negative stock</returns>
        public List<Product> GetCartProductsWithNegativeStock()
        {
            List<Product> productsWithNegativeStock = new List<Product>();

            foreach (Product product in this.GetProducts())
            {
                Product freshProduct = productManager.GetProductByProductID(product.ProductID);
                {
                    if (freshProduct.GetStock() < 0)
                    {
                        productsWithNegativeStock.Add(freshProduct);
                    }
                }
            }

            return productsWithNegativeStock;

        }

        #endregion

        #region "private methods"

        private void CalculateTaxAmountForProduct(Product product)
        {
            double taxAmountForProduct = Math.Round(TaxCalculator.GetTaxAmount(product.GetProductPurchasedQuantity() * product.GetProductUnitPrice()), 2);
            product.AddProperty(GiftShopConstants.ORDERDETAIL_TAXAMOUNT_COLUMNNAME, taxAmountForProduct);
        }

        private bool IsProductAvailableInCart(Product product)
        {
            return (products.Any(x => x.ProductID == product.ProductID) ? true : false);
        }

        #endregion

    }
}
