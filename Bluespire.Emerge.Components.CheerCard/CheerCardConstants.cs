
namespace Bluespire.Emerge.Components.CheerCard
{
    public static class CheerCardConstants
    {

        #region "Custom Table Names"
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS = "customtable.Emerge_{0}_CC_CheerCardRequests";
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_IMAGES = "customtable.Emerge_{0}_CC_Images";
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_CATEGORIES = "customtable.Emerge_{0}_CC_Category";
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_CONFIGURATIONS = "customtable.Emerge_{0}_CC_Configurations_INTERN";
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_ATTACHMENT_IMAGE_CONFIGURATIONS = "customtable.Emerge_{0}_CC_AttachmentImageConfig_INTERN";
        public const string CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_PREVIEW_HTML_CONFIGURATIONS = "customtable.Emerge_{0}_CC_PreviewHtmlConfig_INTERN";
        #endregion "Custom Table Names"

        #region "Custom table Column Names"
        #region "Cheer Card Request"
        public const string FIELDS_CHEERCARDREQUEST_PATIENTEMAIL = "PatientEmail";
        public const string FIELDS_CHEERCARDREQUEST_SENDCHEERCARDTO = "SendCheerCardTo";
        public const string FIELDS_CHEERCARDREQUEST_DELIVERYSTATUS = "DeliveryStatus";
        public const string FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID = "SelectedImageGUID";
        #endregion

        #region "Cheer Card Attachment Image Configuration"
        public const string CHEERCARD_ATTACHMENTIMAGECONFIGURATION_KEY_COLUMNNAME = "[Key]";
        public const string CAPTION_COLUMNNAME = "CaptionToDisplay";
        public const string CAPTION_FONT_NAME_COLUMNNAME = "CaptionFontName";
        public const string CAPTION_FONT_SIZE_COLUMNNAME = "CaptionFontSize";
        public const string CAPTION_FONT_STYLE_COLUMNNAME = "CaptionFontStyle";
        public const string CAPTION_BRUSHCOLOR_R_COLUMNNAME = "CaptionBrushColor_R";
        public const string CAPTION_BRUSHCOLOR_G_COLUMNNAME = "CaptionBrushColor_G";
        public const string CAPTION_BRUSHCOLOR_B_COLUMNNAME = "CaptionBrushColor_B";
        public const string CAPTION_COORDINATE_X_COLUMNNAME = "CaptionCoordinate_X";
        public const string CAPTION_COORDINATE_Y_COLUMNNAME = "CaptionCoordinate_Y";
        public const string CAPTION_BLOCK_HEIGHT_COLUMNNAME = "CaptionBlock_Height";
        public const string CAPTION_BLOCK_WIDTH_COLUMNNAME = "CaptionBlock_Width";
        public const string VALUE_COLUMNNAME = "ValueFormatToDisplay";
        public const string VALUE_FONT_NAME_COLUMNNAME = "ValueFontName";
        public const string VALUE_FONT_SIZE_COLUMNNAME = "ValueFontSize";
        public const string VALUE_FONT_STYLE_COLUMNNAME = "ValueFontStyle";
        public const string VALUE_BRUSHCOLOR_R_COLUMNNAME = "ValueBrushColor_R";
        public const string VALUE_BRUSHCOLOR_G_COLUMNNAME = "ValueBrushColor_G";
        public const string VALUE_BRUSHCOLOR_B_COLUMNNAME = "ValueBrushColor_B";
        public const string VALUE_COORDINATE_X_COLUMNNAME = "ValueCoordinate_X";
        public const string VALUE_COORDINATE_Y_COLUMNNAME = "ValueCoordinate_Y";
        public const string VALUE_BLOCK_HEIGHT_COLUMNNAME = "ValueBlock_Height";
        public const string VALUE_BLOCK_WIDTH_COLUMNNAME = "ValueBlock_Width";
        #endregion

        #region "Cheer Card Configurations"
        public const string CHEERCARD_CONFIGURATION_TABLE_VALUE_COLUMNNAME = "Value";
        #endregion "Cheer Card Configurations"



        #endregion "Custom table Column Names"

        #region "Page Names"
        public const string CHEERCARD_DASHBOARDPAGE = "CheerCardDashboardPage";
        public const string CHEERCARD_LISTPAGE = "CheerCardListPage";
        public const string CHEERCARD_DATAVIEWITEMPAGE = "CheerCardDataViewItemPage";
        public const string CHEERCARD_DATASELECTFIELDSPAGE = "CheerCardDataSelectFieldsPage";
        public const string CHEERCARD_DATALISTPAGE = "CheerCardDataListPage";
        public const string CHEERCARD_DATAEDITITEMPAGE = "CheerCardDataEditItemPage";
        #endregion "Page Names"



        #region "Cheer Card Configuration Keys"
        public const string CHEERCARD_PREVIEWHTML_DEFAULTEMPTYSTRINGPLACEHOLDER = "&nbsp;";
        #endregion "Cheer Card Configuration Keys"

        #region "Cheer Card Attachment Configurations"

