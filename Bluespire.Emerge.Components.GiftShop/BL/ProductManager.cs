using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    public class ProductManager
    {
        private ProductDAL productDAL ;
      

        # region "Constructor"
        public ProductManager()
        {
            this.productDAL = new ProductDAL();
        }
        # endregion "Constructor"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <returns>List of All Products from the Database.</returns>
        public List<Product> GetAllProducts(string orderBy)
        {
            List<Product> products = this.GetProductList(productDAL.GetAllProducts(orderBy));
            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns>List of Products of the passed category ID.</returns>
        public List<Product> GetProductsByCategoryID(int CategoryID)
        {
            List<Product> products = this.GetProductList(productDAL.GetProductsByCategoryID(CategoryID));
            
            return products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns>Product with the passed ProductID</returns>
        /// <exception cref="ProductNotFoundException">thorws exception if Product not found.</exception>
        public Product GetProductByProductID(int ProductID)
        {
            List<Product> products = GetProductList(productDAL.GetProductByProductID(ProductID));
            if (products.Count == 0) throw new ProductNotFoundException();

            return products[0];
        }

        /// <summary>
        /// method will reduces the stock with the purchased quantity.
        /// </summary>
        /// <param name="product"></param>
        public void UpdateStock(Product product)
        {
            productDAL.UpdateStock(product);
        }
        
        private  List<Product> GetProductList(DataSet dsProducts)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow drProduct in dsProducts.Tables[0].Rows)
            {
                Product product = new Product(Convert.ToInt32(drProduct[Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME]), Convert.ToInt32(drProduct[GiftShopConstants.PRODUCT_CATEGORY_COLUMNNAME]));

                foreach (DataColumn dcProduct in dsProducts.Tables[0].Columns)
                {
                    product.AddProperty(dcProduct.ColumnName, drProduct[dcProduct]);
                }
                products.Add(product);
            }
            return products;
        }


        /// <summary>
        /// Method to save Product Information Request (received for this product instance)
        /// </summary>
        /// <param name="SenderInformation">Dictinary object which will be passed to save in custom table.</param>
        public void SaveProductInformationRequest( Dictionary<string, object> productAndSenderInformation)
        {

            productDAL.SaveProductInformationRequest(productAndSenderInformation);
        }


        /// <summary>
        /// Method will combined properties of selected Product with the Sender Information. 
        /// </summary>
        /// <param name="selectedProduct"> Selected product of which information being sent.</param>
        /// <param name="SenderInformation"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetCombinedPropertiesForProductAndSender(Product selectedProduct, Dictionary<string, object> SenderInformation)
        {
            Dictionary<string, object> productInformationRequestProperties = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> property in SenderInformation)
            {
                productInformationRequestProperties.Add(property.Key, property.Value);
            }
            foreach (KeyValuePair<string, object> property in selectedProduct.GetProperties())
            {
                if (productInformationRequestProperties.ContainsKey(property.Key))
                    productInformationRequestProperties[property.Key] = property.Value;
                else
                    productInformationRequestProperties.Add(property.Key, property.Value);
            }
            if (productInformationRequestProperties.ContainsKey(GiftShopConstants.PRODUCTINFORMATIONREQUESTSTABLE_PRODUCTID_COLUMNNAME))
                productInformationRequestProperties[GiftShopConstants.PRODUCTINFORMATIONREQUESTSTABLE_PRODUCTID_COLUMNNAME] = selectedProduct.ProductID;
            else
                productInformationRequestProperties.Add(GiftShopConstants.PRODUCTINFORMATIONREQUESTSTABLE_PRODUCTID_COLUMNNAME, selectedProduct.ProductID);
            return productInformationRequestProperties;
        }
    }
}
