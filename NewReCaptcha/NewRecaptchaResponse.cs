using System;
using System.Net;
using System.Text;
using CMS.IO;
using Newtonsoft.Json.Linq;

namespace CMS.UIControls
{
    /// <summary>
    /// Encapsulates a response from new reCAPTCHA web service.
    /// </summary>
    public class NewRecaptchaResponse
    {
        #region "Variables"

        /// <summary>
        /// Valid solution.
        /// </summary>
        public static readonly NewRecaptchaResponse Valid = new NewRecaptchaResponse(true, string.Empty);

        /// <summary>
        /// Invalid solution.
        /// </summary>
        public static readonly NewRecaptchaResponse Invalid = new NewRecaptchaResponse(false, string.Empty);

        /// <summary>
        /// Invalid secret.
        /// </summary>
        public static readonly NewRecaptchaResponse InvalidSecret = new NewRecaptchaResponse(false, "The secret parameter is invalid or malformed.");

        /// <summary>
        /// Invalid response.
        /// </summary>
        public static readonly NewRecaptchaResponse InvalidResponse = new NewRecaptchaResponse(false, "The response parameter is invalid or malformed.");

        /// <summary>
        /// Missing secret.
        /// </summary>
        public static readonly NewRecaptchaResponse MissingSecret = new NewRecaptchaResponse(false, "The secret parameter is missing.");

        /// <summary>
        /// Missing response.
        /// </summary>
        public static readonly NewRecaptchaResponse MissingResponse = new NewRecaptchaResponse(false, "The response parameter is missing.");

        #endregion


        #region "Properties"

        /// <summary>
        /// Indicates if response is valid.
        /// </summary>
        public bool IsValid
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets response error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        #endregion


        #region "Methods"

        /// <summary>
        /// Initializes a new instance of the <see cref="RecaptchaResponse"/> class.
        /// </summary>
        /// <param name="isValid">Value indicates whether submitted reCAPTCHA is valid.</param>
        /// <param name="errorMessage">Error message returned from reCAPTCHA web service.</param>
        internal NewRecaptchaResponse(bool isValid, string errorMessage)
        {
            NewRecaptchaResponse templateResponse = null;

            if (IsValid)
            {
                templateResponse = Valid;
            }
            else
            {
                switch (errorMessage)
                {
                    case "missing-input-secret":
                        templateResponse = MissingSecret;
                        break;
                    case "invalid-input-secret":
                        templateResponse = InvalidSecret;
                        break;
                    case "missing-input-response":
                        templateResponse = MissingResponse;
                        break;
                    case "invalid-input-response":
                        templateResponse = InvalidResponse;
                        break;
                    case null:
                        throw new ArgumentNullException("errorMessage");
                }
            }

            // Check if template response is not empty
            if (templateResponse != null)
            {
                IsValid = templateResponse.IsValid;
                ErrorMessage = templateResponse.ErrorMessage;
            }
            else
            {
                IsValid = isValid;
                ErrorMessage = errorMessage;
            }
        }


        /// <summary>
        /// Creates NewRecaptchaResponse object from JSON response.
        /// </summary>
        /// <param name="response">Response from google servers in JSON</param>
        public static NewRecaptchaResponse ParseResponse(WebResponse response)
        {
            using (var stream = StreamReader.New(response.GetResponseStream(), Encoding.UTF8))
            {
                var joResponse = JObject.Parse(stream.ReadToEnd());

                var isValid = false;
                var success = joResponse["success"];
                if (success != null)
                {
                    isValid = success.Value<bool>();
                }

                var errorMessage = "";
                var errorCodes = (JArray)joResponse["error-codes"];
                if (errorCodes != null)
                {
                    errorMessage = errorCodes[0].Value<string>();
                }

                return new NewRecaptchaResponse(isValid, errorMessage);
            }
        }


        /// <summary>
        /// Check if two objects are equal.
        /// </summary>
        /// <param name="obj">Object to check</param>
        public override bool Equals(object obj)
        {
            var other = (NewRecaptchaResponse)obj;
            if (other == null)
            {
                return false;
            }

            return ((other.IsValid == IsValid) && (other.ErrorMessage == ErrorMessage));
        }


        /// <summary>
        /// Returns object hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return IsValid.GetHashCode() ^ ErrorMessage.GetHashCode();
        }

        #endregion
    }
}