using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;
namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// Class to handle Deactivation action for a record in Custom table.
    /// </summary>
    public class GridDeactivateAction : IGridDeactivateAction
    {
        #region IGridAction Members

        /// <summary>
        /// Processes Grid Deactivation action. Method updates the Status column of Custom table to false.
        /// </summary>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        /// <param name="CustomTableClassName">Custom Table Class Name</param>
        /// <param name="AfterActionRedirectToUrl">If Set then after action processed, control will be redirect to this Url</param>
        /// <exception cref="ActionNotFeasibleException"> Thrown if  Record trying to deactivate is in use.</exception>
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(ValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);


                if (item != null)
                {
                    if (EmergeRelationHelper.IsActionFeasible(item, Constants.GridActions.Deactivate))
                    {
                        item.SetValue(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME, "false");
                        item.Update();
                        
                        
                    }
                }

            }
            return true;

        }

        public object BeforeAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public object AfterAction(params object[] parameters)
        {
            URLHelper.Redirect(RequestContext.URL.ToString());
            return true;
        }

        #endregion
    }
}
