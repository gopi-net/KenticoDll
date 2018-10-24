using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// Class to handle move item up action.
    /// </summary>
    public class GridMoveUpAction : IGridMoveUpAction
    {
        #region IGridAction Members

        /// <summary>
        /// Moves the selected Item up by one.
        /// </summary>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        /// <param name="CustomTableClassName">Custom Table Class Name</param>
        /// <param name="AfterActionRedirectToUrl">If Set then after action processed, control will be redirect to this Url</param>
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem objItem = new CustomTableItem(CustomTableClassName);
                objItem.Generalized.MoveObjectUp();
            }

            if (AfterActionRedirectToUrl == null || AfterActionRedirectToUrl.Equals(string.Empty))
                URLHelper.Redirect(RequestContext.URL.ToString());
            else
                URLHelper.Redirect(AfterActionRedirectToUrl);
            return true;
        }


        public object BeforeAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public object AfterAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}
