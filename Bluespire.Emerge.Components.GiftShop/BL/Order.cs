using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    public class Order
    {
        # region "Variables"
        private Dictionary<string, object> properties = new Dictionary<string, object>();
        private ProductManager productManager;
        Cart cart;
        public int OrderID;
        # endregion

        # region "properties"
        public Cart Cart { get { return cart; } }
        #endregion

        #region constructor
        public Order(Cart cartDetails)
        {
            this.cart = cartDetails;
            this.productManager = new ProductManager();
            this.OrderID = 0;
        }
        #endregion constructor

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
        /// 
        /// </summary>
        /// <returns>Dictionary containing Properties.</returns>
        public Dictionary<string, object> GetProperties()
        {
            return properties;
        }

        /// <summary>
        /// This method will ensures that all the default properties like tax, total amount available in the order's properties (so that they will be store in the order's custom table)
        /// </summary>
        public void EnsureDefaultProperties()
        {
            double totalAmount = cart.GetTotalAmountForProductsInCart();
            double totalTaxAmount = cart.GetTotalTaxAmountForProductsInCart();
            this.AddProperty("TaxPercentage", TaxCalculator.GetTaxPercentage());
            this.AddProperty("TotalAmount", totalAmount);
            this.AddProperty("TotalTaxAmount", totalTaxAmount);
            this.AddProperty("GrandTotal", Math.Round(totalAmount + totalTaxAmount, 2));
            this.AddProperty("DeliveryStatus", GiftShopConstants.DeliveryStatus.Pending.ToString());
        }

        /// <summary>
        /// if in case order not saved in database, then this html of the order will be passed to admin user through email.
        /// </summary>
        /// <returns></returns>
        public string ToHtml()
        {
            StringBuilder resultHtml = new StringBuilder();

            resultHtml.Append("<table>");
            foreach (KeyValuePair<string, object> property in properties)
            {
                resultHtml.Append("<tr>");
                resultHtml.Append("<td>");
                resultHtml.Append(property.Key);
                resultHtml.Append("</td>");

                resultHtml.Append("<td>");
                resultHtml.Append(property.Value);
                resultHtml.Append("</td>");

                resultHtml.Append("</tr>");
            }
            resultHtml.Append("<tr>");
            resultHtml.Append("<td colspan='2'>");
            resultHtml.Append("<table>");


            resultHtml.Append("<tr>");
            foreach (KeyValuePair<string, object> productProperty in Cart.GetProducts().First().GetProperties())
            {
                resultHtml.Append("<th>");
                resultHtml.Append(productProperty.Key);
                resultHtml.Append("</th>");
            }
            resultHtml.Append("</tr>");

            foreach (Product product in Cart.GetProducts())
            {
                resultHtml.Append("<tr>");

                foreach (KeyValuePair<string, object> productProperty in product.GetProperties())
                {
                    resultHtml.Append("<td>");
                    resultHtml.Append(productProperty.Value);
                    resultHtml.Append("</td>");
                }
                resultHtml.Append("</tr>");
            }

            resultHtml.Append("</table>");
            resultHtml.Append("</td>");
            resultHtml.Append("</tr>");

            resultHtml.Append("</table>");

            return resultHtml.ToString();
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
