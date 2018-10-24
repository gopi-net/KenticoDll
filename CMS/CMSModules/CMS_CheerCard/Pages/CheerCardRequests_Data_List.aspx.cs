using System;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.Components.CheerCard.Pages;
using Bluespire.Emerge.CommonService;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using System.Data;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.CustomTables;

using System.Linq;
using CMS.Base.Web.UI;

public partial class CMSModules_CMS_CheerCard_Pages_CheerCardRequests_Data_List : CheerCardRequestsDataPage
{
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
            customTableDataList.OnExternalDataBound+=customTableDataList_OnExternalDataBound;
            NewItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM;
            ListPage = CheerCardConstants.PAGEURL_LIST_CHEERCARD;
            SelectFieldsPage = CheerCardConstants.PAGEURL_DATA_SELECTFIELDS; 

            customTableDataList.EditItemPage = CheerCardConstants.PAGEURL_DATA_EDITITEM; 
            customTableDataList.ViewItemPage = CheerCardConstants.PAGEURL_DATA_VIEWITEM; 
            ScriptHelper.RegisterDialogScript(Page);

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PrintCheecardFunction", ScriptHelper.GetScript(
                "function PrintCheerCard(itemId,className) { " +
                " modalDialog ('" + ResolveUrl(CheerCardConstants.PAGEURL_CHEERCARDREQUEST_PRINTITEM) + "?ItemID=' + itemId + '&CustomTableName=' + className,'PrintItem',600,600); } "
                ));

           

            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_CheerCard/Pages/CheerCardRequests_Data_List.xml";
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

           // btnPending.Click += btnPending_Click;
            btnDelivered.Click += btnDelivered_Click;
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
    }

    void btnDelivered_Click(object sender, EventArgs e)
    {
        string itemIdstoMarkCompleted = GetMarkedItems();

        try
        {
            if (MarkDeliveryStatusDelivered(customTableDataList.CustomTableClassInfo.ClassName, itemIdstoMarkCompleted))
            {
                customTableDataList.UniGrid.WhereCondition = customTableSearch.Where;

                ShowInformation(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_CHEERCARDREQUEST_STATUSCHANGEMESSAGE));

                customTableDataList.UniGrid.GridView.Columns.Clear();
                customTableDataList.UniGrid.ReloadData();
            }
        }
        catch (NoItemSelectedException)
        {
            ShowInformation(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE));
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
                ShowInformation(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_CHEERCARDREQUEST_STATUSCHANGEMESSAGE));

                customTableDataList.UniGrid.GridView.Columns.Clear();
                customTableDataList.UniGrid.ReloadData();
            }
        }
        catch (NoItemSelectedException)
        {
            ShowInformation(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE));
        }
    }


    object customTableDataList_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string source = sourceName.ToLowerCSafe();
        // Get button and grid view row
        CMSGridActionButton button = sender as CMSGridActionButton;
        GridViewRow grv = parameter as GridViewRow;

        if (grv != null)
        {
            DataRowView drv = grv.DataItem as DataRowView;
            ObjectTypeInfo ti = CustomTableItemProvider.GetTypeInfo(customTableDataList.CustomTableClassInfo.ClassName);
            // Hide Move Up/Down buttons when there is no Order field
            int itemID = Convert.ToInt32(drv[ti.IDColumn]);
            
            switch (source)
            {

                case "printcheercard":
                    if ((button != null) && (drv != null))
                    {
                        
                        button.OnClientClick = "PrintCheerCard(" + drv[ti.IDColumn] +  ", '" + CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS + "' ); return false;";
                    }
                    break;

            }
        }

        return parameter;
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

    private void customTableDataList_OnBeforeDataReload()
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