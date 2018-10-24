using System;
using System.Web.UI.WebControls;
using CMS.Base;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.UIControls;

using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Bluespire.Emerge.Common.CMS.GlobalHelper;


// Check permissions
[Security(Resource = "CMS.ContactManagement", Permission = "ModifyContacts")]
[CheckLicence(FeatureEnum.FullContactManagement)]
[SaveAction(0)]
public partial class CMSModules_CMS_EmergeCustomTables_Tools_CustomTable_Edit_OnlineMarketing : CMSCustomTablesToolsPage
{
    #region "Variables"

    private CMSUserControl mapControl = null;
    private int _customTableId;

    public int CustomTableId
    {
        get { return QueryHelper.GetInteger("objectid", 0); }
        set { _customTableId = value; }
    }
    protected int customTableItemID = 0;
    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

        DataClassInfo dci = null;

        // Read data only if user is site manager global admin or table is bound to current site
        if (CurrentUser.IsGlobalAdministrator || (ClassSiteInfoProvider.GetClassSiteInfo(CustomTableId, EmergeCMSContext.CurrentSiteID) != null))
        {
            // Get CustomTable class
            dci = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        }

        // Set edited object
        EditedObject = dci;

        if (dci == null)
        {
            return;
        }

        // Load mapping dialog control and initialize it
        plcMapping.Controls.Clear();
        DataSet ds = GetCurrentConfiguration();
        mapControl = (CMSUserControl)Page.LoadControl("~/CMSModules/CMS_EmergeCustomTables/Controls/MappingDialogCustomTable.ascx");
        if (mapControl != null)
        {
            mapControl.ID = "ctrlMapping";
            mapControl.SetValue("classname", dci.ClassName);
            if (!EmergeDataHelper.DataSourceIsEmpty(ds))
                mapControl.SetValue("allowoverwrite", ds.Tables[0].Rows[0]["AllowOverwrite"]);
            plcMapping.Controls.Add(mapControl);
        }
        if (!RequestHelper.IsPostBack())
        {
            // Initialize checkbox value and mapping dialog visibility
            SetCurrentConfiguration();
        }

    }

    #endregion


    #region "Event handlers"

    protected void chkLogActivity_CheckedChanged(object sender, EventArgs e)
    {
        // Show/hide mapping dialog
        plcMapping.Visible = chkLogActivity.Checked;
    }

    /// <summary>
    /// Actions handler - saves the changes.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Update the form object and its class        
        if (mapControl != null)
        {
            if (plcMapping.Visible)
            {
                SaveCurrentConfiguration();
            }

            // Update the Table

            SaveCurrentConfiguration();
            // Show save information
            ShowChangesSaved();
        }
    }

    #endregion
    private DataSet GetCurrentConfiguration()
    {
        return CustomTableDataHelper.GetCustomTableItemsByCondition("customtable.Emerge_OM_CustomTableContactMapping", "CustomTableID=" + CustomTableId, string.Empty);
    }
    private void SetCurrentConfiguration()
    {
        DataSet ds = GetCurrentConfiguration();
        if (!EmergeDataHelper.DataSourceIsEmpty(ds))
        {
            plcMapping.Visible = chkLogActivity.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLoggingEnabled"]);
            customTableItemID = Convert.ToInt32(ds.Tables[0].Rows[0]["ItemID"]);
        }
    }
    private int GetCurrentItemId()
    {
        DataSet ds = GetCurrentConfiguration();
        if (!EmergeDataHelper.DataSourceIsEmpty(ds))
        {
            return Convert.ToInt32(ds.Tables[0].Rows[0]["ItemID"]);
        }
        return 0;
    }
    private void SaveCurrentConfiguration()
    {
        int itemId = GetCurrentItemId();
        IDictionary<string, object> data = new Dictionary<string, object>();
        data.Add("CustomTableId", CustomTableId);
        data.Add("ColumnMappings", ValidationHelper.GetString(mapControl.GetValue("mappingdefinition"), string.Empty));
        data.Add("ControlTypes", ValidationHelper.GetString(mapControl.GetValue("controltypes"), string.Empty));
        data.Add("AllowOverwrite", ValidationHelper.GetBoolean(mapControl.GetValue("allowoverwrite"), false));
        data.Add("IsLoggingEnabled", plcMapping.Visible);
        CustomTableDataHelper.SaveCustomTableItem("customtable.Emerge_OM_CustomTableContactMapping", ref itemId, data);
    }




}