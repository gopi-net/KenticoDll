using System;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Base.Web.UI;
using CMS.Helpers;

namespace CMS.UIControls
{
    /// <summary>
    /// This class encapsulates reCAPTCHA UI and logic into an ASP.NET server control.
    /// </summary>
    [ToolboxData("<{0}:NewRecaptchaControl runat=\"server\" />")]
    public class NewRecaptchaControl : WebControl, IValidator, INamingContainer
    {
        #region "Private Fields"

        private const string RECAPTCHA_API_URL = "https://www.google.com/recaptcha/api.js?onload=InitializeNewReCaptchaControls&amp;render=explicit";
        private const string RECAPTCHA_RESPONSE_FIELD = "g-recaptcha-response";

        private NewRecaptchaResponse mRecaptchaResponse;
        private string mErrorMessage;

        #endregion


        #region "Public Properties"

        /// <summary>
        /// The public site key from https://www.google.com/recaptcha/admin.
        /// </summary>
        [Category("Settings")]
        [Description("The public site key from https://www.google.com/recaptcha/admin.")]
        public string SiteKey
        {
            get;
            set;
        }


        /// <summary>
        /// The private secret from https://www.google.com/recaptcha/admin.
        /// </summary>
        [Category("Settings")]
        [Description("The private secret from https://www.google.com/recaptcha/admin.")]
        public string Secret
        {
            get;
            set;
        }


        /// <summary>
        /// Optional. The type of CAPTCHA to serve. Possible values are 'audio' and 'image'. Default is 'image'.
        /// </summary>
        [Category("Settings")]
        [DefaultValue("")]
        [Description("Optional. The type of CAPTCHA to serve. Possible values are 'audio' and 'image'. Default is 'image'.")]
        public string DataType
        {
            get;
            set;
        }


        /// <summary>
        /// reCAPTCHA proxy used to validate.
        /// </summary>
        [Category("Settings")]
        [Description("Set this to override proxy used to validate reCAPTCHA.")]
        public IWebProxy Proxy
        {
            get;
            set;
        }

        /// <summary>
        /// reCAPTCHA theme to be used.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue("red")]
        [Description("The theme for the reCAPTCHA control. Currently supported values are 'dark', 'light'. Default is 'light'.")]
        public string Theme
        {
            get;
            set;
        }
        
        #endregion


        #region Overriden Methods

        /// <summary>
        /// Control init event
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Validators.Add(this);         
        }


        protected override void OnPreRender(EventArgs e)
        {
            if (!RequestHelper.IsAJAXRequest())
            {
                RegisterClientInitializationScripts();
            }
            else
            {
                RegisterIndividualClientInitializationScript();
            }

            base.OnPreRender(e);
        }
        

        /// <summary>
        /// Control render event.
        /// </summary>
        /// <param name="writer">HtmlTextWriter parameter</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // Render the div tag for nesting the reCaptcha
            writer.WriteBeginTag("div");
            if (!string.IsNullOrEmpty(Theme))
            {
                writer.WriteAttribute("data-theme", Theme);
            }
            if (!string.IsNullOrEmpty(DataType))
            {
                writer.WriteAttribute("data-type", DataType);
            }
            writer.Write("></div>");

            base.Render(writer);
        }

        #endregion


        #region IValidator Members

        /// <summary>
        /// Validation error message.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public string ErrorMessage
        {
            get
            {
                return (mRecaptchaResponse != null) ? mRecaptchaResponse.ErrorMessage : mErrorMessage;
            }

            set
            {
                mErrorMessage = value;
            }
        }


        /// <summary>
        /// Indicates if validation was successful.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get
            {
                return ((mRecaptchaResponse == null) || mRecaptchaResponse.IsValid);
            }

            set
            {
                throw new NotImplementedException("IsValid property is not settable. This property is populated automatically.");
            }
        }


        /// <summary>
        /// Perform validation of reCAPTCHA.
        /// </summary>
        public void Validate()
        {
            if (!string.IsNullOrEmpty(Secret) && !string.IsNullOrEmpty(SiteKey))
            {
                if (RequestHelper.IsPostBack() && Visible && Enabled && (Context != null))
                {
                    if (mRecaptchaResponse == null)
                    {
                        if (Visible && Enabled)
                        {
                            var validator = new NewRecaptchaValidator
                            {
                                Secret = Secret,
                                RemoteIP = RequestContext.UserHostAddress,
                                Response = GetCoupledRequestFormValue(RECAPTCHA_RESPONSE_FIELD),
                                Proxy = Proxy
                            };

                            mRecaptchaResponse = validator.Validate();
                        }
                    }
                }
                else
                {
                    mRecaptchaResponse = NewRecaptchaResponse.Valid;
                }
            }
            else
            {
                mRecaptchaResponse = NewRecaptchaResponse.MissingSecret;
                ErrorMessage = "New reCAPTCHA needs to be configured with a secret & site key.";
            }

        }

        #endregion


        #region "Helper methods"

        /// <summary>
        /// Registers client scripts for initialization of all controls of this type within a page.
        /// </summary>
        private void RegisterClientInitializationScripts()
        {
            var thisType = GetType();
            ScriptHelper.RegisterClientScriptBlock(this, thisType, nameof(NewRecaptchaControl) + "-external", String.Format("<script src=\"{0}\" async defer></script>", RECAPTCHA_API_URL));

            StringBuilder sb = new StringBuilder("<script>var InitializeNewReCaptchaControls = function(){");
            foreach (var validator in Page.Validators)
            {
                NewRecaptchaControl newRecaptchaControl = validator as NewRecaptchaControl;
                if (newRecaptchaControl != null)
                {
                    sb.Append(GetClientInitializationScript(newRecaptchaControl.ClientID, newRecaptchaControl.SiteKey));
                }
            }
            sb.Append("};</script>");

            ScriptHelper.RegisterClientScriptBlock(this, thisType, nameof(NewRecaptchaControl) + "-init", sb.ToString());
        }


        /// <summary>
        /// Registers client script for initialization of this control.
        /// </summary>
        private void RegisterIndividualClientInitializationScript()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, GetClientInitializationScript(ClientID, SiteKey), true);
        }


        /// <summary>
        /// Gets client script for initialization of control of this type.
        /// </summary>
        private static string GetClientInitializationScript(string clientId, string siteKey)
        {
            return "if (document.getElementById('" + clientId + "') != null && (typeof grecaptcha!== 'undefined')) { grecaptcha.render('" + clientId + "', {'sitekey' : '" + siteKey + "'}); }";
        }


        /// <summary>
        /// Get form field value
        /// </summary>
        /// <param name="formField">Field name to get its value from</param>
        private string GetCoupledRequestFormValue(string formField)
        {
            // Get form value
            var fieldValue = Context.Request.Form[formField];
            if (!string.IsNullOrEmpty(fieldValue))
            {
                var fieldValuesArray = fieldValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (fieldValuesArray.Length > 0)
                {
                    fieldValue = fieldValuesArray[0];
                }
                else
                {
                    fieldValue = string.Empty;
                }
            }

            return fieldValue;
        }

        #endregion
    }
}