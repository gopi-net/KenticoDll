using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using CMS.DataEngine;
using System.Collections;
using System.Data;
using System.ComponentModel;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base;
using CMS.CustomTables;
using CMS.Base.Web.UI;

public partial class CMSFormControls_EmergeFormControls_GroupCustomTable : EmergeBaseFormEngineUserControl
{

    #region properties

    /// <summary>
    /// Gets or sets the value entered into the field, a hexadecimal color code in this case.
    /// </summary>
    public override Object Value
    {
        get
        {
            if (string.IsNullOrEmpty(hdnItemIdsForUpdate.Value)) return string.Empty;
            else
                return hdnItemIdsForUpdate.Value;
        }
        set { hdnItemIdsForUpdate.Value = Convert.ToString(value); }
    }
    public override bool IsValid()
    {
        if (!this.Visible)
            return true;
        if (this.AllowEmpty)
            return true;
        if (String.IsNullOrEmpty(this.hdnItemIdsForUpdate.Value))
        {
            ValidationError = ResHelper.GetString("BasicForm.ErrorEmptyValue");
            return false;
        }
        return true;
    }
    public bool AllowEmpty
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowEmpty"), false);
        }
        set
        {
            SetValue("AllowEmpty", value);
        }
    }

    /// <summary>
    /// Property used to access the Custom Table name parameter of the form control.
    /// </summary>
    /// 
    public string SelectorCustomTableName
    {
        get
        {
            return ValidationHelper.GetString(GetValue(Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY), string.Empty);
        }
        set
        {
            SetValue(Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY, value);
        }
    }


    /// <summary>
    /// Property used to access the Relationship Column name parameter of the form control.
    /// </summary>
    /// 
    public string RelationColumnName
    {
        get
        {
            return ValidationHelper.GetString(GetValue(Constants.GROUP_CONTROL_RELATIONCOLUMNNAME_PROPERTY), string.Empty);
        }
        set
        {
            SetValue(Constants.GROUP_CONTROL_RELATIONCOLUMNNAME_PROPERTY, value);
        }
    }

    /// <summary>
    /// Gets or sets whether the submit button on the form where this control is used should be hidden.
    /// </summary>
    public bool HideSubmitButton
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideSubmitButton"), false);
        }
        set
        {
            SetValue("HideSubmitButton", value);
        }
    }


    /// <summary>
    /// Gets or sets whether the add button on the form where this control is used should be hidden.
    /// </summary>
    public bool HideAddButton
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideAddButton"), false);
        }
        set
        {
            SetValue("HideAddButton", value);
        }
    }

    /// <summary>
    /// Gets or sets visibility of the Actions column in the grid.
    /// </summary>
    public bool HideActions
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideActions"), false);
        }
        set
        {
            SetValue("HideActions", value);
        }
    }
    #endregion properties

    #region methods

    /// <summary>
    /// Sets up the internal CustomTable.
    /// </summary>
    protected void GetCustomTableFields()
    {
        DataClassInfo dcp = DataClassInfoProvider.GetDataClassInfo(SelectorCustomTableName);
        CustomTableForm.CustomTableId = dcp.ClassID;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        CustomTableForm.OnAfterDataLoad += CustomTableForm_OnAfterDataLoad;
        SetupControl();
        BindGrid();
        btnSave.Visible = !HideAddButton;
        CustomTableForm.Visible = !HideAddButton;
        if (!RequestHelper.IsPostBack())
            CustomTableForm.ReloadData();
    }
    void CustomTableGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            int itemID = ValidationHelper.GetInteger(actionArgument, 0);
            int customTableID = getCustomTableID();
            string className = CustomTableDataHelper.GetCustomTableClassName(customTableID);
            if (className == string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, EmergeCMSContext.CurrentSiteName) && this.FieldInfo.Name == EventsConstants.FIELDS_EVENTSCHEDULE_SESSIONDETAILS)
            {
                List<EventRegistration> list = EventsCalendarHelper.GetRegistrationsBySession(itemID, EventRegistrationStatus.CONFIRMED);
                if (list.Count > 0)
                {
                    ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_REGISTRATIONEXISTSFORSESSION));
                    return;
                }
                int scheduleID = QueryHelper.GetInteger("ItemID", 0);
                if (scheduleID > 0)
                {
                    EventSchedule schedule = EventsCalendarHelper.GetScheduleByScheduleID(scheduleID);
                    if (schedule.Event.EventType != EventsConstants.EVENTTYPE_VOLUNTEER)
                    {
                        EventsCalendarHelper.DeleteSessionByID(itemID);
                        ShowInformation(ResHelper.GetString("Emerge.GroupControl.DeleteMessage"));
                        if (this.CustomTableGrid.RowsCount == 1)
                        {
                            IDictionary<string, object> tableData = new Dictionary<string, object>();
                            tableData.Add(new KeyValuePair<string, object>(EventsConstants.FIELDS_EVENTSCHEDULE_HASSESSIONS, false));
                            CustomTableDataHelper.SaveCustomTableItem(string.Format(EventsConstants.CUSTOMTABLE_EVENT_EVENTSCHEDULE, EmergeCMSContext.CurrentSiteName), ref scheduleID, tableData);
                            this.Form.FieldControls[EventsConstants.FIELDS_EVENTSCHEDULE_HASSESSIONS].Value = false;
                        }
                    }
                    else
                    {
                        if (this.CustomTableGrid.RowsCount == 1)
                        {
                            ShowError(ResHelper.GetString(EventsConstants.STRINGCODE_SESSIONDELETEFORVOLUNTEERSCHEDULE));
                        }
                        else
                        {
                            EventsCalendarHelper.DeleteSessionByID(itemID);
                            ShowInformation(ResHelper.GetString("Emerge.GroupControl.DeleteMessage"));
                        }
                    }
                }
                else
                {
                    EventsCalendarHelper.DeleteSessionByID(itemID);
                    ShowInformation(ResHelper.GetString("Emerge.GroupControl.DeleteMessage"));
                }
                BindGrid();
                ResetForm();
            }
            else
            {
                if (DeleteCustomTableItem(" ItemID =" + itemID.ToString()))
                {
                    ShowInformation(ResHelper.GetString("Emerge.GroupControl.DeleteMessage"));
                    BindGrid();
                    ResetForm();
                }
                else
                    ShowError(ResHelper.GetString("Emerge.GroupControl.ErrorMessage.FailedToDelete"));
            }
        }
        if (actionName == "edit")
        {
            CustomTableForm.Mode = FormModeEnum.Update;
            CustomTableForm.CustomTableId = CustomTableDataHelper.GetCustomTableClassInfo(SelectorCustomTableName).ClassID;//
            CustomTableForm.ItemID = Convert.ToInt32(actionArgument);
            IDataContainer formData = null;
            IDataClass formItem = null;
            formItem = DataClassFactory.NewDataClass(SelectorCustomTableName, Convert.ToInt32(actionArgument));
            if (!formItem.IsEmpty())
            {
                formData = formItem;
                CustomTableForm.LoadData(formData);
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                throw new Exception("Custom table record which is intended to update does not exist.");
            }
        }
    }

    private int getCustomTableID()
    {
        int customTableID = QueryHelper.GetInteger("Customtableid", 0);
        if (customTableID == 0)
        {
            string customTableName = QueryHelper.GetString("customtablename", string.Empty);
            if (!String.IsNullOrEmpty(customTableName))
            {
                string className = string.Format(customTableName, EmergeCMSContext.CurrentSiteName);
                DataClassInfo classInfo = CustomTableDataHelper.GetCustomTableClassInfo(className);
                customTableID = classInfo.ClassID;
            }
        }
        return customTableID;
    }

    private bool DeleteCustomTableItem(string where)
    {
        DataSet customTableItemsToDelete = CustomTableDataHelper.GetCustomTableItemsByCondition(SelectorCustomTableName, where, string.Empty);
        if (!DataHelper.DataSourceIsEmpty(customTableItemsToDelete))
        {
            foreach (DataRow customTableItemDr in customTableItemsToDelete.Tables[0].Rows)
            {
                CustomTableItem.New(SelectorCustomTableName, customTableItemDr).Delete();
            }
            return true;
        }
        return false;
    }

    protected void SetupControl()
    {
        if (StopProcessing)
        {
            CustomTableForm.StopProcessing = true;
        }
        else
        {
            if (!string.IsNullOrEmpty(SelectorCustomTableName))
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(SelectorCustomTableName);
                CustomTableForm.CustomTableId = dci.ClassID;
            }
            CustomTableForm.ShowPrivateFields = true;
            btnSave.Click += btnSave_Click;
            btnUpdate.Click += btnUpdate_Click;
            CustomTableGrid.OnAction += CustomTableGrid_OnAction;
            CustomTableForm.SubmitButton.Visible = !HideSubmitButton;
        }        
    }

    protected void CustomTableForm_OnAfterDataLoad(object sender, EventArgs e)
    {
        List<IDataDefinitionItem> formitems = CustomTableForm.FormInformation.ItemsList;
        foreach (IField item in formitems)
        {
            FormFieldInfo fieldInfo = ((FormFieldInfo)(item));
            if (!fieldInfo.AllowEmpty)
            {
                fieldInfo.Caption = fieldInfo.Caption + Constants.FORM_REQUIRED_FIELD_ASTERISK;
            }

        }
    }

    private void BindGrid()
    {
        CustomTableDataSource.DataSource = null;
        CustomTableDataSource.DataBind();
        CustomTableDataSource.IsSelected = false;
        CustomTableDataSource.SelectedQueryStringKeyName = "";
        int itemID = QueryHelper.GetInteger("ItemID", 0);
        if (hdnItemIdsForUpdate.Value.Trim().EndsWith(","))
        {
            hdnItemIdsForUpdate.Value = hdnItemIdsForUpdate.Value.Substring(0, hdnItemIdsForUpdate.Value.LastIndexOf(","));
        }
        Value = hdnItemIdsForUpdate.Value;
        CustomTableDataSource.WhereCondition = string.Empty;
        CustomTableDataSource.WhereCondition = RelationColumnName + " = " + itemID.ToString();
        if (!string.IsNullOrEmpty(hdnItemIdsForUpdate.Value))
        {
            CustomTableDataSource.WhereCondition += " OR " + Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME + " in ( " + hdnItemIdsForUpdate.Value.Replace(Constants.MULTI_VALUE_SEPERATOR, Constants.COMMA_SEPERATOR) + " )";
        }
        CustomTableDataSource.CustomTable = SelectorCustomTableName;
        CustomTableDataSource.ReBind();
        if (HideActions)
        {
            CustomTableGrid.ShowActionsLabel = false;
            CustomTableGrid.ShowActionsMenu = false;
            CustomTableGrid.GridActions.Actions.Clear();
        }
        List<string> selectedColumns = CustomTableDataHelper.GetCustomTableSelectedColumns(SelectorCustomTableName);
        if (selectedColumns.Count > 0)
        {
            FormInfo mFormInfo = FormHelper.GetFormInfo(SelectorCustomTableName, false);
            foreach (string columnName in selectedColumns)
            {
                FormFieldInfo fieldInfo = mFormInfo.GetFormField(columnName);
                CMS.UIControls.UniGridConfig.Column column = new CMS.UIControls.UniGridConfig.Column();
                column.Source = columnName;
                column.Name = columnName;
                column.Caption = fieldInfo == null ? columnName : (fieldInfo.Caption == string.Empty ? columnName : fieldInfo.Caption);
                CustomTableGrid.GridColumns.Columns.Add(column);
            }
        }
        else
            CustomTableGrid.GridView.AutoGenerateColumns = true;
        CustomTableGrid.DataSource = EmergeRelationHelper.GetRelationShipData((DataSet)CustomTableDataSource.DataSource, SelectorCustomTableName);
        CustomTableGrid.ReBind();
        if (CustomTableGrid.RowsCount == 0)
            this.hdnItemIdsForUpdate.Value = string.Empty;
    }

    void btnSave_Click(object sender, EventArgs e)
    {        
        if (CustomTableForm.ValidateData())
        {
            Save();
            BindGrid();
        }
    }


    void btnUpdate_Click(object sender, EventArgs e)
    {        
        if (CustomTableForm.ValidateData())
        {
            Save();
            BindGrid();
        }
    }

    private void Save()
    {
        try
        {
            SaveCustomTableItem();
            ResetForm();
            ShowInformation(ResHelper.GetString("Emerge.GroupControl.UpdateMessage"));
        }
        catch (Exception ex)
        {
            EmergeLogWriter.WriteError("Group Control - SaveCustomTableItem", EventCode.EMERGE_ADD, ex.ToString());
            ShowError(ResHelper.GetString("Emerge.GroupControl.ErrorMessage.FailedToSave"));
        }
    }

    private IDictionary<string, object> getTableData()
    {
        IDictionary<string, object> tableData = new Dictionary<string, object>();
        IDataContainer data = CustomTableForm.Data;
        foreach (string columnName in CustomTableForm.Fields)
        {
            var columnValueToUpdate = CustomTableForm.GetFieldValue(columnName);

            if (string.IsNullOrEmpty(Convert.ToString(columnValueToUpdate)))
                columnValueToUpdate = DBNull.Value;
            tableData.Add(columnName, columnValueToUpdate);
        }
        return tableData;
    }
    private void SaveCustomTableItem()
    {
        int itemid = CustomTableForm.ItemID;
        bool isUpdate = itemid == 0 ? false : true;

        CustomTableDataHelper.SaveCustomTableItem(CustomTableForm.CustomTableId, ref itemid, getTableData());
        if (hdnItemIdsForUpdate.Value.Trim().Equals(string.Empty))
        {
            hdnItemIdsForUpdate.Value = itemid.ToString();
        }
        else if (!isUpdate)
            hdnItemIdsForUpdate.Value += Constants.MULTI_VALUE_SEPERATOR + itemid.ToString();
    }

    private void ResetForm()
    {
        foreach (string columnName in CustomTableForm.Fields)
        {
            if (!CustomTableForm.FieldControls[columnName].FieldInfo.Settings["controlname"].ToString().ToLower().Equals(Constants.DROPDOWNLISTCONTROL.ToLower()))
                CustomTableForm.FieldControls[columnName].Text = string.Empty;
        }
        btnSave.Visible = true;
        btnUpdate.Visible = false;
        CustomTableForm.Mode = FormModeEnum.Insert;
        CustomTableForm.ItemID = 0;

    }
    #endregion methods

}