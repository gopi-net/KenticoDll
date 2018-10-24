using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public static class EmergeRequestHelper
    {
        public static bool IsPostBack()
        {
            return RequestHelper.IsPostBack();
        }
    }
}
