using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService.License;
using CMS.SiteProvider;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.GridActions
{
    public class GridDeleteLicenseAction : IGridDeleteAction
    {
        #region IGridAction Members

        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(ValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);

                if (item != null)
                {
                    item.Delete();
                    LicenseProvider.ClearCachedLicenseModules();
                    
                    URLHelper.Redirect(RequestContext.URL.ToString());
                }
            }
            return true;
        }

        #endregion

        public object BeforeAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public object AfterAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
