using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.GiftShop.Pages;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.GiftShop;
using Bluespire.Emerge.Common.CMS.GlobalHelper;

public partial class CMSModules_CMS_GiftShop_Pages_Orders_Data_List : GiftShopOrderDataListPage
{
   // private TypeInfo ti = null;
    private const string ItemCheckBoxID = "itemCheckBox";
    private const string HeaderCheckBoxID = "Header";
    private const string headerTextForCheckboxTemplateField = "DeliveryStatus_Chk";

    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
        customTableSearch.UniGrid = customTableDataList.UniGrid;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            customTableDataList.OnBeforeDataReload += customTableDataList_OnBeforeDataReload;

            NewItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM  ;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_EditItem.aspx";
            ListPage = GiftShopConstants.PAGEURL_DATA_LIST;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_List.aspx";
            SelectFieldsPage = GiftShopConstants.PAGEURL_DATA_SELECTFIELDS;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = GiftShopConstants.PAGEURL_DATA_EDITITEM;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = GiftShopConstants.PAGEURL_DATA_VIEWITEM;//"~/CMSModules/CMS_GiftShop/Tools/GiftShop_Data_ViewItem.aspx";

            // customTableDataList.UniGrid.RememberState = true;


            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_GiftShop/Pages/Orders_Data_List.xml";
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";
                customTableDataList.UniGrid.FilterFormData = CustomTableDataHelper.New(DataClassInfo.ClassName); 
                customTableDataList.ShowObjectMenu = false;
                // Set custom pages
                if (DataClassInfo.ClassEditingPageURL != String.Empty)
                {
                    customTableDataList.EditItemPage = DataClassInfo.ClassEditingPageURL;
                }
                if (DataClassInfo.ClassNewPageURL != String.Empty)
                {
                    NewItemPage = DataClassInfo.ClassNewPageURL;
                }
                if (DataClassInfo.ClassViewPageUrl != String.Empty)
                {
                    customTableDataList.ViewItemPage = DataClassInfo.ClassViewPageUrl;
                }
                if (CheckForPermissions())
                {
                    plcContent.Visible = false;
                }
                customTableSearch.CustomTableClassInfo = DataClassInfo;
            }

            //ti = CustomTableItemProvider.GetTypeInfo(DataClassInfo.ClassName);

            btnPending.Click += btnPending_Click;
            btnCompleted.Click += btnCompleted_Click;


        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }



    void btnCompleted_Click(object sender, EventArgs e)
    {

        string itemIdstoMarkCompleted = GetMarkedItems();

        try
        {
            if (MarkDeliveryStatusCompleted(customTableDataList.CustomTableClassInfo.ClassName, itemIdstoMarkCompleted))
            {
                customTableDataList.UniGrid.WhereCondition = customTableSearch.Where;

                ShowInformation(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ORDERDATALIST_STATUSCHANGEMESSAGE));

                customTableDataList.UniGrid.GridView.Columns.Clear();
                customTableDataList.UniGrid.ReloadData();
            }
        }
        catch (NoItemSelectedException)
        {
            ShowInformation(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE));
        }
    }

    void btnPending_Click(object sender, EventArgs e)
    {
        try
        {

            string itemIdstoMarkPending = GetMarkedItems();
            if (MarkDeliveryStatusPending(customTableDataList.CustomTableClassInfo.ClassName, itemIdstoMarkPending))
            {

                customTableDataList.UniGrid.WhereCondition = customTableSearch.Where;
                ShowInformation(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_ORDERDATALIST_STATUSCHANGEMESSAGE));

                customTableDataList.UniGrid.GridView.Columns.Clear();
                customTableDataList.UniGrid.ReloadData();
            }
        }
        catch (NoItemSelectedException)
        {
            ShowInformation(EmergeResHelper.GetString(GiftShopConstants.STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE));
        }
    }



    private string GetMarkedItems()
    {
        int rowCounter = 0;

        string itemIds = string.Empty;




        foreach (GridViewRow grv in customTableDataList.UniGrid.GridView.Rows)
        {
            if (grv != null)
            {
                if (null != grv.FindControl(ItemCheckBoxID) && ((CheckBox)grv.FindControl(ItemCheckBoxID)).Checked)
                {
                    itemIds += customTableDataList.UniGrid.ActionsID.ToArray()[rowCounter] + ", ";

                }
                rowCounter++;
            }
        }

        if (!string.IsNullOrWhiteSpace(itemIds))
        {
            if (itemIds.EndsWith(", "))
            {
                itemIds = itemIds.Substring(0, itemIds.LastIndexOf(","));
            }

        }

        return itemIds;
    }

    void customTableDataList_OnBeforeDataReload()
    {

        TemplateField chk = new TemplateField();

        if (!IsTemplateFieldExists(headerTextForCheckboxTemplateField))
        {

            CheckBox headerCheckBox = new CheckBox();
            headerCheckBox.ID = HeaderCheckBoxID;
            headerCheckBox.CssClass = "checkbox_header_select_all";
            chk.HeaderTemplate = new UniGridTemplateField(ListItemType.Header, headerCheckBox);
            chk.HeaderText = headerTextForCheckboxTemplateField;

            CheckBox itemCheckBox = new CheckBox();

            itemCheckBox.CssClass = "checkbox_record_select_single";
            itemCheckBox.ID = ItemCheckBoxID;
            chk.ItemTemplate = new UniGridTemplateField(ListItemType.Item, itemCheckBox);

            customTableDataList.UniGrid.GridView.Columns.Add(chk);
        }
       
    }

    private bool IsTemplateFieldExists(string headerText)
    {
        bool isExists = false;

        foreach (DataControlField dcf in customTableDataList.UniGrid.GridView.Columns)
        {
            if (dcf.HeaderText == headerText && dcf.GetType().Name.ToString().Equals("TemplateField"))
            {
                isExists = true; break;
            }
        }

        return isExists;
    }

}