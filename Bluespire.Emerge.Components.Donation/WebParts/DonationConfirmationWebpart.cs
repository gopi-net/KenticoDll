using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;

namespace Bluespire.Emerge.Components.Donation.WebParts
{
    public class DonationConfirmationWebpart : DonationBaseWebpart
    {
        protected bool SaveDonationInformation()
        {
            Dictionary<string, object> tableData = (Dictionary<string, object>)EmergeSessionHelper.GetValue(DonationConstants.SESSIONKEY_DONATIONINFO);
            string customTableName = String.Format(DonationConstants.CUSTOMTABLE_DONATIONINFORMATION, EmergeCMSContext.CurrentSiteName);
            int itemID = 0;
            return CustomTableDataHelper.SaveCustomTableItem(customTableName, ref itemID, tableData);
        }

        protected void SendEmail()
        {
            Dictionary<string, object> tableData = (Dictionary<string, object>)EmergeSessionHelper.GetValue(DonationConstants.SESSIONKEY_DONATIONINFO);
            
            EmailMessageInfo messageInfo = new EmailMessageInfo();
            messageInfo.Recipients = EmergeValidationHelper.GetString(tableData[DonationConstants.FIELD_DONATIONINFO_EMAIL], string.Empty);
            string[,] macros = getMacros(tableData);
            string templateName = String.Format(DonationConstants.TEMPLATE_CONFIRMATIONEMAIL, EmergeCMSContext.CurrentSiteName);
            if (macros.Length > 0)
                EmailService.SendEmailUsingTemplate(messageInfo, templateName, macros, true);
        }

        private string[,] getMacros(Dictionary<string, object> tableData)
        {
            string[,] macros = new string[tableData.Count, 2];
            int i = 0;
            foreach (KeyValuePair<string, object> data in tableData)
            {
                macros[i, 0] = data.Key;
                macros[i, 1] = EmergeValidationHelper.GetString(data.Value, string.Empty);
                i++;
            }
            return macros;
        }

        protected void FinalizeSave()
        {
            EmergeSessionHelper.Remove(DonationConstants.SESSIONKEY_DONATIONINFO);
        }
    }

}
