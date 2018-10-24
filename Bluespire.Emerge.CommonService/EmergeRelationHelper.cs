using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using System.Data;
using Bluespire.Emerge.Common.Relations;
using CMS.SiteProvider;
using CMS.FormEngine;
using System.Globalization;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.DataEngine;
namespace Bluespire.Emerge.CommonService
{
    /// <summary>
    /// Helper class to handle all the custom table relationship manager activity
    /// </summary>
    public static class EmergeRelationHelper
    {
        static string[] EMERGE_DATEFORMATS = {"MM/dd/yyyy hh:mm:ss tt", "MM/d/yyyy hh:mm:ss tt", "M/dd/yyyy hh:mm:ss tt",
                                            "M/d/yyyy hh:mm:ss tt","M/d/yyyy h:mm:ss tt", "M/d/yyyy h:mm tt", 
                   "MM/dd/yyyy hh:mm:ss", "M/d/yyyy h:mm:ss", 
                   "M/d/yyyy hh:mm tt", "M/d/yyyy hh tt", 
                   "M/d/yyyy h:mm", "M/d/yyyy h:mm", 
                   "MM/dd/yyyy hh:mm", "M/dd/yyyy hh:mm"};
        /// <summary>
        /// check tablename and columnname exist in custom table relationship master table and if exist then return primary table value
        /// </summary>
        /// <param name="tableName">Foreign table name</param>
        /// <param name="columnName">Foreign relation column name</param>
        /// <param name="columnValue">data value in foreign column</param>
        /// <returns></returns>
        public static string GetRelationColumnValue(string tableName, string columnName, string columnValue)
        {
            string returnValue = string.Empty;
            string where = Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE + " = '" + tableName + "' AND " + Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE_COLUMN + "='" + columnName + "' ";
            //string where = string.Empty;
            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.CUSTOM_TABLE_RELATION_MASTER), where, string.Empty);

            if (ds != null && !DataHelper.DataSourceIsEmpty(ds))
            {

                string primaryTable = Convert.ToString(ds.Tables[0].Rows[0][Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE]);
                string primarycolumn = Convert.ToString(ds.Tables[0].Rows[0][Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_COLUMN]);
                string displayColumn = Convert.ToString(ds.Tables[0].Rows[0][Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_DISPLAY_COLUMNS]);

                returnValue = GetPrimaryTableValue(primaryTable, primarycolumn, displayColumn, columnValue);



            }



