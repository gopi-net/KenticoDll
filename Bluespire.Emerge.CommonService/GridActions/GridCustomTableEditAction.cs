using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;
using CMS.DataEngine;

namespace Bluespire.Emerge.CommonService.GridActions
{
    /// <summary>
    /// Class to handle Edit action for a Custom table. This action shows up all the records of a Selected Custom Table.
    /// </summary>
    public class GridCustomTableEditAction : IGridCustomTableEditAction
    {
        #region IGridAction Members

        /// <summary>
        /// Processes Edit action for a Custom table. This action shows up all the records of a Selected Custom Table.
        /// </summary>
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            int classId = ValidationHelper.GetInteger(actionArgument, 0);
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(classId);
            if (dci != null)
            {
                // Check if custom table class hasn't set specific listing page
                if (dci.ClassListPageURL != String.Empty)
                {
                    URLHelper.Redirect(dci.ClassListPageURL + "?customtableid=" + classId);
                }
                else
                {
                    if (AfterActionRedirectToUrl.Equals(string.Empty) || AfterActionRedirectToUrl == null)
                        throw new AfterEditActionRedirectToUrlNotFoundException("Redirect To Url missing For Custom Table Edit Action.");
                    URLHelper.Redirect(AfterActionRedirectToUrl +  "?customtableid=" + classId);
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
