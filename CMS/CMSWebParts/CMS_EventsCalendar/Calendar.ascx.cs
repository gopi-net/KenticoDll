using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using System.Data;
using System.Text;
using System.IO;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_Calendar : CalendarWebpart
{
    DataSet dsEvents = new DataSet();
    StringBuilder sbEvent;
    StringWriter swEvent;
    HtmlTextWriter hwEvent;

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

    # region "Webpart Properties"
    /// <summary>
    /// on click of Add To Cart button, control will be redirected to this page
    /// </summary>
    public string CartPageURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("CartPageURL"), string.Empty);
        }
        set
        {
            SetValue("CartPageURL", value);
        }
    }

    public string GridViewTransformation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewTransformation"), string.Empty);
        }
        set
        {
            SetValue("GridViewTransformation", value);
        }
    }
    public string GridViewTransformationEnvelopeBefore
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewTransformationEnvelopeBefore"), string.Empty);
        }
        set
        {
            SetValue("GridViewTransformationEnvelopeBefore", value);
        }
    }
    public string GridViewTransformationEnvelopeAfter
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewTransformationEnvelopeAfter"), string.Empty);
        }
        set
        {
            SetValue("GridViewTransformationEnvelopeAfter", value);
        }
    }
    public string GridViewCalloutTransformationEnvelopeAfter
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewCalloutTransformationEnvelopeAfter"), string.Empty);
        }
        set
        {
            SetValue("GridViewCalloutTransformationEnvelopeAfter", value);
        }
    }
    public string GridViewCalloutTransformationEnvelopeBefore
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewCalloutTransformationEnvelopeBefore"), string.Empty);
        }
        set
        {
            SetValue("GridViewCalloutTransformationEnvelopeBefore", value);
        }
    }
    public string GridViewEventCellCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewEventCellCssClass"), string.Empty);
        }
        set
        {
            SetValue("GridViewEventCellCssClass", value);
        }
    }
    public bool IsShowToolTipGridView
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("IsShowToolTipGridView"), true);
        }
        set
        {
            SetValue("IsShowToolTipGridView", value);
        }
    }
    public string GridViewCalloutTransformation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("GridViewCalloutTransformation"), string.Empty);
        }
        set
        {
            SetValue("GridViewCalloutTransformation", value);
        }
    }
    #endregion

    # region "Page Events"
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = CalendarPanel;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            CalendarPanel.Visible = false;
            return;
        }
        AddControlEvents();
        if (!Page.IsPostBack)
        {
            Session["EmergeEventCalendarSelectedDate"] = System.DateTime.Today;
            FillDropDownLists();
        }
        LoadEventsAndBindView(GetSelectedMonth());
        bool isEnabled = EventsCalendarHelper.IsEventCartEnabled();
        ViewCartButton.Visible = isEnabled && CartService.GetItems().Count > 0;
    }
    # endregion

    #region "Common"
    protected void calEvents_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        Session["EmergeEventCalendarSelectedDate"] = e.NewDate.AddDays(8);
        LoadEventsAndBindView(e.NewDate.AddDays(8));
    }
    protected void SubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadEventsAndBindView(GetSelectedMonth());
    }
    protected void Category_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedCategoryIndex = Category.SelectedIndex;
        FillDropDownLists();
        Category.SelectedIndex = selectedCategoryIndex;
        LoadEventsAndBindView(GetSelectedMonth());
    }

    private void AddControlEvents()
    {
        Category.SelectedIndexChanged += Category_SelectedIndexChanged;
        SubCategory.SelectedIndexChanged += SubCategory_SelectedIndexChanged;
        calEvents.DayRender += calEvents_DayRender;
        calEvents.VisibleMonthChanged += calEvents_VisibleMonthChanged;
        repeaterWeekViewParent.ItemDataBound += repeaterWeekViewParent_ItemDataBound;
        ViewCartButton.Click += ViewCartButton_Click;
    }

    void ViewCartButton_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(CartPageURL))
            URLHelper.Redirect(CartPageURL);
    }
    private void FillDropDownLists()
    {
        string parameterName = string.Format("@{0}", EventsConstants.FIELDS_CATEGORY_CATEGORYNAME);
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add(parameterName, Category.SelectedValue);
        SubCategory_DataSource.QueryParameters = parameters;
        LoadListControls(false);
    }
    private void LoadEventsAndBindView(DateTime date)
    {
        LoadEvents(date);
        BindAllViews();
    }
    private void LoadEvents(DateTime date)
    {
        dsEvents = GetSelectedMonthEvents(date);
        calEvents.VisibleDate = date;
        dsEvents = FilterCategory(dsEvents, Category);
        dsEvents = FilterSubCategory(dsEvents, SubCategory);
    }
    private void BindAllViews()
    {
        if (!DataHelper.DataSourceIsEmpty(dsEvents))
        {
            BindListView();
            BindDayView();
        }
        else
        {
            ToggleListView(false);
            ToggleDayView(false);
        }
        BindWeekView();
    }

    private DateTime GetSelectedMonth()
    {
        if (Session["EmergeEventCalendarSelectedDate"] == null)
            return System.DateTime.Today;
        else
            return (DateTime)Session["EmergeEventCalendarSelectedDate"];
    }
    private string GetCalendarDayToolTip(OrderedEnumerableRowCollection<DataRow> dayFilter)
    {
        StringBuilder sbEventTooltip = new StringBuilder();
        StringWriter swEventTooTip = new StringWriter(sbEventTooltip);
        HtmlTextWriter hwEventToolTip = new HtmlTextWriter(swEventTooTip);

        if (IsShowToolTipGridView)
        {
            BindRepeater(repeater, dayFilter, GridViewCalloutTransformation);
            sbEventTooltip.Append(GridViewCalloutTransformationEnvelopeBefore);
            repeater.RenderControl(hwEventToolTip);
            sbEventTooltip.Append(GridViewCalloutTransformationEnvelopeAfter);
        }
        return Convert.ToString(sbEventTooltip);
    }
    #endregion

    #region "Grid"
    protected void calEvents_DayRender(object sender, DayRenderEventArgs e)
    {
        Label lblDay = new Label();
        CalendarDay day = e.Day;
        TableCell cell = e.Cell;
        sbEvent = new StringBuilder();
        swEvent = new StringWriter(sbEvent);
        hwEvent = new HtmlTextWriter(swEvent);
        if (cell.Controls.Count > 0)
        {
            lblDay.Text = day.DayNumberText;
            cell.Controls.Clear();
            cell.Controls.Add(lblDay);
            SetCurrentDayEvents(day, cell);
        }
        if (day.IsOtherMonth)
        {
            lblDay.CssClass = "date hide";
        }
        else
        {
            lblDay.CssClass = "date";
        }

    }
    private void SetCurrentDayEvents(CalendarDay day, TableCell cell)
    {
        if (!DataHelper.DataSourceIsEmpty(dsEvents))
        {
            var dayFilter = FilterData(day.Date, day.Date.AddDays(1), dsEvents.Tables[0]);
            if (dayFilter.Any())
            {
                BindCalendarDayData(dayFilter);
                cell.AddCssClass(GridViewEventCellCssClass);
                cell.Controls.Add(new LiteralControl(sbEvent.ToString()));
            }
            else
            {
                // cell.Controls.Add(new LiteralControl("<br />" + ResHelper.GetString("Emerge.EC.Calendar.Label.NoEventsFound")));
            }
        }
        else
        {
            //cell.Controls.Add(new LiteralControl("<br />" + ResHelper.GetString("Emerge.EC.Calendar.Label.NoEventsFound")));
        }
    }
    private void BindCalendarDayData(OrderedEnumerableRowCollection<DataRow> dayFilter)
    {
        BindRepeater(repeater, dayFilter, GridViewTransformation);
        sbEvent.Append(GridViewTransformationEnvelopeBefore);
        repeater.RenderControl(hwEvent);        
        sbEvent.Append(GridViewTransformationEnvelopeAfter);
    }
    #endregion

    #region "Week"
    private void BindWeekView()
    {
        DateTime weekFirstDate;
        weekFirstDate = GetStartDate();
        DataTable dtWeekEvents = GetWeekDates(weekFirstDate);
        repeaterWeekViewParent.TransformationName = string.Format("customtable.Emerge_{0}_EC_Events.CalendarWeekView", EmergeCMSContext.CurrentSiteName);
        repeaterWeekViewParent.DataSource = dtWeekEvents;
        repeaterWeekViewParent.DataBind();
    }
    protected void repeaterWeekViewParent_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        DateTime date;

        if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
        {
            date = GetItemDate(item);
            CMSRepeater childRepeater = (CMSRepeater)item.Controls[0].FindControl("repeaterWeekViewChild");
            if (!DataHelper.DataSourceIsEmpty(dsEvents))
            {
                BindItemData(item, date, childRepeater);
            }
            else
            {
                ToggleWeekViewItem(item, childRepeater, false);
            }
        }
    }

    private DataTable GetWeekDates(DateTime weekFirstDate)
    {
        DataTable dtWeekEvents = new DataTable();
        dtWeekEvents.Columns.Add(EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE, typeof(DateTime));
        for (DateTime date = weekFirstDate; date <= weekFirstDate.AddDays(6); date = date.AddDays(1))
        {
            DataRow dr = dtWeekEvents.NewRow();
            dr[EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE] = date;
            dtWeekEvents.Rows.Add(dr);
        }
        return dtWeekEvents;
    }
    private void BindItemData(RepeaterItem item, DateTime date, CMSRepeater childRepeater)
    {
        Literal calloutLitral = (Literal)item.Controls[0].FindControl("literalCallout");
        var dayFilter = FilterData(date, date.AddDays(1), dsEvents.Tables[0]);
        if (dayFilter.Any())
        {
            BindRepeater(childRepeater, dayFilter, string.Empty);
            calloutLitral.Text = GetCalendarDayToolTip(dayFilter);
            ToggleWeekViewItem(item, childRepeater, true);
        }
        else
        {
            ToggleWeekViewItem(item, childRepeater, false);
        }
    }
    private DateTime GetItemDate(RepeaterItem item)
    {
        DateTime date = System.DateTime.Today;
        HiddenField hdnWeekDate = (HiddenField)item.Controls[0].FindControl("hdnWeekDate");
        if (hdnWeekDate != null && hdnWeekDate.Value != string.Empty)
        {
            date = Convert.ToDateTime(hdnWeekDate.Value);
        }
        return date;
    }
    private void ToggleWeekViewItem(RepeaterItem item, CMSRepeater childRepeater, bool hasEvents)
    {
        Literal ltNoEventLiteral = (Literal)item.Controls[0].FindControl("ltNoEventLiteral");
        if (ltNoEventLiteral != null)
        {
            ltNoEventLiteral.Text = "";// ResHelper.GetString("Emerge.EC.Calendar.Label.NoEventsFound");
            childRepeater.Visible = hasEvents;
            ltNoEventLiteral.Visible = !hasEvents;
        }
    }
    #endregion

    #region "Day"
    private void BindDayView()
    {
        var filterDay = FilterData(System.DateTime.Today, System.DateTime.Today.AddDays(1), dsEvents.Tables[0]);
        if (filterDay.Any())
        {
            BindRepeater(repeaterDayView, filterDay, "customtable.Emerge_{0}_EC_Events.Day");
            ToggleDayView(true);
        }
        else
        {
            ToggleDayView(false);
        }
    }
    private void ToggleDayView(bool hasEvents)
    {
        repeaterDayView.Visible = hasEvents;
        lblNoEventsDay.Visible = !hasEvents;
    }
    #endregion

    #region "List"
    private void BindListView()
    {
        DateTime listFirstDate, listLastDate;
        listFirstDate = GetSelectedMonth();
        listFirstDate = listFirstDate.AddDays((listFirstDate.Day * -1) + 1);
        listLastDate = listFirstDate.AddMonths(1);
        var filterMonth = FilterData(listFirstDate, listLastDate, dsEvents.Tables[0]);
        if (filterMonth.Any())
        {
            BindRepeater(repeaterListView, filterMonth, "customtable.Emerge_{0}_EC_Events.List");
            ToggleListView(true);
        }
        else
        {
            ToggleListView(false);
        }
    }
    private void ToggleListView(bool hasEvents)
    {
        repeaterListView.Visible = hasEvents;
        lblNoEvents.Visible = !hasEvents;
    }
    #endregion
}