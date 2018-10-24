using System;
using CMS.UIControls;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CMS.FormEngine;
using System.Collections.Generic;
using System.Collections;
using CMS.DataEngine;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Membership;
using CMS.FormEngine.Web.UI;
public partial class CMSModules_Maintenance_Controls_EmergeTableForm : CMSUserControl
{
    #region "Private variables"

    private string editItemPage = string.Empty;
    private DataClassInfo dci = null;
    private string mEditItemPageAdditionalParams = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Custom table class id.
    /// </summary>
    public int CustomTableId
    {
        get
        {
            return customTableForm.CustomTableId;
        }
        set
        {
            customTableForm.CustomTableId = value;
        }
    }

    /// <summary>
    /// Gets or sets URL of the page where whole item is edited.
    /// </summary>
    public string EditItemPage
    {
        get
        {
            if (String.IsNullOrEmpty(editItemPage))
            {
                EmergeLogWriter.WriteError("Emerge Data List", EventCode.EMERGE_PAGENOTFOUND, GetString(Constants.MESSAGE_EDITITEMPAGENOTFOUND));
                throw new ViewItemPageNotFoundException(GetString(Constants.MESSAGE_EDITITEMPAGENOTFOUND));
            }
            return editItemPage;
        }
        set
        {
            editItemPage = value;
        }
    }

    /// <summary>
    /// Custom table item id.
    /// </summary>
    public int ItemId
    {
        get
        {
            return customTableForm.ItemID;
        }
        set
        {
            customTableForm.ItemID = value;
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for edit page.
    /// </summary>
    public string EditItemPageAdditionalParams
    {
        get
        {
            return mEditItemPageAdditionalParams;
        }
        set
        {
            mEditItemPageAdditionalParams = value;
        }
    }

    public CustomTableForm CustomTableForm
    {
        get
        {
            return this.customTableForm;
        }
    }

    #endregion

    #region Events
    public event EventHandler OnAfterSave;
    public event EventHandler OnBeforeSave;
    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CustomTableId > 0)
        {
            // Get form info
            dci = DataClassInfoProvider.GetDataClassInfo(CustomTableId);

            if (dci != null)
            {
                customTableForm.ShowPrivateFields = true;

                if (dci.ClassEditingPageURL != String.Empty)
                {
                    EditItemPage = dci.ClassEditingPageURL;
                }
                customTableForm.OnBeforeDataRetrieval += customTableForm_OnBeforeDataRetrieval;
                customTableForm.OnAfterSave += customTableForm_OnAfterSave;
                customTableForm.OnBeforeSave += customTableForm_OnBeforeSave;
            }
        }

        // Show message about successful save
        if (!RequestHelper.IsPostBack() && QueryHelper.GetBoolean("saved", false))
        {
            customTableForm.ShowChangesSaved();
        }
    }
   

