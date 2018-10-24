using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.UIControls.UniGridConfig;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web;
using Action = CMS.UIControls.UniGridConfig.Action;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.GridActions;
using CMS.DataEngine;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Membership;
using CMS.Base;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;

public partial class CMSModules_Maintenance_Controls_EmergeDataList : EmergeBaseCMSUserControl
{
    #region "Private & protected variables"

    protected string editToolTip = String.Empty;
    protected string deleteToolTip = String.Empty;
    protected string viewToolTip = String.Empty;
    protected string upToolTip = String.Empty;
    protected string downToolTip = String.Empty;

    // Default pages
    private string mEditItemPage = string.Empty;
    private string mViewItemPage = string.Empty;
    private string gridName = string.Empty;
    private string mEditItemPageAdditionalParams = null;
    private string mViewItemPageAdditionalParams = null;

    protected DataSet ds = null;
    private DataClassInfo mCustomTableClassInfo = null;
    private FormInfo mFormInfo = null;
    private ObjectTypeInfo ti = null;
        
    private readonly CustomTableItemProvider ctProvider = new CustomTableItemProvider();

    #endregion


    #region events

    
    public event OnExternalDataBoundHandler OnExternalDataBound;
    public event OnLoadColumnsHandler OnLoadColumns;


    public event OnBeforeDataReloadHandler OnBeforeDataReload;
    public event OnBeforeFilteringHandler OnBeforeFiltering;
    public event OnBeforeSortingHandler OnBeforeSorting;

    public event OnAfterDataReloadHandler OnAfterDataReload;
    public event OnAfterRetrieveDataHandler OnAfterRetrieveData;
    public event OnBeforeAction OnBeforeAction;
    public event OnAfterAction OnAfterAction;

    

    #endregion events



