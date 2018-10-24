using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
namespace Bluespire.Emerge.CommonService.Caching
{
    public class EmergeCustomTable : ICacheable
    {
        public string Key
        { get; set; }

        public DataSet GetData()
        {
            return CustomTableDataHelper.GetCustomTableItemsByCondition(Key, string.Empty, string.Empty);
        }
    }
}
