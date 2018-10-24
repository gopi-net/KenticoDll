using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Xml;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.GenericParsing
{
    public class ExcelParser
    {
        private string e_strExcelProviderName;
        private bool e_blnHeaderPresentInExcel;
        private string e_strExcelExtendedProperties;
        public void Load(bool headerPresentInExcel = true, string providerName = "Microsoft.ACE.OLEDB.12.0", string extendedProperties = "Excel 12.0 Xml")
        {
            e_strExcelProviderName = providerName;
            e_blnHeaderPresentInExcel = headerPresentInExcel;
            e_strExcelExtendedProperties = extendedProperties + ";HDR=" + (e_blnHeaderPresentInExcel ? "Yes" : "No");
        }

        /// <summary>
        ///   Generates a <see cref="XmlDocument"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="xmlPathColumnMappings">Path of XML File that contains the column mappings</param>

        public XmlDocument GetXml(string filePath, string sheetName, string xmlPathColumnMappings)
        {
            return GetXml(filePath, sheetName, GetDataTableFromXML(xmlPathColumnMappings));
        }

        /// <summary>
        ///   Generates a <see cref="XmlDocument"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        public XmlDocument GetXml(string filePath, string sheetName)
        {
            return GetXml(filePath, sheetName, new DataTable());
        }
        /// <summary>
        ///   Generates a <see cref="XmlDocument"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="columnMappings">Two cloumn DataTable containing column mappings. First column contains existing column names. Second column contains new column names. </param>
        public XmlDocument GetXml(string filePath, string sheetName, DataTable columnMappings)
        {

            DataSet dsData;
            XmlDocument xmlDocument = null;

            dsData = this.GetDataSet(filePath, sheetName, columnMappings);

            if (dsData != null)
            {
                xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(dsData.GetXml());
            }

            return xmlDocument;

        }

        /// <summary>
        ///   Generates a <see cref="DataSet"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="xmlPathColumnMappings">Path of XML File that contains the column mappings</param>
        public DataSet GetDataSet(string filePath, string sheetName, string xmlPathColumnMappings)
        {
            return GetDataSet(filePath, sheetName, GetDataTableFromXML(xmlPathColumnMappings));
        }
        /// <summary>
        ///   Generates a <see cref="DataSet"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        public DataSet GetDataSet(string filePath, string sheetName)
        {
            return GetDataSet(filePath, sheetName, new DataTable());
        }
        /// <summary>
        ///   Generates a <see cref="DataSet"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="columnMappings">Two cloumn DataTable containing column mappings. First column contains existing column names. Second column contains new column names. </param>
        public DataSet GetDataSet(string filePath, string sheetName, DataTable columnMappings)
        {
            DataTable dtData;
            DataSet dsData = null;

            dtData = this.GetDataTable(filePath, sheetName, columnMappings);

            if (dtData != null)
            {
                dsData = new DataSet();
                dsData.Tables.Add(dtData);
            }

            return dsData;
        }

        /// <summary>
        ///   Generates a <see cref="DataTable"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="xmlPathColumnMappings">Path of XML File that contains the column mappings</param>
        public DataTable GetDataTable(string filePath, string sheetName, string xmlPathColumnMappings)
        {
            return GetDataTable(filePath, sheetName, GetDataTableFromXML(xmlPathColumnMappings));
        }
        /// <summary>
        ///   Generates a <see cref="DataTable"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        public DataTable GetDataTable(string filePath, string sheetName)
        {
            return GetDataTable(filePath, sheetName, new DataTable());
        }
        /// <summary>
        ///   Generates a <see cref="DataTable"/> based on the data stored within
        ///   the Excel Sheet data source after it was parsed.
        /// </summary>
        /// <param name="filePath">Complete path of Excel File</param>
        /// <param name="sheetName">Excel Sheet Name</param>
        /// <param name="columnMappings">Two cloumn DataTable containing column mappings. First column contains existing column names. Second column contains new column names. </param>
        public DataTable GetDataTable(string filePath, string sheetName, DataTable columnMappings)
        {
            DataTable dt = ReadExcelData(filePath, sheetName);
            dt = UpdateColumnNames(dt, columnMappings);
            return dt;
        }

        private DataTable ReadExcelData(string filePath, string sheetName)
        {
            OleDbDataAdapter excelDataAdapter = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            string connectionStringforExcel;
            connectionStringforExcel = @"Provider=" + e_strExcelProviderName + ";Data Source=" + filePath + ";Extended Properties='" + e_strExcelExtendedProperties + "'";
            using (OleDbConnection connection = new OleDbConnection(connectionStringforExcel))
            {
                connection.Open();
                if (WorksheetExists(sheetName, connection))
                {
                    OleDbCommand command = new OleDbCommand("select * from [" + sheetName + "]", connection);
                    excelDataAdapter.SelectCommand = command;
                    excelDataAdapter.Fill(dt);
                }
                else
                {
                    EmergeLogWriter.WriteError("Method Name: ExcelParser.ReadExcelData", EventCode.EMERGE_PAGENOTFOUND, ResHelper.GetStringFormat("Emerge.Rates.RatesSheetNotFound"));
                    throw new RatesSheetNotFoundException(string.Format("Sheet '{0}' not found in Uploaded Excel Workbook", sheetName.Substring(0, sheetName.LastIndexOf('$'))));
                }
            }
            return dt;
        }

        private DataTable UpdateColumnNames(DataTable dt, DataTable columnMappings)
        {
            if (columnMappings != null && dt != null)
            {
                foreach (DataRow row in columnMappings.Rows)
                {
                    try
                    {
                        dt.Columns[row[0].ToString()].ColumnName = row[1].ToString();
                    }
                    catch (Exception ex)
                    {
                        EmergeLogWriter.WriteError("Method Name: ExcelParser.UpdateColumnNames", EventCode.EMERGE_UNHANDLED, ResHelper.GetStringFormat("Invalid Column Mapping"));
                        throw new RatesIncorrectMappingsException("Invalid Column Mapping");
                    }
                }

            }
            return dt;
        }

        private List<string> GetSheetsInExcel(OleDbConnection connection)
        {
            List<string> listSheet = new List<string>();
            DataTable dtSheets = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            listSheet = GetSheetsInFile(dtSheets);
            return listSheet;
        }

        private bool WorksheetExists(string sheetName, OleDbConnection connection)
        {
            List<string> listSheet = new List<string>();
            listSheet = GetSheetsInExcel(connection);
            return listSheet.Count > 0 && listSheet.Contains(sheetName);
        }

        private List<string> GetSheetsInFile(DataTable dtSheets)
        {
            const string TableName = "TABLE_NAME";
            List<string> listSheet = new List<string>();
            foreach (DataRow drSheet in dtSheets.Rows)
            {
                if (drSheet[TableName].ToString().Contains("$"))
                {
                    listSheet.Add(drSheet[TableName].ToString().Replace("'", ""));
                }
            }
            return listSheet;
        }

        private DataTable GetDataTableFromXML(string xmlPathColumnMappings)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPathColumnMappings);
            return ds.Tables[0];
        }
    }
}
