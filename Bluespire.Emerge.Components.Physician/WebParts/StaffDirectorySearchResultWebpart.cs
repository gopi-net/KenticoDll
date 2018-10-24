using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService.Caching;
using Bluespire.Emerge.Common.CMS.WebAnalytics;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.CMS;
using System.Collections;

namespace Bluespire.Emerge.Components.StaffDirectory.WebParts
{
    public class StaffDirectorySearchResultWebpart : StaffDirectoryWebPart
    {
        #region Private Properties
        public string keywords { get; set; }
        public string description { get; set; }
        public string pagetitle { get; set; }

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
        #endregion

        #region Protected Methods
        protected IEnumerable<DataRow> GetAllPhysicians()
        {
            string queryName;
            DataSet ds;
            IEnumerable<DataRow> physicians;
            queryName = string.Format(StaffDirectoryConstants.CUSTOMTABLE_QUERY_GET_STAFF, EmergeCMSContext.CurrentSiteName);
            ICacheable objCaching = new EmergeCustomQuery();
            objCaching.Key = queryName;
            ds = EmergeCacheHelper.GetData(objCaching);
            if (EmergeDataHelper.DataSourceIsEmpty(ds))
                return null;
            physicians = ds.Tables[0].AsEnumerable();
            return physicians;
        }
        protected DataTable GetPhysicians()
        {
            IEnumerable<DataRow> physicians = GetAllPhysicians();
            if (EmergeDataHelper.DataSourceIsEmpty(physicians))
                return new DataTable();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters = (Dictionary<string, string>)EmergeSessionHelper.GetValue(StaffDirectoryConstants.STAFF_SESSION_SEARCH_PARAMETERS);
            int value;
            DataTable dtPhysicians = new DataTable();
            if (parameters.ContainsKey(StaffDirectoryConstants.MILES_CONTROL_ID) && !String.IsNullOrEmpty(parameters[StaffDirectoryConstants.MILES_CONTROL_ID]) && parameters[StaffDirectoryConstants.MILES_CONTROL_ID] != "-1")
                ShowLocationsWithinRadius = Convert.ToInt32(parameters[StaffDirectoryConstants.MILES_CONTROL_ID]);
            if (parameters.ContainsKey(StaffDirectoryConstants.ZIP_CONTROL_ID) && !String.IsNullOrEmpty(parameters[StaffDirectoryConstants.ZIP_CONTROL_ID]))
            {
                EmergeGeoLocation facility = GetNearestFacility(parameters[StaffDirectoryConstants.ZIP_CONTROL_ID]);
                dtPhysicians = physicians.CopyToDataTable();
                if (facility.Latitude == 0.0 && facility.Longitude == 0.0)
                {
                    dtPhysicians.Clear();
                    physicians = dtPhysicians.AsEnumerable();
                }
                else
                {
                    DataSet dsNearestFacilities = GetFacilities(facility, string.Empty);
                    if (dsNearestFacilities.Tables.Count == 0 || (dsNearestFacilities.Tables.Count > 0 && dsNearestFacilities.Tables[0].Rows.Count == 0))
                    {
                        dtPhysicians.Clear();
                        physicians = dtPhysicians.AsEnumerable();
                    }
                    else
                    {


                        DataTable tmp = physicians.CopyToDataTable();
                        for (int drPhysician = 0; drPhysician < dtPhysicians.Rows.Count; drPhysician++)
                        {
                            string[] multipleFacilities = dtPhysicians.Rows[drPhysician]["Facility"].ToString().Split('|');
                            bool isDelete = false;

                            for (int ifacility = 0; ifacility < multipleFacilities.Count(); ifacility++)
                            {
                                if (!String.IsNullOrWhiteSpace(multipleFacilities[ifacility]))
                                {
                                    if (dsNearestFacilities.Tables[0].Select(" ItemID= " + multipleFacilities[ifacility]) == null || dsNearestFacilities.Tables[0].Select(" ItemID= " + multipleFacilities[ifacility]).Length == 0)
                                    {
                                        isDelete = true;
                                    }
                                    else
                                    {
                                        isDelete = false;
                                        break;
                                    }
                                }
                            }
                            if (isDelete)
                            {
                                (tmp.Select(" ItemID= " + dtPhysicians.Rows[drPhysician]["ItemID"]))[0].Delete();
                                tmp.AcceptChanges();
                            }

                        }
                        physicians = tmp.AsEnumerable();


                    }
                }
            }
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    if (parameter.Key.Contains(StaffDirectoryConstants.LINKBUTTON_SEPERATOR))
                        physicians = physicians.Where(x => (x[parameter.Key.Replace(StaffDirectoryConstants.LINKBUTTON_SEPERATOR, string.Empty)].ToString().StartsWith(parameter.Value.ToString(), StringComparison.CurrentCultureIgnoreCase)));
                    else if (Int32.TryParse(parameter.Value.ToString(), out value))
                    {
                        if (parameter.Key != StaffDirectoryConstants.ZIP_CONTROL_ID && parameter.Key != StaffDirectoryConstants.MILES_CONTROL_ID)
                            physicians = physicians.Where(x => (x[parameter.Key].ToString() == parameter.Value.ToString()));
                    }
                    else
                        physicians = physicians.Where(x => (x[parameter.Key].ToString().ToLower().Contains(parameter.Value.ToString().ToLower())));
                }
                physicians = physicians.Distinct(DataRowComparer.Default);
                if (physicians.Count() == 0)
                    return new DataTable();
                dtPhysicians = physicians.CopyToDataTable();
            }
            return dtPhysicians;
        }
        protected DataTable GetPhysicianDetails()
        {
            IEnumerable<DataRow> physicians = GetAllPhysicians();
            if (EmergeDataHelper.DataSourceIsEmpty(physicians))
                return new DataTable();
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            DataSet ds = new DataSet();
            if (EmergeQueryHelper.GetString(StaffDirectoryConstants.NAME, "") != string.Empty)
            {
                string name = EmergeQueryHelper.GetString(StaffDirectoryConstants.NAME, "").Replace(",", "").Replace("'", "").Replace(".", "").Replace("`", "");
                string[] names = name.Split('_');
                string where = string.Empty;
                if (names.Length >= 3)
                {
                    parameters.Add(StaffDirectoryConstants.FIRST_NAME, names[0]);
                    parameters.Add(StaffDirectoryConstants.MIDDLE_NAME, names[1]);
                    parameters.Add(StaffDirectoryConstants.LAST_NAME, names[2]);
                }
                else if (names.Length >= 2)
                {
                    parameters.Add(StaffDirectoryConstants.FIRST_NAME, names[0]);
                    parameters.Add(StaffDirectoryConstants.LAST_NAME, names[1]);
                }

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, string> parameter in parameters)
                    {
                        physicians = physicians.Where(x => (x[parameter.Key].ToString().ToLower().Replace(",", "").Replace("'", "").Replace(".", "").Replace("`", "") == parameter.Value.ToString().ToLower()));
                    }
                    if (physicians.Count() == 0)
                        return new DataTable();
                }
            }
            ds.Tables.Add(physicians.CopyToDataTable());
            SetMetaTags(ds);
            return ds.Tables[0];
        }
        private void SetMetaTags(DataSet ds)
        {
            if (!EmergeDataHelper.DataSourceIsEmpty(ds))
            {
                if (ds.Tables[0].Columns.Contains(StaffDirectoryConstants.FIRST_NAME))
                    pagetitle = Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.FIRST_NAME]);
                if (ds.Tables[0].Columns.Contains(StaffDirectoryConstants.MIDDLE_NAME))
                    if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.MIDDLE_NAME])))
                        pagetitle += StaffDirectoryConstants.PAGE_TITLE_SEPERATOR + Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.MIDDLE_NAME]);
                if (ds.Tables[0].Columns.Contains(StaffDirectoryConstants.LAST_NAME))
                    pagetitle += StaffDirectoryConstants.PAGE_TITLE_SEPERATOR + Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.LAST_NAME]);
                if (ds.Tables[0].Columns.Contains(StaffDirectoryConstants.KEYWORDS))
                    keywords += Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.KEYWORDS]);
                if (ds.Tables[0].Columns.Contains(StaffDirectoryConstants.METADESCRIPTION))
                    description += Convert.ToString(ds.Tables[0].Rows[0][StaffDirectoryConstants.METADESCRIPTION]);
            }
        }
        protected void RenderMetaTags()
        {
            Page page = HttpContext.Current.Handler as Page;
            HtmlHead pHtml = page.Header;

            Literal tags = (Literal)pHtml.FindControl("tags");
            string metaText = tags.Text;
            if (!string.IsNullOrEmpty(pagetitle))
            {
                page.Header.Title = pagetitle;
            }
            if (!string.IsNullOrEmpty(description))
            {

                description = "<meta name=\"description\" content=\"" + description + "\" />";
                Regex exp = new Regex(@"\<meta name=""description""[' ']*content=.*", RegexOptions.IgnoreCase);// ['name=""description""] [' ']* ['content='][.]*['/']\>", RegexOptions.IgnoreCase);
                //<meta name="description" content="Test Physician," />

                metaText = exp.Replace(metaText, "");
                metaText = metaText + "\n" + description;
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                keywords = "<meta name=\"keywords\" content=\"" + keywords + "\" />";
                Regex exp = new Regex(@"\<meta name=""keywords""[' ']*content=.*", RegexOptions.IgnoreCase);// ['name=""description""] [' ']* ['content='][.]*['/']\>", RegexOptions.IgnoreCase);
                //<meta name="description" content="Test Physician," />

                metaText = exp.Replace(metaText, "");
                metaText = metaText + "\n" + keywords;
            }
            tags.Text = metaText;
        }
        /// <summary>
        /// This Function will return nearest location to "entered zipcode"  provided that Zipcode field is available
        /// If no nearest location found then null is return. 
        /// </summary>
        /// <returns>GeoLocation</returns>
        private EmergeGeoLocation GetNearestFacility(string zip)
        {

            EmergeGeoLocation location = new EmergeGeoLocation();
            try
            {
                location = GeocodingAPIHelper.GetGeoLocation(zip.Trim());
            }
            catch (GeocodingServiceException ex)
            {

            }


            //if (location.Latitude == 0.0 && location.Longitude == 0.0) throw new NearestLocationNotFoundException();
            return location;
        }

        /// <summary>
        /// Retruns facilities along with the distance from the provided longitude and latitude
        /// </summary>
        /// <param name="longitude">longitude</param>
        /// <param name="latitude">latitude</param>
        /// <param name="whereCondition"></param>
        /// <returns>DataSet</returns>
        //  private DataSet GetFacilities(double longitude, double latitude, string whereCondition)
        private DataSet GetFacilities(EmergeGeoLocation location, string whereCondition)
        {
            string queryName;
            queryName = string.Format(StaffDirectoryConstants.QUERY_GETLOCATIONS_NEAREST, EmergeSiteInfoProvider.CurrentSiteName);
            Hashtable hashedQueryParameters = new Hashtable();
            hashedQueryParameters.Add("@Longitude", location.Longitude);
            hashedQueryParameters.Add("@Latitude", location.Latitude);
            hashedQueryParameters.Add("@Radius", Convert.ToDouble(ShowLocationsWithinRadius));
            hashedQueryParameters.Add("@WhereCondition", whereCondition);
            return EmergeSqlHelperClass.ExecuteQuery(queryName, hashedQueryParameters, null, null);
        }
        #endregion
    }
}
