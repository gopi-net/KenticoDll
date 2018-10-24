using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.GenericParsing;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.Helpers;

namespace Bluespire.Emerge.Components.Rates.Services
{
    public class RatesService : IRatesService
    {
        private void ArchivePreviousData()
        {
            string ratesDataTable = string.Format(RatesConstants.CUSTOMTABLE_CODENAME_FOR_RATES_DATA, SiteContext.CurrentSiteName);
            DataClassInfo customTable = CustomTableDataHelper.GetCustomTableClassInfo(ratesDataTable);
            if (customTable != null)
            {
                string where = RatesConstants.ACTIVE_DATA_CONDITION;
                int itemID = 0;
                DataSet customTableItems = CustomTableDataHelper.GetCustomTableItemsByCondition(ratesDataTable, where, null);
                if (!DataHelper.DataSourceIsEmpty(customTableItems))
                {
                    foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
                    {
                        IDictionary<string, object> tableData = new Dictionary<string, object>();
                        itemID = Convert.ToInt32(customTableItemDr[RatesConstants.PRIMARY_KEY_COLUMN].ToString());
                        tableData.Add(RatesConstants.DEACTIVATION_COLUMN, RatesConstants.DEACTIVATION_COLUMN_VALUE);
                        CustomTableDataHelper.SaveCustomTableItem(ratesDataTable, ref itemID, tableData);
                    }
                }
            }
        }
        private DataTable GetColumnMappings()
        {
            string mappingTable = string.Format(RatesConstants.CUSTOMTABLE_CODENAME_FOR_RATES_COLUMN_MAPPING, SiteContext.CurrentSiteName);
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(mappingTable, string.Empty, RatesConstants.PRIMARY_KEY_COLUMN);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Source");
                dt.Columns.Add("Target");
                dt.Columns.Add("Format");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row[RatesConstants.SOURCE_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING], row[RatesConstants.TARGET_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING], row[RatesConstants.FORMATTING_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING]);
                }
                return dt;
            }
            return new DataTable();
        }
        private void UploadData(DataTable dt)
        {
            string ratesDataTable = string.Format(RatesConstants.CUSTOMTABLE_CODENAME_FOR_RATES_DATA, SiteContext.CurrentSiteName);
            ArchivePreviousData();
            DataTable mappingTable = GetColumnMappings();
            foreach (DataRow row in dt.Rows)
            {
                int itemID = 0;
                IDictionary<string, object> tableData = new Dictionary<string, object>();
                foreach (DataRow mappingColumn in mappingTable.Rows)
                {
                    tableData.Add(mappingColumn[1].ToString(), row[mappingColumn[1].ToString()].ToString());
                }
                CustomTableDataHelper.SaveCustomTableItem(ratesDataTable, ref itemID, tableData);
            }
        }
        private DataTable ParseExcel(string serverFile, string sheetName)
        {
            ExcelParser parser = new ExcelParser();
            parser.Load();
            DataTable dt = GetColumnMappings();
            if (dt.Rows.Count > 0)
            {
                return parser.GetDataTable(serverFile, sheetName + "$", dt);
            }
            return parser.GetDataTable(serverFile, sheetName + "$");
        }
        

        public void UpdateCustomTable(string serverFile,string sheetName)
        {
            DataTable dt = ParseExcel(serverFile, sheetName);
            UploadData(dt);
            
        }

        public DataTable GetRatesList(string whereCondition)
        {
            string ratesDataTable = string.Format(RatesConstants.CUSTOMTABLE_CODENAME_FOR_RATES_DATA, SiteContext.CurrentSiteName);
            string filterString = string.Empty;
            if (whereCondition != string.Empty)
            {
                filterString = string.Format("{0} AND {1}", RatesConstants.ACTIVE_DATA_CONDITION, whereCondition);
            }
            else
            {
                filterString = RatesConstants.ACTIVE_DATA_CONDITION;
            }
            DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(ratesDataTable, filterString, RatesConstants.PRIMARY_KEY_COLUMN);
            if (!DataHelper.DataSourceIsEmpty(ds.Tables[0]))
                return ds.Tables[0];
            else
                return new DataTable();
        }
    }
}
