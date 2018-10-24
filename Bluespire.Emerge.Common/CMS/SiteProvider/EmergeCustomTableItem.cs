using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;
using System.Data;
using CMS.CustomTables;

namespace Bluespire.Emerge.Common.CMS.SiteProvider
{
    
    public class EmergeCustomTableItem
    {
        CustomTableItem item;

        public EmergeCustomTableItem(CustomTableItem customTableItem)
        {
            if (customTableItem == null)
                throw new Exception("Custom Table item cannot be null.");
            item = customTableItem;
        }

        public bool SetValue(string columnName, object value)
        {
            return this.item.SetValue(columnName, value);
        }

        public void Update()
        {
            this.item.Update();
        }

        public bool Delete()
        {
            return this.item.Delete();
        }

        public object GetValue(string columnName)
        {
            return this.item.GetValue(columnName);
        }

        public List<string> ColumnNames
        {
            get
            {
                return this.item.ColumnNames;
            }
        }
        public static EmergeCustomTableItem New(DataRow dr, string className)
        {
            CustomTableItem content = CustomTableItem.New(className, dr);

            return new EmergeCustomTableItem(content);

        }
    }
}
