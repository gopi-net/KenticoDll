using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CMS.Helpers;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public class EmergeRegexHelper
    {
        public static Regex GetRegex(string pattern)
        {
            return RegexHelper.GetRegex(pattern);
        }

    }
}
