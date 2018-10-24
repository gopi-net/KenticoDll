using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    public class Category
    {

        #region Variables
        private int categoryID;

        public int CategoryID { get { return categoryID; } }

        private string categoryName;
        public string CategoryName { get { return categoryName; } }


        private List<Product> products;
        private Dictionary<string, object> properties;
        #endregion

        #region Constructors
        public Category(int CategoryID,string CategoryName)
        {
            this.categoryID = CategoryID;
            this.categoryName = CategoryName;
            this.products = new List<Product>();
            this.properties = new Dictionary<string, object>();
        }

        #endregion

        #region "public methods"
        
       /// <summary>
        /// Method to get product list belonging to a category
       /// </summary>
       /// <returns>retunrs products belonging to a category.</returns>
        public List<Product> GetProductsByCategoryID()
        {
            ProductManager productManager = new ProductManager();
            this.products = productManager.GetProductsByCategoryID(this.categoryID);


            return this.products;
        }

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
                throw new PropertyDoesNotExistsException(EmergeResHelper.GetStringFormat("Emerge.Exception.Message.PropertyDoesNotExistsException", key, "Category"));
            return properties[key].ToString();
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
