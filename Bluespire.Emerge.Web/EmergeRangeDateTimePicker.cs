using CMS.Base.Web.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Web
{
    public class EmergeRangeDateTimePicker : RangeDateTimePicker
    {
        string _FromWaterMarkText;
        string _ToWaterMarkText;


        /// <summary>
        /// Property for From Watermark
        /// </summary>
        public string FromWaterMarkText { get { return _FromWaterMarkText; } set { _FromWaterMarkText = value; } }

        /// <summary>
        /// Property for To Watermark
        /// </summary>
        public string ToWaterMarkText { get { return _ToWaterMarkText; } set { _ToWaterMarkText = value; } }

        public EmergeRangeDateTimePicker(string FromWaterMark, string ToWaterWark):base()
        {
             
            this._FromWaterMarkText = FromWaterMark;
            this._ToWaterMarkText = ToWaterWark;
            
        }

        public EmergeRangeDateTimePicker() : this(string.Empty, string.Empty) { }

        protected override void OnLoad(EventArgs e)
        {
            
            base.OnLoad(e);
            
            if (!String.IsNullOrWhiteSpace(_ToWaterMarkText))
            {
                if (String.IsNullOrWhiteSpace(AlternateDateTimeTextBox.Text))
                    AlternateDateTimeTextBox.Text = _ToWaterMarkText;
            }

            if (!String.IsNullOrWhiteSpace(_FromWaterMarkText))
            {
                if (String.IsNullOrWhiteSpace(DateTimeTextBox.Text))
                    DateTimeTextBox.Text = _FromWaterMarkText;

            }
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            bool isScriptRequired = false;

            if (!String.IsNullOrWhiteSpace(_FromWaterMarkText))
            {
                isScriptRequired = true;


                RegisterFocusMethod(DateTimeTextBox, _FromWaterMarkText);
                RegisterBlurMethod(DateTimeTextBox, _FromWaterMarkText); 
                
            }

            if (!String.IsNullOrWhiteSpace(_ToWaterMarkText))
            {
                isScriptRequired = true;

                RegisterFocusMethod(AlternateDateTimeTextBox, _ToWaterMarkText);
                RegisterBlurMethod(AlternateDateTimeTextBox, _ToWaterMarkText); 
              
                AlternateDateTimeTextBox.Attributes.Add("style", "margin-left:10px;");
            }

            if (isScriptRequired)
            {
                RegisterWaterMarkScript();
            }

            

        }

        /// <summary>
        /// method to register control with Water mark blur method
        /// </summary>
        /// <param name="DateBox">Control</param>
        /// <param name="waterMarkText"> Water mark text</param>
        private void RegisterBlurMethod(System.Web.UI.WebControls.TextBox DateBox, string waterMarkText)
        {
            DateBox.Attributes.Add("onBlur", " javascript:Watermark_Blur('" + DateBox.ClientID + "','" + waterMarkText + "');");
        }

        /// <summary>
        /// method to register control with Water mark focus method 
        /// </summary>
        /// <param name="DateBox">Control</param>
        /// <param name="waterMarkText"> Water mark text</param>
        private void RegisterFocusMethod(System.Web.UI.WebControls.TextBox DateBox, string waterMarkText)
        {
            DateBox.Attributes.Add("onFocus", " javascript:Watermark_Focus('" + DateBox.ClientID + "','" + waterMarkText + "');");
        }


        /// <summary>
        /// Add script for methods Watermark_Blur and Watermark_Focus.
        /// </summary>
        private void RegisterWaterMarkScript()
        {
            StringBuilder script = new StringBuilder();

            script.Append(" function Watermark_Focus(controlclientID,watermarkText) {");
            script.Append(" if ( document.getElementById(controlclientID).value == watermarkText) { document.getElementById(controlclientID).value = '';  } ");
            script.Append("}");

            script.Append(" function Watermark_Blur(controlclientID,watermarkText) {");
            script.Append(" if (document.getElementById(controlclientID).value == '') { document.getElementById(controlclientID).value = watermarkText;  } ");

            script.Append("}");

            script.Append("jQuery('span.RangeDateTimePickerLabel').css('display','none');");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "watermark", script.ToString(), true);

        }

        /// <summary>
        /// function to check if date range is valid. 
        /// </summary>
        /// <returns> true if date range is valid else false.</returns>
        public bool ValidateDateTimeRange()
        {


            string[] dateFormats = {"M/d/yyyy h:m:s tt",  
                           
                          "M/d/yyyy h:m:s", "M/d/yyyy", 
                         "M/d/yyyy h:m tt", "M/d/yyyy h:m", 
                         };


            bool isFromDateEntered = true;
            bool isToDateEntered = true;

            
            if (DateTimeTextBox.Text.Trim().Equals(_FromWaterMarkText) ||  String.IsNullOrWhiteSpace( DateTimeTextBox.Text))
                isFromDateEntered = false;
            if (AlternateDateTimeTextBox.Text.Trim().Equals(_ToWaterMarkText) || String.IsNullOrWhiteSpace(AlternateDateTimeTextBox.Text))
                isToDateEntered = false;


            if (isFromDateEntered)
            {
                try
                {
                    DateTime.ParseExact(DateTimeTextBox.Text.Trim(), dateFormats, new CultureInfo("en-US"), DateTimeStyles.None);
                }
                catch (FormatException)
                { return false; }
            }

            if (isToDateEntered)
            {
                try
                {
                    DateTime.ParseExact(AlternateDateTimeTextBox.Text.Trim(), dateFormats, new CultureInfo("en-US"), DateTimeStyles.None);
                }
                catch (FormatException)
                { return false; }
            }

            if (isFromDateEntered && isToDateEntered)
            {
                if (DateTime.ParseExact(AlternateDateTimeTextBox.Text.Trim(), dateFormats, new CultureInfo("en-US"), DateTimeStyles.None) < DateTime.ParseExact(DateTimeTextBox.Text.Trim(), dateFormats, new CultureInfo("en-US"), DateTimeStyles.None))
                    return false;
            }

            return true;
        }


    }
}
