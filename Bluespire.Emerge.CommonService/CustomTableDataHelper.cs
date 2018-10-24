using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.SiteProvider;
using System.Data;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;
using CMS.Base.Web.UI;

namespace Bluespire.Emerge.CommonService
{
    /// <summary>
    /// Helper class for manipulating the data of the custom table
    /// </summary>
    public static class CustomTableDataHelper
    {
        /// <summary>
        /// Save the custom table item.
        /// </summary>
        /// <param name="customTableID">id of the custom table</param>
        /// <param name="itemID">id of the custom table. For new item it will be zero.</param>
        /// <param name="tableData">Dictionary object having the data for the custom table item.</param>
        /// <returns>true if success.</returns>
        public static bool SaveCustomTableItem(int customTableID, ref int itemID, IDictionary<string, object> tableData)
        {
            string className = GetCustomTableClassName(customTableID);
            return SaveCustomTableItem(className, ref itemID, tableData);
        }

        /// <summary>
        /// Save the custom table item.
        /// </summary>
        /// <param name="customTableID">name of the custom table</param>
        /// <param name="itemID">id of the custom table item. For new item it will be zero.</param>
        /// <param name="tableData">Dictionary object having the data for the custom table item.</param>
        /// <returns>true if success.</returns>
        public static bool SaveCustomTableItem(string className, ref int itemID, IDictionary<string, object> tableData)
        {
            bool result = false;
            CustomTableItem content = null;
            FormModeEnum FormMode = FormModeEnum.Insert;
            if (itemID > 0)
            {
                FormMode = FormModeEnum.Update;
            }

            // Get previous form content.
            switch (FormMode)
            {
                case FormModeEnum.Insert:
                    content = CustomTableItem.New(className);
                    FormHelper.LoadDefaultValues(className, content);
                    content = fillCustomTableItem(content, tableData);
                    if (content.OrderEnabled)
                    {
                        content.ItemOrder = CustomTableItemProvider.GetLastItemOrder(className) + 1;
                    }
                    content.Insert();
                    itemID = content.ItemID;
                    result = true;
                    break;

                case FormModeEnum.Update:
                    content = GetCustomTableItem(itemID, className);
                    content = fillCustomTableItem(content, tableData);
                    content.Update();
                    result = true;
                    break;

                default:
                    throw new NotImplementedException();
            }
            CacheHelper.TouchKey(className + Constants.EMERGE_CACHE_CLEAR_KEY_SUFFIX);
            return result;
        }