            return returnValue;
        }
        /// <summary>
        /// Get primary table actual display value
        /// </summary>
        /// <param name="tableName">primary table name</param>
        /// <param name="primaryColumn">primary key require to use in relation table</param>
        /// <param name="displayColumn">return value of display column</param>
        /// <param name="columnValue">actual primary key value use for where condion</param>
        /// <returns></returns>
        public static string GetPrimaryTableValue(string tableName, string primaryColumn, string displayColumn, string columnValue)
        {
            string returnValue = string.Empty;
            if (!string.IsNullOrEmpty(columnValue))
            {
                string[] values = columnValue.Split(Constants.MULTI_VALUE_SEPERATOR);
                foreach (string item in values)
                {
                    string where = primaryColumn + " = '" + item + "' ";

                    DataSet ds = CustomTableItemProvider.GetItems(tableName, where, string.Empty);
                    if (ds != null && !DataHelper.DataSourceIsEmpty(ds))
                    {
                        string[] displayColumns = displayColumn.Split(',');
                        string resultValue = string.Empty;
                        for (int i = 0; i < displayColumns.Length; i++)
                        {
                            if (ds.Tables[0].Columns.Contains(displayColumns[i].Trim()))
                            {

                                string combineValue = string.Empty;

                                if (displayColumns.Length > 1)
                                {
                                    FormInfo mFormInfo = FormHelper.GetFormInfo(tableName, true);
                                    combineValue = mFormInfo.GetFormField(displayColumns[i]).Caption + " = " + Convert.ToString(ds.Tables[0].Rows[0][displayColumns[i]]);
                                }
                                else
                                {
                                    combineValue = Convert.ToString(ds.Tables[0].Rows[0][displayColumns[i]]);
                                }

                                if (string.IsNullOrEmpty(resultValue))
                                    resultValue = combineValue;
                                else
                                    resultValue = resultValue + ", " + combineValue;
                            }
                        }
                        //string resultValue=disp
                        returnValue = resultValue + " " + Constants.MULTI_VALUE_SEPERATOR + " " + returnValue;
                    }
                }
                if (!string.IsNullOrEmpty(returnValue))
                    returnValue = returnValue.Remove(returnValue.LastIndexOf(Constants.MULTI_VALUE_SEPERATOR.ToString())).Trim();

                DateTime dat;

                if (DateTime.TryParseExact(returnValue, EMERGE_DATEFORMATS, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dat))
                {
                    returnValue = Convert.ToDateTime(returnValue).ToString(Constants.EMERGE_DATEFORMAT);
                }
            }
            if (string.IsNullOrEmpty(returnValue))
                return columnValue;
            return returnValue;
        }

        /// <summary>
        /// reterive primary table list associated with paramerter foreign table from custom relation table master
        /// </summary>
        /// <param name="foreignTable">foreign table name</param>
        /// <returns></returns>
        public static List<CustomTableRelationMaster> GetRelationByForeignTable(string foreignTable)
        {
            List<CustomTableRelationMaster> lstRelationMaster = null;
            string where = Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE + " = '" + foreignTable + "' ";

            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.CUSTOM_TABLE_RELATION_MASTER), where, string.Empty);
            lstRelationMaster = GetRelationMasterList(ds);
            return lstRelationMaster;
        }


        /// <summary>
        /// reterive foreign table list associated with paramerter primary table from custom relation table master
        /// </summary>
        /// <param name="foreignTable">foreign table name</param>
        /// <returns></returns>
        public static List<CustomTableRelationMaster> GetRelationByPrimaryTable(string primaryTable)
        {
            List<CustomTableRelationMaster> lstRelationMaster = null;
            string where = Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE + " = '" + primaryTable + "' ";

            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.CUSTOM_TABLE_RELATION_MASTER), where, string.Empty);
            lstRelationMaster = GetRelationMasterList(ds);
            return lstRelationMaster;
        }



        /// <summary>
        /// reterive Relation table list associated with paramerter primary table and foreign table from custom relation table master
        /// </summary>
        /// <param name="Custom Table Relation Master list">custom table relation master list</param>
        /// <returns></returns>
        public static List<CustomTableRelationMaster> GetRelationByPrimaryAndForeignTable(string primaryTable, string foreignTable)
        {
            List<CustomTableRelationMaster> lstRelationMaster = null;
            string where = Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE + " = '" + primaryTable + "' ";
            where += " AND " + Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE + " = '" + foreignTable + "' ";

            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.CUSTOM_TABLE_RELATION_MASTER), where, string.Empty);
            lstRelationMaster = GetRelationMasterList(ds);
            return lstRelationMaster;
        }

        private static List<CustomTableRelationMaster> GetRelationMasterList(DataSet ds)
        {
            List<CustomTableRelationMaster> lstRelationMaster = null;
            if (ds != null && !DataHelper.DataSourceIsEmpty(ds))
            {
                CustomTableRelationMaster relationMaster = null;
                lstRelationMaster = new List<CustomTableRelationMaster>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    relationMaster = new CustomTableRelationMaster();

                    relationMaster.PrimaryTableName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE]);
                    relationMaster.PrimaryPkColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_COLUMN]);
                    relationMaster.ForeignTableName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE]);
                    relationMaster.ForeignTableColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE_COLUMN]);
                    relationMaster.PrimaryDisplayColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_DISPLAY_COLUMNS]);
                    relationMaster.RelationName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_RELATION_NAME]);
                    if (row[Constants.CUSTOM_TABLE_RELATION_MASTER_SKIP_VALIDATION] == DBNull.Value)
                    {
                        relationMaster.SkipValidation = false;
                    }
                    else
                        relationMaster.SkipValidation = Convert.ToBoolean(row[Constants.CUSTOM_TABLE_RELATION_MASTER_SKIP_VALIDATION]);

                    lstRelationMaster.Add(relationMaster);
                }
            }
            return lstRelationMaster;
        }




        /// <summary>
        /// get relation details by relation name
        /// </summary>
        /// <param name="relationName">relation Name column value custom table relation manager</param>
        /// <param name="relationValue">actual item value</param>
        /// <returns>return dataset value</returns>
        public static DataSet GetForeignTableData(string relationName, string relationValue)
        {
            return GetForeignTableData(relationName, relationValue, string.Empty, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relationName">relation name in custom table relation master</param>
        /// <param name="relationValue">actual item value</param>
        /// <param name="primarytable">primary table column value in custom table relation master</param>
        /// <returns>relation table list</returns>
        public static DataSet GetForeignTableData(string relationName, string relationValue, string primarytable)
        {
            return GetForeignTableData(relationName, relationValue, primarytable, string.Empty);
        }

        /// <summary>
        /// get relation details by relation name
        /// </summary>
        /// <param name="wherecondition">where condtion contain relation name/primarytable/foreign table</param>
        /// <returns></returns>
        public static DataSet GetForeignTableData(string relationName, string relationValue, string primarytable, string foreigntable)
        {
            List<CustomTableRelationMaster> lstRelationMaster = null;
            DataSet result = new DataSet();
            string where = string.Empty;
            if (!string.IsNullOrEmpty(relationName))
            {
                where = Constants.CUSTOM_TABLE_RELATION_MASTER_RELATION_NAME + " = '" + relationName + "' ";
            }

            if (!string.IsNullOrEmpty(primarytable))
            {
                if (!string.IsNullOrEmpty(where))
                {
                    where += " AND ";
                }
                where += Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE + " = '" + primarytable + "' ";
            }

            if (!string.IsNullOrEmpty(foreigntable))
            {
                if (!string.IsNullOrEmpty(where))
                {
                    where += " AND ";
                }
                where += Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE + " = '" + foreigntable + "' ";
            }

            DataSet ds = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.CUSTOM_TABLE_RELATION_MASTER), where, string.Empty);

            if (ds != null && !DataHelper.DataSourceIsEmpty(ds))
            {
                CustomTableRelationMaster relationMaster = null;
                lstRelationMaster = new List<CustomTableRelationMaster>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    relationMaster = new CustomTableRelationMaster();
                    relationMaster.PrimaryTableName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_TABLE]);
                    relationMaster.PrimaryPkColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_COLUMN]);
                    relationMaster.ForeignTableName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE]);
                    relationMaster.ForeignTableColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_FOREIGN_TABLE_COLUMN]);
                    relationMaster.PrimaryDisplayColumnName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_PRIMARY_DISPLAY_COLUMNS]);
                    relationMaster.RelationName = Convert.ToString(row[Constants.CUSTOM_TABLE_RELATION_MASTER_RELATION_NAME]);
                    if (row[Constants.CUSTOM_TABLE_RELATION_MASTER_SKIP_VALIDATION] == DBNull.Value)
                    {
                        relationMaster.SkipValidation = false;
                    }
                    else
                        relationMaster.SkipValidation = Convert.ToBoolean(row[Constants.CUSTOM_TABLE_RELATION_MASTER_SKIP_VALIDATION]);

                    lstRelationMaster.Add(relationMaster);

                }
                foreach (CustomTableRelationMaster rel in lstRelationMaster)
                {
                    where = " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + rel.ForeignTableColumnName + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + relationValue + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                    where += " AND " + Constants.CUSTOM_TABLE_STATUS_COLUMNNAME + " = 1 ";

                    result = CustomTableItemProvider.GetItems(rel.ForeignTableName, where, string.Empty);
                    result = GetRelationShipData(result, rel.ForeignTableName);
                    break;
                }
            }

            return result;
        }




        /// <summary>
        /// Reterive relationship data from CustomTableRelationMaster table
        /// </summary>
        /// <param name="ds">grid view dataset</param>
        /// <param name="CustomTableClassName">Custom table code name(class name)</param>
        /// <returns>dataset with relational data</returns>
        public static DataSet GetRelationShipData(DataSet ds, string CustomTableClassName)
        {
            if (!string.IsNullOrEmpty(CustomTableClassName))
            {
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    FormInfo mFormInfo = FormHelper.GetFormInfo(CustomTableClassName, false);
                    DataSet data = ds.Clone();
                    foreach (DataColumn column in data.Tables[0].Columns)
                    {
                        FormFieldInfo ffi = mFormInfo.GetFormField(column.ColumnName);
                        if (!ffi.System && ffi.DataType == FieldDataType.DateTime)
                        {
                            column.DataType = typeof(string);
                        }
                    }

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        data.Tables[0].ImportRow(row);
                    }
                    data.AcceptChanges();

                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        FormFieldInfo ffi = mFormInfo.GetFormField(column.ColumnName);
                        if (!ffi.System && ffi.DataType == FieldDataType.DateTime)
                        {
                            foreach (DataRow dataRecord in data.Tables[0].Rows)
                            {
                                if (!Convert.IsDBNull(dataRecord[column.ColumnName]))
                                    dataRecord[column.ColumnName] = Convert.ToDateTime(dataRecord[column.ColumnName]).ToString(Constants.EMERGE_DATEFORMAT);
                            }
                        }

                    }
                    data.AcceptChanges();

                    List<CustomTableRelationMaster> lstRelationMaster = EmergeRelationHelper.GetRelationByForeignTable(CustomTableClassName);

                    if (data != null && !DataHelper.DataSourceIsEmpty(data) && lstRelationMaster != null && lstRelationMaster.Count > 0)
                    {
                        DataSet dsCloned = data.Clone();
                        foreach (var relationMaster in lstRelationMaster)
                        {
                            if (dsCloned.Tables[0].Columns.Contains(relationMaster.ForeignTableColumnName))
                                dsCloned.Tables[0].Columns[relationMaster.ForeignTableColumnName].DataType = typeof(String);
                        }
                        foreach (DataRow row in data.Tables[0].Rows)
                        {
                            dsCloned.Tables[0].ImportRow(row);
                        }

                        for (int i = 0; i < dsCloned.Tables[0].Rows.Count; i++)
                        {

                            foreach (var relationMaster in lstRelationMaster)
                            {
                                if (dsCloned.Tables[0].Columns.Contains(relationMaster.ForeignTableColumnName))
                                {
                                    string columnValue = Convert.ToString(dsCloned.Tables[0].Rows[i][relationMaster.ForeignTableColumnName]);
                                    if (!string.IsNullOrEmpty(columnValue))
                                    {
                                        string actualValue = EmergeRelationHelper.GetPrimaryTableValue(relationMaster.PrimaryTableName, relationMaster.PrimaryPkColumnName, relationMaster.PrimaryDisplayColumnName, columnValue);

                                        dsCloned.Tables[0].Rows[i][relationMaster.ForeignTableColumnName] = actualValue;

                                    }
                                }

                            }
                        }
                        dsCloned.AcceptChanges();
                        return dsCloned;
                    }
                    return data;
                }
            }
            return ds;
        }



        /// <summary>
        /// Check table relation master if relation exist in relation master and is action feasible.
        /// SkipValidation in Relation table must be false.
        /// </summary>
        /// <param name="item">Custom table item value</param>
        /// <param name="action">Action enum</param>
        /// <returns>true if Action is feasible</returns>
        /// <exception cref="ActionNotFeasibleException"> Thrown if  Action not feasible.</exception>
        public static bool IsActionFeasible(CustomTableItem item, Constants.GridActions action)
        {
            string tablename = item.ClassName;
            List<CustomTableRelationMaster> relations;
            relations = action == Constants.GridActions.Activate ? GetRelationByForeignTable(tablename) : GetRelationByPrimaryTable(tablename);



            if (relations == null || relations.Count == 0) return true;

            foreach (CustomTableRelationMaster rel in relations)
            {

                if (!rel.SkipValidation.HasValue)
                {
                    rel.SkipValidation = false;
                }
                if (!(bool)rel.SkipValidation)
                {

                    string tableClassName = action == Constants.GridActions.Activate ? rel.PrimaryTableName : rel.ForeignTableName;
                    if (IsRelationalKeyDataExits(item, action, rel))
                    {
                        DataSet parentDS = CustomTableItemProvider.GetItems(tableClassName, GetWhereCondition(item, action, rel, tableClassName), string.Empty);

                        if (parentDS.Tables[0].Rows.Count != 0)
                        {
                            DataClassInfo dcp = DataClassInfoProvider.GetDataClassInfo(tableClassName);
                            throw new ActionNotFeasibleException(String.Format(ResHelper.GetString(String.Format("Emerge.Exception.ErrorMessage.{0}ActionNotFeasibleException", action.ToString())), dcp.ClassDisplayName));
                        }
                    }
                }
            }

            return true;

        }

        private static bool IsRelationalKeyDataExits(CustomTableItem item, Constants.GridActions action, CustomTableRelationMaster rel)
        {
            if (action == Constants.GridActions.Activate)
                return (item[rel.ForeignTableColumnName] == null || item[rel.ForeignTableColumnName].ToString().Trim() == string.Empty) ? false : true;
            else
                return (item[rel.PrimaryPkColumnName] == null || item[rel.PrimaryPkColumnName].ToString().Trim() == string.Empty) ? false : true;
        }

        /// <summary>
        /// method to get where condition.
        /// </summary>
        /// <param name="item">Custom Table Item.</param>
        /// <param name="action">Grid Action</param>
        /// <param name="rel">CustomTableRelationMaster object</param>
        /// <returns>string containing Where condtion.</returns>
        private static string GetWhereCondition(CustomTableItem item, Constants.GridActions action, CustomTableRelationMaster rel, string tableClassName)
        {
            string where = string.Empty;

            if (action == Constants.GridActions.Activate)
            {

                string[] primaryItemIDs = Convert.ToString(item[rel.ForeignTableColumnName]).Split(Constants.MULTI_VALUE_SEPERATOR);
                foreach (string primaryitemID in primaryItemIDs)
                {
                    where += rel.PrimaryPkColumnName + Constants.WHERE_CONDITION_OPERATOR_EQUAL + primaryitemID;
                    where += Constants.WHERE_CONDITION_OPERATOR_OR;
                }

                where = where.EndsWith(Constants.WHERE_CONDITION_OPERATOR_OR) ? where.Substring(0, where.LastIndexOf(Constants.WHERE_CONDITION_OPERATOR_OR) + 1) : where;

                if (!String.IsNullOrWhiteSpace(where))
                {
                    where = " ( " + where + " ) ";
                }
                if (CustomTableDataHelper.HasStatusField(tableClassName))
                    where += Constants.WHERE_CONDITION_OPERATOR_AND + Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_INACTIVE_STATUS;
            }
            else
            {
                where = " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + rel.ForeignTableColumnName + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + item[rel.PrimaryPkColumnName].ToString() + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                if (CustomTableDataHelper.HasStatusField(tableClassName))
                    where += Constants.WHERE_CONDITION_OPERATOR_AND + Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
            }

            return where;
        }


        /// <summary>
        /// method to return dictinary instance containing, all relational data available in Primary tables (for given foreign table Name)
        /// </summary>
        /// <param name="ForeignTableName"></param>
        /// <param name="properties"></param>
        /// <returns>dictinary instance containing, all relational data</returns>
        public static Dictionary<string, object> GetRelationshipDataByForeignTable(string ForeignTableName, Dictionary<string, object> properties )
        {
            List<CustomTableRelationMaster> relations = GetRelationByForeignTable(ForeignTableName);
            Dictionary<string, object> relationData = new Dictionary<string, object>();

            foreach (CustomTableRelationMaster relation in relations)
            {
                if (null != properties[relation.ForeignTableColumnName])
                {
                    List<string> displayColumns = GetDisplayColumns(relation);

                    foreach (string displayColumn in displayColumns)
                    {
                        string primaryTableValue = GetPrimaryTableValue(relation.PrimaryTableName, relation.PrimaryPkColumnName, displayColumn, properties[relation.ForeignTableColumnName].ToString());

                        if (relationData.ContainsKey(displayColumn))
                            relationData[displayColumn] = primaryTableValue;
                        else
                            relationData.Add(displayColumn, primaryTableValue);
                    }
                }

            }

            return relationData;

        }

        private static List<string> GetDisplayColumns(CustomTableRelationMaster relation)
        {
            List<string> displayColumns = new List<string>();

            if (relation.PrimaryDisplayColumnName.Split(new char[] { Constants.COMMA_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
            {
                displayColumns = relation.PrimaryDisplayColumnName.Split(new char[] { Constants.COMMA_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }

            if (displayColumns.Count == 0)
            {
                displayColumns.Add(relation.ForeignTableColumnName);
            }
            return displayColumns;
        }
    }
}

