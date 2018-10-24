using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.EventsCalendar.Common
{
    #region Enumerations

    public enum FrequencyType
    {
        Single,
        Weekly,
        Monthly
    }

    [Flags]
    public enum MonthlyInterval
    {
        First = 1,
        Second = 2,
        Third = 4,
        Fourth = 8,
        Fifth = 16
    }

    [Flags]
    public enum DaysOfWeek
    {
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }

    public enum BlackOutDateFilter
    {
        BookingAllowed,
        BookingNotAllowed,
        Nofilter
    }

    public enum EventScheduleType
    {
        Series,
        NonSeries
    }

    public enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public enum EventRegistrationStatus
    {
        CONFIRMED,
        CANCELLED,
        NONE
    }

    public enum RegistrationEmailMode
    {
        VOLUNTEER_INSERT,
        VOLUNTEER_INSERT_UI,
        VOLUNTEER_UPDATE,
        VOLUNTEER_CANCELLED,
        VOLUNTEER_DELETE,
        GENERAL_INSERT,
        GENERAL_INSERT_UI,
        GENERAL_UPDATE,
        GENERAL_CANCELLED,
        GENERAL_DELETE,
        CARTINSERT
    }

    public enum SaveRegistrationStatus
    {
        SUCCESS,
        VALID,
        DISCOUNTCODEUSED,
        DISCOUNTCODEUSEDINCART,
        DUPLICATEREGISTRATIONSINCART,
        REGISTRATIONLIMITREACHED,
        DUPLICATEREGISTRATIONS,
        INVALID,
        FAILED,
        INVALIDDISCOUNTCODE

    }

    public enum OccurenceStatus
    {
        VALID,
        DOESNOTEXIST,
        INVALIDVOLUNTEEROCCURENCE,
        INVALIDGENERALOCCURENCE,
        INVALIDSERIESOCCURENCE
    }

    #endregion
    
    public static class EventsConstants
    {
        #region Custom Table Names
        
        public const string CUSTOMTABLE_EVENT_OCCURENCES = "customtable.Emerge_{0}_EC_EventOccurences";
        public const string CUSTOMTABLE_EVENT_BLACKOUTDATES = "customtable.Emerge_{0}_EC_BlackOutDates";
        public const string CUSTOMTABLE_EVENT_EVENTSCHEDULE = "customtable.Emerge_{0}_EC_EventsSchedule";
        public const string CUSTOMTABLE_EVENT_EVENTREGISTRATIONS = "customtable.Emerge_{0}_EC_EventRegistrations";
        public const string CUSTOMTABLE_EVENT_EVENTSESSIONS = "customtable.Emerge_{0}_EC_Sessions";
        public const string CUSTOMTABLE_EVENT_EVENTS = "customtable.Emerge_{0}_EC_Events";
        public const string CUSTOMTABLE_EVENT_DISCOUNTDETAILS = "customtable.Emerge_{0}_EC_DiscountDetails";
        public const string CUSTOMTABLE_EVENT_CATEGORIES = "customtable.Emerge_{0}_EC_Categories";
        public const string CUSTOMTABLE_EVENT_SUBCATEGORIES = "customtable.Emerge_{0}_EC_SubCategories";
        public const string CUSTOMTABLE_EVENT_VOLUNTEERUSERS = "customtable.Emerge_{0}_EC_VolunteerUsers";
        public const string CUSTOMTABLE_EVENT_VOLUNTEERREGISTRATIONS = "customtable.Emerge_{0}_EC_VolunteerRegistration";
        public const string CUSTOMTABLE_EVENT_REGISTRATIONEMAILCONFIG = "customtable.Emerge_{0}_EC_RegistrationEmailConfig";
        public const string CUSTOMTABLE_EVENT_EVENTCARTCONFIGURATION = "customtable.Emerge_{0}_EC_EventCartConfiguration";
        

        #endregion

        #region Custom Table Fields

        #region Event Category and Sub Category
        public const string FIELDS_CATEGORY_ITEMID = "ItemID";
        public const string FIELDS_CATEGORY_CATEGORYNAME = "CategoryName";
        public const string FIELDS_CATEGORY_ACTIVE_RECORDS = "IsActive=1";

        public const string FIELDS_SUBCATEGORY_ITEMID = "ItemID";
        public const string FIELDS_SUBCATEGORY_SUBCATEGORYNAME = "SubCategoryName";
        public const string FIELDS_SUBCATEGORY_ACTIVE_RECORDS = "IsActive=1";
        #endregion
        #region Events Schedule

        public const string FIELDS_EVENTSCHEDULE_FREQUENCYTYPE = "FrequencyType";
        public const string FIELDS_EVENTSCHEDULE_SCHEDULEID = "ItemID";
        public const string FIELDS_EVENTSCHEDULE_STARTDATE = "StartDate";
        public const string FIELDS_EVENTSCHEDULE_ENDDATE = "EndDate";
        public const string FIELDS_EVENTSCHEDULE_STARTTIME = "StartTime";
        public const string FIELDS_EVENTSCHEDULE_ENDTIME = "EndTime";
        public const string FIELDS_EVENTSCHEDULE_SELECTEDDATES = "SelectedDates";
        public const string FIELDS_EVENTSCHEDULE_DAYOFWEEK = "DayOfWeek";
        public const string FIELDS_EVENTSCHEDULE_WEEKOFMONTH = "WeekOfMonth";
        public const string FIELDS_EVENTSCHEDULE_REGISTRTAIONLIMIT = "RegistrationLimit";
        public const string FIELDS_EVENTSCHEDULE_EVENTID = "EventID";
        public const string FIELDS_EVENTSCHEDULE_HASSESSIONS = "HasSessions";
        public const string FIELDS_EVENTSCHEDULE_ISPAIDSCHEDULE = "IsPaidSchedule";
        public const string FIELDS_EVENTSCHEDULE_SCHEDULETITLE = "ScheduleTitle";
        public const string FIELDS_EVENTSCHEDULE_SESSIONDETAILS = "SessionDetails";
        public const string FIELDS_EVENTSCHEDULE_HASDUPLICATEREGISTRATIONS = "HasDuplicateRegistrations";
        public const string FIELDS_EVENTSCHEDULE_ALLOWGROUPREGISTRATIONS = "AllowGroupRegistrations";
        public const string FIELDS_EVENTSCHEDULE_MINIMUMLIMIT = "MinimumLimit";
        public const string FIELDS_EVENTSCHEDULE_MAXIMUMLIMIT = "MaximumLimit";
        public const string FIELDS_EVENTSCHEDULE_COSTFORPUBLIC = "CostForPublic";
        public const string FIELDS_EVENTSCHEDULE_DISCOUNT = "Discount";
        public const string FIELDS_EVENTSCHEDULE_STATUS = "Status";
        public const string FIELDS_EVENTSCHEDULE_NEEDREGISTRATIONS = "NeedRegistrations";
        public const string FIELDS_EVENTSCHEDULE_REGISTRATIONDEADLINE = "RegistrationDeadline";
        public const string FIELDS_EVENTSCHEDULE_LOCATION = "Location";


        #endregion

        #region BlackoutDates

        public const string FIELDS_BLACKOUTDATES_BLACKOUTDATE = "BlackOutDate";
        public const string FIELDS_BLACKOUTDATES_ALLOWBOOKING = "AllowBooking";
        public const string FIELDS_BLACKOUTDATES_TITLE = "Title";
        public const string FIELDS_BLACKOUTDATES_ITEMID = "ItemID";
        

        #endregion

        #region Event Occurences
        
        public const string FIELDS_EVENTOCCURENCES_OCCURENCEDATE = "OccurenceDate";
        public const string FIELDS_EVENTOCCURENCES_SCHEDULEID = "ScheduleID";
        public const string FIELDS_EVENTOCCURENCES_STARTTIME = "StartTime";
        public const string FIELDS_EVENTOCCURENCES_ENDTIME = "EndTime";
        public const string FIELDS_EVENTOCCURENCES_REGISTRTAIONLIMIT = "RegistrationLimit";
        public const string FIELDS_EVENTOCCURENCES_ITEMID = "ItemID";
        public const string FIELDS_EVENTOCCURENCES_LOCATION = "Location";

        #endregion

        #region Event Registrations

        public const string FIELDS_EVENTREGISTRATIONS_ITEMID = "ItemID";
        public const string FIELDS_EVENTREGISTRATIONS_OCCURENCEID = "OccurenceID";
        public const string FIELDS_EVENTREGISTRATIONS_SCHEDULEID = "ScheduleID";
        public const string FIELDS_EVENTREGISTRATIONS_EVENTSTARTTIME = "EventStartTime";
        public const string FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONS = "SelectedSessions";
        public const string FIELDS_EVENTREGISTRATIONS_FIRSTNAME = "FirstName";
        public const string FIELDS_EVENTREGISTRATIONS_LASTNAME = "LastName";
        public const string FIELDS_EVENTREGISTRATIONS_EMAIL = "Email";
        public const string FIELDS_EVENTREGISTRATIONS_PHONE = "Phone";
        public const string FIELDS_EVENTREGISTRATIONS_STREETADDRESS = "StreetAddress";
        public const string FIELDS_EVENTREGISTRATIONS_CITY = "City";
        public const string FIELDS_EVENTREGISTRATIONS_STATE = "State";
        public const string FIELDS_EVENTREGISTRATIONS_ZIP = "Zip";
        public const string FIELDS_EVENTREGISTRATIONS_PAYMENTTYPE = "PaymentType";
        public const string FIELDS_EVENTREGISTRATIONS_PAYMENTDATE = "PaymentDate";
        public const string FIELDS_EVENTREGISTRATIONS_AMOUNT = "Amount";
        public const string FIELDS_EVENTREGISTRATIONS_STATUS = "Status";
        public const string FIELDS_EVENTREGISTRATIONS_COMMENTS = "Comments";
        public const string FIELDS_EVENTREGISTRATIONS_VOLUNTEERUSER = "VolunteerUser";
        public const string FIELDS_EVENTREGISTRATIONS_COSTFORPUBLIC = "CostForPublic";
        public const string FIELDS_EVENTREGISTRATIONS_DISCOUNTCODE = "DiscountCode";
        public const string FIELDS_EVENTREGISTRATIONS_SELECTEDSESSIONSDETAILS = "SelectedSessionsDetails";

        #endregion

        #region Event Sessions

        public const string FIELDS_EVENTSESSIONS_SESSIONID = "ItemID";
        public const string FIELDS_EVENTSESSIONS_TITLE = "Title";
        public const string FIELDS_EVENTSESSIONS_STARTTIME = "StartTime";
        public const string FIELDS_EVENTSESSIONS_ENDTIME = "EndTime";
        public const string FIELDS_EVENTSESSIONS_SCHEDULEID = "ScheduleID";
        public const string FIELDS_EVENTSESSIONS_OCCURENCEID = "OccurenceID";

        #endregion

        #region Events

        public const string FIELDS_EVENTS_ITEMID = "ItemID";
        public const string FIELDS_EVENTS_EVENTNAME = "EventName";
        public const string FIELDS_EVENTS_SHORTTITLE = "ShortTitle";
        public const string FIELDS_EVENTS_LONGTITLE = "LongTitle";
        public const string FIELDS_EVENTS_TEASERTEXT = "TeaserText";
        public const string FIELDS_EVENTS_EVENTDESCRIPTION = "EventDescription";
        public const string FIELDS_EVENTS_EVENTLOCATION = "EventLocation";
        public const string FIELDS_EVENTS_DEPARTMENT = "Department";
        public const string FIELDS_EVENTS_CATEGORY = "Category";
        public const string FIELDS_EVENTS_SUBCATEGORY = "subcategory";
        public const string FIELDS_EVENTS_ATTACHMENTTEXT = "AttachmentText";
        public const string FIELDS_EVENTS_WEBSITELINK = "WebsiteLink";
        public const string FIELDS_EVENTS_CONTACTNAME = "ContactName";
        public const string FIELDS_EVENTS_CONTACTEMAIL = "ContactEmail";
        public const string FIELDS_EVENTS_CONTACTPHONE = "ContactPhone";
        public const string FIELDS_EVENTS_ISSERIESEVENT = "IsSeriesEvent";
        public const string FIELDS_EVENTS_EVENTTYPE = "EventType";

        #endregion

        #region Event Schedule Discount Details
        
        public const string FIELDS_DISCOUNTS_DISCOUNTID = "ItemID";
        public const string FIELDS_DISCOUNTS_DISCOUNTCODE = "DiscountCode";
        public const string FIELDS_DISCOUNTS_DISCOUNTTYPE = "DiscountType";
        public const string FIELDS_DISCOUNTS_SCHEDULEID = "ScheduleID";
        public const string FIELDS_DISCOUNTS_DISCOUNTFACTOR = "DiscountFactor";
        
        #endregion

        #region Volunteer Users

        public const string FIELDS_VOLUNTEERUSERS_ITEMID = "ItemID";
        public const string FIELDS_VOLUNTEERUSERS_USERNAME = "UserName";
        public const string FIELDS_VOLUNTEERUSERS_FIRSTNAME = "FirstName";
        public const string FIELDS_VOLUNTEERUSERS_LASTNAME = "LastName";
        public const string FIELDS_VOLUNTEERUSERS_EMAIL = "Email";
        public const string FIELDS_VOLUNTEERUSERS_PASSWORD = "Password";
        public const string FIELDS_VOLUNTEERUSERS_USERID = "UserID";
        public const string FIELDS_VOLUNTEERUSERS_PHONE = "Phone";
        public const string FIELDS_VOLUNTEERUSERS_CITY = "City";
        public const string FIELDS_VOLUNTEERUSERS_STREETADDRESS = "StreetAddress";
        public const string FIELDS_VOLUNTEERUSERS_STATE = "State";
        public const string FIELDS_VOLUNTEERUSERS_ZIP = "Zip";
        
        #endregion

        #region Event Cart
        public const string FIELDS_EVENTCARTCONFIGURTAION_ENABLECART = "EnableCart";
        #endregion

        #endregion

        #region Unity

        public const string UNITY_CONFIGFILENAME = "EventsCalendarUnity.config";
        public const string UNITY_EVENTSCONTAINER = "EventsContainer";

        #endregion

        #region Queries
        public const string QUERY_GETEVENTS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetAllEvents";
        public const string QUERY_GETEVENTDETAILS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetEventDetails";
        public const string QUERY_GETVOLUNTEEREVENTS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetVolunteerEvents";
        public const string QUERY_GETVOLUNTEEREVENTDETAILS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetVolunteerEventDetails";
        public const string QUERY_GETSESSIONSFOROCCURENCE = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetSessionsForOccurence";
        public const string QUERY_GETSELECTEDSESSIONS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetSelectedSessions";
        public const string QUERY_GETREGISTRATIONDETAILSFOREMAIL = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetRegistrationDetailsForEmail";
        public const string QUERY_GETEVENTCARTDETAILS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetEventCartDetails";
        public const string QUERY_GETCARTEVENTREGISTRATIONS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetCartEventRegistrations";
        public const string QUERY_GETREGISTRATIONSBYSESSION = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetRegistrationsBySession";
        public const string QUERY_GETUPCOMINGEVENTS = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetUpcomingEvents";
        public const string QUERY_GETADDTOCALENDAREVENTDATA = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetAddToCalendarEventData";
        public const string QUERY_GETEVENTLOCATIONDETAILS = "customtable.Emerge_{0}_EC_Events.GetEventLocationDetails";
        public const string QUERY_GETREGISTRATIONFIELD = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetEventRegistrationField";
        public const string QUERY_GETEVENTEMAILTEMPLATE = "customtable.Emerge_{0}_EC_Events.GetEventEmailTemplates";
        public const string QUERY_GETVOLUNTEEREREGISTERED = "customtable.Emerge_{0}_EC_Events.Emerge_Query_EC_GetRegisteredVolunteer";
        #endregion

        #region Email Templates
        public const string EMAILTEMPLATE_EVENTREGISTRATION_CONFIRMATION = "Emerge_{0}_EventRegistrationConfirmation";
        public const string EMAILTEMPLATE_EVENTREGISTRATION_UPDATE = "Emerge_{0}_EventRegistrationUpdate";
        public const string EMAILTEMPLATE_EVENTREGISTRATION_CANCELLED = "Emerge_{0}_EventRegistrationCancelled";
        public const string EMAILTEMPLATE_EVENTREGISTRATION_DELETED = "Emerge_{0}_EventRegistrationDeleted";
        public const string EMAILTEMPLATE_EVENTREGISTRATION_CARTCONFIRMATION = "Emerge_{0}_CartEventRegistrationConfirmation";
        #endregion

        #region Others
        public const string STRINGCODE_ZEROOCCURENCES = "Emerge.EC.ZeroOccurencesMessage";
        public const string STRINGCODE_SCHEDULECLASH = "Emerge.EC.ScheduleClashMessage";
        public const string STRINGCODE_SCHEDULENOTSAVED = "Emerge.EC.ScheduleNotSaved";
        public const string STRINGCODE_BLACKOUTDATECLASHMESSAGE = "Emerge.EC.BlackOutDateClashMessage";
        public const string STRINGCODE_REGISTRATIONLIMITREACHED = "Emerge.EC.RegistrationLimitReachedMessage";
        public const string STRINGCODE_NOSESSIONSMESSAGE = "Emerge.EC.NoSessionsMessage";
        public const string STRINGCODE_COMMENT_ISPAIDEVENTSCHEDULE = "Emerge.EC.MacroMethodComment.IsPaidEventSchedule";
        public const string STRINGCODE_COMMENT_ISSERIESOCCURENCE = "Emerge.EC.MacroMethodComment.IsSeriesOccurence";
        public const string STRINGCODE_COMMENT_ALLOWSESSIONS = "Emerge.EC.MacroMethodComment.AllowSessions";
        public const string STRINGCODE_COMMENT_ALLOWREGISTRATIONS = "Emerge.EC.MacroMethodComment.AllowRegistrations";
        public const string STRINGCODE_REGISTRATIONSEXISTSFOREVENT = "Emerge.EC.RegistrationsExistsForEvent";
        public const string STRINGCODE_REGISTRATIONSEXISTSFOROCCURENCE = "Emerge.EC.RegistrationsExistsForOccurence";
        public const string STRINGCODE_LASTOCCURENCE = "Emerge.EC.LastEventOcurrence";
        public const string STRINGCODE_REGISTRATIONSEXISTSFORSCHEDULE = "Emerge.EC.RegistrationsExistsForSchedule";
        public const string STRINGCODE_EVENTSCHEDULE = "Emerge.EC.EventSchedule";
        public const string STRINGCODE_EVENTOCCURENCE = "Emerge.EC.EventOccurence";
        public const string STRINGCODE_EVENTHOME = "Emerge.EC.Dashboard";
        public const string STRINGCODE_REGISTRATIONMESSAGE = "Emerge.EC.RegsitrationsMessage";
        public const string STRINGCODE_OCCURENCEDROPDOWNMESSAGE = "Emerge.EC.OccurenceDropdownMessage";
        public const string STRINGCODE_OCCURENCEEDITMESSAGE = "Emerge.EC.OccurenceEditmessage";
        public const string STRINGCODE_SCHEDULEEDITMESSAGE = "Emerge.EC.ScheduleEditmessage";
        public const string STRINGCODE_VIEWREGISTRATIONMESSAGE = "Emerge.EC.ViewRegistrationsMessage";
        public const string STRINGCODE_NOSESSIONSELECTEDMESSAGE = "Emerge.EC.NoSessionSelectedMessage";
        public const string STRINGCODE_SUCCESSFULCARTREGISTRATIONCONFIRMATIONMESSAGE = "Emerge.EC.SuccessfulCartRegistrationConfirmationMessage";
        public const string STRINGCODE_SUCCESSFULCARTREGISTRATIONCONFIRMATIONMESSAGEFREE = "Emerge.EC.SuccessfulCartRegistrationConfirmationMessageFree";
        public const string STRINGCODE_FAILEDCARTREGISTRATIONCONFIRMATIONMESSAGE = "Emerge.EC.FailedCartRegistrationConfirmationMessage";
        public const string STRINGCODE_CARTPAGEURLMISSINGMESSAGE = "Emerge.EC.CartPageURLNotAvailableMessage";
        public const string STRINGCODE_COMMENTFORDISCOUNTCODEUSED = "Emerge.EC.CommentForDiscountCodeUsed";
        public const string STRINGCODE_COMMENT_GETCARTEVENTREGISTRATIONS = "Emerge.EC.CommentForGetCartEventRegistrations";
        public const string STRINGCODE_REGISTRATIONCLOSEDMESSAGE = "Emerge.EC.RegistrationsClosedMessage";
        public const string STRINGCODE_DOESNOTNEEDREGSITRATIONS = "Emerge.EC.DoesNotNeedRegistrations";
        public const string STRINGCODE_INVALIDDISCOUNTCODEMESSAGE = "Emerge.EC.InvalidDiscountCodeMessage";
        public const string STRINGCODE_REGSITRATIONSSAVEEXCEPTIONMESSAGE = "Emerge.EC.RegistrationsSaveExceptionMessage";
        public const string STRINGCODE_SESSIONCLASH = "Emerge.EC.SessionsClash";
        public const string STRINGCODE_VOLUNTEERSELECTSESSIONMESSAGE = "Emerge.EC.VolunteerSelectSessionMessage";
        public const string STRINGCODE_DUPLICATEREGISTRATIONMESSAGE = "Emerge.EC.DuplicateRegistrationMessage";
        public const string STRINGCODE_REGISTRATIONEMAILSENDMESSAGE = "Emerge.EC.RegistrationEmailSendMessage";
        public const string STRINGCODE_REGISTRATIONSUCCESSMESSAGE = "Emerge.EC.RegistrationSuccessfulMessage";
        public const string STRINGCODE_VOLUNTEERDUPLICATEREGISTRTAION = "Emerge.EC.VolunteerDuplicateRegistrationMessage";
        public const string STRINGCODE_NOTAVOLUNTEERUSER = "Emerge.EC.NotAVolunteerUser";
        public const string STRINGCODE_EVENTDETAILSNOTRETRIEVED = "Emerge.EC.EventDetailsNotRetrieved";
        public const string STRINGCODE_VOLUNTEERREGISTRATIONCONFIRMATIONMESSAGE = "Emerge.EC.VolunteerRegsitrationConfirmationMessage";
        public const string STRINGCODE_HASSCHEDULEFOREVENTCOMMENT = "Emerge.EC.HasScheduleForEventComment";
        public const string STRINGCODE_OCCURRENCEDOESNOTEXISTS = "Emerge.EC.OccurenceDoesNotExists";
        public const string STRINGCODE_BLACKOUTDATESALLOWBOOKING = "Emerge.EC.BlackoutDatesClasshesAllowBooking";
        public const string STRINGCODE_DISCOUNTCODEUSED = "Emerge.EC.DiscountCodeUsed";
        public const string STRINGCODE_REGISTRATIONLIMITREACHEDUI = "Emerge.EC.UI.RegistrationLimitReached";
        public const string STRINGCODE_CARTDISCOUNTCODEUSED = "Emerge.EC.DiscountCodeUsedInCart";
        public const string STRINGCODE_CARTDISCOUNTCODEUSEDBEFORE = "Emerge.EC.DiscountCodeUsedBefore";
        public const string STRINGCODE_CARTDUPLICATEREGISTRATIONBEFORE = "Emerge.EC.DuplicateRegistrationsBefore";
        public const string STRINGCODE_CARTDUPLICATEREGISTRATION = "Emerge.EC.CartDuplicateRegistrations";
        public const string STRINGCODE_CARTREGISTRATIONLIMITREACHED = "Emerge.EC.CartRegistrationLimitReached";
        public const string STRINGCODE_CARTINVALIDDISCOUNTCODE = "Emerge.EC.CartInvalidDiscountCode";
        public const string STRINGCODE_SESSIONDELETEFORVOLUNTEERSCHEDULE = "Emerge.EC.SessionDeleteForVolunteerSchedule";
        public const string STRINGCODE_INVALIDVOLUNTEEROCCURENCE = "Emerge.EC.InvalidVolunteerOccurence";
        public const string STRINGCODE_INVALIDGENERALOBSERVATIONOCCURENCE = "Emerge.EC.InvalidGeneralObservationOccurence";
        public const string STRINGCODE_INVAIDSERIESOCCURENCE = "Emerge.EC.InvalidSeriesOccurence";
        public const string STRINGCODE_REGISTRATIONDEADLINECLASH = "Emerge.EC.RegistrationDeadlineClash";
        public const string STRINGCODE_REGISTRATIONEXISTSFORSESSION = "Emerge.EC.RegistrationExistsForSession";
        
        #endregion

        #region OthersConstants
        public const string REGISTRATIONSPOPUP = "RegistrationPopup";
        public const string POPUPCONTROL = "PopupControl";
        public const string CUSTOMTABLESCHEDULEITEM = "CustomTableScheduleItem";
        public const string SCHEDULE = "Schedule";
        public const string OLDOCCURENCES = "OldOccurences";
        public const string NEWOCCURENCES = "NewOccurences";
        public const string DROPDOWNLISTITEMS = "DropdownlistItems";
        public const string NEWREGISTRATIONS = "NewRegistration";
        public const string REGISTRATIONINFO = "RegistrationInfo";
        public const string ROLE_VOLUNTEERUSERS = "Emerge_VolunteerUsers";
        public const string EVENTTYPE_VOLUNTEER = "VOLUNTEER";
        public const string EVENTTYPE_OBSERVATION = "OBSERVATION";
        public const string EVENTTYPE_GENERAL = "GENERAL";
        public const string USERSELECTEDSESSIONS = "UserSelectedSessions";
        public const string GENERALSELECTEDSESSIONS = "GeneralSelectedSessions";
        public const string CSSCLASS_FOR_TEXTBOX_WITH_REDBORDER = "redcolorborder";
        #endregion

        #region PageURLs

        public const string PAGEURL_LIST_EVENTSCALENDAR = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_ViewItem.aspx";
        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_List.aspx";
        public const string PAGEURL_DATA_EDITITEM = "~/CMSModules/CMS_EventsCalendar/Tools/EventsCalendar_Data_EditItem.aspx";
        public const string PAGEURL_NEW_BLACKOUTDATES = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_BlackOutDates.aspx";
        public const string PAGEURL_NEW_SCHEDULE = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_EventSchedule.aspx";
        public const string PAGEURL_NEW_EVENTS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Form.aspx";
        public const string PAGEURL_NEW_EVENTSREGISTRATIONS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_Registrations.aspx";
        public const string PAGEURL_LIST_EVENTSREGISTRATIONS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_RegistrationsList.aspx";
        public const string PAGEURL_LIST_EVENTSOCCURENCE = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_OccurenceList.aspx";
        public const string PAGEURL_LIST_EVENTS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_EventsList.aspx";
        public const string PAGEURL_LIST_EVENTSCHEDULE = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_ScheduleList.aspx";
        public const string PAGEURL_EVENTS_DASHBOARD = "~/CMSModules/CMS_EventsCalendar/Dashboard/Dashboard.aspx";
        public const string PAGEURL_NEW_VOLUNTEERUSERS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_VolunteerUsers.aspx";
        public const string PAGEURL_LIST_VOLUNTEERUSERS = "~/CMSModules/CMS_EventsCalendar/Pages/EventsCalendar_Events_VolunteerList.aspx";
        
        #endregion

        #region "Session Keys"
        public const string SESSIONKEY_EVENTSCART = "EVENTS_CART";
        public const string SESSIONKEY_REGISTRATIONINFO_CART = "EventsCartRegistartionInformation";
        public const string SESSIONKEY_CARTEVENTREGISTRATIONIDS = "CartEventRegistartionIDs";
        public const string SESSIONKEY_SAVEREGISTRATIONSTATUS = "SaveRegistrationStatus";
        #endregion

    }
}
