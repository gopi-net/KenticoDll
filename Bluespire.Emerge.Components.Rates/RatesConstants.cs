using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.Rates
{
    public static class RatesConstants
    {

        #region "Rates"
        public const string RATES_DASHBOARDPAGE = "RatesDashboardPage";
        public const string RATES_LISTPAGE = "RatesListPage";
        public const string RATES_DATAVIEWITEMPAGE = "RatesDataViewItemPage";
        public const string RATES_DATASELECTFIELDSPAGE = "RatesDataSelectFieldsPage";
        public const string RATES_DATALISTPAGE = "RatesDataListPage";
        public const string RATES_DATAEDITITEMPAGE = "RatesDataEditItemPage";
        public const string CUSTOMTABLE_CODENAME_FOR_RATES_DATA = "customtable.Emerge_{0}_RT_ImportedData";
        public const string CUSTOMTABLE_CODENAME_FOR_RATES_COLUMN_MAPPING = "customtable.Emerge_{0}_RT_ColumnMapping";
        public const string SOURCE_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING = "SourceColumnName";
        public const string TARGET_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING = "TargetColumnName";
        public const string FORMATTING_COLUMN_NAME_FOR_RATES_COLUMN_MAPPING = "FormattingPattern";
        public const string RATES_TEMPORARY_FILE_PATH = @"/CMSModules/CMS_EmergeCommon/TemporaryFiles/";
        public const string RATES_TEMPORARY_FILE_NAME_PREFIX = "RatesData";
        public const string ACTIVE_DATA_CONDITION = "IsActive = 1";
        public const string DEACTIVATION_COLUMN = "IsActive";
        public const string DEACTIVATION_COLUMN_VALUE = "False";
        public const string PRIMARY_KEY_COLUMN = "ItemID";
        public const string STRINGCODE_RATESHOME = "Emerge.RT.Dashboard";
        public const string PAGEURL_RATES_DASHBOARD = "~/CMSModules/CMS_Rates/Dashboard/Dashboard.aspx";
        #endregion "Rates"
    }
}
