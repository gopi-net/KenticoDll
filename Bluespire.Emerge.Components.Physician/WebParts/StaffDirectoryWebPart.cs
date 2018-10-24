using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using System.Data;
using Bluespire.Emerge.CommonService;

namespace Bluespire.Emerge.Components.StaffDirectory.WebParts
{
    public class StaffDirectoryWebPart : EmergeBaseWebPart
    {
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.StaffDirectory;
            base.OnInit(e);
        }
        protected DataSet GetRelationTableData(string relationName, string itemid)
        {
            DataSet ds = new DataSet();
            ds = EmergeRelationHelper.GetForeignTableData(relationName, itemid, EmergeStaticHelper.SetSiteName(StaffDirectoryConstants.CUSTOMTABLE_CODENAME_SD_STAFF), string.Empty);
            return ds;
        }
    }
}
