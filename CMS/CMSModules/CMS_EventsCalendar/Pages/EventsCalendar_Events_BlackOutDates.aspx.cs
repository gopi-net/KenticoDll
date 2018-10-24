using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.FormEngine;
using Bluespire.Emerge.Web.Pages;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Unity;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.Web.Pages.BasePages;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using System.Web.Services;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_BlackOutDates : EventsCalendarDataEditItemPage
{
    private string editItemPage = EventsConstants.PAGEURL_NEW_BLACKOUTDATES;
    private DataClassInfo dci = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = EventsConstants.PAGEURL_DATA_LIST;
            NewItemPage = EventsConstants.PAGEURL_NEW_BLACKOUTDATES;
            RegisterEvents();
            OnPageLoad();
            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }
            if (AccessGranted)
            {
                EventFormControl.CustomTableId = CustomTableID;
                EventFormControl.ItemId = ItemID;
            }
            if (CustomTableID > 0)
            {
                dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    private void RegisterEvents()
    {
        SaveButton.Click += SaveButton_Click;
        EmergePopup.OnOKButtonClick += EmergePopup_OnOKButtonClick;
    }


    void EmergePopup_OnOKButtonClick(object sender, EventArgs e)
    {
        EmergePopup.Hide();
    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        if (EventFormControl.CustomTableForm.ValidateData())
        {
            DateTime date = getBlackOutDate();
            bool hasDateClash = EventsCalendarHelper.IsBlackOutDateClashed(date);
            if (hasDateClash)
            {
                LiteralControl literal = new LiteralControl();
                literal.Text = ResHelper.GetString(EventsConstants.STRINGCODE_BLACKOUTDATECLASHMESSAGE);
                EmergePopup.Body.Controls.Add(literal);
                EmergePopup.Show();
            }
            else
            {
                saveData();
            }
        }
    }


    private DateTime getBlackOutDate()
    {
        object value = EventFormControl.CustomTableForm.GetFieldValue(EventsConstants.FIELDS_BLACKOUTDATES_BLACKOUTDATE);
        DateTime date = Convert.ToDateTime(value);
        return date;
    }

    private void saveData()
    {
        EventFormControl.CustomTableForm.SaveData(URLHelper.GetAbsoluteUrl(EventFormControl.EditItemPage) + "?customtableid=" + EventFormControl.CustomTableId + "&itemid=" + EventFormControl.CustomTableForm.ItemID);
    }
}