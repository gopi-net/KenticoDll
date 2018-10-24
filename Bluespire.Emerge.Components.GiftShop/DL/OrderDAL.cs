using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.GiftShop.BL;

namespace Bluespire.Emerge.Components.GiftShop.DL
{
    public class OrderDAL
    {
        string orderTableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.ORDERTABLE_CODENAME);
        string orderDetailTableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.ORDERDETAILTABLE_CODENAME);
        
        public int Save(Order order)
        {
            int orderID = 0;

            Dictionary<string,object> prop = new Dictionary<string,object>();
            if (CustomTableDataHelper.SaveCustomTableItem(orderTableCodeName, ref orderID, order.GetProperties()))
            {
                foreach (Product product in order.Cart.GetProducts())
                {
                    int orderDetailID = 0;
                    product.AddProperty("ProductID",product.ProductID);
                    product.AddProperty("OrderID", orderID);
                    //EmergeLogWriter.WriteError("OrderDal-BeforeSaveToOrderDetailsTable", EventCode.EMERGE_ADD, "OrderID:" + orderID);
                    CustomTableDataHelper.SaveCustomTableItem(orderDetailTableCodeName, ref orderDetailID, product.GetProperties());
                    //EmergeLogWriter.WriteError("OrderDal-AfterSaveToOrderDetailsTable", EventCode.EMERGE_ADD, "OrderID:" + orderID);
                }
                
            }

            return orderID;
        }
    }
}
