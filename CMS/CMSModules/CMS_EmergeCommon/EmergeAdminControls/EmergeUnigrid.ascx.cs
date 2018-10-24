using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using CMS.UIControls;
using CMS.FormEngine;
using Bluespire.Emerge.Web;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Common.Relations;
using Bluespire.Emerge.CommonService.GridActions;
using Bluespire.Emerge.CommonService;

public partial class CMSFormControls_EmergeAdminControls_EmergeUnigrid : EmergeBaseCMSUserControl
{

    private string mCustomTableClassName = string.Empty;
    private string mAfterActionRedirectTo = string.Empty;
    private string mActionContextMenu = string.Empty;
    private string mGridName = string.Empty;

    private string mWhereCondition = string.Empty;
    private string mZeroRowsText = string.Empty;
    private bool mShowObjectMenu = false;

    #region properties
    /// <summary>
    /// Sets xml config file for Unigrid.
    /// </summary>
    public string GridName
    {
        set { mGridName = value; }
        get { return string.IsNullOrEmpty(mGridName) ? Constants.EMERGE_UNIGRID_CONFIGFILE : mGridName; }
    }

    /// <summary>
    /// Gets data unigrid.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return UniGridControl;
        }
    }


    /// <summary>
    /// Sets WhereCondtion For inner Unigrid.
    /// </summary>
    //public string WhereCondition
    //{
    //    set { mWhereCondition = value; }
    //}

    /// <summary>
    /// Sets information message zero rows For inner Unigrid.
    /// </summary>
    public string ZeroRowsText
    {
        set { mZeroRowsText = value; }
    }


    /// <summary>
    /// Gets or sets the class name of custom table for which data are displayed.
    /// </summary>
    public string CustomTableClassName
    {
        get
        {
            return mCustomTableClassName;
        }
        set
        {
            mCustomTableClassName = value;
        }
    }

    /// <summary>
    /// Gets or sets the redirection Url after Process Action method.
    /// </summary>
    public string AfterActionRedirectTo
    {
        get
        {
            return mAfterActionRedirectTo;
        }
        set
        {
            mAfterActionRedirectTo = value;
        }
    }

    /// <summary>
    /// Sets whether to show object menu (menu containing relationships, export/backup, destroy object, ... functionality) 
    /// </summary>
    public bool ShowObjectMenu
    {
        set
        {
            mShowObjectMenu = value;
        }
    }

    /// <summary>
    /// Sets Action Menu control in header (Export to excel Functionality)
    /// </summary>
    public string ActionContextMenu
    {
        set
        {
            mActionContextMenu = value;
        }
        get
        {
            return string.IsNullOrEmpty(mActionContextMenu) ? Constants.EMERGE_UNIGRID_DEFAULT_ACTIONS_MENU : mActionContextMenu;
        }
    }



    /// <summary>
    /// Sets order by clause for Inner Unigrid control.
    /// </summary>
    public string OrderBy
    {
        set
        {
            UniGridControl.OrderBy = value;
        }
    }

    /// <summary>
    /// Sets Object Type for Inner Unigrid Control.
    /// </summary>
    public string ObjectType
    {
        set { UniGridControl.ObjectType = value; }
    }

    #endregion properties

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


    protected void Page_Load(object sender, EventArgs e)
    {

        UniGridControl.GridName = GridName;
        
        UniGridControl.ZeroRowsText = mZeroRowsText;
        UniGridControl.ShowObjectMenu = mShowObjectMenu;


        UniGridControl.OnAction += UniGridControl_OnAction;
        UniGridControl.OnExternalDataBound += UniGridControl_OnExternalDataBound;
        UniGridControl.OnLoadColumns += UniGridControl_OnLoadColumns;

        UniGridControl.OnBeforeDataReload += UniGridControl_OnBeforeDataReload;
        UniGridControl.OnBeforeFiltering += UniGridControl_OnBeforeFiltering;
        UniGridControl.OnBeforeSorting += UniGridControl_OnBeforeSorting;

        UniGridControl.OnAfterDataReload += UniGridControl_OnAfterDataReload;
        UniGridControl.OnAfterRetrieveData += UniGridControl_OnAfterRetrieveData;
    }
    //protected void Page_PreRender(object sender, EventArgs e)
    //{

    //    UniGridControl.GridActions.ContextMenu = ActionContextMenu;
    //    UniGridControl.ReloadData();

    //}

    void UniGridControl_OnBeforeSorting(object sender, EventArgs e)
    {
        if (OnBeforeSorting != null)
        {
            OnBeforeSorting(sender, e);
        }
    }

    string UniGridControl_OnBeforeFiltering(string whereCondition)
    {
        if (OnBeforeFiltering != null)
        {
            return OnBeforeFiltering(whereCondition);
        }
        return null;
    }

    void UniGridControl_OnBeforeDataReload()
    {
        UniGridControl.GridActions.ContextMenu = ActionContextMenu;
        if (OnBeforeDataReload != null)
        {
            OnBeforeDataReload();
        }

    }

    DataSet UniGridControl_OnAfterRetrieveData(DataSet ds)
    {
        ds = EmergeRelationHelper.GetRelationShipData(ds,CustomTableClassName);

        if (!string.IsNullOrWhiteSpace(CustomTableClassName))
        {
            FormInfo mFormInfo = FormHelper.GetFormInfo(CustomTableClassName, false);

            foreach(DataColumn  column in ds.Tables[0].Columns )
            {
                FormFieldInfo ffi = mFormInfo.GetFormField(column.ColumnName);
                if (null != ffi && null != ffi.Settings["controlname"] && ffi.Settings["controlname"].ToString().ToLower() == Constants.CONTROL_ENCRYPTEDFIELD.ToLower())
                {
                    foreach (DataRow dataRecord in ds.Tables[0].Rows)
                    {
                        if (null != dataRecord[column] && !string.IsNullOrWhiteSpace(dataRecord[column].ToString()))
                            dataRecord[column] = EmergeEncryptionHelper.DecryptData(dataRecord[column].ToString());
                    }
                }

            }
            

        }
        if (OnAfterRetrieveData != null)
        {
            return OnAfterRetrieveData(ds);
        }
        return ds;
    }

    void UniGridControl_OnAfterDataReload()
    {
        if (OnAfterDataReload != null)
        {
            OnAfterDataReload();
        }
    }

    void UniGridControl_OnLoadColumns()
    {
        if (OnLoadColumns != null)
        {
            OnLoadColumns();
        }

    }

    object UniGridControl_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (OnExternalDataBound != null)
        {
            return OnExternalDataBound(sender, sourceName, parameter);
        }
        return null;
    }

    void UniGridControl_OnAction(string actionName, object actionArgument)
    {
        if (CustomTableClassName != string.Empty && AuthorizationService.CheckActionPermission(actionName, CustomTableClassName) == false)
        {
            ShowError(String.Format(GetString("customtable.permissiondenied." + actionName), CustomTableClassName));
            return;
        }
        
        IGridAction GridAction = GridActionFactory.GetActionObject(actionName, EmergeStaticHelper.GridActions);
        bool isBeforeSucceed = true;
        if (OnBeforeAction != null)
        {
            isBeforeSucceed = OnBeforeAction(actionName, actionArgument, GridAction);
        }
        bool isProcessActionSucceed = false;
        if (isBeforeSucceed)
        {
            try
            {
                isProcessActionSucceed = GridAction.ProcessAction(actionArgument, CustomTableClassName, AfterActionRedirectTo);
            }
            catch (ActionNotFeasibleException ex)
            {
                base.OnError(ex);
            }
            catch (InvalidCustomTableNameException ex)
            {
                base.OnError(ex);
            }
        }
        if (isProcessActionSucceed)
        {
            if (OnAfterAction != null)
            {
                OnAfterAction(actionName, actionArgument, GridAction);
            }
        }

    }

    //#region "Private functions"


    ///// <summary>
    ///// Reterive relationship data from CustomTableRelationMaster table
    ///// </summary>
    ///// <param name="ds">grid view dataset</param>
    ///// <returns>dataset with relational data</returns>
    //private DataSet GetRelationShipData(DataSet ds)
    //{
    //    if (!string.IsNullOrEmpty(CustomTableClassName))
    //    {
    //        List<CustomTableRelationMaster> lstRelationMaster = EmergeRelationHelper.GetRelationByForeignTable(CustomTableClassName);
    //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //            {

    //                foreach (var relationMaster in lstRelationMaster)
    //                {
    //                    string columnValue = Convert.ToString(ds.Tables[0].Rows[i][relationMaster.ForeignTableColumnName]);
    //                    if (!string.IsNullOrEmpty(columnValue))
    //                    {
                            
                            
    //                        string actualValue = EmergeRelationHelper.GetPrimaryTableValue(relationMaster.PrimaryTableName, relationMaster.PrimaryPkColumnName, relationMaster.PrimaryDisplayColumnName, columnValue);
    //                        ds.Tables[0].Rows[i][relationMaster.ForeignTableColumnName] = actualValue;
                            
    //                    }

    //                }
    //            }
    //            ds.AcceptChanges();
    //        }
    //    }
    //    return ds;
    //}
    //#endregion


}