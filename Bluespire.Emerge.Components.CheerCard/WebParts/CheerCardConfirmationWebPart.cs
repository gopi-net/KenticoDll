using System;
using Bluespire.Emerge.Components.CheerCard.Services;
using System.Data;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.CMS.GlobalHelper;

namespace Bluespire.Emerge.Components.CheerCard.WebParts
{
    public class CheerCardConfirmationWebPart : CheerCardWebPart
    {
        #region "Web part properties"
        /// <summary>
        /// Gets or sets Cheer Card Selection page Url. Will be set at the time of adding web part to the page.
        /// </summary>
        public string CheerCardListingPageURL
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("CheerCardListingPageURL"), string.Empty);
            }
            set
            {
                SetValue("CheerCardListingPageURL", value);
            }
        }
        #endregion "Web part properties"
        /// <summary>
        /// Method will get the resource string and replace the placeholders with the Cheer card request Item.
        /// </summary>
        /// <param name="ItemID">Cheer card Request item whose details will be fetched to replace.</param>
        /// <returns>String with the replaced placeholders</returns>
        /// <exception cref="SessionDataMissingException"> Thrown if required Session data is null </exception>
        protected string GetThankYouMessage()
        {
            ICheerCardsService cheerCardService = new CheerCardService();
            string message = string.Empty;

            if (EmergeSessionHelper.GetValue(CheerCardConstants.SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD) == null )
                throw new SessionDataMissingException();
            int ItemID = Convert.ToInt32(EmergeSessionHelper.GetValue(CheerCardConstants.SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD));
            DataSet ds = cheerCardService.GetCheerCardRequestItemByID(ItemID);
            message = GetResourceString(ds.Tables[0].Columns.Contains(CheerCardConstants.FIELDS_CHEERCARDREQUEST_PATIENTEMAIL) ? ds.Tables[0].Rows[0][CheerCardConstants.FIELDS_CHEERCARDREQUEST_PATIENTEMAIL] : string.Empty);
            message = cheerCardService.ReplacePlaceHolders(message, ds.Tables[0], string.Empty);
            return message;
        }

        /// <summary>
        /// method to get Resource string with placeholders depending upon Patient Email Exists in Cheercard  request or not.
        /// </summary>
        /// <param name="patientEmail">Patient Email</param>
        /// <returns>Resource string with place holders</returns>
        private string GetResourceString(object patientEmail)
        {
            string message = string.Empty;
            if (patientEmail == DBNull.Value || String.IsNullOrEmpty(patientEmail.ToString()))
                message = EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_THANKYOUMESSAGE_HOSPITAL);
            else
                message = EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_THANKYOUMESSAGE_PATIENT);
            return message;
        }
    }
}
