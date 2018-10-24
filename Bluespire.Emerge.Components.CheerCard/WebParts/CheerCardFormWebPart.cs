using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.CheerCard.Services;
using Bluespire.Emerge.CommonService.Email;
using System.Net.Mail;
using Bluespire.Emerge.Common.Exceptions;
using System.IO;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS;
using CMS.Helpers;
using System.Web.UI;

namespace Bluespire.Emerge.Components.CheerCard.WebParts
{
    public class CheerCardFormWebPart : CheerCardWebPart
    {
        #region "Web part properties"
        /// <summary>
        /// on click of Back button, control will be redirected to this page
        /// </summary>
        public string CheerCardSelectionPageURL
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("CheerCardSelectionPageURL"), string.Empty);
            }
            set
            {
                SetValue("CheerCardSelectionPageURL", value);
            }
        }
        /// <summary>
        /// on successfull submission of the form, control will be redirected to this page
        /// User will be redirected to this page after cheer card has been sent.
        /// </summary>
        public string CheerCardConfirmationPageURL
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("CheerCardConfirmationPageURL"), string.Empty);
            }
            set
            {
                SetValue("CheerCardConfirmationPageURL", value);
            }
        }
        public bool SendCheerCardAsAttachement
        {
            get
            {
                return EmergeValidationHelper.GetBoolean(GetValue("CheerCardAsAttachement"), false);
            }
            set
            {
                SetValue("CheerCardAsAttachement", value);
            }
        }
        public string HospitalEmailAddress
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("HospitalEmailAddress"), string.Empty).Trim();
            }
            set
            {
                SetValue("HospitalEmailAddress", value);
            }
        }
        public int CheerCardImageMaxHeight
        {
            get
            {
                return EmergeValidationHelper.GetInteger(GetValue("CheerCardImageMaxHeight"), 193);
            }
            set
            {
                SetValue("CheerCardImageMaxHeight", value);
            }
        }
        public int CheerCardImageMaxWidth
        {
            get
            {
                return EmergeValidationHelper.GetInteger(GetValue("CheerCardImageMaxWidth"), 258);
            }
            set
            {
                SetValue("CheerCardImageMaxWidth", value);
            }
        }
        public int CheerCardImageWriteOnBackgroundImage_XCoordinate
        {
            get
            {
                return EmergeValidationHelper.GetInteger(GetValue("CheerCardImageWriteOnBackgroundImage_XCoordinate"), 18);
            }
            set
            {
                SetValue("CheerCardImageWriteOnBackgroundImage_XCoordinate", value);
            }
        }
        public int CheerCardImageWriteOnBackgroundImage_YCoordinate
        {
            get
            {
                return EmergeValidationHelper.GetInteger(GetValue("CheerCardImageWriteOnBackgroundImage_YCoordinate"), 65);
            }
            set
            {
                SetValue("CheerCardImageWriteOnBackgroundImage_YCoordinate", value);
            }
        }

        public string BackgroundImagePathWithCheerCardImage
        {
            get
            {
                string backgroundImagePath = EmergeValidationHelper.GetString(GetValue("BackgroundImagePathWithCheerCardImage"), "").Trim();
                if (backgroundImagePath.Equals(string.Empty)) return "~/App_Themes/Emerge/images/CheerCardBgDesktop.jpg";
                return backgroundImagePath;
            }
            set
            {
                SetValue("BackgroundImagePathWithCheerCardImage", value);
            }
        }

        public string BackgroundImagePathWithNoCheerCardImage
        {
            get
            {
                string backgroundImagePath = EmergeValidationHelper.GetString(GetValue("BackgroundImagePathWithNoCheerCardImage"), "").Trim();
                if (backgroundImagePath.Equals(string.Empty)) return "~/App_Themes/Emerge/images/CheerCardBgDesktop_NoImage.jpg";
                return backgroundImagePath;

            }
            set
            {
                SetValue("BackgroundImagePathWithNoCheerCardImage", value);
            }
        }

        /// <summary>
        /// This string will be used as empty string placeholder on Generated Preview html as well as on "cheer card attachment image"
        /// </summary>
        public string EmptyStringPlaceHolder
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("EmptyStringPlaceHolder"), CheerCardConstants.CHEERCARD_PREVIEWHTML_DEFAULTEMPTYSTRINGPLACEHOLDER);
            }
            set
            {
                SetValue("EmptyStringPlaceHolder", value);
            }
        }

        #endregion "Web part properties"

        #region veriables
        protected const string CheerCardImagePathKey = "CheerCardImagePath";
        protected const bool sendMailToPatient = false;
        protected const string ControlIDRequiredPatientEmail = "RequiredPatientEmail";
        protected const string ControlIDRegularPatientEmail = "RegularPatientEmail";
        protected const string ControlIDRequiredPatientFirstName = "RequiredPatientFirstName";
        protected const string ControlIDRequiredPatientLastName = "RequiredPatientLastName";
        protected const string ControlIDPatientEmail = "PatientEmail";
        protected const string ControlIDSenderEmail = "SenderEmail";
        protected const string ControlIDRequiredRoomNumber = "RequiredRoomNumber";
        protected int _ItemID = 0;
        ICheerCardsService cheerCardService = new CheerCardService();
        public string SelectedCheerCardImage
        {
            get
            {
                if (EmergeQueryHelper.GetString(CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD, string.Empty) == string.Empty)
                {
                    return CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT;
                }
                else
                    return EmergeQueryHelper.GetString(CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD, string.Empty);
            }
        }

        #endregion veriables

        /// <summary>
        /// Method to set up cheer card form.
        /// </summary>
        protected void SetupCheerCardForm()
        {
            LoadListControls();
            SetFormFields();
        }
        /// <summary>
        /// Method to check if cheer card will be sent as attachement or as html
        /// </summary>
        /// <returns>retunrs true if cheer card as attachement.</returns>
        protected bool IsSendEmailWithCheerCardAsAttachment()
        {
            return SendCheerCardAsAttachement;
        }
        /// <summary>
        /// Method to create cheer card image. This method also stores path of the newely created image in Viewstate.
        /// </summary>
        protected void CreateCheerCardImage()
        {
            string imagePath = string.Empty;
            imagePath = cheerCardService.CreateAndSaveCheerCardAttachmentImage(FormParameters, SelectedCheerCardImage, GetAttachmentImageConfigurations(), EmptyStringPlaceHolder);
            ViewState[CheerCardImagePathKey] = imagePath;
        }
        public CheerCardAttachementImageConfigurationInfo GetAttachmentImageConfigurations()
        {
            CheerCardAttachementImageConfigurationInfo ImageConfigurations = new CheerCardAttachementImageConfigurationInfo();
            ImageConfigurations.ImageMaxHeight = CheerCardImageMaxHeight;
            ImageConfigurations.ImageMaxWidth = CheerCardImageMaxWidth;
            ImageConfigurations.ImageXPosition = CheerCardImageWriteOnBackgroundImage_XCoordinate;
            ImageConfigurations.ImageYPosition = CheerCardImageWriteOnBackgroundImage_YCoordinate;
            ImageConfigurations.BackgroundImagePathWithCCImage = BackgroundImagePathWithCheerCardImage;
            ImageConfigurations.BackgroundImagePathWithNoCCImage = BackgroundImagePathWithNoCheerCardImage;
            return ImageConfigurations;
        }

        /// <summary>
        /// Method to delete cheer card image. 
        /// </summary>
        protected void DeleteCheerCardImage()
        {
            if (null == ViewState[CheerCardImagePathKey]) return;
            cheerCardService.DeleteImage(ViewState[CheerCardImagePathKey].ToString());
        }

        /// <summary>
        /// Method to form email message and send cheer card image as attachment. 
        /// </summary>
        protected void SendCheerCardEmail(bool isSendCheerCardAsAttachement, bool mailToPatient)
        {
            EmailMessageInfo emailMessageInfo = GetEmailMessage(GetSender(), GetRecipients(mailToPatient), isSendCheerCardAsAttachement);
            if (isCCImageSelected())
                AddSelectedImageInFormParameters();
            EmailService.SendEmailUsingTemplate(emailMessageInfo, GetEmailtemplateCode(isSendCheerCardAsAttachement), EmergeStaticHelper.GetMacrosForEmailTemplate(GetFormFieldsForDisplay(EmergeStaticHelper.SetSiteName(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS), FormParameters)), false);
            if (isSendCheerCardAsAttachement)
                emailMessageInfo.Attachments.Dispose();
        }
        /// <summary>
        /// returns true if user selects an cheer card image.
        /// </summary>
        /// <returns></returns>
        private bool isCCImageSelected()
        {
            if (String.IsNullOrEmpty(SelectedCheerCardImage) || SelectedCheerCardImage.Equals(CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT))
                return false;
            return true;
        }

        private string GetEmailtemplateCode(bool isSendCheerCardAsAttachement)
        {
            return String.Format(isSendCheerCardAsAttachement == true ? CheerCardConstants.SEND_CHEER_CARD_AS_ATTACHMENT_EMAIL_TEMPLATE : (isCCImageSelected() ? CheerCardConstants.SEND_CHEER_CARD_AS_HTML_WITHCCIMAGE_EMAIL_TEMPLATE : CheerCardConstants.SEND_CHEER_CARD_AS_HTML_WITHNOCCIMAGE_EMAIL_TEMPLATE), EmergeSiteInfoProvider.CurrentSiteName);
        }

        /// <summary>
        /// Method to add Selected Image (key and its value that is guid) in FormParameter dictionary item. This will help to replace image guid in email.
        /// </summary>
        protected void AddSelectedImageInFormParameters()
        {
            if (!FormParameters.ContainsKey(CheerCardConstants.CHEER_CARD_SELECTEDIMAGE_EMAILTEMPLATE_PLACEHOLDER))
                FormParameters.Add(CheerCardConstants.CHEER_CARD_SELECTEDIMAGE_EMAILTEMPLATE_PLACEHOLDER, SelectedCheerCardImage);
            else
                FormParameters[CheerCardConstants.CHEER_CARD_SELECTEDIMAGE_EMAILTEMPLATE_PLACEHOLDER] = SelectedCheerCardImage;
        }

        private EmailMessageInfo GetEmailMessage(string senderAddress, string recipients, bool isSendCheerCardAsAttachement)
        {
            EmailMessageInfo emailMessageInfo = new EmailMessageInfo();
            if (isSendCheerCardAsAttachement)
            {
                Attachment cheerCardImage = new System.Net.Mail.Attachment(Server.MapPath(ViewState[CheerCardImagePathKey].ToString()));
                cheerCardImage.Name = CheerCardConstants.CheercardAttachementName + Path.GetExtension(Server.MapPath(ViewState[CheerCardImagePathKey].ToString())).ToString();
                emailMessageInfo.Attachments.Add(cheerCardImage);
            }
            emailMessageInfo.From = senderAddress;
            emailMessageInfo.Recipients = recipients;
            emailMessageInfo.IsBodyHtml = true;
            return emailMessageInfo;
        }

        /// <summary>
        /// Method to get Recipients.
        /// </summary>
        private string GetRecipients(bool mailToPatient)
        {
            string recipients = string.Empty;
            if (mailToPatient)
            {
                if (FormParameters.Any(x => x.Key.Equals(ControlIDPatientEmail)))
                    recipients = FormParameters.Where(x => x.Key.Equals(ControlIDPatientEmail)).First().Value.ToString();
                else
                    throw new CheerCardEmailToFormFieldMissingException();
            }
            else
            {
                if (HospitalEmailAddress.Equals(string.Empty))
                    throw new Exception(ResHelper.GetString(CheerCardConstants.STRINGCODE_HOSPITALEMAILADDRESSNOTFOUNDEXCEPTION_MESSAGE));
                recipients = HospitalEmailAddress;
            }
            return recipients;
        }


        /// <summary>
        /// If Email to Hospital is selected then this method will reset form parameter "patient email" to empty.
        /// </summary>
        protected void ResetPatientEmail(bool mailToPatient)
        {
            if (!mailToPatient)
            {
                if (FormParameters.Any(x => x.Key.Equals(ControlIDPatientEmail)))
                {
                    FormParameters[ControlIDPatientEmail] = string.Empty;
                }
            }
        }

        /// <summary>
        /// Method to get Recipients.
        /// </summary>
        private string GetSender()
        {
            string senderAddress = string.Empty;
            if (FormParameters.Any(x => x.Key.Equals(ControlIDSenderEmail)))
                senderAddress = FormParameters.Where(x => x.Key.Equals(ControlIDSenderEmail)).First().Value.ToString();
            return senderAddress;
        }

        /// <summary>
        /// Method to save Cheer card record.
        /// </summary>
        protected int SaveCheerCardDetails()
        {
            return cheerCardService.SaveCheerCardRequest(FormParameters);
        }

        /// <summary>
        /// Method to form Query string for back to Listing Page.
        /// </summary>
        protected Dictionary<string, string> GetListingPageQueryString()
        {
            string parameter = string.Empty;
            string QueryString = string.Empty;
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add(CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD, EmergeQueryHelper.GetString(CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD, string.Empty));
            return query;
        }

        /// <summary>
        /// method to Get Preview Card Html.
        /// </summary>
        /// <exception cref="CheerCardPreviewHtmlItemNotFound"> Thrown in case of key not found in Cheer Card Configuration Custom Table.</exception>
        /// <returns>Html of Preview Card.</returns>
        protected string GetPreviewHtml()
        {
            return cheerCardService.GetCheerCardPreviewHtml(FormParameters, Environment, SelectedCheerCardImage, EmptyStringPlaceHolder, false);
        }

        /// <summary>
        /// Method to fill form fields from Session.
        /// </summary>
        private void SetFormFields()
        {
            Dictionary<string, object> formFields = (Dictionary<string, object>)EmergeSessionHelper.GetValue(CheerCardConstants.SESSION_KEY_NAME_FOR_CHEER_CARD_FORM_FIELDS);
            if (null != formFields)
            {
                SetFormFieldsFromDictionary(formFields);
            }
        }

        /// <summary>
        /// Method to set Session variables
        /// </summary>
        protected void SetSession()
        {
            EmergeSessionHelper.Remove(CheerCardConstants.SESSION_KEY_NAME_FOR_CHEER_CARD_FORM_FIELDS);
            EmergeSessionHelper.SetValue(CheerCardConstants.SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD, _ItemID);
        }

        /// <summary>
        /// Method to activate or deactivate validators depending  Where to send card.
        /// </summary>
        protected void ResetValidators(bool status)
        {
            if (null != ControlPanel.FindControl(ControlIDRequiredPatientEmail))
                ((RequiredFieldValidator)ControlPanel.FindControl(ControlIDRequiredPatientEmail)).Enabled = status;
            if (null != ControlPanel.FindControl(ControlIDRegularPatientEmail))
                ((RegularExpressionValidator)ControlPanel.FindControl(ControlIDRegularPatientEmail)).Enabled = status;
            if (null != ((RequiredFieldValidator)ControlPanel.FindControl(ControlIDRequiredRoomNumber)))
                ((RequiredFieldValidator)ControlPanel.FindControl(ControlIDRequiredRoomNumber)).Enabled = !status;
        }

        protected void ClearCheerCardFormFields()
        {
            ClearFormFields();
            EmergeSessionHelper.Remove( CheerCardConstants.SESSION_KEY_NAME_FOR_CHEER_CARD_FORM_FIELDS);
        }
    }
}
