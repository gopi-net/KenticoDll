using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.GiftShop.BL;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Components.GiftShop.WebParts
{
    public class ProductListingWebPart : GiftShopWebPart
    {

        public string TransformationName
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("TransformationName"), repGiftShopItems.TransformationName);
            }
            set
            {
                SetValue("TransformationName", value);
                repGiftShopItems.TransformationName = value;
            }
        }

        public int PageSize
        {
            get { return EmergeValidationHelper.GetInteger(GetValue("PageSize"), 12); }
            set { SetValue("PageSize", value); }
        }

        public int GroupSize
        {
            get { return EmergeValidationHelper.GetInteger(GetValue("GroupSize"), 4); }
            set { SetValue("GroupSize", value); }
        }



         const string ID_FOR_CATEGORIES_DROPDOWNLIST = "ddlCategories";
         const string ID_FOR_PRODUCTLIST_REPEATER = "repGiftShopItems";
       
         protected PagedDataSource pagedDataSource;

         LocalizedDropDownList ddlCategories
         { get { return (LocalizedDropDownList)ControlPanel.FindControl(ID_FOR_CATEGORIES_DROPDOWNLIST); } }

        CMSRepeater repGiftShopItems
        { get { return (CMSRepeater)ControlPanel.FindControl(ID_FOR_PRODUCTLIST_REPEATER); } }


       

        protected void SetDefaultTextForCategoryDropdown(string CategoryDropDownDefaultText)
        {
            if (!String.IsNullOrEmpty(CategoryDropDownDefaultText))
            {
                ddlCategories.Items.Insert(0, new ListItem(CategoryDropDownDefaultText, "0"));
            }

            ddlCategories.SelectedIndex = 0;
        }

        protected void BindCategories()
        {
            CategoryManager categoryManager = new CategoryManager();
            ddlCategories.DataSource = categoryManager.GetAllCategories(GiftShopConstants.CATEGORY_CATEGORYNAME_COLUMNNAME);
            ddlCategories.DataBind();
        }

        protected void SetupProductRepeater()
        {
            repGiftShopItems.TransformationName = TransformationName;

            repGiftShopItems.ZeroRowsText = ResHelper.GetString(GiftShopConstants.STRINGCODE_NOPRODUCTFOUNDTEXT_MESSAGE);
            pagedDataSource = new PagedDataSource();
            pagedDataSource.PageSize = repGiftShopItems.PageSize = PageSize;
            pagedDataSource.DataSource = GetProducts(ValidationHelper.GetInteger(ddlCategories.SelectedValue, 0));
            pagedDataSource.AllowPaging = true;


            repGiftShopItems.DataSource = pagedDataSource;
            repGiftShopItems.DataBind();
        }

        private List<Product> GetProducts(int categoryID)
        {
            if (null == EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY))
            {
                ProductManager productManager = new ProductManager();
                EmergeSessionHelper.SetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY, productManager.GetAllProducts(GiftShopConstants.PRODUCT_PRODUCTNAME_COLUMNNAME));

            }
            if (categoryID == 0)
            {
                return ((List<Product>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY));
            }
            else
            {
                return ((List<Product>)EmergeSessionHelper.GetValue(GiftShopConstants.GIFT_SHOP_PRODUCTS_SESSIONKEY)).FindAll(x => x.CategoryID == categoryID);
            }
        }
    }
}
