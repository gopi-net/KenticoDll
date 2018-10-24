using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// base interface for Grid Action interfaces.
    /// </summary>
    public interface IGridAction
    {
        /// <summary>
        /// ProcessAction Method Prototype.
        /// </summary>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        /// <param name="CustomTableClassName">Custom Table Class Name</param>
        /// <param name="AfterActionRedirectToUrl">If Set then after action processed, control will be redirect to this Url</param>
        bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl);

        /// <summary>
        /// Processes the action to be done before any grid action.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object BeforeAction(params object[] parameters);

        /// <summary>
        /// Processes the action to be done after any grid action.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object AfterAction(params object[] parameters);
    }
}
