using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.Web.Pages.BasePages;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.FormEngine;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using System.Threading;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_Form : EventsCalendarDataEditItemPage
{
    int id;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = EventsConstants.PAGEURL_DATA_LIST; //"~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_List.aspx";
            NewItemPage = EventsConstants.PAGEURL_NEW_EVENTS; //"~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Form.aspx";

            RegisterEvents();

            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                // customTableForm.EditItemPage = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";
                EventFormControl.CustomTableId = CustomTableID;
                EventFormControl.ItemId = ItemID;
                // customTableForm.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);

                //if header action visible set to false then ok button appears at the bottom of the page.
                //  CurrentMaster.HeaderActions.Visible = false;
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    private void RegisterEvents()
    {
        EventFormControl.OnBeforeSave += EventFormControl_OnBeforeSave;
        EventFormControl.OnAfterSave += EventFormControl_OnAfterSave;
        SaveButton.Click += SaveButton_Click;
        SaveNext.Click += SaveNext_Click;
    }

    void SaveNext_Click(object sender, EventArgs e)
    {
        if(Save())
            Response.Redirect("~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_EventSchedule.aspx?new=1&customtablename=customtable.Emerge_{0}_EC_EventsSchedule&EventID=" + id);
    }

    void EventFormControl_OnAfterSave(object sender, EventArgs e)
    {

    }

    void EventFormControl_OnBeforeSave(object sender, EventArgs e)
    {

    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            EventFormControl.ItemId = id;
            EventFormControl.processAfterSave();
        }
    }

    private bool Save()
    {
        bool status = false;

        if (EventFormControl.CustomTableForm.ValidateData())
        {
            status = SaveCustomTableData();
            if (status)
                ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, ResHelper.GetString("CustomTable.Events.SaveRecord"), "", "", true);
            else
                ShowError(ResHelper.GetString("Emerge.GroupControl.ErrorMessage.FailedToSave"));
        }
        return status;
    }

    /// <summary>
    /// Save customTableForm Data in Database
    /// </summary>
    /// <returns></returns>
    private bool SaveCustomTableData()
    {
        bool result = false;
        try
        {
            Dictionary<string, object> tableData = new Dictionary<string, object>();
            foreach (string columnName in EventFormControl.CustomTableForm.Fields)
            {
                var columnValueToUpdate = EventFormControl.CustomTableForm.GetFieldValue(columnName);

                if (columnValueToUpdate != string.Empty)
                {
                    tableData.Add(columnName, columnValueToUpdate.ToString());
                }
                else
                    tableData.Add(columnName, string.Empty);
            }
            id = EventFormControl.ItemId;
            result = CustomTableDataHelper.SaveCustomTableItem(CustomTableID, ref id, tableData);  
        }

        catch (Exception ex)
        {
            ShowError(ResHelper.GetString("CustomTable.ErrorDuringSave"), ex.Message, null);

        }

        return result;

    }

    /// <summary>
    /// reset customtable form when new record inserted
    /// </summary>
    private void ResetFormFields()
    {
        EventFormControl.CustomTableForm.ReloadData();

    }
}