    #region "Properties"

    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }


    /// <summary>
    /// Gets or sets URL of the page where item is edited.
    /// </summary>
    public string EditItemPage
    {
        get
        {
            if (String.IsNullOrEmpty(mEditItemPage))
            {
                EmergeLogWriter.WriteError("Emerge Data List", EventCode.EMERGE_PAGENOTFOUND, GetString(Constants.MESSAGE_EDITITEMPAGENOTFOUND));
                throw new EditItemPageNotFoundException(GetString(Constants.MESSAGE_EDITITEMPAGENOTFOUND));
            }
            return mEditItemPage;
        }
        set
        {
            mEditItemPage = value;
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for Edit page.
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


    /// <summary>
    /// Gets or sets URL of the page where whole item is displayed.
    /// </summary>
    public string ViewItemPage
    {
        get
        {
            if (String.IsNullOrEmpty(mEditItemPage))
            {
                EmergeLogWriter.WriteError("Emerge Data List", EventCode.EMERGE_PAGENOTFOUND, GetString(Constants.MESSAGE_VIEWITEMPAGENOTFOUND));
                throw new ViewItemPageNotFoundException(GetString(Constants.MESSAGE_VIEWITEMPAGENOTFOUND));
            }
            return mViewItemPage;
        }
        set
        {
            mViewItemPage = value;
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for View page.
    /// </summary>
    public string ViewItemPageAdditionalParams
    {
        get
        {
            return mViewItemPageAdditionalParams;
        }
        set
        {
            mViewItemPageAdditionalParams = value;
        }
    }


    /// <summary>
    /// Gets or sets the class info of custom table which data are displayed.
    /// </summary>
    public DataClassInfo CustomTableClassInfo
    {
        get
        {
            return mCustomTableClassInfo;
        }
        set
        {
            mCustomTableClassInfo = value;
            EmergeGridControl.CustomTableClassName = mCustomTableClassInfo.ClassName;
        }
    }


    /// <summary>
    /// Gets or sets the form info.
    /// </summary>
    public FormInfo FormInfo
    {
        get
        {
            if (mFormInfo == null)
            {
                if (CustomTableClassInfo != null)
                {
                    mFormInfo = FormHelper.GetFormInfo(CustomTableClassInfo.ClassName, true);
                }
            }
            return mFormInfo;
        }
    }


    /// <summary>
    /// Determines whether custom table has ItemOrder field.
    /// </summary>
    public bool HasItemOrderField
    {
        get
        {
            if (FormInfo != null)
            {
                return (FormInfo.FieldExists("ItemOrder") && FormInfo.FieldExists("ItemID"));
            }
            else
            {
                // If form info is not available assume ItemOrder is not present to prevent further exceptions
                return false;
            }
        }
    }

    /// <summary>
    /// Determines whether custom table has Status field.
    /// </summary>
    public bool HasStatusColumn
    {
        get
        {
            if (FormInfo != null)
            {
                return (FormInfo.FieldExists(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME));
            }
            else
            {
                // If form info is not available assume ItemOrder is not present to prevent further exceptions
                return false;
            }
        }
    }
    //


    /// <summary>
    /// Gets custom table data unigrid.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return EmergeGridControl.UniGrid; 
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return EmergeGridControl.UniGrid.IsLiveSite;
        }
        set
        {
            plcMess.IsLiveSite = value;
            
        }
    }

    /// <summary>
    /// Gets or sets the xml config file path.
    /// </summary>
    public string GridName
    {
        get
        {
            return gridName.Equals(string.Empty) ? Constants.EMERGE_UNIGRID_CONFIGFILE : gridName;
        }
        set
        {
            gridName = value;
        }
    }

    /// <summary>
    /// sets the visibility for Object Menu.
    /// </summary>
    public bool ShowObjectMenu
    {
        set
        {
            EmergeGridControl.ShowObjectMenu = value;
        }
    }

    //public string WhereCondition
    //{
    //    set { EmergeGridControl.WhereCondition = value; }
    //}

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!CheckPermissions("Read"))
            return;
        // Register Javascripts
        ScriptHelper.RegisterDialogScript(Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DeleteEditView", ScriptHelper.GetScript(
            "var deleteConfirmation = ''; " +
            "function DeleteConfirm() { return confirm(deleteConfirmation); } " +
            "function EditItem(customtableid, itemId) { " +
            "  document.location.replace('" + ResolveUrl(EditItemPage) + "?" +
            (String.IsNullOrEmpty(mEditItemPageAdditionalParams) ? String.Empty : mEditItemPageAdditionalParams + "&") + "customtableid=' + customtableid + '&itemId=' + itemId); } " +
            "function ViewItem(customtableid, itemId) { " +
            "  modalDialog('" + ResolveUrl(ViewItemPage) + "?" +
            (String.IsNullOrEmpty(mViewItemPageAdditionalParams) ? String.Empty : mViewItemPageAdditionalParams + "&") + "customtableid=' + customtableid + '&itemId=' + itemId,'ViewItem',600,600); } "));

        // Buttons' tooltips
        editToolTip = GetString("general.edit");
        deleteToolTip = GetString("general.delete");
        viewToolTip = GetString("general.view");
        upToolTip = GetString("general.up");
        downToolTip = GetString("general.down");

        // Delete confirmation
        ltlScript.Text = ScriptHelper.GetScript("deleteConfirmation = '" + GetString("customtable.data.DeleteConfirmation") + "';");
        

        EmergeGridControl.GridName = GridName;
        EmergeGridControl.IsLiveSite = IsLiveSite;
        //EmergeGridControl.WhereCondition = EmergeGridControl.UniGrid.WhereCondition;
        EmergeGridControl.ObjectType = CustomTableItemProvider.GetObjectType(CustomTableClassInfo.ClassName);
        ti = CustomTableItemProvider.GetTypeInfo(CustomTableClassInfo.ClassName);
        
        if (HasItemOrderField)
        {
            EmergeGridControl.OrderBy = "ItemOrder ASC";
        }
        else
        {
            EmergeGridControl.OrderBy = ti.IDColumn;
        }
                  
        EmergeGridControl.OnLoadColumns += EmergeGridControl_OnLoadColumns;
        EmergeGridControl.OnExternalDataBound += EmergeGridControl_OnExternalDataBound;
        EmergeGridControl.OnBeforeAction += EmergeGridControl_OnBeforeAction;
        EmergeGridControl.OnAfterAction += EmergeGridControl_OnAfterAction;
        EmergeGridControl.OnBeforeDataReload += EmergeGridControl_OnBeforeDataReload;
        EmergeGridControl.OnAfterDataReload += EmergeGridControl_OnAfterDataReload;
        EmergeGridControl.OnAfterRetrieveData += EmergeGridControl_OnAfterRetrieveData;
       
        EmergeGridControl.UniGrid.Pager.UniPager.OnPageChanged += UniPager_OnPageChanged;
        EmergeGridControl.UniGrid.Pager.PageSizeDropdown.SelectedIndexChanged += PageSizeDropdown_SelectedIndexChanged;
        EmergeGridControl.UniGrid.OnBeforeSorting += UniGrid_OnBeforeSorting;

    

    }

    DataSet EmergeGridControl_OnAfterRetrieveData(DataSet ds)
    {
        if (OnAfterRetrieveData != null)
        {
            return OnAfterRetrieveData(ds);
        }
        return ds;
    }



    void EmergeGridControl_OnAfterDataReload()
        {
        if (OnAfterDataReload != null)
        {
            OnAfterDataReload();
        }
    }

    void EmergeGridControl_OnBeforeDataReload()
        {
        if (OnBeforeDataReload != null)
        {
            OnBeforeDataReload();
        }

    }

    bool EmergeGridControl_OnAfterAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        if (OnAfterAction != null)
        {
            return OnAfterAction(actionName, actionArgument, actionObject);
        }
        return true;
    }

    bool EmergeGridControl_OnBeforeAction(string actionName, object actionArgument, IGridAction actionObject)
    {
        if (OnBeforeAction != null)
        {
            return OnBeforeAction(actionName, actionArgument, actionObject);
        }
        return true;
    }

    

   

    #endregion


    private string GetUserName(int userId)
    {
        string userName = null;

        if (userId != 0)
        {
            string key = "UserInfo_" + userId;
            // Get user name from request cache
            userName = RequestStockHelper.GetItem(key) as string;
            if (userName == null)
            {
                // Get user information
                DataSet users = UserInfoProvider.GetFullUsers("UserID=" + userId, null, 1, "UserName, FullName");
                if (!DataHelper.DataSourceIsEmpty(users))
                {
                    DataRow dr = users.Tables[0].Rows[0];
                    string usrName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "UserName"), null);
                    string usrFullName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "FullName"), null);
                    userName = Functions.GetFormattedUserName(usrName, usrFullName, IsLiveSite);
                    // Store to request cache
                    RequestStockHelper.Add(key, userName);
                }
            }
        }

        return userName;
    }


    #region Emerge Grid Events

    protected object EmergeGridControl_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string source = sourceName.ToLowerCSafe();
        // Get button and grid view row
        CMSGridActionButton button = sender as CMSGridActionButton;
        
        GridViewRow grv = parameter as GridViewRow;

        if (grv != null)
        {
            DataRowView drv = grv.DataItem as DataRowView;

            // Hide Move Up/Down buttons when there is no Order field
            switch (source)
            {
                case "edit":
                    if ((button != null) && (drv != null))
                    {
                        // Add edit script
                        button.OnClientClick = "EditItem(" + CustomTableClassInfo.ClassID + ", " + drv[ti.IDColumn] + "); return false;";
                    }
                    break;

                case "view":
                    if ((button != null) && (drv != null))
                    {
                        // Add view script
                        button.OnClientClick = "ViewItem(" + CustomTableClassInfo.ClassID + ", " + drv[ti.IDColumn] + "); return false;";
                    }
                    break;
                case "moveup":
                case "movedown":
                    if (!HasItemOrderField && (button != null))
                    {
                        // Hide button
                        button.Visible = false;
                    }
                    break;

                case "deactivate":
                    if ((button != null) && (drv != null))
                    {
                        if (drv.Row.Table.Columns.Contains(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME))
                            button.Visible = ValidationHelper.GetBoolean(drv.Row[Constants.CUSTOM_TABLE_STATUS_COLUMNNAME], true);
                        else
                            button.Visible = false;

                    }
                    break;
                case "activate":
                    if ((button != null) && (drv != null))
                    {

                        if (drv.Row.Table.Columns.Contains(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME))
                        {
                            bool isActiveRow = ValidationHelper.GetBoolean(drv.Row[Constants.CUSTOM_TABLE_STATUS_COLUMNNAME], true);
                            button.Visible = !isActiveRow;
                        }
                        else
                        {
                            button.Visible = false;
                        }

                    }
                    break;
            }
        }
        else
        {
            switch (source)
            {
                case "itemcreatedby":
                case "itemmodifiedby":
                    int userId = ValidationHelper.GetInteger(parameter, 0);
                    return HTMLHelper.HTMLEncode(GetUserName(userId));

                default:
                    return HTMLHelper.HTMLEncode(parameter.ToString());
            }
        }
        if (OnExternalDataBound != null)
        {
            return OnExternalDataBound(sender, sourceName, parameter);
        }

        return parameter;
    }

    protected void EmergeGridControl_OnLoadColumns()
    {
        if (OnLoadColumns != null)
        {
            OnLoadColumns(); return;
        }

        if (CustomTableClassInfo != null)
        {
            // Update the actions command argument
            foreach (Action action in EmergeGridControl.UniGrid.GridActions.Actions)
            {
                action.CommandArgument = ti.IDColumn;
            }

            string columnNames = null;
            List<string> columnList = null;

            string columns = CustomTableClassInfo.ClassShowColumns;
            bool isStatusColumnInSelectedColumns = false;
            if (string.IsNullOrEmpty(columns))
            {
                columnList = new List<string>();
                columnList.AddRange(GetExistingColumns(false).Take(5));

                // Show info message
                ShowInformation(GetString("customtable.columnscount.default"));
            }
            else
            {
                // Get existing columns names
                List<string> existingColumns = GetExistingColumns(true);

                // Get selected columns
                List<string> selectedColumns = GetSelectedColumns(columns);
               isStatusColumnInSelectedColumns = selectedColumns.Any(x => x == Constants.CUSTOM_TABLE_STATUS_COLUMNNAME);
                columnList = new List<string>();
                StringBuilder sb = new StringBuilder();

                // Remove non-existing columns
                foreach (string col in selectedColumns)
                {
                    int index = existingColumns.BinarySearch(col, StringComparer.InvariantCultureIgnoreCase);
                    if (index >= 0)
                    {
                        string colName = existingColumns[index];
                        columnList.Add(colName);
                        sb.Append(",[", colName, "]");
                    }
                }

                // Ensure item order column
                selectedColumns.Sort();
                if ((selectedColumns.BinarySearch("ItemOrder", StringComparer.InvariantCultureIgnoreCase) < 0) && HasItemOrderField)
                {
                    sb.Append(",[ItemOrder]");
                }

                // Ensure itemid column
                if (selectedColumns.BinarySearch(ti.IDColumn, StringComparer.InvariantCultureIgnoreCase) < 0)
                {
                    sb.Insert(0, ",[" + ti.IDColumn + "]");
                }

                // Ensure Status Column
                selectedColumns.Sort();
                if (selectedColumns.BinarySearch(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME, StringComparer.InvariantCultureIgnoreCase) < 0 && HasStatusColumn)
                {
                    sb.Append(",[" + Constants.CUSTOM_TABLE_STATUS_COLUMNNAME + "]");
                }

                columnNames = sb.ToString().TrimStart(',');
            }

            // Loop trough all columns
            for (int i = 0; i < columnList.Count; i++)
            {
                string column = columnList[i];

                // Get field caption
                FormFieldInfo ffi = FormInfo.GetFormField(column);
                string fieldCaption = string.Empty;
                if (ffi == null)
                {
                    fieldCaption = column;
                }
                else
                {
                    fieldCaption = (ffi.Caption == string.Empty) ? column : ResHelper.LocalizeString(ffi.Caption);
                }


                Column columnDefinition = new Column
                {
                    Caption = fieldCaption,
                    Source = column,
                    ExternalSourceName = column,
                    AllowSorting = true,
                    Wrap = false
                };

                if (i == columnList.Count - 1)
                {
                    // Stretch last column
                    columnDefinition.Width = "100%";
                }

                if (!isStatusColumnInSelectedColumns)
                {
                    if (columnDefinition.Source.Equals(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME))
                        columnDefinition.Visible = false;
                }
                EmergeGridControl.UniGrid.GridColumns.Columns.Add(columnDefinition);
            }

            // Set column names
            EmergeGridControl.UniGrid.Columns = columnNames;
        }
    }

    #endregion Emerge Grid Events

    #region "Methods"

    /// <summary>
    /// Checks the specified permission.
    /// </summary>
    private bool CheckPermissions(string permissionName)
    {
        // Check 'Modify' permission
        if (!EmergeCMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", permissionName) &&
            !EmergeCMSContext.CurrentUser.IsAuthorizedPerClassName(CustomTableClassInfo.ClassName, permissionName))
        {
            ShowError(String.Format(GetString("customtable.permissiondenied." + permissionName), CustomTableClassInfo.ClassName));
            return false;
        }
        return true;
    }


    /// <summary>
    /// Gets existing columns from form info
    /// </summary>
    /// <param name="sort">Indicates if the columns should be sorted</param>
    private List<string> GetExistingColumns(bool sort)
    {
        var existingColumns = FormInfo.GetColumnNames();
        if (sort)
        {
            existingColumns.Sort(StringComparer.InvariantCultureIgnoreCase);
        }

        return existingColumns;
    }


    /// <summary>
    /// Gets list of selected columns
    /// </summary>
    private static List<string> GetSelectedColumns(string columns)
    {
        return columns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    void PageSizeDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetGridData();
    }

    void UniPager_OnPageChanged(object sender, int pageNumber)
    {
        SetGridData();
    }

    void UniGrid_OnBeforeSorting(object sender, EventArgs e)
    {
        const string DescendingOrder = " DESC";
        const string AscendingOrder = " ASC";

        UniGrid.SortDirect = ((GridViewSortEventArgs)e).SortExpression + " " + UniGrid.SortDirect.Split(' ')[1].ToString();
        UniGrid.SortDirect = UniGrid.SortDirect.ToLower().Contains(DescendingOrder.ToLower()) ? UniGrid.SortDirect.Replace(DescendingOrder, AscendingOrder) : (UniGrid.SortDirect.ToLower().Contains(AscendingOrder.ToLower()) ? UniGrid.SortDirect.Replace(AscendingOrder, DescendingOrder) : UniGrid.SortDirect + AscendingOrder);
        SetGridData();

    }

    /// <summary>
    /// Function to set grid data as per search criteria.
    /// </summary>
    public void SetGridData()
    {

        if (UniGrid.ObjectType == null) return;

        try
        {
            UniGrid.GridView.Columns.Clear();
            UniGrid.ReloadData();

        }

        catch (Exception)
        {

            throw;
        }


        UniGrid.ZeroRowsText = ResHelper.GetString("Emerge.EmergeUniGrid.SearchResult.ZeroText");
    }
    #endregion
}