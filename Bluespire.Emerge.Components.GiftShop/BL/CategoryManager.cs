using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    /// <summary>
    /// Class to communicate with the data access layer.
    /// </summary>
    public class CategoryManager
    {
        /// <summary>
        /// Method to get All Categories.
        /// </summary>
        /// <param name="OrderBy"></param>
        /// <returns>Category List.</returns>
        public List<Category> GetAllCategories(string OrderBy)
        {

            CategoryDAL categoryDAL = new CategoryDAL();
            DataSet dsCategories = categoryDAL.GetAllCategories(OrderBy);

            return this.GetCategoryList(dsCategories);
        }

        private List<Category> GetCategoryList(DataSet dsCategories)
        {
            List<Category> categories = new List<Category>();
            foreach (DataRow drCategory in dsCategories.Tables[0].Rows)
            {
                Category category = new Category(Convert.ToInt32(drCategory[Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME].ToString()), drCategory[GiftShopConstants.CATEGORY_CATEGORYNAME_COLUMNNAME].ToString());

                foreach (DataColumn dcCategory in dsCategories.Tables[0].Columns)
                {
                    category.AddProperty(dcCategory.ColumnName, drCategory[dcCategory].ToString());
                }
                categories.Add(category);
            }
            return categories;
        }
    }
}
