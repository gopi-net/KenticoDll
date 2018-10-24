using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using CMS.SiteProvider;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// Class to handle activation action for a record in Custom table.
    /// </summary>
    public class GridActivateAction : IGridActivateAction
    {
        #region IGridAction Members

        /// <summary>
        /// Processes Grid activation action. Method updates the Status column of Custom table to true.
        /// </summary>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        /// <param name="CustomTableClassName">Custom Table Class Name</param>
        /// <param name="AfterActionRedirectToUrl">If Set then after action processed, control will be redirect to this Url</param>
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(ValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);

                if (item != null)
                {
                    if (EmergeRelationHelper.IsActionFeasible(item, Constants.GridActions.Activate))
                    {
                        item.SetValue(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME, "true");
                        item.Update();
                    }
                }
                if (AfterActionRedirectToUrl == null || AfterActionRedirectToUrl.Equals(string.Empty))
                    URLHelper.Redirect(RequestContext.URL.ToString());
                else
                    URLHelper.Redirect(AfterActionRedirectToUrl);
            }
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
