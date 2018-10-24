using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using System.Data;
using Bluespire.Emerge.Common.Exceptions;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public class DiscountService : IDiscountService
    {
        public void DeleteDiscountsByScheduleID(int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_DISCOUNTS_SCHEDULEID + " = " + scheduleID.ToString();
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_DISCOUNTDETAILS, SiteContext.CurrentSiteName);
            DataSet ds = CustomTableItemProvider.GetItems(className, whereCondition, string.Empty);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int itemID = Convert.ToInt32(row[EventsConstants.FIELDS_DISCOUNTS_DISCOUNTID]);
                    CustomTableDataHelper.DeleteCustomTableItem(itemID, className);
                }
            }
        }

        public DiscountDetails GetDicountDetailsByCodeAndScheduleID(string discountCode, int scheduleID)
        {
            string whereCondition = EventsConstants.FIELDS_DISCOUNTS_SCHEDULEID + " = " + scheduleID.ToString() + " AND " + EventsConstants.FIELDS_DISCOUNTS_DISCOUNTCODE + " = '" + discountCode + "'";
            string className = string.Format(EventsConstants.CUSTOMTABLE_EVENT_DISCOUNTDETAILS, SiteContext.CurrentSiteName);
            DiscountDetails discountDetails = null;
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(className, whereCondition, string.Empty);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                DataRow row = ds.Tables[0].Rows[0];
                discountDetails = row.ToDiscountDetails();

            }
            else
                throw new InvalidDiscountCodeException(string.Format("The discount code '{0}' for schedule ID {1} is invalid.", discountCode, scheduleID.ToString()));

            return discountDetails;
        }

        public double GetDiscountedCostbyCodeAndScheduleID(string discountCode, int scheduleID)
        {
            EventSchedule schedule = EventsCalendarHelper.GetScheduleByScheduleID(scheduleID);
            DiscountDetails discountDetails = GetDicountDetailsByCodeAndScheduleID(discountCode, scheduleID);

            if (discountDetails.DiscountType == "PERCENT")
                return schedule.CostForPublic - (schedule.CostForPublic * discountDetails.DiscountFactor / 100);

            return schedule.CostForPublic - discountDetails.DiscountFactor;
        }

    }
}
