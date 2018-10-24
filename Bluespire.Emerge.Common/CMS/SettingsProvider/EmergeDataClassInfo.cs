using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DataEngine;

namespace Bluespire.Emerge.Common.CMS.SettingsProvider
{
    public class EmergeDataClassInfo
    {
        private DataClassInfo dci;
        public EmergeDataClassInfo(DataClassInfo classInfo)
        {
            dci = classInfo;
        }

        public int ClassID
        {
            get
            {
                return this.dci.ClassID;
            }
            set
            {
                this.dci.ClassID = value;
            }
        }
    }
}
