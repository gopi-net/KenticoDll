using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;

namespace Bluespire.Emerge.Components.GiftShop.DL
{
    public  class CategoryDAL
    {
        string categoryTableCodeName = EmergeStaticHelper.SetSiteName(GiftShopConstants.CATEGORYTABLE_CODENAME);

        public  DataSet GetAllCategories(string OrderBy)
        {
            return CustomTableDataHelper.GetCustomTableItemsByCondition(categoryTableCodeName, string.Empty, OrderBy);
        }
    }
}
