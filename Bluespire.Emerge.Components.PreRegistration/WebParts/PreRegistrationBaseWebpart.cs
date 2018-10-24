using System;
using System.Collections.Generic;
using System.Linq;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using Bluespire.Emerge.Web.Controls;
using CMS.SiteProvider;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.CommonService.Email;

namespace Bluespire.Emerge.Components.PreRegistration.WebParts
{
    public class PreRegistrationBaseWebpart : EmergeBaseWebPart
    {
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.PreRegistration;
            base.OnInit(e);
        }
        protected bool AreDatesValid()
        {
            bool isValid = true;
            foreach (Control control in ControlPanel.Controls.OfType<UserControl>())
            {

                if (control is EmergeDatePickerUserControl)
                {
                    isValid = ((EmergeDatePickerUserControl)control).IsValid() && isValid;
                }
            }
            return isValid;
        }
        protected void FillPreRegInformation(Panel controlPanel, Dictionary<string, object> itemDictionary)
        {
            ControlPanel = controlPanel;
            SetFormFieldsFromDictionary(itemDictionary);
        }
        protected void BindFormValues()
        {
            if (EmergeSessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO) != null)
            {
                FormParameters = ((Dictionary<string, object>)SessionHelper.GetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO));
                AddFormParameters();
            }
            else
            {
                CreateFormParameters();
            }
            EmergeSessionHelper.SetValue(PreRegistrationConstants.SESSIONKEY_PREREGISTRATIONINFO, FormParameters);
        }
        protected string SetValidationExpressionBasedOnState(string state)
        {
            string validationexpression = string.Empty;
            if (state == PreRegistrationConstants.STATE_BRITISH_COLUMBIA)
                validationexpression = PreRegistrationConstants.VALIDATION_EXPRESSION_FOR_STATE_BRITISH_COLUMBIA;
            else
                validationexpression = PreRegistrationConstants.VALIDATION_EXPRESSION_FOR_OTHER_STATE;
            return validationexpression;
        }               
        protected string GetFieldValueById(int Id, string queryname)
        {
            string value = string.Empty;
            DataSet ds = new DataSet();
            ds = GetFieldValueByFieldId(Id, queryname);
            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    value = Convert.ToString(ds.Tables[0].Rows[0][0]);
            }
            return value;
        }
        protected DataSet GetFieldValueByFieldId(int Id, string query)
        {
            string value = string.Empty;
            Hashtable parameters = new Hashtable();
            string queryName = string.Format(query, EmergeCMSContext.CurrentSiteName);
            parameters.Add("@" + PreRegistrationConstants.QUERY_FIELD_PARAMETER, Id);
            return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
        }
        protected void SendMail(string to, string from, string subject, string body)
        {
            EmailMessageInfo messageInfo = new EmailMessageInfo();
            messageInfo.IsBodyHtml = true;
            messageInfo.From = from;
            messageInfo.Recipients = to;
            messageInfo.Subject = subject;
            messageInfo.Body = body;
            EmailService.SendEmail(messageInfo);
        }        
    }
}