        /// <summary>
        /// Converts a custom table item to datarow.
        /// </summary>
        /// <param name="item">Custom table item to convert to datarow.</param>
        /// <returns>returns the datarow representing the custom table item.</returns>
        public static DataRow ToDataRow(this CustomTableItem item)
        {
            try
            {
                DataTable table = new DataTable();
                foreach (string column in item.ColumnNames)
                {
                    table.Columns.Add(column);
                }
                DataRow row = table.NewRow();
                foreach (string column in item.ColumnNames)
                {
                    row[column] = item.GetValue(column);
                }
                return row;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Deletes the custom table item.
        /// </summary>
        /// <param name="itemID">id of the item to be deleted.</param>
        /// <param name="customTableID">id of the custom table.</param>
        /// <returns>returns true if successfully deleted, else false.</returns>
        public static bool DeleteCustomTableItem(int itemID, int customTableID)
        {
            string className = GetCustomTableClassName(customTableID);
            return DeleteCustomTableItem(itemID, className);
        }

        /// <summary>
        /// Deletes the custom table item.
        /// </summary>
        /// <param name="itemID">id of the custom table.</param>
        /// <param name="className">class name of the custom table.</param>
        /// <returns>returns true if successfully deleted, else false.</returns>
        public static bool DeleteCustomTableItem(int itemID, string className)
        {
            bool response;
            CustomTableItem item = GetCustomTableItem(itemID, className);
            response = DeleteCustomTableItem(item);
            if (response)
                CacheHelper.TouchKey(className + Constants.EMERGE_CACHE_CLEAR_KEY_SUFFIX);
            return response;
        }

        /// <summary>
        /// Deletes the Custom Table item.
        /// </summary>
        /// <param name="item">item to be deleted.</param>
        /// <returns>true if successful, else false.</returns>
        private static bool DeleteCustomTableItem(CustomTableItem item)
        {
            try
            {
                return item.Delete();
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("CustomTableDataHelper:DeleteCustomTableItem", EventCode.EMERGE_DELETE, "The itemID {0} of custom table {1} could not be deleted.");
                throw new CustomTableItemDeleteException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the custom table item.
        /// </summary>
        /// <param name="itemID">id of the item to be retrieved.</param>
        /// <param name="customTableID">ID of the custom table.</param>
        /// <returns>custom table item.</returns>
        public static CustomTableItem GetCustomTableItem(int itemID, int customTableID)
        {
            string className = GetCustomTableClassName(customTableID);
            return GetCustomTableItem(itemID, className);
        }

        /// <summary>
        /// Gets the custom table item.
        /// </summary>
        /// <param name="itemID">id of the item to be retrieved.</param>
        /// <param name="className">name of the custom table.</param>
        /// <returns>custom table item.</returns>
        public static CustomTableItem GetCustomTableItem(int itemID, string className)
        {
            try
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(itemID, className);
                if (item == null)
                {
                    EmergeLogWriter.WriteError("CustomTableDataHelper:GetCustomTableItem", EventCode.EMERGE_GET, string.Format(ResHelper.GetString(Constants.CUSTOMTABLEITEMDOESNOTEXISTS), itemID, className));
                    throw new CustomTableItemNotFoundException(string.Format(ResHelper.GetString(Constants.CUSTOMTABLEITEMDOESNOTEXISTS), itemID, className));
                }
                return item;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the name of the custom table.
        /// </summary>
        /// <param name="customTableID">id of the custom table.</param>
        /// <returns>the name of the custom table.</returns>
        public static string GetCustomTableClassName(int customTableID)
        {
            return GetCustomTableClassInfo(customTableID).ClassName;
        }

        /// <summary>
        /// Gets the class info of the custom table.
        /// </summary>
        /// <param name="customTableID">id of the custom table.</param>
        /// <returns>the class info of the custom table.</returns>
        public static DataClassInfo GetCustomTableClassInfo(int customTableID)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(customTableID);
            if (dci == null)
            {
                EmergeLogWriter.WriteError("CustomTableDataHelper:GetCustomTableClassInfo", EventCode.EMERGE_GET, string.Format(ResHelper.GetString(Constants.CUSTOMTABLEDOESNOTEXISTS), customTableID));
                throw new CustomTableNotExistsException(string.Format(Constants.CUSTOMTABLEDOESNOTEXISTS, customTableID));
            }
            return dci;
        }

        /// <summary>
        /// Gets the class info of the custom table.
        /// </summary>
        /// <param name="className">name of the custom table.</param>
        /// <returns>the class info of the custom table.</returns>
        public static DataClassInfo GetCustomTableClassInfo(string className)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(className);
            if (dci == null)
            {
                EmergeLogWriter.WriteError("CustomTableDataHelper:GetCustomTableClassInfo", EventCode.EMERGE_GET, string.Format(ResHelper.GetString(Constants.CUSTOMTABLEDOESNOTEXISTS), className));
                throw new CustomTableNotExistsException(string.Format(Constants.CUSTOMTABLEDOESNOTEXISTS, className));
            }
            return dci;
        }

        /// <summary>
        /// Gets the value of custom table item field.
        /// </summary>
        /// <param name="itemID">id of the custom table item.</param>
        /// <param name="customTableID">id of the custom table.</param>
        /// <param name="fieldName">name of the field.</param>
        /// <returns>the value of the field.</returns>
        public static object GetFieldValue(int itemID, int customTableID, string fieldName)
        {
            string className = GetCustomTableClassName(customTableID);
            return GetFieldValue(itemID, className, fieldName);
        }

        /// <summary>
        /// Gets the value of custom table item field.
        /// </summary>
        /// <param name="itemID">id of the custom table item.</param>
        /// <param name="customTableID">name of the custom table.</param>
        /// <param name="fieldName">name of the field.</param>
        /// <returns>the value of the field.</returns>
        public static object GetFieldValue(int itemID, string className, string fieldName)
        {
            CustomTableItem item = GetCustomTableItem(itemID, className);
            return item.GetValue(fieldName);
        }

        /// <summary>
        /// Gets the custom table items dataset.
        /// </summary>
        /// <param name="customTableID">id of the custom table.</param>
        /// <param name="whereCondition">where condition to be applied to get the items.</param>
        /// <param name="orderBy">order by expression of the items to be retrieved.</param>
        /// <returns>Dataset of the custom table items.</returns>
        public static DataSet GetCustomTableItemsByCondition(int customTableID, string whereCondition, string orderBy)
        {
            return GetCustomTableItemsByCondition(GetCustomTableClassName(customTableID), whereCondition, orderBy);
        }

        /// <summary>
        /// Gets the custom table items dataset.
        /// </summary>
        /// <param name="customTableID">name of the custom table.</param>
        /// <param name="whereCondition">where condition to be applied to get the items.</param>
        /// <param name="orderBy">order by expression of the items to be retrieved.</param>
        /// <returns>Dataset of the custom table items.</returns>
        public static DataSet GetCustomTableItemsByCondition(string className, string whereCondition, string orderBy)
        {
            DataSet itemsDS = new DataSet();
            itemsDS = CustomTableItemProvider.GetItems(className, whereCondition, orderBy);

            return itemsDS;
            #region Commented Caching Code
            //DataSet items = GetData(className);
            //DataSet data = new DataSet();

            //if (!DataHelper.DataSourceIsEmpty(items))
            //{
            //    data = items.Copy();
            //    if (IsFilterRequired(whereCondition, orderBy))
            //    {

            //        data.Tables[0].Clear();
            //        DataRow[] filteredData;
            //        DataTable itemTable = new DataTable();
            //        filteredData = items.Tables[0].Select(whereCondition, orderBy);
            //        foreach (DataRow row in filteredData)
            //        {
            //            data.Tables[0].ImportRow(row);
            //        }
            //    }
            //    items.Dispose();
            //}

            //return data;
            #endregion
        }

        private static bool IsFilterRequired(string whereCondition, string orderBy)
        {
            return (whereCondition != string.Empty || orderBy != string.Empty);
        }



        /// <summary>
        /// Returns Selected columns of Customtable.
        /// </summary>
        /// <param name="className">name of the custom table</param>
        /// <returns>List of selected columns if any.</returns>
        public static List<string> GetCustomTableSelectedColumns(string className)
        {
            DataClassInfo dci = GetCustomTableClassInfo(className);
            string columns = dci.ClassShowColumns ?? string.Empty;

            return columns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        #region Private Methods

        private static CustomTableItem fillCustomTableItem(CustomTableItem content, IDictionary<string, object> tableData)
        {
            foreach (KeyValuePair<string, object> entry in tableData)
            {
                string fieldName = entry.Key.ToString();
                object fieldValue = entry.Value;

                if (fieldName.ToLower().Equals("itemid")) continue;
                if (fieldName.ToLower().Equals("itemguid")) continue;

                content.SetValue(fieldName, fieldValue);
            }
            return content;
        }

        #endregion

        /// <summary>
        /// gets list of custom table items
        /// </summary>
        /// <param name="className">ClassName of the custom table which will be search.</param>
        /// <param name="whereCondition"> specific criteria if any</param>
        /// <returns>list of custom table items</returns>
        public static List<CustomTableItem> GetCustomTableItemsByCriteria(string className, string whereCondition)
        {
            CustomTableItemProvider ctProvider = new CustomTableItemProvider();
            DataSet ds = GetCustomTableItemsByCondition(className, whereCondition, string.Empty);
            List<CustomTableItem> customTableItems = new List<CustomTableItem>();
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    customTableItems.Add(dr.ToCustomTableItem(className));
                }
            }
            return customTableItems;
        }


        /// <summary>
        /// Method to get all the columns of custom table by its class name.
        /// </summary>
        /// <param name="className">Class Name for the custom table.</param>
        /// <returns>List containing all the columns of custom table</returns>
        public static List<string> GetCustomTableColumns(string className)
        {
            FormInfo fi = new FormInfo(GetCustomTableClassInfo(className).ClassFormDefinition);
            return fi.GetColumnNames();
        }


        /// <summary>
        /// method to check if status (isActive) column available in the custom table column list.
        /// </summary>
        /// <param name="className">Class Name for the custom table.</param>
        /// <returns>true if status (isActive) column available in the custom table column list</returns>
        public static bool HasStatusField(string className)
        {
            return GetCustomTableColumns(className).Any(x => x.ToLower() == Constants.CUSTOM_TABLE_STATUS_COLUMNNAME.ToLower());
        }

        /// <summary>
        /// Extended Method to convert datarow to custom table item.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="className">Class Name for the custom table.</param>
        /// <returns></returns>
        public static CustomTableItem ToCustomTableItem(this DataRow dr, string className)
        {
            CustomTableItem content;
            content = CustomTableItem.New(className, dr);
            return content;
        }

        /// <summary>
        /// method to update multiple customtableItems with common data.
        /// </summary>
        /// <param name="items">list of Custom table Items</param>
        /// <param name="tableData">Dictionary object having the data with which each custom table item will be updated.</param>
        /// <returns>true if all the custom table items updated successfully</returns>
        public static bool UpdateCustomTableItems(List<CustomTableItem> items, IDictionary<string, object> tableData)
        {
            foreach (CustomTableItem item in items)
            {
                CustomTableItem content = fillCustomTableItem(item, tableData);
                content.Update();
            }

            return true;
        }

        public static CustomTableItem New(string className)
        {
            return CustomTableItem.New(className);
        }

    }
}