        public const string CHEER_CARD_ATTACHMENT_MESSAGE_CONFIGURATION_KEYVALUE_FOR_WITHNOCCIMAGE = "WithNoCCImage";
        public const string CHEER_CARD_ATTACHMENT_MESSAGE_CONFIGURATION_KEYVALUE_FOR_WITHCCIMAGE = "WithCCImage";

        #endregion

        #region Others
        public const string NO_CHEERCARD_SELECTED_TEXT = "NoImage";
        public const string QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD = "Image";
        public const char VALUE_SEPERATOR = '+';
        public const string QUERYSTRING_PARAMETER_NAME_FOR_NEWLY_SAVED_CHEER_CARD_REQUEST = "ItemID";
        public const string CheercardAttachementName = "CheerCard";
        public const string CHEER_CARD_CONFIG_EMPTY_STRING_PLACEHOLDER = "&nbsp;";
        public const string CHEER_CARD_SELECTEDIMAGE_EMAILTEMPLATE_PLACEHOLDER = "SelectedImage";
        #endregion Others

        #region "Session Keys"
        public const string SESSION_KEY_NAME_FOR_CHEER_CARD_FORM_FIELDS = "CHEER_CARD_FORM_FIELDS";
        public const string SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD = "NEWLY_SAVED_CHEERCARDREQUEST_ITEMID";
        #endregion "Session Keys"

        #region "Email Templates Code Name"
        public const string SEND_CHEER_CARD_AS_ATTACHMENT_EMAIL_TEMPLATE = "Emerge_{0}_CC_SendCard_AsAttachment";
        public const string SEND_CHEER_CARD_AS_HTML_WITHCCIMAGE_EMAIL_TEMPLATE = "Emerge_{0}_CC_SendCardAsHtml_WithCCImage";
        public const string SEND_CHEER_CARD_AS_HTML_WITHNOCCIMAGE_EMAIL_TEMPLATE = "Emerge_{0}_CC_SendCardAsHtml_WithNoCCImage";
        #endregion "Email Templates Code Name"

        #region "String Codes"
        public const string STRINGCODE_CHEERCARDHOME = "Emerge.CC.Dashboard";
        public const string STRINGCODE_ERRORMESSAGE_CHEERCARD_NULLREFERENCEEXCEPTION = "Emerge.Exception.ErrorMessage.CheerCard.ItemID.NullReferenceException";
        public const string STRINGCODE_ERRORMESSAGE_CHEERCARD_FORMATEXCEPTION = "Emerge.Exception.ErrorMessage.Cheercard.ItemID.FormatException";
        public const string STRINGCODE_ERRORMESSAGE_CHEERCARD_OVERFLOWEXCEPTION = "Emerge.Exception.ErrorMessage.Cheercard.ItemID.OverflowException";
        public const string STRINGCODE_ERRORMESSAGE_CHEERCARD_CHEERCARDITEMNOTFOUNDEXCEPTION = "Emerge.Exception.ErrorMessage.Cheercard.CheerCardItemNotFoundException";
        public const string STRINGCODE_THANKYOUMESSAGE_HOSPITAL = "Emerge.CC.CheerCardConfirmation.Hospital.ThankyouMessage";
        public const string STRINGCODE_THANKYOUMESSAGE_PATIENT = "Emerge.CC.CheerCardConfirmation.Patient.ThankyouMessage";
        public const string STRINGCODE_FAILEDTOINSERTMESSAGE = "Emerge.CC.CheerCardForm.ShowMessage.Failes.Insert";
        public const string STRINGCODE_NOCHEERCARDSELECTEDMESSAGE = "Emerge.CC.CheerCardList.ErrorMessage.NoCheerCardSelected";
        public const string STRINGCODE_SENDCHEERCARDTO_HOSPITALFORDELIVERY = "Emerge.CC.SendCheerCardToHospitalForDelivery";
        public const string STRINGCODE_SENDCHEERCARDTO_PATIENTEMAILFORDELIVERY = "Emerge.CC.SendCheerCardToPatientEMailForDelivery";

        public const string STRINGCODE_CHEERCARDREQUEST_STATUSCHANGEMESSAGE = "Emerge.CC.CheerCardRequest.StatusChangeMessage";
        public const string STRINGCODE_NOITEMSELECTEDEXCEPTION_MESSAGE = "Emerge.Exception.ErrorMessage.NoItemSelectedException";
        public const string STRINGCODE_HOSPITALEMAILADDRESSNOTFOUNDEXCEPTION_MESSAGE = "Emerge.Exception.ErrorMessage.HospitalEmailAddressNotFoundException";
        #endregion

        #region PageURLs

        public const string PAGEURL_LIST_CHEERCARD = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_List.aspx";
        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_ViewItem.aspx";
        public const string PAGEURL_DATA_EDITITEM = "~/CMSModules/CMS_CheerCard/Tools/CheerCard_Data_EditItem.aspx";
        public const string PAGEURL_CHEERCARD_DASHBOARD = "~/CMSModules/CMS_CheerCard/Dashboard/Dashboard.aspx";

        public const string PAGEURL_CHEERCARDREQUEST_PRINTITEM = "~/CMSModules/CMS_CheerCard/Pages/CheerCardRequest_Data_PrintItem.aspx";
        #endregion

        # region enum
        public enum DeliveryStatus
        {
            Pending,
            Delivered
        }
        #endregion


    }
}
