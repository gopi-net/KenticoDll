using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.SiteProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.CustomTables;
using CMS.Membership;

namespace Bluespire.Emerge.Common.CMS.SiteProvider
{
    public class EmergeCustomTableItemProvider
    {
        
        public EmergeCustomTableItemProvider(UserInfo userInfo)
        {
        }

        public EmergeCustomTableItem GetItem(int itemId, string className)
        {
            CustomTableItem item = CustomTableItemProvider.GetItem(itemId, className);
            if (item == null)
            {
                EmergeLogWriter.WriteError("CustomTableDataHelper:DeleteCustomTableItem", EventCode.EMERGE_GET, string.Format(EmergeResHelper.GetString(Constants.CUSTOMTABLEITEMDOESNOTEXISTS), itemId, className));
                throw new CustomTableItemNotFoundException(string.Format(EmergeResHelper.GetString(Constants.CUSTOMTABLEITEMDOESNOTEXISTS), itemId, className));
            }
            return new EmergeCustomTableItem(item);
        }

        public DataSet GetItems(string className, string where, string orderBy)
        {
            return CustomTableItemProvider.GetItems(className, where, orderBy);
        }
    }
}