    private void customTableForm_OnBeforeDataRetrieval(object sender, EventArgs e)
    {
        // If editing item
        if (ItemId > 0)
        {
            // Check 'Modify' permission
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Modify") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Modify"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.modify"), dci.ClassName));
                customTableForm.StopProcessing = true;
            }
        }
        else
        {
            // Check 'Create' permission
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Create") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Create"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.create"), dci.ClassName));
                customTableForm.StopProcessing = true;
            }
        }
    }


    private void customTableForm_OnBeforeSave(object sender, EventArgs e)
    {
        // If editing item
        if (ItemId > 0)
        {
            // Check 'Modify' permission
            if (!EmergeCMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Modify") &&
                !EmergeCMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Modify"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.modify"), dci.ClassName), null, null);
                customTableForm.StopProcessing = true;
            }
        }
        else
        {
            // Check 'Create' permission
            if (!EmergeCMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Create") &&
                !EmergeCMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Create"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.create"), dci.ClassName), null, null);
                customTableForm.StopProcessing = true;
            }
        }
        if (OnBeforeSave != null)
        {
            OnBeforeSave(sender, e);
        }
    }


    private void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        if (OnAfterSave != null)
        {
            OnAfterSave(sender, e);
        }
        string param = "&saved=1";

        //Check Form contain custom table selector control and insert value
        if (IsCustomTableSelectorControlAvailable())
        {
            GetAndBulkUpdateCustomTableItems();
        }

        // Include additional parameters if any
        param += (String.IsNullOrEmpty(EditItemPageAdditionalParams) ? String.Empty : "&" + EditItemPageAdditionalParams);

        // Reflect new in query string
        param += (QueryHelper.GetString("new", String.Empty) != String.Empty) ? "&new=1" : String.Empty;

        //Redirect to edit page with saved parameter and new itemId (new item)
        URLHelper.Redirect(URLHelper.GetAbsoluteUrl(EditItemPage) + "?customtableid=" + CustomTableId + "&itemid=" + customTableForm.ItemID + param);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (customTableForm != null)
        {
            customTableForm.SubmitButton.CssClass = "SubmitButton";
        }
    }

    /// <summary>
    /// Check Allow empty false and set '*' after the form field caption.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void customTableForm_OnAfterDataLoad(object sender, EventArgs e)
    {
        List<IDataDefinitionItem> formitems = customTableForm.FormInformation.ItemsList;
        foreach (IField item in formitems)
        {
            FormFieldInfo fieldInfo = ((CMS.FormEngine.FormFieldInfo)(item));
            if (!fieldInfo.AllowEmpty)
            {
                fieldInfo.Caption = fieldInfo.Caption + Constants.FORM_REQUIRED_FIELD_ASTERISK;
            }
        }
    }


    #region [Emerge Code]


    /// <summary>
    /// Check Custom Table Selector(Emerge Group Control) is present or not in form
    /// </summary>
    /// <returns></returns>
    private bool IsCustomTableSelectorControlAvailable()
    {
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        bool isCustomTableAvailable = false;
        if (customTable != null)
        {

            foreach (string columnName in customTableForm.Fields)
            {
                FormFieldInfo ffi = customTableForm.FieldControls[columnName].FieldInfo;
                HiddenField hdnItemIDsForUpdate = ((HiddenField)(customTableForm.FieldControls[columnName].FindControl("hdnItemIdsForUpdate")));

                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower()) && hdnItemIDsForUpdate != null)
                {
                    isCustomTableAvailable = true;
                    break;
                }
            }
        }
        return isCustomTableAvailable;
    }

    private bool IsCustomFileUploaderAvailable()
    {
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        bool isCustomFileuploaderAvailable = false;
        if (customTable != null)
        {
            foreach (string columnName in customTableForm.Fields)
            {
                FormFieldInfo ffi = customTableForm.FieldControls[columnName].FieldInfo;
                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME.ToLower()))
                {
                    isCustomFileuploaderAvailable = true;
                    break;
                }
            }
        }
        return isCustomFileuploaderAvailable;
    }


    private void UpdateFilePath()
    {
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        if (customTable != null)
        {
            foreach (string columnName in customTableForm.Fields)
            {
                FormFieldInfo ffi = customTableForm.FieldControls[columnName].FieldInfo;
                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME.ToLower()))
                {
                    string where = string.Empty;
                    where = " ItemID = " + customTableForm.ItemID.ToString();
                    string uploadPath = ((HiddenField)(customTableForm.FieldControls[columnName].FindControl("hdnFilePath"))).Value;
                    DataSet customTableItems = CustomTableItemProvider.GetItems(customTable.ClassName, where, null);
                    if (!DataHelper.DataSourceIsEmpty(customTableItems))
                    {
                        CustomTableItem updateCustomTableItem = CustomTableItemProvider.GetItem(customTableForm.ItemID, customTable.ClassName);
                        if (updateCustomTableItem != null)
                        {
                            updateCustomTableItem.SetValue(columnName, uploadPath);
                            updateCustomTableItem.Update();
                        }
                    }
                }
            }
        }
    }

    private void GetAndBulkUpdateCustomTableItems()
    {
        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        if (customTable != null)
        {
            foreach (string columnName in customTableForm.Fields)
            {
                FormFieldInfo ffi = customTableForm.FieldControls[columnName].FieldInfo;
                if (ffi != null && ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower()))
                {
                    string where = string.Empty;
                    HiddenField hdnItemIDsForUpdate = ((HiddenField)(customTableForm.FieldControls[columnName].FindControl("hdnItemIdsForUpdate")));
                    string itemIdsForUpdate = string.Empty;
                    if (hdnItemIDsForUpdate != null)
                    {
                        itemIdsForUpdate = hdnItemIDsForUpdate.Value;
                        if (!itemIdsForUpdate.Equals(string.Empty))
                        {
                            itemIdsForUpdate = itemIdsForUpdate.Replace(Constants.MULTI_VALUE_SEPERATOR, Constants.COMMA_SEPERATOR);

                            if (itemIdsForUpdate.Trim().EndsWith(","))
                            {
                                itemIdsForUpdate = itemIdsForUpdate.Substring(0, itemIdsForUpdate.LastIndexOf(","));
                            }
                            where = " ItemID in ( " + itemIdsForUpdate + " )";
                            DataSet customTableItems = CustomTableItemProvider.GetItems(ffi.Settings["SelectorCustomTableName"].ToString(), where, null);
                            if (!DataHelper.DataSourceIsEmpty(customTableItems))
                            {
                                foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
                                {
                                    CustomTableItem modifyCustomTableItem = CustomTableItem.New(ffi.Settings["SelectorCustomTableName"].ToString(), customTableItemDr);
                                    modifyCustomTableItem.SetValue(ffi.Settings["RelationColumnName"].ToString(), customTableForm.ItemID.ToString());
                                    modifyCustomTableItem.Update();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion
}