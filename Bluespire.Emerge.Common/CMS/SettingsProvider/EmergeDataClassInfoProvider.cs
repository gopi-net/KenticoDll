using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DataEngine;

namespace Bluespire.Emerge.Common.CMS.SettingsProvider
{
    public static class EmergeDataClassInfoProvider
    {
        public static EmergeDataClassInfo GetDataClass(string className)
        {
            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(className);
            if (dci == null)
            {
                EmergeLogWriter.WriteError("CustomTableDataHelper:GetCustomTableClassInfo", EventCode.EMERGE_GET, string.Format(EmergeResHelper.GetString(Constants.CUSTOMTABLEDOESNOTEXISTS), className));
                throw new CustomTableNotExistsException(string.Format(Constants.CUSTOMTABLEDOESNOTEXISTS, className));
            }
            return new EmergeDataClassInfo(dci);
        }
    }
}
