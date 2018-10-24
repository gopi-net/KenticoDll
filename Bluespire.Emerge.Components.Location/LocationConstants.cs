using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.Location
{
    public static class LocationConstants
    {
        #region "Page Names"
        public const string LOCATION_DASHBOARDPAGE = "LocationDashboardPage";
        public const string LOCATION_LISTPAGE = "LocationListPage";
        public const string LOCATION_DATAVIEWITEMPAGE = "LocationDataViewItemPage";
        public const string LOCATION_DATASELECTFIELDSPAGE = "LocationDataSelectFieldsPage";
        public const string LOCATION_DATALISTPAGE = "LocationDataListPage";
        public const string LOCATION_DATAEDITITEMPAGE = "LocationDataEditItemPage";
        #endregion

        #region StringCodes
        public const string STRINGCODE_LOCATIONHOME = "Emerge.LOC.Dashboard";
        public const string STRINGCODE_LOCATIONREPEATER_ZEROROWSTEXT = "Emerge.LOC.LocationRepeater.ZeroRowsText";
        public const string STRINGCODE_LOCATION_GMAPMARKERINFOHTML = "Emerge.LOC.GMAPMARKERINFOHTML";
        #endregion

        #region PageURLs

        public const string PAGEURL_LIST_LOCATION = "~/CMSModules/CMS_Location/Tools/Location_List.aspx";
        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_Location/Tools/Location_Data_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_Location/Tools/Location_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_Location/Tools/Location_Data_ViewItem.aspx";
        public const string PAGEURL_DATA_EDITITEM = "~/CMSModules/CMS_Location/Tools/Location_Data_EditItem.aspx";
        public const string PAGEURL_LOCATION_DASHBOARD = "~/CMSModules/CMS_Location/Dashboard/Dashboard.aspx";


        #endregion

        #region Queries 	
        public const string QUERY_GETLOCATIONS_CONTAINS = "customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetLocations_contains";
        public const string QUERY_GETLOCATIONS_NEAREST = "customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetLocations_nearest";
        #endregion

        #region Session keys
        
        public const string SESSION_LOCATIONS = "customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetLocations_nearest";

        #endregion

        #region "Custom Table Field Names"
        #region Location
        public const string FIELDS_LOCATION_LATITUDE = "Latitude";
        public const string FIELDS_LOCATION_LONGITUDE = "Longitude";
        public const string FIELDS_LOCATION_HOURS = "Hours";
        public const string FIELDS_LOCATION_FEATUREICON = "FeatureIcon";
        
        #endregion
        #endregion
    }
}
