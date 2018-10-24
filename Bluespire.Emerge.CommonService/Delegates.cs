using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.CommonService.GridActions;

namespace Bluespire.Emerge.CommonService
{
    public delegate bool OnBeforeAction(string actionName, object actionArgument, IGridAction actionObject);
    public delegate bool OnAfterAction(string actionName, object actionArgument, IGridAction actionObject);
}
