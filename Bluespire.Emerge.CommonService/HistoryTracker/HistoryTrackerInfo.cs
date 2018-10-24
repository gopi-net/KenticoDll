using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.HistoryTracker
{
    public class HistoryTrackerInfo
    {

       
        public string CustomTableClassName { get; set; }
        public string CustomTableDisplayName { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Description { get; set; }
        public string GroupGuid { get; set; }
        

        public int ItemID { get; set; }
        public enum OperationType { DELETE, UPDATE, INSERT }

        //public HistoryTrackerInfo(string _customTableClassName, string _customTableDisplayName, string _columnName, string _oldValue, string _newValue, string _description, string _groupGuid, int _itemID, int _operationBy, string _operationWhen)
        public HistoryTrackerInfo(string _customTableClassName, string _customTableDisplayName, string _columnName, string _oldValue, string _newValue, string _description, string _groupGuid, int _itemID)
        {
            CustomTableClassName =  _customTableClassName ;
            CustomTableDisplayName = _customTableDisplayName;
            ColumnName = _columnName;
            OldValue = _oldValue;
            NewValue = _newValue;

            Description = _description;
            GroupGuid = _groupGuid;
            ItemID = _itemID;
            
        }

    }
}
