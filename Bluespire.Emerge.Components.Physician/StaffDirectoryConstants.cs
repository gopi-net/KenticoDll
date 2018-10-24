using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.StaffDirectory
{
  public static class StaffDirectoryConstants
    {
      public const string STAFF_TYPE = "StaffType";

      public const string CUSTOMTABLE_CODENAME_SD_STAFF = "customtable.Emerge_{0}_SD_Staff";
      public const string FIRST_NAME = "FirstName";
      public const string LAST_NAME = "LastName";
      public const string MIDDLE_NAME = "MiddleName";
      public const string NAME = "Name";
      public const string ItemID = "ItemID";
      public const string PAGE_TITLE_SEPERATOR = "-";
      public const string KEYWORDS = "keywords";
      public const string DESCRIPTION = "description";
      public const string METADESCRIPTION = "MetaDescription";
      public const string TEXTBOX_SEPERATOR= "_txt";
      public const string LINKBUTTON_SEPERATOR = "_lnk";
      public const string ITEM_CREATED_WHEN = "ItemCreatedWhen";
      public const string CUSTOMTABLE_CODENAME_SEARCHCONFIG = "customtable.Emerge_{0}_SD_ExtraSearchConfig";
      public const string STAFF_COLUMN_STAFFTYPE = "StaffType";
      public const string STAFF_SESSION_SEARCH_PARAMETERS = "STAFF_SESSION_SEARCH_PARAMETERS";
      public const string MILES_CONTROL_ID = "Miles";
      public const string ZIP_CONTROL_ID = "Zip";
      public const string SEARCHCONFIG_COLUMN_RELATEDTABLE = "RelatedTable";
      public const string SEARCHCONFIG_COLUMN_SEARCHABLECOLUMN = "SearchableColumn";

      public const string STRINGCODE_STAFFDIRECTORYHOME = "Emerge.SD.Dashboard";
      public const string PAGEURL_STAFFDIRECTORY_DASHBOARD = "~/CMSModules/CMS_StaffDirectory/Dashboard/Dashboard.aspx";

      public const string CUSTOMTABLE_QUERY_GET_STAFF = "customtable.Emerge_{0}_SD_Staff.GetPhysicans";

      #region Queries

      public const string QUERY_GETLOCATIONS_NEAREST = "customtable.Emerge_{0}_SD_StaffFacility.Query_Facility_GetLocations_nearest";
      #endregion
    }
}
