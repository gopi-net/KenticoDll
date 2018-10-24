using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    public class Product
    {

        #region Variables

        private int productID;
        private int categoryID;
        private Dictionary<string, object> properties = new Dictionary<string, object>();

        #endregion Variables

        #region Prop
        public int ProductID { get { return productID; } set { productID = value; } }

        public int CategoryID { get { return categoryID; } set { categoryID = value; } }


        #endregion Prop

        #region constructors
        public Product(int ProductID, int CategoryID)
        {
            this.productID = ProductID;
            this.categoryID = CategoryID;
            
        }

        public Product(int ProductID, int CategoryID, Dictionary<string, object> properties)
            : this(ProductID, CategoryID)
        {
            this.properties = properties;
        }




        #endregion constructors

        #region "public methods"
        /// <summary>
        /// add or overwrite the property
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void AddProperty(string key, object value)
        {
            if (!IsPropertyExists(key))
            {
                if (null != value)
                    properties.Add(key, value);
                else
                    properties.Add(key, string.Empty);
            }
            else
            {
                properties[key] = value.ToString();
            }
        }

        /// <summary>
        /// Search "Properties" dictionary item and returns value.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value from "Properties" dictionary item</returns>
        /// <exception cref="PropertyDoesNotExistsException">Thrown if Property does not exists ...</exception>
        public string GetPropertyValueByName(string key)
        {
            if (!IsPropertyExists(key))
                throw new PropertyDoesNotExistsException(EmergeResHelper.GetStringFormat(GiftShopConstants.STRINGCODE_PROPERTYDOESNOTEXISTSEXCEPTION_MESSAGE, key, "Product"));
            return properties[key].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Properties of the Product</returns>
        public Dictionary<string, object> GetProperties()
        {
            return properties;
        }

        /// <summary>
        /// Compare the Quantity passed with the Stock available in Product's properties.
        /// </summary>
        /// <param name="Quantity">Quantity</param>
        /// <returns>true if Quantity is less than or equal to stock available in Product's properties.</returns>
        public bool IsQuantityInStock(int Quantity)
        {
            if (Quantity <= this.GetStock())
                return true;
            return false;
        }

        /// <summary>
        /// Method will Sync the Stock of Fresh Product with the Stock in the Invoking Product's properties. then Compare purchased quantity of the invoking object with the stock of Passed object.
        /// </summary>
        /// <param name="productFromDb"> Product object</param>
        /// <returns>true if purchased quantity of the invoking object is less than stock of the passed object. </returns>
        /// SyncStockAndCheckIfPurchasedQtyAvailableInStock
        public bool SyncStockAndCheckIfPurchasedQtyAvailableInStock(Product productFromDb)
        {
            this.SyncStock(productFromDb);
            return this.IsPurchasedQuantityAvailableInStock();
        }


        /// <summary>
        /// compare Purchased Quantity and Stock available in the properties of the product.
        /// </summary>
        /// <returns>true if Purchased Quantity less than or equal to Stock avaiable in the Product's properties.</returns>
        public bool IsPurchasedQuantityAvailableInStock()
        {
            return this.IsQuantityInStock(this.GetProductPurchasedQuantity());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Procucts Unit Price.</returns>
        public double GetProductUnitPrice()
        {
            return Convert.ToDouble(this.GetPropertyValueByName(GiftShopConstants.ORDERDETAIL_UNITPRICE_COLUMNNAME));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Purchased Quantity</returns>
        public int GetProductPurchasedQuantity()
        {
            return Convert.ToInt32(this.GetPropertyValueByName(GiftShopConstants.ORDERDETAIL_PURCHASEDQTY_COLUMNNAME));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns true if AddCartOrInformation has value "Add to Cart" </returns>
        public bool IsShowAddToCartOption()
        {
            return (this.GetPropertyValueByName(GiftShopConstants.PRODUCT_ADDTOCART_OR_SHOWINFO_COLUMNNAME).ToLower().Equals(GiftShopConstants.ADDTOCART_VALUE.ToLower()) ? true : false);
        }

        /// <summary>
        /// Set Purchased Quantity in Properties of Product
        /// </summary>
        /// <param name="quantity">quantity</param>
        /// <exception cref="ZeroPurchasedQuantityException">Throws ZeroPurchasedQuantityException if quantity is zero.</exception>
        public void SetPurchasedQuantity(int quantity)
        {
            if (quantity == 0) throw new ZeroPurchasedQuantityException();
            this.AddProperty(GiftShopConstants.ORDERDETAIL_PURCHASEDQTY_COLUMNNAME, quantity);
        }

       

        /// <summary>
        /// Method to get Stock available in the Product properties.
        /// </summary>
        /// <returns>Returns Stock available in the Product properties.</returns>
        public int GetStock()
        {
            return Convert.ToInt32(this.GetPropertyValueByName(GiftShopConstants.PRODUCT_STOCK_COLUMNNAME));
        }

        /// <summary>
        /// Sync Stock of the product with the passed product.
        /// </summary>
        /// <param name="freshProduct"></param>
        public void SyncStock(Product freshProduct)
        {
            this.AddProperty(GiftShopConstants.PRODUCT_STOCK_COLUMNNAME, freshProduct.GetStock());
        }


        public string GetProductName()
        {
            return this.GetPropertyValueByName(GiftShopConstants.PRODUCT_PRODUCTNAME_COLUMNNAME);
            
        }

        #endregion

        #region "private methods"
        private bool IsPropertyExists(string key)
        {
            return properties.ContainsKey(key);
        }

         
        
        #endregion

    }
}
