using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.Donation
{
    public static class DonationConstants
    {
        #region CustomTableNames
        public const string CUSTOMTABLE_DONATIONINFORMATION = "customtable.Emerge_{0}_DN_DonationInformation";
        #endregion

        #region CustomTableField
        public const string FIELD_DONATIONINFO_EMAIL = "Email";
        public const string FIELD_DONATIONINFO_CORPORATIONAME = "CorporationName";
        public const string FIELD_DONATIONINFO_DONORNAME = "DonorName";
        public const string FIELD_DONATIONINFO_ADDRESS1 = "Address1";
        public const string FIELD_DONATIONINFO_ADDRESS2 = "Address2";
        public const string FIELD_DONATIONINFO_CITY = "City";
        public const string FIELD_DONATIONINFO_STATE = "State";
        public const string FIELD_DONATIONINFO_ZIP = "Zip";
        public const string FIELD_DONATIONINFO_PHONE = "Phone";
        public const string FIELD_DONATIONINFO_EXTENSION = "Extension";
        public const string FIELD_DONATIONINFO_DONATIONTYPE = "DonationType";
        public const string FIELD_DONATIONINFO_DONATIONAMOUNT = "DonationAmount";
        public const string FIELD_DONATIONINFO_HONOURTYPE = "HonourType";
        public const string FIELD_DONATIONINFO_PERSONNAME = "PersonName";
        public const string FIELD_DONATIONINFO_NOTIFICATIONNAME = "NotificationName";
        public const string FIELD_DONATIONINFO_NOTIFICATIONADDRESS1 = "NotificationAddress1";
        public const string FIELD_DONATIONINFO_NOTIFICATIONADDRESS2 = "NotificationAddress2";
        public const string FIELD_DONATIONINFO_NOTIFICATIONCITY = "NotificationCity";
        public const string FIELD_DONATIONINFO_NOTIFICATIONSTATE = "NotificationState";
        public const string FIELD_DONATIONINFO_NOTIFICATIONZIP = "NotificationZip";
        #endregion

        #region Page URLs

        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_Donation/Tools/Donation_Data_List.aspx";
        public const string PAGEURL_NEW_ITEM = "~/CMSModules/CMS_Donation/Tools/Donation_Data_EditItem.aspx";
        public const string PAGEURL_LIST = "~/CMSModules/CMS_Donation/Tools/Donation_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_Donation/Tools/Donation_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_Donation/Tools/Donation_Data_ViewItem.aspx";
        public const string PATH_DATALIST_XML = "~/CMSModules/CMS_Donation/Tools/Donation_Data_List.xml";
        public const string PATH_DONATIONDATALIST_XML = "~/CMSModules/CMS_Donation/Pages/Donation_Data_DonationList.xml";
        public const string PAGEURL_DONATION_DASHBOARD = "~/CMSModules/CMS_Donation/Dashboard/Dashboard.aspx";
        #endregion

        #region String Codes
        public const string STRINGCODE_DONATIONHOME = "Emerge.DN.Dashboard";
        public const string STRINGCODE_DONATIONSAVEEXCEPTIONMESSAGE = "Emerge.DN.DonationSaveExceptionMessage";
        public const string STRINGCODE_DONATIONSENDEMAILEXCEPTION = "Emerge.DN.DonationSendEmailException";
        
        #endregion

        #region Queries
        public const string QUERY_GETDONATIONTYPE = "customtable.Emerge_{0}_DN_DonationInformation.GetDonationTypes";
        public const string QUERY_GETAMOUNTINFO = "customtable.Emerge_{0}_DN_DonationInformation.GetAmountInfo";
        #endregion 

        #region Other
        public const string SESSIONKEY_DONATIONINFO = "DonationInfo";
        public const string TEMPLATE_CONFIRMATIONEMAIL = "Emerge_{0}_DonationConfirmation";
        #endregion
    }
}
