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
using Bluespire.Emerge.Web.Pages.HistoryTracker;
using Bluespire.Emerge.Web;
using Bluespire.Emerge.CommonService.HistoryTracker;
using Bluespire.Emerge.Common;

public partial class CMSModules_CMS_HistoryTracker_Pages_History_Data_List : HistoryTrackerHistoryDataListPage
{
    private const string rangeDateTimeToDeleteHistoryItems = "historyRangePicker";


    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
        customTableSearch.UniGrid = customTableDataList.UniGrid;

        EmergeRangeDateTimePicker calendar = new EmergeRangeDateTimePicker("Delete History From", "Delete History To");
        calendar.UseDynamicDefaultTime = true;
        calendar.EditTime = false;
        calendar.ID = rangeDateTimeToDeleteHistoryItems;
        calendar.PostbackOnOK = false;

        panDateRangePicker.Controls.Add(calendar);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            NewItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_HistoryTracker/Tools/HistoryTracker_Data_ViewItem.aspx";

            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_HistoryTracker/Pages/History_Data_List.xml";
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
            btnDelete.Click += btnDelete_Click;
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    void btnDelete_Click(object sender, EventArgs e)
    {
        string where = string.Empty;

        if (panDateRangePicker.FindControl(rangeDateTimeToDeleteHistoryItems) != null)
        {
            EmergeRangeDateTimePicker dateTimePicker = ((EmergeRangeDateTimePicker)panDateRangePicker.FindControl(rangeDateTimeToDeleteHistoryItems));
            if (!dateTimePicker.ValidateDateTimeRange())
            { ShowError(EmergeResHelper.GetString("Emerge.CustomTableSearchControl.ErrorMessage.InvalidDateRange")); return; }

            string FromDate = string.Empty;
            string ToDate = string.Empty;

            FromDate = dateTimePicker.DateTimeTextBox.Text.Trim().ToLower().Equals(dateTimePicker.FromWaterMarkText.ToLower()) ? string.Empty : dateTimePicker.DateTimeTextBox.Text.Trim();
            ToDate = dateTimePicker.AlternateDateTimeTextBox.Text.Trim().ToLower().Equals(dateTimePicker.ToWaterMarkText.ToLower()) ? string.Empty : dateTimePicker.AlternateDateTimeTextBox.Text.Trim();

            if ((FromDate.Equals(string.Empty)) && (ToDate.Equals(string.Empty)))
            { ShowError(EmergeResHelper.GetString(Constants.STRINGCODE_HT_FROMTODATEEMPTYVALIDATION)); return; }

            if (FromDate.Equals(string.Empty))
            { ShowError(EmergeResHelper.GetString(Constants.STRINGCODE_HT_FROMDATEEMPTYVALIDATION)); return; }

            if (ToDate.Equals(string.Empty))
            { ShowError(EmergeResHelper.GetString(Constants.STRINGCODE_HT_TODATEEMPTYVALIDATION)); return; }

            ToDate = SetEndTimeOfDay(Convert.ToDateTime(ToDate));

            HistoryTrackerService.DeleteHistoryDetails(FromDate, ToDate);
            ShowInformation(EmergeResHelper.GetString(Constants.STRINGCODE_HT_DELETEHISTORYDETAILSSUCCESSMESSAGE));
            customTableDataList.UniGrid.ReloadData();

        }

    }

    string SetEndTimeOfDay(DateTime date)
    {

        if (date.Hour == 0 && date.Minute == 0 && date.Second == 0)
        {
            date = date.AddHours(23);
            date = date.AddMinutes(59);
            date = date.AddSeconds(59);
        }
        return date.ToString();

    }
}