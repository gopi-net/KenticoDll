using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Web
{

    public delegate object OnExternalDataBoundHandler(object sender, string sourceName, object parameter);
    public delegate void OnLoadColumnsHandler();
    public delegate void OnBeforeDataReloadHandler();
    public delegate string OnBeforeFilteringHandler(string whereCondition);
    public delegate void OnBeforeSortingHandler(object sender, EventArgs e);
    public delegate void OnAfterDataReloadHandler();
    public delegate DataSet OnAfterRetrieveDataHandler(DataSet ds);    

}
