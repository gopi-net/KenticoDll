using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using System.Collections;
using CMS.CustomTables;
using CMS.SiteProvider;
using Bluespire.Emerge.Common;
using System.Data;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using Bluespire.Emerge.CommonService.Email;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bluespire.Emerge.Components.PreRegistration.WebParts
{
    public class PreRegistrationStep4Webpart : PreRegistrationBaseWebpart
    {
        protected IDictionary<string, object> FormatPreRegistrationData(IDictionary<string, object> FormParameterData)
        {
            IDictionary<string, object> tableData = new Dictionary<string, object>();
            DateTime defaultDate = new DateTime();
            DateTime dateTimeValue;
            int value;
            foreach (KeyValuePair<string, object> dict in FormParameterData)
            {               
                if (DateTime.TryParse(FormParameterData[dict.Key].ToString(), out dateTimeValue))
                {
                    if (Convert.ToDateTime(FormParameterData[dict.Key]) != defaultDate)
                        tableData[dict.Key] = Convert.ToDateTime(FormParameterData[dict.Key]).ToString(PreRegistrationConstants.PREREGINFO_DATEFORMAT);
                    else
                        tableData[dict.Key] = string.Empty;
                }
                else if (Int32.TryParse(FormParameterData[dict.Key].ToString(), out value))
                {
                    if (FormParameterData[dict.Key].ToString() == "-1")
                        tableData[dict.Key] = string.Empty;
                    else
                        tableData[dict.Key] = FormParameterData[dict.Key];
                }
                else
                    tableData[dict.Key] = FormParameterData[dict.Key];
            }
            return tableData;
        }

        protected bool SaveInformation(IDictionary<string, object> FormParameters)
        {            
            string customTableClassName = string.Format(PreRegistrationConstants.CUSTOMTABLE_PREREGISTRATIONINFORMATION, EmergeCMSContext.CurrentSiteName);
            int itemId = 0;
            return CustomTableDataHelper.SaveCustomTableItem(customTableClassName, ref itemId, FormParameters);
        }

        protected void SendAdminEmailNotification()
        {
            string AdminNotificationTo = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.AdminNotidficationEmailTo"), string.Empty);
            string AdminNotificationFrom = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.AdminNotidficationEmailFrom"), string.Empty);
            string AdminNotificationSubject = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.AdminNotidficationEmailSubject"), string.Empty);
            string AdminNotificationBody = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.AdminNotidficationEmailBodyContent"), string.Empty);
            SendMail(AdminNotificationTo, AdminNotificationFrom, AdminNotificationSubject, AdminNotificationBody);
        }
        protected void SendAutoresponderEmail(string EmailTo)
        {
            string AutoresponderEmailFrom = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.PreRegAutoresponderEmailFrom"), string.Empty);
            string AutoresponderEmailSubject = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.PreRegAutoresponderEmailSubject"), string.Empty);
            string AutoresponderEmailBody = EmergeValidationHelper.GetString(ResHelper.GetString("Emerge.PR.AutoresponderEmailBodycontent"), string.Empty);
            SendMail(EmailTo, AutoresponderEmailFrom, AutoresponderEmailSubject, AutoresponderEmailBody);
        }       

        protected DataTable CopyToDataTable(IDictionary itemDictionary)
        {
            DataTable dt = new DataTable();
            dt = AddColumns(dt, itemDictionary);
            DataRow dr = dt.NewRow();
            dr = AssignRowValues(dr, itemDictionary);
            dt.Rows.Add(dr);
            return dt;
        }
        protected DataTable AddColumns(DataTable dt, IDictionary itemDictionary)
        {
            foreach (KeyValuePair<string, object> dict in (IDictionary<string, object>)itemDictionary)
            {
                dt.Columns.Add(dict.Key.ToString());
            }
            return dt;
        }
        protected DataRow AssignRowValues(DataRow dr, IDictionary itemDictionary)
        {
            DateTime defaultDate = new DateTime();
            DateTime dateTimeValue;
            int value;
            foreach (KeyValuePair<string, object> dict in (IDictionary<string, object>)itemDictionary)
            {
                if (Convert.ToString(itemDictionary[dict.Key]) != string.Empty)
                {
                    if (DateTime.TryParse(itemDictionary[dict.Key].ToString(), out dateTimeValue))
                    {
                        if (Convert.ToDateTime(itemDictionary[dict.Key]) != defaultDate)
                            dr[dict.Key] = Convert.ToDateTime(itemDictionary[dict.Key]).ToString(PreRegistrationConstants.PREREGINFO_DATEFORMAT);
                        else
                            dr[dict.Key] = PreRegistrationConstants.PREREGINFO_REVIEW_IFNOVALUE;
                    }
                    else if (Int32.TryParse(itemDictionary[dict.Key].ToString(), out value))
                    {
                        if (itemDictionary[dict.Key].ToString() == "-1")
                            dr[dict.Key] = PreRegistrationConstants.PREREGINFO_REVIEW_IFNOVALUE;
                        else
                            dr[dict.Key] = itemDictionary[dict.Key];
                    }
                    else
                        dr[dict.Key] = itemDictionary[dict.Key];
                }
                else
                    dr[dict.Key] = PreRegistrationConstants.PREREGINFO_REVIEW_IFNOVALUE;
            }
            return dr;
        }
        
    }
}
