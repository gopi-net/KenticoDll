using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.Helpers;


namespace Bluespire.Emerge.Components.EventsCalendar.Services
{
    public static class CartService 
    {


        public static void AddItem(int occurenceID, List<EventSession> selectedSessions)
        {
            DataRowView cartsDetailsDR = GetEventCartDetails(occurenceID);

            if (cartsDetailsDR.Row.Table.Columns.Contains(EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS))
            {
                cartsDetailsDR.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS] = GetSessionIDs(selectedSessions);
            }

            if (cartsDetailsDR.Row.Table.Columns.Contains(EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONSDETAILS))
            {
                cartsDetailsDR.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONSDETAILS] = GetSessionDetails(selectedSessions);
            }

            if (GetItems().AsEnumerable().Any(x => x.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID].ToString() == occurenceID.ToString()))
            {
                RemoveItem(occurenceID);
            }

            GetItems().Insert(0, cartsDetailsDR);

        }

        private static object GetSessionDetails(List<EventSession> selectedSessions)
        {
            string sessionDetails = string.Empty;
            foreach (EventSession session in selectedSessions)
            {
                sessionDetails += session.Title + " : " + session.StartTime + " - " + session.EndTime + "<br/>";
            }
            return sessionDetails;
        }

        private static string GetSessionIDs(List<EventSession> selectedSessions)
        {
            string concatinatedSessionIds = string.Empty;

            foreach (EventSession session in selectedSessions)
            {
                concatinatedSessionIds += session.SessionID + Constants.DELIMITER_IN_LIST_VALUES;

            }

            if (concatinatedSessionIds.EndsWith(Constants.DELIMITER_IN_LIST_VALUES))
            {
                concatinatedSessionIds = concatinatedSessionIds.Remove(concatinatedSessionIds.LastIndexOf(Constants.DELIMITER_IN_LIST_VALUES));
            }

            return concatinatedSessionIds;
        }

        private static DataRowView GetEventCartDetails(int occurenceID)
        {

            string queryName = string.Format(EventsConstants.QUERY_GETEVENTCARTDETAILS, SiteContext.CurrentSiteName);
            QueryDataParameters parameters = new QueryDataParameters();
            parameters.Add("@OccurenceID", occurenceID);
            DataSet eventCartDetail = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
            return eventCartDetail.Tables[0].DefaultView[0];

        }

        public static void RemoveItem(int occurenceID)
        {
            GetItems().Remove(GetItems().Find(x => x.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID].ToString() == occurenceID.ToString()));
        }

        public static List<DataRowView> GetItems()
        {
            if (null == SessionHelper.GetValue(EventsConstants.SESSIONKEY_EVENTSCART))
            {
                List<DataRowView> cartItems = new List<DataRowView>();
                SessionHelper.SetValue(EventsConstants.SESSIONKEY_EVENTSCART, cartItems);
            }
            return (List<DataRowView>)SessionHelper.GetValue(EventsConstants.SESSIONKEY_EVENTSCART);
        }

        public static void UpdateDiscountedCostForItem(string discountCoupon, double newDiscountedCost, int OccurenceID)
        {
            if (GetItems().Any(x => x.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID].ToString() == OccurenceID.ToString()))
            {
                GetItems().Find(x => x.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID].ToString() == OccurenceID.ToString()).Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT] = newDiscountedCost.ToString();
                GetItems().Find(x => x.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_OCCURENCEID].ToString() == OccurenceID.ToString()).Row["DiscountCode"] = discountCoupon;
            }
        }

        public static double GetTotalCost()
        {
            double totalCost = 0.0d;

            foreach (DataRowView cartItem in GetItems())
            {
                totalCost += Convert.IsDBNull(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT]) ? 0.0 :Convert.ToDouble(cartItem.Row[EventsConstants.FIELDS_EVENTREGISTRATIONS_AMOUNT]);
            }

            return totalCost;
        }

        public static void Destroy()
        {
            SessionHelper.Remove(EventsConstants.SESSIONKEY_EVENTSCART);
        }


    }
}
