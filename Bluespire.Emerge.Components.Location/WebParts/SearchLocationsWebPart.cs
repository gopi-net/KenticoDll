using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.MediaLibrary;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Common.CMS.WebAnalytics;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using System.Web.Script.Serialization;
using System.Web.UI;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Common.Logging;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Components.Location.WebParts
{

    public class SearchLocationsWebPart : LocationWebPart
    {

        protected const string ZIPCODE_CONTROL_ID = "Zipcode";
        protected const string LOCATIONREPEATER_CONTROL_ID = "LocationRepeater";

        CMSRepeater LocationRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(LOCATIONREPEATER_CONTROL_ID); } }

        #region "Webpart Properties"
        public string TransformationName
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("TransformationName"), string.Empty);
            }
            set
            {
                LocationRepeater.TransformationName = value;
                SetValue("TransformationName", value);
            }
        }

        public int ShowLocationsWithinRadius
        {
            get
            {
                return EmergeValidationHelper.GetInteger(GetValue("ShowLocationsWithinRadius"), 10000);
            }
            set
            {
                SetValue("ShowLocationsWithinRadius", value);
            }
        }

        public bool IsSearchNearestLocationsToZipcode
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("SearchNearestLocations"), "none").ToLower().Equals("zipcode") ? true : false;
            }
        }

        public bool IsSearchNearestLocationsToUsersIPAddress
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("SearchNearestLocations"), "none").ToLower().Equals("useripaddress") ? true : false;
            }
        }

        public bool IsSearchNearestLocation
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("SearchNearestLocations"), "none").ToLower().Equals("none") ? false : true;
            }
        }

        public string SearchNearestLocations
        {
            set
            {
                SetValue("SearchNearestLocations", value);
            }
        }

        public string ToolTipColumnOnGmapMarker
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("ToolTipColumnOnGmapMarker"), string.Empty);
            }
            set
            {
                SetValue("ToolTipColumnOnGmapMarker", value);
            }
        }

        public string ColumnsToBeDisplayOnGmapMarker
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("ColumnsToBeDisplayOnGmapMarker"), "LocationName,Address,Hours");
            }
            set
            {
                SetValue("ColumnsToBeDisplayOnGmapMarker", value);
            }
        }

        public string DefaultIconGUIDForGmap
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("DefaultIconGUIDForGmap"), string.Empty);
            }
            set
            {
                SetValue("DefaultIconGUIDForGmap", value);
            }
        }

        public string ResourceStringForHtmlInfoDisplayedOnGmapMarker
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("ResourceStringForHtmlInfoDisplayedOnGmapMarker"), LocationConstants.STRINGCODE_LOCATION_GMAPMARKERINFOHTML);
            }
            set
            {
                SetValue("ResourceStringForHtmlInfoDisplayedOnGmapMarker", value);
            }
        }
        #endregion "Webpart Properties"

        #region "Protected Methods"
        protected DataTable LoadLocations(string whereCondition)
        {
            DataSet dsLocations = new DataSet();
            if (EmergeSessionHelper.GetValue(LocationConstants.SESSION_LOCATIONS) == null)
            {
                if (IsSearchNearestLocation)
                {

                    try
                    {
                        EmergeGeoLocation location = GetNearestLocation();
                        dsLocations = GetLocations(location, whereCondition);
                    }
                    catch (NearestLocationNotFoundException)
                    {
                        
                        dsLocations = GetLocations(whereCondition);
                        
                    }
                }
                else
                {
                    dsLocations = GetLocations(whereCondition);
                }

                EmergeSessionHelper.SetValue(LocationConstants.SESSION_LOCATIONS, dsLocations.Tables[0]);
            }

            return (DataTable)EmergeSessionHelper.GetValue(LocationConstants.SESSION_LOCATIONS);
        }

        protected string GetWhereCondition()
        {

            string whereCondition = string.Empty;

            foreach (Control control in ControlPanel.Controls.OfType<WebControl>())
            {
                if (control is EmergeTextBox)
                {
                    if (!string.IsNullOrEmpty(((EmergeTextBox)control).MappedToCustomTableColumns) && !string.IsNullOrEmpty(((EmergeTextBox)control).Text.Trim()))
                    {
                        List<string> columns = ((EmergeTextBox)control).MappedToCustomTableColumns.Split(',').ToList();

                        whereCondition += ((EmergeTextBox)control).MappedToCustomTableColumns.Replace(",", " + ");
                        whereCondition += " like " + "'%" + ((EmergeTextBox)control).Text.Trim() + "%' " + Constants.WHERE_CONDITION_OPERATOR_AND;

                    }
                    else if (!string.IsNullOrEmpty(((EmergeTextBox)control).Text.Trim()))
                        whereCondition += ((EmergeTextBox)control).ID + " like " + "'%" + ((EmergeTextBox)control).Text.Trim() + "%'" + Constants.WHERE_CONDITION_OPERATOR_AND;

                    continue;
                }

                if (control is TextBox)
                {
                    if (!string.IsNullOrEmpty(((TextBox)control).Text.Trim()))
                    {
                        if (control.ID != ZIPCODE_CONTROL_ID || (control.ID == ZIPCODE_CONTROL_ID && !IsSearchNearestLocationsToZipcode))
                            whereCondition += ((TextBox)control).ID + " like " + "'%" + ((TextBox)control).Text.Trim() + "%'" + Constants.WHERE_CONDITION_OPERATOR_AND;
                    }
                    continue;
                }

                if (control is DropDownList)
                {
                    if (((DropDownList)control).SelectedIndex > 0)
                        whereCondition += ((DropDownList)control).ID + " = " + "'" + ((DropDownList)control).SelectedValue.ToString() + "'" + Constants.WHERE_CONDITION_OPERATOR_AND;
                    continue;
                }
                if (control is RadioButtonList)
                {
                    if (((RadioButtonList)control).SelectedIndex > 0)
                        whereCondition += ((RadioButtonList)control).ID + " = " + "'" + ((RadioButtonList)control).SelectedValue.ToString() + "'";
                    continue;
                }

                if (control is CheckBox)
                {
                    if (((CheckBox)control).Checked)
                        whereCondition += ((CheckBox)control).ID + " = 1 " + Constants.WHERE_CONDITION_OPERATOR_AND;
                    continue;
                }

            }

            if (whereCondition.EndsWith(Constants.WHERE_CONDITION_OPERATOR_AND))
            {
                whereCondition = whereCondition.Substring(0, whereCondition.LastIndexOf(Constants.WHERE_CONDITION_OPERATOR_AND));

            }

            if (!string.IsNullOrEmpty(whereCondition.Trim()))
            {
                whereCondition = " AND " + whereCondition;
            }


            return whereCondition;
        }

        protected string GetJSONLocations()
        {
            DataTable dtLocations = (DataTable)EmergeSessionHelper.GetValue(LocationConstants.SESSION_LOCATIONS);
            ArrayList arrLocations = new ArrayList();



            bool IsTitleColumnExists = dtLocations.Columns.Contains(ToolTipColumnOnGmapMarker) ? true : false;

            if (dtLocations != null && dtLocations != null)
            {
                for (int i = 0; i < dtLocations.Rows.Count; i++)
                {

                    DataRow rwLocation = dtLocations.Rows[i];
                    ArrayList arrLocation = new ArrayList();


                    arrLocation.Add(rwLocation[Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME]); //index = 0
                    arrLocation.Add((i + 1) * 10); //index = 1
                    arrLocation.Add(rwLocation[LocationConstants.FIELDS_LOCATION_LATITUDE]); //index = 2
                    arrLocation.Add(rwLocation[LocationConstants.FIELDS_LOCATION_LONGITUDE]); //index = 3

                    if (IsTitleColumnExists)
                        arrLocation.Add(rwLocation[ToolTipColumnOnGmapMarker]); //index = 4
                    else
                        arrLocation.Add(rwLocation[LocationConstants.FIELDS_LOCATION_HOURS]); //index = 4

                    List<string> ValuesToBeDisplayOnGmapMarker = new List<string>();


                    foreach (string column in ColumnsToBeDisplayOnGmapMarker.Split(',').ToList())
                    {
                        if (!string.IsNullOrEmpty(column))
                            ValuesToBeDisplayOnGmapMarker.Add(rwLocation[column].ToString());
                    }

                    arrLocation.Add(EmergeResHelper.GetStringFormat(ResourceStringForHtmlInfoDisplayedOnGmapMarker, ValuesToBeDisplayOnGmapMarker.ToArray())); //index = 5


                    if (!string.IsNullOrEmpty(rwLocation[LocationConstants.FIELDS_LOCATION_FEATUREICON].ToString())) //index = 6
                        arrLocation.Add(HttpUtility.UrlDecode(EmergeMediaLibraryHelper.GetMediaFileUrl(rwLocation[LocationConstants.FIELDS_LOCATION_FEATUREICON].ToString(), EmergeSiteInfoProvider.CurrentSiteName)));
                    else if (!string.IsNullOrEmpty(DefaultIconGUIDForGmap))
                        arrLocation.Add(HttpUtility.UrlDecode(EmergeMediaLibraryHelper.GetMediaFileUrl(DefaultIconGUIDForGmap, EmergeSiteInfoProvider.CurrentSiteName)));
                    else
                        arrLocation.Add(string.Empty);

                    arrLocations.Add(arrLocation.ToArray());
                }
            }


            return (new JavaScriptSerializer()).Serialize(arrLocations);
        }

        protected void BindLocations(DataTable locations)
        {
            LocationRepeater.DataSource = locations;
            LocationRepeater.DataBind();

        }
        #endregion "Protected Methods"

        #region "Private Methods"
        /// <summary>
        /// Returns all locations which satisfies whereCondition.
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        private DataSet GetLocations(string whereCondition)
        {
            string queryName;
            queryName = string.Format(LocationConstants.QUERY_GETLOCATIONS_CONTAINS, EmergeSiteInfoProvider.CurrentSiteName);
            Hashtable hashedQueryParameters = new Hashtable();
            hashedQueryParameters.Add("@WhereCondition", whereCondition);

            return EmergeSqlHelperClass.ExecuteQuery(queryName, hashedQueryParameters, null, null);
        }

        /// <summary>
        /// This Function will return nearest location to "entered zipcode"  provided that Zipcode field is available and  SearchNearestLocationsToZipcode is checked on webpart. 
        /// If Zipcode is blank along with SearchNearestLocationsToZipcode is checked  OR SearchNearestLocationsToUsersIpAddress on webpart is checked then this will return nearest location to users IP Address.
        /// If no nearest location found then null is return. 
        /// </summary>
        /// <returns>GeoLocation</returns>
        private EmergeGeoLocation GetNearestLocation()
        {

            EmergeGeoLocation location = new EmergeGeoLocation();

            if (null != ControlPanel.FindControl(ZIPCODE_CONTROL_ID) && IsSearchNearestLocationsToZipcode && !String.IsNullOrEmpty(((TextBox)ControlPanel.FindControl(ZIPCODE_CONTROL_ID)).Text.Trim()))
            {
                try
                {
                    location = GeocodingAPIHelper.GetGeoLocation(((TextBox)ControlPanel.FindControl(ZIPCODE_CONTROL_ID)).Text.Trim());

                }
                catch (GeocodingServiceException)
                {
                }
            }
            else if (IsSearchNearestLocationsToUsersIPAddress )
            {
                location = GetClientLocation();
            }

            if (location.Latitude == 0.0 && location.Longitude == 0.0) throw new NearestLocationNotFoundException();
            return location;
        }

        /// <summary>
        /// Retruns locations along with the distance from the provided longitude and latitude
        /// </summary>
        /// <param name="longitude">longitude</param>
        /// <param name="latitude">latitude</param>
        /// <param name="whereCondition"></param>
        /// <returns>DataSet</returns>
        //  private DataSet GetLocations(double longitude, double latitude, string whereCondition)
        private DataSet GetLocations(EmergeGeoLocation location, string whereCondition)
        {
            string queryName;


            queryName = string.Format(LocationConstants.QUERY_GETLOCATIONS_NEAREST, EmergeSiteInfoProvider.CurrentSiteName);


            Hashtable hashedQueryParameters = new Hashtable();
            hashedQueryParameters.Add("@Longitude", location.Longitude);
            hashedQueryParameters.Add("@Latitude", location.Latitude);
            hashedQueryParameters.Add("@Radius", Convert.ToDouble( ShowLocationsWithinRadius));
            hashedQueryParameters.Add("@WhereCondition", whereCondition);


            return EmergeSqlHelperClass.ExecuteQuery(queryName, hashedQueryParameters, null, null);
        }

        private EmergeGeoLocation GetClientLocation()
        {
            string ipaddress = GetClientIpAddress();
           
            return EmergeGeoIPHelper.GetLocationByIp(ipaddress);
        }

        private string GetClientIpAddress()
        {
            HttpRequest currentRequest = HttpContext.Current.Request;
            string ipAddress = currentRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (ipAddress == null || ipAddress.ToLower() == "unknown")
                ipAddress = currentRequest.ServerVariables["REMOTE_ADDR"];
            //ipAddress = "38.113.82.109";
          
            return ipAddress;
        }

        #endregion "Private Methods"
    }
}

