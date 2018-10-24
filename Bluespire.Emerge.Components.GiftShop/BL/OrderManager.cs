using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    /// <summary>
    /// This class will manage the Order. This will handle database operations for Order.
    /// </summary>
    public class OrderManager
    {

        private OrderDAL orderDAL;

        private const string DELIMITER = ",";

        public OrderManager()
        {
            orderDAL = new OrderDAL();
        }

        /// <summary>
        /// Save Order
        /// </summary>
        /// <returns>This will saver the Order </returns>
        /// <exception cref="GiftShopOrderNotSavedException">Throws if order not saved in database</exception>
        /// <exception cref="GiftShopStockNotReducedException">Throws if Stock of any Product from the cart not reduced.</exception>
        public void Save(Order order)
        {


            string productIDForWhichStockNotReduced = string.Empty;

            try
            {
                order.OrderID = orderDAL.Save(order);
            }
            catch (Exception ex)
            {
                throw new GiftShopOrderNotSavedException(ex.ToString());
            }

            productIDForWhichStockNotReduced = this.UpdateStock();

            if (!string.IsNullOrEmpty(productIDForWhichStockNotReduced))
                throw new GiftShopStockNotReducedException(productIDForWhichStockNotReduced);

        }

        private string UpdateStock()
        {
            string productIDsForWhichStockNotReduced = string.Empty;

            ProductManager productManager = new ProductManager();

            foreach (Product product in Cart.GetCartObject().GetProducts())
            {
                try
                {
                    productManager.UpdateStock(product);
                    //product.UpdateStock();
                }
                catch
                {
                    productIDsForWhichStockNotReduced += product.ProductID.ToString() + DELIMITER;
                }
            }

            if (productIDsForWhichStockNotReduced.EndsWith(DELIMITER))
            {
                productIDsForWhichStockNotReduced = productIDsForWhichStockNotReduced.Substring(0, productIDsForWhichStockNotReduced.LastIndexOf(DELIMITER));
            }

            return productIDsForWhichStockNotReduced;
        }

    }
}
