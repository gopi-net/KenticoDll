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

namespace Bluespire.Emerge.Components.Career.WebParts
{
    /// <summary>
    /// Base Class for all the webparts to be created for Rates module.
    /// </summary>
    public class CareerWebPart : EmergeBaseWebPart
    {
        /// <summary>
        /// OnInit Method will be used to set the module name to which webpart belongs to.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.Career;
            base.OnInit(e);
        }

        protected int GetItemIdForUser(string customTableName)
        {
            List<CustomTableItem> items;
            string wherecondition = string.Format("{0} = {1}", CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            items = CustomTableDataHelper.GetCustomTableItemsByCriteria(customTableName, wherecondition);
            if (items.Any())
            {
                return Convert.ToInt32(items.First()[CareerConstants.CAREER_COLUMN_ITEMID].ToString());
            }
            return 0;
        }

        protected IDictionary GetUserData(string customTableName)
        {
            return GetUserDataFromCustomTable(customTableName, 0);
        }
        protected IDictionary GetUserData(string customTableName, int itemID)
        {
            return GetUserDataFromCustomTable(customTableName, itemID);
        }

        private static IDictionary GetUserDataFromCustomTable(string customTableName, int itemID)
        {
            customTableName = string.Format(customTableName, EmergeCMSContext.CurrentSiteName);
            string wherecondition = string.Format("{0} = {1}", CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            if (itemID > 0)
                wherecondition += string.Format("AND {0} = {1}", CareerConstants.CAREER_COLUMN_ITEMID, itemID);
            List<CustomTableItem> items = CustomTableDataHelper.GetCustomTableItemsByCriteria(customTableName, wherecondition);
            CustomTableItem item = items.FirstOrDefault();
            IDictionary itemDictionary = new Dictionary<string, object>();
            if (item == null)
                return itemDictionary;
            foreach (string column in item.ColumnNames)
            {
                if (item[column] != null)
                {
                    itemDictionary.Add(column, item[column]);
                }
            }
            return itemDictionary;
        }
        protected DataSet GetUserDataSet(string customTableName)
        {
            customTableName = string.Format(customTableName, EmergeCMSContext.CurrentSiteName);
            string wherecondition = string.Format("{0} = {1}", CareerConstants.CAREER_COLUMN_USERID, EmergeCurrentUser.UserID);
            return CustomTableDataHelper.GetCustomTableItemsByCondition(customTableName, wherecondition, string.Empty);
        }

        protected void FillUserInformation(Panel controlPanel, Dictionary<string, object> itemDictionary)
        {
            ControlPanel = controlPanel;
            SetFormFieldsFromDictionary(itemDictionary);
        }
        private bool IsPhoneAvailable(string phoneNumber)
        {
            return !phoneNumber.Trim().Equals(CareerConstants.CAREER_SPLITTER_PHONE_EXTENSION) && !phoneNumber.Trim().Equals(string.Empty);
        }
        private bool IsExtensionAvailable(string phoneNumber)
        {
            return phoneNumber.Length > 19;
        }
        protected string GetPhoneNumber(string phoneNumber)
        {
            if (IsPhoneAvailable(phoneNumber))
            {
                return phoneNumber.Substring(0, 14);
            }
            return string.Empty;
        }
        protected string GetExtension(string phoneNumber)
        {
            if (IsExtensionAvailable(phoneNumber))
            {
                return phoneNumber.Substring(21);
            }
            return string.Empty;
        }
        protected bool AreDatesValid()
        {
            bool isValid = true;
            foreach (Control control in ControlPanel.Controls.OfType<UserControl>())
            {
                if (control is EmergeDateTimePickerUserControl)
                {
                    isValid = ((EmergeDateTimePickerUserControl)control).IsValid() && isValid;
                }
            }
            return isValid;
        }
        protected void SetValidation(RadioButtonList rbl, EmergeDateTimePickerUserControl dtPicker)
        {
            bool isSelected = rbl.SelectedValue.ToLower() == "yes" ? true : false;
            dtPicker.NeedsValidation = isSelected;
        }
    }


}
