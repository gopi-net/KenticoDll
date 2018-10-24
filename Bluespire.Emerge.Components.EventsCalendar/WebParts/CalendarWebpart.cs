using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Membership;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    public class CalendarWebpart : EventsCalendarWebPart
    {
        private bool IsCategoryFiltered(LocalizedDropDownList Category)
        {
            return Category.SelectedValue != "-1";
        }

        private bool IsSubCategoryFiltered(LocalizedDropDownList SubCategory)
        {
            return SubCategory.SelectedValue != "-1";
        }

        private int GetWeekdayNum(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
                return 0;
            if (day == DayOfWeek.Monday)
                return 1;
            if (day == DayOfWeek.Tuesday)
                return 2;
            if (day == DayOfWeek.Wednesday)
                return 3;
            if (day == DayOfWeek.Thursday)
                return 4;
            if (day == DayOfWeek.Friday)
                return 5;
            if (day == DayOfWeek.Saturday)
                return 6;
            else
                return -1;
        }

        public OrderedEnumerableRowCollection<DataRow> FilterData(DateTime listFirstDate, DateTime listLastDate, DataTable dt)
        {
            return from EventsData in dt.AsEnumerable()
                   where (EventsData.Field<DateTime>(EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE) >= listFirstDate) &&
                   (EventsData.Field<DateTime>(EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE) < listLastDate)
                   orderby EventsData.Field<DateTime>(EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE)
                   select EventsData;
        }

        public void BindRepeater(CMSRepeater repeater, OrderedEnumerableRowCollection<DataRow> filteredData, string transformationName)
        {
            if (filteredData != null)
            {
                DataTable newTable = filteredData.CopyToDataTable();
                repeater.DataSource = newTable;
            }
            if (transformationName != string.Empty)
            {
                repeater.TransformationName = string.Format(transformationName, EmergeCMSContext.CurrentSiteName);
            }
            repeater.DataBind();
        }

        public DataSet FilterCategory(DataSet dsEvents, LocalizedDropDownList Category)
        {
            if (IsCategoryFiltered(Category))
            {
                var filterCategory = from EventsData in dsEvents.Tables[0].AsEnumerable()
                                     where (EventsData.Field<string>(EventsConstants.FIELDS_EVENTS_CATEGORY) == Category.SelectedValue)
                                     select EventsData;

                if (filterCategory.Any())
                {
                    DataTable table = filterCategory.CopyToDataTable();
                    dsEvents = new DataSet();
                    dsEvents.Tables.Add(table);
                }
                else
                    dsEvents.Tables[0].Rows.Clear();

            }
            return dsEvents;

        }

        public DataSet FilterSubCategory(DataSet dsEvents, LocalizedDropDownList SubCategory)
        {
            if (IsSubCategoryFiltered(SubCategory))
            {
                var filterSubCategory = from EventsData in dsEvents.Tables[0].AsEnumerable()
                                        where (EventsData.Field<string>(EventsConstants.FIELDS_EVENTS_SUBCATEGORY) == SubCategory.SelectedValue)
                                        select EventsData;

                if (filterSubCategory.Any())
                {
                    DataTable table = filterSubCategory.CopyToDataTable();
                    dsEvents = new DataSet();
                    dsEvents.Tables.Add(table);
                }
                else
                    dsEvents.Tables[0].Rows.Clear();

            }
            return dsEvents;
        }

        public DateTime GetStartDate()
        {
            DateTime weekFirstDate = System.DateTime.Today;
            DayOfWeek dw = weekFirstDate.DayOfWeek;
            int weekday = GetWeekdayNum(dw);
            if (weekday > 0)
            {
                weekday = weekday * -1;
                weekFirstDate = weekFirstDate.AddDays(weekday);
            }
            return weekFirstDate;
        }
        public DataSet GetSelectedMonthEvents(DateTime date)
        {
            string queryName;
            QueryDataParameters parameters;
            queryName = string.Format(EventsConstants.QUERY_GETEVENTS, SiteContext.CurrentSiteName);
            if (MembershipContext.AuthenticatedUser.IsInRole(EventsConstants.ROLE_VOLUNTEERUSERS, SiteContext.CurrentSiteName))
                queryName = string.Format(EventsConstants.QUERY_GETVOLUNTEEREVENTS, SiteContext.CurrentSiteName);
            parameters = new QueryDataParameters();
            parameters.Add("@Date", date);
            return ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
        }

    }
}
