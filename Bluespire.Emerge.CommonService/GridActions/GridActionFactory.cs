using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// Class to Get the object based on action name.
    /// </summary>
    public class GridActionFactory
    {
        /// <summary>
        /// Returns back the object based on action name.
        /// </summary>
        /// <param name="actionName">name of the action being called on the grid.</param>
        public static IGridAction GetActionObject(string actionName, Dictionary<string, Func<IGridAction>> gridActions)
        {
            IGridAction gridAction;
            if (gridActions.ContainsKey(actionName))
            {
                var gridActionInstance = gridActions[actionName];
                gridAction = gridActionInstance();
            }
            else
                throw new GridActionNotDefinedExcetpion(String.Format(ResHelper.GetString("Emerge.Exception.ErrorMessage.GridActionNotDefinedExcetpion"), actionName));

            return gridAction;
        }
    }
}
