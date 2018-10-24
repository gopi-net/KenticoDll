using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager
{
    /// <summary>
    /// Entity Class declared for License.
    /// </summary>

    public class LicenseInfo
    {

        /// <summary>
        /// License Key.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// Expiration date.
        /// </summary>
        public DateTime? ExpirationDate
        {
            get;
            set;
        }

        /// <summary>
        /// Domain Name.
        /// </summary>
        public string DomainName
        {
            get;
            set;
        }

        /// <summary>
        /// Module Names.
        /// </summary>
        public List<string> ModuleNames
        {
            get;
            set;
        }

    }

}
