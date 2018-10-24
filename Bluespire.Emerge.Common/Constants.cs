using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Common
{
    /// <summary>
    /// Class declared to include Constants
    /// </summary>
    /// 

    public static class Constants
    {
        public enum Modules
        {
            Rates,
            StaffDirectory,
            EventsCalendar,
            CheerCard,
            GiftShop,
            Career,
            Maintenance,
            License,
            Location,
            Donation,
            HistoryTracker,
            PreRegistration
            
        }

        public enum Environments
        {
            Mobile,
            Desktop
        }

        public enum GridActions
        {
            Activate,
            Deactivate,
            Delete,
            Moveup,
            Movedown,
            DeleteLicense
        }

        #region PageNames
        
        #region Maintenance
        public const string MAINTENANCE_LISTPAGE = "MaintenanceListPage";
        public const string MAINTENANCE_DATAVIEWITEMPAGE = "MaintenanceDataViewItemPage";
        public const string MAINTENANCE_DATASELECTFIELDSPAGE = "MaintenanceDataSelectFieldsPage";
        public const string MAINTENANCE_DATALISTPAGE = "MaintenanceDataListPage";
        public const string MAINTENANCE_DATAEDITITEMPAGE = "MaintenanceDataEditItemPage";
        #endregion

        #region Events Calendar
        public const string EVENTSCALENDAR_DASHBOARDPAGE = "EventsCalendarDashboardPage";
        public const string EVENTSCALENDAR_LISTPAGE = "EventsCalendarListPage";
        public const string EVENTSCALENDAR_DATAVIEWITEMPAGE = "EventsCalendarDataViewItemPage";
        public const string EVENTSCALENDAR_DATASELECTFIELDSPAGE = "EventsCalendarDataSelectFieldsPage";
        public const string EVENTSCALENDAR_DATALISTPAGE = "EventsCalendarDataListPage";
        public const string EVENTSCALENDAR_DATAEDITITEMPAGE = "EventsCalendarDataEditItemPage";
        #endregion

        #region Cheer Card

        #endregion

        #region License
        public const string LICENSE_LISTPAGE = "LicenseListPage";
        public const string LICENSE_DATAVIEWITEMPAGE = "LicenseDataViewItemPage";
        public const string LICENSE_DATASELECTFIELDSPAGE = "LicenseDataSelectFieldsPage";
        public const string LICENSE_DATALISTPAGE = "LicenseDataListPage";
        public const string LICENSE_DATAEDITITEMPAGE = "LicenseDataEditItemPage";
        #endregion 

        #region HistoryTracker
        public const string HISTORYTRACKER_LISTPAGE = "HistoryTrackerListPage";
        public const string HISTORYTRACKER_DATAVIEWITEMPAGE = "HistoryTrackerDataViewItemPage";
        public const string HISTORYTRACKER_DATASELECTFIELDSPAGE = "HistoryTrackerDataSelectFieldsPage";
        public const string HISTORYTRACKER_DATALISTPAGE = "HistoryTrackerDataListPage";
        public const string HISTORYTRACKER_DATAEDITITEMPAGE = "HistoryTrackerDataEditItemPage";
        #endregion

        public const string STAFF_DIRECTORY_DASHBOARDPAGE = "StaffDirectoryDashboardPage";
        public const string STAFF_DIRECTORY_LISTPAGE = "StaffDirectoryListPage";
        public const string STAFF_DIRECTORY_DATAVIEWITEMPAGE = "StaffDirectoryDataViewItemPage";
        public const string STAFF_DIRECTORY_DATASELECTFIELDSPAGE = "StaffDirectoryDataSelectFieldsPage";
        public const string STAFF_DIRECTORY_DATALISTPAGE = "StaffDirectoryDataListPage";
        public const string STAFF_DIRECTORY_DATAEDITITEMPAGE = "StaffDirectoryDataEditItemPage";

        #region Donation
        public const string DONATION_DASHBOARDPAGE = "DonationDashboardPage";
        public const string DONATION_LISTPAGE = "DonationListPage";
        public const string DONATION_DATAVIEWITEMPAGE = "DonationDataViewItemPage";
        public const string DONATION_DATASELECTFIELDSPAGE = "DonationDataSelectFieldsPage";
        public const string DONATION_DATALISTPAGE = "DonationDataListPage";
        public const string DONATION_DATAEDITITEMPAGE = "DonationDataEditItemPage";
        #endregion

        #region Pre-Registration
        public const string PREREGISTRATION_DASHBOARDPAGE = "PreRegistrationDashboardPage";
        public const string PREREGISTRATION_LISTPAGE = "PreRegistrationListPage";
        public const string PREREGISTRATION_DATAVIEWITEMPAGE = "PreRegistrationDataViewItemPage";
        public const string PREREGISTRATION_DATASELECTFIELDSPAGE = "PreRegistrationDataSelectFieldsPage";
        public const string PREREGISTRATION_DATALISTPAGE = "PreRegistrationDataListPage";
        public const string PREREGISTRATION_DATAEDITITEMPAGE = "PreRegistrationDataEditItemPage";
        #endregion

        #endregion

        #region Constants Added for Kentico 8 Upgrade
        public const int KENTICO_MAX_INT_LENGTH = 11;
        public const int KENTICO_MAX_LONGINT_LENGTH = 20;
        public const int KENTICO_MAX_NVARCHAR_LENGTH = 4000;
        public const string KENTICO_EMAILOBJECTTYPE_EMAILTEMPLATE = "cms.emailtemplate";
        #endregion

        public const string EMERGE_CACHE_CLEAR_KEY_SUFFIX = "_TOUCH";
        public const string EMERGE_TABLE_CACHED_TABLES = "customtable.Emerge_{0}_Common_CachedTables";
        public const string EMERGE_COLUMN_CACHED_TABLENAME = "CUSTOMTABLE";

        public const string EMERGE_PHYSICIAN = "Emerge_Physician";
        public const string QUERYSTRING_PREFIX = "emgr_";
        public const string DROPDOWN_DEFAULT_TEXT = "--Select--";
        public const string DROPDOWN_DEFAULT_VALUE = "";
        public const string LICENSE_CUSTOM_TABLE_CODE_NAME = "customtable.Emerge_{0}_LIC_License_Details";
        public const string LICENSE_KEY_CUSTOM_TABLE_COLUMN_NAME = "LicenseKey";
        public const int LICENSE_KEY_CACHE_EXPIRATION_TIME = 20; 
        public const string LICENSE_KEY_CACHE_KEY_SUFFIX = "Key";
        public const string LICENSED_MODULES_CACHE_KEY_SUFFIX = "Modules";
        public const string WHERECONDITION_FOR_LICENSE_KEY = " DomainName = '{0}' AND IsActive = 1 ";
        public const string ORDERBY_FOR_LICENSE_KEY = " ItemCreatedWhen Desc ";

        public const int MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY = 200;
        public const int MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY = 200;
        public const string GROUP_CONTROL_CODE_NAME = "cms.emergegroupcontrol";
        public const string DROPDOWNLISTCONTROL = "dropdownlistcontrol";
        public const string GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY = "SelectorCustomTableName";
        public const string GROUP_CONTROL_RELATIONCOLUMNNAME_PROPERTY = "RelationColumnName";
        
        public const string GROUP_CONTROL_EXPORT_DELIMITER = "|";
        public const string MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME = "cms.emergemediafileuploadercontrol";
        public const string MEDIA_LIBRARY_PREFIX = "Emerge_";

        public const string EXCEPTION_ERROR_PAGE_URL_ADMIN = "~/CMSMessages/Error.aspx";
        public const string UNHNADLED_EXCEPTION_ERROR_PAGE_ADMIN = "~/CMSModules/CMS_EmergeCommon/Error.aspx";
        
        public const string CONTROL_ENCRYPTEDFIELD = "EncryptedTextField";
        public const string CONTROL_ENCRYPTEDFIELDLABLE = "EncryptedLabelField";

        public const string WHERECONDITION_MAINTENANCE_LIST = "AND ClassName like '%[_]MTN[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_CHEERCARD_LIST = "AND ClassName like '%[_]CC[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_LICENSE_LIST = "AND ClassName like '%[_]LIC[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_EVENTSCALENDAR_LIST = "AND ClassName like '%[_]EC[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_STAFF_DIRECTORY_LIST = "AND ClassName like '%[_]SD[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_RATES_LIST = "AND ClassName like '%[_]RT[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_GIFTSHOP_LIST = "AND ClassName like '%[_]GS[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_CAREER_LIST = "AND ClassName like '%[_]CR[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_LOCATION_LIST = "AND ClassName like '%[_]LOC[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_DONATION_LIST = "AND ClassName like '%[_]DN[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_PREREGISTRATION_LIST = "AND ClassName like '%[_]PR[_]%' AND ClassName not like '%[_]INTERN'";
        public const string WHERECONDITION_HISTORYTRACKER_LIST = "AND ClassName like '%[_]HT[_]%' AND ClassName not like '%[_]INTERN'";
        public const string CONFIGURATION_FOLDER = "bin\\Configurations";
        public const string UNITY_SECTION = "unity";
        
        #region "Module Code Names"

        public const string RATES_MODULE_CODE_NAME = "CMS.Rates";
        public const string STAFF_DIRECTORY_MODULE_CODE_NAME = "CMS.StaffDirectory";
        public const string EVENTS_CALENDAR_MODULE_CODE_NAME = "CMS.EventsCalendar";
        public const string CHEER_CARD_MODULE_CODE_NAME = "CMS.CheerCard";
        public const string MAINTENANCE_MODULE_CODE_NAME = "CMS.Maintenance";
        public const string LICENSE_MODULE_CODE_NAME = "CMS.License";
        public const string PRE_REGISTRATION_MODULE_CODE_NAME = "CMS.EmergeWebParts";
        public const string ONLINE_BILLPAY_MODULE_CODE_NAME = "CMS.EmergeWebParts";
        public const string GIFTSHOP_MODULE_CODE_NAME = "CMS.GiftShop";
        public const string CAREER_MODULE_CODE_NAME = "CMS.Career";
        public const string DONATION_MODULE_CODE_NAME = "CMS.Donation";
            
        public const string LOCATION_MODULE_CODE_NAME = "CMS.Location";
        public const string PREREGISTRATION_MODULE_CODE_NAME = "CMS.PreRegistration";
        public const string HISTORYTRACKER_MODULE_CODE_NAME = "CMS.HistoryTracker";    
            
        #endregion "Module Code Names"

        
        public const string PERMISSIONS_CONFIG_FILE_PATH = @"~/CMSModules/CMS_EmergeCommon/ConfigurationFiles/Permissions.xml";
        public const string PERMISSIONS_CONFIG_ROOT_NODE_NAME = "Permissions";
        public const string PERMISSIONS_CONFIG_PAGE_PERMISSION_NODE_NAME = "Permission";
        public const string PERMISSIONS_PAGENAME = "PageName";
        public const string PERMISSIONS_PAGEPERMISSIONS = "PagePermissions";
        public const string PERMISSIONS_SEPARATOR = "|";
        public const string PERMISSIONS_CONFIG_DOCUMENT_CACHE_KEY_NAME = "Emerge.Permissions.Config";
        public const int PERMISSIONS_CONFIG_DOCUMENT_CACHE_EXPIRATION_TIME = 20;

        #region "Action Permission"
        //public const string ACTION_PERMISSIONS_CONFIG_FILE_PATH = @"C:\Emerge2.0\Emerge.Sprint-02\EmergeWebsite\CMSModules\CMS_EmergeCommon\ConfigurationFiles\ActionPermissions.xml";
        public const string ACTION_PERMISSIONS_CONFIG_FILE_PATH = @"~/CMSModules/CMS_EmergeCommon/ConfigurationFiles/ActionPermissions.xml";
        public const string ACTION_PERMISSIONS_CONFIG_ROOT_NODE_NAME = "Permissions";
        public const string ACTION_PERMISSIONS_CONFIG_PAGE_PERMISSION_NODE_NAME = "Permission";
        public const string ACTION_PERMISSIONS_ACTIONNAME = "ActionName";
        public const string ACTION_PERMISSIONS_PERMISSIONNAME = "PermissionName";
        public const string ACTION_PERMISSIONS_SEPARATOR = "|";
        public const string ACTION_PERMISSIONS_CONFIG_DOCUMENT_CACHE_KEY_NAME = "Emerge.Action.Permissions.Config";
       
        #endregion


        public const string EMERGE_UNIGRID_CONFIGFILE = "~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.xml";
        public const string EMERGE_UNIGRID_DEFAULT_ACTIONS_MENU = "~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/EmergeUniGridMenu.ascx";
        public const string MODULES_EXCLUDED_FROM_LICENSE_CHECK = "License;HistoryTracker;Maintenance";
        public const string MODULES_EXCLUDED_FROM_LICENSE_CHECK_SEPARATOR = ";";

        public const string FORM_REQUIRED_FIELD_ASTERISK = "<span style='color:red;'>* </span>";

        #region
        public const string CUSTOM_TABLE_STATUS_COLUMNNAME = "IsActive";
       

       
        
        #endregion
        
        #region Resource String Constants
        public const string MESSAGE_EDITITEMPAGENOTFOUND = "Message_EditItemPageNotFound";
        public const string MESSAGE_NEWITEMPAGENOTFOUND = "Message_NewItemPageNotFound";
        public const string MESSAGE_VIEWITEMPAGENOTFOUND = "Message_ViewItemPageNotFound";
        public const string CUSTOMTABLEITEMDOESNOTEXISTS = "Emerge.CustomTableItemDoesNotExists";
        public const string CUSTOMTABLEDOESNOTEXISTS = "Emerge.CustomTableDoesNotExists";
        public const string CUSTOMTABLEITEMS_BYCRITERIA_DOESNOTEXISTS = "Emerge.Exception.ErrorMessage.CustomTableItemsByCriteriaDoesNotExists";
        #endregion


        #region "Relationship master table"
        public const string CUSTOM_TABLE_RELATION_MASTER = "customtable.Emerge_{0}_CustomTableRelationMaster";
        public const string CUSTOM_TABLE_RELATION_MASTER_RELATION_NAME = "RelationName";
        public const string CUSTOM_TABLE_RELATION_MASTER_SKIP_VALIDATION = "SkipValidation";
        public const string CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE = "PrimaryTableName";
        public const string CUSTOM_TABLE_RELATION_MASTER_PRIMARY_COLUMN = "PrimaryPkColumnName";
        public const string CUSTOM_TABLE_RELATION_MASTER_PRIMARY_DISPLAY_COLUMNS = "PrimaryDisplayColumnNames";
        public const string CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE = "ForeignTableName";
        public const string CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE_COLUMN = "ForeignTableColumnName";
        public const char MULTI_VALUE_SEPERATOR = '|';
        public const char COMMA_SEPERATOR = ',';

        #endregion 

       
        public const string DELIMITER_IN_LIST_VALUES = "|";
        public const string TEMP_IMAGES_FOLDER_NAME = "~/temp_images";

        public const string DEFAULT_SENDER_ADDRESS = "{0}@Noreply.com";

        public const string EMERGE_WEBSITE = "EmergeWebsite";

        #region "Control Types"

        public const string TYPE_TEXTBOX = "System.Web.UI.WebControls.TextBox";
        public const string TYPE_DROPDOWNLIST = "System.Web.UI.WebControls.DropDownList";
        public const string TYPE_LISTBOX = "System.Web.UI.WebControls.ListBox";
        public const string TYPE_RADIOBUTTON = "System.Web.UI.WebControls.RadioButton";
        public const string TYPE_RADIOBUTTONLIST = "System.Web.UI.WebControls.RadioButtonList";
        public const string TYPE_CHECKBOX = "System.Web.UI.WebControls.CheckBox";
        public const string TYPE_CHECKBOXLIST = "System.Web.UI.WebControls.CheckBoxList";

        public const string TYPE_CMS_TEXTBOX = "CMS.Base.Web.UI.CMSTextBox";
        public const string TYPE_LOCALIZED_DROPDOWNLIST = "CMS.Base.Web.UI.LocalizedDropDownList";
        public const string TYPE_LOCALIZED_RADIOBUTTONLIST = "CMS.Base.Web.UI.LocalizedRadioButtonList";
        public const string TYPE_LOCALIZED_RADIOBUTTON = "CMS.Base.Web.UI.LocalizedRadioButton";
        public const string TYPE_LOCALIZED_CHECKBOX = "CMS.Base.Web.UI.LocalizedCheckBox";
        #endregion

        public const string IMAGE_PLACE_HOLDER_KEY_EMAILTEMPLATE = "ImageName_{0}"; 

        public const string CUSTOM_TABLE_MODULE_NAME = "cms.customtables";
        #region "Where Condition"
        public const string WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS = " " + CUSTOM_TABLE_STATUS_COLUMNNAME + " = 1";
        public const string WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_INACTIVE_STATUS = " " + CUSTOM_TABLE_STATUS_COLUMNNAME + " =  0";
        public const string WHERE_CONDITION_SINGLE_QUOTE = "'";
        public const string WHERE_CONDITION_OPERATOR_AND = " AND ";
        public const string WHERE_CONDITION_OPERATOR_EQUAL = " = ";
        public const string WHERE_CONDITION_OPERATOR_OR = " OR ";
        #endregion "Where Condition"

        #region HistoryTracker
        public const string CUSTOMTABLE_CODENAME_CONTAINING_TABLESEXCLUDED_FROM_HISTORYTRACKING = "customtable.Emerge_{0}_HT_ExcludedTables";
        public const string CUSTOMTABLE_CODENAME_FOR_HISTORYTRACKER = "customtable.Emerge_{0}_HT_HistoryTracker";
        public const string QUERY_DELETELOGGEDHISTORYDETAILS = "customtable.Emerge_{0}_HT_HistoryTracker.Query_HT_DeleteHistoryDetails";

        public const string FIELDS_HISTORYTRACKER_CUSTOMTABLEBEINGCHANGED = "CustomTableName";
        public const string FIELDS_HISTORYTRACKER_DISPLAYNAMEOFCUSTOMTABLEBEINGCHANGED = "CustomTableDisplayName";
        public const string FIELDS_HISTORYTRACKER_COLUMNNAMEOFTABLEBEINGCHANGED = "ColumnName";
        public const string FIELDS_HISTORYTRACKER_OLDVALUE = "OldValue";
        public const string FIELDS_HISTORYTRACKER_NEWVALUE = "NewValue";
        public const string FIELDS_HISTORYTRACKER_DESCRIPTION = "Description";
        public const string FIELDS_HISTORYTRACKER_GROUPGUID = "GroupGUID";
        public const string FIELDS_HISTORYTRACKER_ITEMIDOFITEMBEINGCHANGED  ="ItemIDOfItemBeingChanged";
 

        public const string DESCRIPTION_FOR_NEW_ITEM = "NEW";
        public const string DESCRIPTION_FOR_UPDATE_ITEM = "OLD VALUE => NEW VALUE";
        public const string DESCRIPTION_FOR_DELETE_ITEM = "DELETE";
        #endregion HistoryTracker

        public const string CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME = "ItemID";

        #region "Smart Search Transformation index type"

        public const string INDEX_TYPE_CUSTOMTABLE = "cms.customtable";
        public const string SMART_SEARCH_RESULT_URL_TABLE = "customtable.Emerge_{0}_Common_SearchResultUrls";
        public const string SMART_SEARCH_RESULT_URL_CUSTOMTABLE_COLUMN = "CustomTable";
        public const string SMART_SEARCH_RESULT_URL_COLUMN_RESULT_URL = "ResultUrl";
        public const string SMART_SEARCH_RESULT_URL_COLUMN_PARAMETER_COLUMN = "ParameterColumns";
        #endregion

        public const string EMERGE_DATEFORMAT = "MM/dd/yyyy";

        #region StringCodes
        public const string STRINGCODE_DATELESSTHANVALIDATIONMESSAGE = "Emerge.EC.DateLessThanValidationMessage";
        public const string STRINGCODE_DATEGREATERTHANVALIDATIONMESSAGE = "Emerge.EC.DateGreaterThanValidationMessage";
        public const string STRINGCODE_DATEEQUALVALIDATIONMESSAGE = "Emerge.EC.DateEqualValidationMessage";
        public const string STRINGCODE_LESSTHANVALIDATIONMESSAGE = "Emerge.EC.LessThanValidationMessage";
        public const string STRINGCODE_GREATERTHANVALIDATIONMESSAGE = "Emerge.Common.GreaterThanValidationMessage";
        public const string STRINGCODE_EQUALVALIDATIONMESSAGE = "Emerge.Common.EqualValidationmessage";
        public const string STRINGCODE_DATESSELECTORMESSAGE = "Emerge.Common.DatesSelectorMessage";
        public const string STRINGCODE_COMMENT_GETDOMAINWITHPORT = "Emerge.Common.MacroMethodComment.GetDomainWithPort";
        public const string STRINGCODE_COMMENT_ISMODULELICENSED = "Emerge.Common.MacroMethodComment.IsModuleLicensed";
        public const string STRINGCODE_DATEUNIQUEVALIDATION = "Emerge.Common.DateUniqueValidation";
        public const string STRINGCODE_HT_FROMDATEEMPTYVALIDATION = "Emerge.HT.ErrorMessage.EmptyFromDate";
        public const string STRINGCODE_HT_TODATEEMPTYVALIDATION = "Emerge.HT.ErrorMessage.EmptyToDate";
        public const string STRINGCODE_HT_FROMTODATEEMPTYVALIDATION = "Emerge.HT.ErrorMessage.EmptyFromToDate";
        public const string STRINGCODE_HT_DELETEHISTORYDETAILSSUCCESSMESSAGE = "Emerge.HT.Delete.SuccessMessage";
        #endregion

        public const string CUSTOMTABLE_VIEWPAGEEXCLUDEDFIELDS = "customtable.Emerge_{0}_ViewPageExcludedFields";
        public const string FIELDS_VIEWPAGEEXCLUDEDFIELDS_COLUMNNAME = "ColumnName";
        public const string FIELDS_VIEWPAGEEXCLUDEDFIELDS_CUSTOMTABLE = "CustomTable";

        public const string CUSTOMTABLE_MANDATORYFIELDS = "customtable.Emerge_{0}_MandatoryFields";
        public const string FIELDS_MANDATORYFIELDS_CUSTOMTABLE = "CustomTable";
        public const string FIELDS_MANDATORYFIELDS_FIELDNAME = "FieldName";

        public const string HIDDENPASSWORD = "********";

        #region "Cookie Names"
        public const string COOKIE_NAME_MOBILEFULLSITELINK = "CookieFullSiteLinkClicked";
        #endregion "Cookie Names"
    }
}


